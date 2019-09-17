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

    // TODO - refactor this to make the message finder a parameter
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

   
    let prettyPrint (nameOfFieldWithTag:int->string) (message:Message) = 
        
        let builder = StringBuilder()

        // let messageName = 
        //      match message.Fields |> Seq.tryFind(fun field -> field.Tag = Repo.FIX_4_4.Fields.MsgType.Tag) with
        //      | Some msgType -> Repo.FIX_4_4.nameOfMessageWithMsgType msgType.Value
        //      | None -> "None"

        // builder.AppendFormat("{0}\n{\n", messageName) |> ignore

        let fields = message.Fields |> Seq.map(fun field -> (field, (nameOfFieldWithTag field.Tag)))
        let widestName = fields |> Seq.map(fun (field, name) -> name.Length) |> Seq.max
        fields
        |> Seq.iter(fun (field, name) -> 
                builder.AppendFormat("{0} {1} - {2}\n", 
                                     name.PadLeft(widestName), 
                                     String.Format("({0})", field.Tag).PadLeft(6), 
                                     field.Value) |> ignore
        )

        builder.Append("}\n") |> ignore

        builder.ToString()