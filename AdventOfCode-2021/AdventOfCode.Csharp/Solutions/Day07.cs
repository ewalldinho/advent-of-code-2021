using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 7: The Treachery of Whales (https://adventOfCode.com/2021/day/7)
    public class Day07 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var crabHorizontalPositions = inputData.Split(',').Select(int.Parse).ToList();

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(crabHorizontalPositions)}",
                Parts.Part2 => $"{SolvePart2(crabHorizontalPositions)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }

        private static int SolvePart1(IReadOnlyCollection<int> horizontalPositions)
        {
            var min = horizontalPositions.Min();
            var max = horizontalPositions.Max();
            var minFuelUsed = int.MaxValue;
            for (var i = min; i <= max; i++)
            {
                var sum = horizontalPositions.Select(x => x >= i ? x - i : i - x).Sum();
                if (sum < minFuelUsed)
                    minFuelUsed = sum;
            }

            return minFuelUsed;
        }

        private static int SolvePart2(IReadOnlyCollection<int> positions)
        {
            var min = positions.Min();
            var max = positions.Max();
            var minFuelUsed = int.MaxValue;
            var moveCosts = PreCalculateCosts(max-min);
            for (var i = min; i <= max; i++)
            {
                var sum = positions.Select(x => moveCosts[x >= i ? x - i : i - x]).Sum();
                if (sum < minFuelUsed)
                    minFuelUsed = sum;
            }

            return minFuelUsed;
        }

        private static Dictionary<int, int> PreCalculateCosts(int upToSteps)
        {
            var costs = new Dictionary<int, int>
            {
                [0] = 0
            };
            for (var i = 1; i <= upToSteps; i++)
            {
                costs[i] = costs[i - 1] + i;
            }
            return costs;
        }
    }
}
