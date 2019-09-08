namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestCodeSet () =

    [<TestMethod>]
    member this.TestLoadCodeSet () =
        let text = 
            @"<fixr:codeSets xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
               <fixr:codeSet name=""AdvSideCodeSet"" id=""4"" type=""char"">\n\
                 <fixr:code name=""Buy"" id=""4001"" value=""B"" sort=""1"" added=""FIX.2.7"">\n\
                     <fixr:annotation>\n\
                         <fixr:documentation purpose=""SYNOPSIS"">\n\
                             Buy\n\
                         </fixr:documentation>\n\
                     </fixr:annotation>\n\
                 </fixr:code>\n\
               </fixr:codeSet>\n\
            </fixr:codeSets>";
        let codeSets = loadOrchestraFragment<CodeSets> text
        let codeSet = codeSets.CodeSets.[0]
        Assert.AreEqual("AdvSideCodeSet", codeSet.Name)
        let code = codeSet.Codes.[0]
        Assert.AreEqual("Buy", code.Name)
