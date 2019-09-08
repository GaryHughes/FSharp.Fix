namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestSection () =

    [<TestMethod>]
    member this.TestLoadSection () =
        let text = 
            @"<fixr:sections xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                <fixr:section name=""Session"" displayOrder=""0"" FIXMLFileName=""session"">\n\
                    <fixr:annotation>\n\
                        <fixr:documentation purpose=""SYNOPSIS"">\n\
                            Session level messages to establish and control a FIX session\n\
                        </fixr:documentation>\n\
                    </fixr:annotation>\n\
                </fixr:section>\n\
            </fixr:sections>";
        let sections = loadOrchestraFragment<Sections> text
        let section = sections.Sections.[0]
        Assert.AreEqual("Session", section.Name)
