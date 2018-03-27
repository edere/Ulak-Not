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

namespace UlakNot.Web.Controllers
{
    public class NoteController : Controller
    {
        private NoteManager noteManager = new NoteManager();

        // Kendi Notlarını Göster
        public ActionResult Index()
        {
            var unNotes = db.UnNotes.Include(u => u.Hashtags);
            return View(unNotes.ToList());
        }

        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnNotes unNotes = db.UnNotes.Find(id);
            if (unNotes == null)
            {
                return HttpNotFound();
            }
            return View(unNotes);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            ViewBag.HashtagsId = new SelectList(db.UnHashtags, "Id", "Code");
            return View();
        }

        // POST: Note/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Draft,LikeTotal,HashtagsId,UpdatedUserName,CreatedDate,UpdatedDate")] UnNotes unNotes)
        {
            if (ModelState.IsValid)
            {
                db.UnNotes.Add(unNotes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HashtagsId = new SelectList(db.UnHashtags, "Id", "Code", unNotes.HashtagsId);
            return View(unNotes);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnNotes unNotes = db.UnNotes.Find(id);
            if (unNotes == null)
            {
                return HttpNotFound();
            }
            ViewBag.HashtagsId = new SelectList(db.UnHashtags, "Id", "Code", unNotes.HashtagsId);
            return View(unNotes);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Draft,LikeTotal,HashtagsId,UpdatedUserName,CreatedDate,UpdatedDate")] UnNotes unNotes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unNotes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HashtagsId = new SelectList(db.UnHashtags, "Id", "Code", unNotes.HashtagsId);
            return View(unNotes);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnNotes unNotes = db.UnNotes.Find(id);
            if (unNotes == null)
            {
                return HttpNotFound();
            }
            return View(unNotes);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnNotes unNotes = db.UnNotes.Find(id);
            db.UnNotes.Remove(unNotes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}