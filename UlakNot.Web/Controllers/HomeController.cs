using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using UlakNot.BusinessLayer.Control;
using UlakNot.BusinessLayer.Results;
using UlakNot.Entity;
using UlakNot.Entity.UserObjects;
using UlakNot.Web.Models;

namespace UlakNot.Web.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private HashtagManager hashtagManager = new HashtagManager();
        private UserManager userManager = new UserManager();
        private FriendManager friendManager = new FriendManager();


        public ActionResult CreateDatabase()
        {
            BusinessLayer.CreateDatabase dbc = new BusinessLayer.CreateDatabase();
            return View(dbc);
        }

        public ActionResult ChangeLanguage(string LanguageAbbrevation, string redirectUrl)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return Redirect(redirectUrl);
        }

        // GET: Home
        public ActionResult Index()
        {
            //BusinessLayer.CreateDatabase dbc = new BusinessLayer.CreateDatabase();
            return View(noteManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList());
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
                ErrorResult<UnUsers> res = userManager.LoginUser(model);

                if (res.Error.Count > 0)
                {
                    res.Error.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

               
                var newSessionId = CreateSessionId(System.Web.HttpContext.Current);
                SetSessionId(System.Web.HttpContext.Current, newSessionId);
                Session["login"] = res.Result;
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            AbandonSession();
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-30);
            return RedirectToAction("Login");
        }

        public void AbandonSession()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
        }

        private static string CreateSessionId(HttpContext httpContext)
        {
            var manager = new SessionIDManager();

            string newSessionId = manager.CreateSessionID(httpContext);

            return newSessionId;
        }

        public static void SetSessionId(HttpContext httpContext, string newSessionId)
        {
            var manager = new SessionIDManager();

            bool redirected;
            bool cookieAdded;

            manager.SaveSessionID(httpContext, newSessionId, out redirected, out cookieAdded);

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
                ErrorResult<UnUsers> res = userManager.RegisterUser(model);

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

        public ActionResult Settings()
        {
            UnUsers currentUser = Session["Login"] as UnUsers;
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            ErrorResult<UnUsers> res = userManager.GetUserById(currentUser.Id);
            if (res.Error.Count > 0)
            {
                return RedirectToAction("Login");
            }

            return View(res.Result);
        }

        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notes = noteManager.ListQueryable().Include("Hashtags").Include("Owner").Where(
                x => x.Owner.Id == id).OrderByDescending(
                x => x.CreatedDate);

            return View(notes.ToList());
        }

        public ActionResult MyProfile()
        {
            var notes = noteManager.ListQueryable().Include("Hashtags").Include("Owner").Where(
                x => x.Owner.Id == SessionManager.User.Id).OrderByDescending(
                x => x.CreatedDate);

            return View(notes.ToList());
        }

        public ActionResult EditProfile()
        {
            UnUsers currentUser = Session["Login"] as UnUsers;
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            ErrorResult<UnUsers> res = userManager.GetUserById(currentUser.Id);
            if (res.Error.Count > 0)
            {
                return RedirectToAction("Login");
            }

            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(UnUsers user, HttpPostedFileBase ImageName)
        {
            if (ImageName != null &&
                (ImageName.ContentType == "image/jpeg" ||
                 ImageName.ContentType == "image/png" ||
                 ImageName.ContentType == "image/jpg"))
            {
                string filename = $"user_{user.Id}.{ImageName.ContentType.Split('/')[1]}";
                ImageName.SaveAs(Server.MapPath($"~/images/{filename}"));
                user.ImageName = filename;
            }

            ErrorResult<UnUsers> res = userManager.UpdateProfile(user);

            if (res.Error.Count > 0)
            {
                return RedirectToAction("Login");
            }

            Session["Login"] = res.Result;
            return RedirectToAction("Settings");
        }

        public ActionResult UserActivate(Guid id)
        {
            ErrorResult<UnUsers> user = userManager.ActivateUser(id);

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


        [HttpPost]
        public ActionResult Index(string txtSearch)
        {
            List<UnNotes> note;
            if (string.IsNullOrEmpty(txtSearch))
            {
                note = noteManager.ListQueryable().OrderByDescending(x => x.CreatedDate).ToList();
            }

            else
            {
                note = noteManager.ListQueryable().Where(x => x.Title.StartsWith(txtSearch)).OrderByDescending(x => x.CreatedDate).ToList();
            }

            return View(note.ToList());
        }

        public JsonResult GetSearchNotes(string txtSearch)
        {
            List<string> result = noteManager.ListQueryable().Where(x => x.Title.StartsWith(txtSearch)).Select(x => x.Title).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NoteDetail(int? id)
        {
            return PartialView(noteManager.Find(x => x.Id == id));
        }

        public ActionResult Department()
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

            return View(hashtagManager.List());
        }

        public ActionResult SelectDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnCategories cat = categoryManager.Find(x => x.Id == id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }
            return View("DepartmentId", cat.Hashtags);
        }

        public ActionResult DepartmentId()
        {
            return View(hashtagManager.List());
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

            UnHashtags hash = hashtagManager.Find(x => x.Id == id.Value);

            if (hash == null)
            {
                return HttpNotFound();
            }

            return View("Index", hash.Notes);
        }

        public ActionResult GetFriend(int[] ids)
        {
            List<int> getFriendIds = friendManager
                .List(x => x.UserId == SessionManager.User.Id && ids.Contains(x.FriendId)).Select(x => x.User.Id)
                .ToList();

            return Json(new { result = getFriendIds });
        }

        public ActionResult AddFriend(int userid, bool addi)
        {
            int res = 0;
            if (SessionManager.User == null)
                return Json(new { hasError = true, errorMessage = "Hata", result = 0 });

            UnFriend add = friendManager.Find(x => x.FriendId == userid && x.UserId == SessionManager.User.Id);
            UnUsers user = userManager.Find(x => x.Id == userid);

            if (add != null && addi == false)
            {
                res = friendManager.Delete(add);
            }
            else if (add == null && addi == true)
            {
                res = friendManager.Insert(new UnFriend()
                {
                    UserId = SessionManager.User.Id,
                    FriendId = user.Id
                });
            }

            if (res > 0)
            {
                return Json(new { hasError = false, errorMessage = string.Empty });
            }

            return Json(new
            {
                hasError = true,
                errorMessage = "Arkadaşlık isteği gönderilemedi. Hata!"
            });
        }

        public ActionResult ConfirmFriend(int userid, bool addi)
        {
            int res = 0;
            if (SessionManager.User == null)
                return Json(new { hasError = true, errorMessage = "Hata", result = 0 });

            UnFriend add = friendManager.Find(x => x.FriendId == userid && x.UserId == SessionManager.User.Id);
            UnUsers user = userManager.Find(x => x.Id == userid);

            if (add == null && addi == true)
            {
                res = friendManager.Update(new UnFriend()
                {
                    Status = true
                });
            }

            return Json(new

            {
                hasError = true,
                errorMessage = "Onaylandı."
            });
        }

        public ActionResult FriendList(int? id)
        {
            return View(friendManager.ListQueryable());
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(UnContact contact)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Name: " + contact.Name);
                body.AppendLine("Surname: " + contact.Surname);
                body.AppendLine("Mail: " + contact.Mail);
                body.AppendLine("Subject: " + contact.Subject);
                body.AppendLine("Message: " + contact.Message);

                //Mail.SendMail(body,ToString());
                ViewBag.Success = true;
            }

            return View();
        }
    }
}