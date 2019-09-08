namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestFieldRef () =

    [<TestMethod>]
    member this.TestLoadFieldRef () =
        let text = 
            @"<fixr:fieldRef id=""688"" added=""FIX.4.4"" xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                 <fixr:annotation>\n\
                     <fixr:documentation>\n\
                         Required if NoLegStipulations &gt;0\n\
                     </fixr:documentation>\n\
                 </fixr:annotation>\n\
             </fixr:fieldRef>";
        let fieldRef = loadOrchestraFragment<FieldRef> text
        Assert.AreEqual(688, fieldRef.Id)
