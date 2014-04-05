Imports Csla

Public Class ProcessInfo
    Inherits BusinessBase(Of ProcessInfo)

    Public Const FINANCE_TYPE_RETAIL As String = "Retail"
    Public Const FINANCE_TYPE_LEASE As String = "Lease"
    Public Const FINANCE_TYPE_BALLOON As String = "Balloon"
    Public Const PRODUCT_TYPE_SIMPLE_INTEREST As String = "Simple Interest Method"
    Public Const PRODUCT_TYPE_SINGLE As String = "Single"
    Public Const PRODUCT_TYPE_MONTHLY As String = "Monthly"

    Public _HasCoApplicantOrGuarantor As Boolean = False
    Public _HasCoApplicantOrCoApplicant2 As Boolean = False
    Public _HasCoApplicant2 As Boolean = False

    Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicationType), "DEAL_APPLICANTTYPE", "INDIVIDUAL")
    Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "DLR_COUNTRY", "US")
    Public Shared ReadOnly DealTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DealType), "DEAL_FCALC", "0")
    Public Shared ReadOnly FinanceTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.FinanceType), "DEAL_FINANCETYPE", FINANCE_TYPE_RETAIL)
    Public Shared ReadOnly ProductTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ProductType), "PRODUCT_TYPE", PRODUCT_TYPE_SIMPLE_INTEREST)

#Region "  Properties "
    Public Property ApplicationType As String
        Get
            Return GetProperty(ApplicationTypeProperty)
        End Get
        Set(value As String)
            SetProperty(ApplicationTypeProperty, value)
        End Set
    End Property
    Public ReadOnly Property Country As String
        Get
            Return GetProperty(CountryProperty)
        End Get
    End Property
    Public ReadOnly Property DealType As String
        Get
            Return GetProperty(DealTypeProperty)
        End Get
    End Property
    Public ReadOnly Property FinanceType As String
        Get
            Return GetProperty(FinanceTypeProperty)
        End Get
    End Property
    Public ReadOnly Property ProductType As String
        Get
            Return GetProperty(ProductTypeProperty)
        End Get
    End Property
#End Region

#Region "  Data Access "
    Public Sub New()
    End Sub
    Public Shared Function FetchExisting() As ProcessInfo
        Return New ProcessInfo
    End Function
    Public Sub Populate(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        PopulateField(ApplicationTypeProperty, d)
        PopulateDealFinanceTypes(d)
        PopulateProductType(d)
        PopulateCountry(d)
        PopulateCoApplicantGuarantorFlags(d)
    End Sub
    Public Sub PopulateDealFinanceTypes(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        PopulateField(DealTypeProperty, d)
        Dim dkey As String = Utility.SimplePopulateKey(FinanceTypeProperty)
        If DealType = "0" OrElse String.IsNullOrWhiteSpace(DealType) Then
            If d.ContainsKey("DEAL_BALOON") AndAlso d("DEAL_BALOON") <> 0 Then
                LoadProperty(FinanceTypeProperty, FINANCE_TYPE_BALLOON)
            Else
                LoadProperty(FinanceTypeProperty, FINANCE_TYPE_RETAIL)
            End If
        Else
            LoadProperty(FinanceTypeProperty, FINANCE_TYPE_LEASE)
        End If
    End Sub
    Public Sub PopulateProductType(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        Dim TERMKEY As String = "DEAL_TERM"
        Dim AMORTTERMKEY As String = "DEAL_AMORTTERM"
        Dim term As Integer = 1
        Dim amortTerm As Integer = 1
        If d.ContainsKey(TERMKEY) Then term = d(TERMKEY)
        If d.ContainsKey(AMORTTERMKEY) Then amortTerm = d(AMORTTERMKEY)
        If Utility.IsLease(Me) Then
            If term = amortTerm Then
                LoadProperty(ProductTypeProperty, PRODUCT_TYPE_MONTHLY)
            Else
                LoadProperty(ProductTypeProperty, PRODUCT_TYPE_SINGLE)
            End If
        Else
            LoadProperty(ProductTypeProperty, PRODUCT_TYPE_SIMPLE_INTEREST)
        End If
    End Sub
    Public Sub PopulateCountry(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        Dim cKey As String = Utility.SimplePopulateKey(CountryProperty)
        If d.ContainsKey(cKey) Then
            If d(cKey).ToString.StartsWith("CA") Then
                LoadProperty(CountryProperty, "CA")
            Else
                LoadProperty(CountryProperty, "US")
            End If
        End If
    End Sub
    Public Sub PopulateCoApplicantGuarantorFlags(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        Dim cob1OrGuarantorKey As String = "COBUYER1_CODE"
        Dim cob1OrCob2Key As String = "COBUYER2_CODE"
        Dim cob2Key As String = "COBUYER3_CODE"
        If d.ContainsKey(cob1OrGuarantorKey) Then _HasCoApplicantOrGuarantor = Not String.IsNullOrWhiteSpace(d(cob1OrGuarantorKey))
        If d.ContainsKey(cob1OrCob2Key) Then _HasCoApplicantOrCoApplicant2 = Not String.IsNullOrWhiteSpace(d(cob1OrCob2Key))
        If d.ContainsKey(cob2Key) Then _HasCoApplicant2 = Not String.IsNullOrWhiteSpace(d(cob2Key))
    End Sub
    Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal d As Dictionary(Of String, Object))
        Dim key As String = Utility.SimplePopulateKey(pi)
        If d.ContainsKey(key) Then LoadProperty(pi, d(key))
    End Sub
    Public Function Serialize() As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)

        d.Add(ApplicationTypeProperty.FriendlyName, ApplicationType)
        d.Add(CountryProperty.FriendlyName, Country)
        d.Add(DealTypeProperty.FriendlyName, DealType)
        d.Add(FinanceTypeProperty.FriendlyName, FinanceType)
        d.Add(ProductTypeProperty.FriendlyName, ProductType)

        Return d
    End Function
#End Region
End Class
