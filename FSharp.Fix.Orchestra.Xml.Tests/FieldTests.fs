namespace FSharp.Fix.Orchestra.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestField () =

    [<TestMethod>]
    member this.TestLoadSection () =
        let text = 
            @"<fixr:fields latestEP=""95"" xmlns:fixr=""http://fixprotocol.io/2016/fixrepository"">\n\
                <fixr:field id=""1"" name=""Account"" type=""String"" added=""FIX.2.7"" abbrName=""Acct"">\n\
                    <fixr:annotation>\n\
                        <fixr:documentation purpose=""SYNOPSIS"">\n\
                            Account mnemonic as agreed between buy and sell sides, e.g. broker and institution or investor/intermediary and fund manager.\n\
                        </fixr:documentation>\n\
                    </fixr:annotation>\n\
                </fixr:field>\n\
            </fixr:fields>";
        let fields = loadOrchestraFragment<Fields> text
        let field = fields.Fields.[0]
        Assert.AreEqual("Account", field.Name)
