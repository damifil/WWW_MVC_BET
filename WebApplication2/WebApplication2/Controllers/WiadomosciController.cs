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
        // GET: Wiadomosci
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult wiadomosc()
        {
            ProjektEntities db = new ProjektEntities();

            var messageTo = from m in db.MESSAGES
                          where (m.Message_From == User.Identity.Name && m.Message_To == "Damian") || (m.Message_From == "Damian" && m.Message_To == User.Identity.Name)
                            orderby m.Message_ID
                          select new { m.Message_From, m.Message_To, m.Content, m.Message_ID };

            var messageFrom = from m in db.MESSAGES
                              where (m.Message_From == "Damian" && m.Message_To == User.Identity.Name) 
                              orderby m.Message_ID
                              select new { m.Message_From, m.Message_To, m.Content, m.Message_ID };
            var mod = new MessageListView
            {
                ListMessage = new List<MessageView>(),
                ListUsersMessage = new List<MessageView>()
            };

            foreach(var item in messageTo)
            {
                mod.ListMessage.Add(new MessageView { fromUser = item.Message_From, toUser = item.Message_To, content = item.Content, messageID = item.Message_ID});
            }

            foreach (var item in messageFrom)
            {
                mod.ListUsersMessage.Add(new MessageView { fromUser = item.Message_From, toUser = item.Message_To, content = item.Content, messageID = item.Message_ID });
            }
            return View(mod);
        }
    }
}