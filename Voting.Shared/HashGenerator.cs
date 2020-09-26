using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Voting.Shared
{
    public class HashGenerator
    {
        public static string Hash(string defaultAdminPassword)
        {
            using (var sha1 = new SHA1Managed())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(defaultAdminPassword))).Replace("-", "");
            }
        }
    }
}
