namespace Fix.Repository.Xml

open System.Xml.Serialization

// Types to load the FIX repository MsgContents.xml file. 
//
// <MsgContents xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.4.0" xsi:noNamespaceSchemaLocation="../../schema/MsgContents.xsd">
//     <MsgContent added="FIX.2.7">
//      <ComponentID>1</ComponentID>
//      <TagText>StandardHeader</TagText>
//      <Indent>0</Indent>
//      <Position>1</Position>
//      <Reqd>1</Reqd>
//      <Description>MsgType = 0</Description>
//  </MsgContent>
// </MsgContents>

[<CLIMutable>]
type MsgContent =
    {
        ComponentID:string
        TagText:string
        Indent:string
        Position:string
        Reqd:string
        Description:string
        [<XmlAttribute("added")>]
        Added:string   
    }

[<CLIMutable>]
[<XmlRoot("MsgContents")>]
type MsgContents =
    {
        [<XmlElement("MsgContent")>]
        Items : MsgContent[]
    }
