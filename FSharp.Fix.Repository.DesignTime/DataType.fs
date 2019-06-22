module Repository.DataType

open ProviderImplementation.ProvidedTypes
open Repository.Xml.DataType

let createDataTypes namespaceName assembly versionPath =

    let dataTypesType = ProvidedTypeDefinition(assembly, namespaceName, "DataTypes", Some typeof<obj>)

    let dataTypes = loadDataTypes versionPath

    for item in dataTypes do
    
        let dataType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        let baseTypeValue = item.BaseType
        let descriptionValue = item.Description
        let addedValue = item.Added
        let updatedValue = item.Updated
        let updatedEPValue = item.UpdatedEP

        let baseType = ProvidedProperty(
                        propertyName = "BaseType",
                        propertyType = typeof<string>,
                        isStatic = true,
                        getterCode = (fun args -> <@@ baseTypeValue @@>))

        let description = ProvidedProperty(
                            propertyName = "Description",
                            propertyType = typeof<string>,
                            isStatic = true,
                            getterCode = (fun args -> <@@ descriptionValue @@>))

        let added = ProvidedProperty(
                        propertyName = "Added",
                        propertyType = typeof<string>,
                        isStatic = true,
                        getterCode = (fun args -> <@@ addedValue @@>))

        let updated = ProvidedProperty(
                        propertyName = "Updated",
                        propertyType = typeof<string>,
                        isStatic = true,
                        getterCode = (fun args -> <@@ updatedValue @@>))

        let updatedEP = ProvidedProperty(
                            propertyName = "UpdatedEP",
                            propertyType = typeof<string>,
                            isStatic = true,
                            getterCode = (fun args -> <@@ updatedEPValue @@>))

        dataType.AddMember(baseType)
        dataType.AddMember(description)
        dataType.AddMember(added)
        dataType.AddMember(updated)
        dataType.AddMember(updatedEP)

        dataTypesType.AddMember(dataType)

    [dataTypesType]