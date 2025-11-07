using DataCatalogMini.Features.Login.Contracts;
using DataCatalogMini.Features.Login.Services;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login( LoginRequest loginRequest )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginRequest.Id) || string.IsNullOrWhiteSpace(loginRequest.Password))
                    return BadRequest("Kullanıcı adı ve şifre zorunludur.");

                var response = await _authService.AuthenticateAsync(loginRequest);

                if (response == null)
                    return Unauthorized("Geçersiz kullanıcı adı veya şifre.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login işlemi sırasında hata oluştu");
                return StatusCode(500, "Sunucu hatası. Lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}
