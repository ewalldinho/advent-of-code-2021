using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day03Tests
    {
        private readonly Day03 _day03Solution = new();

        [Fact]
        public void SolutionPart1Test()
        {
            var inputData = GetTestData();
            
            var result = _day03Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("198", result);
        }

        [Fact]
        public void SolutionPart2Test()
        {
            var inputData = GetTestData();

            var result = _day03Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("230", result);
        }
        

        private static string GetTestData()
        {
            return
@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";
        }
    }
}