namespace Orchestra.Xml

open System.Xml.Serialization

//  <fixr:categories>
//      <fixr:category name="Session" FIXMLFileName="session" componentType="Message" section="Session"/>
//      <fixr:category name="Indication" FIXMLFileName="indications" componentType="Message" section="PreTrade" includeFile="components"/>
//  </fixr:categories>

[<CLIMutable>]
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
type Categories =
    {
        [<XmlElement("category")>]
        Categories : Category[]
    }
