using System;
using System.Collections.Generic;
using System.Text;
using MathTraining.Data.Common;
using MathTraining.Data.Domain.Statistic;

namespace MathTraining.Data.Core
{
    public class MainUnitOfWork: IUnitOfWork
    {
        private readonly MainContext _context;

        public MainUnitOfWork(MainContext context)
        {
            _context = context;
        }

        public IRepository<ExerciseStatistic> ExerciseStatisticRepository => new Repository<ExerciseStatistic>(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void CommitAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
