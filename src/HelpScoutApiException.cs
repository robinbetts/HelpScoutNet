using System;

namespace HelpScoutNet
{
    public class HelpScoutApiException : ApplicationException
    {
        public int Code { get; set; }
        
        public HelpScoutApiException(string message, int code)
            : base(message)
        {
            Code = code;            
        }

        public HelpScoutApiException(string message, int code, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

    }
}
