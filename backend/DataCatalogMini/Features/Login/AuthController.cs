using DataCatalogMini.Features.Login.Contracts;
using DataCatalogMini.Features.Login.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataCatalogMini.Features.Login
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    { 
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger   )
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = await _authService.AuthenticateAsync(loginRequest);

            if (response == null)
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");

            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = response.RefreshTokenExpiresAt
            });

            return Ok(new { response.Token, response.ExpiresAt, response.Role });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var result = await _authService.RefreshJwtAsync(refreshToken);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _authService.RevokeRefreshTokenAsync(refreshToken);
            }

            Response.Cookies.Delete("refreshToken");
            return Ok();
        }

    }
}
