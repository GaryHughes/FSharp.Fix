namespace Orchestra.Xml

open System
open System.Xml.Serialization

// <fixr:metadata>
//  <dc:title>Orchestra</dc:title>
//  <dc:creator>unified2orchestra.xslt script</dc:creator>
//  <dc:publisher>FIX Trading Community</dc:publisher>
//  <dc:date>2019-03-12T10:11:49.744-05:00</dc:date>
//  <dc:format>Orchestra schema</dc:format>
//  <dc:source>FIX Unified Repository</dc:source>
// </fixr:metadata>

[<CLIMutable>]
type Metadata =
    {
        [<XmlAttribute("title")>]
        Title:string
        [<XmlAttribute("creator")>]
        Creator:string
        [<XmlAttribute("publisher")>]
        Publisher:string
        [<XmlAttribute("date")>]
        Date:DateTime
        [<XmlAttribute("format")>]
        Format:string
        [<XmlAttribute("source")>]
        Source:string
    }
