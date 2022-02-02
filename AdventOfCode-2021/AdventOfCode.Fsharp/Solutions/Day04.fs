namespace AdventOfCode.Fsharp.Solutions

open System
open System.Linq
open AdventOfCode.Fsharp.InputData
open System.Collections.Generic

module Day04 = 
    // let input = Day04Data.testInput
    let solvePart1 = fun inputData -> 
        inputData
        |> fun (text : string) -> text.Split(Environment.NewLine)
        |> fun lines -> lines.Select(fun line -> line |> int)
        |> fun values -> values.Aggregate((0, Int32.MaxValue), fun (counter, previousValue) value ->
            if value > previousValue then (counter + 1, value)
            else (counter, value))
        |> fun (counter, lastValue) -> counter

    let solvePart2 = fun inputData -> 
        inputData
        |> fun (text:string) -> text.Split(Environment.NewLine)
        |> fun lines -> lines.Select(fun line -> line |> int)
        0
   
