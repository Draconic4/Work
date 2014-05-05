Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VWContractValidation.ValidationLib

<TestClass()> Public Class TestProcessInfo
    <TestMethod()> Public Sub TestDefaultConstructor()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object)
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = cRun}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("US", tst.Country, True)
        Assert.AreEqual("MN", tst.StateOrProvinceExecution, True)
        Assert.AreEqual("INDIVIDUAL", tst.ApplicationType, True)
        Assert.AreEqual(0, tst.DealType)
        Assert.AreEqual("Retail", tst.FinanceType, True)
        Assert.AreEqual("Simple Interest Method", tst.ProductType, True)
    End Sub
    <TestMethod()> Public Sub TestCountry()
        Dim bArgs As New BaseConstructionArgs With {.PreviousRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}, .CurrentRun = New Dictionary(Of String, Object)}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("CA", tst.Country, True)
        bArgs.PreviousRun = New Dictionary(Of String, Object)
        bArgs.CurrentRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("CA", tst2.Country, True)
        bArgs.PreviousRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}}
        bArgs.CurrentRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}
        Dim tst3 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("CA", tst3.Country, True)
    End Sub
    <TestMethod()> Public Sub TestStateOrProvinceExecution()
        Dim bArgs As New BaseConstructionArgs With {.PreviousRun = New Dictionary(Of String, Object) From {{"VW_EXECUTIONSTATEORPROVINCE", "AB"}}, .CurrentRun = New Dictionary(Of String, Object)}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("AB", tst.StateOrProvinceExecution, True)
        bArgs.PreviousRun = New Dictionary(Of String, Object)
        bArgs.CurrentRun = New Dictionary(Of String, Object) From {{"VW_EXECUTIONSTATEORPROVINCE", "AB"}}
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("MN", tst2.StateOrProvinceExecution, True)
    End Sub
    <TestMethod()> Public Sub TestApplicationType()
        Dim bArgs As New BaseConstructionArgs With {.PreviousRun = New Dictionary(Of String, Object) From {{"", ""}}, .CurrentRun = New Dictionary(Of String, Object) From {{"COBUYER1_CODE", "1234"}}}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("INDIVIDUAL AND COAPPLICANT", tst.ApplicationType, True)
        bArgs.CurrentRun.Add("COBUYER2_CODE", "2345")
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("INDIVIDUAL AND TWO COAPPLICANTS", tst2.ApplicationType, True)
        bArgs.CurrentRun.Add("BUY_ISBUSINESS", True)
        Dim tst3 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("BUSINESS WITH TWO COAPPLICANTS", tst3.ApplicationType, True)
        bArgs.CurrentRun("COBUYER2_CODE") = ""
        Dim tst4 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("BUSINESS WITH COAPPLICANT", tst4.ApplicationType, True)
    End Sub
    <TestMethod()> Public Sub TestFinanceType()
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = New Dictionary(Of String, Object) From {{"DEAL_BALOON", 6000D}}, .PreviousRun = New Dictionary(Of String, Object)}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("Balloon", tst.FinanceType, True)
        bArgs.CurrentRun.Add("DEAL_FCALC", 1)
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("Lease", tst2.FinanceType, True)
    End Sub
    <TestMethod()> Public Sub TestProductType()
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = New Dictionary(Of String, Object) From {{"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}},
                                                    .PreviousRun = New Dictionary(Of String, Object)}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("Simple Interest Method", tst.ProductType, True)
        bArgs.CurrentRun.Add("DEAL_FCALC", 1)
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("Single", tst2.ProductType, True)
        bArgs.CurrentRun("DEAL_AMORTTERM") = 60
        Dim tst3 As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Assert.AreEqual("Monthly", tst3.ProductType, True)
    End Sub
    <TestMethod()> Public Sub TestReplicateCurrentState()
        Dim bArgs As New BaseConstructionArgs With {.PreviousRun = New Dictionary(Of String, Object),
                                                    .CurrentRun = New Dictionary(Of String, Object) From {{"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}}}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Dim outRun As New Dictionary(Of String, Object)
        tst.ReplicateCurrentState(outRun)
        Assert.AreNotEqual(outRun.Count, 0)
    End Sub
    <TestMethod()> Public Sub TestBusinessRules()
        Dim bArgs As New BaseConstructionArgs With {.PreviousRun = New Dictionary(Of String, Object),
                                                    .CurrentRun = New Dictionary(Of String, Object) From {{"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}}}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(bArgs)
        Dim outRun As New Dictionary(Of String, Object)
        tst.CheckRules()
        Dim validationRule As New VWContractValidation.Rule(New VWContractValidation.ValidationFormat With {.Message = "Hammer", .BottomLayer = False})
        tst.Requirement(Nothing, validationRule)
        Assert.AreEqual("Hammer", validationRule.Name)
        Assert.AreEqual(4, validationRule.Rules(0).Rules.Count)
        Assert.AreEqual("Validation Error - Credit Application must be completed successfully before Contract Validation.", validationRule.Rules(0).Rules(0).Name)
        Assert.AreEqual("Validation Error - Dealer Code must be assigned before Contract Validation.", validationRule.Rules(0).Rules(1).Name)
        Assert.AreEqual("Validation Warning - ContractFormID may not be required.", validationRule.Rules(0).Rules(2).Name)
        Assert.AreEqual("Validation Warning - ContractFormRevision may not be required.", validationRule.Rules(0).Rules(3).Name)
        tst.ContractFormID = "134547"
        tst.CheckRules()
        validationRule.Clear()
        tst.Requirement(Nothing, validationRule)
        Assert.AreEqual(3, validationRule.Rules(0).Rules.Count)
    End Sub
End Class