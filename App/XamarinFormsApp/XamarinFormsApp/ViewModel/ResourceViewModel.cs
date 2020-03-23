
using XamarinFormsApp.Helpers;
using Models;
using System.Collections.ObjectModel;
using Autofac;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using AutoMapper;
using Models.Interfaces;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{

    public class ResourceViewModel : AutoMapper.Profile
    {
        public string ErrorMessage { get; private set; }
        public List<Resource> Resources { get; set; }
        private ApiClientProxy _proxy;
        private Mapper _mapper;
        public ResourceViewModel()
        {
           _mapper = AutofacHelper.Container.Resolve<Mapper>();
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
        }


       



        public ResourceViewModel InitializeWithResourceData()
        {
            var response =  _proxy.Get<ApiResponse<List<Resource>>>(@"Resource");
           
            if( response?.Code != ApiResponseCode.OK)
            {
                ErrorMessage = _proxy.GenerateErrorMessage(response);
            }
            
            return new ResourceViewModel()
            {
                Resources = response.Value
            };
        }
        
    }
    
}
