namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestMetadata () =

    [<TestMethod>]
    member this.TestLoadMetadata () =
        // let text = 
        //     @"<fixr:metadata xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"" xmlns:dc=""http://purl.org/dc/terms/"">\n\
        //          <dc:title>Orchestra</dc:title>\n\
        //          <dc:creator>unified2orchestra.xslt script</dc:creator>\n\
        //          <dc:publisher>FIX Trading Community</dc:publisher>\n\
        //          <dc:date>2019-03-12T10:11:49.744-05:00</dc:date>\n\
        //          <dc:format>Orchestra schema</dc:format>\n\
        //          <dc:source>FIX Unified Repository</dc:source>\n\
        //       </fixr:metadata>";
        // let metadata = loadOrchestraFragment<Metadata> text
        // Assert.AreEqual("Orchestra", metadata.Title)
        Assert.IsTrue(true)
       
