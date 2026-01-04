using Microsoft.EntityFrameworkCore;
using ShopCMS.Domain.Entities;
using ShopCMS.Infrastructure.Persistence;
using ShopCMS.Infrastructure.Persistence.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using ShopCMS.Infrastructure.Auth.Dtos;
using ShopCMS.Infrastructure.Auth.Interfaces;


namespace ShopCMS.Infrastructure.Auth
{

  

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly IJwtTokenService _jwt;

        public AuthService(ApplicationDbContext db, IJwtTokenService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest req)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Username == req.Username);

            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException();

            if (!PasswordHasher.Verify(req.Password, user.PasswordHash))
                throw new UnauthorizedAccessException();

            var (access, exp) = _jwt.CreateAccessToken(user);

            var refresh = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            var refreshEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.UserId,
                TokenHash = refresh,
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(14)
            };

            _db.RefreshTokens.Add(refreshEntity);
            await _db.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = access,
                AccessTokenExpiresAtUtc = exp,
                RefreshToken = refresh,
                RefreshTokenExpiresAtUtc = refreshEntity.ExpiresAtUtc
            };
        }

        public async Task<TokenResponse> RefreshAsync(RefreshRequest req)
        {
            var token = await _db.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == req.RefreshToken);

            if (token == null || !token.IsActive)
                throw new UnauthorizedAccessException();

            var user = await _db.Users.FindAsync(token.UserId);

            token.RevokedAtUtc = DateTime.UtcNow;

            var (access, exp) = _jwt.CreateAccessToken(user);

            await _db.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = access,
                AccessTokenExpiresAtUtc = exp,
                RefreshToken = req.RefreshToken,
                RefreshTokenExpiresAtUtc = token.ExpiresAtUtc
            };
        }
    }


}
