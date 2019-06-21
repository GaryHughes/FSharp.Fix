// macOS
//#r "/Users/geh/.nuget/packages/fsharp.data/3.1.1/lib/netstandard2.0/FSharp.Data.dll"
//#r "System.Xml.Linq.dll"
//#r "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/2.2.4/netstandard.dll"
//#r "../FSharp.Fix.Repository/bin/Debug/netstandard2.0/FSharp.Fix.Repository.DesignTime.dll"

// Windows
#r "C:\\Users\\Gary Hughes\\.nuget\\packages\\fsharp.data\\3.1.1\\lib\\netstandard2.0\\FSharp.Data.dll"
#r "System.Xml.Linq.dll"
//#r "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/2.2.4/netstandard.dll"
#r "../FSharp.Fix.Repository/bin/Debug/netstandard2.0/FSharp.Fix.Repository.DesignTime.dll"


open FSharp.Data
open FSharp.Fix

// macOS
//type repo = Repository< @"/Users/geh/Downloads/Repository" >

type repo = Repository< @"C:/Users/Gary Hughes/Downloads/Repository" >

repo.FIX_4_0.OrdType


type enums = FSharp.Data.XmlProvider< @"C:/Users/Gary Hughes/Downloads/Repository/FIX.4.0/Base/Enums.xml" >

















