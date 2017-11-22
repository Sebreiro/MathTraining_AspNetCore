using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MathTraining.Service.Common.Identity
{
    public interface IAccountService<TUser, TRole> where TUser : class where TRole : class
    {
        Task<TUser> GetUserAsync(ClaimsPrincipal principal);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent);
        Task SignOutAsync();
    }
}
