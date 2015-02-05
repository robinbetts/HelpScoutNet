using System;

namespace HelpScoutNet
{
    public static class Extensions
    {
        public static string ToIso8601(this DateTime dt)
        {            
            return dt.ToString("yyyy-MM-ddTHH:mm:ssK");
        }

        public static string FirstCharacterToLower(this string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
                return str;

            return Char.ToLowerInvariant(str[0]).ToString() + str.Substring(1);
        }

    }
}