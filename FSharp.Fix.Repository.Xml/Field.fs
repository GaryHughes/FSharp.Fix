namespace Fix.Repository.Xml

open System.Xml.Serialization

// Types to load the FIX repository Fields.xml file.
//
// <Fields xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.4.0" xsi:noNamespaceSchemaLocation="../../schema/Fields.xsd" generated="2010-03-13T14:54:02-05:00">
//  <Field added="FIX.2.7">
//      <Tag>1</Tag>
//      <Name>Account</Name>
//      <Type>char</Type>
//      <NotReqXML>1</NotReqXML>
//      <Description>Account mnemonic as agreed between broker and institution.</Description>
//  </Field>
// </Fields>
  
[<CLIMutable>]
type Field =
    {
        Tag:int
        Name:string
        Type:string
        NotReqXML:string
        Description:string
        [<XmlAttribute("added")>]
        Added:string
    }

[<CLIMutable>]
[<XmlRoot("Fields")>]
type Fields =
    {
        [<XmlElement("Field")>]
        Items : Field[]
    }
