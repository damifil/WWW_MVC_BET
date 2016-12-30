using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class BetSetView
    {
        public int BetID { get; set; }

        public string Driver_Name1 { get; set; }
        public string Driver_Name2 { get; set; }
        public string Driver_Name3 { get; set; }
        public string Driver_Time1 { get; set; }

        public BetSetView()
        {

        }

    }
}