namespace FSharp.Fix.Repository.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Repository.Xml.Field

[<TestClass>]
type TestXmlField () =

    [<TestMethod>]
    member this.TestLoadFields () =
        let fields = loadFields "TestRepository/FIX.4.0"
        Assert.AreEqual(5, fields.Length)
        let item = fields |> Seq.head
        Assert.AreEqual(1, item.Tag)
        Assert.AreEqual("Account", item.Name)
        Assert.AreEqual("Account mnemonic as agreed between broker and institution.", item.Description)
  