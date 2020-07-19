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
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payment
        public ActionResult Index()
        {
            var t = db.RoomTypes;
            var r = db.Rooms;

            //var pa = db.Payments.Where(p => p.OrderId == db.Orders.Where(o => o.Room.RoomType.TypeName == type));
            return View(db.Payments.ToList());
        }
        public ActionResult DanhThu()
        {
            var t = db.RoomTypes;
            var r = db.Rooms;
            ViewBag.TypeID = new SelectList(db.RoomTypes, "TypeID", "TypeName");

            //var pa = db.Payments.Where(p => p.OrderId == db.Orders.Where(o => o.Room.RoomType.TypeName == type).Select(o=>o.OrderId).FirstOrDefault());
            return View();
        }
        [HttpPost]
        public ActionResult DanhThu(Room ro)
        {
            var t = db.RoomTypes;
            //var r = db.Rooms;
            ViewBag.TypeID = new SelectList(db.RoomTypes, "TypeID", "TypeName");
            
            var type = db.Rooms.Where(r => r.RoomType.TypeID == ro.TypeID).Select(r => r.RoomType.TypeName).FirstOrDefault();
            //var sum = db.Payments.Sum(p => p.OrderId == db.Orders.Where(o => o.Room.RoomType.TypeName == type).Select(o => o.OrderId).FirstOrDefault());
            var pa = db.Payments.Where(p => p.OrderId == db.Orders.Where(o => o.Room.RoomType.TypeName == type).Select(o => o.OrderId).FirstOrDefault()).ToList();
            return View("ThongKe",pa);
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

        // GET: Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payment/Create
        public ActionResult Create(int? id)
        {
            //Payment payment = new Payment();
            //PaymentDetail paymentDetail = new PaymentDetail();
            //payment.TotalPay = 0;
            //Tìm mã đặt phòng cần thanh toán
            var order = db.Orders.Find(id);

            //lấy thông tin người đại diện
            if (order.OrderDetails.FirstOrDefault().ApplicationContext != null)
            {
                ViewBag.Customer = order.OrderDetails.FirstOrDefault().ApplicationContext;
            }
            if (order.OrderDetails.FirstOrDefault().Guest != null)
            {
                ViewBag.Customer = order.OrderDetails.FirstOrDefault().Guest;
            }

            //lấy thông tin phòng,giá,hệ cơ số,ngày thuê...
            double factor = 1;
            ViewBag.factor = factor;
            ViewBag.order = order;
            ViewBag.room = order.Room;
            ViewBag.price = order.Room.RoomType.Price;
            if (order.QuantityGuest > 2)
            {
                ViewBag.factor = factor + factor * 0.25;
            }

            bool flagIncrease = true;     
            foreach (var detail in order.OrderDetails)
            {
                if(detail.ApplicationContext!=null)
                {
                    if (detail.ApplicationContext.Nation != "VN")
                    {
                        ViewBag.factor = ViewBag.factor * 1.5;
                        flagIncrease = false;
                    }
                }
                if (detail.Guest != null)
                {
                    if (detail.Guest.Nation != "VN" && flagIncrease == true)
                    {
                        ViewBag.factor = ViewBag.factor * 1.5;
                        flagIncrease = false;
                    }
                }
            }      

            //tạo ngày checkout bằng ngày hiện tại nếu chưa có
            if (order.CheckOut == null)
            {
                order.CheckOut = DateTime.Now;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            //lấy số ngày thuê
            DateTime? cout = order.CheckOut;
            DateTime? cin = order.CheckIn;
            var day = cout - cin;
            TimeSpan t = new TimeSpan(day.Value.Ticks);
            var totalday = (double)t.TotalDays;
            var finalday = Math.Round(totalday);
            if (finalday < totalday)
            {
                finalday = finalday + 1;
            }
            //số ngày thuê
            ViewBag.day = finalday;
            ViewBag.sum = order.Room.RoomType.Price * ViewBag.factor * finalday;
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,CustomerId,TotalPay,Factor,OrderId")] Payment payment,int? id)
        {                                 
            var order = db.Orders.Find(id);
            var user = order.OrderDetails.FirstOrDefault().UserId;
            var guest = order.OrderDetails.FirstOrDefault().GuestId;
            if (user != null)
            {
                payment.CustomerId = user;
            }
            else
            {
                payment.CustomerId = guest.ToString();
            }
            payment.OrderId = order.OrderId;
            db.Payments.Add(payment);
            db.SaveChanges();          

            double factor = 1;
            if (ModelState.IsValid)
            {
                if (order.CheckOut == null)
                {
                    order.CheckOut = DateTime.Now;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                DateTime? cout = order.CheckOut;
                DateTime? cin = order.CheckIn;
                var day = cout - cin;
                TimeSpan t = new TimeSpan(day.Value.Ticks);
                var totalday = (double)t.TotalDays;
                var finalday = Math.Round(totalday);
                if (finalday < totalday)
                {
                    finalday = finalday + 1;
                }
                //paymentDetail.OrderId = order.OrderId;
                //paymentDetail.PaymentId = payment.PaymentId;
                bool flagInscrease = true;
                if (order.QuantityGuest > 2)
                {
                    factor = factor + factor* 0.25;
                    foreach (var detail in order.OrderDetails)
                    {
                        if (detail.ApplicationContext != null && flagInscrease == true)
                        {
                            if (detail.ApplicationContext.Nation != "VN")
                            {
                                factor = factor * 1.5;
                                flagInscrease = false;
                            }
                        }
                        if (detail.Guest != null)
                        {
                            if (detail.Guest.Nation != "VN" && flagInscrease == true)
                            {
                                factor = factor * 1.5;
                                flagInscrease = false;
                            }
                        }
                    }
                }

                payment.Factor = factor;
                var sum = order.Room.RoomType.Price * factor * finalday;
                payment.TotalPay = sum;
                order.Pay = true;
                db.Entry(payment).State = EntityState.Modified;
                db.Entry(order).State = EntityState.Modified;                
                db.SaveChanges();
                SetAlert("Thanh toán thành công", "success");
                return RedirectToAction("Index","Order");                
            }            
            return View(payment);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,CustomerId,TotalPay")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
