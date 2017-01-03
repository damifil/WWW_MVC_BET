using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class ZnajomiController : Controller
    {
        // GET: Znajomi
        public ActionResult Index()
        {
            ProjektEntities db = new ProjektEntities();
            var friend = from i in db.FRIENDS
                         where i.User_ID == User.Identity.Name
                         join us in db.USER
                         on i.Friend_ID equals us.User_ID
                         select new { us.User_ID, us.Image };
          
            var mod = new FriendsListView
            {
                ListFriends = new List<FriendsView>(),
                ListSearch = new List<FriendsView>()
            };

            foreach(var x in friend)
            {
                mod.ListFriends.Add(new FriendsView { UserID = x.User_ID, imageData = x.Image });
            }

            return View(mod);
        }

        [HttpPost]
        public ActionResult Index(FriendsListView friendsListView)
        {
            ProjektEntities db = new ProjektEntities();
            var friend = from i in db.FRIENDS
                         where i.User_ID == User.Identity.Name
                         join us in db.USER
                         on i.Friend_ID equals us.User_ID
                         select new { us.User_ID, us.Image };

            var mod = new FriendsListView
            {
                ListFriends = new List<FriendsView>(),
                ListSearch = new List<FriendsView>()
            };

            foreach (var x in friend)
            {
                mod.ListFriends.Add(new FriendsView { UserID = x.User_ID, imageData = x.Image });
                
            }
    
            string a = Request["pole"];
            if (a != string.Empty)
            {
                var search = from us in db.USER
                             where us.User_ID.Contains(a)
                             orderby us.User_ID
                             select new { us.User_ID, us.Image };

                foreach (var x in search)
                {
                    mod.ListSearch.Add(new FriendsView { UserID = x.User_ID, imageData = x.Image });
                }
            }

            return View(mod);
        }
    }
}