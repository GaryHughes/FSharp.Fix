namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:datatypes>
// <fixr:datatype name="int" added="FIX.2.7" issue="SPEC-370">
//  <fixr:mappedDatatype standard="XML" base="xs:integer" builtin="1">
//      <fixr:annotation>
//          <fixr:documentation purpose="SYNOPSIS">
//  Sequence of digits without commas or decimals and optional sign character (ASCII characters "-" and "0" - "9" ). The sign character utilizes one byte (i.e. positive int is "99999" while negative int is "-99999"). Note that int values may contain leading zeros (e.g. "00023" = "23").
//  Examples:
//  723 in field 21 would be mapped int as |21=723|.
//  -723 in field 12 would be mapped int as |12=-723|
//  The following data types are based on int.
//             </fixr:documentation>
//      </fixr:annotation>
//  </fixr:mappedDatatype>
//  <fixr:annotation>
//      <fixr:documentation purpose="SYNOPSIS">
//  Sequence of digits without commas or decimals and optional sign character (ASCII characters "-" and "0" - "9" ). The sign character utilizes one byte (i.e. positive int is "99999" while negative int is "-99999"). Note that int values may contain leading zeros (e.g. "00023" = "23").
//  Examples:
//  723 in field 21 would be mapped int as |21=723|.
//  -723 in field 12 would be mapped int as |12=-723|
//  The following data types are based on int.
//         </fixr:documentation>
//  </fixr:annotation>
// </fixr:datatype>
// </fixr:datatypes>

[<CLIMutable>]
[<XmlType("mappedDatatype")>]
type MappedDataType =
    {
        [<XmlElement("annotation")>]
        Annotation:Annotation[]
    }

[<CLIMutable>]
[<XmlType("datatype")>]
type DataType =
    {
        [<XmlAttribute("name")>]
        Name : string
        [<XmlElement("mappedDatatype")>]
        MappedDataType:MappedDataType[]
        [<XmlElement("annotation")>]
        Annotation:Annotation[]
    }

[<CLIMutable>]
[<XmlType("datatypes")>]
type DataTypes =
    {
        [<XmlElement("datatype")>]
        DataType : DataType[]
    }