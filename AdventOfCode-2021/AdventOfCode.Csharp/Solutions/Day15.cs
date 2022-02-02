using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    public class Day15 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            return part switch
            {
                Parts.Part1 => $"{SolvePart1(inputData)}",
                Parts.Part2 => $"{SolvePart2(inputData)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static int SolvePart1(string inputData)
        {
            var scan = ChitonDensityScan.Parse(inputData);
            var lowestRisk = scan.FindLowestRiskPath();
            return lowestRisk;
        }

        private static int SolvePart2(string inputData)
        {
            
            return 0;
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
        
        private class ChitonDensityScan
        {
            private static readonly PointOffset[] NeighborsOffsets = new PointOffset[]
            {
                new(0, 1), new(1, 0), 
                new(0, -1), new(-1, 0)
            };

            private int _currentLowestRisk;
            private readonly int _height;
            private readonly int _width;
            private readonly int[][] _riskLevelMap;
            private readonly bool[,] _checkMarkMap;
            private readonly int[,] _weightMap;

            private ChitonDensityScan(int[][] map)
            {
                _height = map.Length;
                _width = map.Length > 0 ? map[0].Length : 0;
                _riskLevelMap = map;
                _checkMarkMap = new bool[_height, _width];
                _weightMap = new int[_height, _width];
            }

            public static ChitonDensityScan Parse(string scanData)
            {
                var data = scanData.Split(Environment.NewLine)
                    .Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
                
                return new ChitonDensityScan(data);
            }

            public int FindLowestRiskPath()
            {
                _currentLowestRisk = int.MaxValue;

                FindLowestRiskPath(0, 0, 0);

                return _currentLowestRisk;
            }

            public void CalculateWeights()
            {
                for (var row = 0; row < _height; row++)
                {
                    for (var col = 0; col < _width; col++)
                    {
                        // set every weight to max
                        _weightMap[row, col] = int.MaxValue;
                    }
                }

                _weightMap[0, 0] = 0;

                for (var row = 0; row < _height; row++)
                {
                    for (var col = 0; col < _width; col++)
                    {
                        var neighborsRisks = new List<int>();
                        var currentPositionRisk = _weightMap[row, col];
                        // evaluate distance weight
                        foreach (var offset in NeighborsOffsets)
                        {
                             neighborsRisks.Add(EvaluateWeight(row + offset.Vertical, col + offset.Horizontal, currentPositionRisk));

                        }
                        _weightMap[row, col] = neighborsRisks.Min();
                    }
                }


            }

            private int EvaluateWeight(int row, int col, int currentPositionRisk)
            {
                if (_weightMap[row, col] == int.MaxValue)
                    return int.MaxValue;

                return _weightMap[row, col] + currentPositionRisk;
            }

            private void FindLowestRiskPath(int row, int col, int totalRisk)
            {
                if (row < 0 || row >= _height || col < 0 || col >= _width)
                    return;

                Debug.WriteLine($"{row},{col} = {_riskLevelMap[row][col]}, risk={totalRisk}");
                
                if (row > 0 || col > 0)
                    totalRisk += _riskLevelMap[row][col];

                if (totalRisk >= _currentLowestRisk)
                    return;

                if (row == _height - 1 && col == _width - 1)
                {
                    if (totalRisk < _currentLowestRisk)
                        _currentLowestRisk = totalRisk;
                    return;
                }

                if (_checkMarkMap[row, col])
                    return;

                _checkMarkMap[row, col] = true;

                foreach (var offset in NeighborsOffsets)
                {
                    FindLowestRiskPath(row + offset.Vertical, col + offset.Horizontal, totalRisk);
                }

                _checkMarkMap[row, col] = false;
            }
        }
    }
}
