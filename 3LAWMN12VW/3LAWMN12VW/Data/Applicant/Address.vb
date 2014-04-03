Imports Csla

Public Class Address
    Inherits BusinessBase(Of Address)

    Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicantType))
    Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "COUNTRY", "US")
    Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicationType), "_APPLICANTTYPE", "INDIV")
    Public Shared ReadOnly AddressTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.AddressType), "ADDRESSTYPE", "HomeAddress")
    Public Shared ReadOnly Line1Property As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Line1), "_ADDRESS", String.Empty)
    Public Shared ReadOnly Line2Property As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Line2), "_ADDRESS", String.Empty)
    Public Shared ReadOnly CityProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.City), "_CITY", String.Empty)
    Public Shared ReadOnly StateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.State), "_PROV", String.Empty)
    Public Shared ReadOnly PostalCodeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.PostalCode), "_CITY", String.Empty)
    Public Shared ReadOnly CountyProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.County), "_COUNTY", String.Empty)

#Region "  Properties "
    Public ReadOnly Property Country As String
        Get
            Return GetProperty(CountryProperty)
        End Get
    End Property
    Public ReadOnly Property ApplicantType As String
        Get
            Return GetProperty(ApplicantTypeProperty)
        End Get
    End Property
    Public Property ApplicationType As String
        Get
            Return GetProperty(ApplicantTypeProperty)
        End Get
        Set(value As String)
            SetProperty(ApplicantTypeProperty, ApplicationType)
        End Set
    End Property
    Public Property AddressType As String
        Get
            Return GetProperty(AddressTypeProperty)
        End Get
        Set(value As String)
            SetProperty(AddressTypeProperty, AddressType)
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
    Public Property PostalCode As String
        Get
            Return GetProperty(PostalCodeProperty)
        End Get
        Set(value As String)
            SetProperty(PostalCodeProperty, value)
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
    Public Shared Function FetchExisting(ByVal prefix As String) As Address
        Return New Address(prefix)
    End Function
    Public Sub Populate(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        If d.ContainsKey("DLR_COUNTRY") Then LoadProperty(CountryProperty, d("DLR_COUNTRY"))
        If String.IsNullOrWhiteSpace(Country) Then Exit Sub
        Dim addrKey As String = ApplicantType & Line1Property.FriendlyName
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
            LoadProperty(StateProperty, Utility.ProvinceOrStateConverter(d(stateKey), Country))
        End If
        PopulateField(CountyProperty, d)
        PopulateField(PostalCodeProperty, d)
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

        d.Add(ApplicantType & Line1Property.FriendlyName, Line1)
        d.Add(ApplicantType & Line2Property.FriendlyName, Line2)
        d.Add(ApplicantType & CityProperty.FriendlyName, City)
        d.Add(ApplicantType & StateProperty.FriendlyName, State)
        d.Add(ApplicantType & PostalCodeProperty.FriendlyName, PostalCode)

        Return d
    End Function
#End Region

#Region "  Business Rules "
    Protected Overrides Sub AddBusinessRules()
        Me.BusinessRules.AddRule(New HasRequiredValue(Line1Property))
        Me.BusinessRules.AddRule(New HasRequiredValue(CityProperty))
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
            Me.InputProperties = New List(Of Core.IPropertyInfo) From {CountryProperty, ApplicationTypeProperty, Line1Property, Line2Property, CityProperty, StateProperty, PostalCodeProperty}
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Address = context.Target
            Dim x As String = t.ReadProperty(Me.PrimaryProperty)
            If PrimaryProperty.Name = "Line1" AndAlso IsInValidValue(x) Then context.AddErrorResult("Validation Error - Street Address is required.")
            If PrimaryProperty.Name = "City" AndAlso IsInValidValue(x) Then context.AddErrorResult("Validation Error - City is required.")
        End Sub
        'Adding a comment...
        Private Function IsInValidValue(ByVal val As String) As Boolean
            Return String.IsNullOrWhiteSpace(val)
        End Function
    End Class
    Public Class IsValidZip
        Inherits Csla.Rules.BusinessRule

        Public Sub New()
            Me.PrimaryProperty = PostalCodeProperty
        End Sub

        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Address = context.Target
            If Not t.Country.StartsWith("C") Then Exit Sub
            If t.PostalCode.Length = 5 Or t.PostalCode.Length = 10 Then context.AddErrorResult("Validation Error - Requires Valid Zip Code Format ##### Or #####-####.")
        End Sub
    End Class
    Public Class IsValidPostalCode
        Inherits Csla.Rules.BusinessRule

        Public Sub New()
            Me.PrimaryProperty = PostalCodeProperty
        End Sub

        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Address = context.Target
            If Not t.Country.StartsWith("U") Then Exit Sub
            If t.PostalCode.Length = 6 Then context.AddErrorResult("Validation Error - Requires Valid Postal Code Format A#A#A# Where A denotes Alphabetical character.")
        End Sub
    End Class
    Public Class CountyRequired
        Inherits Csla.Rules.BusinessRule

        Public Sub New()
            Me.PrimaryProperty = CountyProperty
        End Sub

        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Address = context.Target
            If Not t.Country.StartsWith("U") Then Exit Sub
            If String.IsNullOrWhiteSpace(t.County) Then context.AddErrorResult("Validation Error - US Lease Require County.")
        End Sub
    End Class
#End Region
End Class
