namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:codeSets>
//    <fixr:codeSet name="AdvSideCodeSet" id="4" type="char">
//      <fixr:code name="Buy" id="4001" value="B" sort="1" added="FIX.2.7">
//          <fixr:annotation>
//              <fixr:documentation purpose="SYNOPSIS">
//                  Buy
//              </fixr:documentation>
//          </fixr:annotation>
//      </fixr:code>
//    </fixr:codeSet>
// </fixr:codeSets>

[<CLIMutable>]
[<XmlType("code")>]
type Code =
    {
        [<XmlAttribute("name")>]
        Name : string
        [<XmlAttribute("id")>]
        Id : int
        [<XmlAttribute("value")>]
        Value : string
        [<XmlAttribute("sort")>]
        Sort : int
        [<XmlAttribute("added")>]
        Added : string
        [<XmlElement("annotation")>]
        Annotation:Annotation
    }

[<CLIMutable>]
[<XmlType("codeSet")>]
type CodeSet =
    {
        [<XmlAttribute("name")>]
        Name : string
        [<XmlAttribute("id")>]
        Id : int
        [<XmlAttribute("type")>]
        Type : string
        [<XmlElement("code")>]
        Codes : Code[]
    }

[<CLIMutable>]
[<XmlType("codeSets")>]
type CodeSets =
    {
        [<XmlElement("codeSet")>]
        CodeSets : CodeSet[]
    }
