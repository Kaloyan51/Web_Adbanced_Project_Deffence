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

            
            builder.Entity<User>().ToTable("Users");

            // Задаване на прост ключ за CreateListing
            builder.Entity<CreateListing>()
                .HasKey(cl => cl.Id); // Слагаме основен ключ за CreateListing

            // Връзка CreateListing -> User
            builder.Entity<CreateListing>()
                .HasOne(cl => cl.Owner)
                .WithMany(u => u.Listings)
                .HasForeignKey(cl => cl.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Връзка CreateListing -> PhoneModel
            builder.Entity<CreateListing>()
                .HasOne(cl => cl.PhoneCharacteristics)
                .WithMany() // Ако искате да имате обратно множество, добавете 'WithMany(p => p.CreateListings)'
                .HasForeignKey(cl => cl.PhoneCharacteristicsId)
                .OnDelete(DeleteBehavior.NoAction);

            // Връзка Review -> User
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Връзка Review -> CreateListing
            builder.Entity<Review>()
                .HasOne(r => r.CreateListing)
                .WithMany() // Може да добавите обратно множество, ако искате да имате рецензии за конкретни обяви
                .HasForeignKey(r => r.ListingId)
                .OnDelete(DeleteBehavior.NoAction);

            // Настройка на типа на Price в CreateListing
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

        public virtual DbSet<User> АppUsers { get; set; }

        public virtual DbSet<CreateListing> CreateListings { get; set; }

        public virtual DbSet<PhoneModel> PhoneModels { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }
    }
}
