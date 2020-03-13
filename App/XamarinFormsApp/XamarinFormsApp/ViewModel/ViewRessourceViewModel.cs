using System;
using System.Collections.Generic;
using System.Text;
using XamarinFormsApp.Helpers;
using Models;

namespace XamarinFormsApp.ViewModel
{
   
        public class ViewRessourceViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public List<IResource> AllRessources;

        public ViewRessourceViewModel()
        {
            //RessourceServices hlp = new RessourceServices();
            //AllRessources = hlp.GetAllRessources();

            List<IResource> tmp = new List<IResource>();
            Resource res = new Resource();
            res.Id = Guid.NewGuid();
            res.strName = "Meeting room 1";

            tmp.Add(res);
            AllRessources = tmp;
        }

        }
    
}
