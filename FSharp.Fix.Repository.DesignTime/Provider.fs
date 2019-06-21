module FSharp.Fix

open System
open System.Collections.Generic
open System.IO
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
open System.Xml.Linq


let createVersionEnums (versionType:ProvidedTypeDefinition) namespaceName assembly versionPath =
    let fieldData = XDocument.Load(Path.Combine(versionPath, "Base", "Fields.xml"))
    let enumData = XDocument.Load(Path.Combine(versionPath, "Base", "Enums.xml"))
    //let tagValues = new Dictionary<string, List<XElement>>()
    enumData.Root.Descendants(XName.Get("Enum")) 
    |> Seq.map(fun enum ->
        let tag = enum.Element(XName.Get("Tag")).Value
        let value = enum.Element(XName.Get("Value")).Value
        (tag, value))
    |> Seq.groupBy(fun (tag, value) -> tag)
    |> Seq.iter(fun (tag, values) ->
        let field = query {
            for field in fieldData.Root.Descendants(XName.Get("Field")) do
            let fieldTag = field.Element(XName.Get("Tag")).Value
            let fieldName = field.Element(XName.Get("Name")).Value
            where (fieldTag = tag)
            select (Some (fieldTag, fieldName, values))
            headOrDefault
        }
        match field with
        | None -> eprintf "Could not find field with Tag=%s version: %s\n" tag versionPath 
        | Some (_, fieldName, _) ->
            let enumType = ProvidedTypeDefinition(assembly, namespaceName, fieldName, Some typeof<obj>)
            versionType.AddMember(enumType)
        )
    versionType
  

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
            createVersionEnums versionType namespaceName thisAssembly directory |> ignore
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


