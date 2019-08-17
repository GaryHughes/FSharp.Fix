namespace FSharp.Fix.Tests

open System
open System.IO
open System.Text
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type InputTests () =

    [<TestMethod>]
    member this.TestReadOneField () =
        let bytes = Encoding.ASCII.GetBytes("8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001");
        use stream = new MemoryStream(bytes)
        let field = FSharp.Fix.IO.readFieldFromStream stream
        Assert.AreEqual(8, field.Tag);
        Assert.AreEqual("FIX.4.4", field.Value)

    [<TestMethod>]
    member this.TestReadTwoFields () =
        let bytes = Encoding.ASCII.GetBytes("8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001");
        use stream = new MemoryStream(bytes)
        let fieldOne = FSharp.Fix.IO.readFieldFromStream stream
        Assert.AreEqual(8, fieldOne.Tag);
        Assert.AreEqual("FIX.4.4", fieldOne.Value)
        let fieldTwo = FSharp.Fix.IO.readFieldFromStream stream
        Assert.AreEqual(9, fieldTwo.Tag);
        Assert.AreEqual("72", fieldTwo.Value)

    [<TestMethod>]
    member this.TestReadMessage () =
        let bytes = Encoding.ASCII.GetBytes("8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001");
        use stream = new MemoryStream(bytes)
        match FSharp.Fix.IO.readMessageFromStream stream with
        | Ok message -> 
            Assert.AreEqual(10, message.Fields.Length)
            Assert.AreEqual(8, message.Fields.[0].Tag);
            Assert.AreEqual("INITIATOR", message.Fields.[4].Value)
        | Error message  -> Assert.IsTrue(false, message)

    [<TestMethod>]
    member this.TestReadTwoMessages () =
        let bytes = Encoding.ASCII.GetBytes("8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u00018=FIX.4.4\u00019=72\u000135=A\u000149=INITIATOR\u000156=ACCEPTOR\u000134=1\u000152=20190816-10:34:27.752\u000198=0\u0001108=30\u000110=013\u0001");
        use stream = new MemoryStream(bytes)
        match FSharp.Fix.IO.readMessageFromStream stream with
        | Ok message -> 
            Assert.AreEqual(10, message.Fields.Length)
            Assert.AreEqual(8, message.Fields.[0].Tag);
            Assert.AreEqual("INITIATOR", message.Fields.[4].Value)
        | Error message  -> Assert.IsTrue(false, message)
        match FSharp.Fix.IO.readMessageFromStream stream with
        | Ok message -> 
            Assert.AreEqual(10, message.Fields.Length)
            Assert.AreEqual(8, message.Fields.[0].Tag);
            Assert.AreEqual("ACCEPTOR", message.Fields.[4].Value)
        | Error message  -> Assert.IsTrue(false, message)

    [<TestMethod>]
    member this.TestReadMessageFromByteArray () =
        let data = Encoding.ASCII.GetBytes("8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001");
        match FSharp.Fix.IO.readMessageFromBytes data with
        | Ok message -> 
            Assert.AreEqual(10, message.Fields.Length)
            Assert.AreEqual(8, message.Fields.[0].Tag);
            Assert.AreEqual("INITIATOR", message.Fields.[4].Value)
        | Error message  -> Assert.IsTrue(false, message)

    [<TestMethod>]
    member this.TestReadMessageFromString () =
        let data = "8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001";
        match FSharp.Fix.IO.readMessageFromString data with
        | Ok message -> 
            Assert.AreEqual(10, message.Fields.Length)
            Assert.AreEqual(8, message.Fields.[0].Tag);
            Assert.AreEqual("INITIATOR", message.Fields.[4].Value)
        | Error message  -> Assert.IsTrue(false, message)

    [<TestMethod>]
    member this.TestReadMessageFromLogLine () =
        let data = "2019-08-17 13:00:00.000 8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001";
        match FSharp.Fix.IO.parseMessageFromLogLine data with
        | Ok message -> 
            Assert.AreEqual(10, message.Fields.Length)
            Assert.AreEqual(8, message.Fields.[0].Tag);
            Assert.AreEqual("INITIATOR", message.Fields.[4].Value)
        | Error message  -> Assert.IsTrue(false, message)

    [<TestMethod>]
    member this.TestReadMultipleMessageFromLog () =
        let data = "2019-08-17 13:00:00.000 8=FIX.4.4\u00019=72\u000135=A\u000149=ACCEPTOR\u000156=INITIATOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001\n\
                    2019-08-17 13:00:01.000 8=FIX.4.4\u00019=72\u000135=A\u000149=INITIATOR\u000156=ACCEPTOR\u000134=1\u000152=20190816-10:34:27.742\u000198=0\u0001108=30\u000110=012\u0001"
        use stream = new MemoryStream(Encoding.UTF8.GetBytes(data))
        let messages = FSharp.Fix.IO.parseMessagesFromLog stream |> Seq.toArray
        Assert.AreEqual(2, messages.Length)
        match messages.[0] with
        | Ok message -> Assert.AreEqual("ACCEPTOR", message.Fields.[3].Value)
        | Error _ -> Assert.IsTrue(false)
        match messages.[1] with
        | Ok message -> Assert.AreEqual("INITIATOR", message.Fields.[3].Value)
        | Error _ -> Assert.IsTrue(false)
