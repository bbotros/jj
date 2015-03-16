using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JJTrailer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<JJTrailer.Library.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.Product> Products { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.ImageLib> ImageLibs { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.Carousel> Carousels { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.Career> Careers { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.JobCategory> JobCategories { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.JobLocation> JobLocations { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.Resume> Resumes { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<JJTrailer.Library.DepartmentMenu> DepartmentMenus { get; set; }
    }
}