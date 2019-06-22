namespace FSharp.Fix.Repository.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Repository.Xml.Component

[<TestClass>]
type TestXmlComponent () =

    [<TestMethod>]
    member this.TestLoadComponents () =
        let components = loadComponents "TestRepository/FIX.4.0"
        Assert.AreEqual(2, components.Length)
        let item = components |> Seq.head
        Assert.AreEqual("1001", item.ComponentID)
        Assert.AreEqual("StandardHeader", item.Name)
       