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

            var message = from m in db.MESSAGES
                          where (m.Message_From == User.Identity.Name && m.Message_To == "Damian") || (m.Message_From == "Damian" && m.Message_To == User.Identity.Name)
                            orderby m.Message_ID
                          select new { m.Message_From, m.Message_To, m.Content, m.Message_ID };

          
            var mod = new MessageListView
            {
                ListMessage = new List<MessageView>(),
                ListUsersMessage = new List<MessageView>()
            };

            foreach(var item in message)
            {
                mod.ListMessage.Add(new MessageView { fromUser = item.Message_From, toUser = item.Message_To, content = item.Content, messageID = item.Message_ID});
            }


            var listMessage = from m in db.MESSAGES
                              where m.Message_From == User.Identity.Name
                              group m by  m.Message_From 
                              into gr
                            select gr.Distinct();
            //select new { m.Message_To, m.Date };
            var a = db.MESSAGES.Where(m => m.Message_From == User.Identity.Name)
                .GroupBy(p => p.Message_To).Select(s => s.FirstOrDefault()); 
                     
            foreach (var item in a)
            {
                mod.ListUsersMessage.Add(new MessageView {  toUser = item.Message_To,  date = item.Date });
                
            }
            return View(mod);
        }

        [HttpPost]
        public ActionResult wyslij(MessageListView messageListView)
        {
            ProjektEntities db = new ProjektEntities();
            var mod = new MessageListView
            {
                ListMessage = new List<MessageView>(),
                ListUsersMessage = new List<MessageView>()
            };
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
            var message = db.MESSAGES;
            
            System.Diagnostics.Debug.WriteLine( " " + messageListView.newMessageContent);
            return View(messageListView);
        }
    }
}