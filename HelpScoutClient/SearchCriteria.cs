using System.Collections.Specialized;

namespace HelpScout
{
    public interface ISearchQuery
    {
        NameValueCollection BuildQueryString();
    }
}