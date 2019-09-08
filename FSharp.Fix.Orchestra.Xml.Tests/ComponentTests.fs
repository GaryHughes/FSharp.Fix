namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestComponent () =

    [<TestMethod>]
    member this.TestLoadComponent () =
        let text = 
            @"<fixr:components latestEP=""97"" xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                <fixr:component name=""FinancingDetails"" id=""1002"" category=""Common"" added=""FIX.4.4"" abbrName=""FinDetls"">\n\
                    <fixr:fieldRef id=""913"" added=""FIX.4.4"">\n\
                        <fixr:annotation>\n\
                            <fixr:documentation>\n\
                                The full name of the base standard agreement, annexes and amendments in place between the principals and applicable to this deal\n\
                            </fixr:documentation>\n\
                        </fixr:annotation>\n\
                    </fixr:fieldRef>\n\
                </fixr:component>\n\
            </fixr:components>";
        let components = loadOrchestraFragment<Components> text
        Assert.AreEqual("97", components.LatestEP)
        let cmp = components.Components.[0]
        Assert.AreEqual("FinancingDetails", cmp.Name)
