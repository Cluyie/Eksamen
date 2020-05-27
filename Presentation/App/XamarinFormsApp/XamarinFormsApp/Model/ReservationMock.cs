using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace XamarinFormsApp.Model
{
    public class ReservationMock
    {
        User user;
        List<Reservation> reservations;

        public ReservationMock(User _user)
        {
            user = _user;
            reservations = new List<Reservation>();
            CreateReservations();
        }

        List<Resource> resources = new List<Resource>()
        {
            new Resource()
            {
                Id = Guid.NewGuid(),
                Name = "Trailer",
                Description = "sfdjsdfls"
            },

            new Resource()
            {
                Id = Guid.NewGuid(),
                Name = "Skovl",
                Description = "asdgf"
            },
            new Resource()
            {
                Id = Guid.NewGuid(),
                Name = "Spade",
                Description = "sdfsdfas"
            },
            new Resource()
            {
                Id = Guid.NewGuid(),
                Name = "Le",
                Description = "yuyfdf"
            }
        };

         
        public void CreateReservations()
        {

            foreach (Resource r in resources)
            {
                reservations.Add(new Reservation()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ResourceId = r.Id,
                    Timeslot = new ReserveTime() { FromDate = new DateTime(2020, 4, 14), ToDate = DateTime.Now }

                });
            }           
        }

        public Resource GetResource(Guid resourceId)
        {
            return resources.SingleOrDefault(resource => resource.Id == resourceId);
        }

        public List<Reservation> GetReservationsByUserId(Guid userId)
        {
            return reservations.Where(r => r.UserId == userId).ToList();
        }




    }
}
