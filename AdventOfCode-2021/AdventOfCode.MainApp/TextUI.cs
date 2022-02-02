using System;
using System.Linq;
using AdventOfCode.Csharp.Solutions;
using AdventOfCode.MainApp.Infrastructure;

namespace AdventOfCode.MainApp
{
    internal static  class TextUI
    {
        private enum UserChoice
        {
            Error = -1,
            Quit = 0,
            Solve = 1,
        }

        public static void Run()
        {
            var isRunning = true;
            while (isRunning)
            {
                PrintMenu();

                var (userChoice, day) = ReadUserChoice();
                switch (userChoice)
                {
                    case UserChoice.Error:
                        Console.WriteLine(" Invalid advent day. It must be from interval [1..25]");
                        break;
                    case UserChoice.Solve:
                        Console.WriteLine();
                        Console.WriteLine($" Solution for day {day:00}");
                        var (left, top) = Console.GetCursorPosition();
                        Console.WriteLine(" wait for it...");

                        var (part1, part2) = RunSolutionForDay(day);
                        
                        Console.SetCursorPosition(left, top);
                        Console.WriteLine("               ");
                        Console.WriteLine($"Part 1: {Environment.NewLine}{part1}");
                        Console.WriteLine();
                        Console.WriteLine($"Part 2: {Environment.NewLine}{part2}");
                        Console.WriteLine();
                        break;
                    case UserChoice.Quit:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Try again");
                        break;
                }

                Console.WriteLine(" Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine(" .--- --- ---- --- ---.");
            Console.WriteLine(" :   Advent of Code   |");
            Console.WriteLine(" |        2021        :");
            Console.WriteLine(" '--- --- ---- --- ---'");
            Console.WriteLine(" Enter the day of advent [1..25] ");
            Console.Write(" or { [q]uit, e[x]it, 0 } to quit: ");
        }

        private static (UserChoice choice, int day) ReadUserChoice()
        {
            var quitCommands = new[] { "q", "quit", "exit", "x", "0" };
            var choice = Console.ReadLine();

            if (int.TryParse(choice, out var day) && day is >= 1 and <= 25)
                return (UserChoice.Solve, day);
            return quitCommands.Contains(choice?.Trim(), StringComparer.InvariantCultureIgnoreCase) ? (UserChoice.Quit, 0) : (UserChoice.Error, 0);
        }

        private static (string part1, string part2) RunSolutionForDay(int day)
        {
            var dayOfAdvent = (AdventDays)day;
            var inputData = PuzzleData.GetData(dayOfAdvent);
            var puzzleSolution = PuzzleSolutionFactory.GetPuzzleSolution(dayOfAdvent);

            var part1Result = puzzleSolution.CalculateSolution(Parts.Part1, inputData);
            var part2Result = puzzleSolution.CalculateSolution(Parts.Part2, inputData);

            return (part1Result, part2Result);
        }
    }
}
