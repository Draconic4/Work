Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VWContractValidationPrompt.ValidationRuleData

<TestClass()> Public Class TestAddress

    <TestMethod()> Public Sub TestDefaultConstructor()
        Dim appArgs As New VWContractValidationPrompt.ApplicantArgs With {.globalProperty = VWContractValidationPrompt.ProcessInfo.Fetch(Nothing, Nothing), .customer = True, .applicantType = "Primary", .previousRun = Nothing, .currentRun = Nothing}
        Dim addr As Address = Address.Fetch("Home Address", appArgs)
        Assert.AreEqual("Primary", addr.ApplicantType)
        Assert.AreEqual("Home Address", addr.AddressType)
        Assert.AreEqual(True, addr.SameAsHomeAddress)
        Assert.AreEqual(String.Empty, addr.Line1)
        Assert.AreEqual(String.Empty, addr.Line2)
        Assert.AreEqual(String.Empty, addr.City)
        Assert.AreEqual(String.Empty, addr.State)
        Assert.AreEqual(String.Empty, addr.Zip)
        Assert.AreEqual(String.Empty, addr.County)
        
        addr.CheckRules()
        Dim v As New VWContractValidationPrompt.Rule("TestAddress", False)
        addr.Requirement(v)
        Assert.AreEqual("Validation Error - Street Address is required.", v.Rules(0).Rules(1).Name)
        Assert.AreEqual("Validation Error - City is required.", v.Rules(0).Rules(0).Name)
        Assert.AreEqual("Validation Error - State is required.", v.Rules(0).Rules(2).Name)
        Assert.AreEqual("Validation Error - Requires Valid Zip Code Format ##### Or #####-####.", v.Rules(0).Rules(3).Name)

        Dim d As New Dictionary(Of String, Object)
        addr.ReplicateCurrentState(d)
        Assert.AreEqual(True, d("BUY_SAMEASHOME"))
        Assert.AreEqual(String.Empty, d("BUY_ADDR"), True)
        Assert.AreEqual(String.Empty, d("BUY_ADDR2"), True)
        Assert.AreEqual(String.Empty, d("BUY_CITY"), True)
        Assert.AreEqual(String.Empty, d("BUY_PROV"), True)
        Assert.AreEqual(String.Empty, d("BUY_PCODE"), True)
        Assert.AreEqual(String.Empty, d("BUY_COUNTY"), True)
    End Sub
    <TestMethod()> Public Sub TestGarageConstructor()

        Dim appArgs As New VWContractValidationPrompt.ApplicantArgs With {.globalProperty = VWContractValidationPrompt.ProcessInfo.Fetch(Nothing, New Dictionary(Of String, Object) From {{"DEAL_FCALC", 1}}), .customer = True, .applicantType = "Primary", .previousRun = Nothing, .currentRun = Nothing}
        Dim addr As Address = Address.Fetch("Garage Address", appArgs)
        Assert.AreEqual("Primary", addr.ApplicantType)
        Assert.AreEqual("Garage Address", addr.AddressType)
        Assert.AreEqual(True, addr.SameAsHomeAddress)
        Assert.AreEqual(String.Empty, addr.Line1)
        Assert.AreEqual(String.Empty, addr.Line2)
        Assert.AreEqual(String.Empty, addr.City)
        Assert.AreEqual(String.Empty, addr.State)
        Assert.AreEqual(String.Empty, addr.Zip)
        Assert.AreEqual(String.Empty, addr.County)

        addr.SameAsHomeAddress = False
        addr.CheckRules()
        Dim v As New VWContractValidationPrompt.Rule("TestAddress", False)
        addr.Requirement(v)
        Assert.AreEqual("Validation Error - Garage Address is required for Leasing.", v.Rules(0).Rules(0).Name)
        Assert.AreEqual("Validation Error - Street Address is required.", v.Rules(0).Rules(2).Name)
        Assert.AreEqual("Validation Error - City is required.", v.Rules(0).Rules(1).Name)
        Assert.AreEqual("Validation Error - State is required.", v.Rules(0).Rules(3).Name)
        Assert.AreEqual("Validation Error - Requires Valid Zip Code Format ##### Or #####-####.", v.Rules(0).Rules(4).Name)

        Dim d As New Dictionary(Of String, Object)
        addr.ReplicateCurrentState(d)
        Assert.AreEqual(False, d("BUY_GARAGE_SAMEASHOME"))
        Assert.AreEqual(String.Empty, d("BUY_GARAGE_ADDR"), True)
        Assert.AreEqual(String.Empty, d("BUY_GARAGE_ADDR2"), True)
        Assert.AreEqual(String.Empty, d("BUY_GARAGE_CITY"), True)
        Assert.AreEqual(String.Empty, d("BUY_GARAGE_PROV"), True)
        Assert.AreEqual(String.Empty, d("BUY_GARAGE_PCODE"), True)
        Assert.AreEqual(String.Empty, d("BUY_GARAGE_COUNTY"), True)
    End Sub
End Class