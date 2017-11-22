using MathTraining.Data.Domain.Statistic;

namespace MathTraining.Data.Common
{
    public interface IUnitOfWork
    {
        IRepository<ExerciseStatistic> ExerciseStatisticRepository { get; }

        void Commit();

        void CommitAsync();
    }
}
