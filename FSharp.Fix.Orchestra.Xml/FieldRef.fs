namespace Orchestra.Xml

open System.Xml.Serialization

//  <fixr:fieldRef id="688" added="FIX.4.4">
//      <fixr:annotation>
//          <fixr:documentation>
//              Required if NoLegStipulations &gt;0
//          </fixr:documentation>
//      </fixr:annotation>
//  </fixr:fieldRef>

[<CLIMutable>]
type FieldRef =
    {
        [<XmlAttribute("id")>]
        Id:int
        [<XmlAttribute("added")>]
        Added:string
        [<XmlAttribute("annotation")>]
        Annotation:Annotation
    }