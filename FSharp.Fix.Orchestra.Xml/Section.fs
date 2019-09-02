namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:sections>
//     <fixr:section name="Session" displayOrder="0" FIXMLFileName="session">
//         <fixr:annotation>
//             <fixr:documentation purpose="SYNOPSIS">
//                 Session level messages to establish and control a FIX session
//             </fixr:documentation>
//         </fixr:annotation>
//     </fixr:section>
// </fixr:sections>

[<CLIMutable>]
type Section =
    {
        [<XmlAttribute("name")>]
        Name:string
        [<XmlAttribute("displayOrder")>]
        DisplayOrder:int
        FIXMLFieldName:string
        [<XmlElement("annotation")>]
        Annotations:Annotation[]
    }

[<CLIMutable>]
type Sections =
    {
        [<XmlElement("section")>]
        Sections : Section[]
    }