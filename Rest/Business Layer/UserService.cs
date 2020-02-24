using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Text;
using Business_Layer.Models;
using Data_Access_Layer.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer
{
    public class UserService
    {
        Data_Access_Layer.AppContext AppContext;

        public UserService(Data_Access_Layer.AppContext appContext)
        {
            AppContext = appContext;
        }

        UserData GetFromID(Guid id)
        {
            return AppContext.UserData.FirstOrDefault(user => user.Id == id.ToString());
        }

        UserData GetFromEmail(string email)
        {
            return AppContext.UserData.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
        }

        UserData GetFromUsername(string username)
        {
            return AppContext.UserData.FirstOrDefault(user => user.UserName.ToLower() == username.ToString());
        }

        UserData GetUserFromToken(string token)
        {
            // TODO: Validate token against token in the database instead of username
            return AppContext.UserData.FirstOrDefault(user => user.UserName == token);
        }

        void UpdateUser(Guid id, UserData userData)
        {
            AppContext.Attach(userData).State = EntityState.Modified;
            AppContext.SaveChanges();
        }

        ApiResponse<string> Login(LoginDTO credentials)
        {
            // TODO: Make proper login functionality. For now it always authenticates
            // if the username matches a user
            UserData user = GetFromEmail(credentials.UsernameOrMail);

            if(user == null)
            {
                user = GetFromUsername(credentials.UsernameOrMail);
            }

            if(user == null)
            {
                return new ApiResponse<string>(ApiResponseCode.BadRequest, "");
            }

            // TODO: Send a proper token instead of just the username
            return new ApiResponse<string>(ApiResponseCode.OK, user.UserName);
        }
    }
}
