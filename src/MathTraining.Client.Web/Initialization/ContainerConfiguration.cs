using Autofac;
using MathTraining.Data.Common;
using MathTraining.Data.Core;
using MathTraining.Data.Domain.Identity;
using MathTraining.Service.Common.Identity;

namespace MathTraining.Client.Web.Initialization
{
    public class ContainerConfiguration
    {
        public static void Configure(ContainerBuilder builder)
        {

            builder.RegisterType<MainUnitOfWork>().As<IUnitOfWork>();
            
            builder.RegisterType<AccountService<ApplicationUser,ApplicationRole>>().As<IAccountService<ApplicationUser,ApplicationRole>>();
            
            
        }
    }
}
