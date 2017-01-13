using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class ProfileView
    {
        public string login { get; set; }
        public byte[] imageData { get; set; }
        public DateTime date_join { get; set; }
        public string description { get; set; }
        public bool invite_delete { get; set; }

        public List<string> groups { get; set; }
        
    }
}