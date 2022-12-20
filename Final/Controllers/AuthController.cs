using AutoMapper;
using Final.Authorization;
using Final.Entities;
using Final.Helpers;
using Final.Model.Auth;
using Final.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace Final.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthController(
            IAuthService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _authService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        
        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            try
            {
            var response = _authService.Authenticate(model);
                
                HttpContext.Response.Headers.Add("Authorization", response.Token);
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
               
        }

        

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            try
            {
                _authService.Register(request);
                return Ok(new { message = "Verify your account to login" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<ActionResult> VerifyEmail(string otp)
        {
            try
            {
                await _authService.Verify(otp);
                return Ok(new { message = "User has been Verified successfully" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                await _authService.ForgotPassword(request);
                return Ok(new { message = "Reset password after OTP verification!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPassword(request);
                return Ok(new { message = "Your Password has been changed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutRequest request)
        {
            try
            {
                await _authService.Logout(request);
                return Ok(new { message = "Logged out successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
