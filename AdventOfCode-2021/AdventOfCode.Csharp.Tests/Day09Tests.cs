using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day09Tests
    {
        private readonly Day09 _day09Solution = new();

        [Fact]
        public void SolutionPart1Test()
        {
            var inputData = GetTestData();

            var result = _day09Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("15", result);
        }

        [Fact]
        public void SolutionPart2Test()
        {
            var inputData = GetTestData();

            var result = _day09Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("1134", result);
        }


        private static string GetTestData() =>
            @"2199943210
3987894921
9856789892
8767896789
9899965678";

    }
}
