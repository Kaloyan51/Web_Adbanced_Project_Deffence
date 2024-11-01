using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SellingMobileApp;
using SellingMobileApp.Data.Models;
using System.Reflection.Emit;

namespace SellingMobileApp.Data
{
    public class ApplicationDbContext : IdentityDbContext { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CreateListing>()
         .Property(c => c.Price)
         .HasColumnType("decimal(18,2)");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KALOYAN\\SQLEXPRESS01;Database=SellingMobileAppWeb;Integrated Security=True;Encrypt=False",
                    x => x.MigrationsAssembly("SellingMobileApp.Web"));
            }
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<CreateListing> CreateListings { get; set; }
    }
}
