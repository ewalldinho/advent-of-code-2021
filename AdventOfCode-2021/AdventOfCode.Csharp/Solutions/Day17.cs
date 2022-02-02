using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    public class Day17 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var targetArea = ParseInput(inputData);

            return part switch
            {
                Parts.Part1 => $"{SolvePart1(targetArea)}",
                Parts.Part2 => $"{SolvePart2(targetArea)}",
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.")
            };
        }
        
        private static int SolvePart1(TargetArea targetArea)
        {
            var highestPoints = new Dictionary<(int, int), int?>();
            var b2 = 1 + 4 * 2 * targetArea.FromX;
            // we need only positive speed value
            var minHorizontalSpeed = (-1 + (int)Math.Sqrt(b2)) / 2;
            var maxHeight = 0;
            var hitStatus = 0;  // 0 - not hit yet, 1 - has hit, 2 - overshot
            for (var speedX = minHorizontalSpeed; speedX <= targetArea.ToX; speedX++)
            {
                for (var speedY = -100; speedY < 1000; speedY++)
                {
                    var height = SimulateProbeLaunch(targetArea, speedX, speedY);
                    if (height <= 0)
                    {
                        if (hitStatus == 1)
                        {
                            hitStatus++;
                            break;
                        }
                    }
                    else
                    {
                        if (hitStatus == 0)
                            hitStatus++;
                    }
                    
                    if (height > maxHeight)
                    {
                        maxHeight = height;
                    }
                    highestPoints.Add((speedX, speedY), height);
                }
            }

            var hitTargets = highestPoints.Where(kvp => kvp.Value > 0).ToList();
            var hitTarget = highestPoints.Where(kvp => kvp.Value == maxHeight).ToList();
            return maxHeight;
        }

        private static int SolvePart2(TargetArea targetArea)
        {
            var hitPoints = new Dictionary<(int, int), bool>();
            var b2 = 1 + 4 * 2 * targetArea.FromX;
            // we need only positive speed value
            var minHorizontalSpeed = (-1 + (int)Math.Sqrt(b2)) / 2;
            //var maxHeight = 0;
            //var hitStatus = 0;  // 0 - not hit yet, 1 - has hit, 2 - overshot
            for (var speedX = minHorizontalSpeed; speedX <= targetArea.ToX; speedX++)
            {
                for (var speedY = -10000; speedY < 10000; speedY++)
                {
                    var hasHitTarget = SimulateProbeLaunchUntilHit(targetArea, speedX, speedY);

                    hitPoints.Add((speedX, speedY), hasHitTarget);
                }
            }

            var hits = hitPoints.Where(kvp => kvp.Value).ToList();
            return hits.Count;
        }
        
        private class TargetArea
        {
            public int FromX { get; set; }
            public int ToX { get; set; }
            public int FromY { get; set; }
            public int ToY { get; set; }
        }

        private static TargetArea ParseInput(string inputData)
        {
            var targetArea = inputData.Split(": ").Last()
                .Split(", ").Select(coords => coords.Split('='))
                .ToDictionary(coordinate => coordinate[0], coordinate => coordinate[1].Split("..").Select(int.Parse).ToArray());
            return new TargetArea
            {
                FromX = targetArea["x"][0],
                ToX = targetArea["x"][1],
                FromY = targetArea["y"][0],
                ToY = targetArea["y"][1]
            };
        }
        
        private static int SimulateProbeLaunch(TargetArea targetArea, int speedX, int speedY)
        {
            var maxHeight = 0;
            var posX = 0;
            var posY = 0;
            var speedHorizontal = speedX;
            var speedVertical = speedY;
            var hasMissedTarget = false;
            var isWithinTargetArea = false;
            while (!(isWithinTargetArea || hasMissedTarget))
            {
                posX += speedHorizontal;
                posY += speedVertical;
                if (posY > maxHeight)
                    maxHeight = posY;

                if (speedHorizontal > 0)
                    speedHorizontal--;
                speedVertical--;

                isWithinTargetArea = targetArea.FromX <= posX && posX <= targetArea.ToX && targetArea.FromY <= posY && posY <= targetArea.ToY;
                hasMissedTarget = posX > targetArea.ToX || posY < targetArea.FromY;
            }

            if (isWithinTargetArea)
                return (speedY * speedY + speedY) / 2;
            if (hasMissedTarget)
                return 0;
            return -1;
        }

        private static bool SimulateProbeLaunchUntilHit(TargetArea targetArea, int speedX, int speedY)
        {
            var maxHeight = 0;
            var posX = 0;
            var posY = 0;
            var speedHorizontal = speedX;
            var speedVertical = speedY;
            var hasMissedTarget = false;
            var isWithinTargetArea = false;
            while (!(isWithinTargetArea || hasMissedTarget))
            {
                posX += speedHorizontal;
                posY += speedVertical;
                if (posY > maxHeight)
                    maxHeight = posY;

                if (speedHorizontal > 0)
                    speedHorizontal--;
                speedVertical--;

                isWithinTargetArea = targetArea.FromX <= posX && posX <= targetArea.ToX && targetArea.FromY <= posY && posY <= targetArea.ToY;
                hasMissedTarget = posX > targetArea.ToX || posY < targetArea.FromY;
            }

            if (isWithinTargetArea)
                return true;

            return hasMissedTarget && false;
        }


    }
}
