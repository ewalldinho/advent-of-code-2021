using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day05Tests
    {
        private readonly Day05 _day05Solution = new(); 

        [Fact]
        public void SolutionPart1Test()
        {
            var inputData = GetTestData();

            var result = _day05Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("5", result);
        }

        [Fact]
        public void SolutionPart2Test()
        {
            var inputData = GetTestData();

            var result = _day05Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("12", result);
        }


        private string GetTestData() =>
@"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

    }
}
