using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day01Tests
    {
        private readonly Day01 _day01Solution = new();

        [Fact]
        public void SolutionPart1Test()
        {
            var inputData = GetTestData();
            
            var result = _day01Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("7", result);
        }

        [Fact]
        public void SolutionPart2Test()
        {
            var inputData = GetTestData();

            var result = _day01Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("5", result);
        }
        

        private static string GetTestData()
        {
            return
@"199
200
208
210
200
207
240
269
260
263";
        }
    }
}