namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestAnnotation () =

    [<TestMethod>]
    member this.TestLoadAnnotation () =
        let text = 
            @"<fixr:annotation xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                  <fixr:documentation purpose=""SYNOPSIS"">\n\
                      Buy\n\
                  </fixr:documentation>\n\
             </fixr:annotation>";
        let annotation = loadOrchestraFragment<Annotation> text
        let documentation = annotation.Documentation.[0]
        Assert.AreEqual("SYNOPSIS", documentation.Purpose)
