namespace Fix.Repository.Xml

open System.Xml.Serialization

// Types to load the FIX repository Messages.xml file.
//
// <Messages xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.4.0" xsi:noNamespaceSchemaLocation="../../schema/Messages.xsd" generated="2010-03-13T14:54:02-05:00">
//  <Message added="FIX.2.7">
//      <ComponentID>1</ComponentID>
//      <MsgType>0</MsgType>
//      <Name>Heartbeat</Name>
//      <CategoryID>Session</CategoryID>
//      <SectionID>Session</SectionID>
//      <NotReqXML>1</NotReqXML>
//      <Description>The Heartbeat monitors the status of the communication link and identifies when the last of a string of messages was not received.</Description>
//  </Message>
// </Messages>

[<CLIMutable>]
type Message =
    {
        ComponentID:string
        MsgType:string
        Name:string
        CategoryID:string
        SectionID:string
        NotReqXML:string
        Description:string
        [<XmlAttribute("added")>]
        Added:string
    }

[<CLIMutable>]
[<XmlRoot("Messages")>]
type Messages =
    {
        [<XmlElement("Message")>]
        Items : Message[]
    }
