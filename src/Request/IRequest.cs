using System.Collections.Specialized;

namespace HelpScoutNet.Request
{
    public interface IRequest
    {
        NameValueCollection ToNameValueCollection();
    }
}
