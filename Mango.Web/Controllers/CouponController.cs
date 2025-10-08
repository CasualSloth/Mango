using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        // Invoked when our item is pressed in the dropdown
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            if (response != null && response.IsSuccessful && response.Result != null)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result)!);
            }

            return View(list); // Returns the list to the view (CouponIndex.cshtml - Name of this method) for display
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);
                if (response != null && response.IsSuccessful && response.Result != null)
                {
                    return RedirectToAction(nameof(CouponIndex)); // Basically calls CouponIndex so we can see the new Coupon
                }
            }

            return View(model); // Return to the Create page with our current model
        }
        
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);
            if (response != null && response.IsSuccessful && response.Result != null)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result)!);
                return View(model);
            }

            return NotFound(); // .NET's default NotFound page
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
            if (response != null && response.IsSuccessful)
            {
                return RedirectToAction(nameof(CouponIndex)); // Basically calls CouponIndex so we can see the new Coupon
            }

            return View(couponDto); // Return to the CouponDelete page with our current model
        }
    }
}
