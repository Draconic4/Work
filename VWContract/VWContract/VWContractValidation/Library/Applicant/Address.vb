Imports Csla

Namespace ValidationLib
    Public Class Address
        Inherits BaseFormField

        Public Const C_HOMEADDRESS As String = "HOME ADDRESS"
        Public Const C_WORKADDRESS As String = "WORK ADDRESS"
        Public Const C_GARAGEADDRESS As String = "GARAGE"
        Public Const C_BILLINGADDRESS As String = "BILLING"

        Private _AddressRoleOverride As Boolean = False

        'Local Business Rule Dependencies See Inheritance worker below for RegisterPropertyLocal
        Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of ApplicantArgs.ApplicantTypes) = RegisterPropertyLocal(Of ApplicantArgs.ApplicantTypes, Address)(Function(c) (c.ApplicantType), String.Empty, ApplicantArgs.ApplicantTypes.BUYER1)
        Public Shared ReadOnly AddressTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.AddressType), String.Empty, String.Empty)
        Public Shared ReadOnly SameAsHomeAddressProperty As PropertyInfo(Of Boolean) = RegisterPropertyLocal(Of Boolean, Address)(Function(c) (c.SameAsHomeAddress), "_SAMEASHOME", True)
        Public Shared ReadOnly Line1Property As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.Line1), "_ADDR", String.Empty)
        Public Shared ReadOnly Line2Property As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.Line2), "_ADDR2", String.Empty)
        Public Shared ReadOnly CityProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.City), "_CITY", String.Empty)
        Public Shared ReadOnly StateProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.State), "_PROV", "MN")
        Public Shared ReadOnly ZipProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.Zip), "_PCODE", String.Empty)
        Public Shared ReadOnly CountyProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.County), "_COUNTY", String.Empty)
        Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, Address)(Function(c) (c.Country), "_COUNTRY", String.Empty)

#Region "  Properties "
        Public ReadOnly Property ApplicantType As ApplicantArgs.ApplicantTypes
            Get
                Return GetProperty(ApplicantTypeProperty)
            End Get
        End Property
        Public ReadOnly Property AddressType As String
            Get
                Return GetProperty(AddressTypeProperty)
            End Get
        End Property
        Public Property SameAsHomeAddress As Boolean
            Get
                Return GetProperty(SameAsHomeAddressProperty)
            End Get
            Set(value As Boolean)
                SetProperty(SameAsHomeAddressProperty, value)
            End Set
        End Property
        Public Property Line1 As String
            Get
                Return GetProperty(Line1Property)
            End Get
            Set(value As String)
                SetProperty(Line1Property, value)
            End Set
        End Property
        Public Property Line2 As String
            Get
                Return GetProperty(Line2Property)
            End Get
            Set(value As String)
                SetProperty(Line2Property, value)
            End Set
        End Property
        Public Property City As String
            Get
                Return GetProperty(CityProperty)
            End Get
            Set(value As String)
                SetProperty(CityProperty, value)
            End Set
        End Property
        Public Property State As String
            Get
                Return GetProperty(StateProperty)
            End Get
            Set(value As String)
                SetProperty(StateProperty, value)
            End Set
        End Property
        Public Property Zip As String
            Get
                Return GetProperty(ZipProperty)
            End Get
            Set(value As String)
                SetProperty(ZipProperty, value)
            End Set
        End Property
        Public Property County As String
            Get
                Return GetProperty(CountyProperty)
            End Get
            Set(value As String)
                SetProperty(CountyProperty, value)
            End Set
        End Property
        Public Property Country As String
            Get
                Return GetProperty(CountryProperty)
            End Get
            Set(value As String)
                SetProperty(CountryProperty, value)
            End Set
        End Property
#End Region

#Region " Data Access "
        Public Sub New(ByVal parent As ApplicantArgs.ApplicantTypes, ByVal addrType As String, gProp As ProcessInfo)
            MyBase.New(gProp)
            LoadProperty(ApplicantTypeProperty, parent)
            LoadProperty(AddressTypeProperty, addrType)
        End Sub
        Public Shared Function Fetch(ByVal addrType As String, appArgs As ApplicantArgs) As Address
            Dim addr As New Address(appArgs.applicantType, addrType, appArgs.GlobalProperty)
            addr.ReadDictionary(appArgs)
            Return addr
        End Function
        Public Overrides Sub Populate(ByVal pRun As Dictionary(Of String, Object))
            If pRun Is Nothing Then Exit Sub
            PopulateField(pRun, SameAsHomeAddressProperty)
            If Not SameAsHomeAddress Then
                _AddressRoleOverride = False
                PopulateField(pRun, Line1Property)
                PopulateField(pRun, Line2Property)
                PopulateField(pRun, CityProperty)
                PopulateField(pRun, StateProperty)
                Dim provAbbr As String = GetProvinceAbbreviation.Fetch(State)
                LoadProperty(StateProperty, provAbbr)
                PopulateField(pRun, ZipProperty)
                PopulateField(pRun, CountyProperty)
            End If
        End Sub
        Public Overrides Sub PopulateOverride(ByVal cRun As Dictionary(Of String, Object))
            If cRun Is Nothing Then Exit Sub
            If SameAsHomeAddress Then
                _AddressRoleOverride = True
                Dim stateKey As String = GenerateKey(StateProperty) ' No Garage/Billing Address in Aristo One Address only.
                PopulateField(cRun, Line1Property)
                PopulateField(cRun, Line2Property)
                PopulateField(cRun, CityProperty)
                Dim sAbbr As String = String.Empty
                If cRun.TryGetValue(stateKey, sAbbr) Then LoadProperty(StateProperty, GetProvinceAbbreviation.Fetch(sAbbr))
                PopulateField(cRun, ZipProperty)
                PopulateField(cRun, CountyProperty)
                _AddressRoleOverride = False
            End If
        End Sub
        Public Overrides Sub Calculate(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object))
            If GlobalProperty Is Nothing Then Exit Sub
            LoadProperty(CountryProperty, ProcessUtility.GetCountry(GlobalProperty.Country))
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
            StoreField(SameAsHomeAddressProperty, d)
            If IsHomeAddress() OrElse Not SameAsHomeAddress Then
                _AddressRoleOverride = False
                StoreField(Line1Property, d)
                StoreField(Line2Property, d)
                StoreField(CityProperty, d)
                StoreField(StateProperty, d)
                StoreField(ZipProperty, d)
                StoreField(CountyProperty, d)
            End If
        End Sub
        Public Function AsKey(ByVal str As String) As String
            If _AddressRoleOverride OrElse String.Compare(str, Address.C_HOMEADDRESS, True) = 0 Then Return ""
            Return "_" & str.Replace(" ", "_")
        End Function
        Public Overrides Function OnGenerateKey(ByVal friendlyName As String) As String
            If _AddressRoleOverride OrElse AddressType = Address.C_HOMEADDRESS Then Return ApplicantArgs.KeyParent(ApplicantType, GlobalProperty) & friendlyName
            Return ApplicantArgs.KeyParent(ApplicantType, GlobalProperty) & AsKey(AddressType) & friendlyName
        End Function
#End Region

#Region "  Business Rules "
        Protected Overrides Sub AddBusinessRules()
            Me.BusinessRules.AddRule(New IsLeaseGarageAddressRequired)
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(Line1Property, "Validation Error - Street Address is required.", False))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(CityProperty, "Validation Error - City is required.", False))
            Me.BusinessRules.AddRule(New HasRequiredStateOrProvince(StateProperty))
            Me.BusinessRules.AddRule(New IsValidZip)
            Me.BusinessRules.AddRule(New IsValidPostalCode)
        End Sub
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)
            Dim v As Rule = New Rule(New ValidationFormat With {.Message = AddressType, .BottomLayer = False})
            AttachPropertyRequirements(v)
            validationRoot.Rules.Add(v)
        End Sub
        Public Class IsLeaseGarageAddressRequired
            Inherits Csla.Rules.BusinessRule
            Public Sub New()
                Me.PrimaryProperty = AddressTypeProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                If ProcessUtility.IsLease(t.GlobalProperty) Then
                    If String.Compare(t.AddressType, Address.C_GARAGEADDRESS, True) = 0 Then
                        If Not t.SameAsHomeAddress Then
                            Dim count As Integer = 0
                            If String.IsNullOrWhiteSpace(t.Line1) Then count = count + 1
                            If String.IsNullOrWhiteSpace(t.City) Then count = count + 1
                            If String.IsNullOrWhiteSpace(t.State) Then count = count + 1
                            If String.IsNullOrWhiteSpace(t.Zip) Then count = count + 1
                            If String.IsNullOrWhiteSpace(t.County) Then count = count + 1
                            If count <> 0 Then context.AddErrorResult("Validation Error - Garage Address is required for Leasing.")
                        End If
                    End If
                End If
            End Sub
        End Class
        Public Class HasRequiredStateOrProvince
            Inherits Csla.Rules.BusinessRule
            Public Sub New(ByVal pi As PropertyInfo(Of String))
                Me.PrimaryProperty = StateProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                Dim x As String = t.ReadProperty(Me.PrimaryProperty)
                If String.IsNullOrWhiteSpace(x) Then
                    Dim safePropName As String = ProcessUtility.GetPostalCodeOrZipCodeText(t.GlobalProperty)
                    context.AddErrorResult("Validation Error - " & PrimaryProperty.Name & " is required.")
                End If
            End Sub
        End Class
        Public Class IsValidZip
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = ZipProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                If Not ProcessUtility.IsCanadian(t.GlobalProperty) Then
                    If t.Zip.Length <> 5 Or t.Zip.Length <> 10 Then context.AddErrorResult("Validation Error - Requires Valid Zip Code Format ##### Or #####-####.")
                End If
            End Sub
        End Class
        Public Class IsValidPostalCode
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = ZipProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                If ProcessUtility.IsCanadian(t.GlobalProperty) Then
                    If t.Zip.Length = 6 Then context.AddErrorResult("Validation Error - Requires Valid Postal Code Format A#A#A# Where A denotes Alphabetical character.")
                End If
            End Sub
        End Class
        Public Class IsCountyRequired
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = CountyProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                If ProcessUtility.IsCanadian(t.GlobalProperty) Then Exit Sub
                If Not ProcessUtility.IsLease(t.GlobalProperty) Then Exit Sub
                If String.IsNullOrWhiteSpace(t.County) Then context.AddErrorResult("Validation Error - US Lease Require County.")
            End Sub
        End Class
#End Region
        Public Function IsHomeAddress() As Boolean
            Return String.Compare(Me.AddressType, C_HOMEADDRESS, True) = 0 Or String.Compare(Me.AddressType, C_WORKADDRESS, True) = 0
        End Function
        Public Function UseHomeAddress() As Boolean
            If IsHomeAddress() Then Return False
            Return Me.SameAsHomeAddress
        End Function
    End Class
End Namespace
