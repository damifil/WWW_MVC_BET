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
                             where us.User_ID.Contains(a) && us.Is_Exists == true
                             orderby us.User_ID
                             select new { us.User_ID, us.Image };

                foreach (var x in search)
                {
                    mod.ListSearch.Add(new FriendsView { UserID = x.User_ID, imageData = x.Image });
                }
            }

            return View(mod);
        }

        public ActionResult wiadomosc(string userID)
        {
            ProjektEntities db = new ProjektEntities();

            var message = from m in db.MESSAGES
                          where (m.Message_From == User.Identity.Name && m.Message_To == userID) || (m.Message_From == userID && m.Message_To == User.Identity.Name)
                          orderby m.Message_ID
                          select new { m.Message_From, m.Message_To, m.Content, m.Message_ID };


            var mod = new MessageListView
            {
                ListMessage = new List<MessageView>(),
                ListUsersMessage = new List<MessageView>()
            };

            foreach (var item in message)
            {
                mod.ListMessage.Add(new MessageView { fromUser = item.Message_From, toUser = item.Message_To, content = item.Content, messageID = item.Message_ID });
            }


            var listMessage = from m in db.MESSAGES
                              where m.Message_From == User.Identity.Name
                              group m by m.Message_From
                              into gr
                              select gr.Distinct();
            //select new { m.Message_To, m.Date };
            var a = db.MESSAGES.Where(m => m.Message_From == User.Identity.Name)
                .GroupBy(p => p.Message_To).Select(s => s.FirstOrDefault());

            foreach (var item in a)
            {
                mod.ListUsersMessage.Add(new MessageView { toUser = item.Message_To, date = item.Date });

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
           

            INVITATIONS invitation = new INVITATIONS();
            invitation.Accept = false;
            invitation.From_ID = User.Identity.Name;
            invitation.To_ID = userID;
            System.Diagnostics.Debug.WriteLine("coś tam+ " + invitation.Accept + " " + invitation.From_ID + " " + invitation.To_ID );
            db.INVITATIONS.Add(invitation);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
/*
        [HttpPost]
        public ActionResult Zapros(string userID)
        {
            if (userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ProjektEntities())
            {
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
                if (user == null)
                {
                    return HttpNotFound();
                }

                INVITATIONS invitation = new INVITATIONS();
                invitation.Accept = false;
                invitation.From_ID = User.Identity.Name;
                invitation.To_ID = userID;
                db.INVITATIONS.Add(invitation);
                db.SaveChanges();
                return View("Index");
            }
        }*/
    }
}