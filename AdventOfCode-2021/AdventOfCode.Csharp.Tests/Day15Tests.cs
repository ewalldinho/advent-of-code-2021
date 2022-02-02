using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day15Tests
    {
        private readonly Day15 _day15Solution = new();

        [Fact]
        public void CorrectlyCalculatesSolutionPart1_After10Steps()
        {
            var inputData = GetTestData();

            var totalRisk = _day15Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("40", totalRisk);
        }
        
        [Fact]
        public void CorrectlyCalculatesSolutionPart2_After40Steps()
        {
            var inputData = GetTestData();

            var diffBetweenMostAndLeastCommonElements = _day15Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("0", diffBetweenMostAndLeastCommonElements);
        }


        private static string GetTestData()
        {
            return @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

        }


    }
}
