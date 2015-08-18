using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report
{
    public class PagedReport<T>
    {
        public int Pages { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public Collection<T> Results { get; set; }
    }
}
