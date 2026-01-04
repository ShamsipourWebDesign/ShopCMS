using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ShopCMS.Infrastructure.Auth
{
   

    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, 100_000, HashAlgorithmName.SHA256, 32);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string stored)
        {
            var parts = stored.Split('.');
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            var check = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, 100_000, HashAlgorithmName.SHA256, 32);

            return CryptographicOperations.FixedTimeEquals(hash, check);
        }
    }

}
