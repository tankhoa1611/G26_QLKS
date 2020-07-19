using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationQLKS.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace WebApplicationQLKS.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: HomeAdmin
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult SoDoPhong()
        {            
            var model = new RoomModel();
            var u = db.Users;
            model.Rooms = db.Rooms.Include(t => t.RoomType).Include(s => s.RoomStatus).ToList();
            model.ApplicationContexts = db.Users.ToList();
            model.OrderDetails = db.OrderDetails.ToList();
            model.Orders = db.Orders.ToList();

            var user = User.Identity.GetUserId();
            var odd = db.Orders.Where(o => o.OrderDetails.FirstOrDefault().ApplicationContext.Id == user);            
            int roomorder = 0;
            foreach (var item in odd)
            {
                if (item.Pay == false)
                {
                    roomorder++;
                }
            }
            ViewBag.ordercount = roomorder;

            return View(model);
        }

        [Authorize(Roles ="Admin")]        
        public ActionResult ThemNhanVien()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ThemNhanVien(EmployeeModels emp)
        {            
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();                
                var UserManager = new UserManager<ApplicationContext>(new UserStore<ApplicationContext>(context));
                var user = new ApplicationContext { UserName = emp.Email, Email = emp.Email, Address = emp.Address, FullName = emp.FullName, IdentityNumber = emp.IdentityNumber, PhoneNumber = emp.PhoneNumber };
                var result = await UserManager.CreateAsync(user, emp.Password);
                if (result.Succeeded)
                {
                    //var u = db.Users.Where(f => f.Email == emp.Email).FirstOrDefault();
                    //Session["User"] = u;                    
                    UserManager.AddToRole(user.Id, emp.Role);              
                    return RedirectToAction("Index", "Home");
                }           
            }     
            return View(emp);
        }
        //private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Room
        //public ActionResult Index()
        //{
        //    var rooms = db.Rooms.Include(r => r.RoomStatus).Include(r => r.RoomType);
        //    return View(rooms.ToList());
        //}
        public ActionResult Index()
        {
            return View();
        }
    }
}