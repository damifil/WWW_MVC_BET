using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.EntityManager;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index(string userID)
        {
        
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
            return View(profileView);
        }


     
    }


}