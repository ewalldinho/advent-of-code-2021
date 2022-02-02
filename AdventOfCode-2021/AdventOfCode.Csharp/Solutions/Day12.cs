using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 12: Passage Pathing (https://adventofcode.com/2021/day/12)
    public class Day12 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var connectedCavePairs = inputData.Split(Environment.NewLine).Select(line => line.Split('-')).ToList();

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(connectedCavePairs)}",
                Parts.Part2 => $"{SolvePart2(connectedCavePairs)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static int SolvePart1(IEnumerable<string[]> connectedCavePairs)
        {
            var caveSystem = new CaveSystem(connectedCavePairs);
            return caveSystem.CountPathsThroughCaveSystem();
        }

        private static int SolvePart2(IEnumerable<string[]> connectedCavePairs)
        {
            var caveSystem = new CaveSystem(connectedCavePairs);
            return caveSystem.CountPathsThroughCaveSystemWithExtraVisit();
        }


        private class Cave
        {
            public List<string> ConnectedCaves { get; } = new();
        }

        public class CaveSystem
        {
            private readonly Dictionary<string, Cave> _caves;
            private int _pathsCount;
            private List<string> _allPaths;

            public CaveSystem(IEnumerable<string[]> connectedCavePairs)
            {
                _caves = new Dictionary<string, Cave>();
                _allPaths = new List<string>();

                foreach (var pair in connectedCavePairs)
                {
                    if (!_caves.ContainsKey(pair[0]))
                        _caves[pair[0]] = new Cave();
                    _caves[pair[0]].ConnectedCaves.Add(pair[1]);

                    if (!_caves.ContainsKey(pair[1]))
                        _caves[pair[1]] = new Cave();
                    _caves[pair[1]].ConnectedCaves.Add(pair[0]);
                }
            }

            public int CountPathsThroughCaveSystem()
            {
                const string starCave = "start";
                _pathsCount = 0;
                TraverseCountPaths(starCave, Enumerable.Empty<string>());
                return _pathsCount;
            }

            public int CountPathsThroughCaveSystemWithExtraVisit()
            {
                const string starCave = "start";
                _pathsCount = 0;
                TraverseCountPathsWithExtraVisit(starCave, Enumerable.Empty<string>());
                return _pathsCount;
            }
            
            public List<string> FindAllPaths()
            {
                const string starCave = "start";
                _allPaths = new List<string>();

                TraversePaths(starCave, Enumerable.Empty<string>(), _allPaths);

                return _allPaths;
            }

            public List<string> FindAllPathsWithExtraVisit()
            {
                const string starCave = "start";
                _allPaths = new List<string>();

                TraversePathsWithExtraVisit(starCave, Enumerable.Empty<string>(), _allPaths);

                return _allPaths;
            }

            private void TraverseCountPaths(string currentCave, IEnumerable<string> path)
            {
                var currentPath = new List<string>(path) { currentCave };
                if (currentCave != "end")
                {
                    var cave = _caves[currentCave];
                    foreach (var connectedCave in cave.ConnectedCaves)
                    {
                        if (connectedCave.All(char.IsLower))
                        {
                            if (currentPath.Contains(connectedCave))
                                continue;
                        }

                        TraverseCountPaths(connectedCave, currentPath);
                    }
                }
                else
                {
                    _pathsCount++;
                }
            }

            private void TraverseCountPathsWithExtraVisit(string currentCave, IEnumerable<string> path)
            {
                var currentPath = new List<string>(path) { currentCave };
                if (currentCave != "end")
                {
                    var cave = _caves[currentCave];
                    foreach (var connectedCave in cave.ConnectedCaves)
                    {
                        if (connectedCave.All(char.IsLower))
                        {
                            if (currentPath.Contains(connectedCave))
                            {
                                switch (connectedCave)
                                {
                                    case "start":
                                    case "end":
                                        continue;
                                }

                                if (currentPath.Any(caveName => caveName.All(char.IsLower) && currentPath.Count(caveInPath => caveInPath == caveName) > 1))
                                    continue;
                            }
                        }
                        TraverseCountPathsWithExtraVisit(connectedCave, currentPath);
                    }
                }
                else
                {
                    _pathsCount++;
                }
            }

            private void TraversePaths(string currentCave, IEnumerable<string> path, ICollection<string> allPaths)
            {
                var currentPath = new List<string>(path) { currentCave };
                if (currentCave != "end")
                {
                    var cave = _caves[currentCave];
                    foreach (var connectedCave in cave.ConnectedCaves)
                    {
                        if (connectedCave.Any(char.IsUpper) || !currentPath.Contains(connectedCave))
                            TraversePaths(connectedCave, currentPath, allPaths);
                    }
                }
                else
                {
                    allPaths.Add(string.Join(',', currentPath));
                }
            }

            private void TraversePathsWithExtraVisit(string currentCave, IEnumerable<string> path, ICollection<string> allPaths)
            {
                var currentPath = new List<string>(path) { currentCave };
                if (currentCave != "end")
                {

                    var cave = _caves[currentCave];
                    foreach (var connectedCave in cave.ConnectedCaves)
                    {
                        if (connectedCave.All(char.IsLower) && currentPath.Contains(connectedCave))
                        {
                            switch (connectedCave)
                            {
                                case "start":
                                case "end":
                                    continue;
                            }

                            if (currentPath.Any(caveName => caveName.All(char.IsLower) && currentPath.Count(caveInPath => caveInPath == caveName) > 1))
                                continue;
                        }
                        TraversePathsWithExtraVisit(connectedCave, currentPath, allPaths);
                    }
                }
                else
                {
                    allPaths.Add(string.Join(',', currentPath));
                }
            }


        }

    }
}
