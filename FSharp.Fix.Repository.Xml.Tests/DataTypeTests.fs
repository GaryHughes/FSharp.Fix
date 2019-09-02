namespace FSharp.Fix.Repository.Xml.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Fix.Repository.Xml

[<TestClass>]
type TestXmlDataType () =

    [<TestMethod>]
    member this.TestLoadDataTypes () =
        let dataTypes = loadDataTypes "TestRepository/FIX.4.0"
        Assert.AreEqual(4, dataTypes.Length)
        let item = dataTypes |> Seq.head
        Assert.AreEqual("int", item.Name)
        Assert.IsTrue(item.Description.StartsWith("Sequence of digits"))
   