using System;
using System.Linq;
using AdventOfCode.Csharp.Solutions;

namespace AdventOfCode.MainApp.Infrastructure
{
    public static class PuzzleSolutionFactory
    {
        public static IPuzzle GetPuzzleSolution(AdventDays day)
        {
            if (! Enum.IsDefined(typeof(AdventDays), day))
                throw new ArgumentException($"Argument {nameof(day)} must have value in range [1..25]");

            var dayNumber = (int)day;
            var className = $"Day{dayNumber:00}";
            var assembly = typeof(IPuzzle).Assembly;
            var solutionClass = assembly.GetTypes()
                .Where(t => t.IsClass).Where(t => t.GetInterfaces().Contains(typeof(IPuzzle)))
                .FirstOrDefault(t => t.Name == className);

            if (solutionClass != null)
            {
                var solution = Activator.CreateInstance(solutionClass) as IPuzzle;
                return solution ?? throw new ApplicationException($"Can not create instance of class '{solutionClass.FullName}'");
            }

            throw new ApplicationException($"There is no solution for day {day}");
        }
    }
}
