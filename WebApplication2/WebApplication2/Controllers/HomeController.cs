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
    public class HomeController : Controller
    {

        // tak można wyświetlić coś na konsoli  System.Diagnostics.Debug.WriteLine("coś tam");
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult statystyki()
        {
            return View();
        }

        public ActionResult aktualnosci()
        {
            return View();
        }

        public ActionResult przyjaciele()
        {
            return View();
        }

        public ActionResult twoje_grupy()
        {
            return View();
        }

        public ActionResult ustawienia()
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
                    return RedirectToAction("Welcome", "Home");    // przekierowanie do metody Welcome w kontrolerze Home
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
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                string password = userManager.GetUserPassword(userView.UserLoginView.Login);
                MD5 md5Hash = MD5.Create();

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "Login lub hasło jest nieprawidłowe.");
                else
                {
                    if (password.Equals(UserManager.GetMd5Hash(md5Hash, userView.UserLoginView.Password)))
                    {
                        FormsAuthentication.SetAuthCookie(userView.UserLoginView.Login, false);
                        return RedirectToAction("Welcome", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hasło jest nieprawidłowe.");
                    }
                }
            }

            return View("Index");  //wyswietlenie widoku z wpisanymi danymi
        }


        [Authorize] //atrybut(dekorator) ta metoda zadziała tylko dla użytkowników zalogowanych
        public ActionResult SignOut()   //wylogowywanie
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
