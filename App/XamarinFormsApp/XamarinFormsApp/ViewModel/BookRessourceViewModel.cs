using Autofac;
using Microsoft.AspNetCore.SignalR.Client;
using Models.Interfaces;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Helpers.Graphish;
using XamarinFormsApp.Model;

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

        private HubConnection _hubConnectionRerservation;
        private HubConnection _hubConnectionAvaiableTime;

        public BookRessourceViewModel()
        {
            proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            drawBookning = new DrawResourcer();
            Date = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;
            SlutTime = DateTime.Now.TimeOfDay;

            _hubConnectionRerservation = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}ReservationHub").Build();

            Connect();
            //SignalR Client methods for Reservation
            _hubConnectionRerservation.On<Reservation<ReserveTime>>("CreateReservation", (reservation) =>
            {
                if(reservation.ResourceId == Id)
                {
                    Reservations.Add(reservation);
                    reftesh(reservation.Timeslot.FromDate.DayOfWeek);
                }
            });
            _hubConnectionRerservation.On<Reservation<ReserveTime>>("UpdateReservation", (reservation) =>
            {
                if (reservation.ResourceId == Id)
                {
                    Reservations[Reservations.FindIndex(r => r.Id == reservation.Id)] = reservation;
                    reftesh();
                }
            });
            _hubConnectionRerservation.On<Reservation<ReserveTime>>("DeleteReservation", (reservation) =>
            {
                if (reservation.ResourceId == Id)
                {
                    Reservations.RemoveAt(Reservations.FindIndex(re => reservation.Id == re.Id));
                    reftesh(reservation.Timeslot.FromDate.DayOfWeek);
                }
            });


            _hubConnectionAvaiableTime = new HubConnectionBuilder().WithUrl($"{Properties.Resources.SignalRBaseAddress}AvailableTimeHub").Build();
            _hubConnectionAvaiableTime.StartAsync();
            _hubConnectionAvaiableTime.On<AvailableTime>("CreateAvailableTime", (TimeAvaiable) =>
            {
                if (TimeAvaiable.ResourceId == Id)
                {
                    TimeSlots.Add(TimeAvaiable);
                    reftesh(TimeAvaiable.From.DayOfWeek);
                }
            });
            _hubConnectionAvaiableTime.On<AvailableTime>("UpdateAvailableTime", (TimeAvaiable) =>
            {
                if (TimeAvaiable.ResourceId == Id)
                {
                    TimeSlots[TimeSlots.FindIndex(r => r.Id == TimeAvaiable.Id)] = TimeAvaiable;
                    reftesh();
                }
            });
            _hubConnectionAvaiableTime.On<AvailableTime>("DeleteAvailableTime", (TimeAvaiable) =>
            {
                if (TimeAvaiable.ResourceId == Id)
                {
                    TimeSlots.RemoveAt(TimeSlots.FindIndex(re => TimeAvaiable.Id == re.Id));
                    reftesh();
                }
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
                    if (available.To > Resevation.FromDate && available.From < Resevation.ToDate)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    //return;
                }
                List<IReservation<ReserveTime>> Resvertions = FindAllReservationer(Resevation.FromDate);
                foreach (IReservation<ReserveTime> resv in Resvertions)
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
                IReservation<IReserveTime> res = new Reservation<IReserveTime>() { UserId = user.Id, Id = Guid.NewGuid(), Timeslot = Resevation,ResourceId = Id };
                var test = proxy.Post("Reservation/", res);
                //Reservations.Add(res);
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
        private async Task Connect()
        {
            await _hubConnectionRerservation.StartAsync();
        }
    }
}
