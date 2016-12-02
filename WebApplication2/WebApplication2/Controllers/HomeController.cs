using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}