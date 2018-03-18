using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UlakNot.BusinessLayer.Control;
using UlakNot.BusinessLayer.Results;
using UlakNot.Entity;
using UlakNot.Entity.UserObjects;

namespace UlakNot.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                ErrorResult<UnUsers> res = um.LoginUser(model);

                if (res.Error.Count > 0)
                {
                    res.Error.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                Session["login"] = res.Result;
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                ErrorResult<UnUsers> res = um.RegisterUser(model);

                if (res.Error.Count > 0)
                {
                    res.Error.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult UserAction(Guid g_id)
        {
            return View();
        }

        public ActionResult PasswordReset()
        {
            return View();
        }

        // GET: Home
        public ActionResult Index()
        {
            NoteManager nm = new NoteManager();

            //BusinessLayer.CreateDatabase dbc = new BusinessLayer.CreateDatabase();
            return View(nm.GetNotes());
        }

        public ActionResult Department(int? id)
        {
            //if (id == null)
            //{
            //    //todo: 404 hata sayfası gerçekleştir
            //}

            //CategoryManager cm = new CategoryManager();
            //UnCategories cat = cm.GetCategoryId(id);

            //if (cat == null)
            //{
            //    //todo: 404 sayfası gerçekleştir
            //}

            return View();
        }

        public ActionResult Hashtag(int id)
        {
            HashtagManager hm = new HashtagManager();
            UnHashtags hash = hm.HashtagId(id);

            return View("Index", hash.Notes);
        }
    }
}