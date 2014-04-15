Imports Csla
Namespace ValidationRuleData
    Public Class Address
        Inherits BusinessBase(Of Address)

        Private _globalProperties As ProcessInfo

        'Local Business Rule Dependencies
        Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicantType))
        Public Shared ReadOnly AddressTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.AddressType))
        Public Shared ReadOnly SameAsHomeAddressProperty As PropertyInfo(Of Boolean) = RegisterProperty(Of Boolean)(Function(c) (c.SameAsHomeAddress), "_SAMEASHOME", True)
        Public Shared ReadOnly Line1Property As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Line1), "_ADDRESS", String.Empty)
        Public Shared ReadOnly Line2Property As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Line2), "_ADDRESS2", String.Empty)
        Public Shared ReadOnly CityProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.City), "_CITY", String.Empty)
        Public Shared ReadOnly StateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.State), "_PROV", String.Empty)
        Public Shared ReadOnly ZipProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Zip), "_PCODE", String.Empty)
        Public Shared ReadOnly CountyProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.County), "_COUNTY", String.Empty)

#Region "  Properties "
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperties
            End Get
        End Property
        Public ReadOnly Property ApplicantType As String
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
#End Region

#Region " Data Access "
        Public Sub New(ByVal parent As String, ByVal addrType As String, gProp As ProcessInfo)
            _globalProperties = gProp
            LoadProperty(ApplicantTypeProperty, parent)
            LoadProperty(AddressTypeProperty, addrType)
        End Sub
        Public Shared Function Fetch(ByVal keyParent As String, ByVal addrType As String, gProp As ProcessInfo, ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As Address
            Dim addr As New Address(keyParent, addrType, gProp)
            If currentRun Is Nothing Then Return addr
            addr.Populate(previousRun)
            addr.PopulateOverride(currentRun)
            addr.Calculate(previousRun, currentRun)
            Return addr
        End Function
        Public Sub Populate(ByVal pRun As Dictionary(Of String, Object))
            If pRun Is Nothing Then Exit Sub
            Dim addressKey As String = ApplicantType & AsKey(AddressType)
            Dim sameAsHomeKey As String = addressKey & SameAsHomeAddressProperty.FriendlyName 'Same As Home is Always True By Default
            If pRun.ContainsKey(sameAsHomeKey) Then LoadProperty(SameAsHomeAddressProperty, pRun(sameAsHomeKey))
            If Not SameAsHomeAddress Then
                PopulateField(Line1Property, addressKey, pRun)
                PopulateField(Line2Property, addressKey, pRun)
                PopulateField(CityProperty, addressKey, pRun)
                Dim sAbbr As String = String.Empty
                If pRun.TryGetValue(addressKey & StateProperty.FriendlyName, sAbbr) Then LoadProperty(StateProperty, GetProvinceAbbreviation.Fetch(sAbbr))
                PopulateField(ZipProperty, addressKey, pRun)
                PopulateField(CountyProperty, addressKey, pRun)
            End If
        End Sub
        Public Sub PopulateOverride(ByVal cRun As Dictionary(Of String, Object))
            If cRun Is Nothing Then Exit Sub
            Dim addressKey As String = ApplicantType ' No Garage/Billing Address in Aristo One Address only.
            If SameAsHomeAddress Then
                PopulateField(Line1Property, addressKey, cRun)
                PopulateField(Line2Property, addressKey, cRun)
                PopulateField(CityProperty, addressKey, cRun)
                Dim sAbbr As String = String.Empty
                If cRun.TryGetValue(addressKey & StateProperty.FriendlyName, sAbbr) Then LoadProperty(StateProperty, GetProvinceAbbreviation.Fetch(sAbbr))
                PopulateField(ZipProperty, addressKey, cRun)
                PopulateField(CountyProperty, addressKey, cRun)
            End If
        End Sub
        Public Sub Calculate(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            If cRun Is Nothing Then Exit Sub
            'Should not be required...
        End Sub
        Function SaveData() As Dictionary(Of String, Object)
            Dim d As New Dictionary(Of String, Object)
            Dim addressKey As String = ApplicantType & AsKey(AddressType)
            d.Add(addressKey & SameAsHomeAddressProperty.FriendlyName, SameAsHomeAddress)
            d.Add(addressKey & Line1Property.FriendlyName, Line1)
            d.Add(addressKey & Line2Property.FriendlyName, Line2)
            d.Add(addressKey & CityProperty.FriendlyName, City)
            d.Add(addressKey & StateProperty.FriendlyName, State)
            d.Add(addressKey & ZipProperty.FriendlyName, Zip)
            d.Add(addressKey & CountyProperty.FriendlyName, County)
            Return d
        End Function
        Public Function AsKey(ByVal str As String) As String
            If str = Applicant.C_HOMEADDRESS Then Return ""
            If str = Applicant.C_GARAGEADDRESS Then Return "_GARAGE"
            Return "_BILLING"
        End Function
        Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal prefixKey As String, ByVal d As Dictionary(Of String, Object))
            Dim key As String = prefixKey & pi.FriendlyName
            Dim xVal As String = String.Empty
            If d.TryGetValue(key, xVal) Then LoadProperty(pi, xVal)
        End Sub
#End Region

#Region "  Business Rules "
        Protected Overrides Sub AddBusinessRules()
            Me.BusinessRules.AddRule(New HasRequiredValueString(Line1Property, "Validation Error - Street Address is required."))
            Me.BusinessRules.AddRule(New HasRequiredValueString(CityProperty, "Validation Error - City is required."))
            Me.BusinessRules.AddRule(New HasRequiredStateOrProvince(StateProperty))
            Me.BusinessRules.AddRule(New IsValidZip)
            Me.BusinessRules.AddRule(New IsValidPostalCode)
        End Sub
        Public Sub CheckRules()
            Me.BusinessRules.CheckRules()
        End Sub
        Public Class HasRequiredValueString
            Inherits Csla.Rules.BusinessRule

            Public _overrideMessage As String

            Public Sub New(ByVal pi As PropertyInfo(Of String), ByVal overrideMessage As String)
                Me.PrimaryProperty = pi
                Me._overrideMessage = overrideMessage
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                Dim x As String = t.GetProperty(Me.PrimaryProperty)
                If IsInvalidValue(x) Then
                    Dim msg As String = _overrideMessage
                    If String.IsNullOrWhiteSpace(msg) Then msg = "Validation Error - " & PrimaryProperty.Name & " is required."
                    context.AddErrorResult(msg)
                End If
            End Sub
            Private Function IsInvalidValue(ByVal val As String) As Boolean
                Return String.IsNullOrWhiteSpace(val)
            End Function
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
                    Dim safePropName As String = Address.MakeLocaleSafe(PrimaryProperty.Name, Utility.IsCanadian(t.GlobalProperty))
                    context.AddErrorResult("Validation Error - " & PrimaryProperty.Name & "is required.")
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
                If Not Utility.IsCanadian(t.GlobalProperty) Then Exit Sub
                If t.Zip.Length = 5 Or t.Zip.Length = 10 Then context.AddErrorResult("Validation Error - Requires Valid Zip Code Format ##### Or #####-####.")
            End Sub
        End Class
        Public Class IsValidPostalCode
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = ZipProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                If Utility.IsCanadian(t.GlobalProperty) Then Exit Sub
                If t.Zip.Length = 6 Then context.AddErrorResult("Validation Error - Requires Valid Postal Code Format A#A#A# Where A denotes Alphabetical character.")
            End Sub
        End Class
        Public Class IsCountyRequired
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = CountyProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As Address = context.Target
                If Utility.IsCanadian(t.GlobalProperty) Then Exit Sub
                If t.ApplicantType.StartsWith("BUSINESS", StringComparison.InvariantCultureIgnoreCase) Then Exit Sub
                If Not Utility.IsLease(t.GlobalProperty) Then Exit Sub
                If String.IsNullOrWhiteSpace(t.County) Then context.AddErrorResult("Validation Error - US Lease Require County.")
            End Sub
        End Class
#End Region
        Public Shared Function MakeLocaleSafe(ByVal str As String, IsCanadian As Boolean) As String
            If Not IsCanadian Then Return str
            If str = "State" Then Return "Province"
            If str = "Zip" Then Return "Postal Code"
            Return String.Empty
        End Function
    End Class
End Namespace
