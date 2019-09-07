#r "../FSharp.Fix.Repository.Xml/bin/Debug/netstandard2.0/FSharp.Fix.Repository.Xml.dll"
#r "../FSharp.Fix.Repository/bin/Debug/netstandard2.0/FSharp.Fix.Repository.DesignTime.dll"
#r "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/2.2.4/netstandard.dll"

open FSharp.Fix

type repo = Repository< @"/Users/geh/Downloads/Repository" >

repo.FIX_4_0.Messages.ExecutionReport.Fields.[4].Name


repo.FIX_4_4.Messages.MarketDataSnapshotFullRefresh.Fields 
|> Seq.iter(fun field -> printfn "%*s %s (%i) %b" (field.Indent * 4) " " field.Name field.Tag field.Required)

repo.FIX_4_0.Messages.ExecutionReport.Fields.[0]

repo.FIX_4_0.Messages.ExecutionReport.MsgType

repo.FIX_4_4.OrdStatus.Suspended.Description

repo.FIX_4_0.OrdStatus.Filled.Tag

repo.FIX_4_0.OrdStatus.PartiallyFilled.Description

repo.FIX_4_0.DataTypes.float.Description
repo.FIX_4_0.DataTypes.date.Description

repo.FIX_4_0.Fields.TimeInForce.Tag 
repo.FIX_4_0.Fields.TimeInForce.Description
repo.FIX_4_0.Fields.TimeInForce.Name
repo.FIX_4_0.Fields.TimeInForce.Type

repo.FIX_4_0.Messages.OrderSingle.Description

repo.FIX_4_0.Messages.TestRequest.Fields.DeliverToCompID.Description

repo.FIX_4_0.nameOfFieldWithTag 100












