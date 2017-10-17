using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Infrastructure.Extensions;
using System.Security.Cryptography;

namespace Passenger.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DeriveBytesIterationCount = 10000;
        private static readonly int SaltSize = 40;
        public string GetSalt(string value)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Can not generate salt from an empty value", nameof(value));
            }

            var random = new Random();
            var saltBytes = new Byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Can not generate hash from an empty value", nameof(value));
            }
            if (salt.Empty())
            {
                throw new ArgumentException("Can not use an empty salt", nameof(value));
            }

            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));

        }

        private static byte[] GetBytes(string salt)
        {
            var bytes = new byte[salt.Length * sizeof(char)];
            Buffer.BlockCopy(salt.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
