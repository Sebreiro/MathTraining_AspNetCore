using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using MathTraining.Data.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace MathTraining.Service.Common.Identity
{
    public class AccountService<TUser,TRole>:IAccountService<TUser,TRole> where TUser:class where TRole : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly RoleManager<TRole> _roleManager;
        private readonly SignInManager<TUser> _signInManager;



        public AccountService(
            UserManager<TUser> userManager,
            RoleManager<TRole> roleManager,
            SignInManager<TUser> signInManager)
        { 
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public Task<TUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return _userManager.GetUserAsync(principal);
        }

        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent)
        {
            return _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure: false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        } 
    }
}
