using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MathTraining.Data.Common;
using MathTraining.Data.Domain.Identity;
using MathTraining.Service.Common.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MathTraining.Api.Controllers.Identity
{
    [Route("api/[controller]/[action]")]
    public class AccountController:ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private readonly IAccountService<ApplicationUser, ApplicationRole> _accountService;

        public AccountController(
            IUnitOfWork unitOfWork, 
            IAccountService<ApplicationUser, ApplicationRole> accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<UserResultParam> GetCurrentUser()
        {
            var user = await _accountService.GetUserAsync(User);

            if (user != null)
                return new UserResultParam(username: user.UserName);
            else
                return new UserResultParam(username: null, success: false, message: "Error");

        }

        [HttpPost]
        public async Task<UserResultParam> Login([FromBody] LoginParam param)
        {
            var result = await _accountService.PasswordSignInAsync(param.Username, param.Password, false);
            return result.Succeeded 
                ? new UserResultParam(username: param.Username, success: true) 
                : new UserResultParam(username: null, success: false, message: "Failed");
        }

        [HttpPost]
        public async void LogOut()
        {
            await _accountService.SignOutAsync();
        }
    }

    public class LoginParam
    {
        public string Username;
        public string Password;
    }

    public class UserResultParam
    {
        public bool Success;
        public string Message;
        public string Username;

        public UserResultParam(string username, bool success = true, string message = "Success")
        {
            Success = success;
            Message = message;
            Username = username;

        }
    }
}
