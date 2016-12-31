using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models.DB;
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
            UserSettingView userSettingView = new UserSettingView();
            userSettingView.emailView.email = userManager.GetEmail(login);
            userSettingView.userDescriptionView.description = userManager.GetDescription(login);
            

            if (userManager.GetImage(login) == null)
                return View(userSettingView);
            userSettingView.imageView.imageData = userManager.GetImage(login);
            return View(userSettingView);
        }

        [HttpPost]
        public ActionResult Zmiana_zdjecia(UserSettingView userSettingView)
        {
            UserManager userManager = new UserManager();
            userSettingView.userDescriptionView.description = userManager.GetDescription(User.Identity.Name);
            if (userManager.GetImage(User.Identity.Name) == null)
                return View(userSettingView);
            userSettingView.imageView.imageData = userManager.GetImage(User.Identity.Name);
            userSettingView.emailView.email = userManager.GetEmail(User.Identity.Name);

            if (userSettingView.imageView.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("", "Wybrane zdjęcie jest większe niż 2MB.");
            }
            else if (!(userSettingView.imageView.File.ContentType == "image/jpg" || userSettingView.imageView.File.ContentType == "image/jpeg" || userSettingView.imageView.File.ContentType == "image/gif"))
            {
                ModelState.AddModelError("", "Zły format zdjęcia.");
            }
            else
            {
                byte[] data = new byte[userSettingView.imageView.File.ContentLength];
                userSettingView.imageView.File.InputStream.Read(data, 0, userSettingView.imageView.File.ContentLength);
                userSettingView.imageView.imageData = data;
                
                userManager.ChangeImage(userSettingView, User.Identity.Name);

            }
            return View(userSettingView);
        }
              
        [HttpPost]
        public ActionResult Zmiana_email(UserSettingView userSettingView)
        {
            UserManager userManager = new UserManager();
            userSettingView.userDescriptionView.description = userManager.GetDescription(User.Identity.Name);
            if (userManager.GetImage(User.Identity.Name) == null)
                return View(userSettingView);
            userSettingView.imageView.imageData = userManager.GetImage(User.Identity.Name);
            userSettingView.emailView.email = userManager.GetEmail(User.Identity.Name);

            if (ModelState.IsValid)
            {
                if (userSettingView.emailView.email == userSettingView.emailView.newEmail)
                {
                    ModelState.AddModelError("", "Nowy e-mail jest taki sam jak aktualny.");
                }
                else
                {
                    
                    userManager.ChangeEmail(userSettingView, User.Identity.Name);
                    ViewBag.Status = "Adres e-mail został zmieniony.";
                }
               
            }
            return View(userSettingView);
        }

        [HttpPost]
        public ActionResult Zmiana_opisu(UserSettingView userSettingView)
        {
            UserManager userManager = new UserManager();
            userSettingView.userDescriptionView.description = userManager.GetDescription(User.Identity.Name);
            if (userManager.GetImage(User.Identity.Name) == null)
                return View(userSettingView);
            userSettingView.imageView.imageData = userManager.GetImage(User.Identity.Name);
            userSettingView.emailView.email = userManager.GetEmail(User.Identity.Name);

            if (ModelState.IsValid)
            {
                
                userManager.ChangeDescription(userSettingView, User.Identity.Name);
                ViewBag.Status = "Opis został zmieniony.";
            }

            return View(userSettingView);
        }

        [HttpPost]
        public ActionResult Zmiana_hasla(UserSettingView userSettingView)
        {
            UserManager userManager = new UserManager();
            userSettingView.userDescriptionView.description = userManager.GetDescription(User.Identity.Name);
            if (userManager.GetImage(User.Identity.Name) == null)
                return View(userSettingView);
            userSettingView.imageView.imageData = userManager.GetImage(User.Identity.Name);
            userSettingView.emailView.email = userManager.GetEmail(User.Identity.Name);

            if (ModelState.IsValid)
            {
                MD5 md5Hash = MD5.Create();
              
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

            return View(userSettingView);
        }

        [HttpPost]
        public ActionResult Usuniecie_konta(UserSettingView userSettingView)
        {
            UserManager userManager = new UserManager();
            userSettingView.userDescriptionView.description = userManager.GetDescription(User.Identity.Name);
            if (userManager.GetImage(User.Identity.Name) == null)
                return View(userSettingView);
            userSettingView.imageView.imageData = userManager.GetImage(User.Identity.Name);
            userSettingView.emailView.email = userManager.GetEmail(User.Identity.Name);

            if (ModelState.IsValid && userSettingView.deleteUserView.deleteU == true)
            {
                
                userManager.DeleteUser(userSettingView, User.Identity.Name);
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

            return View(userSettingView);       
        }
    }
}