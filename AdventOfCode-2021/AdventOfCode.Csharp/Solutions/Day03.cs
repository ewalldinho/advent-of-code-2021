using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Csharp.Solutions
{
    // Day 3: Binary Diagnostic (https://adventOfCode.com/2021/day/3)
    public class Day03 : IPuzzle
    {
        public string CalculateSolution(Parts part, string inputData)
        {
            var data = GetStructuredData(inputData);

            switch (part)
            {
                case Parts.Part1:
                    var gammaRate = CalcGammaRate(data);
                    var epsilonRate = CalcEpsilonRate(data);
                    var gammaRateDec = Convert.ToInt32(gammaRate, 2);
                    var epsilonRateDec = Convert.ToInt32(epsilonRate, 2);
                    var powerConsumption = gammaRateDec * epsilonRateDec;
                    return $"{powerConsumption}";

                case Parts.Part2:
                    var oxygenRating = CalcOxygenRating(data);
                    var co2Rating = CalcCo2Rating(data);
                    var oxygenRatingDec = Convert.ToInt32(oxygenRating, 2);
                    var co2RatingDec = Convert.ToInt32(co2Rating, 2);
                    var lifeSupportRating = oxygenRatingDec * co2RatingDec;
                    return $"{lifeSupportRating}";

                default:
                    throw new ArgumentOutOfRangeException(nameof(part), part, "There are only 2 parts.");
            }
        }
        

        private static string CalcGammaRate(List<string> data)
        {
            if (data.Count == 0) throw new ArgumentException($"{nameof(data)} can not be empty.");

            var binLength = data[0].Length;
            var gammaRate = data.Aggregate(new int[binLength], (acc, binary) =>
            {
                for (var i = 0; i < binLength; i++)
                    acc[i] += binary[i] == '1' ? 1 : 0;
                return acc;
            });

            return string.Join("", gammaRate.Select(bit1Count => bit1Count * 2 > data.Count ? '1' : '0'));
        }

        private static string CalcEpsilonRate(List<string> data)
        {
            if (data.Count == 0) throw new ArgumentException($"{nameof(data)} can not be empty.");

            var binLength = data[0].Length;
            var gammaRate = data.Aggregate(new int[binLength], (acc, binary) =>
            {
                for (var i = 0; i < binLength; i++)
                    acc[i] += binary[i] == '1' ? 1 : 0;
                return acc;
            });

            return string.Join("", gammaRate.Select(bit1Count => bit1Count * 2 < data.Count ? '1' : '0'));
        }

        private static string CalcOxygenRating(List<string> data)
        {
            var oxygenRatingSet = data;
            var bit = 0;
            while (oxygenRatingSet.Count > 1)
            {
                var bit1Count = oxygenRatingSet.Count(value => value[bit] == '1');
                var mostCommonBit = bit1Count * 2 >= oxygenRatingSet.Count ? '1' : '0';
                oxygenRatingSet = oxygenRatingSet.Where(value => value[bit] == mostCommonBit).ToList();
                bit++;
            }

            return oxygenRatingSet.Single();
        }

        private static string CalcCo2Rating(List<string> data)
        {
            var bit = 0;
            var co2RatingSet = data;
            while (co2RatingSet.Count > 1)
            {
                var bit1Count = co2RatingSet.Count(value => value[bit] == '1');
                var leastCommonBit = bit1Count * 2 < co2RatingSet.Count ? '1' : '0';
                co2RatingSet = co2RatingSet.Where(value => value[bit] == leastCommonBit).ToList();
                bit++;
            }

            return co2RatingSet.Single();
        }

        private static List<string> GetStructuredData(string input)
        {
            var binNumbers = input.Split(Environment.NewLine);
            return binNumbers.ToList();
        }
        
    }
}
