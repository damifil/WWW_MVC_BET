using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class UserDescriptionView
    {
        public string description { get; set; }

        public UserDescriptionView()
        {
            this.description = null;
        }
    }
}