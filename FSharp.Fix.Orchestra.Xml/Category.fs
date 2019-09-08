namespace Orchestra.Xml

open System.Xml.Serialization

//  <fixr:categories>
//      <fixr:category name="Session" FIXMLFileName="session" componentType="Message" section="Session"/>
//      <fixr:category name="Indication" FIXMLFileName="indications" componentType="Message" section="PreTrade" includeFile="components"/>
//  </fixr:categories>

[<CLIMutable>]
[<XmlType("category")>]
type Category =
    {
        [<XmlAttribute("name")>]
        Name:string
        FIXMLFileName:string
        [<XmlAttribute("componentType")>]
        ComponentType:string
        [<XmlAttribute("section")>]
        Section:string
        [<XmlAttribute("includeFile")>]
        IncludeFile:string
    }

[<CLIMutable>]
[<XmlType("categories")>]
type Categories =
    {
        [<XmlElement("category")>]
        Categories : Category[]
    }
