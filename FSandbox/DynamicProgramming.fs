namespace DynamicProgramming

module Coins =

    let private update (matrix : int[]) (coin : int) (sum : int) =
        Array.set matrix sum (min (matrix.[sum - coin] + 1) (matrix.[sum]))

    let private step (coins : int[]) (matrix : int[]) (sum : int) =
        coins |> Seq.filter (fun x -> x <= sum)
              |> Seq.iter (fun x -> update matrix x sum)

    let minNumberOfCoins (coins : int[]) (sum : int) = 
        let matrix : int array = Array.create (sum + 1) System.Int32.MaxValue
        Array.set matrix 0 0
        for i in 1 .. sum do 
            step coins matrix i
        matrix.[sum]


module NonDecreasing =
    
    let private step (sequence : int[]) (matrix : int[]) (index : int) =
        let filtered = sequence  |> Seq.take index
                                 |> Seq.mapi (fun i el -> i, el)
                                 |> Seq.filter (fun (i,el) -> el <= (sequence.[index]))

        let maxLength = match (Seq.length filtered) with 
                        | 0 -> 0
                        | _ -> filtered
                                 |> Seq.map (fun (i,el) -> matrix.[i])
                                 |> Seq.max

        Array.set matrix index (maxLength + 1)

    let longestNonDecreasing (sequence : int[]) =
        let count = Array.length sequence
        
        let matrix : int array = Array.zeroCreate count
        
        Array.set matrix 0 1
        
        for i in 1 .. (count - 1) do
            step sequence matrix i

        matrix.[count - 1]

        