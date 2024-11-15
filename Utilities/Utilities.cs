using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;

namespace FRIchat.Utilities;

public static class CryptoUtils
{
    private static readonly byte[] Key = { 128, 255, 13, 24, 100, 50, 11, 123, 143, 12, 15, 16, 17, 18, 19, 20 };
    
    public static string Encrypt(string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = aesAlg.IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            byte[] cipherBytes = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(cipherBytes);
        }
    }

    public static string Decrypt(string data)
    {
        byte[] bytes = Convert.FromBase64String(data);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = aesAlg.IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            byte[] plainBytes = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(plainBytes);

        }
    }
}

public static class Cookies
{
    public static bool CookieExists(IHttpContextAccessor httpContextAccessor)
    {
        var cookie = httpContextAccessor.HttpContext.Request.Cookies["AuthCookie"];

        if (cookie != null)
        {
            return true;
        }
        return false;
    }

    public static bool ClearCookie(IHttpContextAccessor httpContextAccessor)
    {
        var cookie = httpContextAccessor.HttpContext.Request.Cookies["AuthCookie"];
            
        if (cookie != null)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete("AuthCookie");
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool SetCookie(IHttpContextAccessor httpContextAccessor, string username)
    {
        try
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
                Secure = true
            };
            
            string encryptedUsername = Utilities.CryptoUtils.Encrypt(username);
            
            httpContextAccessor.HttpContext.Response.Cookies.Append("AuthCookie", encryptedUsername, cookieOptions);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string GetCookie(IHttpContextAccessor httpContextAccessor)
    {
        var cookie = httpContextAccessor.HttpContext.Request.Cookies["AuthCookie"];
        return cookie != null ? cookie : "";
    }
}