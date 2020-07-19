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
    [Authorize(Roles ="Admin")]
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Room
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.RoomStatus).Include(r => r.RoomType);
            //var rooms = db.Rooms;
            
            return View(rooms.ToList());
        }

        // GET: Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            //ViewBag.minroom = db.Parameters.Where(p=>p.ParameterName == "minroom").FirstOrDefault().ParameterValue;
            ViewBag.StatusID = new SelectList(db.RoomStatuse, "StatusID", "StatusName");
            ViewBag.TypeID = new SelectList(db.RoomTypes, "TypeID", "TypeName");
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,RoomName,TypeID,StatusID")] Room room)
        {
            //int c = db.Rooms.Count();
            //var d = db.Rooms.Select(r => r.RoomName).ElementAt(c);
            if (db.Rooms.Where(r =>r.RoomName == room.RoomName).FirstOrDefault() != null)
            {
                ModelState.AddModelError("ExistName", "Room Name  is exist, use other name");
                ViewBag.StatusID = new SelectList(db.RoomStatuse, "StatusID", "StatusName");
                ViewBag.TypeID = new SelectList(db.RoomTypes, "TypeID", "TypeName");
                return View(room);
            }
            if (ModelState.IsValid)
            {
                
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.RoomStatuse, "StatusID", "StatusName");
            ViewBag.TypeID = new SelectList(db.RoomTypes, "TypeID", "TypeName");            
            return View(room);            
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.RoomStatuse, "StatusID", "StatusName", room.StatusID);
            ViewBag.TypeID = new SelectList(db.RoomTypes, "TypeID", "TypeName", room.TypeID);
            return View(room);  
        }        

        // POST: Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,RoomName,TypeID,StatusID")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.RoomStatuse, "StatusID", "StatusName", room.StatusID);
            ViewBag.TypeID = new SelectList(db.RoomTypes, "TypeID", "TypeName", room.TypeID);
            return View(room);
        }        

        // GET: Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
