namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:fields latestEP="95">
//     <fixr:field id="1" name="Account" type="String" added="FIX.2.7" abbrName="Acct">
//         <fixr:annotation>
//             <fixr:documentation purpose="SYNOPSIS">
//                 Account mnemonic as agreed between buy and sell sides, e.g. broker and institution or investor/intermediary and fund manager.
//             </fixr:documentation>
//         </fixr:annotation>
//     </fixr:field>
//     <fixr:field id="2" name="AdvId" type="String" added="FIX.2.7" abbrName="AdvId">
//         <fixr:annotation>
//             <fixr:documentation purpose="SYNOPSIS">
//                 Unique identifier of advertisement message.
//                 (Prior to FIX 4.1 this field was of type int)
//             </fixr:documentation>
//         </fixr:annotation>
//     </fixr:field>
// </fixr:fields>

[<CLIMutable>]
type Field =
    {
        [<XmlAttribute("name")>]
        Name:string
        [<XmlAttribute("type")>]
        Type:string
        [<XmlAttribute("added")>]
        Added:string
        [<XmlAttribute("abbrName")>]
        AbbrName:string
        [<XmlElement("annotation")>]
        Annotations:Annotation[]
    }

[<CLIMutable>]
type Fields =
    {
        [<XmlElement("field")>]
        Fields : Field[]
    }