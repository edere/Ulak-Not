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

                return RedirectToAction("RegisterOk");
            }

            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            UnUsers currentUser = Session["Login"] as UnUsers;
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }
            UserManager um = new UserManager();
            ErrorResult<UnUsers> res = um.GetUserById(currentUser.Id);
            if (res.Error.Count > 0)
            {
                return RedirectToAction("Login");
            }

            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            UnUsers currentUser = Session["Login"] as UnUsers;
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }
            UserManager um = new UserManager();
            ErrorResult<UnUsers> res = um.GetUserById(currentUser.Id);
            if (res.Error.Count > 0)
            {
                return RedirectToAction("Login");
            }

            return View(res.Result);
        }

        public ActionResult UserActivate(Guid id)
        {
            UserManager um = new UserManager();
            ErrorResult<UnUsers> user = um.ActivateUser(id);

            if (user.Error.Count > 0)
            {
                TempData["errors"] = user.Error;
                return RedirectToAction("UserActivateError");
            }

            return RedirectToAction("UserActivateOk");
        }

        public ActionResult UserActivateOk(Guid id)
        {
            return View();
        }

        public ActionResult UserActivateError()
        {
            List<string> errors = null;
            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<string>;
            }
            return View(errors);
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
            return View(nm.GetNotes().OrderByDescending(x => x.CreatedDate).ToList());
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