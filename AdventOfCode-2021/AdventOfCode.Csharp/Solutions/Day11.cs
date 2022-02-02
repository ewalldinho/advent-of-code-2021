using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 11: Dumbo Octopus (https://adventOfCode.com/2021/day/11)
    public class Day11 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var startingEnergyLevels = inputData.Split(Environment.NewLine)
                .Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
                .ToList();

            return part switch
            {
                Parts.Part1 => SolvePart1(startingEnergyLevels).ToString(),
                Parts.Part2 => $"{SolvePart2(startingEnergyLevels)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static int SolvePart1(IReadOnlyList<int[]> energyLevelsMatrix)
        {
            var flashesCount = SimulateFlashes(energyLevelsMatrix, 100);
            return flashesCount;
        }

        private static int SolvePart2(IReadOnlyList<int[]> energyLevelsMatrix)
        {
            var step = FindStepAllOctopusesFlashSimultaneously(energyLevelsMatrix);
            return step;
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

        private class Octopus
        {
            public bool HasFlashed { get; private set; }
            public int EnergyLevel { get; set; }

            public Octopus(int energyLevel)
            {
                EnergyLevel = energyLevel;
            }

            public bool CanFlash => !HasFlashed && EnergyLevel > 9;

            public void IncreaseEnergy()
            {
                EnergyLevel++;
            }

            public void Flash()
            {
                HasFlashed = true;
            }

            public void Reset()
            {
                HasFlashed = false;
                EnergyLevel = 0;
            }
        }

        public static int SimulateFlashes(IReadOnlyList<int[]> energyLevels, int simulationSteps)
        {
            var flashesCount = 0;

            var neighborsDeltas = new PointOffset[]
            {
                new(-1, -1), new(0, -1), new(1, -1),
                new(-1, 0), new(1, 0),
                new(-1, 1), new(0, 1), new(1, 1),
            };

            var octopuses = energyLevels.Select(row => row.Select(energyLevel => new Octopus(energyLevel)).ToArray()).ToList();

            var height = energyLevels.Count;
            var width = height > 0 ? energyLevels[0].Length : 0;

            for (var step = 1; step <= simulationSteps; step++)
            {
                for (var row = 0; row < height; row++)
                {
                    for (var col = 0; col < width; col++)
                    {
                        octopuses[row][col].IncreaseEnergy();
                    }
                }

                var isFlashing = octopuses.Any(row => row.Any(octopus => octopus.CanFlash));

                while (isFlashing)
                {
                    for (var row = 0; row < height; row++)
                    {
                        for (var col = 0; col < width; col++)
                        {
                            if (octopuses[row][col].CanFlash)
                            {
                                octopuses[row][col].Flash();
                                flashesCount++;

                                foreach (var delta in neighborsDeltas)
                                {
                                    var posY = row + delta.Vertical;
                                    var posX = col + delta.Horizontal;
                                    if (posX < 0 || posY < 0 || posX >= width || posY >= height)
                                        continue;

                                    octopuses[posY][posX].IncreaseEnergy();
                                }
                            }

                            //foreach (var delta in neighborsDeltas)
                            //{
                            //    var posY = row + delta.Vertical;
                            //    var posX = col + delta.Horizontal;
                            //    if (posX < 0 || posY < 0 || posX >= width || posY >= height)
                            //        continue;

                            //    if (octopuses[posY][posX].CanFlash && octopuses[posY][posX].EnergyLevel < int.MaxValue)
                            //    {
                            //        octopuses[row][col].IncreaseEnergy();
                            //    }

                            //}
                        }
                    }

                    isFlashing = octopuses.Any(row => row.Any(octo => octo.CanFlash));
                    //isFlashing = energyLevels.Any(row => row.Any(level => level > 9));
                }

                for (var row = 0; row < height; row++)
                {
                    for (var col = 0; col < width; col++)
                    {
                        if (octopuses[row][col].HasFlashed)
                            octopuses[row][col].Reset();
                    }
                }
            }

            return flashesCount;
        }

        public static int FindStepAllOctopusesFlashSimultaneously(IReadOnlyList<int[]> energyLevels)
        {
            
            var neighborsDeltas = new PointOffset[]
            {
                new(-1, -1), new(0, -1), new(1, -1),
                new(-1, 0), new(1, 0),
                new(-1, 1), new(0, 1), new(1, 1),
            };

            var octopuses = energyLevels.Select(row => row.Select(energyLevel => new Octopus(energyLevel)).ToArray()).ToList();

            var height = energyLevels.Count;
            var width = height > 0 ? energyLevels[0].Length : 0;
            var octopusesCount = height * width;
            var flashesCount = 0;
            var step = 0;

            while (flashesCount < octopusesCount)
            {
                step++;
                flashesCount = 0;

                for (var row = 0; row < height; row++)
                {
                    for (var col = 0; col < width; col++)
                    {
                        octopuses[row][col].IncreaseEnergy();
                    }
                }

                var isFlashing = octopuses.Any(row => row.Any(octopus => octopus.CanFlash));

                while (isFlashing)
                {
                    for (var row = 0; row < height; row++)
                    {
                        for (var col = 0; col < width; col++)
                        {
                            if (octopuses[row][col].CanFlash)
                            {
                                octopuses[row][col].Flash();
                                flashesCount++;

                                foreach (var delta in neighborsDeltas)
                                {
                                    var posY = row + delta.Vertical;
                                    var posX = col + delta.Horizontal;
                                    if (posX < 0 || posY < 0 || posX >= width || posY >= height)
                                        continue;

                                    octopuses[posY][posX].IncreaseEnergy();
                                }
                            }
                        }
                    }

                    isFlashing = octopuses.Any(row => row.Any(octo => octo.CanFlash));
                }

                for (var row = 0; row < height; row++)
                {
                    for (var col = 0; col < width; col++)
                    {
                        if (octopuses[row][col].HasFlashed)
                            octopuses[row][col].Reset();
                    }
                }
            }

            return step;
        }


    }
}
