Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports _3LAWMN12VW.ValidationRuleData

<TestClass()> Public Class TestProcessInfo

    <TestMethod()> Public Sub TestConstruction_Default()
        Dim tstObj As ProcessInfo = ProcessInfo.Fetch(Nothing, Nothing)
        Assert.AreEqual("US", tstObj.Country, True)
        Assert.AreEqual("MN", tstObj.StateOrProvinceExecution, True)
        Assert.AreEqual(_3LAWMN12VW.Utility.C_APPTYPE_PRIM, tstObj.ApplicationType, True)
        Assert.AreEqual(_3LAWMN12VW.Utility.FINANCE_TYPE_RETAIL, tstObj.FinanceType, True)
        Assert.AreEqual(_3LAWMN12VW.Utility.PRODUCT_TYPE_SIMPLE_INTEREST, tstObj.ProductType, True)
    End Sub
    <TestMethod()> Public Sub TestApplicationList_PRIMARYAPPLICANT()
        Dim ata As New ProcessInfo.ApplicationTypeArgs(False, False, False)
        Dim l As List(Of String) = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(1, l.Count)
        Assert.AreEqual(_3LAWMN12VW.Utility.C_APPTYPE_PRIM, l(0)) 'Content Check
        ata.HasCoApplicantOrGuarantor = True
        l = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(3, l.Count)
        ata.HasCoApplicant2OrCoApplicantAndAlsoGuarantor = True
        l = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(4, l.Count)
        ata.HasCoApplicantOrGuarantor = False 'Set back.
        Assert.AreEqual(False, ata.HasCoApplicant2OrCoApplicantAndAlsoGuarantor)
        l = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(1, l.Count)
    End Sub
    <TestMethod()> Public Sub TestApplicationList_BUSINESSAPPLICANT()
        Dim ata As New ProcessInfo.ApplicationTypeArgs(True, False, False)
        Dim l As List(Of String) = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(1, l.Count)
        Assert.AreEqual(_3LAWMN12VW.Utility.C_APPTYPE_BUSINESS, l(0)) 'Content Check
        ata.HasCoApplicantOrGuarantor = True
        l = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(3, l.Count)
        ata.HasCoApplicant2OrCoApplicantAndAlsoGuarantor = True
        l = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(5, l.Count)
        ata.HasCoApplicantOrGuarantor = False
        Assert.AreEqual(False, ata.HasCoApplicant2OrCoApplicantAndAlsoGuarantor)
        l = ProcessInfo.GenerateApplicationTypeList(ata)
        Assert.AreEqual(1, l.Count)
    End Sub
    <TestMethod()> Public Sub TestApplicationRewrite()
        'Pass Through
        Dim itm As String = _3LAWMN12VW.Utility.C_APPTYPE_BUSINESSTWOCOAPP
        Dim ata As New ProcessInfo.ApplicationTypeArgs(True, True, True)
        Dim appList As List(Of String) = ProcessInfo.GenerateApplicationTypeList(ata)
        Dim rewriteItm As String = ProcessInfo.ValidateApplicationType(itm, appList, ata)
        Assert.AreEqual(itm, rewriteItm)

        'Removed CoApplicant 2
        ata.HasCoApplicant2OrCoApplicantAndAlsoGuarantor = False
        appList = ProcessInfo.GenerateApplicationTypeList(ata)
        rewriteItm = ProcessInfo.ValidateApplicationType(itm, appList, ata)
        Assert.AreEqual(_3LAWMN12VW.Utility.C_APPTYPE_BUSINESSCOAPP, rewriteItm)

        'Removed CoApplicant 1
        ata.HasCoApplicantOrGuarantor = False
        appList = ProcessInfo.GenerateApplicationTypeList(ata)
        rewriteItm = ProcessInfo.ValidateApplicationType(itm, appList, ata)
        Assert.AreEqual(_3LAWMN12VW.Utility.C_APPTYPE_BUSINESS, rewriteItm)

        'Changed Applicant Type (Business -> Primary)
        ata.IsBusiness = False
        appList = ProcessInfo.GenerateApplicationTypeList(ata)
        rewriteItm = ProcessInfo.ValidateApplicationType(itm, appList, ata)
        Assert.AreEqual(_3LAWMN12VW.Utility.C_APPTYPE_PRIM, rewriteItm)
    End Sub
    <TestMethod()> Public Sub Test_ExecutionState()
        Dim d As New Dictionary(Of String, Object)
        d.Add("DLR_COUNTRY", "US")
        d.Add("VW_EXECUTIONSTATEORPROVINCE", "MN") 'Set in a previous step
        Dim tstObj As ProcessInfo = ProcessInfo.Fetch(d, Nothing)
        Assert.AreEqual("MN", tstObj.StateOrProvinceExecution)
        d.Remove("VW_EXECUTIONSTATEORPROVINCE")
        d.Add("DLR_PROV", "AB")
        tstObj = ProcessInfo.Fetch(Nothing, d) 'Set by Aristo Data
        Assert.AreEqual("AB", tstObj.StateOrProvinceExecution)
        d.Add("VW_EXECUTIONSTATEORPROVINCE", "WA")
        tstObj = ProcessInfo.Fetch(d, d)
        Assert.AreEqual("WA", tstObj.StateOrProvinceExecution) ' Use Previous Step over Aristo
    End Sub
    <TestMethod()> Public Sub Test_FinanceType()
        Dim d As New Dictionary(Of String, Object)
        d.Add("DEAL_FCALC", 0)
        d.Add("VW_FINANCETYPE", _3LAWMN12VW.Utility.FINANCE_TYPE_RETAIL) 'Set in a previous step
        Dim tstObj As ProcessInfo = ProcessInfo.Fetch(d, Nothing)
        Assert.AreEqual(_3LAWMN12VW.Utility.FINANCE_TYPE_RETAIL, tstObj.FinanceType)
        d("DEAL_FCALC") = 1
        tstObj = ProcessInfo.Fetch(Nothing, d) 'Set by Aristo Data
        Assert.AreEqual(_3LAWMN12VW.Utility.FINANCE_TYPE_LEASE, tstObj.FinanceType)
        d("DEAL_FCALC") = 0
        d.Add("DEAL_BALOON", 100D)
        tstObj = ProcessInfo.Fetch(d, d)
        Assert.AreEqual(_3LAWMN12VW.Utility.FINANCE_TYPE_BALLOON, tstObj.FinanceType) ' Use Previous Step over Aristo
    End Sub
    <TestMethod()> Public Sub Test_ProductType()
        Dim d As New Dictionary(Of String, Object)
        d.Add("DEAL_FCALC", 0)
        d.Add("VW_PRODUCTTYPE", _3LAWMN12VW.Utility.PRODUCT_TYPE_SIMPLE_INTEREST) 'Set in a previous step
        Dim tstObj As ProcessInfo = ProcessInfo.Fetch(d, Nothing)
        Assert.AreEqual(_3LAWMN12VW.Utility.PRODUCT_TYPE_SIMPLE_INTEREST, tstObj.ProductType)
        d("DEAL_FCALC") = 1
        tstObj = ProcessInfo.Fetch(Nothing, d) 'Reset by Aristo Data
        Assert.AreEqual(_3LAWMN12VW.Utility.PRODUCT_TYPE_MONTHLY, tstObj.ProductType)
        d.Add("DEAL_TERM", 60)
        d.Add("DEAL_AMORTTERM", 30)
        tstObj = ProcessInfo.Fetch(d, d)
        Assert.AreEqual(_3LAWMN12VW.Utility.PRODUCT_TYPE_SINGLE, tstObj.ProductType) ' Reset by Aristo Data
    End Sub
End Class
