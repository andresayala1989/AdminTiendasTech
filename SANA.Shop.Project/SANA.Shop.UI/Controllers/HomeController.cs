using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SANA.Shop.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = System.Web.HttpContext.Current;
            ViewBag.sesion = context.Application["inMemory"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}