
using XamarinFormsApp.Helpers;
using Models;
using System.Collections.ObjectModel;
using Autofac;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using AutoMapper;

namespace XamarinFormsApp.ViewModel
{

    public class ViewRessourceViewModel : AutoMapper.Profile
    {
        public string ErrorMessage { get; private set; }
        public List<Resource> Resources { get; set; }
        private ApiClientProxy _proxy;
        private Mapper _mapper;
        public ViewRessourceViewModel()
        {
           _mapper = AutofacHelper.Container.Resolve<Mapper>();
            _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
        }


       



        public ViewRessourceViewModel InitializeWithResourceData()
        {
            var response =  _proxy.Get<HttpResponseMessage>(@"http://81.27.216.103/webAPI/Resource");
            var result =  ApiClientProxy.ReadAnswer<ApiResponse<List<Resource>>>(response);
            if(!response.IsSuccessStatusCode && result?.Code != ApiResponseCode.OK)
            {
                ErrorMessage = _proxy.GenerateErrorMessage(result, response);
            }
            return _mapper.Map<ViewRessourceViewModel>(_mapper.Map<List<Resource>>(result.Value));
            
        }
        
    }
    
}
