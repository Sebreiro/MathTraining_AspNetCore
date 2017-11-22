using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathTraining.Application
{
    public static class MedianCalculationHelper
    {
        public static double CalculateMedian(List<double> list)
        {
            var count = list.Count();
            if (list.Count == 1) return list.First();
            var halfIndex = count / 2;
            var sortedList = list.OrderBy(x => x).ToList();
            if (count % 2 == 0)
            {
                return (sortedList.ElementAt(halfIndex) + sortedList.ElementAt(halfIndex - 1)) / 2;
            }
            else
            {
                return sortedList.ElementAt(halfIndex);
            }
        }
    }
}
