using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModel
{
    public class FriendsListView
    {
        public List<FriendsView> ListFriends { get; set; }
        public List<FriendsView> ListSearch { get; set; }
    }
}