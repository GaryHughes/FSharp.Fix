namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:codeSets>
//    <fixr:codeSet name="AdvSideCodeSet" id="4" type="char">
//      <fixr:code name="Buy" id="4001" value="B" sort="1" added="FIX.2.7">
//          <fixr:annotation>
//              <fixr:documentation purpose="SYNOPSIS">
//                  Buy
//              </fixr:documentation>
//          </fixr:annotation>
//      </fixr:code>
//    </fixr:codeSet>
// </fixr:codeSets>

[<CLIMutable>]
type Code =
    {
        Annotation:Annotation
    }

[<CLIMutable>]
type CodeSets =
    {
        [<XmlElement("code")>]
        Codes : Code[]
    }
