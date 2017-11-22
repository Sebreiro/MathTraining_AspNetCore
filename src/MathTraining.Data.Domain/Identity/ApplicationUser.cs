using Microsoft.AspNetCore.Identity;

namespace MathTraining.Data.Domain.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
    }
}
