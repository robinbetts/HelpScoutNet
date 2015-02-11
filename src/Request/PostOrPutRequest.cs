using System.Collections.Specialized;

namespace HelpScoutNet.Request
{
    public interface IPostOrPutRequest : IRequest
    {
        bool Reload { get; set; }
    }

    public class PostOrPutRequest : IPostOrPutRequest
    {
        public bool Reload { get; set; }
        
        protected NameValueCollection Nv = new NameValueCollection();

        public virtual NameValueCollection ToNameValueCollection()
        {
            if(Reload)
                Nv.Add("reload", "true");
            return Nv;
        }        
    }
}
