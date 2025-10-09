using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto requestDto)
        {
            var errorMessage = await _authService.Register(requestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccessful = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            var loginResponse = await _authService.Login(requestDto);

            if (loginResponse.User == null)
            {
                _response.IsSuccessful = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }

            _response.Result = loginResponse;
            return Ok(_response);
        }
    }
}
