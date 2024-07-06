
using System.IO;
using System.Security.Cryptography;
using System;
using System.Text;

public class SecurityManager
{
    public byte[] CreateKey(string str)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(str)).AsSpan(0, 16).ToArray();
        }
    }

    public byte[] CreateIV(string str)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(str + str)).AsSpan(0, 16).ToArray();
        }
    }

    public string Encrypt(string content, byte[] key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(content);
                    }

                    var base64Encrypted  = Convert.ToBase64String(msEncrypt.ToArray());

                    // URL Ssafe
                    return base64Encrypted.Replace('+', '-').Replace('/', '_').TrimEnd('='); ;
                }
            }
        }
    }

    public string Decrypt(string content, byte[] key, byte[] iv)
    {
        // URL Ssafe
        var base64Encrypted = content.Replace('-', '+').Replace('_', '/');

        switch (base64Encrypted.Length % 4)
        {
            case 2: base64Encrypted += "=="; break;
            case 3: base64Encrypted += "="; break;
        }

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(base64Encrypted)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
