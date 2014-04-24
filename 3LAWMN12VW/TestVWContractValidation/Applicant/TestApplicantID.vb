Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VWContractValidationPrompt
<TestClass()> Public Class TestApplicantID

    <TestMethod()> Public Sub TestDefaultConstructor()
        Dim appArgs As New VWContractValidationPrompt.ApplicantArgs With {.globalProperty = VWContractValidationPrompt.ProcessInfo.Fetch(Nothing, Nothing), .customer = True, .applicantType = "Primary", .previousRun = Nothing, .currentRun = Nothing}
        Dim addr As ApplicantID = ApplicantID.Fetch(appArgs)
        Assert.AreEqual("Primary", addr.ApplicantType)
        Assert.AreEqual(String.Empty, addr.Family)
        Assert.AreEqual(String.Empty, addr.Middle)
        Assert.AreEqual(String.Empty, addr.Given)
        Assert.AreEqual(String.Empty, addr.Suffix)
        Assert.AreEqual(String.Empty, addr.BirthDate)
        Assert.AreEqual(-1, addr.Age)
        Assert.AreEqual(String.Empty, addr.NationalID)
        Assert.AreEqual(String.Empty, addr.IssuingState)
        Assert.AreEqual(String.Empty, addr.DriverLicense)
        Assert.AreEqual(String.Empty, addr.DriverLicenseExpiry)

        addr.CheckRules()
        Dim v As New VWContractValidationPrompt.Rule("TestAddress", False)
        addr.Requirement(v)
        Assert.AreEqual("Validation Error - Applicant must have a Last Name.", v.Rules(0).Rules(3).Name)
        Assert.AreEqual("Validation Error - Applicant must have a Given Name.", v.Rules(0).Rules(4).Name)
        Assert.AreEqual("Validation Error - Applicant must have BirthDate.", v.Rules(0).Rules(0).Name)
        Assert.AreEqual("Validation Error - Applicant must have a valid Drivers' License.", v.Rules(0).Rules(1).Name)
        Assert.AreEqual("Validation Error - Applicant must have a valid Drivers' License Expiry.", v.Rules(0).Rules(2).Name)
        Assert.AreEqual("Validation Error - Applicant must have a valid Social Insurance Number.", v.Rules(0).Rules(5).Name)

        Dim d As New Dictionary(Of String, Object)
        addr.ReplicateCurrentState(d)
        Assert.AreEqual(String.Empty, d("BUY_LAST"), True)
        Assert.AreEqual(String.Empty, d("BUY_INIT"), True)
        Assert.AreEqual(String.Empty, d("BUY_FIRST"), True)
        Assert.AreEqual(String.Empty, d("BUY_SUFFIX"), True)
        Assert.AreEqual(String.Empty, d("BUY_BDTE"), True)
        Assert.AreEqual(String.Empty, d("BUY_DLICENSESTATE"), True)
        Assert.AreEqual(String.Empty, d("BUY_DLICENSE"), True)
        Assert.AreEqual(String.Empty, d("BUY_DLICENSEEXP"), True)
        Assert.AreEqual(String.Empty, d("BUY_SIN"), True)
    End Sub

End Class