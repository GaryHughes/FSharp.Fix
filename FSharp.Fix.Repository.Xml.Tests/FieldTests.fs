namespace FSharp.Fix.Repository.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Fix.Repository.Xml

[<TestClass>]
type TestXmlField () =

    [<TestMethod>]
    member this.TestLoadFields () =
        let fields = loadFields "TestRepository/FIX.4.0"
        Assert.AreEqual(92, fields.Length)
        let item = fields |> Seq.head
        Assert.AreEqual(1, item.Tag)
        Assert.AreEqual("Account", item.Name)
        Assert.AreEqual("Account mnemonic as agreed between buy and sell sides, e.g. broker and institution or investor/intermediary and fund manager.", item.Description)
  