using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult CreateDatabase()
        {
            BusinessLayer.CreateDatabase dbc = new BusinessLayer.CreateDatabase();
            return View(dbc);
        }

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

        public ActionResult Department()
        {
            HashtagManager hm = new HashtagManager();

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

            return View(hm.GetHashtags());
        }

        public ActionResult SelectDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            UnCategories cat = cm.GetCategoryId(id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }
            return View("DepartmentId", cat.Hashtags);
        }

        public ActionResult DepartmentId()
        {
            HashtagManager hm = new HashtagManager();
            return View(hm.GetHashtags());
        }

        public ActionResult HashtagList()
        {
            return View();
        }

        public ActionResult Hashtag(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HashtagManager hm = new HashtagManager();
            UnHashtags hash = hm.HashtagId(id.Value);

            if (hash == null)
            {
                return HttpNotFound();
            }

            return View("Index", hash.Notes);
        }
    }
}