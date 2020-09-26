using Voting.DAL.SQLLite.Entities;
using Voting.DAL.SQLLite.Repositories.Interfaces;
using Voting.Service.Interfaces;
using Voting.Service.Models;
using Voting.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Voting.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        IConfiguration configuration;

        public UserService(IUserRepository userRepository, IConfiguration _configuration)
        {
            _UserRepository = userRepository;
            configuration = _configuration;
        }

        public string GetSecretKey()
        {
            return configuration.GetSection("AppSettings").GetSection("Secret").Value;
        }

        public UserDto Authenticate(string username, string password)
        {
            var user = _UserRepository.GetUser(username, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetSecretKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("role",user.Role)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var expiration = configuration.GetSection("AppSettings").GetSection("TokenExpiration").Value;
            if(expiration != null && int.Parse(expiration) > 0)
                tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(int.Parse(expiration));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return Service_Mapper.mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetAllUsers(long adminUserId)
        {
            var currentUser = _UserRepository.GetUser(adminUserId);
            if (currentUser == null)
                return null;

            return Service_Mapper.mapper.Map<List<UserDto>>(_UserRepository.GetAllUsers());

        }

        public UserDto GetUser(long userId)
        {
            return Service_Mapper.mapper.Map<UserDto>(_UserRepository.GetUser(userId));
        }

        public bool ChangePassword(long userId, ChangePasswordDto model)
        {
            var user = _UserRepository.GetUser(model.Username, HashGenerator.Hash(model.OldPassword));

            // return null if user not found
            if (user == null)
                return false;

            if(user.Id != userId)
                return false;

            return _UserRepository.ChangePassword(userId, HashGenerator.Hash(model.NewPassword));
        }

        public bool ResetPassword(long userId)
        {
            var user = _UserRepository.GetUser(userId);

            // return null if user not found
            if (user == null)
                return false;

            var newPassword = HashGenerator.Hash(Constants.DefaultUserPassword);

            if (user.Role == Policies.Admin)
                newPassword = HashGenerator.Hash(Constants.DefaultAdminPassword);

            return _UserRepository.ChangePassword(userId, newPassword);
        }

        public bool AddAdminUser(long adminUserId, RegistrationDto model)
        {
            var currentUser = _UserRepository.GetUser(adminUserId);
            if (currentUser == null)
                return false;

            var user = new User
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = HashGenerator.Hash(model.Password),
                Email = model.Email,
                Mobile = model.Mobile,
                Role = Policies.Admin,
                CreationTime = DateTime.Now
            };

            if (_UserRepository.AddUser(user) > 0)
                return true;

            return false;
        }

        public long Register(RegistrationDto model)
        {
            var user = new User
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = HashGenerator.Hash(model.Password),
                Email = model.Email,
                Mobile = model.Mobile,
                Role = Policies.User,
                CreationTime = DateTime.Now
            };

            return _UserRepository.AddUser(user);
        }

        public bool CheckUserNameExist(string userName)
        {
           return _UserRepository.CheckUserNameExist(userName);
        }
    }
}
