module Repository.DataType

open ProviderImplementation.ProvidedTypes
open Repository.Xml.Version

let createDataTypes namespaceName assembly version =

    let dataTypesType = ProvidedTypeDefinition(assembly, namespaceName, "DataTypes", Some typeof<obj>)

    for item in version.DataTypes do
    
        let dataType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        let baseTypeValue = item.BaseType
        let descriptionValue = item.Description
        let addedValue = item.Added
        let updatedValue = item.Updated
        let updatedEPValue = item.UpdatedEP

        // nameof operator please https://github.com/fsharp/fslang-design/blob/master/RFCs/FS-1003-nameof-operator.md
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