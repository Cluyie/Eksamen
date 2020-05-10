using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabitMQEasy
{
    public interface ILissener<TInstance, TInterface> where TInstance : class, TInterface
    {
        public Task action(TInterface Obj);
    }
    public interface ILissener<TInstance> : ILissener<TInstance, TInstance> where TInstance : class
    {

    }
}
