using Voting.DAL.SQLLite.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.DAL.SQLLite.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUser(string username, string password);
        User GetUser(long userId);
        bool ChangePassword(long userId, string newPassword);
        long AddUser(User user);
        bool CheckUserNameExist(string userName);
    }
}
