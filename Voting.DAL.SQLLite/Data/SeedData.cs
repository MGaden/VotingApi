using Voting.DAL.SQLLite.Entities;
using Voting.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voting.DAL.SQLLite.Data
{
    public class SeedData
    {
        public static void SeedDatabase(IServiceProvider services)
        {
            using (var context = services.GetRequiredService<VotingDbContext>())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                // Adds a default admin user
                var defaultAdmin = context.Users.FirstOrDefault(u => u.UserName == Constants.DefaultAdminName);
                if(defaultAdmin == null)
                {
                    var user = new User
                    {
                        Id = 1,
                        UserName = Constants.DefaultAdminName,
                        FirstName = Constants.DefaultAdminName,
                        LastName = Constants.DefaultAdminName,
                        Password = HashGenerator.Hash(Constants.DefaultAdminPassword),
                        Role = Policies.Admin,
                        CreationTime = DateTime.Now
                    };
                    context.Users.Add(user);

                    // Saves changes
                    context.SaveChanges();
                }

            }
        }
    }
}
