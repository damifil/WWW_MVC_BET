using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models.ViewModel
{
    public class BetSetGetView
    {
        public List<SelectListItem> MenuLevel1 { get; set; }
        public List<SelectListItem> MenuLevel2 { get; set; }
        public int? CategoryLevel1 { get; set; }
        public string CategoryLevel2 { get; set; }

        public BetGetView betGetView { get; set; }
       public BetSetView betSetView { get; set; }
        public List<RacesView> betRaces { get; set; }
        public BetSetGetView()
        {
            this.betSetView = new BetSetView();
            this.betGetView = new BetGetView();
        }
    }
}