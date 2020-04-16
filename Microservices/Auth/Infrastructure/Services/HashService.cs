using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace UCLDreamTeam.Auth.Api.Infrastructure.Services
{
    public class HashService
    {
        public PasswordVerificationResult Compare(string password, string dbHash, string salt)
        {
            string hashedPassword = Hasher(password, salt);

            if (string.Equals(hashedPassword, dbHash))
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }

        public string Hasher(string password, string salt)
        {
            string hashedPassword =
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 512 / 8));

            return hashedPassword;
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
