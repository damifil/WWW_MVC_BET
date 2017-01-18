using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                         where us.Is_Exists == true
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
                         where us.Is_Exists == true
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
                             where us.User_ID.Contains(a) && us.Is_Exists == true && us.User_ID != User.Identity.Name
                             orderby us.User_ID
                             select new { us.User_ID, us.Image };

                foreach (var x in search)
                {
                    System.Diagnostics.Debug.WriteLine("imie " + x.User_ID + " wpis " + a);
                    mod.ListSearch.Add(new FriendsView { UserID = x.User_ID, imageData = x.Image });
                }
            }

            return View(mod);
        }


        public ActionResult Zapros(string userID)
        {
            var db = new ProjektEntities();
            var friend = from i in db.FRIENDS
                         where i.User_ID == User.Identity.Name
                         join us in db.USER
                         on i.Friend_ID equals us.User_ID
                         where us.Is_Exists == true
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

            USER user = db.USER.Find(userID);


            /*  INVITATIONS invitation = new INVITATIONS();
              invitation.Accept = false;
              invitation.From_ID = User.Identity.Name;
              invitation.To_ID = userID;
              db.INVITATIONS.Add(invitation);*/

            FRIENDS newFriend = new FRIENDS();
            newFriend.Friend_ID = userID;
            newFriend.User_ID = User.Identity.Name;
            db.FRIENDS.Add(newFriend);
            db.SaveChanges();
            newFriend.User_ID = userID;
            newFriend.Friend_ID = User.Identity.Name;
            db.FRIENDS.Add(newFriend);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}