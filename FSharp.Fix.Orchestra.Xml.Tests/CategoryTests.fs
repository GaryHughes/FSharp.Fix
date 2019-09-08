namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestCategory () =

    [<TestMethod>]
    member this.TestLoadCategory () =
        let text = 
            @"<fixr:categories xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                 <fixr:category name=""Session"" FIXMLFileName=""session"" componentType=""Message"" section=""Session""/>\n\
                 <fixr:category name=""Indication"" FIXMLFileName=""indications"" componentType=""Message"" section=""PreTrade"" includeFile=""components""/>\n\
              </fixr:categories>";
        let categories = loadOrchestraFragment<Categories> text
        Assert.AreEqual(2, categories.Categories.Length)
        let category = categories.Categories.[0]
        Assert.AreEqual("Session", category.Name)








