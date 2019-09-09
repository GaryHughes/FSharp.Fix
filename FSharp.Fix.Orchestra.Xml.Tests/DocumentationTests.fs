namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestDocumentation () =

    [<TestMethod>]
    member this.TestLoadDocumentation () =
        let text = 
            @"<fixr:documentation xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"" purpose=""SYNOPSIS"">\n\
                Buy\n\
              </fixr:documentation>";
        let documentation = loadOrchestraFragment<Documentation> text
        Assert.AreEqual("SYNOPSIS", documentation.Purpose)
    


