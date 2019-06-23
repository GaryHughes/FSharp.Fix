module Repository.Message

open System
open ProviderImplementation.ProvidedTypes
open Repository.Xml.Version
open Repository.Xml.Message
open Repository.Xml.MsgContent
open Repository.Field

let createFieldsForMessage assembly namespaceName version (message:Message) =

    let fieldsType = ProvidedTypeDefinition(assembly, namespaceName, "Fields", Some typeof<obj>)

    let contentByComponentID = version.MsgContents |> Seq.groupBy(fun content -> content.ComponentID) |> dict
    let componentsByName = version.Components |> Seq.map(fun comp -> (comp.Name, comp)) |> dict

    let rec createFields tag =
        let tagExists, msgContents = contentByComponentID.TryGetValue(tag)
        match tagExists with
        | false -> printfn "could not find MsgContent with ComponentID = %s" tag
        | true -> 
            for content in msgContents do
                // If TagText is an int it is a Field.Tag otherwise it is a Component.ComponentID
                let isInt, fieldTag = Int32.TryParse(content.TagText)
                match isInt with
                | true -> 
                    // TODO - index this lookup 
                    let field = version.Fields |> Seq.find(fun field -> field.Tag = fieldTag)
                    let fieldType = createField namespaceName assembly field
                    fieldsType.AddMember(fieldType)
                | false ->
                    let exists, comp = componentsByName.TryGetValue(content.TagText)             
                    match exists with
                    | false -> printfn "could not find Component with Name = %s" content.TagText
                    | true -> createFields comp.ComponentID

    createFields message.ComponentID
   
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

        createFieldsForMessage assembly namespaceName version item |> messageType.AddMember
        
        messageType |> messagesType.AddMember

    [messagesType]