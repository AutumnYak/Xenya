using System;
using System.Text;
class Crypto
{
    public static string AES_Encrypt(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();
        var Hash_AES = new System.Security.Cryptography.MD5CryptoServiceProvider();
        string encrypted = "";
        try
        {
            var hash = new byte[32];
            var temp = Hash_AES.ComputeHash(Encoding.ASCII.GetBytes(pass));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;
            var DESEncrypter = AES.CreateEncryptor();
            var Buffer = Encoding.ASCII.GetBytes(input);
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            return encrypted;
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    public static string AES_Decrypt(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();
        var Hash_AES = new System.Security.Cryptography.MD5CryptoServiceProvider();
        string decrypted = "";
        try
        {
            var hash = new byte[32];
            var temp = Hash_AES.ComputeHash(Encoding.ASCII.GetBytes(pass));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;
            var DESDecrypter = AES.CreateDecryptor();
            var Buffer = Convert.FromBase64String(input);
            decrypted = Encoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            return decrypted;
        }
        catch (Exception ex)
        {
        }
        return "";
    }
}