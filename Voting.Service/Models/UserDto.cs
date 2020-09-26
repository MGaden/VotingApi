using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Service.Models
{
    public class UserDto
    {
        public long Id { get; set; }

        public string CompanyCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Token { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastUpdatedTime { get; set; }
    }
}
