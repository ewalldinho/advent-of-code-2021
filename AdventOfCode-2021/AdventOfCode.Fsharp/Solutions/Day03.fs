module AdventOfCode.Fsharp.Solutions.Day03

open System
open System.Linq
open System.Collections.Generic


let convertBinaryToInt (binaryBits : IEnumerable<char>) : int =
    binaryBits
    |> fun bits -> String.Join("", bits)
    |> fun binaryStr -> Convert.ToInt32(binaryStr, 2)

let calculateBit1Rate (data : string[]) (binLength : int) = 
    let bitRate = data.Aggregate(Array.create binLength 0, fun (acc : int[]) (binary : string) ->
        for i = 0 to binLength-1 do
            acc[i] <- acc[i] + if binary[i] = '1' then 1 else 0
        acc)
    bitRate
    

let calculateGammaRate (binaries : string[]) (binLength : int) = 
    binaries
    |> fun binaries -> calculateBit1Rate binaries binLength
    |> fun bit1Rate -> bit1Rate.Select(fun bit1Count -> if bit1Count * 2 > binaries.Length then '1' else '0')
    |> fun gammaRate -> convertBinaryToInt gammaRate

let calculateEpsilonRate (binaries : string[]) (binLength : int) = 
    binaries
    |> fun binaries -> calculateBit1Rate binaries binLength
    |> fun bit1Rate -> bit1Rate.Select(fun bit1Count -> if bit1Count * 2 < binaries.Length then '1' else '0')
    |> fun epsilonRate -> convertBinaryToInt epsilonRate



let solvePart1 (inputData : string) : int =
    let binaries = 
        inputData
        |> fun inputData -> inputData.Split(Environment.NewLine)
    
    if binaries.Length = 0 then raise (ArgumentException($"{nameof(binaries)} can not be empty."))
    let binLength = binaries[0].Length

    let gammaRate = calculateGammaRate binaries binLength
    let epsilonRate = calculateEpsilonRate binaries binLength

    gammaRate * epsilonRate

 

let calculateOxygenRating (binaries : string[]) (binLength : int) = 
    let mutable oxygenRatingSet = binaries;
    let mutable bit = 0
    while (oxygenRatingSet.Length > 1) && (bit < binLength) do
        let bit1Count = oxygenRatingSet.Count(fun value -> value[bit] = '1')
        let mostCommonBit = if bit1Count * 2 >= oxygenRatingSet.Length then '1' else '0'
        oxygenRatingSet <- oxygenRatingSet.Where(fun value -> value[bit] = mostCommonBit).ToArray();
        bit <- bit + 1
    Convert.ToInt32(oxygenRatingSet.Single(), 2)
    
let calculateCo2Rating (binaries : string[]) (binLength : int) =
    let mutable co2RatingSet = binaries;
    let mutable bit = 0
    while (co2RatingSet.Length > 1) && (bit < binLength) do
        let bit1Count = co2RatingSet.Count(fun value -> value[bit] = '1')
        let leastCommonBit = if bit1Count * 2 < co2RatingSet.Length then '1' else '0'
        co2RatingSet <- co2RatingSet.Where(fun value -> value[bit] = leastCommonBit).ToArray();
        bit <- bit + 1

    Convert.ToInt32(co2RatingSet.Single(), 2)

let solvePart2 (inputData : string) : int =
    let binaries = 
        inputData
        |> fun inputData -> inputData.Split(Environment.NewLine)
    
    if binaries.Length = 0 then raise (ArgumentException($"{nameof(binaries)} can not be empty."))
    let binLength = binaries[0].Length

    let oxygenRating = calculateOxygenRating binaries binLength
    let co2Rating = calculateCo2Rating binaries binLength

    oxygenRating * co2Rating


    //private static string CalcOxygenRating(List<string> data)
    //{
    //    var oxygenRatingSet = data;
    //    var bit = 0;
    //    while (oxygenRatingSet.Count > 1)
    //    {
    //        var bit1Count = oxygenRatingSet.Count(value => value[bit] == '1');
    //        var mostCommonBit = bit1Count * 2 >= oxygenRatingSet.Count ? '1' : '0';
    //        oxygenRatingSet = oxygenRatingSet.Where(value => value[bit] == mostCommonBit).ToList();
    //        bit++;
    //    }

    //    return oxygenRatingSet.Single();
    //}

    //private static string CalcCo2Rating(List<string> data)
    //{
    //    var bit = 0;
    //    var co2RatingSet = data;
    //    while (co2RatingSet.Count > 1)
    //    {
    //        var bit1Count = co2RatingSet.Count(value => value[bit] == '1');
    //        var leastCommonBit = bit1Count * 2 < co2RatingSet.Count ? '1' : '0';
    //        co2RatingSet = co2RatingSet.Where(value => value[bit] == leastCommonBit).ToList();
    //        bit++;
    //    }

    //    return co2RatingSet.Single();
    //}