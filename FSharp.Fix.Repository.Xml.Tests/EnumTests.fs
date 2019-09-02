namespace FSharp.Fix.Repository.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Fix.Repository.Xml

[<TestClass>]
type TestXmlEnum () =

    [<TestMethod>]
    member this.TestLoadEnums () =
        let enums = loadEnums "TestRepository/FIX.4.0"
        Assert.AreEqual(7, enums.Length)
        let item = enums |> Seq.head
        Assert.AreEqual(4, item.Tag)
        Assert.AreEqual("B", item.Value)
        Assert.AreEqual("Buy", item.SymbolicName)
        Assert.AreEqual("Buy", item.Description)
   