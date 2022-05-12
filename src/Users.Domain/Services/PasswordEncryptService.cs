using System;
using System.Security.Cryptography;
using System.Text;
using Users.Domain.Interfaces;

namespace Users.Domain.Services
{

    public class PasswordEncryptService : IPasswordEncryption
    {
        public string GenerateSalt(int length)
        {
            var saltBytes = new byte[length];

            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GenerateHash(string pass, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);

            return Convert.ToBase64String(inArray);
        }

    }
}