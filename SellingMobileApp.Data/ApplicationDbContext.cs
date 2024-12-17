using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SellingMobileApp;
using SellingMobileApp.Data.Models;
using System.Reflection.Emit;

namespace SellingMobileApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User> { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CreateListing>()
        .HasMany(cl => cl.Reviews)
        .WithOne(r => r.CreateListing)
        .HasForeignKey(r => r.ListingId)
        .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CreateListing>()
                .HasOne(c => c.DeviceType)              
                .WithMany(d => d.Listings)             
                .HasForeignKey(c => c.DeviceTypeId)    
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().ToTable("Users");

            builder.Entity<User>().ToTable("Users").Property(u => u.Name).IsRequired();

            builder.Entity<UserCreateListing>()
                .HasKey(uc => new { uc.UserId, uc.ListingId });

            builder.Entity<CreateListing>()
                .HasKey(cl => cl.Id);

            builder.Entity<CreateListing>()
                .HasOne(cl => cl.User)
                .WithMany(u => u.Listings)
                .HasForeignKey(cl => cl.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CreateListing>()
                .HasOne(cl => cl.PhoneModel)
                .WithMany(p => p.CreateListings) 
                .HasForeignKey(cl => cl.PhoneCharacteristicId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne(r => r.CreateListing)
                .WithMany()
                .HasForeignKey(r => r.ListingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CreateListing>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Category>()
                .HasData(
                new Category { Id = 1, Name = "Нов" },
                new Category { Id = 2, Name = "Употребяван" });

            builder.Entity<DeviceType>()
           .HasData(
               new DeviceType { Id = 1, Type = "Телефон" },
               new DeviceType { Id = 2, Type = "Таблет" },
               new DeviceType { Id = 3, Type = "Аксесоари" }
           );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KALOYAN\\SQLEXPRESS01;Database=SellingMobileAppWeb;Integrated Security=True;Encrypt=False",
                    x => x.MigrationsAssembly("SellingMobileApp.Web"));
            }
        }

        public virtual DbSet<User> АppUsers { get; set; }

        public virtual DbSet<CreateListing> CreateListings { get; set; }

        public virtual DbSet<PhoneModel> PhoneModels { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<UserCreateListing> UsersCreateListings { get; set; }
    
        public virtual DbSet<DeviceType> DeviceTypes { get; set; }
    }
}
