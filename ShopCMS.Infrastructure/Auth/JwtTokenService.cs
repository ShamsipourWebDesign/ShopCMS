using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopCMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Infrastructure.Auth
{
    public interface IJwtTokenService
    {
        (string token, DateTime expiresAt) CreateAccessToken(User user);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _opt;

        public JwtTokenService(IOptions<JwtOptions> opt)
        {
            _opt = opt.Value;
        }

        public (string token, DateTime expiresAt) CreateAccessToken(User user)
        {
            var expires = DateTime.UtcNow.AddMinutes(_opt.AccessTokenMinutes);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("tv", user.TokenVersion.ToString())
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_opt.SigningKey));

            var token = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                expires: expires,
                signingCredentials:
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
