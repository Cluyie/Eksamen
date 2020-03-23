using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Models.Interfaces;

namespace XamarinFormsApp.ViewModel
{
    public class BookRessourceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<IAvailableTime> AllRessources;
        


    }   
}       
        