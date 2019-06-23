module Repository.Enum

open System.Collections.Generic
open ProviderImplementation.ProvidedTypes
open Repository.Xml.Version

let createEnums namespaceName assembly version =
    
    let types = new List<ProvidedTypeDefinition>()

    version.Enums    
    |> Seq.groupBy(fun enum -> enum.Tag)
    |> Seq.iter(fun (tag, values) ->
        let field = query {
            for field in version.Fields do
            where (field.Tag = tag)
            select (Some (field, values))
            headOrDefault
        }
        match field with
        | None -> eprintf "Could not find field with Tag=%i version: %s\n" tag version.Path 
        | Some (field, values) ->
            let enumType = ProvidedTypeDefinition(assembly, namespaceName, field.Name, Some typeof<obj>)

            values
            |> Seq.iter(fun value ->
                let valueType = ProvidedTypeDefinition(assembly, namespaceName, value.SymbolicName, Some typeof<obj>)

                let tagValue = value.Tag
                let symbolicNameValue = value.SymbolicName
                let descriptionValue = value.Description
                let addedValue = value.Added

                // nameof operator please https://github.com/fsharp/fslang-design/blob/master/RFCs/FS-1003-nameof-operator.md
                let tag = ProvidedProperty(
                            propertyName = "Tag",
                            propertyType = typeof<int>,
                            isStatic = true,
                            getterCode = (fun args -> <@@ tagValue @@>))

                let symbolicName = ProvidedProperty(
                                    propertyName = "SymbolicName",
                                    propertyType = typeof<string>,
                                    isStatic = true,
                                    getterCode = (fun args -> <@@ symbolicNameValue @@>))

                let description = ProvidedProperty(
                                    propertyName = "Description",
                                    propertyType = typeof<string>,
                                    isStatic = true,
                                    getterCode = (fun args -> <@@ descriptionValue @@>))

                let addedValue = ProvidedProperty(
                                    propertyName = "AddedValue",
                                    propertyType = typeof<string>,
                                    isStatic = true,
                                    getterCode = (fun args -> <@@ addedValue @@>))

                valueType.AddMember(tag)
                valueType.AddMember(symbolicName)
                valueType.AddMember(description)
                valueType.AddMember(addedValue)

                enumType.AddMember(valueType)
            )

            types.Add(enumType)
        )        

    types