using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace HelpScout
{
    public static class Extensions
    {
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static IEnumerable<KeyValuePair<string, string>> ToPairs(this NameValueCollection collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            return collection.Cast<string>().Select(key => new KeyValuePair<string, string>(key, collection[key]));
        }


        public static string ToCommaSeparated(this IEnumerable<string> coll)
        {
            return string.Join(",", coll);
        }


        public static string ConvertToQueryString(this NameValueCollection queryParameters)
        {
            return string.Join("&", queryParameters.AllKeys
                .Select(a => $"{HttpUtility.UrlEncode(a)}={HttpUtility.UrlEncode(queryParameters[a])}"));
        }
    }
}