using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Voting.DAL.SQLLite.Entities;

namespace Voting.DAL.SQLLite.Data
{
    public class VotingDbContext : DbContext
    {
        IConfiguration configuration;
        public VotingDbContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
             => options.UseSqlite(GetConnectionString());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public string GetConnectionString()
        {
            return configuration.GetSection("ConnectionStrings").GetSection("SQLLiteDBConnection").Value;
        }

        public DbSet<User> Users { get; set; }
    }
}
