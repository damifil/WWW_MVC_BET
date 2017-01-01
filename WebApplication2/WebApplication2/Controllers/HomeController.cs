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
    public class HomeController : Controller
    {

        // tak można wyświetlić coś na konsoli  System.Diagnostics.Debug.WriteLine("coś tam");
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

 
        

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult SignUp() // Rejestracja
        {
            return View("Index");
        }

        [HttpPost] // obsługa POST
        public ActionResult SignUp(LoginRegisterView userView)
        {
            if (ModelState.IsValid) // sprawdzenie czy wpisane dane zgadzają się z modelem
            {
                UserManager userManager = new UserManager();
                if (!userManager.IsLoginExist(userView.UserSignUpView.Login))     // sprawdzenie czy login istnieje w bazie
                {
                    userManager.AddUserAccount(userView.UserSignUpView);
                    FormsAuthentication.SetAuthCookie(userView.UserSignUpView.Login, false);    // ustawienie ciasteczka
                    return RedirectToAction("Index", "Aktualnosci");    // przekierowanie do metody Welcome w kontrolerze Home
                }
                else
                {
                    ModelState.AddModelError("", "Login jest już zajęty");
                }

            }
            return View("Index");
        }

        public ActionResult LogIn()    // logowanie
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult LogIn(LoginRegisterView userView, string returnUrl)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid )
            {
                if (!userManager.IsUserExists(userView.UserLoginView.Login))
                {
                    ModelState.AddModelError("", "Konto nie istnieje");
                    return View("Index");
                }
                string password = userManager.GetUserPassword(userView.UserLoginView.Login);
                MD5 md5Hash = MD5.Create();

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "Login lub hasło jest nieprawidłowe.");
                else
                {
                    if (password.Equals(UserManager.GetMd5Hash(md5Hash, userView.UserLoginView.Password)))
                    {
                        FormsAuthentication.SetAuthCookie(userView.UserLoginView.Login, false);
                        userManager.SetLogIn1(userView.UserLoginView.Login);
                        return RedirectToAction("Index", "Aktualnosci");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hasło jest nieprawidłowe.");
                    }
                }
            }

            return View("Index");  
        }


        [Authorize] //atrybut(dekorator) ta metoda zadziała tylko dla użytkowników zalogowanych
        public ActionResult SignOut()   //wylogowywanie
        {
            UserManager userManager = new UserManager();
            userManager.SetLogIn0(User.Identity.Name);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
