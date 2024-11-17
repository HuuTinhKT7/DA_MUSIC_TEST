using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DA_MusicApp
{
    public class PasswordHasher
    {
        public (byte[] hash, byte[] salt) HashPassword(string password)
        {
            // Tạo muối ngẫu nhiên
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Băm mật khẩu với muối
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

                Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

                byte[] hash = sha256.ComputeHash(saltedPassword);
                return (hash, salt);
            }
        }

        public bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

                Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

                byte[] computedHash = sha256.ComputeHash(saltedPassword);
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}
