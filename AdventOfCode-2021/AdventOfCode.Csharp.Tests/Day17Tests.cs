using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day17Tests
    {
        private readonly Day17 _day17Solution = new();

        [Fact]
        public void CorrectlyCalculatesSolutionPart1()
        {
            var inputData = GetTestData(0);

            var maxHeight = _day17Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("45", maxHeight);
        }

        [Fact]
        public void CorrectlyCalculatesSolutionPart2_After40Steps()
        {
            var inputData = GetTestData(0);

            var expressionValue = _day17Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("112", expressionValue);
        }


        private static string GetTestData(int testCase)
        {
            return testCase switch
            {
                0 => "target area: x=20..30, y=-10..-5",
                1 => "", 
                _ => ""
            };

        }


    }
}
