namespace FSharp.Fix.Repository.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Repository.Xml.MsgContent

[<TestClass>]
type TestXmlMsgContent () =

    [<TestMethod>]
    member this.TestLoadMsgContents () =
        let msgContents = loadMsgContents "TestRepository/FIX.4.0"
        Assert.AreEqual(2, msgContents.Length)
        let item = msgContents |> Seq.head
        Assert.AreEqual("1", item.ComponentID)
        Assert.AreEqual("StandardHeader", item.TagText)
       