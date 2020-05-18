
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;
using RabitMQEasy;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RabitMQEasyExtensions.DependencyInjection
{
    public static class RabitMQEasyExtensions
    {
        public static RabitMQCunsumer AddRabitMQ(this IServiceCollection serviceCollection, string Url = "localhost")
        {
            ConnectionFactory connection = new ConnectionFactory() { HostName = "localhost" };
            RabitMQCunsumer cunsumer = new RabitMQCunsumer(connection);
            serviceCollection.TryAddSingleton(connection);
            serviceCollection.TryAddSingleton(cunsumer);
            serviceCollection.TryAddScoped<RabitMQPublicer>();
            return cunsumer;
        }



        public static async Task AddEventAsync<T, TInstance, TInterface>(this IApplicationBuilder App)where T : IEventHandler<TInstance, TInterface>  where TInstance : class, TInterface
        {
            await App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnEvent(App.ApplicationServices.GetService<T>());
        }
        public static void AddEvent<T, TInstance, TInterface>(this IApplicationBuilder App) where T : IEventHandler<TInstance, TInterface> where TInstance : class, TInterface
        {
            App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnEvent(App.ApplicationServices.GetService<T>()).Wait();
        }
        public static async Task AddEventAsync<T, TInstance>(this IApplicationBuilder App)where T : IEventHandler<TInstance>  where TInstance : class
        {
            await App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnEvent(App.ApplicationServices.GetService<T>());
        }
        public static void AddEvent<T, TInstance>(this IApplicationBuilder App) where T : IEventHandler<TInstance> where TInstance : class
        {
            App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnEvent(App.ApplicationServices.GetService<T>()).Wait();
        }





        public static async Task AddLissenerAsync<T, TInstance, TInterface>(this IApplicationBuilder App) where T : ILissener<TInstance, TInterface> where TInstance : class, TInterface
        {
            await App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnObject(App.ApplicationServices.GetService<T>());
        }
        public static void AddLissener<T, TInstance, TInterface>(this IApplicationBuilder App) where T : ILissener<TInstance, TInterface> where TInstance : class, TInterface
        {
            App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnObject(App.ApplicationServices.GetService<T>()).Wait();
        }
        public static async Task AddLissenerAsync<T, TInstance>(this IApplicationBuilder App) where T : ILissener<TInstance> where TInstance : class
        {
            await App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnObject(App.ApplicationServices.GetService<T>());
        }
        public static void AddLissener<T, TInstance>(this IApplicationBuilder App) where T : ILissener<TInstance> where TInstance : class
        {
            App.ApplicationServices.GetService<RabitMQCunsumer>().SubscribeOnObject(App.ApplicationServices.GetService<T>()).Wait();
        }



        public static async Task<RabitMQCunsumer> AddLissenerAsync<TInstance, TInterface>(this RabitMQCunsumer serviceCollection, string exchange, Func<TInterface, Task> funtion) where TInstance : class, TInterface
        {
            await serviceCollection.Subscribe<TInstance, TInterface>(exchange, funtion);
            return serviceCollection;
        }
        public static RabitMQCunsumer AddLissener<TInstance, TInterface>(this RabitMQCunsumer serviceCollection, string exchange, Func<TInterface, Task> funtion) where TInstance : class, TInterface
        {
            serviceCollection.Subscribe<TInstance, TInterface>(exchange, funtion).Wait();
            return serviceCollection;
        }
        public static async Task<RabitMQCunsumer> AddLissenerAsync<TInstance>(this RabitMQCunsumer serviceCollection, string exchange, Func<TInstance, Task> funtion) where TInstance : class
        {
            await serviceCollection.Subscribe<TInstance, TInstance>(exchange, funtion);
            return serviceCollection;
        }
        public static RabitMQCunsumer AddLissener<TInstance>(this RabitMQCunsumer serviceCollection, string exchange, Func<TInstance, Task> funtion) where TInstance : class
        {
            serviceCollection.Subscribe<TInstance, TInstance>(exchange, funtion).Wait();
            return serviceCollection;
        }
        public static async Task<RabitMQCunsumer> AddLissenerAsync(this RabitMQCunsumer serviceCollection, string exchange, Func<string, Task> funtion)
        {
            await serviceCollection.Subscribe<string, string>(exchange, funtion);
            return serviceCollection;
        }
        public static RabitMQCunsumer AddLissener(this RabitMQCunsumer serviceCollection, string exchange, Func<string, Task> funtion)
        {
            serviceCollection.Subscribe<string, string>(exchange, funtion).Wait();
            return serviceCollection;
        }
    }
}