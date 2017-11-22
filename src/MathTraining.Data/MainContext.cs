using MathTraining.Data.Domain.Identity;
using MathTraining.Data.Domain.Statistic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MathTraining.Data.Core
{
    public class MainContext: IdentityDbContext<ApplicationUser, ApplicationRole, string> 
    {
        public DbSet<ExerciseStatistic> ExerciseStatistic { get; set; }

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExerciseStatistic>();

        }

    }


}
