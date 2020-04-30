using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;

namespace XamarinFormsApp.ViewModel
{
    public class ReservationListViewModel : Profile
    {
        //User user;
        //ReservationMock reservationMock;

        

        private ApiClientProxy _proxy;       

        public ReservationListViewModel()
        {

        }

        public List<Reservation<ReserveTime>> reservationsList;
        public ObservableCollection<ReservationListItem> ReservationListItems { get; set; }
        public ReservationListViewModel initialize()
        {
            //MOCK
            //user = new User() { Id = Guid.NewGuid() };
            //reservationMock = new ReservationMock(user);
            //reservationsList = reservationMock.GetReservationsByUserId(user.Id);


            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            

            
            /*var user = (User) Application.Current.Properties["UserData"]*/;
            var user = _proxy.Get<User>("User");
            var response = _proxy.Get<ApiResponse<List<Reservation<ReserveTime>>>>($"Reservation/{user.Id}");
            if (response?.Code == ApiResponseCode.OK)
            {
                foreach (var item in response.Value)
                {
                     reservationsList.Add(item);
                }
                ReservationListItems = new ObservableCollection<ReservationListItem>();

                foreach (Reservation<ReserveTime> reservation in reservationsList)
                {
                    var apiResponse = _proxy.Get<ApiResponse<Resource>>($"Resource/Guid={reservation.ResourceId}");
                    if (apiResponse?.Code == ApiResponseCode.OK)
                    {
                        var resource = apiResponse.Value;
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
                   
                }


            }
            




            return this;


        }
    }






}

