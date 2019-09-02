namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:components latestEP="97">
//     <fixr:component name="FinancingDetails" id="1002" category="Common" added="FIX.4.4" abbrName="FinDetls">
//         <fixr:fieldRef id="913" added="FIX.4.4">
//             <fixr:annotation>
//                 <fixr:documentation>
//                     The full name of the base standard agreement, annexes and amendments in place between the principals and applicable to this deal
//                 </fixr:documentation>
//             </fixr:annotation>
//         </fixr:fieldRef>
//     </fixr:component>
// </fixr:components>

[<CLIMutable>]
type Component =
    {
        [<XmlAttribute("name")>]
        Name:string
        [<XmlAttribute("id")>]
        Id:int
        [<XmlAttribute("category")>]
        Category:string
        [<XmlAttribute("added")>]
        Added:string
        [<XmlAttribute("abbrName")>]
        AbbrName:string
        [<XmlElement("annotation")>]
        Annotations:Annotation[]
    }

[<CLIMutable>]
type Components =
    {
        [<XmlElement("component")>]
        Components : Component[]
    }