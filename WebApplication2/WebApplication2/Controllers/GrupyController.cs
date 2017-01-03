using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class GrupyController : Controller
    {
        // GET: Grupy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult zarzadzaj()
        {
            return View();
        }
    }
}