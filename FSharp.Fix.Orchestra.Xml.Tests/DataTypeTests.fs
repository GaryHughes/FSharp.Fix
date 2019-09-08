namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestDataType () =

    [<TestMethod>]
    member this.TestLoadDataType () =
        let text = 
            @"<fixr:datatypes xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">
               <fixr:datatype name=""int"" added=""FIX.2.7"" issue=""SPEC-370"">\n\
                 <fixr:mappedDatatype standard=""XML"" base=""xs:integer"" builtin=""1"">\n\
                     <fixr:annotation>\n\
                         <fixr:documentation purpose=""SYNOPSIS"">\n\
                 Sequence of digits without commas or decimals and optional sign character (ASCII characters ""-"" and ""0"" - ""9"" ). The sign character utilizes one byte (i.e. positive int is ""99999"" while negative int is ""-99999""). Note that int values may contain leading zeros (e.g. ""00023"" = ""23"").\n\
                 Examples:\n\
                 723 in field 21 would be mapped int as |21=723|.\n\
                 -723 in field 12 would be mapped int as |12=-723|\n\
                 The following data types are based on int.\n\
                            </fixr:documentation>\n\
                     </fixr:annotation>\n\
                 </fixr:mappedDatatype>\n\
                 <fixr:annotation>\n\
                     <fixr:documentation purpose=""SYNOPSIS"">\n\
                         Sequence of digits without commas or decimals and optional sign character (ASCII characters ""-"" and ""0"" - ""9"" ). The sign character utilizes one byte (i.e. positive int is ""99999"" while negative int is ""-99999""). Note that int values may contain leading zeros (e.g. ""00023"" = ""23"").\n\
                         Examples:\n\
                         723 in field 21 would be mapped int as |21=723|.\n\
                         -723 in field 12 would be mapped int as |12=-723|\n\
                         The following data types are based on int.\n\
                     </fixr:documentation>\n\
                 </fixr:annotation>\n\
              </fixr:datatype>\n\
             </fixr:datatypes>";
        let datatypes = loadOrchestraFragment<DataTypes> text
        let datatype = datatypes.DataType.[0]
        Assert.AreEqual("int", datatype.Name)
