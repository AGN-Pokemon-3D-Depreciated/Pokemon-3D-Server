using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Modules.System.Security.Cryptography
{
    public static class CryptographyHelper
    {
        private const int KeySize = 256;
        private const int DerivationIterations = 1000;

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
            catch (Exception)
            {
                return value;
            }
        }

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
            catch (Exception)
            {
                return null;
            }
        }

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
            catch (Exception)
            {
                return value;
            }
        }

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
            catch (Exception)
            {
                return null;
            }
        }

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
            catch (Exception)
            {
                return value;
            }
        }

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
            catch (Exception)
            {
                return null;
            }
        }

        public static string EncryptString(this string plainText, string passPhrase)
        {
            try
            {
                byte[] saltStringBytes = Generate256BitsOfRandomEntropy();
                byte[] ivStringBytes = Generate256BitsOfRandomEntropy();
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        byte[] keyBytes = password.GetBytes(KeySize / 8);

                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;

                        using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                        {
                            MemoryStream memoryStream;
                            using (var cryptoStream = new CryptoStream(memoryStream = new MemoryStream(), encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                byte[] cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string DecryptString(this string cipherText, string passPhrase)
        {
            try
            {
                byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                byte[] saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KeySize / 8).ToArray();
                byte[] ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KeySize / 8).Take(KeySize / 8).ToArray();
                byte[] cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((KeySize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((KeySize / 8) * 2)).ToArray();

                using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        byte[] keyBytes = password.GetBytes(KeySize / 8);

                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;

                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(new MemoryStream(cipherTextBytes), decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            byte[] randomBytes = new byte[32];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
                return randomBytes;
            }
        }
    }
}