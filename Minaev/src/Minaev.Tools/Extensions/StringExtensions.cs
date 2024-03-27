using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace Minaev.Tools.Extensions;

public static class StringExtensions
{
    public static Boolean IsNullOrWhitespace([NotNullWhen(false)] this String? str)
    {
        return String.IsNullOrWhiteSpace(str);
    }

    public static Boolean IsNotNullOrWhitespace([NotNullWhen(true)] this String? str)
    {
        return !String.IsNullOrWhiteSpace(str);
    }

    public static String GetHash(this String str)
    {
        Byte[] bytes = Encoding.Unicode.GetBytes(str);
        MD5 md5 = MD5.Create();

        Byte[] byteHash = md5.ComputeHash(bytes);
        String hash = Convert.ToHexString(byteHash);

        return hash;
    }
}