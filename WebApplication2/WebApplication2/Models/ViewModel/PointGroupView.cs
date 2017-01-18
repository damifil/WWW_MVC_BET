using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class PointGroupView
    {
        public List<PointUserView> members { get; set; }
        public string name { get; set; }

        public PointGroupView(string group_name)
        {
            this.name = group_name;
            members = new List<PointUserView>();
        }
    }
}