using System;
using System.Linq;
using System.Text;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 13: Transparent Origami (https://adventofcode.com/2021/day/13)
    public class Day13 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var manual = ThermalCameraManual.Parse(inputData);

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(manual)}",
                Parts.Part2 => SolvePart2(manual),
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static int SolvePart1(ThermalCameraManual cameraManual)
        {
            cameraManual.Fold(1);
            return cameraManual.GetDotsCount();
        }

        private static string SolvePart2(ThermalCameraManual cameraManual)
        {
            cameraManual.Fold();
            return cameraManual.ToString();
        }
        

        internal class FoldInstruction
        {
            public string Axis { get; init; }
            public int Lines { get; init; }

            public FoldInstruction(string axis, int lines)
            {
                Axis = axis;
                Lines = lines;
            }
        }

        internal class ThermalCameraManual
        {
            private readonly bool[,] _manualSheet;
            private readonly FoldInstruction[] _foldingInstructions;

            private ThermalCameraManual(bool[,] sheet, FoldInstruction[] instructions)
            {
                _manualSheet = sheet;
                _foldingInstructions = instructions;
            }

            public static ThermalCameraManual Parse(string data)
            {
                var parts = data.Split(Environment.NewLine + Environment.NewLine);
                (int x, int y)[] dotCoords = parts[0].Split(Environment.NewLine)
                    .Select(line => line.Split(',').Select(int.Parse).ToArray())
                    .Select(xy => (xy[0], xy[1]))
                    .ToArray();
                var foldingInstructions = parts[1].Split(Environment.NewLine)
                    .Select(fi => fi.Replace("fold along ", string.Empty).Split('='))
                    .Select(fi => new FoldInstruction(fi[0], int.Parse(fi[1])))
                    .ToArray();

                var columns = dotCoords.Max(dot => dot.x) + 1;
                var rows = dotCoords.Max(dot => dot.y) + 1;
                var sheet = new bool[rows, columns];
                foreach (var (x, y) in dotCoords)
                {
                    sheet[y, x] = true;
                }
                return new ThermalCameraManual(sheet, foldingInstructions);
            }

            public void Fold(int? limit = null)
            {
                var instructions = limit.HasValue ? this._foldingInstructions.Take(limit.Value) : this._foldingInstructions;
                foreach (var fi in instructions)
                {
                    switch (fi.Axis)
                    {
                        case "x":
                        {
                            for (var deltaCol = 1; deltaCol <= fi.Lines; deltaCol++)
                            {
                                for (var row = 0; row < _manualSheet.GetLength(0); row++)
                                {
                                    if (fi.Lines + deltaCol < _manualSheet.GetLength(1))
                                    {
                                        _manualSheet[row, fi.Lines - deltaCol] |= _manualSheet[row, fi.Lines + deltaCol];
                                        _manualSheet[row, fi.Lines + deltaCol] = false;
                                    }
                                }
                            }
                            break;
                        }
                        case "y":
                        {
                            for (var deltaRow = 1; deltaRow <= fi.Lines; deltaRow++)
                            {
                                for (var col = 0; col < _manualSheet.GetLength(1); col++)
                                {
                                    if (fi.Lines + deltaRow < _manualSheet.GetLength(0))
                                    {
                                        _manualSheet[fi.Lines - deltaRow, col] |= _manualSheet[fi.Lines + deltaRow, col];
                                        _manualSheet[fi.Lines + deltaRow, col] = false;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }

            public int GetDotsCount()
            {
                var count = 0;
                for (var row = 0; row < _manualSheet.GetLength(0); row++)
                    for (var col = 0; col < _manualSheet.GetLength(1); col++)
                        if (_manualSheet[row, col])
                            count++;
                return count;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var row = 0; row < _foldingInstructions.GetLength(0); row++)
                {
                    var line = new StringBuilder();
                    for (var col = 0; col < _manualSheet.GetLength(1); col++)
                    {
                        line.Append(_manualSheet[row, col] ? '*' : ' ');
                    }

                    sb.AppendLine(line.ToString().TrimEnd());
                }

                return sb.ToString().Trim();
            }
        }
    }
}
