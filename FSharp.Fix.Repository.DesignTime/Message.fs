module Repository.Message

open ProviderImplementation.ProvidedTypes
open Repository.Xml.Version

let createFieldsForMessage assembly namespaceName version message =
    let fieldsType = ProvidedTypeDefinition(assembly, namespaceName, "Fields", Some typeof<obj>)

    fieldsType

let createMessages namespaceName assembly version =
    
    let messagesType = ProvidedTypeDefinition(assembly, namespaceName, "Messages", Some typeof<obj>)

    for item in version.Messages do

        let messageType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        // the property getters fail at runtime if we use item.property directly
        let componentIDValue = item.ComponentID
        let msgTypeValue = item.MsgType
        let nameValue = item.Name
        let categoryIDValue = item.CategoryID
        let sectionIDValue = item.SectionID
        let notReqXMLValue = item.NotReqXML
        let descriptionValue = item.Description
        let addedValue = item.Added

        // nameof operator please https://github.com/fsharp/fslang-design/blob/master/RFCs/FS-1003-nameof-operator.md
        ProvidedProperty(
            propertyName = "ComponentID",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ componentIDValue @@>)) 
        |> messageType.AddMember

        ProvidedProperty(
            propertyName = "MsgType",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ msgTypeValue @@>))
        |> messageType.AddMember

        ProvidedProperty(
            propertyName = "Name",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ nameValue @@>))
        |> messageType.AddMember

        ProvidedProperty(
            propertyName = "CategoryID",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ categoryIDValue @@>))
        |> messageType.AddMember

        ProvidedProperty(
            propertyName = "SectionID",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ sectionIDValue @@>))
        |> messageType.AddMember

        ProvidedProperty(
            propertyName = "NotReqXML",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ notReqXMLValue @@>))
        |> messageType.AddMember

        ProvidedProperty(
            propertyName = "Description",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ descriptionValue @@>))
        |> messageType.AddMember

        ProvidedProperty(
            propertyName = "Added",
            propertyType = typeof<string>,
            isStatic = true,
            getterCode = (fun args -> <@@ addedValue @@>))
        |> messageType.AddMember

        createFieldsForMessage assembly namespaceName version item |> messagesType.AddMember
        messageType |> messagesType.AddMember

    [messagesType]