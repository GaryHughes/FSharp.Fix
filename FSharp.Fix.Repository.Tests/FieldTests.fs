namespace FSharp.Fix.Repository.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open FSharp.Fix

type repo = Repository< @"TestRepository" >

[<TestClass>]
type TestFieldNameIndexesArePadded () =

    [<TestMethod>]
    member this.TestFieldIndexesArePadded () =
        Assert.AreEqual(String.Empty, repo.FIX_4_0.nameOfFieldWithTag 0)
        Assert.AreEqual("Account", repo.FIX_4_0.nameOfFieldWithTag 1)
        Assert.AreEqual("IOIID", repo.FIX_4_0.nameOfFieldWithTag 23)
        Assert.AreEqual(String.Empty, repo.FIX_4_0.nameOfFieldWithTag 24)
        Assert.AreEqual("IOIQltyInd", repo.FIX_4_0.nameOfFieldWithTag 25)
        Assert.AreEqual("ExDestination", repo.FIX_4_0.nameOfFieldWithTag 100)
