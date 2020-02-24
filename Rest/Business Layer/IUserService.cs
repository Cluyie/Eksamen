using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Text;
using Business_Layer.Models;
using Data_Access_Layer.Models;

namespace Business_Layer
{
    public interface IUserService
    {
        User GetUserFromToken(string token);

        void UpdateUser(Guid id, User user);

        ApiResponse<string> Login(LoginDTO credentials);
    }
}
