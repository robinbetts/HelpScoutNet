using System.Collections.ObjectModel;

namespace HelpScoutNet.Model
{
    public class Paged<T>
    {
        public int Pages { get; set; }
        public int Count { get; set; }
        public Collection<T> Items { get; set; }
    }    
}
