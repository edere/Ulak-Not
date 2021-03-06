﻿using System;
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
using UlakNot.Web.Models;

namespace UlakNot.Web.Controllers
{
    public class AdministratorController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();
        private HashtagManager hashtagManager = new HashtagManager();
        private NoteManager noteManager = new NoteManager();
        private HashtagManager hastagManager = new HashtagManager();
        private LikedManager likedManager = new LikedManager();
        private BagManager bagManager = new BagManager();


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

        public ActionResult Note()
        {
            var notes = noteManager.ListQueryable().Include("Hashtags").Include("Owner")
                .OrderByDescending(x => x.Id);
            return View(notes.ToList());
        }

        // Not Detay Görüntüleme
        public ActionResult NDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnNotes notes = noteManager.Find(x => x.Id == id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // Not oluşturulurken hashtag'de seçildiğinden hashtag bilgileri dolduruluyor(combobox vs'ye doldur)
        public ActionResult NCreate()
        {
            ViewBag.HashtagsId = new SelectList(hastagManager.List(), "Id", "Code");
            return View();
        }

        // Not Insert İşlemi Yapılıyor.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NCreate(UnNotes notes)
        {
            ModelState.Remove("UpdatedDate");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedUserName");
            if (ModelState.IsValid)
            {
                notes.Owner = SessionManager.User;
                noteManager.Insert(notes);
                return RedirectToAction("Note");
            }

            ViewBag.HashtagsId = new SelectList(hastagManager.List(), "Id", "Code", notes.HashtagsId);
            return View(notes);
        }

        // Not düzenleme işlemi(notun gösterildiği sayfaya gönderildiği aşama)
        public ActionResult NEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnNotes notes = noteManager.Find(x => x.Id == id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            ViewBag.HashtagsId = new SelectList(hastagManager.List(), "Id", "Code", notes.HashtagsId);
            return View(notes);
        }

        // Not düzenleme Post işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NEdit(UnNotes notes)
        {
            ModelState.Remove("UpdatedDate");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedUserName");
            if (ModelState.IsValid)
            {
                UnNotes unote = noteManager.Find(x => x.Id == notes.Id);
                unote.Draft = notes.Draft;
                unote.HashtagsId = notes.HashtagsId;
                unote.Text = notes.Text;
                unote.Title = notes.Title;

                noteManager.Update(unote);

                return RedirectToAction("Note");
            }
            ViewBag.HashtagsId = new SelectList(hastagManager.List(), "Id", "Code", notes.HashtagsId);
            return View(notes);
        }

        // Silinecek kayıt gösterimi
        public ActionResult NDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnNotes notes = noteManager.Find(x => x.Id == id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // Not silme işlemi
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult NDeleteConfirmed(int id)
        {
            UnNotes notes = noteManager.Find(x => x.Id == id);
            noteManager.Delete(notes);
            return RedirectToAction("Note");
        }
    }
}