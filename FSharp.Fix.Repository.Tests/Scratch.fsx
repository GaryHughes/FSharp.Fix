//#r "/Users/geh/.nuget/packages/fsharp.data/3.1.1/lib/netstandard2.0/FSharp.Data.DesignTime.dll"
#r "/Users/geh/.nuget/packages/fsharp.data/3.1.1/lib/netstandard2.0/FSharp.Data.dll"
#r "System.Xml.Linq.dll"
#r "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/2.2.4/netstandard.dll"
#r "../FSharp.Fix.Repository/bin/Debug/netstandard2.0/FSharp.Fix.Repository.DesignTime.dll"

//#r "../FSharp.Fix.Repository/bin/Debug/netstandard2.0/FSharp.Fix.Repository.dll"

open FSharp.Data
open FSharp.Fix

type repo = Repository< @"/Users/geh/Downloads/Repository" >



type enums = FSharp.Data.XmlProvider< @"/Users/geh/Downloads/Repository/FIX.4.0/Base/Enums.xml" >

















