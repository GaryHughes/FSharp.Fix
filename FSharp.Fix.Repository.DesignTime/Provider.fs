module FSharp.Fix

open System
open System.Collections.Generic
open System.IO
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
//open FSharp.Data
//open System.Xml.Linq

[<Literal>]
let FieldSample = """
<Fields xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.4.4" xsi:noNamespaceSchemaLocation="../../schema/Fields.xsd" generated="2010-03-13T14:54:02-05:00">
	<Field added="FIX.2.7">
		<Tag>1</Tag>
		<Name>Account</Name>
		<Type>String</Type>
		<AbbrName>Acct</AbbrName>
		<NotReqXML>0</NotReqXML>
		<Description>Account mnemonic as agreed between buy and sell sides, e.g. broker and institution or investor/intermediary and fund manager.</Description>
	</Field>
	<Field added="FIX.2.7">
		<Tag>2</Tag>
		<Name>AdvId</Name>
		<Type>String</Type>
		<AbbrName>AdvId</AbbrName>
		<NotReqXML>0</NotReqXML>
		<Description>Unique identifier of advertisement message.
(Prior to FIX 4.1 this field was of type int)</Description>
	</Field>
</Fields>
"""

[<Literal>]
let EnumSample = """ 
<Enums>
  <Enum added="FIX.2.7">
    <Tag>4</Tag>
    <Value>B</Value>
    <SymbolicName>Buy</SymbolicName>
    <Description>Buy</Description>
  </Enum>
  <Enum added="FIX.2.7">
    <Tag>4</Tag>
    <Value>S</Value>
    <SymbolicName>Sell</SymbolicName>
    <Description>Sell</Description>
  </Enum>
 </Enums>
"""

// type Fields = XmlProvider< FieldSample >
// type Enums = XmlProvider< EnumSample >

// let createVersionEnums (versionType:ProvidedTypeDefinition) namespaceName assembly versionPath =
//     let fieldData = Path.Combine(versionPath, "Base", "Fields.xml") |> Fields.Load
//     let enumData = Path.Combine(versionPath, "Base", "Enums.xml") |> Enums.Load
//     let tagValues = new Dictionary<int, List<XElement>>()
//     enumData.Enums 
//     |> Seq.cache
//     |> Seq.groupBy(fun enum -> enum.Tag)
//     |> Seq.iter(fun pair ->
//         let tag = fst pair
//         let values = snd pair
//         match fieldData.Fields |> Seq.tryFind(fun field -> field.Tag = tag) with
//         | None -> eprintf "Could not find field with Tag=%i version: %s\n" tag versionPath 
//         | Some field -> 
//             let enumType = ProvidedTypeDefinition(assembly, namespaceName, field.Name, Some typeof<obj>)
//             versionType.AddMember(enumType)
//     )
//     versionType

[<TypeProvider>]
type RepositoryProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces (config, assemblyReplacementMap=[("FSharp.Fix.DesignTime", "FSharp.Fix")], addDefaultProbingLocation=true)
   
    let namespaceName = "FSharp.Fix"
    let thisAssembly = Assembly.GetExecutingAssembly()

    let repoType = ProvidedTypeDefinition(thisAssembly, namespaceName, "Repository", None, hideObjectMethods=true, nonNullable = true)

    let buildTypes (typeName:string) (args:obj[]) =
        let ty = ProvidedTypeDefinition(thisAssembly, namespaceName, typeName, Some typeof<obj>)
        let path = args.[0] :?> string
        for directory in Directory.EnumerateDirectories(path, "FIX*") do
            let versionName = Path.GetFileName(directory).Replace(".", "_");
            let versionType = ProvidedTypeDefinition(thisAssembly, namespaceName, versionName, Some typeof<obj>)
            //createVersionEnums versionType namespaceName thisAssembly directory |> ignore
            ty.AddMember(versionType)
        ty
    
    let parameters = [ ProvidedStaticParameter("Path", typeof<string>, parameterDefaultValue = "") ]

    let helpText = 
           """<summary>Typed representation of fixtrading.org XML repository.</summary>
              <param name='Path'>Root path.</param>"""

    do repoType.AddXmlDoc helpText
    do repoType.DefineStaticParameters(parameters, buildTypes)
  
    do this.AddNamespace(namespaceName, [ repoType ])

[<assembly:TypeProviderAssembly>]
do ()


