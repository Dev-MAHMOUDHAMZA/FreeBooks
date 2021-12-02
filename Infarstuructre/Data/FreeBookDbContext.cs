using Domin.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.Data
{
   public class FreeBookDbContext :IdentityDbContext<ApplicationUser>
    {
        public FreeBookDbContext(DbContextOptions<FreeBookDbContext> options ) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<IdentityUser>().ToTable("Users","Identity");
            //builder.Entity<IdentityRole>().ToTable("Roles", "Identity");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Identity");
            //builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Identity");
            //builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Identity");
            //builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Identity");
            //builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Identity");

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LogCategory> LogCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<LogSubCategory> LogSubCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<LogBook> LogBooks { get; set; }
    }
}
