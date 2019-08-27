open System
open FSharp.Fix

[<EntryPoint>]
let main argv =

    use stream = Console.OpenStandardInput()
   
    parseMessagesFromLog stream
    |> Seq.map(fun result -> 
        match result with 
        | Ok message -> Some message
        | Error _ -> None)
    |> Seq.choose id
    |> Seq.map prettyPrint
    |> Seq.iter Console.WriteLine
    
    0 // return an integer exit code
