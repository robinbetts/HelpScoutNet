using System;

namespace HelpScout.Users
{
    public class UserDetail
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Timezone { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Type { get; set; }
        public long CompanyId { get; set; }
    }
}