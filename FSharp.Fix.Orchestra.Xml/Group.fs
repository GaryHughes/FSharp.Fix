namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:groups latestEP="97">
//     <fixr:group id="1007" added="FIX.4.4" name="LegStipulations" category="Common" abbrName="Stip">
//         <fixr:numInGroup id="683"/>
//         <fixr:fieldRef id="688" added="FIX.4.4">
//             <fixr:annotation>
//                 <fixr:documentation>
//                     Required if NoLegStipulations &gt;0
//                 </fixr:documentation>
//             </fixr:annotation>
//         </fixr:fieldRef>
//     </fixr:group>
// </fixr:groups>

[<CLIMutable>]
[<XmlType("numInGroup")>]
type NumInGroup =
    {
        [<XmlAttribute("id")>]
        Id:int
    }

[<CLIMutable>]
[<XmlType("group")>]
type Group =
    {
        [<XmlAttribute("id")>]
        Id:int
        [<XmlAttribute("added")>]
        Added:string
        [<XmlAttribute("abbrName")>]
        AbbrName:string
        [<XmlElement>]
        NumInGroup:NumInGroup
        [<XmlElement("annotation")>]
        Annotations:Annotation[]
    }

[<CLIMutable>]
[<XmlType("groups")>]
type Groups =
    {
        [<XmlAttribute("latestEP")>]
        LatestEP:string
        [<XmlElement("group")>]
        Groups : Group[]
    }