using Autofac;
using AutoMapper;
using System;
using System.Threading.Tasks;
using UCLToolBox;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Profile = AutoMapper.Profile;

namespace XamarinFormsApp.ViewModel
{
    class ReservationDetailsViewModel : Profile
    {


        private readonly ApiClientProxy _proxy;
        private readonly Mapper _mapper;
        private readonly AuthService _authService;

        public ReservationDetailsViewModel()
        {
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
            _mapper = AutofacHelper.Container.Resolve<Mapper>();
            _authService = AutofacHelper.Container.Resolve<AuthService>();
        }

        public async Task<bool> DeleteReservation(Guid guid)
        {
            var response = await _proxy.DeleteAsync($"Reservation/{guid}");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }


    }
}
