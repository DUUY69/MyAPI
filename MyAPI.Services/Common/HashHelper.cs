using System.Security.Cryptography;
using System.Text;

namespace MyAPI.Services.Common
{
    public interface IHashHelper
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class HashHelper : IHashHelper
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == hash;
        }
    }
}
