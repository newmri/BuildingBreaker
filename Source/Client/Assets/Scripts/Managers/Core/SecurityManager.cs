
public class SecurityManager
{
    public string Encrypt(byte key, string str)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        for (int i = 0; i < bytes.Length; ++i)
            bytes[i] = (byte)(bytes[i] ^ key);

        return System.Convert.ToBase64String(bytes);
    }

    public string Decrypt(byte key, string str)
    {
        var bytes = System.Convert.FromBase64String(str);

        for (int i = 0; i < bytes.Length; ++i)
            bytes[i] = (byte)(bytes[i] ^ key);

        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}
