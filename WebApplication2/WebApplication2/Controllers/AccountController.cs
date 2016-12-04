using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models.EntityManager;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public ActionResult SignUp() // Rejestracja
        {
            return View();
        }

        [HttpPost] // obsługa POST
        public ActionResult SignUp(UserSignUpView userView)
        {
            if (ModelState.IsValid) // sprawdzenie czy wpisane dane zgadzają się z modelem
            {
                UserManager userManager = new UserManager();
                if (!userManager.IsLoginExist(userView.Login))     // sprawdzenie czy login istnieje w bazie
                {
                    userManager.AddUserAccount(userView);
                    FormsAuthentication.SetAuthCookie(userView.Login, false);    // ustawienie ciasteczka
                    return RedirectToAction("Welcome", "Home");    // przekierowanie do metody Welcome w kontrolerze Home
                }
                else
                {
                    ModelState.AddModelError("", "Login jest już zajęty");
                }

            }
            return View();
        }

        public ActionResult LogIn()    // logowanie
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginView userView, string returnUrl) 
        {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                string password = userManager.GetUserPassword(userView.Login);

                System.Diagnostics.Debug.WriteLine("Password z bazy: " + password + " password z wpisane: " + userView.Password);
                System.Diagnostics.Debug.WriteLine("czy prawda: " + password.Length + " i " + userView.Password.Length);
           

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "Login lub hasło jest nieprawidłowe.");
                else
                {
                    if (password.Equals(userView.Password))
                    {
                        FormsAuthentication.SetAuthCookie(userView.Login, false);
                        return RedirectToAction("Welcome", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hasło jest nieprawidłowe.");
                    }
                }
            }

            return View(userView);  //wyswietlenie widoku z wpisanymi danymi
        }


        [Authorize] //atrybut(dekorator) ta metoda zadziała tylko dla użytkowników zalogowanych
        public ActionResult SignOut()   //wylogowywanie
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}