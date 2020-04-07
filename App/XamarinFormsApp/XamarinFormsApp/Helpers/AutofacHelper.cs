using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Models;
using Models.Interfaces;
using UCLToolBox;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.Helpers
{
    public static class AutofacHelper
    {
        public static IContainer Container;

        public static void Initialize()
        {
            if (Container == null)
            {
                //Automapper setup
                var types = Assembly.GetExecutingAssembly().GetTypes();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap(typeof(Model.Profile), typeof(User)).ReverseMap();
                    cfg.CreateMap(typeof(LoginSettings), typeof(User)).ReverseMap();
                    cfg.CreateMap(typeof(List<Resource>), typeof(ResourceViewModel));
                    cfg.CreateMap<IResource<Reservation<ReserveTime>,ReserveTime,AvailableTime>, Resource>();
                  
                    foreach (var type in types)
                    {
                        string viewmodelNamespace = $"{nameof(XamarinFormsApp)}.{nameof(ViewModel)}";
                        if (type.Namespace == viewmodelNamespace)
                        {
                            string name = type.Name.Replace("ViewModel", "");
                            string modelNamespace = $"{nameof(XamarinFormsApp)}.{nameof(Model)}";
                            var modelItem = types.FirstOrDefault(t => t.Namespace == modelNamespace && t.Name == name);
                            if (modelItem != null)
                            {
                                cfg.CreateMap(type, modelItem).ReverseMap();
                            }
                        }
                    }
                });
                Mapper mapper = new Mapper(config);

                var client = new HttpClient
                {
                    BaseAddress = new Uri(FindUrl())
                };

                var builder = new ContainerBuilder();
                builder.RegisterInstance(mapper);
                builder.RegisterInstance(client);
                builder.RegisterType<ApiClientProxy>();
                builder.RegisterType<AuthService>();
                Container = builder.Build();
            }
        }

        private static string FindUrl()
        {
                return "http://81.27.216.103/webAPI/";
            //Skal helst uptimeres
            //Offentlig base adresse: http://81.27.216.103/webAPI/
            //Intern base adresse: http://10.56.8.34/webAPI/
            //Lokal base adresse til emulator http://10.0.2.2:5000/
          
//#if DEBUG
//            if (TestUrl("http://10.0.2.2:53524/"))
//            {
//                return "http://10.0.2.2:53524/";
//            }
//#endif
//            //If you are not on the same Net as the server
//            if (TestUrl("http://81.27.216.103/webAPI/User/GetProfile"))
//            {
//                return "http://81.27.216.103/webAPI/";
//            }
//            //If you are on the same Net as the server
//            if (TestUrl("http://10.56.8.34/webAPI/User/GetProfile"))
//            {
//                return "http://10.56.8.34/webAPI/";
//            }
//            return "http://81.27.216.103/webAPI/";
          
#if DEBUG
            if (TestUrl("http://10.0.2.2:53524/"))
            {
                return "http://10.0.2.2:53524/";
            }
#endif
            //If you are not on the same Net as the server
            if (TestUrl("http://81.27.216.103/webAPI/User/GetProfile"))
            {
                return "http://81.27.216.103/webAPI/";
            }
            //If you are on the same Net as the server
            if (TestUrl("http://10.56.8.34/webAPI/User/GetProfile"))
            {
                return "http://10.56.8.34/webAPI/";
            }
            return null;
        }

        private static bool TestUrl(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}