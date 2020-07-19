using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplicationQLKS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationContext : IdentityUser
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationContext> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            // Add custom user claims here
            //var u = db.Users.Where(f => f.Email == model.Email).FirstOrDefault();
            //Session["User"] = u;
            return userIdentity;
        }

        
        [DisplayName("Địa chỉ")]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Address { get; set; }
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        [DisplayName("Họ tên")]        
        public string FullName { get; set; }
        [DisplayName("CMND")]        
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string IdentityNumber { get; set; }    
        [DisplayName("Quốc tịch")]        
        [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} kí tự và tối đa {1} kí tự.", MinimumLength = 1)]
        public string Nation { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationContext>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Room> Rooms { get; set; }        
        public DbSet<RoomStatus> RoomStatuse { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }        
        public DbSet<Order> Orders { get; set; }        
        public DbSet<Guest> Guests { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<Service> Servives { get; set; }
        //public DbSet<ServiceDetail> ServiceDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}