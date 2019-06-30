FSharp.Fix
===========

This is/will be an F# implementation of the FIX protocol described at fixtrading.org.

It is a vehicle for me to learn F# in a problem domain I'm very familiar with. Code quality will be variable and probably full of silly things while I learn the language.

# FSharp.Fix.Repository

This is a Type Provider for the fixtrading.org XML repository.

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
#r "bin/Debug/netstandard2.0/FSharp.Fix.Repository.DesignTime.dll"

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


