using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context, IConfiguration configuration) : ITokenManagement
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            context.RefreshToken.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken
            });
            return await context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken == null) return [];
            return jwtToken.Claims.ToList();
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken)        
            => (await context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == refreshToken))!.UserId;

        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            var user = await context.RefreshToken.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            if (user == null) return -1;
            user.Token = refreshToken;
            return await context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var user = await context.RefreshToken
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            return user != null;
        }
    }
}
