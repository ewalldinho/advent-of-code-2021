using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day02Tests
    {
        private readonly Day02 _day02Solution = new();

        [Fact]
        public void SolutionPart1Test()
        {
            var inputData = GetTestData();
            
            var result = _day02Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("150", result);
        }

        [Fact]
        public void SolutionPart2Test()
        {
            var inputData = GetTestData();

            var result = _day02Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("900", result);
        }


        [Fact]
        public void SolutionPart1LongVersionTest()
        {
            var inputData = GetTestData();

            var result = _day02Solution.SolvePart1v2(inputData);

            Assert.Equal(150, result);
        }

        [Fact]
        public void SolutionPart2LongVersionTest()
        {
            var inputData = GetTestData();

            var result = _day02Solution.SolvePart2v2(inputData);

            Assert.Equal(900, result);
        }

        private static string GetTestData()
        {
            return
@"forward 5
down 5
forward 8
up 3
down 8
forward 2";
        }
    }
}