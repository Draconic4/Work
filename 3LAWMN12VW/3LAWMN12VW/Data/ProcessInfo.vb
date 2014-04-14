Imports Csla

Namespace ValidationRuleData

    Public Class ProcessInfo
        Inherits BusinessBase(Of ProcessInfo)

        Private _ApplicationArgs As ApplicationTypeArgs

        Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "DLR_COUNTRY", "US")
        Public Shared ReadOnly StateOrProvinceExecutionProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.StateOrProvinceExecution, "VW_EXECUTIONSTATEORPROVINCE", "MN")
        Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicationType), "VW_APPLICANTTYPE", Utility.C_APPTYPE_PRIM)
        Public Shared ReadOnly DealTypeProperty As PropertyInfo(Of Integer) = RegisterProperty(Of Integer)(Function(c) c.DealType, "DEAL_FCALC", 0)
        Public Shared ReadOnly FinanceTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.FinanceType, "VW_FINANCETYPE", Utility.FINANCE_TYPE_RETAIL)
        Public Shared ReadOnly ProductTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.ProductType, "VW_PRODUCTTYPE", Utility.PRODUCT_TYPE_SIMPLE_INTEREST)

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

        Public Sub New()
        End Sub
        Public Shared Function Fetch(ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As ProcessInfo
            Dim pi As New ProcessInfo
            If currentRun Is Nothing Then Return pi
            pi.Populate(previousRun)
            pi.PopulateOverride(currentRun)
            pi.Calculate(previousRun, currentRun)
            Return pi
        End Function
        Private Sub Populate(ByVal pRun As Dictionary(Of String, Object))
            If pRun Is Nothing Then Exit Sub
            PopulateField(CountryProperty, pRun)
            PopulateField(StateOrProvinceExecutionProperty, pRun)
            PopulateField(ApplicationTypeProperty, pRun) 'Created on subsequent runs
            PopulateField(FinanceTypeProperty, pRun)
            PopulateField(ProductTypeProperty, pRun)
        End Sub
        Private Sub PopulateOverride(ByVal cRun As Dictionary(Of String, Object))
            If cRun Is Nothing Then Exit Sub
            PopulateField(CountryProperty, cRun) 'Required Aristo Field
            PopulateField(DealTypeProperty, cRun)
        End Sub
        Private Sub Calculate(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            CalculateApplicationType(pRun, cRun)
            CalculateFinanceType(pRun, cRun)
            CalculateProductType(pRun, cRun)
            CalculateStateOrProvinceExecution(cRun)
        End Sub
        Private Sub PopulateField(pi As PropertyInfo(Of String), d As Dictionary(Of String, Object))
            Dim key As String = pi.FriendlyName
            Dim xVal As String = String.Empty
            If d.TryGetValue(key, xVal) Then
                LoadProperty(pi, xVal)
            End If
        End Sub
        Private Sub PopulateField(pi As PropertyInfo(Of Integer), d As Dictionary(Of String, Object))
            Dim key As String = pi.FriendlyName
            Dim xVal As Integer = -1
            If d.TryGetValue(key, xVal) Then
                LoadProperty(pi, xVal)
            End If
        End Sub
        Private Sub CalculateApplicationType(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            If cRun Is Nothing Then Exit Sub
            Dim businessFlg As Boolean = cRun.ContainsKey("BUY_ISBUSINESS") AndAlso cRun("BUY_ISBUSINESS")
            Dim hasCoAppFlg As Boolean = cRun.ContainsKey("COBUYER1_CODE") AndAlso Not String.IsNullOrWhiteSpace(cRun("COBUYER1_CODE"))
            Dim hasCoApp2Flg As Boolean = cRun.ContainsKey("COBUYER2_CODE") AndAlso Not String.IsNullOrWhiteSpace(cRun("COBUYER2_CODE"))
            _ApplicationArgs = New ApplicationTypeArgs(businessFlg, hasCoAppFlg, hasCoApp2Flg)
            _ApplicationTypeList = GenerateApplicationTypeList(_ApplicationArgs)
            ApplicationType = ValidateApplicationType(ApplicationType, _ApplicationTypeList, _ApplicationArgs)
        End Sub
        Private Sub CalculateFinanceType(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            If Not Utility.IsLease(Me) Then
                If cRun.ContainsKey("DEAL_BALOON") AndAlso cRun("DEAL_BALOON") <> 0 Then
                    LoadProperty(FinanceTypeProperty, Utility.FINANCE_TYPE_BALLOON)
                Else
                    LoadProperty(FinanceTypeProperty, Utility.FINANCE_TYPE_RETAIL)
                End If
            Else
                LoadProperty(FinanceTypeProperty, Utility.FINANCE_TYPE_LEASE)
            End If
        End Sub
        Private Sub CalculateProductType(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            If Utility.IsLease(Me) Then
                If cRun.ContainsKey("DEAL_TERM") AndAlso cRun.ContainsKey("DEAL_AMORTTERM") AndAlso cRun("DEAL_TERM") <> cRun("DEAL_AMORTTERM") Then
                    LoadProperty(ProductTypeProperty, Utility.PRODUCT_TYPE_SINGLE)
                Else
                    LoadProperty(ProductTypeProperty, Utility.PRODUCT_TYPE_MONTHLY)
                End If
            Else
                LoadProperty(ProductTypeProperty, Utility.FINANCE_TYPE_LEASE)
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
                l.Add(Utility.C_APPTYPE_BUSINESS)
                If appArgs.HasCoApplicantOrGuarantor Then
                    l.Add(Utility.C_APPTYPE_BUSINESSCOAPP)
                    l.Add(Utility.C_APPTYPE_BUSINESSGUARANTOR)
                    If appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor Then
                        l.Add(Utility.C_APPTYPE_BUSINESSTWOCOAPP)
                        l.Add(Utility.C_APPTYPE_BUSINESSGUARANTORCOAPP)
                    End If
                End If
            Else
                l.Add(Utility.C_APPTYPE_PRIM)
                If appArgs.HasCoApplicantOrGuarantor Then
                    l.Add(Utility.C_APPTYPE_PRIMCOAPP)
                    l.Add(Utility.C_APPTYPE_PRIMGUARANTOR)
                    If appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor Then l.Add(Utility.C_APPTYPE_PRIMTWOCOAPP)
                End If
            End If
            Return l
        End Function
        Public Shared Function ValidateApplicationType(ByVal currentType As String, ByVal validApplicationList As List(Of String), ByVal appArgs As ApplicationTypeArgs) As String
            If validApplicationList.Contains(currentType) Then Return currentType 'Current Type from Previous Data is good, leave as is.
            If appArgs.IsBusiness Then
                If appArgs.HasCoApplicantOrGuarantor Then
                    Return IIf(appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor, Utility.C_APPTYPE_BUSINESSTWOCOAPP, Utility.C_APPTYPE_BUSINESSCOAPP)
                Else
                    Return Utility.C_APPTYPE_BUSINESS
                End If
            Else
                If appArgs.HasCoApplicantOrGuarantor Then
                    Return IIf(appArgs.HasCoApplicant2OrCoApplicantAndAlsoGuarantor, Utility.C_APPTYPE_PRIMTWOCOAPP, Utility.C_APPTYPE_PRIMCOAPP)
                Else
                    Return Utility.C_APPTYPE_PRIM
                End If
            End If
            Return Utility.C_APPTYPE_PRIM
        End Function
        Public Function SaveData() As Dictionary(Of String, Object)
            Dim d As New Dictionary(Of String, Object)
            d.Add(CountryProperty.FriendlyName, Country)
            d.Add(StateOrProvinceExecutionProperty.FriendlyName, StateOrProvinceExecution)
            d.Add(ApplicationTypeProperty.FriendlyName, ApplicationType)
            d.Add(FinanceTypeProperty.FriendlyName, FinanceType)
            d.Add(ProductTypeProperty.FriendlyName, ProductType)
            d.Add(DealTypeProperty.FriendlyName, DealType)
            Return d
        End Function
        Public Class ApplicationTypeArgs
            Private _hasCoApplicantOrGuarantor As Boolean = False
            Private _hasCoApplicant2OrCoApplicantAndAlsoGuarantor = False

            Property IsBusiness As Boolean
            Property HasCoApplicantOrGuarantor As Boolean
                Get
                    Return _hasCoApplicantOrGuarantor
                End Get
                Set(value As Boolean)
                    If value <> _hasCoApplicantOrGuarantor Then 'Lock Step hasCoApp and hasCoApp2
                        If _hasCoApplicantOrGuarantor Then HasCoApplicant2OrCoApplicantAndAlsoGuarantor = False
                        _hasCoApplicantOrGuarantor = value
                    End If
                End Set
            End Property
            Property HasCoApplicant2OrCoApplicantAndAlsoGuarantor As Boolean
                Get
                    Return _hasCoApplicant2OrCoApplicantAndAlsoGuarantor
                End Get
                Set(value As Boolean)
                    If _hasCoApplicantOrGuarantor Then
                        _hasCoApplicant2OrCoApplicantAndAlsoGuarantor = value
                    End If
                End Set
            End Property

            Public Sub New(ByVal isBusiness As Boolean, ByVal hasCoApplicantOrGuarantor As Boolean, ByVal hasCoApplicant2OrCoApplicantAndAlsoGuarantor As Boolean)
                Me.IsBusiness = isBusiness
                Me._hasCoApplicantOrGuarantor = hasCoApplicantOrGuarantor 'Lock Step hasCoApp and hasCoApp2.
                If hasCoApplicantOrGuarantor Then
                    _hasCoApplicant2OrCoApplicantAndAlsoGuarantor = hasCoApplicant2OrCoApplicantAndAlsoGuarantor
                End If
            End Sub
        End Class
    End Class
End Namespace
