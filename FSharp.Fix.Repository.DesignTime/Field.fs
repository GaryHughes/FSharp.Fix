module Repository.Field

open ProviderImplementation.ProvidedTypes
open Repository.Xml.Field

let createFields namespaceName assembly versionPath =
    
    let fieldsType = ProvidedTypeDefinition(assembly, namespaceName, "Fields", Some typeof<obj>)

    let fields = loadFields versionPath

    for item in fields do

        let fieldType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        let tagValue = item.Tag
        let nameValue = item.Name
        let typeValue = item.Type
        let notReqXMLValue = item.NotReqXML
        let descriptionValue = item.Description
        let addedValue = item.Added

        let tagType = ProvidedProperty(
                        propertyName = "Tag",
                        propertyType = typeof<int>,
                        isStatic = true,
                        getterCode = (fun args -> <@@ tagValue @@>))

        let nameType = ProvidedProperty(
                        propertyName = "Name",
                        propertyType = typeof<string>,
                        isStatic = true,
                        getterCode = (fun args -> <@@ nameValue @@>))

        let typeType = ProvidedProperty(
                        propertyName = "Type",
                        propertyType = typeof<string>,
                        isStatic = true,
                        getterCode = (fun args -> <@@ typeValue @@>))

        let notReqXMLType = ProvidedProperty(
                                propertyName = "NotReqXML",
                                propertyType = typeof<string>,
                                isStatic = true,
                                getterCode = (fun args -> <@@ notReqXMLValue @@>))

        let descriptionType = ProvidedProperty(
                                propertyName = "Description",
                                propertyType = typeof<string>,
                                isStatic = true,
                                getterCode = (fun args -> <@@ descriptionValue @@>))

        let addedType = ProvidedProperty(
                            propertyName = "Added",
                            propertyType = typeof<string>,
                            isStatic = true,
                            getterCode = (fun args -> <@@ addedValue @@>))

        fieldType.AddMember(tagType)
        fieldType.AddMember(nameType)
        fieldType.AddMember(typeType)
        fieldType.AddMember(notReqXMLType)
        fieldType.AddMember(descriptionType)
        fieldType.AddMember(addedType)

        fieldsType.AddMember(fieldType)

    [fieldsType]