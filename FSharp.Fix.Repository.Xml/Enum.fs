namespace Fix.Repository.Xml

open System.Xml.Serialization

// Types to load the FIX repository Enums.xml file.
//
// <Enums xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.4.0" xsi:noNamespaceSchemaLocation="../../schema/Enums.xsd">
//   <Enum added="FIX.2.7">
//     <Tag>4</Tag>
//     <Value>B</Value>
//     <SymbolicName>Buy</SymbolicName>
//     <Description>Buy</Description>
//   </Enum>
//  </Enums>

[<CLIMutable>]
type Enum =
    {
        Tag:int
        Value:string
        SymbolicName:string
        Description:string
        [<XmlAttribute("added")>]
        Added:string
    }

[<CLIMutable>]
[<XmlRoot("Enums")>]
type Enums =
    {
        [<XmlElement("Enum")>]
        Items : Enum[]
    }


