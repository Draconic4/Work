Imports Csla

Namespace ValidationLib
    Public Class BusinessApplicantID
        Inherits BaseFormField

        'Implements IReplicableFormField, IValidationTarget

        '    'Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("ApplicantType", String.Empty, String.Empty))
        '    'Public Shared ReadOnly FamilyProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("Family", "_LAST", String.Empty))
        '    'Public Shared ReadOnly MiddleProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("Middle", "_INIT", String.Empty))
        '    'Public Shared ReadOnly GivenProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("Given", "_FIRST", String.Empty))
        '    'Public Shared ReadOnly SuffixProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("Suffix", "_SUFFIX", String.Empty))
        '    'Public Shared ReadOnly BirthDateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("BirthDate", "_BDTE", String.Empty))
        '    'Public Shared ReadOnly AgeProperty As PropertyInfo(Of Integer) = RegisterProperty(Of Integer)(New PropertyInfo(Of Integer)("Age", "_AGE", -1))
        '    'Public Shared ReadOnly NationalIDProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("NationalID", "_SIN", String.Empty))
        '    'Public Shared ReadOnly IssuingStateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("IssuingState", "_DLICENSESTATE", String.Empty))
        '    'Public Shared ReadOnly DriverLicenseProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("DriverLicense", "_DLICENSE", String.Empty))
        '    'Public Shared ReadOnly DriverLicenseExpiryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(New PropertyInfo(Of String)("DriverLicenseExpiry", "_DLICENSEEXP", String.Empty))

        '#Region "  Properties "
        Public ReadOnly Property Name As String
            Get
                Return "Company Name"
            End Get
        End Property
        '    Public ReadOnly Property ApplicantType As String
        '        Get
        '            Return GetProperty(ApplicantTypeProperty)
        '        End Get
        '    End Property
        '    Public Property Family As String
        '        Get
        '            Return GetProperty(FamilyProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(FamilyProperty, value)
        '        End Set
        '    End Property
        '    Public Property Middle As String
        '        Get
        '            Return GetProperty(MiddleProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(MiddleProperty, value)
        '        End Set
        '    End Property
        '    Public Property Given As String
        '        Get
        '            Return GetProperty(GivenProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(GivenProperty, value)
        '        End Set
        '    End Property
        '    Public Property Suffix As String
        '        Get
        '            Return GetProperty(SuffixProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(SuffixProperty, value)
        '        End Set
        '    End Property
        '    Public Property BirthDate As String
        '        Get
        '            Return GetProperty(BirthDateProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(BirthDateProperty, value)
        '        End Set
        '    End Property
        '    Public Property Age As Integer
        '        Get
        '            Return GetProperty(AgeProperty)
        '        End Get
        '        Set(value As Integer)
        '            SetProperty(AgeProperty, value)
        '        End Set
        '    End Property
        '    Public Property NationalID As String
        '        Get
        '            Return GetProperty(NationalIDProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(NationalIDProperty, value)
        '        End Set
        '    End Property
        '    Public Property DriverLicense As String
        '        Get
        '            Return GetProperty(DriverLicenseProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(DriverLicenseProperty, value)
        '        End Set
        '    End Property
        '    Public Property DriverLicenseExpiry As String
        '        Get
        '            Return GetProperty(DriverLicenseExpiryProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(DriverLicenseExpiryProperty, value)
        '        End Set
        '    End Property
        '    Public Property IssuingState As String
        '        Get
        '            Return GetProperty(IssuingStateProperty)
        '        End Get
        '        Set(value As String)
        '            SetProperty(IssuingStateProperty, value)
        '        End Set
        '    End Property
        '#End Region

#Region "  Data Access "
        Public Sub New(ByVal gProp As ProcessInfo, ByVal parent As String)
            MyBase.New(gProp)
        End Sub
        Public Shared Function Fetch(appArgs As ApplicantArgs) As BusinessApplicantID
            Dim bid As New BusinessApplicantID(appArgs.GlobalProperty, ApplicantArgs.KeyParent(appArgs.applicantType, appArgs.GlobalProperty))
            Return bid
        End Function
#End Region
        '    Public Shared Function Fetch(appArgs As ApplicantArgs) As BusinessApplicantID
        '        Dim bid As New BusinessApplicantID(appArgs.globalProperty, ApplicantArgs.KeyParent(appArgs.applicantType, appArgs.globalProperty))
        '        FormFieldManager.Populate(bid, appArgs.previousRun, appArgs.currentRun)
        '        Return bid
        '    End Function
        '    Public Sub Calculate(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object)) Implements IReplicableFormField.Calculate
        '        'Do Nothing for Applicant ID.
        '    End Sub
        '    Public Sub Populate(previousRun As Dictionary(Of String, Object)) Implements IReplicableFormField.Populate
        '        PopulateField(SuffixProperty, previousRun)
        '    End Sub
        '    Public Sub PopulateOverride(currentRun As Dictionary(Of String, Object)) Implements IReplicableFormField.PopulateOverride
        '        PopulateField(FamilyProperty, currentRun)
        '        PopulateField(MiddleProperty, currentRun)
        '        PopulateField(GivenProperty, currentRun)
        '        PopulateField(BirthDateProperty, currentRun)
        '        PopulateField(AgeProperty, currentRun)
        '        PopulateField(NationalIDProperty, currentRun)
        '        PopulateField(IssuingStateProperty, currentRun)
        '        PopulateField(DriverLicenseProperty, currentRun)
        '        PopulateField(DriverLicenseExpiryProperty, currentRun)
        '    End Sub
        '    Public Sub ReplicateCurrentState(d As Dictionary(Of String, Object)) Implements IReplicableFormField.ReplicateCurrentState
        '        d.Add(GenerateKey(FamilyProperty), Family)
        '        d.Add(GenerateKey(MiddleProperty), Middle)
        '        d.Add(GenerateKey(GivenProperty), Given)
        '        d.Add(GenerateKey(SuffixProperty), Suffix)
        '    End Sub
        '    Public Overrides Function OnGenerateKey(key As String) As String
        '        Return ApplicantType & key
        '    End Function
        '#End Region

        '#Region "  Business Rules "
        '    Protected Overrides Sub AddBusinessRules()
        '        Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(FamilyProperty, "Validation Error - Applicant must have a Last Name."))
        '        Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(GivenProperty, "Validation Error - Applicant must have a Given Name."))
        '        Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(BirthDateProperty, "Validation Error - Applicant must have BirthDate."))
        '        Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(DriverLicenseProperty, "Validation Error - Applicant must have a valid Drivers' License."))
        '        Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(DriverLicenseExpiryProperty, "Validation Error - Applicant must have a valid Drivers' License Expiry."))
        '        Me.BusinessRules.AddRule(New IsUSHasValidSSN)
        '    End Sub
        '    Public Sub CheckRules() Implements IValidationTarget.CheckRules
        '        Me.BusinessRules.CheckRules()
        '    End Sub
        '    Public Sub Requirement(validationRoot As Rule) Implements IValidationTarget.Requirement
        '        Dim v As New Rule(ApplicantType, False)
        '        AttachPropertyRequirements(v)
        '        validationRoot.Rules.Add(v)
        '    End Sub
        '    Public Class IsUSHasValidSSN
        '        Inherits Csla.Rules.BusinessRule

        '        Public Sub New()
        '            Me.PrimaryProperty = NationalIDProperty
        '        End Sub
        '        Protected Overrides Sub Execute(context As Rules.RuleContext)
        '            Dim t As ApplicantID = context.Target
        '            If Not ProcessUtility.IsCanadian(t.GlobalProperty) Then
        '                If t.NationalID.Length >= 9 Then context.AddErrorResult("Validation Error - Applicant must have a valid Social Insurance Number.")
        '            End If
        '        End Sub
        '    End Class
        '#End Region

        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub

        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
    End Class
End Namespace
