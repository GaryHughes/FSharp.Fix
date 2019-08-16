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

    let readField (stream:Stream) =
    
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
     
    let readMessage (stream:Stream) =
        
        let mutable checkSum = None

        let fields = 
            seq {
                while true do
                    let field = readField stream
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