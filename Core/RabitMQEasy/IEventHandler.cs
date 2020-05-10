using System;
using System.Collections.Generic;
using System.Text;

namespace RabitMQEasy
{
    public interface IEventHandler<TInstance, TInterface> : ILissener<TInstance, TInterface> where TInstance : class, TInterface
    {
        Events events { get; set; }
    }
    public interface IEventHandler<TInstance> : IEventHandler<TInstance, TInstance>  where TInstance : class
    {
    }
}
