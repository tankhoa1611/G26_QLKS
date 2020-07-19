using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class OrderDetailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IdentityUserRole userRole = new IdentityUserRole();

        // GET: OrderDetail
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.ApplicationContext).Include(o => o.Guest).Include(o => o.Order);
            return View(orderDetails.ToList());
        }

        public JsonResult GetUserList(string GuestPhone)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var user = db.Users.Where(u => u.PhoneNumber == GuestPhone).ToList();
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGuestList(string GuestPhone)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var guest = db.Guests.Where(u => u.PhoneNumber == GuestPhone).ToList();
            return Json(guest, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomerList(string GuestPhone)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var iden = User.Identity.GetUserId();
            var customer = db.Users.Where(u => u.PhoneNumber == GuestPhone && u.Id == iden).ToList();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        // GET: OrderDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetail/Create
        public ActionResult Create(int? id, DateTime? checkin, DateTime? checkout, int? quantity)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = db.Rooms.Find(id);

            ViewBag.quantity = quantity;
            ViewBag.checkin = checkin;
            ViewBag.checkout = checkout;
            ViewBag.RoomId = id;
            if (room == null)
            {
                return HttpNotFound();
            }
            var model = new OrderModel();
            model.ApplicationContexts = db.Users.ToList();
            model.Guests = db.Guests.ToList();
            return View(model);
        }

        private void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailId,OrderId,UserId,GuestId")] OrderDetail orderDetail, string name1, string name2, string name3, string nation1, string nation2, string nation3, string phone1, string phone2, string phone3, string cmnd1, string cmnd2, string cmnd3, string address1, string address2, string address3, DateTime? checkin, DateTime? checkout, int quantity, int id)
        {
            var room = db.Rooms.Find(id);
            Order order = new Order();

            order.RoomId = id;
            order.CheckIn = checkin;
            order.CheckOut = checkout;
            order.QuantityGuest = quantity;
            db.Orders.Add(order);
            db.SaveChanges();

            var roleid = db.Roles.Where(u => u.Name == "Guest").FirstOrDefault();
            var us = db.Users.Where(u => u.Roles.FirstOrDefault().RoleId == roleid.Id);

            var userone = db.Users.Where(o => o.PhoneNumber == phone1 && o.Roles.FirstOrDefault().RoleId == roleid.Id).FirstOrDefault();
            var guestone = db.Guests.Where(o => o.PhoneNumber == phone1).FirstOrDefault();
            var usertwo = db.Users.Where(o => o.PhoneNumber == phone2 && o.Roles.FirstOrDefault().RoleId == roleid.Id).FirstOrDefault();
            var guesttwo = db.Guests.Where(o => o.PhoneNumber == phone2).FirstOrDefault();
            var userthree = db.Users.Where(o => o.PhoneNumber == phone3 && o.Roles.FirstOrDefault().RoleId == roleid.Id).FirstOrDefault();
            var guestthree = db.Guests.Where(o => o.PhoneNumber == phone3).FirstOrDefault();


            var user = User.Identity.GetUserId();
            var odd = db.Orders.Where(o => o.OrderDetails.FirstOrDefault().ApplicationContext.Id == user);

            OrderDetail orderDetail2 = new OrderDetail();
            OrderDetail orderDetail3 = new OrderDetail();
            Guest guest = new Guest();
            Guest guest2 = new Guest();
            Guest guest3 = new Guest();

            if (ModelState.IsValid)
            {
                if (userone != null)
                {
                    orderDetail.OrderId = order.OrderId;
                    orderDetail.UserId = userone.Id;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                }
                if (guestone != null && userone == null)
                {
                    orderDetail.OrderId = order.OrderId;
                    orderDetail.GuestId = guestone.GuestId;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                }
                if (phone1 != null && guestone == null && userone == null)
                {
                    guest.FullName = name1;
                    guest.Nation = nation1;
                    guest.PhoneNumber = phone1;
                    guest.IdentityNumber = cmnd1;
                    guest.Address = address1;
                    db.Guests.Add(guest);
                    db.SaveChanges();

                    orderDetail.OrderId = order.OrderId;
                    orderDetail.GuestId = guest.GuestId;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                }

                if (usertwo != null)
                {
                    orderDetail2.OrderId = order.OrderId;
                    orderDetail2.UserId = usertwo.Id;
                    db.OrderDetails.Add(orderDetail2);
                    db.SaveChanges();
                }
                if (guesttwo != null && usertwo == null)
                {
                    orderDetail2.OrderId = order.OrderId;
                    orderDetail2.GuestId = guesttwo.GuestId;
                    db.OrderDetails.Add(orderDetail2);
                    db.SaveChanges();
                }
                if (phone2 != null && guesttwo == null && usertwo == null)
                {
                    guest2.FullName = name2;
                    guest2.Nation = nation2;
                    guest2.PhoneNumber = phone2;
                    guest2.IdentityNumber = cmnd2;
                    guest2.Address = address2;
                    db.Guests.Add(guest2);
                    db.SaveChanges();

                    orderDetail2.OrderId = order.OrderId;
                    orderDetail2.GuestId = guest2.GuestId;
                    db.OrderDetails.Add(orderDetail2);
                    db.SaveChanges();
                }

                if (userthree != null)
                {
                    orderDetail3.OrderId = order.OrderId;
                    orderDetail3.UserId = userthree.Id;
                    db.OrderDetails.Add(orderDetail3);
                    db.SaveChanges();
                }
                if (guestthree != null && userthree == null)
                {
                    orderDetail3.OrderId = order.OrderId;
                    orderDetail3.GuestId = guestthree.GuestId;
                    db.OrderDetails.Add(orderDetail3);
                    db.SaveChanges();
                }
                if (phone3 != null && guestthree == null && userthree == null)
                {
                    guest3.FullName = name3;
                    guest3.Nation = nation3;
                    guest3.PhoneNumber = phone3;
                    guest3.IdentityNumber = cmnd3;
                    guest3.Address = address3;
                    db.Guests.Add(guest3);
                    db.SaveChanges();

                    orderDetail3.OrderId = order.OrderId;
                    orderDetail3.GuestId = guest3.GuestId;
                    db.OrderDetails.Add(orderDetail3);
                    db.SaveChanges();
                }
                if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                {
                    order.Confirm = true;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    SetAlert("Đặt phòng thành công", "success");
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    order.Confirm = false;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    SetAlert("Đặt phòng thành công", "success");
                    return RedirectToAction("SoDoPhong", "HomeAdmin");
                }                             
            }
            return View(orderDetail);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: OrderDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuestId = new SelectList(db.Guests, "GuestId", "Address", orderDetail.GuestId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // POST: OrderDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailId,OrderId,GuestId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestId = new SelectList(db.Guests, "GuestId", "Address", orderDetail.GuestId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
