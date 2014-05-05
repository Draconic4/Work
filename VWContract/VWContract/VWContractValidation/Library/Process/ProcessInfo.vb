Imports Csla
Namespace ValidationLib
    Public Class ProcessInfo
        Inherits BaseFormField

        Private Const C_VWDealerCode As String = "VCI"
        Private _ApplicationArgs As ApplicationTypeArgs

        Public Shared ReadOnly ContractIDProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.ContractID), "DEAL_ID", String.Empty)
        Public Shared ReadOnly ContractFormIdProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.ContractFormID), "VW_CONTRACTFORMNUMBER", "553-MN")
        Public Shared ReadOnly ContractFormRevisionProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.ContractFormRevision), "VW_CONTRACTREVISION", "2012-10-01")
        Public Shared ReadOnly ContractDateProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.ContractDate), "DEAL_CONTRACTDATE", String.Empty)
        Public Shared ReadOnly CreditApplicationSourceNumberProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.CreditApplicationSourceNumber), "DEAL_CREDITAPPNUMBER", String.Empty)
        Public Shared ReadOnly PaymentPerYearProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer, ProcessInfo)(Function(c) (c.PaymentPerYear), "DEAL_PAYMENTSPERYEAR", "12")
        Public Shared ReadOnly VehicleCountProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer, ProcessInfo)(Function(c) (c.VehicleCount), "VEH", 0)
        Public Shared ReadOnly DealerIdProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.DealerId), "DLR_CODE", String.Empty)
        Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.Country), "DLR_COUNTRY", "US")
        Public Shared ReadOnly StateOrProvinceExecutionProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.StateOrProvinceExecution), "VW_EXECUTIONSTATEORPROVINCE", "MN")
        Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.ApplicationType), "VW_APPLICANTTYPE", ProcessUtility.C_APPTYPE_PRIM)
        Public Shared ReadOnly DealTypeProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer, ProcessInfo)(Function(c) (c.DealType), "DEAL_FCALC", 0)
        Public Shared ReadOnly FinanceTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.FinanceType), "VW_FINANCETYPE", ProcessUtility.FINANCE_TYPE_RETAIL)
        Public Shared ReadOnly ProductTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.ProductType), "VW_PRODUCTTYPE", ProcessUtility.PRODUCT_TYPE_SIMPLE_INTEREST)
        Public Shared ReadOnly VerificationDateProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ProcessInfo)(Function(c) (c.VerificationDate), "VW_VERIFICATIONDATE", String.Empty)

#Region "  Properties "
        Public ReadOnly Property ContractID As String
            Get
                Return GetProperty(ContractIDProperty)
            End Get
        End Property
        Public Property ContractFormID As String
            Get
                Return GetProperty(ContractFormIdProperty)
            End Get
            Set(value As String)
                If ContractFormID <> value Then
                    SetProperty(ContractFormIdProperty, value)
                    ContractFormIDChanged = True
                End If
            End Set
        End Property
        Public Property ContractFormIDChanged As Boolean = False
        Public Property ContractFormRevision As String
            Get
                Return GetProperty(ContractFormRevisionProperty)
            End Get
            Set(value As String)
                If ContractFormRevision <> value Then
                    SetProperty(ContractFormRevisionProperty, value)
                    ContractFormRevisionChanged = True
                End If
            End Set
        End Property
        Public Property ContractFormRevisionChanged As Boolean = False
        Public ReadOnly Property ContractDate As String
            Get
                Return GetProperty(ContractDateProperty)
            End Get
        End Property
        Public Property PaymentPerYear As Integer
            Get
                Return GetProperty(PaymentPerYearProperty)
            End Get
            Set(value As Integer)
                SetProperty(PaymentPerYearProperty, value)
            End Set
        End Property
        Public Property VehicleCount As Integer
            Get
                Return GetProperty(VehicleCountProperty)
            End Get
            Set(value As Integer)
                SetProperty(VehicleCountProperty, value)
            End Set
        End Property
        Public Property DealerId As String
            Get
                Return GetProperty(DealerIdProperty)
            End Get
            Set(value As String)
                SetProperty(DealerIdProperty, value)
            End Set
        End Property
        Public Property CreditApplicationSourceNumber As String
            Get
                Return GetProperty(CreditApplicationSourceNumberProperty)
            End Get
            Set(value As String)
                SetProperty(CreditApplicationSourceNumberProperty, value)
            End Set
        End Property
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
        Public ReadOnly Property VerificationDate As String
            Get
                Return GetProperty(VerificationDateProperty)
            End Get
        End Property
#End Region
#Region "  Data Access "
        Public Sub New()
            MyBase.New(Nothing) 'Is the global properties base class!
        End Sub
        Public Shared Function Fetch(baseArgs As BaseConstructionArgs) As ProcessInfo
            Dim pi As New ProcessInfo
            pi.ReadDictionary(baseArgs)
            Return pi
        End Function
        Public Overrides Sub Populate(ByVal pRun As Dictionary(Of String, Object))
            PopulateField(pRun, CountryProperty)
            PopulateField(pRun, StateOrProvinceExecutionProperty)
            PopulateField(pRun, ApplicationTypeProperty) 'Created on subsequent runs
            PopulateField(pRun, FinanceTypeProperty)
            PopulateField(pRun, ProductTypeProperty)
            PopulateField(pRun, VerificationDateProperty)
        End Sub
        Public Overrides Sub PopulateOverride(ByVal cRun As Dictionary(Of String, Object))
            PopulateField(cRun, ContractIDProperty)
            PopulateField(cRun, ContractDateProperty)
            PopulateField(cRun, DealerIdProperty)
            PopulateField(cRun, CreditApplicationSourceNumberProperty)
            PopulateField(cRun, CountryProperty)
            FormatCountry()
            PopulateField(cRun, DealTypeProperty)
            PopulateField(cRun, PaymentPerYearProperty)
            Dim v2 As String = String.Empty
            cRun.TryGetValue("VEH2_YEAR", v2)
            If String.IsNullOrWhiteSpace(v2) Then LoadProperty(VehicleCountProperty, 2)
        End Sub
        Public Overrides Sub Calculate(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            ExtractDealerCode(pRun, cRun)
            CalculateApplicationType(pRun, cRun)
            CalculateFinanceType(pRun, cRun)
            CalculateProductType(pRun, cRun)
            CalculateStateOrProvinceExecution(cRun)
        End Sub
        Private Sub ExtractDealerCode(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            Dim codeStr As String = DealerId
            codeStr = Text.RegularExpressions.Regex.Replace(codeStr, "/s*", " ")
            Dim _codes As List(Of String) = codeStr.Split(New Char() {" "c}).ToList
            Dim found As String = String.Empty
            For i = 0 To _codes.Count - 1
                If _codes(i).StartsWith(C_VWDealerCode, StringComparison.OrdinalIgnoreCase) Then
                    found = _codes(i).Substring(3)
                End If

            Next
            DealerId = found
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
            _ApplicationArgs = New ApplicationTypeArgs(businessFlg, hasCoAppFlg, hasCoApp2Flg)
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
        Protected Overrides Sub AddBusinessRules()
            Me.BusinessRules.AddRule(New PaymentPerYearValid() With {.Priority = ValidationFormat.RuleDisplay.AristoMissingInformation})
            Me.BusinessRules.AddRule(New VehicleCountValid() With {.Priority = ValidationFormat.RuleDisplay.AristoMissingInformation + 1})
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(CreditApplicationSourceNumberProperty, "Credit Application must be completed successfully before Contract Validation.", False) With {.Priority = ValidationFormat.RuleDisplay.RequiredResponse})
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(DealerIdProperty, "Dealer Code must be assigned before Contract Validation.", False) With {.Priority = ValidationFormat.RuleDisplay.AristoMissingInformation + 2})
            Me.BusinessRules.AddRule(New RequiredValueChanged(ContractFormIdProperty, "") With {.Priority = ValidationFormat.RuleDisplay.CodingRequirement})
            Me.BusinessRules.AddRule(New RequiredValueChanged(ContractFormRevisionProperty, "") With {.Priority = ValidationFormat.RuleDisplay.CodingRequirement + 1})
        End Sub
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)
            Dim v As Rule = New Rule(New ValidationFormat With {.Message = "Process Check", .BottomLayer = False})
            AttachPropertyRequirements(v)
            validationRoot.Rules.Add(v)
        End Sub
        Public Class RequiredValueChanged
            Inherits Csla.Rules.BusinessRule
            Public _overrideMessage As String
            Public Sub New(ByVal pi As PropertyInfo(Of String), ByVal warningMessage As String)
                Me.PrimaryProperty = pi
                Me._overrideMessage = _overrideMessage
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim pi As ProcessInfo = context.Target
                Dim hasChanged As Boolean = False
                Select Case Me.PrimaryProperty.Name
                    Case "ContractFormID"
                        hasChanged = pi.ContractFormIDChanged
                    Case "ContractFormRevision"
                        hasChanged = pi.ContractFormRevisionChanged
                End Select
                If Not hasChanged Then
                    context.AddErrorResult(Utility.FormatWarningOrErrorMessage(Me, _overrideMessage, True))
                End If
            End Sub
        End Class
        Public Class PaymentPerYearValid
            Inherits Csla.Rules.BusinessRule
            Public Sub New()
                Me.PrimaryProperty = PaymentPerYearProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim pi As ProcessInfo = context.Target
                If pi.PaymentPerYear <> 12 Then
                    context.AddErrorResult(Utility.FormatWarningOrErrorMessage(Me, "VW Contract Validation only supports monthly payments", False))
                End If
            End Sub
        End Class
        Public Class VehicleCountValid
            Inherits Csla.Rules.BusinessRule
            Public Sub New()
                Me.PrimaryProperty = VehicleCountProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As ProcessInfo = context.Target
                If t.VehicleCount > 1 Then
                    context.AddErrorResult(Utility.FormatWarningOrErrorMessage(Me, "VW Contract Validation does not support deals allowing Multiple Vehicles.", False))
                End If
            End Sub
        End Class
#End Region
        Public Sub FormatCountry()
            If ProcessUtility.IsCanadian(Me) Then
                LoadProperty(CountryProperty, "CA")
            Else
                LoadProperty(CountryProperty, "US")
            End If
        End Sub
    End Class
End Namespace