using Voting.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Service.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers(long adminUserId);
        UserDto Authenticate(string username, string password);
        UserDto GetUser(long userId);
        bool ChangePassword(long userId, ChangePasswordDto model);
        bool ResetPassword(long userId);
        bool AddAdminUser(long adminUserId, RegistrationDto model);
        long Register(RegistrationDto model);
        bool CheckUserNameExist(string userName);
    }
}
