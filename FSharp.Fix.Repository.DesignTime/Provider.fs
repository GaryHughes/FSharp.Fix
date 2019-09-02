module FSharp.Fix

open System.IO
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
open FSharp.Quotations
open Fix.Repository.Xml

open Repository.Enum
open Repository.DataType
open Repository.Field
open Repository.Message
  
[<TypeProvider>]
type RepositoryProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces (config, assemblyReplacementMap=[("FSharp.Fix.Repository.DesignTime", "FSharp.Fix.Repository")], addDefaultProbingLocation=true)
   
    let namespaceName = "FSharp.Fix"
    let thisAssembly = Assembly.GetExecutingAssembly()

    let repoType = ProvidedTypeDefinition(thisAssembly, namespaceName, "Repository", None, hideObjectMethods=true, nonNullable = true)

    let buildTypes (typeName:string) (args:obj[]) =
        let ty = ProvidedTypeDefinition(thisAssembly, namespaceName, typeName, Some typeof<obj>)
        let path = args.[0] :?> string
        for directory in Directory.EnumerateDirectories(path, "FIX*") do
            let versionName = Path.GetFileName(directory).Replace(".", "_");
            let versionType = ProvidedTypeDefinition(thisAssembly, namespaceName, versionName, Some typeof<obj>)
            let version = loadVersion directory
            createEnums namespaceName thisAssembly version |> Seq.iter versionType.AddMember
            createDataTypes namespaceName thisAssembly version |> Seq.iter versionType.AddMember
            createFields namespaceName thisAssembly version |> Seq.iter versionType.AddMember
            createMessages namespaceName thisAssembly version |> Seq.iter versionType.AddMember

            // This is hack - ideally we need a couple of things, I can do 1 or 2 but not both on the same type.
            // 1. The ability to treat Fields like a seq
            // 2. The ability to have Fields properties for each Field. Fields.MsgType etc.
            // Will probably add helper methods to avoid linear search but the above is useful as well.
            
            // Transform the field list into an array so we can do constant time lookups using the tag as an index.
            // There is no 0 tag and there are gaps in the sequence so insert blanks.
            let fieldNames = 
                version.Fields 
                |> Seq.sortBy(fun field -> field.Tag) 
                |> Seq.fold(fun (lastTag, names) field -> 
                    let gapFillers = if field.Tag - lastTag > 1 then [ for _ in lastTag .. field.Tag - 2 -> "" ] else []
                    (field.Tag, names @ gapFillers @ [field.Name])) 
                    (-1, []) 
                |> snd
                |> Seq.toArray

            ProvidedMethod(
                methodName = "nameOfFieldWithTag",
                parameters = [ProvidedParameter(parameterName = "tag", parameterType = typeof<int>)],
                returnType = typeof<string>,
                invokeCode = (fun args -> 
                    <@@ 
                        let tag = (unbox<int> (%%(args.[0]) : int)) 
                        if tag >= fieldNames.Length then "" else fieldNames.[ tag ] 
                    @@>),
                isStatic = true)
            |> versionType.AddMember

            // Provide a method to map a MsgType to a name.
            // let messageNames = 
            //     version.Messages
            //     |> Seq.map(fun message -> (message.MsgType, message.Name))
            //     |> Seq.toArray

            // let nameOfMessage desiredMsgType =
            //     match messageNames |> Seq.tryFind(fun (msgType, name) -> msgType = desiredMsgType) with
            //     | Some (msgType, name) -> name
            //     | None -> "Unknown"
        
            // ProvidedMethod(
            //     methodName = "nameOfMessageWithMsgType",
            //     parameters = [ProvidedParameter(parameterName = "msgType", parameterType = typeof<string>)],
            //     returnType = typeof<string>,
            //     invokeCode = (fun args ->
            //     <@@
            //         let desiredMsgType = (unbox<string> (%%(args.[0]) : string))
            //         nameOfMessage desiredMsgType
            //     @@>),
            //     isStatic = true    
            // ) |> versionType.AddMember

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


