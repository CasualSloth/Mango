using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> couponList = _dbContext.Coupons.ToList();
                return couponList;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(u => u.CouponId == id);
                return coupon;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}