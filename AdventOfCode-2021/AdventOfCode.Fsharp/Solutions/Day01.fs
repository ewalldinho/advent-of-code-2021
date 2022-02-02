namespace AdventOfCode.Fsharp.Solutions

open System
open System.Linq
open AdventOfCode.Fsharp.InputData
open System.Collections.Generic

module Day01 = 
    // let testInput = Day01Data.testInput
    let solvePart1 = fun inputData -> 
        inputData
        |> fun (text : string) -> text.Split(Environment.NewLine)
        |> fun lines -> lines.Select(fun line -> line |> int)
        |> fun values -> values.Aggregate((0, Int32.MaxValue), fun (counter, previousValue) value ->
            if value > previousValue then (counter + 1, value)
            else (counter, value))
        |> fun (counter, lastValue) -> counter


    let count3MeasureSlidingWindows (measurements : IEnumerable<int>) : int = 
        let measures = List.ofSeq(measurements)
        let mutable count = 0
        let mutable lastSum = Int32.MaxValue
        for i = 2 to measures.Count()-1 do
            let newSum = measures.Item(i) + measures.Item(i-1) + measures.Item(i-2)
            count <- if lastSum < newSum then count + 1
                     else count
            lastSum <- newSum
        count
    
    let solvePart2 = fun inputData -> 
        inputData
        |> fun (text:string) -> text.Split(Environment.NewLine)
        |> fun lines -> lines.Select(fun line -> line |> int)
        |> fun values -> count3MeasureSlidingWindows values
        |> fun (counter) -> counter
   
