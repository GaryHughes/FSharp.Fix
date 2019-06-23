module Repository.Field

open ProviderImplementation.ProvidedTypes
open Repository.Xml.Version

let createFields namespaceName assembly version =
    
    let fieldsType = ProvidedTypeDefinition(assembly, namespaceName, "Fields", Some typeof<obj>)

    for item in version.Fields do

        let fieldType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        // the property getters fail at runtime if we use item.property directly
        let tagValue = item.Tag
        let nameValue = item.Name
        let typeValue = item.Type
        let notReqXMLValue = item.NotReqXML
        let descriptionValue = item.Description
        let addedValue = item.Added

        // nameof operator please https://github.com/fsharp/fslang-design/blob/master/RFCs/FS-1003-nameof-operator.md
        ProvidedProperty(
            propertyName = "Tag",
            propertyType = typeof<int>,
            isStatic = true,
            getterCode = (fun args -> <@@ tagValue @@>))
        |> fieldType.AddMember

        ProvidedProperty(
            propertyName = "Name",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ nameValue @@>))
        |> fieldType.AddMember

        ProvidedProperty(
            propertyName = "Type",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ typeValue @@>))
        |> fieldType.AddMember

        ProvidedProperty(
            propertyName = "NotReqXML",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ notReqXMLValue @@>))
        |> fieldType.AddMember

        ProvidedProperty(
            propertyName = "Description",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ descriptionValue @@>))
        |> fieldType.AddMember

        ProvidedProperty(
            propertyName = "Added",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ addedValue @@>))
        |> fieldType.AddMember

        fieldType |> fieldsType.AddMember

    [fieldsType]