using System.Collections.Specialized;

namespace HelpScoutNet.Request
{
    public interface IPostOrPutRequest : IRequest
    {
        bool Reload { get; set; }
        bool Imported { get; set; }
    }

    public class PostOrPutRequest : IPostOrPutRequest
    {
        public bool Reload { get; set; }
        public bool Imported { get; set; }
        
        protected NameValueCollection Nv = new NameValueCollection();

        public virtual NameValueCollection ToNameValueCollection()
        {
            if(Reload)
                Nv.Add("reload", "true");
            if(Imported)
                Nv.Add("imported","true");
            return Nv;
        }        
    }
}
