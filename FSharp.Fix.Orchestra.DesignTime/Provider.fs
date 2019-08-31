module FSharp.Fix

open System.IO
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
open FSharp.Quotations
  
[<TypeProvider>]
type OrchestraProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces (config, assemblyReplacementMap=[("FSharp.Fix.Orchestra.DesignTime", "FSharp.Fix.Orchestra")], addDefaultProbingLocation=true)
   
    let namespaceName = "FSharp.Fix"
    let thisAssembly = Assembly.GetExecutingAssembly()

    let repoType = ProvidedTypeDefinition(thisAssembly, namespaceName, "Orchestra", None, hideObjectMethods=true, nonNullable = true)

   
    
    let parameters = [ ProvidedStaticParameter("Path", typeof<string>, parameterDefaultValue = "") ]

    let helpText = 
           """<summary>Typed representation of fixtrading.org XML repository.</summary>
              <param name='Path'>Root path.</param>"""

    do repoType.AddXmlDoc helpText
    //do repoType.DefineStaticParameters(parameters, buildTypes)
  
    do this.AddNamespace(namespaceName, [ repoType ])

[<assembly:TypeProviderAssembly>]
do ()


