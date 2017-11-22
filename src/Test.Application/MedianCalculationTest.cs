using System;
using System.Collections.Generic;
using MathTraining.Application;
using Xunit;

namespace Test.Application
{
    public class MedianCalculationTest
    {
        [Fact]
        public void EvenNumbers()
        {
            var list = new List<double>() {1, 2, 3, 4, 5, 6, 7, 8};
            var median = MedianCalculationHelper.CalculateMedian(list);
            Assert.Equal(median, 4.5);
        }

        [Fact]
        public void OddNumbers()
        {
            var list = new List<double>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
            var median = MedianCalculationHelper.CalculateMedian(list);
            Assert.Equal(median, 5);
        }
    }
}
