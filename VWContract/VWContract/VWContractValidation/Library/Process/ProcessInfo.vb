Imports Csla

Public Class ProcessInfo
    Inherits BaseFormField

    Private _ApplicationArgs As ApplicationTypeArgs

    Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("Country", "DLR_COUNTRY", "US"))
    Public Shared ReadOnly StateOrProvinceExecutionProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("StateOrProvinceExecution", "VW_EXECUTIONSTATEORPROVINCE", "MN"))
    Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("ApplicationType", "VW_APPLICANTTYPE", ProcessUtility.C_APPTYPE_PRIM))
    Public Shared ReadOnly DealTypeProperty As PropertyInfo(Of Integer) = RegisterProperty(Of Integer)(New PropertyInfo(Of Integer)("DealType", "DEAL_FCALC", 0))
    Public Shared ReadOnly FinanceTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("FinanceType", "VW_FINANCETYPE", ProcessUtility.FINANCE_TYPE_RETAIL))
    Public Shared ReadOnly ProductTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("ProductType", "VW_PRODUCTTYPE", ProcessUtility.PRODUCT_TYPE_SIMPLE_INTEREST))

#Region "  Properties "
    Public ReadOnly Property Country As String
        Get
            Return GetProperty(CountryProperty)
        End Get
    End Property
    Public Property StateOrProvinceExecution() As String
        Get
            Return GetProperty(StateOrProvinceExecutionProperty)
        End Get
        Set(ByVal value As String)
            SetProperty(StateOrProvinceExecutionProperty, value)
        End Set
    End Property

    Public Property ApplicationType As String
        Get
            Return GetProperty(ApplicationTypeProperty)
        End Get
        Set(value As String)
            SetProperty(ApplicationTypeProperty, value)
        End Set
    End Property
    Private _ApplicationTypeList As List(Of String)
    Public ReadOnly Property ApplicationTypeList As List(Of String)
        Get
            Return _ApplicationTypeList
        End Get
    End Property
    Public ReadOnly Property DealType() As Integer
        Get
            Return GetProperty(DealTypeProperty)
        End Get
    End Property
    Public ReadOnly Property FinanceType() As String
        Get
            Return GetProperty(FinanceTypeProperty)
        End Get
    End Property
    Public ReadOnly Property ProductType() As String
        Get
            Return GetProperty(ProductTypeProperty)
        End Get
    End Property
    Public ReadOnly Property HasCoAppFlg As Boolean
        Get
            If _ApplicationArgs Is Nothing Then Return False
            Return _ApplicationArgs.HasCoApplicantOrGuarantor
        End Get
    End Property
    Public ReadOnly Property HasCoApp2Flg As Boolean
        Get
            If _ApplicationArgs Is Nothing Then Return False
            Return _ApplicationArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor
        End Get
    End Property
#End Region

#Region "  Data Access "
    Public Sub New()
        MyBase.New(Nothing) 'Is the global properties base class!
    End Sub
    Public Shared Function Fetch(ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As ProcessInfo
        Dim pi As New ProcessInfo
        FormFieldManager.Populate(pi, previousRun, currentRun)
        Return pi
    End Function
    Public Overrides Sub Populate(ByVal pRun As Dictionary(Of String, Object))
        PopulateField(pRun, CountryProperty)
        PopulateField(pRun, StateOrProvinceExecutionProperty)
        PopulateField(pRun, ApplicationTypeProperty) 'Created on subsequent runs
        PopulateField(pRun, FinanceTypeProperty)
        PopulateField(pRun, ProductTypeProperty)
    End Sub
    Public Overrides Sub PopulateOverride(ByVal cRun As Dictionary(Of String, Object))
        PopulateField(cRun, CountryProperty)
        FormatCountry()
        PopulateField(cRun, DealTypeProperty)
    End Sub
    Public Overrides Sub Calculate(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
        CalculateApplicationType(pRun, cRun)
        CalculateFinanceType(pRun, cRun)
        CalculateProductType(pRun, cRun)
        CalculateStateOrProvinceExecution(cRun)
    End Sub
    Private Sub CalculateApplicationType(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
        If cRun Is Nothing Then Exit Sub
        Dim businessFlg As Boolean = False
        cRun.TryGetValue("BUY_ISBUSINESS", businessFlg)
        Dim cobuyerCode As String = String.Empty
        Dim hasCoAppFlg As Boolean = cRun.TryGetValue("COBUYER1_CODE", cobuyerCode) AndAlso Not String.IsNullOrWhiteSpace(cobuyerCode)
        Dim hasCoApp2Flg As Boolean = False
        If hasCoAppFlg Then
            Dim cobuyer2Code As String = String.Empty
            hasCoApp2Flg = cRun.TryGetValue("COBUYER2_CODE", cobuyer2Code) AndAlso Not String.IsNullOrWhiteSpace(cobuyer2Code)
        End If
        _ApplicationArgs = New ApplicationTypeArgs(businessFlg, hasCoAppFlg, HasCoApp2Flg)
        _ApplicationTypeList = GenerateApplicationTypeList(_ApplicationArgs)
        ApplicationType = ValidateApplicationType(ApplicationType, _ApplicationTypeList, _ApplicationArgs)
    End Sub
    Private Sub CalculateFinanceType(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
        If Not ProcessUtility.IsLease(Me) Then
            If cRun.ContainsKey("DEAL_BALOON") AndAlso cRun("DEAL_BALOON") <> 0 Then
                LoadProperty(FinanceTypeProperty, ProcessUtility.FINANCE_TYPE_BALLOON)
            Else
                LoadProperty(FinanceTypeProperty, ProcessUtility.FINANCE_TYPE_RETAIL)
            End If
        Else
            LoadProperty(FinanceTypeProperty, ProcessUtility.FINANCE_TYPE_LEASE)
        End If
    End Sub
    Private Sub CalculateProductType(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
        If ProcessUtility.IsLease(Me) Then
            If cRun.ContainsKey("DEAL_TERM") AndAlso cRun.ContainsKey("DEAL_AMORTTERM") AndAlso cRun("DEAL_TERM") <> cRun("DEAL_AMORTTERM") Then
                LoadProperty(ProductTypeProperty, ProcessUtility.PRODUCT_TYPE_SINGLE)
            Else
                LoadProperty(ProductTypeProperty, ProcessUtility.PRODUCT_TYPE_MONTHLY)
            End If
        Else
            LoadProperty(ProductTypeProperty, ProcessUtility.PRODUCT_TYPE_SIMPLE_INTEREST)
        End If
    End Sub
    Private Sub CalculateStateOrProvinceExecution(ByVal cRun As Dictionary(Of String, Object))
        Dim x As String = String.Empty
        If StateOrProvinceExecution = "MN" AndAlso cRun.TryGetValue("DLR_PROV", x) Then
            LoadProperty(StateOrProvinceExecutionProperty, GetProvinceAbbreviation.Fetch(cRun("DLR_PROV")))
        End If
    End Sub
    Public Shared Function GenerateApplicationTypeList(ByVal appArgs As ApplicationTypeArgs) As List(Of String)
        Dim l As New List(Of String)
        If appArgs.IsBusiness Then
            l.Add(ProcessUtility.C_APPTYPE_BUSINESS)
            If appArgs.HasCoApplicantOrGuarantor Then
                l.Add(ProcessUtility.C_APPTYPE_BUSINESSCOAPP)
                l.Add(ProcessUtility.C_APPTYPE_BUSINESSGUARANTOR)
                If appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor Then
                    l.Add(ProcessUtility.C_APPTYPE_BUSINESSTWOCOAPP)
                    l.Add(ProcessUtility.C_APPTYPE_BUSINESSGUARANTORCOAPP)
                End If
            End If
        Else
            l.Add(ProcessUtility.C_APPTYPE_PRIM)
            If appArgs.HasCoApplicantOrGuarantor Then
                l.Add(ProcessUtility.C_APPTYPE_PRIMCOAPP)
                l.Add(ProcessUtility.C_APPTYPE_PRIMGUARANTOR)
                If appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor Then l.Add(ProcessUtility.C_APPTYPE_PRIMTWOCOAPP)
            End If
        End If
        Return l
    End Function
    Public Shared Function ValidateApplicationType(ByVal currentType As String, ByVal validApplicationList As List(Of String), ByVal appArgs As ApplicationTypeArgs) As String
        If currentType <> ProcessUtility.C_APPTYPE_PRIM AndAlso validApplicationList.Contains(currentType) Then Return currentType 'Current Type from Previous Data is good, leave as is.
        If appArgs.IsBusiness Then
            If appArgs.HasCoApplicantOrGuarantor Then
                Return IIf(appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor, ProcessUtility.C_APPTYPE_BUSINESSTWOCOAPP, ProcessUtility.C_APPTYPE_BUSINESSCOAPP)
            Else
                Return ProcessUtility.C_APPTYPE_BUSINESS
            End If
        Else
            If appArgs.HasCoApplicantOrGuarantor Then
                Return IIf(appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor, ProcessUtility.C_APPTYPE_PRIMTWOCOAPP, ProcessUtility.C_APPTYPE_PRIMCOAPP)
            Else
                Return ProcessUtility.C_APPTYPE_PRIM
            End If
        End If
        Return ProcessUtility.C_APPTYPE_PRIM
    End Function
    Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
        StoreField(CountryProperty, d)
        StoreField(StateOrProvinceExecutionProperty, d)
        StoreField(ApplicationTypeProperty, d)
        StoreField(FinanceTypeProperty, d)
        StoreField(ProductTypeProperty, d)
        StoreField(DealTypeProperty, d)
    End Sub
#End Region
#Region "  Business Rules "
    Public Overrides Sub Requirement(validationRoot As Rule)
        ' Do Nothing no business rules for Process Info.
    End Sub
#End Region
    Public Sub FormatCountry()
        If ProcessUtility.IsCanadian(Me) Then
            LoadProperty(CountryProperty, "CA")
        Else
            LoadProperty(CountryProperty, "US")
        End If
    End Sub
End Class


