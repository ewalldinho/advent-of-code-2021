using System;
using System.Linq;
using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day12Tests
    {
        private readonly Day12 _day12Solution = new();


        [Fact]
        public void CaveSystem_CorrectlyFindsAllPaths()
        {
            var inputData = GetTestData(0);
            var cavePairs = inputData.Split(Environment.NewLine).Select(line => line.Split('-')).ToList();
            var caveSystem = new Day12.CaveSystem(cavePairs);

            var allPaths = caveSystem.FindAllPaths();

            Assert.Equal(10, allPaths.Count);
            Assert.Contains("start,A,b,A,c,A,end", allPaths);
            Assert.Contains("start,A,b,A,end", allPaths);
            Assert.Contains("start,A,b,end", allPaths);
            Assert.Contains("start,A,c,A,b,A,end", allPaths);
            Assert.Contains("start,A,c,A,b,end", allPaths);
            Assert.Contains("start,A,c,A,end", allPaths);
            Assert.Contains("start,A,end", allPaths);
            Assert.Contains("start,b,A,c,A,end", allPaths);
            Assert.Contains("start,b,A,end", allPaths);
            Assert.Contains("start,b,end", allPaths);
        }

        [Fact]
        public void CaveSystem_CorrectlyFindsAllPathsWithExtraVisit()
        {
            var inputData = GetTestData(0);
            var cavePairs = inputData.Split(Environment.NewLine).Select(line => line.Split('-')).ToList();
            var caveSystem = new Day12.CaveSystem(cavePairs);

            var allPaths = caveSystem.FindAllPathsWithExtraVisit();
            
            Assert.Contains("start,A,b,A,b,A,c,A,end", allPaths);
            Assert.Contains("start,A,b,A,b,A,end", allPaths);
            Assert.Contains("start,A,b,A,b,end", allPaths);
            Assert.Contains("start,A,b,A,c,A,b,A,end", allPaths);
            Assert.Contains("start,A,b,A,c,A,b,end", allPaths);
            Assert.Contains("start,A,b,A,c,A,c,A,end", allPaths);
            Assert.Contains("start,A,b,A,c,A,end", allPaths);
            Assert.Contains("start,A,b,A,end", allPaths);
            Assert.Contains("start,A,b,d,b,A,c,A,end", allPaths);
            Assert.Contains("start,A,b,d,b,A,end", allPaths);
            Assert.Contains("start,A,b,d,b,end", allPaths);
            Assert.Contains("start,A,b,end", allPaths);
            Assert.Contains("start,A,c,A,b,A,b,A,end", allPaths);
            Assert.Contains("start,A,c,A,b,A,b,end", allPaths);
            Assert.Contains("start,A,c,A,b,A,c,A,end", allPaths);
            Assert.Contains("start,A,c,A,b,A,end", allPaths);
            Assert.Contains("start,A,c,A,b,d,b,A,end", allPaths);
            Assert.Contains("start,A,c,A,b,d,b,end", allPaths);
            Assert.Contains("start,A,c,A,b,end", allPaths);
            Assert.Contains("start,A,c,A,c,A,b,A,end", allPaths);
            Assert.Contains("start,A,c,A,c,A,b,end", allPaths);
            Assert.Contains("start,A,c,A,c,A,end", allPaths);
            Assert.Contains("start,A,c,A,end", allPaths);
            Assert.Contains("start,A,end", allPaths);
            Assert.Contains("start,b,A,b,A,c,A,end", allPaths);
            Assert.Contains("start,b,A,b,A,end", allPaths);
            Assert.Contains("start,b,A,b,end", allPaths);
            Assert.Contains("start,b,A,c,A,b,A,end", allPaths);
            Assert.Contains("start,b,A,c,A,b,end", allPaths);
            Assert.Contains("start,b,A,c,A,c,A,end", allPaths);
            Assert.Contains("start,b,A,c,A,end", allPaths);
            Assert.Contains("start,b,A,end", allPaths);
            Assert.Contains("start,b,d,b,A,c,A,end", allPaths);
            Assert.Contains("start,b,d,b,A,end", allPaths);
            Assert.Contains("start,b,d,b,end", allPaths);
            Assert.Contains("start,b,end", allPaths);
        }

        [Fact]
        public void CaveSystem_CorrectlyCountsPaths()
        {
            var inputData = GetTestData(0);
            var cavePairs = inputData.Split(Environment.NewLine).Select(line => line.Split('-')).ToList();
            var caveSystem = new Day12.CaveSystem(cavePairs);

            var actualPathsCount = caveSystem.CountPathsThroughCaveSystem();

            Assert.Equal(10, actualPathsCount);
        }

        [Fact]
        public void CaveSystem_CorrectlyCountsPathsWithExtraVisit()
        {
            var inputData = GetTestData(0);
            var cavePairs = inputData.Split(Environment.NewLine).Select(line => line.Split('-')).ToList();
            var caveSystem = new Day12.CaveSystem(cavePairs);

            var actualPathsCount = caveSystem.CountPathsThroughCaveSystemWithExtraVisit();

            Assert.Equal(36, actualPathsCount);
        }


        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 19)]
        [InlineData(2, 226)]
        public void CorrectlyCalculatesSolutionPart1(int testDataNumber, int expectedPathsCount)
        {
            var inputData = GetTestData(testDataNumber);

            var result = _day12Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal(expectedPathsCount.ToString(), result);
        }
        
        [Theory]
        [InlineData(0, 36)]
        [InlineData(1, 103)]
        [InlineData(2, 3509)]
        public void CorrectlyCalculatesSolutionPart2(int testDataNumber, int expectedPathsCount)
        {
            var inputData = GetTestData(testDataNumber);

            var result = _day12Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal(expectedPathsCount.ToString(), result);
        }


        private static string GetTestData(int dataNumber)
        {
            return dataNumber switch
            {
                0 => @"start-A
start-b
A-c
A-b
b-d
A-end
b-end",
                1 => @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc",
                _ => @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW"
            };
        }


    }
}
