using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Reports
{
    public class Rating
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public int TicketID { get; set; }
        public int ThreadID { get; set; }
        public string Rating { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
