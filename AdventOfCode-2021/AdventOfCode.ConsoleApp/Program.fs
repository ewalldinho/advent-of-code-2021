open AdventOfCode.Fsharp.Solutions
open AdventOfCode.Fsharp.InputData
open System


[<EntryPoint>]
let main args =
    let mutable isRunning = true;
    while isRunning do
        printfn "Press any key"
        let key = Console.ReadKey()
        printfn $"{key.KeyChar}"
        isRunning <- if key.Key = ConsoleKey.Escape then false else true

    
    // Day 1
    let inputDataDay1 = Day01Data.testInput
    let answerDay01P1 = Day01.solvePart1 inputDataDay1
    let answerDay01P2 = Day01.solvePart2 inputDataDay1
    printfn $"Day 1, part 1: {answerDay01P1}"
    printfn $"Day 1, part 2: {answerDay01P2}"
    printfn ""

    // Day 2
    let inputDataDay2 = Day02Data.testInput
    let answerDay02P1 = Day02.solvePart1 inputDataDay2
    let answerDay02P2 = Day02.solvePart2 inputDataDay2
    printfn $"Day 2, part 1: {answerDay02P1}"
    printfn $"Day 2, part 2: {answerDay02P2}"
    printfn ""

    // Day 3
    let inputDataDay3 = Day03Data.testInput
    let answerDay03P1 = Day03.solvePart1 inputDataDay3
    let answerDay03P2 = Day03.solvePart2 inputDataDay3
    printfn $"Day 3, part 1: {answerDay03P1}"
    printfn $"Day 3, part 2: {answerDay03P2}"
    printfn ""

    // Return 0. This indicates success
    0


//let showMenu i = 
//    fun () -> printfn $"Menu {i}"

//let isRunning = true

//while isRunning do
//    showMenu 1
//    let keyInfo = Console.ReadKey
//    if keyInfo.ToString() == "a" then isRunning = false
