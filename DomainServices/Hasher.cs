using System;
using System.Security.Cryptography;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IHasher
    {
        string GenerateSalt();
        string ComputeHash(string plainText, string salt);
    }

    public class Hasher : IHasher
    {
        private const int SaltLength = 16;
        public string GenerateSalt()
        {
            var saltBytes = new byte[SaltLength];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        public string ComputeHash(string plainText, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var plainData = System.Text.Encoding.UTF8.GetBytes(plainText);
            var plainDataWithSalt = new byte[plainData.Length + saltBytes.Length];

            for (int i = 0; i < plainData.Length; i++)
            {
                plainDataWithSalt[i] = plainData[i];
            }

            for (int i = 0; i < saltBytes.Length; i++)
            {
                plainDataWithSalt[plainData.Length + i] = saltBytes[i];
            }

            byte[] hashValue;
            using (var sha = new SHA512Managed())
            {
                hashValue = sha.ComputeHash(plainDataWithSalt);
            }

            var result = new byte[hashValue.Length + saltBytes.Length];
            for (int x = 0; x < hashValue.Length; x++)
            {
                result[x] = hashValue[x];
            }

            for (int n = 0; n < saltBytes.Length; n++)
            {
                result[hashValue.Length + n] = saltBytes[n];
            }

            return Convert.ToBase64String(result);
        }
    }
}