module Repository.Message

open ProviderImplementation.ProvidedTypes
open Repository.Xml.Message

let createMessages namespaceName assembly versionPath =
    
    let messagesType = ProvidedTypeDefinition(assembly, namespaceName, "Messages", Some typeof<obj>)

    let messages = loadMessages versionPath

    for item in messages do

        let messageType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        let componentIDValue = item.ComponentID
        let msgTypeValue = item.MsgType
        let nameValue = item.Name
        let categoryIDValue = item.CategoryID
        let sectionIDValue = item.SectionID
        let notReqXMLValue = item.NotReqXML
        let descriptionValue = item.Description
        let addedValue = item.Added

        let componentIDType = ProvidedProperty(
                                propertyName = "ComponentID",
                                propertyType = typeof<string>,
                                isStatic = true,
                                getterCode = (fun args -> <@@ componentIDValue @@>))

        let msgTypeType = ProvidedProperty(
                            propertyName = "MsgType",
                            propertyType = typeof<string>,
                            isStatic = true,
                            getterCode = (fun args -> <@@ msgTypeValue @@>))

        let nameType = ProvidedProperty(
                        propertyName = "Name",
                        propertyType = typeof<string>,
                        isStatic = true,
                        getterCode = (fun args -> <@@ nameValue @@>))

        let categoryIDType = ProvidedProperty(
                                propertyName = "CategoryID",
                                propertyType = typeof<string>,
                                isStatic = true,
                                getterCode = (fun args -> <@@ categoryIDValue @@>))

        let sectionIDType = ProvidedProperty(
                                propertyName = "SectionID",
                                propertyType = typeof<string>,
                                isStatic = true,
                                getterCode = (fun args -> <@@ sectionIDValue @@>))

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

        messageType.AddMember(componentIDType)
        messageType.AddMember(msgTypeType)
        messageType.AddMember(nameType)
        messageType.AddMember(categoryIDType)
        messageType.AddMember(sectionIDType)
        messageType.AddMember(notReqXMLType)
        messageType.AddMember(descriptionType)
        messageType.AddMember(addedType)
  
        messagesType.AddMember(messageType)

    [messagesType]