using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models.EntityManager;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class UstawieniaController : Controller
    {
        public ActionResult Index()
        {
            string login = User.Identity.Name;
            UserManager userManager = new UserManager();
            UserSettingView userSettingView = userManager.GetEmailImage(userManager.GetLogin(login));
            return View(userSettingView);
        }
              
        [HttpPost]
        public ActionResult Zmiana_email(UserSettingView userSettingView)
        {
            if(ModelState.IsValid)
            {
                if (userSettingView.emailView.email == userSettingView.emailView.newEmail)
                {
                    ModelState.AddModelError("", "Nowy e-mail jest taki sam jak aktualny.");
                }
                else
                {
                    UserManager userManager = new UserManager();
                    userManager.ChangeEmail(userSettingView, User.Identity.Name);
                    ViewBag.Status = "Adres e-mail został zmieniony.";
                }
               
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Zmiana_hasla(UserSettingView userSettingView)
        {
            if (ModelState.IsValid)
            {
                MD5 md5Hash = MD5.Create();
                UserManager userManager = new UserManager();
                string password = userManager.GetUserPassword(User.Identity.Name);

                if ((UserManager.GetMd5Hash(md5Hash, userSettingView.passwordView.Password)) == password)
                {
                    ModelState.AddModelError("", "Nowe hasło jest takie samo jak aktualne.");
                }
                else if ((UserManager.GetMd5Hash(md5Hash, userSettingView.passwordView.oldPassword)) != password)
                {
                    ModelState.AddModelError("", "Stare hasło nie jest poprawne.");
                }
                else
                {
                    userManager.ChangePassword(userSettingView, User.Identity.Name);
                    ViewBag.Status = "Hasło zostało zmienione.";
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Usuniecie_konta(UserSettingView userSettingView)
        {
            if (ModelState.IsValid && userSettingView.deleteUserView.deleteU == true)
            {
                UserManager userManager = new UserManager();
                userManager.DeleteUser(userSettingView, User.Identity.Name);
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

            return View("Index");       
        }
    }
}