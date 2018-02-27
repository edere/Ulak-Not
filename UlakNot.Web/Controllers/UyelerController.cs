using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UlakNot.Web.Controllers
{
    public class UyelerController : Controller
    {
        // GET: Uyeler
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Messages()
        {
            return View();
        }


    }
}