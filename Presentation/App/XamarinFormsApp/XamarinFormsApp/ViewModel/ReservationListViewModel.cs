using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
    public class ReservationListViewModel
    {
        User user;
        ReservationMock reservationMock;

        public ReservationListViewModel()
        {

        }

        public List<Reservation<ReserveTime>> reservationsList;
        public ObservableCollection<Reservation<ReserveTime>> Reservations;
        public ObservableCollection<ReservationListItem> ReservationListItems { get; set; }
        public ReservationListViewModel initialize()
        {
            user = new User() { Id = Guid.NewGuid() };
            reservationMock = new ReservationMock(user);
            reservationsList = reservationMock.GetReservationsByUserId(user.Id);

            ReservationListItems = new ObservableCollection<ReservationListItem>();
            
            //ReservationListItems.Add(new ReservationListItem()
            //{
            //    Id = new Guid(),
            //    Description = "ksdfkdsf"
            //});

            foreach (Reservation<ReserveTime> reservation in reservationsList)
            {
                Resource resource = reservationMock.GetResource(reservation.ResourceId);
                ReservationListItems.Add(new ReservationListItem()
                {
                    Id = reservation.Id,
                    From = reservation.Timeslot.FromDate,
                    To = reservation.Timeslot.ToDate,
                    UserId = reservation.UserId,
                    ResourceId = resource.Id,
                    Description = resource.Description,
                    Name = resource.Name
                });
            }


            return this;

            
        }
    }
            

           
        


}

