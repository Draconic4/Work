Imports Csla

Public Class ApplicantID
    Inherits BusinessBase(Of ApplicantID)

    Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicantType))
    Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "COUNTRY", "US")
    Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicationType), "_APPLICANTTYPE", "INDIV")
    Public Shared ReadOnly FamilyProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Family), "_LASTNAME", String.Empty)
    Public Shared ReadOnly GivenProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Given), "_FIRSTNAME", String.Empty)
    Public Shared ReadOnly SSNProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.SSN), "_SIN", String.Empty)
    Public Shared ReadOnly SINProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.SIN), "_SIN", String.Empty)
    Public Shared ReadOnly IssuingStateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.IssuingState), "_DLIC_STATE", String.Empty)
    Public Shared ReadOnly DriverLicenseProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DriverLicense), "_DLIC_NO", String.Empty)
    Public Shared ReadOnly DriverLicenseExpiryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DriverLicenseExpiry), "_DLIC_EXP", String.Empty)

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
            Return GetProperty(ApplicationTypeProperty)
        End Get
        Set(value As String)
            SetProperty(ApplicationTypeProperty, value)
        End Set
    End Property
    Public Property Family As String
        Get
            Return GetProperty(FamilyProperty)
        End Get
        Set(value As String)
            SetProperty(FamilyProperty, value)
        End Set
    End Property
    Public Property Given As String
        Get
            Return GetProperty(GivenProperty)
        End Get
        Set(value As String)
            SetProperty(GivenProperty, value)
        End Set
    End Property
    Public Property SSN As String
        Get
            Return GetProperty(SSNProperty)
        End Get
        Set(value As String)
            SetProperty(SSNProperty, value)
        End Set
    End Property
    Public Property SIN As String
        Get
            Return GetProperty(SINProperty)
        End Get
        Set(value As String)
            SetProperty(SINProperty, value)
        End Set
    End Property
    Public Property DriverLicense As String
        Get
            Return GetProperty(DriverLicenseProperty)
        End Get
        Set(value As String)
            SetProperty(DriverLicenseProperty, value)
        End Set
    End Property
    Public Property DriverLicenseExpiry As String
        Get
            Return GetProperty(DriverLicenseExpiryProperty)
        End Get
        Set(value As String)
            SetProperty(DriverLicenseExpiryProperty, value)
        End Set
    End Property
    Public Property IssuingState As String
        Get
            Return GetProperty(IssuingStateProperty)
        End Get
        Set(value As String)
            SetProperty(IssuingStateProperty, value)
        End Set
    End Property
#End Region

#Region "  Data Access "
    Public Sub New(ByVal prefix As String)
        LoadProperty(ApplicantTypeProperty, prefix)
    End Sub
    Public Shared Function FetchExisting(ByVal prefix As String) As ApplicantID
        Return New ApplicantID(prefix)
    End Function
    Public Sub Populate(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        If d.ContainsKey("DLR_COUNTRY") Then LoadProperty(CountryProperty, d("DLR_COUNTRY"))
        If String.IsNullOrWhiteSpace(Country) Then Exit Sub
        PopulateField(FamilyProperty, d)
        PopulateField(GivenProperty, d)
        If Country.StartsWith("U") Then PopulateField(SSNProperty, d)
        PopulateField(SINProperty, d)
        PopulateField(IssuingStateProperty, d)
        PopulateField(DriverLicenseProperty, d)
        PopulateField(DriverLicenseExpiryProperty, d)
    End Sub
    Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal d As Dictionary(Of String, Object))
        Dim key As String = ApplicantType & pi.FriendlyName
        If d.ContainsKey(key) Then LoadProperty(pi, d(key))
    End Sub
    Public Function SerializeField(ByVal pi As PropertyInfo(Of String)) As Dictionary(Of String, Object)
        Dim d As New Dictionary(Of String, Object)

        d.Add(ApplicantType & FamilyProperty.FriendlyName, Family)
        d.Add(ApplicantType & GivenProperty.FriendlyName, Given)
        If Country.StartsWith("U") Then d.Add(ApplicantType & SSNProperty.FriendlyName, SSN)
        d.Add(ApplicantType & SINProperty.FriendlyName, SIN)
        d.Add(ApplicantType & IssuingStateProperty.FriendlyName, IssuingState)
        d.Add(ApplicantType & DriverLicenseProperty.FriendlyName, DriverLicense)
        d.Add(ApplicantType & DriverLicenseExpiryProperty.FriendlyName, DriverLicenseExpiry)

        Return d
    End Function
#End Region

#Region "  Business Rules "
    Protected Overrides Sub AddBusinessRules()
        Me.BusinessRules.AddRule(New HasRequiredValue)
        Me.BusinessRules.AddRule(New IsUSHasValidSSN)
    End Sub
    Public Sub CheckRules()
        Me.BusinessRules.CheckRules()
    End Sub
    Public Class HasRequiredValue
        Inherits Csla.Rules.BusinessRule
        Public Sub New()
            Me.InputProperties = New List(Of Core.IPropertyInfo) From {CountryProperty, ApplicationTypeProperty, FamilyProperty, GivenProperty, DriverLicenseProperty, SSNProperty}
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As ApplicantID = context.Target
            If HasValidValue(t.Family) Then context.AddErrorResult("Validation Error - Last Name is required.")
            If HasValidValue(t.Given) Then context.AddErrorResult("Validation Error - First Name is required.")
            'If HasValidValue(t.DriverLicense) Then context.AddErrorResult("Validation Error - Driver License is required.")
        End Sub
        Private Function HasValidValue(ByVal val As String) As Boolean
            Return String.IsNullOrWhiteSpace(val)
        End Function
    End Class
    Public Class IsUSHasValidSSN
        Inherits Csla.Rules.BusinessRule

        Public Sub New()
            Me.PrimaryProperty = SSNProperty
            Me.InputProperties = New List(Of Csla.Core.IPropertyInfo) From {CountryProperty}
        End Sub

        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As ApplicantID = context.Target
            If t.Country.StartsWith("U") Then
                If t.SSN.Length >= 9 Then context.AddErrorResult("Validation Error - Applicant must have a valid Social Insurance Number.")
            End If
        End Sub
    End Class
#End Region
End Class
