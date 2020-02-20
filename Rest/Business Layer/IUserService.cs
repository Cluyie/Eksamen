using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    public interface IUserService
    {
        void UpdateUser(Guid id, UserData userData);
    }
}
