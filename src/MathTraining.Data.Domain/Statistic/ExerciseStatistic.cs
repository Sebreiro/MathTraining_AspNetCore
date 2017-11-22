using System;
using MathTraining.Application;
using MathTraining.Data.Domain.Identity;

namespace MathTraining.Data.Domain.Statistic
{
    public class ExerciseStatistic
    {
        public long Id { get; set; }
        public ExerciseOperatorType Operator { get; set; }
        public float FirstNumber { get; set; }
        public float SecondNumber { get; set; }
        public float? UserResult { get; set; }
        public bool IsResultCorrect { get; set; }
        public TimeSpan UserResponsetime { get; set; }
        public DateTimeOffset Date { get; set; }
        public ApplicationUser User { get; set; }
    }
}
