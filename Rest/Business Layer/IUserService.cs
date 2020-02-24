using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Text;
using Business_Layer.Models;

namespace Business_Layer
{
    public interface IUserService
    {
        UserData GetUserFromToken(string token);

        void UpdateUser(Guid id, UserData userData);

        ApiResponse<string> Login(LoginDTO credentials);
    }
}
