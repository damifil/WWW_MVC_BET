using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;


namespace WebApplication2.Controllers
{
    public class GrupyController : Controller
    {
        // GET: Grupy
        public ActionResult Index(string groupName)
        {
            ProjektEntities db = new ProjektEntities();


            var message = from m in db.Group_mesage
                          where ( m.Group_Name == "Nieznani") 
                          orderby m.Group_chat_ID
                          select new { m.from_user, m.message,m.Group_Name };



            var mod = new MessageListGroupView
            {
                ListGroupMessage = new List<MessageGroupView>()
            };

            foreach (var item in message)
            {
                mod.ListGroupMessage.Add(new MessageGroupView { fromUser = item.from_user,message=item.message ,
                    groupname = item.Group_Name });
            }



            return View(mod);
        }

        public ActionResult zarzadzaj()
        {
            return View();
        }
    }
}