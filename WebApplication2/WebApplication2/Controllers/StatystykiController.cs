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
    public class StatystykiController : Controller
    {
        // GET: Statystyki
        public ActionResult Index()
        {
            ProjektEntities db = new ProjektEntities();

            var GlobalStats = from i in db.USER orderby i.Total_score descending
                              select new { i.User_ID, i.Total_score };

            var FriendStats = from i in db.FRIENDS
                              where i.User_ID == User.Identity.Name
                              join us in db.USER 
                              on i.Friend_ID equals us.User_ID
                              where us.Is_Exists == true
                              orderby us.Total_score descending
                              select new { us.User_ID, us.Total_score };

            var GroupStats = from i in db.USER
                             select new {  };

            var mod = new StatisticView
            {
                global = new List<PointUserView>(),
                group = new List<PointGroupView>(),
                friend = new List<PointUserView>()
            };

            int m = 0;
            foreach(var item in GlobalStats)
            {
                m++;
                mod.global.Add(new PointUserView { login = item.User_ID, position = m, points = item.Total_score });
            }

            //group stats?


            return View(mod);
        }
    }
}