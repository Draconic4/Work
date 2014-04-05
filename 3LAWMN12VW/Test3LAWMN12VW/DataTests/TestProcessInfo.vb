Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports _3LAWMN12VW

<TestClass()> Public Class TestProcessInfo
    Public Shared Sub GenerateDictionaryEntry(d As Dictionary(Of String, Object), ByVal key As String, ByVal val As Object)
        If val IsNot Nothing Then
            If d.ContainsKey(key) Then
                d(key) = val
            Else
                d.Add(key, val)
            End If
        Else
            If d.ContainsKey(key) Then d.Remove(key)
        End If
    End Sub
    Private Const COUNTRYKEY As String = "DLR_COUNTRY"
    Public Shared Sub GenerateDefaultCountry(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, COUNTRYKEY, Nothing)
    End Sub
    Public Shared Sub GenerateCountryUS(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, COUNTRYKEY, "US")
    End Sub
    Public Shared Sub GenerateCountryCanada(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, COUNTRYKEY, "CANADA")
    End Sub
    Private Const DEALKEY As String = "DEAL_FCALC"
    Private Const DEALBALLOON As String = "DEAL_BALOON"
    Public Shared Sub GenerateDefaultDealType(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALKEY, Nothing)
        GenerateDictionaryEntry(d, DEALBALLOON, Nothing)
    End Sub
    Public Shared Sub GenerateFinanceDealType(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALKEY, "0")
        GenerateDictionaryEntry(d, DEALBALLOON, 0D)
    End Sub
    Public Shared Sub GenerateLease(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALKEY, "M")
        GenerateDictionaryEntry(d, DEALBALLOON, 0D)
    End Sub
    Public Shared Sub GenerateBalloon(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALKEY, "0")
        GenerateDictionaryEntry(d, DEALBALLOON, 1234D)
    End Sub
    Private Const DEALTERM As String = "DEAL_TERM"
    Private Const AMORTTERM As String = "DEAL_AMORTTERM"
    Public Shared Sub GenerateDefaultFinanceType(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALTERM, Nothing)
        GenerateDictionaryEntry(d, AMORTTERM, Nothing)
    End Sub
    Public Shared Sub GenerateEvenTerm(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALTERM, 60)
        GenerateDictionaryEntry(d, AMORTTERM, 60)
    End Sub
    Public Shared Sub GenerateUnEvenTerm1(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALTERM, 60)
        GenerateDictionaryEntry(d, AMORTTERM, 1)
    End Sub
    Public Shared Sub GenerateUnEvenTerm2(d As Dictionary(Of String, Object))
        GenerateDictionaryEntry(d, DEALTERM, 1)
        GenerateDictionaryEntry(d, AMORTTERM, 60)
    End Sub
#Region "  Generate Deal Types "
    Public Shared Function GenerateDefaultAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateDefaultCountry(d)
        GenerateDefaultDealType(d)
        GenerateDefaultFinanceType(d)
        Return d
    End Function
    Public Shared Function GenerateUSFinanceAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryUS(d)
        GenerateFinanceDealType(d)
        GenerateEvenTerm(d)
        Return d
    End Function
    Public Shared Function GenerateUSBalloonAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryUS(d)
        GenerateBalloon(d)
        GenerateUnEvenTerm1(d)
        Return d
    End Function
    Public Shared Function GenerateUSMonthlyLeaseAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryUS(d)
        GenerateLease(d)
        GenerateEvenTerm(d)
        Return d
    End Function
    Public Shared Function GenerateUSSingleLeaseAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryUS(d)
        GenerateLease(d)
        GenerateUnEvenTerm1(d)
        Return d
    End Function
    Public Shared Function GenerateCanadaFinanceAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryCanada(d)
        GenerateFinanceDealType(d)
        GenerateEvenTerm(d)
        Return d
    End Function
    Public Shared Function GenerateCanadaBalloonAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryCanada(d)
        GenerateBalloon(d)
        GenerateEvenTerm(d)
        Return d
    End Function
    Public Shared Function GenerateCanadaMonthlyLeaseAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryCanada(d)
        GenerateLease(d)
        GenerateEvenTerm(d)
        Return d
    End Function
    Public Shared Function GenerateCanadaSingleLeaseAristo() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryCanada(d)
        GenerateLease(d)
        GenerateUnEvenTerm1(d)
        Return d
    End Function
    Public Shared Function GenerateAristoData() As Dictionary(Of String, Object)
        Return New Dictionary(Of String, Object)
    End Function
#End Region
    
    <TestMethod()> Public Sub TestConstruction_Default()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        pi.Populate(GenerateDefaultAristo)
        Assert.AreEqual("INDIVIDUAL", pi.ApplicationType)
        Assert.AreEqual("US", pi.Country)
        Assert.AreEqual(ProcessInfo.FINANCE_TYPE_RETAIL, pi.FinanceType)
        Assert.AreEqual(ProcessInfo.PRODUCT_TYPE_SIMPLE_INTEREST, pi.ProductType)
    End Sub
    <TestMethod()> Public Sub TestConstruction_US()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryUS(d)
        pi.Populate(d)
        Assert.AreEqual("US", pi.Country)
    End Sub
    <TestMethod()> Public Sub TestConstruction_Canada()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        Dim d As New Dictionary(Of String, Object)
        GenerateCountryCanada(d)
        pi.Populate(d)
        Assert.AreEqual("CA", pi.Country)
    End Sub
    <TestMethod()> Public Sub TestConstruction_USFinance()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        pi.Populate(GenerateUSFinanceAristo)
        Assert.AreEqual(ProcessInfo.FINANCE_TYPE_RETAIL, pi.FinanceType)
        Assert.AreEqual(ProcessInfo.PRODUCT_TYPE_SIMPLE_INTEREST, pi.ProductType)
    End Sub
    <TestMethod()> Public Sub TestConstruction_USBalloon()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        pi.Populate(GenerateUSBalloonAristo)
        Assert.AreEqual(ProcessInfo.FINANCE_TYPE_BALLOON, pi.FinanceType)
        Assert.AreEqual(ProcessInfo.PRODUCT_TYPE_SIMPLE_INTEREST, pi.ProductType)
    End Sub
    <TestMethod()> Public Sub TestConstruction_USMonthlyLease()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        pi.Populate(GenerateUSMonthlyLeaseAristo)
        Assert.AreEqual(ProcessInfo.FINANCE_TYPE_LEASE, pi.FinanceType)
        Assert.AreEqual(ProcessInfo.PRODUCT_TYPE_MONTHLY, pi.ProductType)
    End Sub
    <TestMethod()> Public Sub TestConstruction_USSingleLease()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        pi.Populate(GenerateUSSingleLeaseAristo)
        Assert.AreEqual(ProcessInfo.FINANCE_TYPE_LEASE, pi.FinanceType)
        Assert.AreEqual(ProcessInfo.PRODUCT_TYPE_SINGLE, pi.ProductType)
    End Sub
    <TestMethod()> Public Sub TestApplicationType_Change()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        pi.Populate(GenerateUSFinanceAristo)
        pi.ApplicationType = "BUSINESS"
        Assert.AreEqual("BUSINESS", pi.ApplicationType, True)
        Dim d As Dictionary(Of String, Object) = pi.Serialize()
        Assert.AreEqual("BUSINESS", d("DEAL_APPLICANTTYPE"), True)
        pi.ApplicationType = "INDIVIDUAL"
        pi.Populate(d)
        Assert.AreEqual("BUSINESS", pi.ApplicationType, True)
    End Sub
    '<TestMethod()> Public Sub TestPopulateDealType()
    '    Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
    '    pi.PopulateDealType(Nothing) 'DEFAULT FINANCE
    '    Assert.AreEqual(pi.DealType, "FINANCE", True)
    '    Dim d As New Dictionary(Of String, Object)
    '    d.Add("DEAL_FCALC", "0")
    '    d.Add("DEAL_BALOON", 0D)
    '    pi.PopulateDealType(d) ' FINANCE
    '    Assert.AreEqual(pi.DealType, "FINANCE", True)
    '    d("DEAL_FCALC") = "0"
    '    d("DEAL_BALOON") = 12345D
    '    pi.PopulateDealType(d) ' BALLOON
    '    Assert.AreEqual(pi.DealType, "BALLOON", True)
    '    d("DEAL_FCALC") = "M"
    '    d("DEAL_BALOON") = 0D
    '    pi.PopulateDealType(d) ' LEASE
    '    Assert.AreEqual(pi.DealType, "LEASE", True)
    '    d("DEAL_FCALC") = "S"
    '    d("DEAL_BALOON") = 12345D
    '    pi.PopulateDealType(d) ' LEASE BALLOON MAKES NO DIFFERENCE
    '    Assert.AreEqual(pi.DealType, "LEASE", True)
    'End Sub
    '<TestMethod()> Public Sub TestPopulateProductType()
    '    Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
    '    pi.Populate(Nothing) 'DEFAULT SIMPLE INTEREST METHOD
    '    Assert.AreEqual(pi.ProductType, "SIMPLE INTEREST METHOD", True)
    '    Dim d As New Dictionary(Of String, Object)
    '    d.Add()
    '    d.Add("DEAL_TERM", 60)
    '    d.Add("DEAL_AMORTTERM", 60)
    '    pi.PopulateProductType(d) ' SIMPLE INTEREST METHOD
    '    Assert.AreEqual(pi.ProductType, "SIMPLE INTEREST METHOD", True)
    '    pi.DealType = "LEASE"
    '    d("DEAL_TERM") = 60
    '    d("DEAL_AMORTTERM") = 1
    '    pi.PopulateProductType(d) 'SINGLE PAYMENT (CA)/PRE PAYMENT (US) LEASE
    '    Assert.AreEqual(pi.ProductType, "SINGLE", True)
    '    pi.DealType = "LEASE"
    '    d("DEAL_TERM") = 1
    '    d("DEAL_AMORTTERM") = 60
    '    pi.PopulateProductType(d) 'SINGLE PAYMENT (CA)/PRE PAYMENT (US) LEASE
    '    Assert.AreEqual(pi.ProductType, "SINGLE", True)
    '    pi.DealType = "LEASE"
    '    d("DEAL_TERM") = 60
    '    d("DEAL_AMORTTERM") = 60
    '    pi.PopulateProductType(d) 'MONTHLY PAYMENT LEASE
    '    Assert.AreEqual(pi.ProductType, "MONTHLY", True)
    '    pi.DealType = "BALLOON"
    '    d("DEAL_TERM") = 60
    '    d("DEAL_AMORTTERM") = 60
    '    pi.PopulateProductType(d) 'SIMPLE INTEREST METHOD
    '    Assert.AreEqual(pi.ProductType, "SIMPLE INTEREST METHOD", True)
    'End Sub
    '<TestMethod()> Public Sub TestPopulateCountry()
    '    Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
    '    pi.PopulateCountry(Nothing)
    '    Assert.AreEqual("US", pi.Country)
    '    Dim d As New Dictionary(Of String, Object)
    '    d.Add("DLR_COUNTRY", "CANADA")
    '    pi.PopulateCountry(d)
    '    Assert.AreEqual("CA", pi.Country)
    '    d("DLR_COUNTRY") = "UNITED STATES"
    '    pi.PopulateCountry(d)
    '    Assert.AreEqual("US", pi.Country)
    '    d("DLR_COUNTRY") = "US"
    '    pi.PopulateCountry(d)
    '    Assert.AreEqual("US", pi.Country)
    '    d("DLR_COUNTRY") = "CA"
    '    pi.PopulateCountry(d)
    '    Assert.AreEqual("CA", pi.Country)
    'End Sub
    '<TestMethod()> Public Sub TestIsCanadian()
    '    Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
    '    pi.Populate(GenerateSecondRunData)
    '    Assert.IsTrue(pi.IsCanadian)
    '    Dim pi2 As ProcessInfo = ProcessInfo.FetchExisting()
    '    Assert.IsFalse(pi2.IsCanadian)
    'End Sub
    '<TestMethod()> Public Sub TestIsLease()
    '    Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
    '    pi.Populate(GenerateSecondRunData)
    '    Assert.IsTrue(pi.IsLease)
    '    Dim pi2 As ProcessInfo = ProcessInfo.FetchExisting()
    '    pi2.Populate(GenerateAristoData)
    '    Assert.IsFalse(pi2.IsLease)
    'End Sub
    '<TestMethod()> Public Sub TestAristoToSerialization()
    '    Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
    '    Dim aristoD As New Dictionary(Of String, Object)
    '    aristoD.Add("DLR_COUNTRY", "CANADA") 'Standardized and Replicated
    '    aristoD.Add("DEAL_FCALC", "0") 'Replicated
    '    aristoD.Add("DEAL_BALOON", 0D)
    '    aristoD.Add("DEAL_TERM", 60)
    '    aristoD.Add("DEAL_AMORTTERM", 60)
    '    pi.Populate(aristoD)
    '    Dim d As Dictionary(Of String, Object) = pi.Serialize()
    '    Assert.AreEqual(d("DLR_COUNTRY"), "CA", True)
    '    Assert.AreEqual(d("DEAL_FCALC"), "0", True)
    '    Assert.AreEqual(d("DEAL_APPLICANTTYPE"), "INDIVIDUAL", True)
    'End Sub
    '<TestMethod()> Public Sub TestSecondRunDataSerialization()
    '    Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
    '    Dim d As New Dictionary(Of String, Object)
    '    d.Add("DLR_COUNTRY", "CA")
    '    d.Add("DEAL_APPLICANTTYPE", "BUSINESS")
    '    d.Add("DEAL_FCALC", "M") 'Replicated
    '    d.Add("DEAL_BALOON", 0D)
    '    d.Add("DEAL_TERM", 60)
    '    d.Add("DEAL_AMORTTERM", 60)
    '    pi.Populate(d)
    '    Dim sd As Dictionary(Of String, Object) = pi.Serialize()
    '    Assert.AreEqual(d("DEAL_APPLICANTTYPE"), "BUSINESS", True)
    '    Assert.AreEqual(d("DEAL_FCALC"), "M", True)
    'End Sub
End Class
