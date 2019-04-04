using System.Linq;
using System.Net;

namespace HelpScout
{
    public static class ApiResponseExtension
    {
        public static HelpScoutException ToException(this IApiResponse response, string message)
        {
            return new HelpScoutException(message, response.Errors) {StatusCode = response.StatusCode};
        }

        public static void WithValidation(this IApiResponse source)
        {
            if (source.IsSuccessful)
                return;
            if (source.StatusCode == HttpStatusCode.Unauthorized)
                throw new HelpScoutAuthenticationException("Authentication Failed: Invalid Token or expired one.");

            throw source.ToException($"Errored status:{source.StatusCode}");
        }

        public static T WithValidation<T>(this IApiResponse<T> source)
        {
            if (source.IsSuccessful)
                return source.Record;
            if (source.StatusCode == HttpStatusCode.Unauthorized)
                throw new HelpScoutAuthenticationException("Authentication Failed: Invalid Token or expired one.");

            throw source.ToException($"Errored status:{source.StatusCode}");
        }

        public static string GetHeaderValueSingle(this IApiResponse source, string header)
        {
            return source.ResponseHeader.GetValues(header).First();
        }
    }
}