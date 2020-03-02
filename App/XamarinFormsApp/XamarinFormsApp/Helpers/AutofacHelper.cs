using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Autofac;
using AutoMapper;
using Models;
using XamarinFormsApp.Model;

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
          //Offentlig base adresse: http://81.27.216.103/webAPI/
          //Intern base adresse: http://10.56.8.34/webAPI/
          //Lokal base adresse til emulator http://10.0.2.2:5000/
          BaseAddress = new Uri("http://81.27.216.103/webAPI/")
        };
        

        var builder = new ContainerBuilder();
        builder.RegisterInstance(mapper);
        builder.RegisterInstance(client);
        builder.RegisterType<ApiClientProxy>();
        builder.RegisterType<AuthService>();
        Container = builder.Build();
      }
    }
  }
}



