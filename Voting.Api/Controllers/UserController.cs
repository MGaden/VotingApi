using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Voting.Service.Interfaces;
using Voting.Service.Models;
using Voting.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Voting.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, HashGenerator.Hash(model.Password));

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet("getallusers")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAllUsers()
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(_userService.GetAllUsers(long.Parse(userId)));
        }

        [HttpGet("getUser")]
        [Authorize]
        public IActionResult GetUser()
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.Name)?.Value;

            if(string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(_userService.GetUser(long.Parse(userId)));
        }

        [HttpPost("changepassword")]
        [Authorize]
        public IActionResult ChangePassword([FromBody]ChangePasswordDto model)
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(_userService.ChangePassword(long.Parse(userId),model));
        }

        [HttpGet("resetpassword/{userId}")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult ResetPassword(long userId)
        {
            return Ok(_userService.ResetPassword(userId));
        }

        [HttpPost("vuser")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult AddAdminUser(RegistrationDto model)
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            bool isUserNameExist = _userService.CheckUserNameExist(model.UserName);
            if (isUserNameExist)
                return BadRequest(new { message = "Please use another user name" });

            return Ok(_userService.AddAdminUser(long.Parse(userId), model));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegistrationDto model)
        {
            bool isUserNameExist = _userService.CheckUserNameExist(model.UserName);
            if(isUserNameExist)
                return BadRequest(new { message = "Please use another user name" });

            var userId = _userService.Register(model);
            if (userId > 0)
            {
                return Ok(true);
            }

            return Ok(false);
        }
    }
}