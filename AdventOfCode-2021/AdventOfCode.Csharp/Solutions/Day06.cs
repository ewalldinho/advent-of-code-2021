using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 6: Lanternfish (https://adventOfCode.com/2021/day/6)
    public class Day06 : IPuzzle
    {
        private const byte YoungLanternFishTimeToBreed = 8;
        private const byte AdultLanternFishTimeToBreed = 6;

        public string CalculateSolution(Parts part, string inputData)
        {
            var data = inputData.Split(',').Select(byte.Parse).ToList();
            return part switch
            {
                Parts.Part1 => $"{SimulateLanternFishes(data, 80)}",
                Parts.Part2 => $"{SimulateLanternFishesOptimized(data, 256)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }

        private static int SimulateLanternFishes(IEnumerable<byte> fishSchool, int simulationDuration)
        {
            for (var day = 0; day < simulationDuration; day++)
            {
                fishSchool = SimulateDay(fishSchool);

                if ((day+1) % 16 > 0) Console.Write(".");
                else Console.WriteLine($". {1+day:00} days passed");
            }
            return fishSchool.Count();
        }

        private static long SimulateLanternFishesOptimized(IEnumerable<byte> fishSchool, int simulationDuration)
        {
            var packedFish = PackFishes(fishSchool);
            for (var day = 0; day < simulationDuration; day++)
            {
                packedFish = SimulateDay(packedFish);
                
                if ((day + 1) % 16 > 0) Console.Write(".");
                else Console.WriteLine($". {1 + day:00} days passed");
            }
            return packedFish.Select(f => f.Value).Sum();
        }


        private static IEnumerable<byte> SimulateDay(IEnumerable<byte> fishes)
        {
            var fishesAfterDay = fishes.ToList();
            var spawnsCount = fishesAfterDay.Count(internalTimer => internalTimer == 0);
            fishesAfterDay = fishesAfterDay.Select(timer => (byte)(timer == 0 ? AdultLanternFishTimeToBreed : timer - 1)).ToList();
            fishesAfterDay.AddRange(Enumerable.Repeat(YoungLanternFishTimeToBreed, spawnsCount));
            return fishesAfterDay;
        }

        private static Dictionary<byte, long> PackFishes(IEnumerable<byte> fishes)
        {
            var packedFishes = fishes.GroupBy(timer => timer).ToDictionary(g => g.Key, g => (long)g.Count());
            for (byte timer = 0; timer <= YoungLanternFishTimeToBreed; timer++)
            {
                if (!packedFishes.ContainsKey(timer))
                    packedFishes[timer] = 0;
            }
            return packedFishes;
        }

        private static Dictionary<byte, long> SimulateDay(IReadOnlyDictionary<byte, long> fishSchool)
        {
            var spawnCount = fishSchool[0];
            var newBatch = new Dictionary<byte, long>();
            for (var timer = YoungLanternFishTimeToBreed; timer > 0; timer--)
            {
                newBatch[(byte)(timer - 1)] = fishSchool[timer];
            }
            newBatch[6] += spawnCount;
            newBatch[8] = spawnCount;
            return newBatch;
        }
    }
}
