namespace AdventOfCode.Fsharp.Solutions

open System
open System.Linq
open AdventOfCode.Fsharp.InputData

module Day02 = 
    //let input = Day02Data.testInput
    let solvePart1 = fun (inputData : string) -> 
        inputData
        |> fun (instructions : string) -> instructions.Split(Environment.NewLine)
        |> fun lines -> lines.Select(fun line -> line.Trim().Split(' '))
        |> fun commands -> commands.Select(fun line -> {| Cmd = line[0]; Arg = line[1] |> int |})
        |> fun commands -> commands.Aggregate((0, 0), fun (pos, dep) cmd -> 
            match cmd.Cmd with
            | "forward" -> (pos + cmd.Arg, dep)
            | "up" -> (pos, dep - cmd.Arg)
            | "down" -> (pos, dep + cmd.Arg)
            | _ -> raise (ArgumentException($"Invalid command name {cmd.Cmd}")))
        |> fun (position, depth) -> $"{position * depth}"

    let solvePart2 = fun (inputData : string) ->
        inputData
        |> fun (instructions : string) -> instructions.Split(Environment.NewLine)
        |> fun lines -> lines.Select(fun line -> line.Trim().Split(' '))
        |> fun commands -> commands.Select(fun line -> {| Cmd = line[0]; Arg = line[1] |> int |})
        |> fun commands -> commands.Aggregate((0, 0, 0), fun (pos, dep, aim) cmd -> 
            match cmd.Cmd with
            | "forward" -> (pos + cmd.Arg, dep + aim * cmd.Arg, aim)
            | "up" -> (pos, dep, aim - cmd.Arg)
            | "down" -> (pos, dep, aim + cmd.Arg)
            | _ -> raise (ArgumentException($"Invalid command name {cmd.Cmd}")))
        |> fun (position, depth, _) -> $"{position * depth}"

