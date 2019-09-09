namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestActor () =

    [<TestMethod>]
    member this.TestLoadActor () =
        let text = 
            @"<fixr:actors xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                <fixr:actor name=""BuySide"">\n\
                    <fixr:annotation supported=""supported"">\n\
                        <fixr:documentation supported=""supported"">An order originator that intends to make a profit or mitigate risk</fixr:documentation>\n\
                    </fixr:annotation>\n\
                </fixr:actor>\n\
                <fixr:actor name=""Market"">\n\
                    <fixr:states name=""MarketPhase"">\n\
                        <fixr:initial name=""Closed"">\n\
                            <fixr:transition name=""Reopening"" target=""Preopen""/>\n\
                            <fixr:onentry>\n\
                                <fixr:messageRef name=""TradingSessionStatus"" implMinOccurs=""1"" implMaxOccurs=""unbounded"" id=""41"" scenario=""closed""/>\n\
                            </fixr:onentry>\n\
                            <fixr:annotation supported=""supported"">\n\
                                <fixr:documentation supported=""supported"">No orders or quotes may be entered.</fixr:documentation>\n\
                            </fixr:annotation>\n\
                        </fixr:initial>\n\
                        <fixr:state name=""Halted"">\n\
                            <fixr:transition name=""Resumed"" target=""Preopen"">\n]
                                <fixr:annotation supported=""supported"">\n\
                                    <fixr:documentation supported=""supported"">Authorities resume a market after a halt.</fixr:documentation>\n\
                                </fixr:annotation>\n\
                            </fixr:transition>\n\
                            <fixr:onentry>\n\
                                <fixr:messageRef name=""TradingSessionStatus"" implMinOccurs=""1"" implMaxOccurs=""unbounded"" id=""41"" scenario=""halted""/>\n\
                            </fixr:onentry>\n\
                            <fixr:annotation supported=""supported"">\n\
                                <fixr:documentation supported=""supported"">Order matching is suspended due to unexpected conditions or by a circuit-breaker rule.</fixr:documentation>\n\
                            </fixr:annotation>\n\
                        </fixr:state>\n\
                        <fixr:annotation><fixr:documentation>OrderState state machine associated to OrdStatus (39) code set and order state matrices</fixr:documentation></fixr:annotation>\n\
                        </fixr:states>\n\
                        <fixr:fieldRef id=""75"" scenario=""base"" supported=""supported"" presence=""required""/>\n\
                        <fixr:fieldRef id=""1679"" scenario=""base"" supported=""supported"" presence=""required""/>\n\
                        <fixr:groupRef implMaxOccurs=""unbounded"" presence=""required"" id=""2186"" scenario=""base"" supported=""supported"">\n\
                            <fixr:annotation supported=""supported"">\n\
                                <fixr:documentation supported=""supported"">Repeating group of security status by SecurityID</fixr:documentation>\n\
                            </fixr:annotation>\n\
                        </fixr:groupRef>\n\
                        <fixr:annotation supported=""supported"">\n\
                            <fixr:documentation supported=""supported"">Matches orders entered by buy-side participants</fixr:documentation>\n\
                        </fixr:annotation>\n\
                </fixr:actor>\n\
                <fixr:flow name=""OrderEntry"" source=""BuySide"" destination=""Market""/>\n\
                <fixr:flow name=""Executions"" source=""Market"" destination=""BuySide""/>\n\
            </fixr:actors>";
        let actors = loadOrchestraFragment<Actors> text
        //Assert.AreEqual("SYNOPSIS", documentation.Purpose)
        Assert.IsTrue(true)


