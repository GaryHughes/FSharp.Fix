module Repository.Message

open ProviderImplementation.ProvidedTypes
open Repository.Xml.Version

let createMessages namespaceName assembly version =
    
    let messagesType = ProvidedTypeDefinition(assembly, namespaceName, "Messages", Some typeof<obj>)

    for item in version.Messages do

        let messageType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        let componentIDValue = item.ComponentID
        let msgTypeValue = item.MsgType
        let nameValue = item.Name
        let categoryIDValue = item.CategoryID
        let sectionIDValue = item.SectionID
        let notReqXMLValue = item.NotReqXML
        let descriptionValue = item.Description
        let addedValue = item.Added

        // nameof operator please https://github.com/fsharp/fslang-design/blob/master/RFCs/FS-1003-nameof-operator.md
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