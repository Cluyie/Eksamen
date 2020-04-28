using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
   public class ReservationListViewModel
    {

        public ReservationListViewModel()
        {

        }
        public int id { get; set; } = 23834834;

        public ObservableCollection<ReservationMock> Reservations { get; set; }
        public ReservationListViewModel initialize()
        {
            Reservations = new ObservableCollection<ReservationMock>
            {
                new ReservationMock()
                {
                    Id = new Guid(),
                    ResourceId = new Guid(),
                    UserId = new Guid(),
                    From = new DateTime(2020, 1, 15),
                    To = DateTime.Now
                },

            new ReservationMock()
            {
                Id = new Guid(),
                ResourceId = new Guid(),
                UserId = new Guid(),
                From = new DateTime(2020, 4, 13),
                To = DateTime.Now
            }

            };

            return this;
        }


    }
}
