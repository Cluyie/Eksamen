using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Autofac;
using AutoMapper;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;
using Profile = XamarinFormsApp.Model.Profile;

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
                    cfg.CreateMap(typeof(Profile), typeof(User)).ReverseMap();
                    cfg.CreateMap(typeof(LoginSettings), typeof(User)).ReverseMap();
                    cfg.CreateMap(typeof(List<Resource>), typeof(ResourceViewModel));
                    cfg.CreateMap<IResource<Reservation<ReserveTime>, ReserveTime, AvailableTime>, Resource>();

                    foreach (var type in types)
                    {
                        var viewmodelNamespace = $"{nameof(XamarinFormsApp)}.{nameof(ViewModel)}";
                        if (type.Namespace == viewmodelNamespace)
                        {
                            var name = type.Name.Replace("ViewModel", "");
                            var modelNamespace = $"{nameof(XamarinFormsApp)}.{nameof(Model)}";
                            var modelItem = types.FirstOrDefault(t => t.Namespace == modelNamespace && t.Name == name);
                            if (modelItem != null) cfg.CreateMap(type, modelItem).ReverseMap();
                        }
                    }
                });
                var mapper = new Mapper(config);

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
            //Skal helst uptimeres
            //Offentlig base adresse: http://81.27.216.103/AppBff/
            //Intern base adresse: http://10.56.8.34/WebBff/
            //Lokal base adresse til emulator http://10.0.2.2:5000/
            return "http://10.0.2.2:5000";
            //return "http://10.0.2.2:53524/";
            //"http://81.27.216.103/AppBff/"; //If you are not on the same Net as the server
            //return "http://10.56.8.34/AppBff/"; //If you are on the same Net as the server
        }
    }
}