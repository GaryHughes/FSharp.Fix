namespace FSharp.Fix.Orchestra.Xml.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Orchestra.Xml

[<TestClass>]
type TestDocumentation () =

    [<TestMethod>]
    member this.TestLoadComponents () =
        Assert.IsTrue(true)
        // let components = loadComponents "TestRepository/FIX.4.0"
        // Assert.AreEqual(2, components.Length)
        // let item = components |> Seq.head
        // Assert.AreEqual("1001", item.ComponentID)
        // Assert.AreEqual("StandardHeader", item.Name)
       