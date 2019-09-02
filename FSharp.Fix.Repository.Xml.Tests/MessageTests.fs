namespace FSharp.Fix.Repository.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Fix.Repository.Xml

[<TestClass>]
type TestXmlMessage () =

    [<TestMethod>]
    member this.TestLoadMessages () =
        let messages = loadMessages "TestRepository/FIX.4.0"
        Assert.AreEqual(1, messages.Length)
        let item = messages |> Seq.head
        Assert.AreEqual("1", item.ComponentID)
        Assert.AreEqual("Heartbeat", item.Name)
        Assert.IsTrue(item.Description.StartsWith("The Heartbeat monitors"))
  
