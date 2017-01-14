using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class StatisticView
    {
        public List<PointUserView> global { get; set; }
        public List<PointUserView> friend { get; set; }
        public List<PointGroupView> group { get; set; }
        public List<string> groups { get; set; }
    }
}