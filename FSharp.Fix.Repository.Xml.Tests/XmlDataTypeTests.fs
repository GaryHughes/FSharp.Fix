namespace FSharp.Fix.Repository.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Repository.Xml.DataType

[<TestClass>]
type TestXmlDataType () =

    [<TestMethod>]
    member this.TestLoadDataTypes () =
        let dataTypes = loadDataTypes "TestRepository/FIX.4.0"
        Assert.AreEqual(4, dataTypes.Length)
        let item = dataTypes |> Seq.head
        Assert.AreEqual("int", item.Name)
        Assert.IsTrue(item.Description.StartsWith("Sequence of digits"))
   