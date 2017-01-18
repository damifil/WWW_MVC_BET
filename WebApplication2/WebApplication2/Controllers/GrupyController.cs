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
            System.Diagnostics.Debug.WriteLine(groupName);
            System.Diagnostics.Debug.WriteLine("po nazwiegrupyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");

            var message = from m in db.Group_mesage
                          where (m.Group_Name == groupName)
                          orderby m.Group_chat_ID
                          select new { m.from_user, m.message, m.Group_Name };

            var GroupStat = from i in db.MEMBERSHIPS
                            where (i.Group_Name==groupName)
                            join u in db.USER
                            on i.User_ID equals u.User_ID
                            where u.Is_Exists == true
                            orderby u.Total_score descending
                            select new { i.User_ID, u.Total_score };

            var mod = new MessageListGroupView
            {
                ListGroupMessage = new List<MessageGroupView>(),
                group = new PointGroupView(groupName)
            };

            foreach (var item in message)
            {
                mod.ListGroupMessage.Add(new MessageGroupView
                {
                    fromUser = item.from_user,
                    message = item.message,
                    groupname = item.Group_Name
                });
            }

            foreach(var item in GroupStat)
            {
                mod.group.members.Add(new PointUserView() { login = item.User_ID, points = item.Total_score });
            }

            return View(mod);
        }

        public ActionResult zarzadzaj()
        {
            return View();
        }




        [HttpPost]
        public ActionResult wyslij(MessageListGroupView messageListGroupView)
        {

            ProjektEntities db = new ProjektEntities();
            var mod = new MessageListGroupView
            {
                ListGroupMessage = new List<MessageGroupView>(),
            };

            var message = from m in db.Group_mesage
                          where (m.Group_Name == "as")
                          orderby m.Group_chat_ID
                          select new { m.from_user, m.message, m.Group_Name };

            foreach (var item in message)
            {
                mod.ListGroupMessage.Add(new MessageGroupView
                {
                    fromUser = item.from_user,
                    message = item.message,
                    groupname = item.Group_Name,                    
                });
            }


            Group_mesage newMessage = new Group_mesage();
             newMessage.from_user = User.Identity.Name;
             newMessage.message= messageListGroupView.newMessageContent;
             newMessage.Group_Name= "as";
             

         

            db.Group_mesage.Add(newMessage);
            db.SaveChanges();

            return RedirectToAction("Index", new { groupName = "as" });
        }
    }

}
