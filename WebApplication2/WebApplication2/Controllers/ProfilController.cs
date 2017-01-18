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
            profileView.login = userID;
            profileView.description = userManager.GetDescription(userID);
            profileView.imageData = userManager.GetImage(userID);
            profileView.date_join = userManager.GetDateJoin(userID);

            //Pobranie statystyk globalnych
            var GlobalStats = from i in db.USER
                              orderby i.Total_score descending
                              select new { i.User_ID, i.Total_score };

            //Wypełnienie danych statystyk globalnych
            foreach (var item in GlobalStats)
            {
                profileView.global.Add(new PointUserView { login = item.User_ID, points = item.Total_score });
            }

            return View(profileView);
        }
    }
}