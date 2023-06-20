using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Water_consumption_monitors.Models;
using Water_consumption_monitors.Services;

namespace Water_consumption_monitors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) 
        {
            _authService = authService; 
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync( [FromBody] Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var register = await _authService.RegisterAsync(model);

            if (!register.IsAuthenticated)
                return BadRequest( register.Message );

            SetRefreshTokenInCookie(register.RefreshToken, register.RefreshTokenExpiration);

            return Ok(new { Token = register.Token, Exception = register.ExpiresOn });

        }

        [HttpPost("Token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequest model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var register = await _authService.GetTokenAsync(model);

            if (!register.IsAuthenticated)
                return BadRequest(register.Message);

            if (string.IsNullOrEmpty(register.RefreshToken))
                SetRefreshTokenInCookie(register.RefreshToken, register.RefreshTokenExpiration);

            return Ok(new { Token = register.Token, Exception = register.ExpiresOn });
           

        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRole model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);

        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result);  
            }

            SetRefreshTokenInCookie(result.RefreshToken , result.RefreshTokenExpiration);   

            return Ok(result);  

        }

        [HttpPost ("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authService.RevokeTokenAsync(token);   

            if (!result) 
                return BadRequest("Token is invalid!");

            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,    
                Expires = expires.ToLocalTime(),
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

    }
}
