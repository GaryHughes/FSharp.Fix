module Repository.Message

open System
open System.Collections
open System.Collections.Generic
open ProviderImplementation.ProvidedTypes
open Repository.Xml.Version
open Repository.Xml.Message


// This is the definition of a field for a specific message.
type public Field(field:Xml.Field.Field, required:bool, indent:int, added:string) =
    // Common properties from the version field definition
    member public __.Tag with get() = field.Tag 
    member public __.Name with get() = field.Name
    member public __.Type with get() = field.Type
    member public __.NotReqXML with get() = field.NotReqXML
    member public __.Description with get() = field.Description
    // Is this field required in this message
    member public __.Required = required
    // When was this field added to this message
    member public __.Added = added
    // What is the nesting level of this instance of this field in this message.
    // Each nested group adds one to the indent.
    member public __.Indent = indent


let fieldsForMessage path componentID =

    // Can we, should we cache this stuff better?
    let versionFields = Xml.Field.loadFields(path)
    let msgContents = Xml.MsgContent.loadMsgContents(path)
    let components = Xml.Component.loadComponents(path)

    let messageFields = List<Field>()

    let contentByComponentID = msgContents |> Seq.groupBy(fun content -> content.ComponentID) |> dict
    let componentsByName = components |> Seq.map(fun comp -> (comp.Name, comp)) |> dict

    let rec createFields tag parentIndent =
        let tagExists, msgContents = contentByComponentID.TryGetValue(tag)
        match tagExists with
        | false -> printfn "could not find MsgContent with ComponentID = %s" tag
        | true -> 
            for content in msgContents do
                // If TagText is an int it is a Field.Tag otherwise it is a Component.ComponentID
                let isInt, fieldTag = Int32.TryParse(content.TagText)
                let indent = Convert.ToInt32(content.Indent)
                match isInt with
                | true -> 
                    // TODO - index this lookup 
                    let field = versionFields |> Seq.find(fun field -> field.Tag = fieldTag)
                    messageFields.Add(
                        Field(
                            field, 
                            Convert.ToInt32(content.Reqd) <> 0, 
                            parentIndent + Convert.ToInt32(content.Indent), 
                            content.Added
                        )
                    )
                | false ->
                    let exists, comp = componentsByName.TryGetValue(content.TagText)             
                    match exists with
                    | false -> printfn "could not find Component with Name = %s" content.TagText
                    | true -> createFields comp.ComponentID indent

    createFields componentID 0
   
    messageFields.ToArray()

type public Fields(path, componentID) =

    let fields = fieldsForMessage path componentID

    member public __.Item index = fields.[index]

    interface IEnumerable<Field> with
        member __.GetEnumerator() : IEnumerator<Field> = 
            (Seq.init fields.Length __.Item).GetEnumerator()
        member __.GetEnumerator() : IEnumerator = 
            (Seq.init fields.Length __.Item).GetEnumerator() :> IEnumerator

 
let createMessages namespaceName assembly version =
    
    let messagesType = ProvidedTypeDefinition(assembly, namespaceName, "Messages", Some typeof<obj>)

    for item in version.Messages do

        let messageType = ProvidedTypeDefinition(assembly, namespaceName, item.Name, Some typeof<obj>)

        // the property getters fail at runtime if we use item.property directly
        let repositoryPath = version.Path
        let componentID = item.ComponentID

        ProvidedProperty(
            propertyName = "Fields",
            propertyType = typeof<Fields>,
            isStatic = true,
            getterCode = (fun args -> <@@ Fields(repositoryPath, componentID) @@> ))
        |>  messageType.AddMember
        
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

        messageType |> messagesType.AddMember

    [messagesType]