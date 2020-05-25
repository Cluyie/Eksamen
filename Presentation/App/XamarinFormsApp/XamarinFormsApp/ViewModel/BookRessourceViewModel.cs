using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.AspNetCore.SignalR.Client;
using SkiaSharp;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Helpers.Graphish;
using XamarinFormsApp.Model;
using XamarinFormsApp.Properties;
using Profile = AutoMapper.Profile;

namespace XamarinFormsApp.ViewModel
{
    public class BookRessourceViewModel : Profile, IResource<Reservation<ReserveTime>, ReserveTime, AvailableTime>
    {
        public delegate void Reftesh(DayOfWeek? day = null);

        private readonly HubConnection _hubConnectionRerservation;
        private readonly HubConnection _hubConnectionResource;

        public BookRessourceViewModel()
        {
            proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            drawBookning = new DrawResourcer();
            Date = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;
            SlutTime = DateTime.Now.TimeOfDay;

            _hubConnectionRerservation = new HubConnectionBuilder()
                .WithUrl($"{Resources.SignalRBaseAddress}ReservationHub").Build();
            _hubConnectionRerservation.StartAsync();
            _hubConnectionResource =
                new HubConnectionBuilder().WithUrl($"{Resources.SignalRBaseAddress}ResourceHub").Build();
            _hubConnectionResource.StartAsync();

            //SignalR Client methods for Reservation
            _hubConnectionRerservation.On<Reservation<ReserveTime>>("CreateReservation", reservation =>
            {
                Reservations.Add(reservation);
                reftesh(reservation.Timeslot.FromDate.DayOfWeek);
            });
            _hubConnectionRerservation.On<Reservation<ReserveTime>>("UpdateReservation", reservation =>
            {
                Reservations[Reservations.FindIndex(r => r.Id == reservation.Id)] = reservation;
                reftesh();
            });
            _hubConnectionRerservation.On<Reservation<ReserveTime>>("DeleteReservation", reservation =>
            {
                Reservations.RemoveAt(Reservations.FindIndex(re => reservation.Id == re.Id));
                reftesh(reservation.Timeslot.FromDate.DayOfWeek);
            });

            //SignalR Client methods for Resource
            _hubConnectionResource.On<Resource>("UpdateResource", resource =>
            {
                if (resource.Id == Id)
                {
                    if (resource.TimeSlots == null)
                    {
                        TimeSlots = new List<AvailableTime>();
                        reftesh();
                    }

                    foreach (var timeslot in resource.TimeSlots)
                        if (TimeSlots.Find(r => r.Id == timeslot.Id) != null)
                        {
                            TimeSlots[TimeSlots.FindIndex(r => r.Id == timeslot.Id)] = timeslot;
                            reftesh();
                        }
                        else
                        {
                            TimeSlots.Add(timeslot);
                            reftesh();
                        }
                }
            });
        }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan SlutTime { get; set; }

        public ApiClientProxy proxy { get; set; }
        public DrawResourcer drawBookning { get; set; }

        public Reftesh reftesh { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AvailableTime> TimeSlots { get; set; }
        public List<Reservation<ReserveTime>> Reservations { get; set; }

        public void Reserver()
        {
            if (Application.Current.Properties.ContainsKey("UserData"))
            {
                var user = (User)Application.Current.Properties["UserData"];
                var Resevation = GetCorrentSelected();
                var availables = FindAllAvailableTime(Resevation.FromDate);
                var valid = false;
                foreach (var available in availables)
                    if (available.From.TimeOfDay <= Resevation.FromDate.TimeOfDay &&
                        available.To.TimeOfDay >= Resevation.ToDate.TimeOfDay)
                    {
                        valid = true;
                        break;
                    }

                if (!valid) return;
                var Resvertions = FindAllReservationer(Resevation.FromDate);
                foreach (IReservation<IReserveTime> resv in Resvertions)
                {
                    var reservation = resv.Timeslot;
                    {
                        //Mulig en bug
                        if (reservation.ToDate > Resevation.FromDate && reservation.FromDate < Resevation.ToDate)
                            valid = false;
                    }
                }

                if (!valid) return;
                IReservation<IReserveTime> res = new Reservation<IReserveTime>
                { UserId = user.Id, Timeslot = Resevation, ResourceId = Id };
                var responseMessage = proxy.Post("Reservation", res);
                if (responseMessage.IsSuccessStatusCode) _hubConnectionRerservation.SendAsync("CreateReservation", res);
                //Reservations.Add(res);
            }
        }

        public List<IAvailableTime> FindAllAvailableTime(DateTime date)
        {
            var availablesFound = new List<IAvailableTime>();
            foreach (IAvailableTime Available in TimeSlots)
            {
                if (date.DayOfYear == Available.From.DayOfYear) availablesFound.Add(Available);
                else if (Available.From.DayOfWeek == date.DayOfWeek)
                {
                    availablesFound.Add(Available);
                }
            }

            return availablesFound;
        }

        public List<IReservation<ReserveTime>> FindAllReservationer(DateTime date)
        {
            var FoundRes = new List<IReservation<ReserveTime>>();
            foreach (IReservation<ReserveTime> reservation in Reservations)
                if (date.DayOfYear == reservation.Timeslot.FromDate.DayOfYear)
                    FoundRes.Add(reservation);
            return FoundRes;
        }

        public ReserveTime GetCorrentSelected()
        {
            if (StartTime > SlutTime)
                return new ReserveTime
                {
                    FromDate = new DateTime(Date.Year, Date.Month, Date.Day, SlutTime.Hours, SlutTime.Minutes, 0),
                    ToDate = new DateTime(Date.Year, Date.Month, Date.Day, StartTime.Hours, StartTime.Minutes, 0)
                };
            return new ReserveTime
            {
                FromDate = new DateTime(Date.Year, Date.Month, Date.Day, StartTime.Hours, StartTime.Minutes, 0),
                ToDate = new DateTime(Date.Year, Date.Month, Date.Day, SlutTime.Hours, SlutTime.Minutes, 0)
            };
        }

        public void DrawDay(SKCanvas canvas, int wtidth, int height, DateTime date)
        {
            var FoundRes = FindAllReservationer(date);
            IUser user = new User { Id = Guid.Empty };
            if (Application.Current.Properties.ContainsKey("UserData"))
                user = (User)Application.Current.Properties["UserData"];
            if (date.DayOfYear == Date.DayOfYear && date.Year == Date.Year)
                FoundRes.Add(new Reservation<ReserveTime>
                { Id = Guid.Empty, UserId = user.Id, Timeslot = GetCorrentSelected(), ResourceId = Id });
            drawBookning.DrawDay(canvas, wtidth, height, FindAllAvailableTime(date), FoundRes, user.Id);
        }
    }
}