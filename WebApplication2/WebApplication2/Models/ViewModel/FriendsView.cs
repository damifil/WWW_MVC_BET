using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class FriendsView
    {
        public string UserID { get; set; }
        public byte[] imageData { get; set; }

       
        public FriendsView()
        {
            this.UserID = null;
            this.imageData = null;
        }
    }
}