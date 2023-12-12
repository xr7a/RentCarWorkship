using System.Text;
using System.Security.Cryptography;

namespace RentCarWorkship.Common;
public class Hash
{
    public static string GetHash(string password)
    {
        var hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(password));
        
        int i;
        StringBuilder sOutput = new StringBuilder(hash.Length);
        for (i=0;i < hash.Length -1; i++)
        {
            sOutput.Append(hash[i].ToString("X2"));
        }
        return sOutput.ToString();
    }
}