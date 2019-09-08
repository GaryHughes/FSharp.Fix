namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:documentation purpose="SYNOPSIS">
//      Buy
// </fixr:documentation>

[<CLIMutable>]
[<XmlType("documentation")>]
type Documentation =
    {
        [<XmlAttribute("purpose")>]
        Purpose:string
    }