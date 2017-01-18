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

            //Inicjacja zmiennych
            var mod = new StatisticView
            {
                global = new List<PointUserView>(),
                group = new List<PointGroupView>(),
                friend = new List<PointUserView>(),
                user = new PointUserView()
            };

            //Pobranie statystyk użytkownika
            var UserStats = from i in db.USER
                            where i.User_ID == User.Identity.Name
                            select new { i.User_ID, i.Total_score };

            //Inicjalizacja statystyk użytkownika
            foreach(var item in UserStats)
            {
                mod.user = new PointUserView { login = item.User_ID, points = item.Total_score };
            }

            //Pobranie statystyk globalnych
            var GlobalStats = from i in db.USER orderby i.Total_score descending
                              select new { i.User_ID, i.Total_score };

            //Pobranie statystyk przyjaciół
            var FriendStats = from i in db.FRIENDS
                              where i.User_ID == User.Identity.Name
                              join us in db.USER 
                              on i.Friend_ID equals us.User_ID
                              where us.Is_Exists == true
                              orderby us.Total_score descending
                              select new { us.User_ID, us.Total_score };

            //Pobranie listy grup użytkownika
            var GroupList = from i in db.MEMBERSHIPS
                            where i.User_ID == User.Identity.Name
                            select new { i.Group_Name };

            //Inicjacja statystyk grup
            foreach(var item in GroupList)
            {
                mod.group.Add(new PointGroupView(item.Group_Name));
            }

            //Pobranie statystyk członków grup do których należy użytkownika
            var GroupStats = from i in db.MEMBERSHIPS
                             where i.User_ID == User.Identity.Name
                             join j in db.MEMBERSHIPS
                             on i.Group_Name equals j.Group_Name
                             join u in db.USER
                             on j.User_ID equals u.User_ID
                             where u.Is_Exists == true
                             orderby u.Total_score descending
                             select new { j.Group_Name, j.User_ID, u.Total_score };

            //Wypełnienie statystyk group
            foreach(var item in GroupStats)
            {
                foreach(PointGroupView grupa in mod.group)
                {
                    if (item.Group_Name == grupa.name) { grupa.members.Add(new PointUserView { login = item.User_ID, points = item.Total_score }); }
                }
            }

            //Wypełnienie danych statystyk globalnych
            foreach(var item in GlobalStats)
            {
                mod.global.Add(new PointUserView { login = item.User_ID, points = item.Total_score });
            }

            //Wypełnienie danych statystyk znajomych
            foreach(var item in FriendStats)
            {
                mod.friend.Add(new PointUserView { });
            }

            //Dodanie statystyk użytkownika do statystyk jego przyjaciół
            mod.friend.Add(mod.user);

            //Sortowanie statystyk
            mod.global = mod.global.OrderByDescending(x => x.points).ToList();
            mod.friend = mod.friend.OrderByDescending(x => x.points).ToList();
            foreach(var item in mod.group)
            {
                item.members = item.members.OrderByDescending(x => x.points).ToList();
            }

            //Zwróć mod
            return View(mod);
        }
    }
}