namespace FSharp.Fix

open System
open System.Collections.Generic
open System.IO
open System.Text

[<AutoOpen>]
module IO =

    let FieldDelimiter = '\x01'
    let FieldValueSeparator = '='
    let CheckSumTag = 10

    let readFieldFromStream (stream:Stream) =
    
        let readToken separator =
            seq { 
                while true do
                  yield stream.ReadByte() 
            }
            |> Seq.takeWhile(fun byte -> byte <> -1)
            |> Seq.map char
            |> Seq.takeWhile(fun character -> character <> separator)
            |> Seq.fold(fun accumulator next -> accumulator + (string next)) ""

        {   Tag = System.Int32.Parse(readToken FieldValueSeparator); 
            Value = readToken FieldDelimiter }
     
    let readMessageFromStream (stream:Stream) =
        
        let mutable checkSum = None

        let fields = 
            seq {
                while true do
                    let field = readFieldFromStream stream
                    if field.Tag = CheckSumTag then
                        checkSum <- Some field
                        yield field
                    else
                        yield field 
            } 
            |> Seq.takeWhile(fun field -> field.Tag <> CheckSumTag)
            |> Seq.toList

        match checkSum with
        | Some field -> Ok { Fields = fields @ [field] }
        | None -> Ok { Fields = fields }

    let readMessageFromBytes (data:byte[]) =
        use stream = new MemoryStream(data)
        readMessageFromStream stream

    let readMessageFromString (data:string) =
        let bytes = Encoding.UTF8.GetBytes(data)
        readMessageFromBytes bytes

    let parseMessageFromLogLine (line:string) =
        match line.IndexOf("8=FIX.") with
        | -1 -> Error ("Could not find a FIX message in " + line)
        | index -> 
            let data = line.Substring(index)
            readMessageFromString data

    let parseMessagesFromLog (stream:Stream) =
        seq {
            use reader = new StreamReader(stream)
            while true do
                yield reader.ReadLine()
        }
        |> Seq.takeWhile(String.IsNullOrEmpty >> not)
        |> Seq.map parseMessageFromLogLine


    let prettyPrint (message:Message) = 
        printfn "%A" message