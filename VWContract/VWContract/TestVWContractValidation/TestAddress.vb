Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VWContractValidation.ValidationLib

<TestClass()> Public Class TestAddress

    <TestMethod()> Public Sub TestDefaultConstructor()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object)
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = cRun}
        Dim aArgs As New ApplicantArgs With {.GlobalProperty = ProcessInfo.Fetch(bArgs), .applicantType = ApplicantArgs.ApplicantTypes.BUYER1, .CurrentRun = cRun}
        Dim tst As Address = Address.Fetch("UNASSOCIATED", aArgs)
        Assert.AreEqual("US", tst.Country, True)
        Assert.AreEqual("MN", tst.State, True)
        Assert.AreEqual("UNASSOCIATED", tst.AddressType, True)
        Assert.AreEqual(ApplicantArgs.ApplicantTypes.BUYER1, tst.ApplicantType)
        Assert.AreEqual(String.Empty, tst.Line1, True)
        Assert.AreEqual(String.Empty, tst.Line2, True)
        Assert.AreEqual(String.Empty, tst.City, True)
        Assert.AreEqual(String.Empty, tst.County, True)
        Assert.AreEqual(False, tst.IsHomeAddress)
        Assert.AreEqual(True, tst.UseHomeAddress)
    End Sub

    <TestMethod()> Public Sub TestUseHomeAddress()
        Dim pRun As New Dictionary(Of String, Object) From {{"BUY_BILLING_SAMEASHOME", False}, {"BUY_BILLING_ADDR", "1234 EVERYWHERE ST."}}
        Dim cRun As New Dictionary(Of String, Object)
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = cRun, .PreviousRun = pRun}
        Dim aArgs As New ApplicantArgs With {.GlobalProperty = ProcessInfo.Fetch(bArgs), .applicantType = ApplicantArgs.ApplicantTypes.BUYER1, .CurrentRun = cRun, .PreviousRun = pRun}
        Dim tst As Address = Address.Fetch(Address.C_BILLINGADDRESS, aArgs)
        Assert.AreEqual(False, tst.UseHomeAddress)
        Assert.AreEqual("1234 EVERYWHERE ST.", tst.Line1, True)
    End Sub

    <TestMethod()> Public Sub TestSameAsHomeAddressOverride()
        Dim pRun As New Dictionary(Of String, Object) From {{"BUY_SAMEASHOME", True}, {"BUY_ADDR", "1234 EVERYWHERE ST."}}
        Dim cRun As New Dictionary(Of String, Object) From {{"BUY_ADDR", "4321 SOMEWHERE ELSE ST."}}
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = cRun, .PreviousRun = pRun}
        Dim aArgs As New ApplicantArgs With {.GlobalProperty = ProcessInfo.Fetch(bArgs), .applicantType = ApplicantArgs.ApplicantTypes.BUYER1, .CurrentRun = cRun, .PreviousRun = pRun}
        Dim tst As Address = Address.Fetch(Address.C_HOMEADDRESS, aArgs)
        Assert.AreEqual(True, tst.SameAsHomeAddress)
        Assert.AreEqual("4321 SOMEWHERE ELSE ST.", tst.Line1, True)
    End Sub

    <TestMethod()> Public Sub TestReplicateCurrentState()
        Dim pRun As New Dictionary(Of String, Object) From {{"BUY_BILLING_SAMEASHOME", True}, {"BUY_BILLING_ADDR", "1234 EVERYWHERE ST."}}
        Dim cRun As New Dictionary(Of String, Object) From {{"BUY_ADDR", "4321 SOMEWHERE ELSE ST."}}
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = cRun, .PreviousRun = pRun}
        Dim aArgs As New ApplicantArgs With {.GlobalProperty = ProcessInfo.Fetch(bArgs), .applicantType = ApplicantArgs.ApplicantTypes.BUYER1, .CurrentRun = cRun, .PreviousRun = pRun}
        Dim tst As Address = Address.Fetch(Address.C_BILLINGADDRESS, aArgs)
        Dim outRun As New Dictionary(Of String, Object)
        tst.ReplicateCurrentState(outRun)
        Assert.AreNotEqual(outRun.Count, 0)
    End Sub
End Class