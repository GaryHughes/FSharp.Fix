#r "../FSharp.Fix.Repository/bin/Debug/netstandard2.0/FSharp.Fix.Repository.DesignTime.dll"

open FSharp.Fix

type repo = Repository< @"/Users/geh/Downloads/Repository" >

let tag = repo.FIX_4_0.OrdStatus.Filled.Tag

repo.FIX_4_0.OrdStatus.PartiallyFilled.Description

repo.FIX_4_0.DataTypes.float.Description
 
repo.FIX_4_0.Fields.TimeInForce.Description

repo.FIX_4_0.Messages.OrderSingle.Description

repo.FIX_4_0.Messages.TestRequest.Fields.













