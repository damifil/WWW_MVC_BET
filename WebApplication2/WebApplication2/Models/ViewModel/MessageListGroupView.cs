using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class MessageListGroupView
    {
        public List<MessageGroupView> ListGroupMessage { get; set; }
        public string newMessageContent { get; set; }
        public PointGroupView group { get; set; }
    }
}