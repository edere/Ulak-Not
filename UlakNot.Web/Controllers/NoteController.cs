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
        private LikedManager likedManager = new LikedManager();

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

        public ActionResult MyLikedNotes()
        {
            var notes = likedManager.ListQueryable().Include("LikedUser").Include("Note").Where(
                x => x.LikedUser.Id == SessionManager.User.Id).Select(
                x => x.Note).Include("Hashtags").Include("Owner").OrderByDescending(
                x => x.CreatedDate);

            return View(notes.ToList());
        }

        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {
            if (SessionManager.User != null)
            {
                List<int> likedNoteIds = likedManager.List(
                    x => x.LikedUser.Id == SessionManager.User.Id && ids.Contains(x.Note.Id)).Select(
                    x => x.Note.Id).ToList();

                return Json(new { result = likedNoteIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }
        }

        [HttpPost]
        public ActionResult SetLikeState(int noteid, bool liked)
        {
            int res = 0;

            if (SessionManager.User == null)
                return Json(new { hasError = true, errorMessage = "Giriş yapmış kullanıcılar not beğenebilir!", result = 0 });

            UnLike like =
                likedManager.Find(x => x.Note.Id == noteid && x.LikedUser.Id == SessionManager.User.Id);

            UnNotes note = noteManager.Find(x => x.Id == noteid);

            if (like != null && liked == false)
            {
                res = likedManager.Delete(like);
            }
            else if (like == null && liked == true)
            {
                res = likedManager.Insert(new UnLike()
                {
                    LikedUser = SessionManager.User,
                    Note = note
                });
            }

            if (res > 0)
            {
                if (liked)
                {
                    note.LikeTotal++;
                }
                else
                {
                    note.LikeTotal--;
                }

                res = noteManager.Update(note);

                return Json(new { hasError = false, errorMessage = string.Empty, result = note.LikeTotal });
            }

            return Json(new { hasError = true, errorMessage = "Beğenme işlemi gerçekleştirilemedi.", result = note.LikeTotal });
        }

        public ActionResult GetNoteText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnNotes note = noteManager.Find(x => x.Id == id);

            if (note == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialNoteText", note);
        }
    }
}