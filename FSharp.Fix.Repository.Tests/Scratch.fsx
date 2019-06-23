#r "../FSharp.Fix.Repository/bin/Debug/netstandard2.0/FSharp.Fix.Repository.DesignTime.dll"

open FSharp.Fix

type repo = Repository< @"/Users/geh/Downloads/Repository" >

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













