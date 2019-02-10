using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Common
{
    public class Rating
    {
        public int Number { get; set; }
        public long Id { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ConversationType Type { get; set; }
        public long ThreadID { get; set; }
        public DateTime? ThreadCreatedAt { get; set; }
        /// <summary>
        /// 1 = Great
        /// 2 = OK
        /// 3 = Bad
        /// </summary>
        public long RatingID { get; set; }
        public long RatingCustomerID { get; set; }
        public string RatingComments { get; set; }
        public DateTime? RatingCreatedAt { get; set; }
        public string RatingCustomerName { get; set; }
        public long RatingUserID { get; set; }
        public string RatingUserName { get; set; }
    }
}
