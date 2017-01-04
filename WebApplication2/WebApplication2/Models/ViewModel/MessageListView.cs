using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class MessageListView
    {
        public List<MessageView> ListMessage { get; set; }
        public List<MessageView> ListUsersMessage { get; set; }
    }
}