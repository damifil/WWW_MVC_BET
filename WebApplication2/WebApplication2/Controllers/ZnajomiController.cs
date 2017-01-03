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
            var mod = new List<FriendsView>();
            foreach (var x in friend)
            {
                mod.Add(new FriendsView { UserID = x.User_ID, imageData = x.Image });
            }

            return View(mod);

        }
    }
}