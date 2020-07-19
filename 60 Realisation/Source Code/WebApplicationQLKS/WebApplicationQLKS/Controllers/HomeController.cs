using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationQLKS.Models;

namespace WebApplicationQLKS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Auto()
        {
            var model = new OrderModel();
            model.ApplicationContexts = db.Users.ToList();
            model.Guests = db.Guests.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auto([Bind(Include = "GuestId,FullName")] Guest guest, int? id, string i1)
        {
            var test = guest.GuestId;
            var model = new OrderModel();
            //model.ApplicationContext = db.Users.SingleOrDefault();
            //model.ApplicationContexts = db.Users;
            //var u = db.Users;
            //model.ApplicationContext = db.Users;
            //model.Order = db.Orders;
            //model.OrderDetails = db.OrderDetails.ToList();
            //model.Orders = db.Orders.ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}