Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VWContractValidation

<TestClass()> Public Class TestProcessInfo
    <TestMethod()> Public Sub TestDefaultConstructor()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object)
        Dim tst As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("US", tst.Country, True)
        Assert.AreEqual("MN", tst.StateOrProvinceExecution, True)
        Assert.AreEqual("INDIVIDUAL", tst.ApplicationType, True)
        Assert.AreEqual(0, tst.DealType)
        Assert.AreEqual("Retail", tst.FinanceType, True)
        Assert.AreEqual("Simple Interest Method", tst.ProductType, True)
    End Sub
    <TestMethod()> Public Sub TestCountry()
        Dim pRun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}
        Dim cRun As New Dictionary(Of String, Object)
        Dim tst As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("CA", tst.Country, True)
        Dim pRun2 As New Dictionary(Of String, Object)
        Dim cRun2 As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(pRun2, cRun2)
        Assert.AreEqual("CA", tst2.Country, True)
        Dim pRun3 As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}}
        Dim cRun3 As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}
        Dim tst3 As ProcessInfo = ProcessInfo.Fetch(pRun3, cRun3)
        Assert.AreEqual("CA", tst3.Country, True)
    End Sub
    <TestMethod()> Public Sub TestStateOrProvinceExecution()
        Dim pRun As New Dictionary(Of String, Object) From {{"VW_EXECUTIONSTATEORPROVINCE", "AB"}}
        Dim cRun As New Dictionary(Of String, Object)
        Dim tst As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("AB", tst.StateOrProvinceExecution, True)
        Dim pRun2 As New Dictionary(Of String, Object)
        Dim cRun2 As New Dictionary(Of String, Object) From {{"VW_EXECUTIONSTATEORPROVINCE", "AB"}}
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(pRun2, cRun2)
        Assert.AreEqual("MN", tst2.StateOrProvinceExecution, True)
    End Sub
    <TestMethod()> Public Sub TestApplicationType()
        Dim pRun As New Dictionary(Of String, Object) From {{"", ""}}
        Dim cRun As New Dictionary(Of String, Object) From {{"COBUYER1_CODE", "1234"}}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("INDIVIDUAL AND COAPPLICANT", tst.ApplicationType, True)
        cRun.Add("COBUYER2_CODE", "2345")
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("INDIVIDUAL AND TWO COAPPLICANTS", tst2.ApplicationType, True)
        cRun.Add("BUY_ISBUSINESS", True)
        Dim tst3 As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("BUSINESS WITH TWO COAPPLICANTS", tst3.ApplicationType, True)
        cRun("COBUYER2_CODE") = ""
        Dim tst4 As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("BUSINESS WITH COAPPLICANT", tst4.ApplicationType, True)
    End Sub
    <TestMethod()> Public Sub TestFinanceType()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object) From {{"DEAL_BALOON", 6000D}}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("Balloon", tst.FinanceType, True)
        cRun.Add("DEAL_FCALC", 1)
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("Lease", tst2.FinanceType, True)
    End Sub
    <TestMethod()> Public Sub TestProductType()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object) From {{"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}}
        Dim tst As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("Simple Interest Method", tst.ProductType, True)
        cRun.Add("DEAL_FCALC", 1)
        Dim tst2 As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("Single", tst2.ProductType, True)
        cRun("DEAL_AMORTTERM") = 60
        Dim tst3 As ProcessInfo = ProcessInfo.Fetch(pRun, cRun)
        Assert.AreEqual("Monthly", tst3.ProductType, True)
    End Sub
End Class