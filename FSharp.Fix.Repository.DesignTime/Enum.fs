module Repository.Enum

open ProviderImplementation.ProvidedTypes
open Repository.Xml.Enum
open Repository.Xml.Field

let createEnums (versionType:ProvidedTypeDefinition) namespaceName assembly versionPath =
    
    let fields = loadFields versionPath
    
    loadEnums versionPath
    |> Seq.groupBy(fun enum -> enum.Tag)
    |> Seq.iter(fun (tag, values) ->
        let field = query {
            for field in fields do
            where (field.Tag = tag)
            select (Some (field, values))
            headOrDefault
        }
        match field with
        | None -> eprintf "Could not find field with Tag=%i version: %s\n" tag versionPath 
        | Some (field, values) ->
            let enumType = ProvidedTypeDefinition(assembly, namespaceName, field.Name, Some typeof<obj>)

            values
            |> Seq.iter(fun value ->
                let valueType = ProvidedTypeDefinition(assembly, namespaceName, value.SymbolicName, Some typeof<obj>)

                let tagValue = value.Tag
                let symbolicNameValue = value.SymbolicName
                let descriptionValue = value.Description
                let addedValue = value.Added

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

            versionType.AddMember(enumType)
        )        

    versionType