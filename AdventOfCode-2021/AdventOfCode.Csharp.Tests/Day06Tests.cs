using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day06Tests
    {
        private readonly Day06 _day06Solution = new();

        [Fact]
        public void SolutionPart1Test()
        {
            var inputData = GetTestData();

            var result = _day06Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("5934", result);
        }

        [Fact]
        public void SolutionPart2Test()
        {
            var inputData = GetTestData();

            var result = _day06Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("26984457539", result);
        }


        private static string GetTestData() =>
            @"3,4,3,1,2";
    }
}
