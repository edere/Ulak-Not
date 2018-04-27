using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class AdministratorController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();
        private HashtagManager hashtagManager = new HashtagManager();
        

        // GET: Administrator
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
                return RedirectToAction("Index", "Administrator");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            return View(categoryManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnCategories category = categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnCategories category)
        {
            ModelState.Remove("UpdatedDate");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedUserName");
            if (ModelState.IsValid)
            {
                categoryManager.Insert(category);

                return RedirectToAction("Category");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnCategories category = categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UnCategories category)
        {
            ModelState.Remove("UpdatedDate");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedUserName");
            if (ModelState.IsValid)
            {
                UnCategories cat = categoryManager.Find(x => x.Id == category.Id);
                cat.Name = category.Name;
                cat.Description = category.Description;

                categoryManager.Update(cat);

                return RedirectToAction("Category");
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnCategories category = categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnCategories category = categoryManager.Find(x => x.Id == id);
            categoryManager.Delete(category);
            return RedirectToAction("Category");
        }

        public ActionResult Hashtag()
        {
            var hashtag = hashtagManager.ListQueryable().Include("Categories");
            return View(hashtag.ToList());
        }

        public ActionResult HDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnHashtags hashtag = hashtagManager.Find(x => x.Id == id.Value);
            if (hashtag == null)
            {
                return HttpNotFound();
            }
            return View(hashtag);
        }

        public ActionResult HCreate()
        {
            ViewBag.CategoriesId = new SelectList(categoryManager.List(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HCreate(UnHashtags hashtag)
        {
            ModelState.Remove("HashtagUser");
            ModelState.Remove("HashtagUser_Id");
            if (ModelState.IsValid)
            {
                hashtagManager.Insert(hashtag);
                return RedirectToAction("Hashtag");
            }

            ViewBag.CategoriesId = new SelectList(hashtagManager.List(), "Id", "Name", hashtag.CategoriesId);
            return View(hashtag);
        }

        public ActionResult HEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnHashtags hashtag = hashtagManager.Find(x => x.Id == id);

            if (hashtag == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoriesId = new SelectList(hashtagManager.List(), "Id", "Name", hashtag.CategoriesId);
            return View(hashtag);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HEdit(UnHashtags hashtag)
        {
            ModelState.Remove("HashtagUser");
            ModelState.Remove("HashtagUser_Id");
            if (ModelState.IsValid)
            {
                UnHashtags unhastag = hashtagManager.Find(x => x.Id == hashtag.Id);
                unhastag.Code = hashtag.Code;
                unhastag.Description = hashtag.Description;
                //unhastag.CategoriesId = hashtag.CategoriesId;
                hashtagManager.Update(unhastag);
                return RedirectToAction("Hashtag");
            }

            ViewBag.CategoriesId = new SelectList(hashtagManager.List(), "Id", "Name", hashtag.CategoriesId);

            return View(hashtag);
        }

        public ActionResult HDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnHashtags hashtag = hashtagManager.Find(x => x.Id == id);
            if (hashtag == null)
            {
                return HttpNotFound();
            }
            return View(hashtag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult HDeleteConfirmed(int id)
        {
            UnHashtags hashtag = hashtagManager.Find(x => x.Id == id);
            hashtagManager.Delete(hashtag);
            return RedirectToAction("Hashtag");
        }
    }
}