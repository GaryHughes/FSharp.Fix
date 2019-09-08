namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:annotation>
//      <fixr:documentation purpose="SYNOPSIS">
//          Buy
//      </fixr:documentation>
//  </fixr:annotation>

[<CLIMutable>]
[<XmlType("annotation")>]
type Annotation =
    {
        [<XmlElement("documentation")>] 
        Documentation : Documentation[]
    }