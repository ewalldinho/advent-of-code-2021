using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 5: Hydrothermal Venture (https://adventOfCode.com/2021/day/5)
    public class Day05 : IPuzzle
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

        private int SolvePart1(string inputData)
        {
            var lines = ParseData(inputData);
            var matrix = new int[1000, 1000];
            foreach (var line in lines)
            {
                if (line.P1.X != line.P2.X && line.P1.Y != line.P2.Y)
                    continue;

                var (fromX, toX) = line.P1.X <= line.P2.X ? (line.P1.X, line.P2.X) : (line.P2.X, line.P1.X);
                var (fromY, toY) = line.P1.Y <= line.P2.Y ? (line.P1.Y, line.P2.Y) : (line.P2.Y, line.P1.Y);

                for (var row = fromY; row <= toY; row++)
                {
                    for (var col = fromX; col <= toX; col++)
                    {
                        matrix[row, col]++;
                    }
                }
            }

            var overlapCount = 0;
            for (var r = 0; r < matrix.GetLength(0); r++)
            {
                for (var c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] > 1)
                        overlapCount++;
                }
            }

            return overlapCount;
        }

        private int SolvePart2(string inputData)
        {
            var lines = ParseData(inputData);
            var matrix = new int[1000, 1000];
            foreach (var line in lines)
            {
                var isDiagonal = line.P1.X != line.P2.X && line.P1.Y != line.P2.Y;

                if (isDiagonal)
                {
                    var deltaX = line.P1.X <= line.P2.X ? 1 : -1;
                    var deltaY = line.P1.Y <= line.P2.Y ? 1 : -1;
                    var diagonalRow = line.P1.Y;
                    var diagonalCol = line.P1.X;
                    var count = Math.Abs(line.P2.X - line.P1.X);
                    while (count >= 0)
                    {
                        matrix[diagonalRow, diagonalCol]++;
                        diagonalRow += deltaY;
                        diagonalCol += deltaX;
                        count--;
                    }
                }
                else
                {
                    var (fromX, toX) = line.P1.X <= line.P2.X ? (line.P1.X, line.P2.X) : (line.P2.X, line.P1.X);
                    var (fromY, toY) = line.P1.Y <= line.P2.Y ? (line.P1.Y, line.P2.Y) : (line.P2.Y, line.P1.Y);

                    for (var row = fromY; row <= toY; row++)
                    {
                        for (var col = fromX; col <= toX; col++)
                        {
                            matrix[row, col]++;
                        }
                    }
                }
            }

            var overlapCount = 0;
            for (var r = 0; r < matrix.GetLength(0); r++)
            {
                for (var c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] > 1)
                        overlapCount++;
                }
            }

            return overlapCount;
        }


        private List<Line> ParseData(string inputData)
        {
            var lines = inputData.Split(Environment.NewLine)
                .Select(line => line.Split(" -> "))
                .Select(coords => new[] { Point.Parse(coords[0]), Point.Parse(coords[1]) })
                .Select(points => new Line(points[0], points[1]));
            return lines.ToList();
        }

        private class Point
        {
            public int X { get; init; }
            public int Y { get; init; }
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static Point Parse(string xyData)
            {
                var xy = xyData.Split(',').Select(int.Parse).ToArray();
                return new Point(xy[0], xy[1]);
            }
        }

        private class Line
        {
            public Point P1 { get; init; }
            public Point P2 { get; init; }

            public Line(Point p1, Point p2)
            {
                P1 = p1;
                P2 = p2;
            }
        }
    }
}
