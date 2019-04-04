using System.Collections.Generic;

namespace HelpScout
{
    public class HelpScoutAuthenticationException : HelpScoutException
    {
        public HelpScoutAuthenticationException(string message, IList<string> errors = null) : base(message, errors)
        {
        }
    }
}