using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Modules.System.Security.Cryptography
{
    public static class CryptographyHelper
    {
        private const int KEYSIZE = 256;
        private const int DERIVATIONITERATIONS = 1000;

        /// <summary>
        /// Convert a String to <see cref="MD5"/> checksum.
        /// </summary>
        /// <param name="value">String to convert.</param>
        public static string ToMD5(this string value)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
                    return string.Join("", hash.Select(a => a.ToString("X2")).ToArray());
                }
            }
            catch (Exception) { return value; }
        }

        /// <summary>
        /// Convert a <see cref="Stream"/> to <see cref="MD5"/> checksum.
        /// </summary>
        /// <param name="stream">Stream to convert.</param>
        public static string ToMD5(this Stream stream)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return string.Join("", hash.Select(a => a.ToString("X2")).ToArray());
                }
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Convert a String to <see cref="SHA1"/> checksum.
        /// </summary>
        /// <param name="value">String to convert.</param>
        public static string ToSHA1(this string value)
        {
            try
            {
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
                    return string.Join("", hash.Select(a => a.ToString("X2")).ToArray());
                }
            }
            catch (Exception) { return value; }
        }

        /// <summary>
        /// Convert a <see cref="Stream"/> to <see cref="SHA1"/> checksum.
        /// </summary>
        /// <param name="stream">Stream to convert.</param>
        public static string ToSHA1(this Stream stream)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] hash = sha1.ComputeHash(stream);
                    return string.Join("", hash.Select(a => a.ToString("X2")).ToArray());
                }
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Convert a String to <see cref="SHA256"/> checksum.
        /// </summary>
        /// <param name="value">String to convert.</param>
        public static string ToSHA256(this string value)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                    return string.Join("", hash.Select(a => a.ToString("X2")).ToArray());
                }
            }
            catch (Exception) { return value; }
        }

        /// <summary>
        /// Convert a <see cref="Stream"/> to <see cref="SHA256"/> checksum.
        /// </summary>
        /// <param name="stream">Stream to convert.</param>
        public static string ToSHA256(this Stream stream)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(stream);
                    return string.Join("", hash.Select(a => a.ToString("X2")).ToArray());
                }
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Encrypt a String with a password.
        /// </summary>
        /// <param name="plainText">String to convert.</param>
        /// <param name="passPhrase">Password.</param>
        public static string EncryptString(this string plainText, string passPhrase)
        {
            try
            {
                // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
                // so that the same Salt and IV values can be used when decrypting.  
                var saltStringBytes = Generate256BitsOfRandomEntropy();
                var ivStringBytes = Generate256BitsOfRandomEntropy();
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DERIVATIONITERATIONS);
                var keyBytes = password.GetBytes(KEYSIZE / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Decrypt a String with a password.
        /// </summary>
        /// <param name="cipherText">Encrypted string to convert.</param>
        /// <param name="passPhrase">Password.</param>
        public static string DecryptString(this string cipherText, string passPhrase)
        {
            try
            {
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KEYSIZE / 8).ToArray();
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KEYSIZE / 8).Take(KEYSIZE / 8).ToArray();
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((KEYSIZE / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((KEYSIZE / 8) * 2)).ToArray();
                var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DERIVATIONITERATIONS);
                var keyBytes = password.GetBytes(KEYSIZE / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
            catch (Exception) { return null; }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32];
            var rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}