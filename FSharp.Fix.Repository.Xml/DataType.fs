namespace Fix.Repository.Xml

open System.Xml.Serialization

// Types to load the FIX repository Datatypes.xml file. 
//
// <Datatypes xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.4.0" xsi:noNamespaceSchemaLocation="../../schema/Datatypes.xsd" generated="2010-03-13T14:54:02-05:00">
//  <Datatype added="FIX.2.7">
//      <Name>float</Name>
//      <Description>Sequence of digits with optional decimal point and sign character (ASCII characters "-", "0" - "9" and "."); the absence of the decimal point within the string will be interpreted as the float representation of an integer value. All float fields must accommodate up to fifteen significant digits.</Description>
//  </Datatype>
// </DataTypes>

[<CLIMutable>]
type DataType =
    {
        Name:string
        BaseType:string
        Description:string
        [<XmlAttribute("added")>]
        Added:string
        [<XmlAttribute("updated")>]
        Updated:string
        [<XmlAttribute("updatedEP")>]
        UpdatedEP:string
    }

[<CLIMutable>]
[<XmlRoot("Datatypes")>]
type DataTypes =
    {
        [<XmlElement("Datatype")>]
        Items : DataType[]
    }
