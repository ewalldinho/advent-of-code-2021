using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 1: Sonar Sweep (https://adventOfCode.com/2021/day/1)
    public class Day01 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var data = inputData.Split(Environment.NewLine).Select(int.Parse).ToList();
            return part switch
            {
                Parts.Part1 => $"{SolvePart1(data)}",
                Parts.Part2 => $"{SolvePart2(data)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }

        private static int SolvePart1(IReadOnlyList<int> data)
        {
            var count = 0;
            for (var i = 1; i < data.Count; i++)
            {
                if (data[i] > data[i - 1])
                    count++;
            }
            return count;
        }

        private static int SolvePart2(IReadOnlyList<int> data)
        {
            var count = 0;
            int? prevMeasurementWindow = null;
            for (var i = 2; i < data.Count; i++)
            {
                var windowSum = data[i] + data[i - 1] + data[i - 2];
                if (windowSum > prevMeasurementWindow)
                {
                    count++;
                }
                prevMeasurementWindow = windowSum;
            }
            return count;
        }
    }
}
