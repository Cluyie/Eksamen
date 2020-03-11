using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
    public class AvailableTime : AutoMapper.Profile, IAvailableTime
    {
        public bool Available { get; set ; }
        public DateTime From { get; set; }
        public int? Recurring { get; set; }
        public DateTime To { get; set; }
    }
}
