using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class MessageView
    {
        public string fromUser { get; set; }
        public string toUser { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public int messageID { get; set; }
        
    }
}