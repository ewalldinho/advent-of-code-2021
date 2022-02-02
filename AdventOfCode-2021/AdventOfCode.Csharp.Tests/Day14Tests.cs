using AdventOfCode.Csharp.Solutions;
using Xunit;

namespace AdventOfCode.Csharp.Tests
{
    public class Day14Tests
    {
        private readonly Day14 _day14Solution = new();

        [Fact]
        public void CorrectlyCalculatesSolutionPart1_After10Steps()
        {
            var inputData = GetTestData();

            var diffBetweenMostAndLeastCommonElements = _day14Solution.CalculateSolution(Parts.Part1, inputData);

            Assert.Equal("1588", diffBetweenMostAndLeastCommonElements);
        }

        [Fact]
        public void CorrectlyCalculatesSolutionPart2_FragmentAfter40Steps()
        {
            var inputData = @"NN

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

            var diffBetweenMostAndLeastCommonElements = _day14Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("2188189693529", diffBetweenMostAndLeastCommonElements);
        }

        [Fact]
        public void CorrectlyCalculatesSolutionPart2_After40Steps()
        {
            var inputData = GetTestData();

            var diffBetweenMostAndLeastCommonElements = _day14Solution.CalculateSolution(Parts.Part2, inputData);

            Assert.Equal("2188189693529", diffBetweenMostAndLeastCommonElements);
        }


        private static string GetTestData()
        {
            return @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

        }


    }
}
