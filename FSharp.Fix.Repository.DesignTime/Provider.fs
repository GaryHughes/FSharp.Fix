module FSharp.Fix

open System.IO
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
open Repository.Enum
  

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
            createEnums versionType namespaceName thisAssembly directory |> ty.AddMember
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


