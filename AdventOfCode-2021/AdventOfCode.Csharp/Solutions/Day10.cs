using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 10: Syntax Scoring (https://adventOfCode.com/2021/day/10)
    public class Day10 : IPuzzle
    {
        private const string OpeningCharacters = "([{<";
        private const string ClosingCharacters = ")]}>";
        private static readonly Dictionary<char, int> ErrorScoreTable = new()
        {
            [')'] = 3,
            [']'] = 57,
            ['}'] = 1197,
            ['>'] = 25137
        };
        private static readonly Dictionary<char, int> AutocompleteScoreTable = new()
        {
            [')'] = 1,
            [']'] = 2,
            ['}'] = 3,
            ['>'] = 4
        };

        public string CalculateSolution(Parts part, string inputData)
        {
            var navigationSubsystem = inputData.Split(Environment.NewLine);

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(navigationSubsystem)}",
                Parts.Part2 => $"{SolvePart2(navigationSubsystem)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static int SolvePart1(IEnumerable<string> navigationSubsystem)
        {
            var syntaxErrorScore = 0;
            foreach (var line in navigationSubsystem)
            {
                var errorIndex = FindIllegalCharacterIndex(line);
                if (errorIndex > -1)
                {
                    syntaxErrorScore += ErrorScoreTable[line[errorIndex]];
                }
            }
            return syntaxErrorScore;
        }

        private static long SolvePart2(IEnumerable<string> navigationSubsystem)
        {
            var incompleteLines = navigationSubsystem.Where(line => -1 == FindIllegalCharacterIndex(line)).ToList();

            var autocompleteScores = new List<long>();

            foreach (var line in incompleteLines)
            {
                var completionString = FindCompletionString(line);
                var score = CalcAutocompleteScore(completionString);
                autocompleteScores.Add(score);
            }

            var count = autocompleteScores.Count;
            if (count % 2 == 0)
                throw new InvalidOperationException($"Total number of autocomplete scores is invalid: {count}");

            var orderedScores = autocompleteScores.OrderBy(score => score).ToList();

            return orderedScores[count/2];
        }


        public static int FindIllegalCharacterIndex(string line)
        {
            var characters = new Stack<char>();
            for (var i = 0; i < line.Length; i++)
            {
                if (ClosingCharacters.Contains(line[i]))
                {
                    if (characters.Count == 0)
                        return i;

                    var closingCharIndex = ClosingCharacters.IndexOf(line[i]);
                    var prevChar = characters.Pop();
                    if (closingCharIndex != OpeningCharacters.IndexOf(prevChar))
                        return i;
                }
                else 
                {
                    characters.Push(line[i]);
                }
            }

            return -1;
        }


        public static string FindCompletionString_v0(string line)
        {
            var lineCharacters = new Stack<char>(line.ToCharArray());
            var tempCloseCharacters = new Stack<char>();
            var completeCharacters = new List<char>();
            while (lineCharacters.Count > 0)
            {
                var character = lineCharacters.Pop();
                if (OpeningCharacters.Contains(character))
                {
                    if (tempCloseCharacters.Count == 0)
                    {
                        var index = OpeningCharacters.IndexOf(character);
                        completeCharacters.Add(ClosingCharacters[index]);
                    }
                    else {
                        var closeChar = tempCloseCharacters.Pop();
                        if (ClosingCharacters.IndexOf(closeChar) != OpeningCharacters.IndexOf(character))
                            throw new InvalidOperationException($"There is characters mismatch '{character}' and '{closeChar}' ");
                        // otherwise just forget those 2 characters
                    }
                }
                else if (ClosingCharacters.Contains(character))
                {
                    tempCloseCharacters.Push(character);
                }

            }

            return new string(completeCharacters.ToArray());
        }

        public static string FindCompletionString(string line)
        {
            var extraOpeningChars = new Stack<char>();
            foreach (var ch in line)
            {
                if (OpeningCharacters.Contains(ch)) extraOpeningChars.Push(ch);
                else extraOpeningChars.Pop();
            }

            var completionChars = extraOpeningChars.Select(c => ClosingCharacters[OpeningCharacters.IndexOf(c)]);

            return string.Join(string.Empty, completionChars);
        }

        public static long CalcAutocompleteScore(string completionString)
        {
            var score = 0L;
            foreach (var character in completionString)
            {
                score *= 5;
                score += AutocompleteScoreTable[character];
            }
            return score;
        }
    }
}
