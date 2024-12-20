using System.Security.Cryptography;
using System.Text;

namespace FriendToTalkAdminApi.Services;

public class EncryptDecryptService
{
    readonly string myKey = "hello!!this|sdf!forps";

    // Use Aes.Create() to get an instance of the AES algorithm
    readonly Aes cryptAES = Aes.Create();
    readonly SHA256 cryptSHA256Hash = SHA256.Create();

    public string Decrypt(string myString)
    {
        cryptAES.Key = cryptSHA256Hash.ComputeHash(Encoding.ASCII.GetBytes(myKey));
        cryptAES.Mode = CipherMode.CBC; // Use CBC mode for better security
        cryptAES.Padding = PaddingMode.PKCS7;
        // Ensure IV is set; this is crucial for CBC mode
        cryptAES.IV = new byte[cryptAES.BlockSize / 8]; // Example, use a fixed IV or derive it securely
        ICryptoTransform aesDecrypt = cryptAES.CreateDecryptor();
        byte[] buff = Convert.FromBase64String(myString);
        return Encoding.ASCII.GetString(aesDecrypt.TransformFinalBlock(buff, 0, buff.Length));
    }

    public string Encrypt(string myString)
    {
        cryptAES.Key = cryptSHA256Hash.ComputeHash(Encoding.ASCII.GetBytes(myKey));
        cryptAES.Mode = CipherMode.CBC; // Use CBC mode for better security
        cryptAES.Padding = PaddingMode.PKCS7;
        // Ensure IV is set; this is crucial for CBC mode
        cryptAES.IV = new byte[cryptAES.BlockSize / 8]; // Example, use a fixed IV or derive it securely
        ICryptoTransform aesEncrypt = cryptAES.CreateEncryptor();
        byte[] buff = Encoding.ASCII.GetBytes(myString);
        return Convert.ToBase64String(aesEncrypt.TransformFinalBlock(buff, 0, buff.Length));
    }
}