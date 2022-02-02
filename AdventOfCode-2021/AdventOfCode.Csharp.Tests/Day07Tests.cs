using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day07Tests
    {
        private readonly Day07 _day07Solution = new();

        [Fact]
        public void SolutionPart1Test()
        {
            var inputData = GetTestData();

            var result = _day07Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("37", result);
        }

        [Fact]
        public void SolutionPart2Test()
        {
            var inputData = GetTestData();

            var result = _day07Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("168", result);
        }


        private static string GetTestData() =>
            @"16,1,2,0,4,2,7,1,2,14";
    }
}
