namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestMessage () =

    [<TestMethod>]
    member this.TestLoadMessage () =
        let text = 
            @"<fixr:messages latestEP=""97"" xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                <fixr:message name=""Heartbeat"" id=""1"" msgType=""0"" category=""Session"" added=""FIX.2.7"" abbrName=""Heartbeat"">\n\
                    <fixr:structure>\n\
                        <fixr:componentRef id=""1024"" presence=""required"" added=""FIX.2.7"">\n\
                            <fixr:annotation>\n\
                                <fixr:documentation>\n\
                                    MsgType = 0\n\
                                </fixr:documentation>\n\
                            </fixr:annotation>\n\
                        </fixr:componentRef>\n\
                        <fixr:fieldRef id=""112"" added=""FIX.4.0"">\n\
                            <fixr:annotation>\n\
                                <fixr:documentation>\n\
                                    Required when the heartbeat is the result of a Test Request message.\n\
                                </fixr:documentation>\n\
                            </fixr:annotation>\n\
                        </fixr:fieldRef>\n\
                    </fixr:structure>\n\
                    <fixr:annotation>\n\
                        <fixr:documentation purpose=""SYNOPSIS"">\n\
                            The Heartbeat monitors the status of the communication link and identifies when the last of a string of messages was not received.\n\
                        </fixr:documentation>\n\
                    </fixr:annotation>\n\
                </fixr:message>\n\
            </fixr:messages>";
        let messages = loadOrchestraFragment<Messages> text
        let message = messages.Message.[0]
        Assert.AreEqual("Heartbeat", message.Name)
