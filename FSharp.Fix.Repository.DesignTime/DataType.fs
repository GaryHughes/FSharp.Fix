module Repository.DataType

open ProviderImplementation.ProvidedTypes
open Fix.Repository.Xml

let createDataTypes namespaceName assembly version =

    let dataTypesType = ProvidedTypeDefinition(assembly, namespaceName, "DataTypes", Some typeof<obj>)

    for item in version.DataTypes do
    
        let dataType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        // the property getters fail at runtime if we use item.property directly
        let baseTypeValue = item.BaseType
        let descriptionValue = item.Description
        let addedValue = item.Added
        let updatedValue = item.Updated
        let updatedEPValue = item.UpdatedEP

        // nameof operator please https://github.com/fsharp/fslang-design/blob/master/RFCs/FS-1003-nameof-operator.md
        ProvidedProperty(
            propertyName = "BaseType",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ baseTypeValue @@>))
        |> dataType.AddMember

        ProvidedProperty(
            propertyName = "Description",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ descriptionValue @@>))
        |> dataType.AddMember

        ProvidedProperty(
            propertyName = "Added",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ addedValue @@>))
        |> dataType.AddMember

        ProvidedProperty(
            propertyName = "Updated",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ updatedValue @@>))
        |> dataType.AddMember

        ProvidedProperty(
            propertyName = "UpdatedEP",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ updatedEPValue @@>))
        |> dataType.AddMember

        dataType |> dataTypesType.AddMember

    [dataTypesType]