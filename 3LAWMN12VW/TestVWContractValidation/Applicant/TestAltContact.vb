Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestAltContact

    <TestMethod()> Public Sub TestDefaultConstructor()
        Dim appArgs As New VWContractValidationPrompt.ApplicantArgs With {.globalProperty = VWContractValidationPrompt.ProcessInfo.Fetch(Nothing, Nothing), .customer = True, .applicantType = "Primary", .previousRun = Nothing, .currentRun = Nothing}
        Dim altContact As VWContractValidationPrompt.AltContact = VWContractValidationPrompt.AltContact.Fetch(appArgs)
        Assert.AreEqual("Primary", altContact.ApplicantType)
        Assert.AreEqual(String.Empty, altContact.HomePhone)
        Assert.AreEqual(String.Empty, altContact.WorkPhone)
        Assert.AreEqual(String.Empty, altContact.CellPhone)
        Assert.AreEqual(String.Empty, altContact.FaxPhone)
        Assert.AreEqual(String.Empty, altContact.EMail)
        altContact.CheckRules()
        Dim v As New VWContractValidationPrompt.Rule("TestAltContact", False)
        altContact.Requirement(v)
        Assert.AreEqual(v.Rules(0).Rules(0).Name, "Validation Error - US Contract requires one phone number")
        Dim d As New Dictionary(Of String, Object)
        altContact.ReplicateCurrentState(d)
        Assert.AreEqual(String.Empty, d("BUY_HOMEPHONE"), True)
        Assert.AreEqual(String.Empty, d("BUY_WORKPHONE"), True)
        Assert.AreEqual(String.Empty, d("BUY_CELLPHONE"), True)
        Assert.AreEqual(String.Empty, d("BUY_FAX"), True)
        Assert.AreEqual(String.Empty, d("BUY_EMAIL"), True)
    End Sub

End Class