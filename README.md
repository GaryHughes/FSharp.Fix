[![Build Status](https://dev.azure.com/garyedwardhughes/FSharp.Fix/_apis/build/status/GaryHughes.FSharp.Fix?branchName=master)](https://dev.azure.com/garyedwardhughes/FSharp.Fix/_build/latest?definitionId=1&branchName=master)

FSharp.Fix
===========

This is/will be an F# implementation of the FIX protocol described at fixtrading.org.

It is a vehicle for me to learn F# in a problem domain I'm very familiar with. Code quality will be variable and probably full of silly things while I learn the language.

# FSharp.Fix.Orchestra

This is a Type Provider for the fixtrading.org Orchestra standard.



# FSharp.Fix.Repository

This is a Type Provider for the fixtrading.org repository standard.

Download one or more repositories from fixtrading.org and unpack them into a common parent so you end up with a structure like the following.

    Repository/FIX.4.0/Base/Components.xml
    Repository/FIX.4.0/Base/DataTypes.xml
    Repository/FIX.4.0/Base/Enums.xml
    Repository/FIX.4.0/Base/Fields.xml
    Repository/FIX.4.0/Base/Messages.xml
    Repository/FIX.4.0/Base/MsgContents.xml
    Repository/FIX.4.4/Base/Components.xml
    Repository/FIX.4.4/Base/DataTypes.xml
    Repository/FIX.4.4/Base/Enums.xml
    Repository/FIX.4.4/Base/Fields.xml
    Repository/FIX.4.4/Base/Messages.xml
    Repository/FIX.4.4/Base/MsgContents.xml

Instantiate the Type Provider as shown in the following script and pass the repository root directory as a type parameter. You will then be able to navigate the available versions and their supported messages and fields.

```fsharp
#r "FSharp.Fix.Repository.Xml.dll"
#r "FSharp.Fix.Repository.DesignTime.dll"

open FSharp.Fix

type repo = Repository< @"~/Downloads/Repository" >

repo.FIX_4_0.OrdStatus.Filled.Tag

repo.FIX_4_0.OrdStatus.PartiallyFilled.Description

repo.FIX_4_0.DataTypes.float.Description
repo.FIX_4_0.DataTypes.date.Description

repo.FIX_4_0.Fields.TimeInForce.Tag 
repo.FIX_4_0.Fields.TimeInForce.Description
repo.FIX_4_0.Fields.TimeInForce.Name
repo.FIX_4_0.Fields.TimeInForce.Type

repo.FIX_4_0.Messages.OrderSingle.Description

// Show all the fields of a message, indent each nested repeating group.
repo.FIX_4_4.Messages.MarketDataSnapshotFullRefresh.Fields 
|> Seq.iter(fun field -> printfn "%*s %s (%i) %b" (field.Indent * 4) " " field.Name field.Tag field.Required)
```

# FSharp.Fix

```fsharp
readFieldFromStream: 
    stream: Stream 
        -> Field

readMessageFromStream: 
    stream: Stream 
        -> Result<Message,'a>

readMessageFromBytes: 
    data: byte [] 
        -> Result<Message,'a>

readMessageFromString: 
    data: string 
        -> Result<Message,'a>

parseMessageFromLogLine: 
    line: string 
        -> Result<Message,string>

parseMessagesFromLog: 
    stream: Stream 
        -> seq<Result<Message,string>>

prettyPrint: 
    message: Message 
        -> string
```

# fixcat

fixcat is modelled on the UNIX cat utility; it will print FIX messages in human readable format with  field names.

It will read from standard input or a list of one or more filenames specified on the command line.

    less fixlog | fixcat
    fixcat fixlog1.txt fixlog2.txt fixlog3.txt

The input is expected to be raw FIX messages, one per line, with an optional prefix which would typically be a timestamp etc as is typical in log files. The parser searches for **8=FIX.** and assumes that is the start of the message which continues until a CheckSum field or end of line is reached.

    8=FIX.4.49=7235=A49=INITIATOR56=ACCEPTOR34=152=20190816-10:34:27.75298=0108=3010=013
    
    2019-08-17 13:00:00.000 8=FIX.4.49=7235=A49=INITIATOR56=ACCEPTOR34=152=20190816-10:34:27.75298=0108=3010=013

Parsing either of these lines with fixcat will product the following output.

      BeginString    (8) - FIX.4.4
       BodyLength    (9) - 72
          MsgType   (35) - A
     SenderCompID   (49) - INITIATOR
     TargetCompID   (56) - ACCEPTOR
        MsgSeqNum   (34) - 1
      SendingTime   (52) - 20190816-10:34:27.752
    EncryptMethod   (98) - 0
       HeartBtInt  (108) - 30
         CheckSum   (10) - 013

