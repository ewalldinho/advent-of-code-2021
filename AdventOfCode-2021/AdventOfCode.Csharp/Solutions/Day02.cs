using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 2: Dive! (https://adventOfCode.com/2021/day/2)
    public class Day02 : IPuzzle
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
        
        private static int SolvePart1(string input)
        {
            var (shipPos, shipDepth) = input.Split(Environment.NewLine)
                .Select(line => line.Trim().Split(' '))
                .Select(row => new { Cmd = row[0], Arg = int.Parse(row[1]) })
                .Aggregate((shipPos: 0, shipDepth: 0), (acc, item) => 
                    item.Cmd switch
                    {
                        "forward" => (acc.shipPos + item.Arg, acc.shipDepth),
                        "up" => (acc.shipPos, acc.shipDepth - item.Arg),
                        "down" => (acc.shipPos, acc.shipDepth + item.Arg),
                        _ => throw new ArgumentException($"Invalid command name {item.Cmd}")
                    });
            
            return shipPos * shipDepth;
        }
        
        private static int SolvePart2(string input)
        {
            var (pos, depth, _) = input.Split(Environment.NewLine)
                .Select(line => line.Trim().Split(' '))
                .Select(row => new { CmdName = row[0], Arg = int.Parse(row[1]) })
                .Aggregate((pos: 0, depth: 0, aim: 0), (ship, command) =>
                    command.CmdName switch
                    {
                        "forward" => (ship.pos + command.Arg, ship.depth + ship.aim * command.Arg, ship.aim),
                        "up" => (ship.pos, ship.depth, ship.aim - command.Arg),
                        "down" => (ship.pos, ship.depth, ship.aim + command.Arg),
                        _ => throw new ArgumentException($"Invalid command name {command.CmdName}")
                    });

            return pos * depth;
        }

        /*
         * alternative solution
         */
        private class Submarine
        {
            public int Position { get; protected set; }
            public int Depth { get; protected set; }

            public Submarine(int position, int depth)
            {
                Position = position;
                Depth = depth;
            }

            public void Forward(int distance)
            {
                Position += distance;
            }

            public void MoveUp(int deltaDepth)
            {
                Depth -= deltaDepth;
            }

            public void MoveDown(int deltaDepth)
            {
                Depth += deltaDepth;
            }

        }

        private class Submarine2 : Submarine
        {
            private int Aim { get; set; }
            public Submarine2(int position, int depth, int aim) : base(position, depth)
            {
                Position = position;
                Depth = depth;
                Aim = aim;
            }

            public new void Forward(int distance)
            {
                Position += distance;
                Depth += Aim * distance;
            }

            public new void MoveUp(int delta)
            {
                Aim -= delta;
            }

            public new void MoveDown(int delta)
            {
                Aim += delta;
            }

        }

        public int SolvePart1v2(string inputData)
        {
            var instructions = GetStructuredData(inputData);
            var sub = new Submarine(0, 0);
            foreach (var (cmd, arg) in instructions)
            {
                switch (cmd)
                {
                    case "forward":
                        sub.Forward(arg);
                        break;
                    case "up":
                        sub.MoveUp(arg);
                        break;
                    case "down":
                        sub.MoveDown(arg);
                        break;
                }
            }
            return sub.Position * sub.Depth;
        }
        
        public int SolvePart2v2(string inputData)
        {
            var instructions = GetStructuredData(inputData);
            var sub = new Submarine2(0, 0, 0);
            foreach (var (cmd, arg) in instructions)
            {
                switch (cmd)
                {
                    case "forward":
                        sub.Forward(arg);
                        break;
                    case "up":
                        sub.MoveUp(arg);
                        break;
                    case "down":
                        sub.MoveDown(arg);
                        break;
                }
            }
            return sub.Position * sub.Depth;
        }

        private static List<(string, int)> GetStructuredData(string inputData)
        {
            return inputData.Split(Environment.NewLine)
                .Select(line => line.Trim().Split(' '))
                .Select(instruction => (instruction[0], int.Parse(instruction[1])))
                .ToList();
        }
    }

}
