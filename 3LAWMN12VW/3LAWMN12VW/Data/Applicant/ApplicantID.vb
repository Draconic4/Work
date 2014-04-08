Imports Csla

Namespace ValidationRuleData
    Public Class ApplicantID
        Inherits BusinessBase(Of ApplicantID)

        Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicationType), "_APPLICANTTYPE", "INDIV")
        Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of KeyBindInfo) = RegisterProperty(Of KeyBindInfo)(Function(c) (c.ApplicantType))
        Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "COUNTRY", "US")
        Public Shared ReadOnly FamilyProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Family), "_LASTNAME", String.Empty)
        Public Shared ReadOnly MiddleProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Middle), "_INITNAME", String.Empty)
        Public Shared ReadOnly GivenProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Given), "_FIRSTNAME", String.Empty)
        Public Shared ReadOnly SuffixProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Suffix), "_SUFFIX", String.Empty)
        Public Shared ReadOnly BirthDateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.BirthDate), "_BIRTHDATE", String.Empty)
        Public Shared ReadOnly AgeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Age), "_AGE", String.Empty)
        Public Shared ReadOnly NationalIDProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.NationalID), "_SIN", String.Empty)
        Public Shared ReadOnly IssuingStateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.IssuingState), "_DLIC_STATE", String.Empty)
        Public Shared ReadOnly DriverLicenseProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DriverLicense), "_DLIC_NO", String.Empty)
        Public Shared ReadOnly DriverLicenseExpiryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DriverLicenseExpiry), "_DLIC_EXP", String.Empty)

#Region "  Properties "
        Public ReadOnly Property FullName As String
            Get
                Return Given & " " & Family
            End Get
        End Property
        Public ReadOnly Property Country As String
            Get
                Return GetProperty(CountryProperty)
            End Get
        End Property
        Public ReadOnly Property ApplicantType As KeyBindInfo
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
        Public Property Middle As String
            Get
                Return GetProperty(MiddleProperty)
            End Get
            Set(value As String)
                SetProperty(MiddleProperty, value)
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
        Public Property Suffix As String
            Get
                Return GetProperty(SuffixProperty)
            End Get
            Set(value As String)
                SetProperty(SuffixProperty, value)
            End Set
        End Property
        Public Property BirthDate As String
            Get
                Return GetProperty(BirthDateProperty)
            End Get
            Set(value As String)
                SetProperty(BirthDateProperty, value)
            End Set
        End Property
        Public Property Age As String
            Get
                Return GetProperty(AgeProperty)
            End Get
            Set(value As String)
                SetProperty(AgeProperty, value)
            End Set
        End Property
        Public Property NationalID As String
            Get
                Return GetProperty(NationalIDProperty)
            End Get
            Set(value As String)
                SetProperty(NationalIDProperty, value)
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
        Public Sub New(ByVal parent As KeyBindInfo)
            LoadProperty(ApplicantTypeProperty, parent)
        End Sub
        Public Shared Function FetchExisting(ByVal keyParent As KeyBindInfo) As ApplicantID
            Return New ApplicantID(keyParent)
        End Function
        Public Sub Populate(ByVal d As Dictionary(Of String, Object))
            If d Is Nothing Then Exit Sub
            If d.ContainsKey("DLR_COUNTRY") Then LoadProperty(CountryProperty, d("DLR_COUNTRY"))
            If String.IsNullOrWhiteSpace(Country) Then Exit Sub
            PopulateField(FamilyProperty, d)
            PopulateField(MiddleProperty, d)
            PopulateField(GivenProperty, d)
            PopulateField(SuffixProperty, d)
            PopulateField(BirthDateProperty, d)
            PopulateField(AgeProperty, d)
            PopulateField(NationalIDProperty, d)
            PopulateField(IssuingStateProperty, d)
            PopulateField(DriverLicenseProperty, d)
            PopulateField(DriverLicenseExpiryProperty, d)
        End Sub
        Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal d As Dictionary(Of String, Object))
            Dim key As String = ApplicantType.KeyValue & pi.FriendlyName
            If d.ContainsKey(key) Then LoadProperty(pi, d(key))
        End Sub
        Public Function SerializeField(ByVal pi As PropertyInfo(Of String)) As Dictionary(Of String, Object)
            Dim d As New Dictionary(Of String, Object)

            d.Add(ApplicantType.KeyValue & FamilyProperty.FriendlyName, Family)
            d.Add(ApplicantType.KeyValue & MiddleProperty.FriendlyName, Middle)
            d.Add(ApplicantType.KeyValue & GivenProperty.FriendlyName, Given)
            d.Add(ApplicantType.KeyValue & SuffixProperty.FriendlyName, Suffix)
            d.Add(ApplicantType.KeyValue & BirthDateProperty.FriendlyName, BirthDate)
            d.Add(ApplicantType.KeyValue & AgeProperty.FriendlyName, Age)
            d.Add(ApplicantType.KeyValue & NationalIDProperty.FriendlyName, NationalID)
            d.Add(ApplicantType.KeyValue & IssuingStateProperty.FriendlyName, IssuingState)
            d.Add(ApplicantType.KeyValue & DriverLicenseProperty.FriendlyName, DriverLicense)
            d.Add(ApplicantType.KeyValue & DriverLicenseExpiryProperty.FriendlyName, DriverLicenseExpiry)

            Return d
        End Function
        Public Function GetPropertyValue(ByVal pi As PropertyInfo(Of String)) As String
            Return Me.ReadProperty(pi)
        End Function
#End Region

#Region "  Business Rules "
        Protected Overrides Sub AddBusinessRules()
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(FamilyProperty, ""))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(GivenProperty, ""))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(BirthDateProperty, ""))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(DriverLicenseProperty, ""))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(DriverLicenseExpiryProperty, ""))
            Me.BusinessRules.AddRule(New IsUSHasValidSSN)
        End Sub
        Public Sub CheckRules()
            Me.BusinessRules.CheckRules()
        End Sub

        Public Class IsUSHasValidSSN
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = NationalIDProperty
                Me.InputProperties = New List(Of Csla.Core.IPropertyInfo) From {CountryProperty}
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As ApplicantID = context.Target
                If t.Country.StartsWith("U") Then
                    If t.NationalID.Length >= 9 Then context.AddErrorResult("Validation Error - Applicant must have a valid Social Insurance Number.")
                End If
            End Sub
        End Class
#End Region
    End Class
End Namespace
