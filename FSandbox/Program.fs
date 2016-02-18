// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open DynamicProgramming.Coins
open DynamicProgramming.NonDecreasing

module Main =

    [<EntryPoint>]
    let main argv = 
    
        // Test Coins
//        let coins = [|1;3;5|]
//    
//        for i in 1 .. 11 do
//            printfn "%A" (minNumberOfCoins coins i)

        // Test Longest nondecreasing
        let sequence = [|5; 3; 4; 8; 6; 7|]

        printfn "%A" (longestNonDecreasing sequence)

        0 // return an integer exit code
