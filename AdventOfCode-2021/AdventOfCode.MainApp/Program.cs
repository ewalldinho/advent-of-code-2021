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
                    TextUI.Run();
                    break;
                case 1:
                    ShowGui();
                    break;
                case 2:
                    // C# zone
                    const AdventDays day = AdventDays.Day01;
                    var input = PuzzleData.GetData(day);

                    Console.WriteLine($"Day #{day}");
                    var results = RunSolution((int)day);
                    Console.WriteLine($"Part 1: ");
                    Console.WriteLine($"{results.part1}");
                    Console.WriteLine($"Part 2: ");
                    Console.WriteLine($"{results.part2}");
                    break;
                case 3:
                    // F# zone
                    RunFsharpSolution(AdventDays.Day01, null);
                    break;
            }
            
        }

        private static void ShowGui()
        {
            (string part1, string part2) answer;
            var ui = new AppUI();
            var selectedDay = 1;
            var isRunning = true;
            while (isRunning)
            {
                ui.Display(selectedDay);

                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(10);
                }

                var key = Console.ReadKey();
                while (Console.KeyAvailable)
                    key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;
                    case ConsoleKey.LeftArrow:
                        selectedDay -= 1;
                        if (selectedDay < 1) selectedDay = 25;
                        break;
                    case ConsoleKey.RightArrow:
                        selectedDay += 1;
                        if (selectedDay > 25) selectedDay = 1;
                        break;
                    case ConsoleKey.Enter:
                        ui.DisplayProgress();
                        try
                        {
                            answer = RunSolution(selectedDay);

                            ui.DisplayResults(answer.part1, answer.part2);
                        }
                        catch (Exception ex)
                        {
                            ui.DisplayChoice(selectedDay);

                            Console.WriteLine(ex.Message);

                            while (!Console.KeyAvailable)
                            {
                                Thread.Sleep(10);
                            }
                        }

                        break;
                }

                if (key.KeyChar >= '0' || key.KeyChar <= '9')
                {
                    Console.Write(key.KeyChar);
                }

                Thread.Sleep(10);
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

        private static void RunFsharpSolution(AdventDays dayOfAdvent, Parts? part = null)
        {
            var inputData = PuzzleData.GetData(dayOfAdvent);
            var answerPart1 = "part1 - not solved";
            var answerPart2 = "part2 - not solved";

            switch (dayOfAdvent)
            {
                case AdventDays.Day01:
                    var answerD01P1 = Fsharp.Solutions.Day01.solvePart1(inputData);
                    var answerD01P2 = Fsharp.Solutions.Day01.solvePart2(inputData);
                    Console.WriteLine($"Day 01: {answerD01P1}, {answerD01P2}");
                    break;

                case AdventDays.Day02:
                    var answerD02P1 = Fsharp.Solutions.Day02.solvePart1(inputData);
                    var answerD02P2 = Fsharp.Solutions.Day02.solvePart2(inputData);
                    Console.WriteLine($"Day 02: {answerD02P1}, {answerD02P2}");
                    break;

                case AdventDays.Day03:
                    if (part is null)
                    {
                        var answerD3P1 = Fsharp.Solutions.Day03.solvePart1(inputData);
                        var answerD3P2 = Fsharp.Solutions.Day03.solvePart2(inputData);
                        answerPart1 = $"{answerD3P1}";
                        answerPart2 = $"{answerD3P2}";
                    }
                    else
                    {
                        switch (part)
                        {
                            case Parts.Part1:
                                var answerD3P1 = Fsharp.Solutions.Day03.solvePart1(inputData);
                                answerPart1 = $"{answerD3P1}";
                                break;
                            case Parts.Part2:
                            {
                                var answerD3P2 = Fsharp.Solutions.Day03.solvePart2(inputData);
                                answerPart2 = $"{answerD3P2}";
                                break;
                            }
                            default:
                                throw new ArgumentOutOfRangeException(nameof(part), part, null);
                        }
                    }
                    break;

                default:
                    Console.WriteLine($"Day {dayOfAdvent} still has no solution.");
                    break;
            }

            Console.WriteLine($"Day {dayOfAdvent}: {answerPart1}, {answerPart2}");
        }
    }

}

