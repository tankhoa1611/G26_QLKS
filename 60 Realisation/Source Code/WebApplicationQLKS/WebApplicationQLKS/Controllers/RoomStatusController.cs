using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationQLKS.Models;

namespace WebApplicationQLKS.Controllers
{
    public class RoomStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoomStatus
        public ActionResult Index()
        {
            return View(db.RoomStatuse.ToList());
        }

        // GET: RoomStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomStatus roomStatus = db.RoomStatuse.Find(id);
            if (roomStatus == null)
            {
                return HttpNotFound();                
            }
            return View(roomStatus);
        }

        // GET: RoomStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusID,StatusName,StatusColor")] RoomStatus roomStatus)
        {
            if (ModelState.IsValid)
            {
                db.RoomStatuse.Add(roomStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomStatus);
        }

        // GET: RoomStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomStatus roomStatus = db.RoomStatuse.Find(id);
            if (roomStatus == null)
            {
                return HttpNotFound();
            }
            return View(roomStatus);
        }

        // POST: RoomStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusID,StatusName,StatusColor")] RoomStatus roomStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomStatus);
        }

        // GET: RoomStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomStatus roomStatus = db.RoomStatuse.Find(id);
            if (roomStatus == null)
            {
                return HttpNotFound();
            }
            return View(roomStatus);
        }

        // POST: RoomStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomStatus roomStatus = db.RoomStatuse.Find(id);
            db.RoomStatuse.Remove(roomStatus);
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
