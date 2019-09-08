namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestGroup () =

    [<TestMethod>]
    member this.TestLoadGroup () =
        let text = 
            @"<fixr:groups latestEP=""97"" xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                <fixr:group id=""1007"" added=""FIX.4.4"" name=""LegStipulations"" category=""Common"" abbrName=""Stip"">\n\
                    <fixr:numInGroup id=""683""/>\n\
                    <fixr:fieldRef id=""688"" added=""FIX.4.4"">\n\
                        <fixr:annotation>\n\
                            <fixr:documentation>\n\
                                Required if NoLegStipulations &gt;0\n\
                            </fixr:documentation>\n\
                        </fixr:annotation>\n\
                    </fixr:fieldRef>\n\
                </fixr:group>\n\
            </fixr:groups>";
        let groups = loadOrchestraFragment<Groups> text
        Assert.AreEqual("97", groups.LatestEP)
        let group = groups.Groups.[0]
        Assert.AreEqual(1007, group.Id)
