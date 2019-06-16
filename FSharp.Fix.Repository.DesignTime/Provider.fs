module FSharp.Fix

open System
open System.IO
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices


[<TypeProvider>]
type RepositoryProvider (config : TypeProviderConfig) as _this =
    inherit TypeProviderForNamespaces (config)

    let namespaceName = "FSharp.Fix"
    let thisAssembly = Assembly.GetExecutingAssembly()

    let staticParams = [ProvidedStaticParameter("path", typeof<string>)]

    let repoType = ProvidedTypeDefinition(thisAssembly, namespaceName, "Repository", Some typeof<obj>, hideObjectMethods = true)

    do repoType.DefineStaticParameters(
        parameters = staticParams,
        instantiationFunction = (fun typeName paramValues ->
            match paramValues with
            | [| :? string as path |] ->
                
                let ty = ProvidedTypeDefinition(
                            thisAssembly,
                            namespaceName,
                            typeName,
                            Some typeof<obj>
                        )

                for directory in Directory.EnumerateDirectories(path, "FIX*") do
                    let versionName = Path.GetFileName(directory).Replace(".", "_");
                    let versionType = ProvidedTypeDefinition(
                                          thisAssembly,
                                          namespaceName,
                                          versionName,
                                          Some typeof<obj>
                                      )
                    ty.AddMember(versionType)
            
                ty
            | _ -> failwith "No idea what you're doing"
        )
    )

[<assembly:TypeProviderAssembly>]
do ()


