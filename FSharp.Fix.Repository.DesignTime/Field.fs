module Repository.Field

open ProviderImplementation.ProvidedTypes
open Fix.Repository.Xml

let createField namespaceName assembly (field:Fix.Repository.Xml.Field) =

    let fieldType = ProvidedTypeDefinition(assembly, namespaceName, field.Name, Some typeof<obj>)

    // the property getters fail at runtime if we use item.property directly
    let tagValue = field.Tag
    let nameValue = field.Name
    let typeValue = field.Type
    let notReqXMLValue = field.NotReqXML
    let descriptionValue = field.Description
    let addedValue = field.Added

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

    fieldType

let createFields namespaceName assembly version =
    
    let fieldsType = ProvidedTypeDefinition(assembly, namespaceName, "Fields", Some typeof<obj>)

    version.Fields 
    |> Seq.map(fun field -> createField namespaceName assembly field)
    |> Seq.iter fieldsType.AddMember

    [fieldsType]