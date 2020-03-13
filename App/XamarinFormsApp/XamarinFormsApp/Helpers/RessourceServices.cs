using System;
using System.Collections.Generic;
using System.Text;

using Models;


namespace XamarinFormsApp.Helpers
{

    internal class RessourceServices
    {
        private ApiClientProxy _proxy;

        public RessourceServices(ApiClientProxy proxy)
        {
            _proxy = proxy;
        }

        public List<Resource> GetAllRessources()
        {
            List<Resource> tmp = new List<Resource>();
            Resource res = new Resource();
            res.Id = Guid.NewGuid();
            res.strName = "Meeting room 1";

            tmp.Add(res);

            return tmp;
        }

        //public void UpdateUser(User user = null)
        //{
        //    user ??= _proxy.Get<ApiResponse<User>>("User/GetProfile").Value;
        //    Application.Current.Properties["UserData"] = user;
        //}
    }
}
