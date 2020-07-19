using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationQLKS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Ajax.Utilities;
using System.Web.Security;
using WebGrease.Css.Extensions;
using System.Threading;

namespace WebApplicationQLKS.Controllers
{
    public class OrderController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private IdentityUserRole userRole = new IdentityUserRole();


        // GET: Order
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Room).Include(o=>o.OrderDetails);                        
            return View(orders.ToList());
        }

        


        [Authorize(Roles = "Admin,Employee,Guest")]
        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderDetail = new SelectList(db.OrderDetails.Where(s => s.OrderId == order.OrderId).Select(s => s.OrderDetailId), "OrderDetailId");
            return View(order);
        }        
        [Authorize(Roles = "Admin,Employee,Guest")]
        // GET: Order/Create
        public ActionResult Create(int? id)
        {            
            if(id != null)
            {
                ViewBag.RoomId = db.Rooms.Where(r => r.StatusID == 1 && r.RoomId == id);                
                //ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.StatusID == 1 && r.RoomId == id), "RoomId", "RoomName");
            }
            else
            {
                ViewBag.RoomId = db.Rooms.Where(r => r.StatusID == 1);
                //ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.StatusID == 1 && r.RoomId == id), "RoomId", "RoomName");
            }
            ViewBag.date = DateTime.Now;
            Order order = new Order();     
            return View();
        }
        [Authorize(Roles = "Admin,Employee,Guest")]
        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,RoomId,QuantityGuest,Confirm,Pay,CheckIn,CheckOut")] Order order,int? id)
        {            
            //ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.StatusID == 1), "RoomId", "RoomName", id);
            ViewBag.RoomId = db.Rooms.Where(r => r.StatusID == 1 && r.RoomId == id);
            if (ModelState.IsValid)
            {       
                if(order.CheckIn == null)
                {                    
                    order.CheckIn = DateTime.Now;
                }          
                return RedirectToAction("Create","OrderDetail",new { id = id, checkin = order.CheckIn,checkout = order.CheckOut, quantity = order.QuantityGuest});
            }
            return View(order);          
        }
        
        [Authorize(Roles = "Admin,Employee")]
        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order.CheckIn == null)
            {
                order.CheckIn = db.Orders.Where(o => o.OrderId == id).Select(o => o.CheckIn).FirstOrDefault();
            }
            if (order == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.Users.Where(o => o.FullName == order.ApplicationContext.FullName), "Id", "FullName");           
            //ViewBag.UserEmail = new SelectList(db.Users.Where(o => o.Email == order.ApplicationContext.Email), "Id", "Email");
            //ViewBag.UserPhone = new SelectList(db.Users.Where(o => o.PhoneNumber == order.ApplicationContext.PhoneNumber), "Id", "PhoneNumber");
            //c2 ViewBag.UserPhone = new SelectList(db.Users.Where(o => o.PhoneNumber == order.ApplicationContext.PhoneNumber), "Id", "PhoneNumber");
            ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.StatusID == 1|| r.RoomId == order.RoomId), "RoomId", "RoomId", order.RoomId);

            return View(order);
        }

        [Authorize(Roles = "Admin,Employee")]
        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderDateTime,RoomId,QuantityGuest,CheckIn,CheckOut")] Order order,int? id)
        {
            //order.CheckIn = db.Orders.Where(o => o.OrderId == id).Select(o => o.CheckIn).FirstOrDefault();            
            if (ModelState.IsValid)
            {
                if (order.CheckIn == null)
                {
                    order.CheckIn = db.Orders.Where(o => o.OrderId == id).Select(o => o.CheckIn).FirstOrDefault();
                }
                order.Confirm = true;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.StatusID == 1 || r.RoomId == order.RoomId ), "RoomId", "RoomId", order.RoomId);
            return View(order);
        }

        [Authorize(Roles = "Admin,Employee")]
        // GET: Order/Edit/5
        public ActionResult Confirm(int? id,int? idd)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            ViewBag.checkin = order.CheckIn;
            ViewBag.checkout = order.CheckOut;         
            if (order == null)
            {
                return HttpNotFound();
            }            
            if(idd != null)
            {
                ViewBag.RoomId = new SelectList(db.Rooms.Where(r =>r.RoomStatus.StatusID == 1 || r.RoomId == order.RoomId), "RoomId", "RoomName");
            }
            else
            {
                ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.RoomId == order.RoomId), "RoomId", "RoomName");
            }
            return View(order);
        }

        [Authorize(Roles = "Admin,Employee")]
        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "OrderId,OrderDateTime,RoomId,QuantityGuest,CheckIn,CheckOut")] Order order,int? id)
        {            
            if (ModelState.IsValid)
            {
                if (order.CheckIn == null)
                {
                    order.CheckIn = db.Orders.Where(o => o.OrderId == id).Select(o => o.CheckIn).FirstOrDefault();
                }
                if (order.CheckOut == null)
                {
                    order.CheckOut = db.Orders.Where(o => o.OrderId == id).Select(o => o.CheckOut).FirstOrDefault();
                }
                order.Confirm = true;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.Users.Where(o => o.FullName == order.ApplicationContext.FullName), "Id", "FullName");
            ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.RoomId == order.RoomId), "RoomId", "RoomName", order.RoomId);
            return View(order);
        }

        [Authorize(Roles = "Admin,Employee")]
        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [Authorize(Roles = "Admin,Employee")]
        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
