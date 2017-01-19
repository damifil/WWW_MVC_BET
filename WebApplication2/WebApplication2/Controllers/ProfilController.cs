using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.EntityManager;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{

    public class ProfilController : Controller
    {
        public static class MyStaticValues
        {
            public static string userName { get; set; }
        }
        // GET: Profil
        public ActionResult Index(string userID)
        {
            ProjektEntities db = new ProjektEntities();
            UserManager userManager = new UserManager();
            ProfileView profileView = new ProfileView();

            if (userID == null)
            {
                userID = User.Identity.Name;
            }
            MyStaticValues.userName = userID;
            profileView.login = userID;
            profileView.description = userManager.GetDescription(userID);
            profileView.imageData = userManager.GetImage(userID);
            profileView.date_join = userManager.GetDateJoin(userID);
            ViewBag.user = userID;
            //Pobranie statystyk globalnych
            var GlobalStats = from i in db.USER
                              orderby i.Total_score descending
                              select new { i.User_ID, i.Total_score };

            profileView.global = new List<PointUserView>();

            //Wypełnienie danych statystyk globalnych
            foreach (var item in GlobalStats)
            {
                profileView.global.Add(new PointUserView { login = item.User_ID, points = item.Total_score });
            }

            return View(profileView);
        }

        [HttpPost]
        public ActionResult Zmiana_opisu(ProfileView profileView)
        {
            UserManager userManager = new UserManager();
            ProjektEntities db = new ProjektEntities();


            profileView.login = MyStaticValues.userName;

            profileView.imageData = userManager.GetImage(MyStaticValues.userName);
            profileView.date_join = userManager.GetDateJoin(MyStaticValues.userName);

            //Pobranie statystyk globalnych
            var GlobalStats = from i in db.USER
                              orderby i.Total_score descending
                              select new { i.User_ID, i.Total_score };

            profileView.global = new List<PointUserView>();

            //Wypełnienie danych statystyk globalnych
            foreach (var item in GlobalStats)
            {
                profileView.global.Add(new PointUserView { login = item.User_ID, points = item.Total_score });
            }



            if (ModelState.IsValid)
            {
                userManager.ChangeDescription(profileView, User.Identity.Name);
                ViewBag.Status = "Opis został zmieniony.";
            }

            return View(profileView);
        }

    }
}