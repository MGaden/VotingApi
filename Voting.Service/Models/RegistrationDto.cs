using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Voting.Service.Models
{
    public class RegistrationDto
    {
        [Required]
        public string CompanyCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Mobile { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
