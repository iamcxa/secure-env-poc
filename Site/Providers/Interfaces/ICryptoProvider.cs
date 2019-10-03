namespace Demo.Providers.Interface
{
    public interface ICryptoProvider
    {
        string Bcrypt_Encrypt(string text);
        bool Bcrypt_Verify(string cipherText, string text);
        (string N, string E) RSA_GetPublicKey();
        string RSA_Encrypt(string text);
        bool RSA_Decrypt(string cipherText, out string text);
    }
}
