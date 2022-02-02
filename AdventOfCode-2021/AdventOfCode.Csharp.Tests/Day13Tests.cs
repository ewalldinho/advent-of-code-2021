using System;
using System.Linq;
using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day13Tests
    {
        private readonly Day13 _day13Solution = new();

        [Fact]
        public void CorrectlyCalculatesSolutionPart1()
        {
            var inputData = GetTestData();

            var visibleDotsCount = _day13Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("17", visibleDotsCount);
        }
        
        [Fact]
        public void CorrectlyCalculatesSolutionPart2()
        {
            var inputData = GetTestData();

            var result = _day13Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal(0.ToString(), result);
        }


        private static string GetTestData()
        {
            return @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

        }


    }
}
