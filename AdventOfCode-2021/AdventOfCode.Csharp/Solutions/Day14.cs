using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 14: Extended Polymerization (https://adventofcode.com/2021/day/14)
    public class Day14 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var data = inputData.Split(Environment.NewLine + Environment.NewLine);
            var polymerTemplate = data[0];
            var insertionRules = data[1].Split(Environment.NewLine)
                .Select(rule => rule.Split(" -> "))
                .Select(pair => (pair[0], pair[1][0])).ToList();

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(polymerTemplate, insertionRules, 10)}",
                Parts.Part2 => $"{SolvePart1(polymerTemplate, insertionRules, 40)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static long SolvePart1(string polymerTemplate, IReadOnlyList<(string match, char insertChar)> insertionRules, int stepsCount)
        {
            var currentPolymer = polymerTemplate;
            for (var step = 0; step < stepsCount; step++)
            {
                Console.WriteLine($"- - Step - {step} -");
                Dictionary<int, char> insertions = new();
                foreach (var (match, insertChar) in insertionRules)
                {
                    var index = -1;
                    do
                    {
                        index = currentPolymer.IndexOf(match, index + 1, StringComparison.Ordinal);
                        if (index > -1)
                        {
                            insertions[index+1] = insertChar;
                        }
                    } while (index > -1);
                }

                // apply insertions backwards
                var polymerElements = currentPolymer.ToList();
                foreach (var (index, insertChar) in insertions.OrderByDescending(ins => ins.Key))
                {
                    polymerElements.Insert(index, insertChar);
                }

                currentPolymer = new string(polymerElements.ToArray());

                var statis = currentPolymer.GroupBy(ch => ch).Select(g => new
                {
                    ch = g.Key,
                    cnt = g.Count()
                });

            }

            var stats = currentPolymer.GroupBy(c => c).Select(g => (element: g.Key, count: g.LongCount())).ToList();
            var mostCommonElementQuantity = stats.Max(e => e.count);
            var leastCommonElementQuantity = stats.Min(e => e.count);

            return mostCommonElementQuantity - leastCommonElementQuantity;
        }

        private static int SolvePart2(string polymerTemplate, IEnumerable<(string match, char insertChar)> insertionRules)
        {
            
            return 0;
        }
        
    }
}
