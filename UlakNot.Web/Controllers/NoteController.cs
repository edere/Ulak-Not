using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UlakNot.BusinessLayer.Control;
using UlakNot.Entity;
using UlakNot.Web.Models;

namespace UlakNot.Web.Controllers
{
    public class NoteController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private HashtagManager hastagManager = new HashtagManager();

        // Kendi Notlarını Göster
        public ActionResult Index()
        {
            var notes = noteManager.ListQueryable().Include("Hashtags").Include("Owner")
                .Where(x => x.Owner.Id == SessionManager.User.Id).OrderByDescending(x => x.Id);
            return View(notes.ToList());
        }

        // Not Detay Görüntüleme
        public ActionResult Details(int? id)
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
        public ActionResult Create()
        {
            ViewBag.HashtagsId = new SelectList(hastagManager.List(), "Id", "Code");
            return View();
        }

        // Not Insert İşlemi Yapılıyor.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnNotes notes)
        {
            ModelState.Remove("UpdatedDate");
            ModelState.Remove("CreatedDate");
            ModelState.Remove("UpdatedUserName");
            if (ModelState.IsValid)
            {
                notes.Owner = SessionManager.User;
                noteManager.Insert(notes);
                return RedirectToAction("Index");
            }

            ViewBag.HashtagsId = new SelectList(hastagManager.List(), "Id", "Code", notes.HashtagsId);
            return View(notes);
        }

        // Not düzenleme işlemi(notun gösterildiği sayfaya gönderildiği aşama)
        public ActionResult Edit(int? id)
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
        public ActionResult Edit(UnNotes notes)
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

                return RedirectToAction("Index");
            }
            ViewBag.HashtagsId = new SelectList(hastagManager.List(), "Id", "Code", notes.HashtagsId);
            return View(notes);
        }

        // Silinecek kayıt gösterimi
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
        {
            UnNotes notes = noteManager.Find(x => x.Id == id);
            noteManager.Delete(notes);
            return RedirectToAction("Index");
        }
    }
}