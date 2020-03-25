using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AutoMapper;
using Models.Interfaces;
using SkiaSharp;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Helpers.Graphish;
using System.Linq;
using XamarinFormsApp.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace XamarinFormsApp.ViewModel
{
    public class BookRessourceViewModel : AutoMapper.Profile, IResource<Reservation<ReserveTime>, ReserveTime, AvailableTime>
    {
        public delegate void Reftesh(DayOfWeek? day = null);

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AvailableTime> TimeSlots { get; set; }
        public List<Reservation<ReserveTime>> Reservations { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan SlutTime { get; set; }

        public ApiClientProxy proxy { get; set; }
        public DrawResourcer drawBookning { get; set; }

        public Reftesh reftesh { get; set; }

        private HubConnection _hubConnection;

        public BookRessourceViewModel()
        {
            proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            drawBookning = new DrawResourcer();
            Date = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;
            SlutTime = DateTime.Now.TimeOfDay;

            _hubConnection = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}ReservationHub").Build();
            Connect();

            //SignalR Client methods for Reservation
            _hubConnection.On<Reservation<ReserveTime>>("CreateReservation", (reservation) =>
            {
                Reservations.Add(reservation);
                reftesh(reservation.Timeslot.FromDate.DayOfWeek);
            });

            _hubConnection.On<Reservation<ReserveTime>>("UpdateReservation", (reservation) =>
            {
                Reservations[Reservations.FindIndex(r => r.Id == reservation.Id)] = reservation;
                reftesh();
            });

            _hubConnection.On<Reservation<ReserveTime>>("DeleteReservation", (reservation) =>
            {
                Reservations.Remove(reservation);
                reftesh(reservation.Timeslot.FromDate.DayOfWeek);
            });
        }
        public void Reserver()
        {
            if (Application.Current.Properties.ContainsKey("UserData"))
            {
                User user = (User)Application.Current.Properties["UserData"];
                ReserveTime Resevation = GetCorrentSelected();
                List<IAvailableTime> availables = FindAllAvailableTime(Resevation.FromDate);
                bool valid = true;
                foreach (IAvailableTime available in availables)
                {
                    if (available.From >= Resevation.FromDate && available.To <= Resevation.ToDate)
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    return;
                }
                List<IReservation<ReserveTime>> Resvertions = FindAllReservationer(Resevation.FromDate);
                foreach (IReservation<IReserveTime> resv in Resvertions)
                {
                    IReserveTime reservation = resv.Timeslot;
                    {
                        //Mulig en bug
                        if (reservation.ToDate > Resevation.FromDate && reservation.FromDate < Resevation.ToDate)
                        {
                            valid = false;
                        }
                    }
                }
                if (!valid)
                {
                    return;
                }
                Reservation<ReserveTime> res = new Reservation<ReserveTime>() { UserId = user.Id, Id = Guid.NewGuid(), Timeslot = Resevation };
                proxy.Post("Reservation/", res);
                Reservations.Add(res);
            }
        }
        public List<IAvailableTime> FindAllAvailableTime(DateTime date)
        {
            List<IAvailableTime> availablesFound = new List<IAvailableTime>();
            foreach (IAvailableTime Available in TimeSlots)
            {
                if (!Available.Available)
                {
                    continue;
                }
                if (Available.Recurring == null)
                {
                    if (date.DayOfYear == Available.From.DayOfYear)
                    {
                        availablesFound.Add(Available);
                    }
                }
                else if ((DayOfWeek)((Available.Recurring + 1) % 7) == date.DayOfWeek)
                {
                    availablesFound.Add(Available);
                }
            }
            return availablesFound;
        }
        public List<IReservation<ReserveTime>> FindAllReservationer(DateTime date)
        {
            List<IReservation<ReserveTime>> FoundRes = new List<IReservation<ReserveTime>>();
            foreach (IReservation<ReserveTime> reservation in Reservations)
            {
                if (date.DayOfYear == reservation.Timeslot.FromDate.DayOfYear)
                {
                    FoundRes.Add(reservation);
                }
            }
            return FoundRes;
        }
        public ReserveTime GetCorrentSelected()
        {
            if (StartTime > SlutTime)
            {
                return new ReserveTime()
                {
                    ReservationId = Guid.NewGuid(),
                    FromDate = new DateTime(Date.Year, Date.Month, Date.Day, SlutTime.Hours, SlutTime.Minutes, 0),
                    ToDate = new DateTime(Date.Year, Date.Month, Date.Day, StartTime.Hours, StartTime.Minutes, 0)
                };

            }
            return new ReserveTime()
            {
                ReservationId = Guid.NewGuid(),
                FromDate = new DateTime(Date.Year, Date.Month, Date.Day, StartTime.Hours, StartTime.Minutes, 0),
                ToDate = new DateTime(Date.Year, Date.Month, Date.Day, SlutTime.Hours, SlutTime.Minutes, 0)
            };
        }
        public void DrawDay(SKCanvas canvas, int wtidth, int height, DateTime date)
        {
            List<IReservation<ReserveTime>> FoundRes = FindAllReservationer(date);
            IUser user = new User() { Id = Guid.Empty };
            if (Application.Current.Properties.ContainsKey("UserData"))
            {
                user = (User)Application.Current.Properties["UserData"];
            }
            if (date.DayOfYear == Date.DayOfYear && date.Year == Date.Year)
            {
                FoundRes.Add(new Reservation<ReserveTime>() { Id = Guid.Empty, UserId = user.Id, Timeslot = GetCorrentSelected(), ResourceId = Id });
            }
            drawBookning.DrawDay(canvas, wtidth, height, FindAllAvailableTime(date), FoundRes, user.Id);
        }
        public async Task GetReseveringFromSignelR()
        {

        }
        private async Task Connect()
        {
            await _hubConnection.StartAsync();
        }
    }
}
