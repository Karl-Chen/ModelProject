using System.Security.Cryptography;
using System.Text;

namespace WebProject.WorkFunction
{
    public class MimaHandler
    {
        public string Get_SHA256_Hash(string value)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray).ToUpper();
        }
    }
}
