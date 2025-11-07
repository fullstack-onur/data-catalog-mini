using DataCatalogMini.Data;
using DataCatalogMini.Features.Login.Contracts;
using DataCatalogMini.Features.Login.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                .FirstOrDefaultAsync(u => u.Username == request.Id && u.Password == request.Password);

            if (user == null)
                return null;

            var jwtSection = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var expiresAt = DateTime.UtcNow.AddMinutes(15);

            var jwtToken = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            if (string.IsNullOrEmpty(user.RefreshToken) || user.RefreshTokenExpiresAt <= DateTime.UtcNow)
            {
                user.RefreshToken = GenerateRefreshToken();
                user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(15);
                await _appDbContext.SaveChangesAsync();
            }

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresAt = expiresAt,
                Role = user.Role,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiresAt = user.RefreshTokenExpiresAt
            };
        }


        public async Task<LoginResponse?> RefreshJwtAsync(string refreshToken)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiresAt > DateTime.UtcNow);

            if (user == null)
                return null;

            return await AuthenticateAsync(new LoginRequest { Id = user.Username, Password = user.Password });
        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
                return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiresAt = null;

            await _appDbContext.SaveChangesAsync();
            return true;
        }


        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
