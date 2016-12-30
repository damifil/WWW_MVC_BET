using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class BetGetView
    {
        public string selectedTrack { get; set; }
        public string betPos1 { get; set; }
        public string betPos2 { get; set; }
        public string betPos3 { get; set; }
        public string betTime1 { get; set; }

        public string racePos1 { get; set; }
        public string racePos2 { get; set; }
        public string racePos3 { get; set; }
        public string raceTime1 { get; set; }

        public int scorePos1 { get; set; }
        public int scorePos2 { get; set; }
        public int scorePos3 { get; set; }
        public int scoreTime1 { get; set; }

        public int scoreSum { get; set; }

        public BetGetView()
        {

        }

    }
}