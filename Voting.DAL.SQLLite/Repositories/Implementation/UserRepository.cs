using Voting.DAL.SQLLite.Data;
using Voting.DAL.SQLLite.Entities;
using Voting.DAL.SQLLite.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voting.DAL.SQLLite.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        VotingDbContext context;
        public UserRepository(VotingDbContext _context)
        {
            context = _context;
        }

        public bool ChangePassword(long userId, string newPassword)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
                return false;

            user.Password = newPassword;
            user.LastUpdatedTime = DateTime.Now;

            context.SaveChanges();

            return true;
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public User GetUser(string username, string password)
        {
            return context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);
        }

        public User GetUser(long userId)
        {
            return context.Users.SingleOrDefault(x => x.Id == userId);
        }

        public long AddUser(User user)
        {
            if (user == null)
                return 0;

            context.Users.Add(user);

            // Saves changes
            context.SaveChanges();

            return user.Id;
        }

        public bool CheckUserNameExist(string userName)
        {
            return context.Users.Any(u => u.UserName == userName);
        }
    }
}
