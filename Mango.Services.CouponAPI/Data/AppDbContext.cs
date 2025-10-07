using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        // Pass constructor arguments over to base class - Default setting required by Entity Framework Core
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        // Used to Query and Save instances of Coupon to database. Name of the DbSet will be the name of the table in the database
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Base method call is generally recommended
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10OFF",
                DiscountAmount = 10.0,
                MinAmount = 20
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "20OFF",
                DiscountAmount = 20.0,
                MinAmount = 40
            });
        }
    }
}
