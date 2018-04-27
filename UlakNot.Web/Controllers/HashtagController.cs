using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using UlakNot.BusinessLayer.Control;
using UlakNot.Entity;
using UlakNot.Web.Models;

namespace UlakNot.Web.Controllers
{
    public class HashtagController : Controller
    {
        private HashtagManager hashtagManager = new HashtagManager();
        private CategoryManager categoryManager = new CategoryManager();

        public ActionResult Index()
        {
            var hashtag = hashtagManager.ListQueryable().Include("Categories");
            return View(hashtag.ToList());
        }

        public ActionResult Details(int? id)
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

        public ActionResult Create()
        {
            ViewBag.CategoriesId = new SelectList(categoryManager.List(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnHashtags hashtag)
        {
            ModelState.Remove("HashtagUser");
            ModelState.Remove("HashtagUser_Id");
            if (ModelState.IsValid)
            {
                hashtagManager.Insert(hashtag);
                return RedirectToAction("Index");
            }

            ViewBag.CategoriesId = new SelectList(hashtagManager.List(), "Id", "Name", hashtag.CategoriesId);
            return View(hashtag);
        }

        public ActionResult Edit(int? id)
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
        public ActionResult Edit(UnHashtags hashtag)
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
                return RedirectToAction("Index");
            }

            ViewBag.CategoriesId = new SelectList(hashtagManager.List(), "Id", "Name", hashtag.CategoriesId);

            return View(hashtag);
        }

        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
        {
            UnHashtags hashtag = hashtagManager.Find(x => x.Id == id);
            hashtagManager.Delete(hashtag);
            return RedirectToAction("Index");
        }
    }
}