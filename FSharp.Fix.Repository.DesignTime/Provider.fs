module FSharp.Fix

open System
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices


[<TypeProvider>]
type RepositoryProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces (config)

    let ns = "FSharp.Fix"
    let asm = Assembly.GetExecutingAssembly()

    let createTypes () =
        let myType = ProvidedTypeDefinition(asm, ns, "Repository", Some typeof<obj>)
        let myProp = ProvidedProperty("Versions", typeof<string>, isStatic = true,
                                        getterCode = (fun args -> <@@ "Hello world" @@>))
        myType.AddMember(myProp)
        [myType]

    do
        this.AddNamespace(ns, createTypes())

[<assembly:TypeProviderAssembly>]
do ()


