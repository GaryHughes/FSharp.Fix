namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:messages latestEP="97">
//     <fixr:message name="Heartbeat" id="1" msgType="0" category="Session" added="FIX.2.7" abbrName="Heartbeat">
//         <fixr:structure>
//             <fixr:componentRef id="1024" presence="required" added="FIX.2.7">
//                 <fixr:annotation>
//                     <fixr:documentation>
//                         MsgType = 0
//                     </fixr:documentation>
//                 </fixr:annotation>
//             </fixr:componentRef>
//             <fixr:fieldRef id="112" added="FIX.4.0">
//                 <fixr:annotation>
//                     <fixr:documentation>
//                         Required when the heartbeat is the result of a Test Request message.
//                     </fixr:documentation>
//                 </fixr:annotation>
//             </fixr:fieldRef>
//         </fixr:structure>
//         <fixr:annotation>
//             <fixr:documentation purpose="SYNOPSIS">
//                 The Heartbeat monitors the status of the communication link and identifies when the last of a string of messages was not received.
//             </fixr:documentation>
//         </fixr:annotation>
//     </fixr:message>
// </fixr:messages>

[<CLIMutable>]
type ComponentRef =
    {
        [<XmlAttribute("id")>]
        Id : int
        [<XmlAttribute("presence")>]
        Presence : string
        [<XmlAttribute("added")>]
        Added : string
        [<XmlElement>]
        Annotation : Annotation[]
    }


[<CLIMutable>]
type Structure = 
    {
        // This typed grouping breaks ordering, is that important?
        [<XmlElement("componentRef")>]
        ComponentRefs : ComponentRef[]
        [<XmlElement("fieldRef")>]
        FieldRefs : FieldRef[]
    }

[<CLIMutable>]
type Message =
    {
        Structure : Structure
    }

[<CLIMutable>]
type Messages =
    {
        [<XmlElement("message")>]
        Message : Message[]
    }