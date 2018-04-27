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
    public class AdministratorController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();

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
    }
}