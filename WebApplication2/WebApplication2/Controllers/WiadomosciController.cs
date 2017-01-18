using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class WiadomosciController : Controller
    {
        public static string userTo;
        public ActionResult Index(string userID)
        {
            ProjektEntities db = new ProjektEntities();

            var message = from m in db.MESSAGES
                          where (m.Message_From == User.Identity.Name && m.Message_To == userID) || (m.Message_From == userID && m.Message_To == User.Identity.Name)
                          orderby m.Message_ID
                          select new { m.Message_From, m.Message_To, m.Content, m.Message_ID, m.Date };

            userTo = userID;
            var mod = new MessageListView
            {
                ListMessage = new List<MessageView>(),
                ListUsersMessage = new List<MessageView>()
            };

            foreach (var item in message)
            {
                mod.ListMessage.Add(new MessageView { fromUser = item.Message_From, toUser = item.Message_To, content = item.Content, messageID = item.Message_ID, date = item.Date });
            }


            var listUser = db.MESSAGES.Where(m => m.Message_From == User.Identity.Name)
               .GroupBy(p => p.Message_To, (a, b) => b.OrderByDescending(e => e.Message_ID)).Select(s => s.FirstOrDefault());

            var listUser1 = db.MESSAGES.Where(m => m.Message_To == User.Identity.Name)
               .GroupBy(p => p.Message_From, (a, b) => b.OrderByDescending(e => e.Message_ID)).Select(s => s.FirstOrDefault());
            foreach (var item in listUser)
            {
                mod.ListUsersMessage.Add(new MessageView { toUser = item.Message_To, date = item.Date });

            }
           /* foreach(var item in listUser1)
            {
                mod.ListMessage.Add(new MessageView { toUser = item.Message_To, date = item.Date });
            }*/
            return View(mod);
        }

        [HttpPost]
        public ActionResult Index(MessageListView messageListView) // szukaj
        {
            ProjektEntities db = new ProjektEntities();

            var us = db.USER.Where(o => o.User_ID.Equals(messageListView.newMessageTo));
            if (us.Any())
            {
                userTo = messageListView.newMessageTo;
            }
            else
            {
                ModelState.AddModelError("", "Użytkownik o podanym loginie nie istnieje.");
            }

            var message = from m in db.MESSAGES
                          where (m.Message_From == User.Identity.Name && m.Message_To == userTo) || (m.Message_From == userTo && m.Message_To == User.Identity.Name)
                          orderby m.Message_ID
                          select new { m.Message_From, m.Message_To, m.Content, m.Message_ID, m.Date };

            var mod = new MessageListView
            {
                ListMessage = new List<MessageView>(),
                ListUsersMessage = new List<MessageView>()
            };

            foreach (var item in message)
            {
                mod.ListMessage.Add(new MessageView { fromUser = item.Message_From, toUser = item.Message_To, content = item.Content, messageID = item.Message_ID , date = item.Date});
            }


            var listUser = db.MESSAGES.Where(m => m.Message_From == User.Identity.Name)
                  .GroupBy(p => p.Message_To, (a, b) => b.OrderByDescending(e => e.Message_ID)).Select(s => s.FirstOrDefault());

            foreach (var item in listUser)
            {
                mod.ListUsersMessage.Add(new MessageView { toUser = item.Message_To, date = item.Date });

            }
            return View(mod);

        }

        [HttpPost]
        public ActionResult wyslij(MessageListView messageListView)
        {
            
            if (string.IsNullOrWhiteSpace(messageListView.newMessageContent))
            {
             return RedirectToAction("Index", new { userID = userTo });
            }
            ProjektEntities db = new ProjektEntities();
            var mod = new MessageListView
            {
                ListMessage = new List<MessageView>(),
                ListUsersMessage = new List<MessageView>()
            };
            var message = from m in db.MESSAGES
                          where (m.Message_From == User.Identity.Name && m.Message_To == userTo) || (m.Message_From == userTo && m.Message_To == User.Identity.Name)
                          orderby m.Message_ID
                          select new { m.Message_From, m.Message_To, m.Content, m.Message_ID, m.Date };


            foreach (var item in message)
            {
                mod.ListMessage.Add(new MessageView { fromUser = item.Message_From, toUser = item.Message_To, content = item.Content, messageID = item.Message_ID, date = item.Date });
            }

            var listUser = db.MESSAGES.Where(m => m.Message_From == User.Identity.Name)
                          .GroupBy(p => p.Message_To, (a, b) => b.OrderByDescending(e => e.Message_ID)).Select(s => s.FirstOrDefault());

            foreach (var item in listUser)
            {
                mod.ListUsersMessage.Add(new MessageView { toUser = item.Message_To, date = item.Date });

            }

            if (userTo == null)
                userTo = User.Identity.Name;
            MESSAGES newMessage = new MESSAGES();
            newMessage.Message_From = User.Identity.Name;
            newMessage.Message_To = userTo;
            newMessage.Content = messageListView.newMessageContent;
            newMessage.Date = DateTime.Now;
            db.MESSAGES.Add(newMessage);
            db.SaveChanges();

            return RedirectToAction("Index", new { userID = userTo}); 
        }
    }
}