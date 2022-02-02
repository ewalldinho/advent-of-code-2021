using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 8: Seven Segment Search (https://adventOfCode.com/2021/day/8)
    public class Day08 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var notesEntries = inputData.Split(Environment.NewLine)
                .Select(line => line.Split(" | ")).ToList();

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(notesEntries)}",
                Parts.Part2 => $"{SolvePart2(notesEntries)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }

        private static int SolvePart1(IEnumerable<string[]> notes)
        {
            var uniqueLengths = new int[] { 2, 3, 4, 7 };
            var digit1478Count = notes.Select(n => n[1])
                .Select(output => output.Split(' '))
                .Sum(output => output.Count(pattern => uniqueLengths.Contains(pattern.Length)));
            return digit1478Count;
        }

        private static int SolvePart2(IEnumerable<string[]> entries)
        {
            var outputSum = 0;
            foreach (var note in entries)
            {
                var digitPatterns = GetDigitPatterns(note[0]);
                var outputNumber = CalcOutputNumber(note[1], digitPatterns);
                outputSum += outputNumber;
            }
            return outputSum;
        }

        private static List<string> GetDigitPatterns(string uniqueSignalPatterns)
        {
            var digitPatterns = new string[10];

            var sortedPatterns = uniqueSignalPatterns.Split(' ').Select(pattern => string.Join("", pattern.ToCharArray().OrderBy(c => c))).ToList();
            digitPatterns[1] = sortedPatterns.First(p => p.Length == 2);
            digitPatterns[4] = sortedPatterns.First(p => p.Length == 4);
            digitPatterns[7] = sortedPatterns.First(p => p.Length == 3);
            digitPatterns[8] = sortedPatterns.First(p => p.Length == 7);
            
            var otherPatterns = sortedPatterns.Where(p => p.Length == 5 || p.Length == 6);
            foreach (var pattern in otherPatterns)
            {
                if (pattern.Length == 5)
                {
                    // 2, 3, or 5
                    if (CalcDifference(digitPatterns[1], pattern) == 0)
                        digitPatterns[3] = pattern;
                    else if (CalcDifference(digitPatterns[4], pattern) == 1)
                        digitPatterns[5] = pattern;
                    else
                        digitPatterns[2] = pattern;
                }
                else
                {
                    // 0, 6, or 9
                    if (CalcDifference(digitPatterns[4], pattern) == 0)
                        digitPatterns[9] = pattern;
                    else if (CalcDifference(digitPatterns[1], pattern) == 0)
                        digitPatterns[0] = pattern;
                    else
                        digitPatterns[6] = pattern;
                }
            }

            return digitPatterns.ToList();
        }

        private static int CalcDifference(string sourcePattern, string lookupPattern)
        {
            return sourcePattern.Count(c => !lookupPattern.Contains(c));
        }

        private static int CalcOutputNumber(string fourDigitOutputValue, List<string> digitsPatterns)
        {
            var number = 0;
            var dec = 1;
            var digits = fourDigitOutputValue.Split(' ').Select(d => string.Join("", d.ToCharArray().OrderBy(c => c)));
            foreach (var digitPattern in digits.Reverse())
            {
                var digit = digitsPatterns.IndexOf(digitPattern);
                number += digit * dec;
                dec *= 10;
            }
            return number;
        }
    }
}
