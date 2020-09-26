using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Service.Models
{
    public class FileDto
    {
        public string DisplayNameAr { get; set; }

        public string DisplayNameEn { get; set; }
        public IFormFile file { get; set; }
    }
}
