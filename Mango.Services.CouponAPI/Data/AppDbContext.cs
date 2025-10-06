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
    }
}
