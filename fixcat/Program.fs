open System
open System.IO
open FSharp.Fix

let catStream stream = 
    parseMessagesFromLog stream
    |> Seq.map(fun result -> 
        match result with 
        | Ok message -> Some message
        | Error _ -> None)
    |> Seq.choose id
    |> Seq.map prettyPrint
    |> Seq.iter Console.WriteLine

[<EntryPoint>]
let main argv =

    if argv.Length = 0 then
        use stream = Console.OpenStandardInput()
        catStream stream
    else
        argv
        |> Seq.iter(fun filename ->
            use stream = File.OpenRead(filename)
            catStream stream
        )
    
    0 // return an integer exit code
