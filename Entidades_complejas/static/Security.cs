using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class Security
    {
        private const int KeyLength = 2048;
        private const int KeyCB = 32;
        private const int IvCB = 16;
        private const CryptoStreamMode Mode = CryptoStreamMode.Write;
        private const PaddingMode Padding = PaddingMode.PKCS7;
        private readonly byte[] Salt = Encoding.ASCII.GetBytes("Inventarios_2013_pmS");
        private readonly string key = "363c246596532b1db9c0129e741c14510c7296b2";
        private readonly string keyDinamico = "363c246596532b1db9c0129e741c14510c7296b2" + DateTime.Now.ToShortDateString();
        public string Encriptar(string origin)
        {
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(origin);
                using (var encryptor = Aes.Create())
                {
                    encryptor.Padding = Padding;
                    var pdb = new Rfc2898DeriveBytes(key, Salt);
                    encryptor.Key = pdb.GetBytes(KeyCB);
                    encryptor.IV = pdb.GetBytes(IvCB);
                    using var ms = new MemoryStream();
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), Mode))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    origin = Convert.ToBase64String(ms.ToArray());
                }
                return origin;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Desencriptar(string encrypted)
        {
            try
            {
                encrypted = encrypted.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(encrypted);
                using (var encryptor = Aes.Create())
                {
                    encryptor.Padding = Padding;
                    var pdb = new Rfc2898DeriveBytes(key, Salt);
                    encryptor.Key = pdb.GetBytes(KeyCB);
                    encryptor.IV = pdb.GetBytes(IvCB);
                    using var ms = new MemoryStream();
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), Mode))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    encrypted = Encoding.Unicode.GetString(ms.ToArray());
                }
                return encrypted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string EncriptarId(string origin)
        {
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(origin);
                using (var encryptor = Aes.Create())
                {
                    encryptor.Padding = Padding;
                    var pdb = new Rfc2898DeriveBytes(keyDinamico, Salt);
                    encryptor.Key = pdb.GetBytes(KeyCB);
                    encryptor.IV = pdb.GetBytes(IvCB);
                    using var ms = new MemoryStream();
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), Mode))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    origin = Convert.ToBase64String(ms.ToArray());
                }
                return origin;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DesencriptarId(string encrypted)
        {
            try
            {
                encrypted = encrypted.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(encrypted);
                using (var encryptor = Aes.Create())
                {
                    encryptor.Padding = Padding;
                    var pdb = new Rfc2898DeriveBytes(keyDinamico, Salt);
                    encryptor.Key = pdb.GetBytes(KeyCB);
                    encryptor.IV = pdb.GetBytes(IvCB);
                    using var ms = new MemoryStream();
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), Mode))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    encrypted = Encoding.Unicode.GetString(ms.ToArray());
                }
                return encrypted;
            }
            catch (Exception)
            {
                throw new Exception("Url no valida.");
            }
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64_Decode(string str)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return System.Text.Encoding.UTF8.GetString(decbuff);
            }
            catch
            {
                return "";
            }
        }





        //static readonly String PasswordHash = "@PMS|FM0";
        //static readonly String SaltKey = "S@LT&KEY";
        //static readonly String VIKey = "@6D65GKJFZA4GSSD";

        //public String Encrypt(String plainText)
        //{
        //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        //    byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        //    var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        //    var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

        //    byte[] cipherTextBytes;

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        //        {
        //            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //            cryptoStream.FlushFinalBlock();
        //            cipherTextBytes = memoryStream.ToArray();
        //            cryptoStream.Close();
        //        }
        //        memoryStream.Close();
        //    }
        //    return Convert.ToBase64String(cipherTextBytes);
        //}

        //public String Decrypt(String encryptedText)
        //{
        //    byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        //    byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        //    var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        //    var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        //    var memoryStream = new MemoryStream(cipherTextBytes);
        //    var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        //    byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        //    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        //    memoryStream.Close();
        //    cryptoStream.Close();
        //    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        //}
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        //public string Base64Decode(string base64EncodedData)
        //{
        //    var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        //    return Encoding.UTF8.GetString(base64EncodedBytes);
        //}
    }
}
