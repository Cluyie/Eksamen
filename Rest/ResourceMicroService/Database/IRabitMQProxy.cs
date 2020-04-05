using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceMicroService.Database
{
    interface IRabitMQProxy
    {
        public async Task NewResrource(IResource resource);
        public async Task UpdatereResrource(IResource resource);
        public async Task ResrourceDelte(Guid resourceId);
    }
}
