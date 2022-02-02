using System;
using System.Linq;
using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day11Tests
    {
        private readonly Day11 _day11Solution = new();

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 9)]
        public void CorrectlyCalculates1Step(int testCase, int expectedFlashesCount)
        {
            var energyMatrix = GetTestData(testCase).Split(Environment.NewLine)
                .Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
                .ToList();

            var result = Day11.SimulateFlashes(energyMatrix, 1);

            Assert.Equal(expectedFlashesCount, result);
        }

        [Fact]
        public void CorrectlyCalculatesSolutionPart1()
        {
            var inputData = GetTestData();

            var result = _day11Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("1656", result);
        }
        
        [Fact]
        public void CorrectlyCalculatesSolutionPart2()
        {
            var inputData = GetTestData();

            var result = _day11Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("195", result);
        }


        private static string GetTestData(int testCase = 0)
        {
            return testCase switch
            {
                1 => @"11111
19991
19191
19991
11111",
                _ => @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526"
            };
        }
    }
}
