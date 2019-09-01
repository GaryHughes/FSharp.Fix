module Repository.Xml.Component

open System.Xml.Serialization
open System.IO

// Types to load the FIX repository Components.xml file. 
//
// <Components xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.4.0" xsi:noNamespaceSchemaLocation="../../schema/Components.xsd" generated="2010-03-13T14:54:02-05:00">
//  <Component added="FIX.4.0">
//     <ComponentID>1001</ComponentID>
//      <ComponentType>Block</ComponentType>
//      <CategoryID>Session</CategoryID>
//      <Name>StandardHeader</Name>
//      <NotReqXML>1</NotReqXML>
//      <Description>The standard FIX message header</Description>
//  </Component>
// </Components>

[<CLIMutable>]
type Component =
    {
        ComponentID:string
        ComponentType:string
        CategoryID:string
        Name:string
        NotReqXML:string
        Description:string
        [<XmlAttribute("added")>]
        Added:string
    }

[<CLIMutable>]
[<XmlRoot("Components")>]
type Components =
    {
        [<XmlElement("Component")>]
        Items : Component[]
    }
   
 let loadComponents versionPath =
    use stream = new FileStream(Path.Combine(versionPath, "Base", "Components.xml"), FileMode.Open)
    let serializer = XmlSerializer(typeof<Components>)
    let data = serializer.Deserialize(stream) :?> Components
    data.Items