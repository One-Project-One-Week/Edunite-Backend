using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Edunite.Application.Extension.PasswordHah
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8; // 128 bits
        private const int KeySize = 256 / 8;  // 256 bits
        private const int Iterations = 100_000;

        public string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Derive a key from the password and salt
            byte[] key = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize
            );

            // Combine salt and key into one string
            var hashBytes = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
            Buffer.BlockCopy(key, 0, hashBytes, SaltSize, KeySize);

            // Return as base64 string
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract salt and stored key
            byte[] salt = new byte[SaltSize];
            byte[] storedKey = new byte[KeySize];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(hashBytes, SaltSize, storedKey, 0, KeySize);

            // Derive key from provided password and stored salt
            byte[] derivedKey = KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize
            );

            // Compare keys
            return CryptographicOperations.FixedTimeEquals(storedKey, derivedKey);
        }
    }
}
