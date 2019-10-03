using System;
using System.Security.Cryptography;
using System.Text;
using BCryptHelper = BCrypt.Net.BCrypt;

namespace Demo.Providers
{
    public class CryptoProvider : Interface.ICryptoProvider
    {
        #region Bcrypt
        public string Bcrypt_Encrypt(string text) => BCryptHelper.HashPassword(text);
        public bool Bcrypt_Verify(string cipherText, string text) => BCryptHelper.Verify(text, cipherText);
        #endregion

        #region RSA
        private static RSAParameters _Privatekey { set; get; }
        private static RSAParameters _Publickey { set; get; }
        private static Encoding encoding => Encoding.UTF8;
        public CryptoProvider()
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.KeySize = 1024;
                _Publickey = RSA.ExportParameters(false);
                _Privatekey = RSA.ExportParameters(true);
            }
        }

        private byte[] StringToBytes(string str) => encoding.GetBytes(str);
        private string BytesToString(byte[] bytes) => encoding.GetString(bytes);

        private byte[] RSAEncrypt(byte[] DataToEncrypt, bool DoOAEPPadding = false)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(_Publickey);
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
        private byte[] RSADecrypt(byte[] DataToDecrypt, bool DoOAEPPadding = false)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(_Privatekey);
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        public (string, string) RSA_GetPublicKey()
            => (Convert.ToBase64String(_Publickey.Modulus), Convert.ToBase64String(_Publickey.Exponent));
        public bool RSA_Decrypt(string cipherText, out string text)
        {
            text = string.Empty;
            try
            {
                var cipherBytes = Convert.FromBase64String(cipherText);
                var textBytes = RSADecrypt(cipherBytes);
                if (textBytes == null) return false;
                text = BytesToString(textBytes);
                return true;
            }
            catch { return false; }
        }
        public string RSA_Encrypt(string text)
        {
            try
            {
                var textBytes = StringToBytes(text);
                var cipherBytes = RSAEncrypt(textBytes);
                return Convert.ToBase64String(cipherBytes);
            }
            catch { return null; }
        }
        #endregion
    }
}
