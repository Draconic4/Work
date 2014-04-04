Imports Csla

Public Class Address
    Inherits BusinessBase(Of Address)

    Private _globalProperties As ProcessInfo

    'Global Business Rule Dependency
    Public Shared ReadOnly GlobalRuleProperty As PropertyInfo(Of ProcessInfo) = RegisterProperty(Of ProcessInfo)(Function(c) (c.GlobalProperty), RelationshipTypes.PrivateField)
    'Local Business Rule Dependencies
    Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicantType))
    Public Shared ReadOnly AddressTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.AddressType), "ADDRESSTYPE", "HomeAddress")
    Public Shared ReadOnly SameAsHomeAddressProperty As PropertyInfo(Of Boolean) = RegisterProperty(Of Boolean)(Function(c) (c.SameAsHomeAddress), "_SAMEASHOME", True)
    'Data 
    Public Shared ReadOnly Line1Property As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Line1), "_ADDRESS", String.Empty)
    Public Shared ReadOnly Line2Property As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Line2), "_ADDRESS", String.Empty)
    Public Shared ReadOnly CityProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.City), "_CITY", String.Empty)
    Public Shared ReadOnly StateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.State), "_PROV", String.Empty)
    Public Shared ReadOnly ZipProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Zip), "_CITY", String.Empty)
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
    Public Property AddressType As String
        Get
            Return GetProperty(AddressTypeProperty)
        End Get
        Set(value As String)
            SetProperty(AddressTypeProperty, AddressType)
        End Set
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
    Public Sub New(ByVal prefix As String)
        LoadProperty(ApplicantTypeProperty, prefix)
    End Sub
    Public Shared Function FetchExisting(ByVal prefix As String, pInfo As ProcessInfo) As Address
        Return New Address(prefix)
    End Function
    Public Sub Populate(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        Dim addrKey As String = PopulateKey(Line1Property)
        If d.ContainsKey(addrKey) Then
            Dim addrValue As String = d(addrKey)
            If addrValue.Length >= 40 Then
                LoadProperty(Line1Property, String.Empty)
                LoadProperty(Line2Property, String.Empty)
            Else
                LoadProperty(Line1Property, addrValue)
            End If
        End If
        PopulateField(CityProperty, d)
        Dim stateKey As String = ApplicantType & StateProperty.FriendlyName
        If d.ContainsKey(stateKey) Then
            LoadProperty(StateProperty, Utility.ProvinceOrStateConverter(d(stateKey), GlobalProperty.IsCanadian))
        End If
        PopulateField(CountyProperty, d)
        PopulateField(ZipProperty, d)
    End Sub
    Public Function PopulateKey(ByVal pi As PropertyInfo(Of String)) As String
        If AddressType = "HomeAddress" Then Return ApplicantType & pi.FriendlyName
        Return ApplicantType & AddressType & pi.FriendlyName
    End Function
    Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal d As Dictionary(Of String, Object))
        Dim key As String = PopulateKey(pi)
        If d.ContainsKey(key) Then LoadProperty(pi, d(key))
    End Sub
    Public Function SerializeField(ByVal pi As PropertyInfo(Of String)) As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)

        d.Add(PopulateKey(Line1Property), Line1)
        d.Add(PopulateKey(Line2Property), Line2)
        d.Add(PopulateKey(CityProperty), City)
        d.Add(PopulateKey(StateProperty), State)
        d.Add(PopulateKey(ZipProperty), Zip)

        Return d
    End Function
#End Region

#Region "  Business Rules "
    Protected Overrides Sub AddBusinessRules()
        Me.BusinessRules.AddRule(New HasRequiredValue(Line1Property))
        Me.BusinessRules.AddRule(New HasRequiredValue(CityProperty))
        Me.BusinessRules.AddRule(New HasRequiredValue(StateProperty))
        Me.BusinessRules.AddRule(New IsValidZip)
        Me.BusinessRules.AddRule(New IsValidPostalCode)
    End Sub
    Public Sub CheckRules()
        Me.BusinessRules.CheckRules()
    End Sub
    Public Class HasRequiredValue
        Inherits Csla.Rules.BusinessRule
        Public Sub New(ByVal pi As PropertyInfo(Of String))
            Me.PrimaryProperty = pi
            Me.InputProperties = New List(Of Core.IPropertyInfo) From {GlobalRuleProperty, Line1Property, Line2Property, CityProperty, StateProperty, ZipProperty}
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Address = context.Target
            Dim x As String = t.ReadProperty(Me.PrimaryProperty)
            If PrimaryProperty.Name = "Line1" Or PrimaryProperty.Name = "Line2" AndAlso IsInValidValue(x) Then
                context.AddErrorResult("Validation Error - Street Address is required.")
            Else
                Dim safePropName As String = Address.MakeLocaleSafe(PrimaryProperty.Name, t.GlobalProperty.IsCanadian)
                context.AddErrorResult("Validation Error - " & PrimaryProperty.Name & "is required.")
            End If
        End Sub
        Private Function IsInValidValue(ByVal val As String) As Boolean
            Return String.IsNullOrWhiteSpace(val)
        End Function
    End Class
    Public Class IsValidZip
        Inherits Csla.Rules.BusinessRule

        Public Sub New()
            Me.PrimaryProperty = ZipProperty
        End Sub

        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Address = context.Target
            If Not t.GlobalProperty.IsCanadian Then Exit Sub
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
            If t.GlobalProperty.IsCanadian Then Exit Sub
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
            If t.GlobalProperty.IsCanadian Then Exit Sub
            If t.ApplicantType <> "BUSINESS" Then Exit Sub
            If Not t.GlobalProperty.IsLease Then Exit Sub
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
