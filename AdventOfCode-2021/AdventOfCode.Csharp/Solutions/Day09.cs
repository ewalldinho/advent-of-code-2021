using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 9: Smoke Basin (https://adventOfCode.com/2021/day/9)
    public class Day09 : IPuzzle
    {
        private static readonly PointOffset[] NeighborsOffsets =
        {
            new(-1, 0), 
            new(0, 1), 
            new(1, 0), 
            new(0, -1)
        };

        public string CalculateSolution(Parts part, string inputData)
        {
            var heightMap = inputData.Split(Environment.NewLine).Select(line => 
                line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToList();

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(heightMap)}",
                Parts.Part2 => $"{SolvePart2(heightMap)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }

        private static int SolvePart1(IReadOnlyList<int[]> heightMap)
        {
            var riskLevels = FindLowPoints(heightMap)
                .Select(p => p + 1);

            return riskLevels.Sum();
        }

        private static int SolvePart2(List<int[]> heightMap)
        {
            var basinSizes = ExploreBasins(heightMap);
            
            return basinSizes.OrderByDescending(size => size).Take(3).Aggregate(1, (acc, size) => acc * size);
        }


        private static IEnumerable<int> FindLowPoints(IReadOnlyList<int[]> heightMap)
        {
            var lowPoints = new List<int>();
            var neighborsDeltas = new PointOffset[] { new(-1, 0), new(0, 1), new(1, 0), new(0, -1) };
            var height = heightMap.Count;
            var width = height > 0 ? heightMap[0].Length : 0;
            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    var isLowPoint = true;
                    foreach (var delta in neighborsDeltas)
                    {
                        var posY = row + delta.Vertical;
                        var posX = col + delta.Horizontal;
                        if (posX < 0 || posY < 0 || posX >= width || posY >= height)
                            continue;
                        if (heightMap[posY][posX] <= heightMap[row][col])
                            isLowPoint = false;
                    }
                    if (isLowPoint)
                        lowPoints.Add(heightMap[row][col]);
                }
            }

            return lowPoints;
        }


        private static IEnumerable<int> ExploreBasins(List<int[]> heightMap)
        {
            var basinSizes = new List<int>();
            var height = heightMap.Count;
            var width = height > 0 ? heightMap[0].Length : 0;
            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    if (heightMap[row][col] >= 9)
                        continue;

                    var basinSize = ExploreBasin(heightMap, row, col);
                    if (basinSize > 0)
                        basinSizes.Add(basinSize);
                }
            }
            return basinSizes;
        }

        private static int ExploreBasin(IReadOnlyList<int[]> heightMap, int row, int col)
        {
            if (col < 0 || row < 0 || row >= heightMap.Count || col >= heightMap[0].Length)
                return 0;
            if (heightMap[row][col] >= 9)
                return 0;
            // mark the point to skip next time
            heightMap[row][col] = 10;
            var basinSize = 1;
            foreach (var offset in NeighborsOffsets)
            {
                basinSize += ExploreBasin(heightMap, row + offset.Vertical, col + offset.Horizontal);
            }

            return basinSize;
            //return 1 + NeighborsOffsets.Sum(offset => ExploreBasin(heightMap, row + offset.Vertical, col + offset.Horizontal));
        }

        private class PointOffset
        {
            public int Vertical { get; }
            public int Horizontal { get; }

            public PointOffset(int vertical, int horizontal)
            {
                Vertical = vertical;
                Horizontal = horizontal;
            }
        }
    }
}
