using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public CouponAPIController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> couponList = _dbContext.Coupons.ToList();
                var couponDtoList = _mapper.Map<IEnumerable<CouponDto>>(couponList);
                _response.Result = couponDtoList;
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(u => u.CouponId == id);
                var couponDto = _mapper.Map<CouponDto>(coupon);
                _response.Result = couponDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}