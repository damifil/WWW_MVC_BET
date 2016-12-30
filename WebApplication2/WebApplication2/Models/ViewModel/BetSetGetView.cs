using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class BetSetGetView
    {

       
       public BetGetView betGetView { get; set; }
       public BetSetView betSetView { get; set; }
        public BetSetGetView()
        {
            this.betSetView = new BetSetView();
            this.betGetView = new BetGetView();
        }
    }
}