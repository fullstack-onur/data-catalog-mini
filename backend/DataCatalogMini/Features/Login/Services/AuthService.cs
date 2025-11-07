using DataCatalogMini.Data;
using DataCatalogMini.Features.Login.Contracts;
using DataCatalogMini.Features.Login.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataCatalogMini.Features.Login.Services
{
    public class AuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> AuthenticateAsync(LoginRequest request)
        {
            var user = await _appDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.Username == request.Id &&
                    u.Password == request.Password);

            if (user == null)
                return null;

            // 2️⃣ Token oluştur
            var jwtSection = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var expiresAt = DateTime.UtcNow.AddMinutes(15); 
            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt,
                Role = user.Role
            };
        }
    }
}
