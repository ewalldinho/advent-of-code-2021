using System;
using System.Threading;
using AdventOfCode.Csharp.Solutions;
using AdventOfCode.MainApp.Infrastructure;

namespace AdventOfCode.MainApp
{

    public static class Program
    {
        public static void Main(string[] args)
        {
            var uiMode = 0;

            switch (uiMode)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    // C# zone
                    const AdventDays day = AdventDays.Day1;
                    var input = PuzzleData.GetData(day);

                    Console.WriteLine($"Day #{day}");
                    var results = RunSolution((int)day);
                    Console.WriteLine($"Part 1: ");
                    Console.WriteLine($"{results.part1}");
                    Console.WriteLine($"Part 2: ");
                    Console.WriteLine($"{results.part2}");
                    break;
                case 3:
                    break;
            }
            
        }

        
        private static (string part1, string part2) RunSolution(int day)
        {
            if (day is < 1 or > 25)
                throw new ArgumentException("Day must be in range 1..25");
            
            var dayOfAdvent = (AdventDays)day;
            var inputData = PuzzleData.GetData(dayOfAdvent);
            var puzzleSolution = PuzzleSolutionFactory.GetPuzzleSolution(dayOfAdvent);
            
            var part1Result = puzzleSolution.CalculateSolution(Parts.Part1, inputData);
            var part2Result = puzzleSolution.CalculateSolution(Parts.Part2, inputData);

            return (part1Result, part2Result);
        }
    }

}

