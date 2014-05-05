Imports Csla

Namespace ValidationLib
    Public Class ApplicantID
        Inherits BaseFormField

        'See Inheritance Worker for RegisterPropertyLocal
        Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.ApplicantType), String.Empty, "Primary")
        Public Shared ReadOnly FamilyProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.Family), "_LAST", String.Empty)
        Public Shared ReadOnly MiddleProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.Middle), "_INIT", String.Empty)
        Public Shared ReadOnly GivenProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.Given), "_FIRST", String.Empty)
        Public Shared ReadOnly SuffixProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.Suffix), "_SUFFIX", String.Empty)
        Public Shared ReadOnly BirthDateProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.BirthDate), "_BDTE", String.Empty)
        Public Shared ReadOnly AgeProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer, ApplicantID)(Function(c) (c.Age), "_AGE", -1)
        Public Shared ReadOnly NationalIDProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.NationalID), "_SIN", String.Empty)
        Public Shared ReadOnly IssuingStateProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.IssuingState), "_DLICENSESTATE", String.Empty)
        Public Shared ReadOnly DriverLicenseProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.DriverLicense), "_DLICENSE", String.Empty)
        Public Shared ReadOnly DriverLicenseExpiryProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, ApplicantID)(Function(c) (c.DriverLicenseExpiry), "_DLICENSEEXP", String.Empty)

#Region "  Properties "
        Public ReadOnly Property FullName As String
            Get
                Return Given & " " & Family
            End Get
        End Property
        Public ReadOnly Property ApplicantType As String
            Get
                Return GetProperty(ApplicantTypeProperty)
            End Get
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
        Public Property Age As Integer
            Get
                Return GetProperty(AgeProperty)
            End Get
            Set(value As Integer)
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
        Public Sub New(ByVal gProp As ProcessInfo, ByVal parent As String)
            MyBase.New(gProp)
            LoadProperty(ApplicantTypeProperty, parent)
        End Sub
        Public Shared Function Fetch(appArgs As ApplicantArgs) As ApplicantID
            Dim aid As New ApplicantID(appArgs.GlobalProperty, appArgs.applicantType)
            aid.ReadDictionary(appArgs)
            Return aid
        End Function
        Public Overrides Sub Populate(previousRun As Dictionary(Of String, Object))
            PopulateField(previousRun, SuffixProperty)
        End Sub
        Public Overrides Sub PopulateOverride(currentRun As Dictionary(Of String, Object))
            PopulateField(currentRun, FamilyProperty)
            PopulateField(currentRun, MiddleProperty)
            PopulateField(currentRun, GivenProperty)
            PopulateField(currentRun, BirthDateProperty)
            PopulateField(currentRun, AgeProperty)
            PopulateField(currentRun, NationalIDProperty)
            PopulateField(currentRun, IssuingStateProperty)
            PopulateField(currentRun, DriverLicenseProperty)
            PopulateField(currentRun, DriverLicenseExpiryProperty)
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
            StoreField(FamilyProperty, d)
            StoreField(MiddleProperty, d)
            StoreField(GivenProperty, d)
            StoreField(SuffixProperty, d)
            StoreField(BirthDateProperty, d)
            StoreField(NationalIDProperty, d)
            StoreField(IssuingStateProperty, d)
            StoreField(DriverLicenseProperty, d)
            StoreField(DriverLicenseExpiryProperty, d)
        End Sub
        Public Overrides Function OnGenerateKey(key As String) As String
            Return ApplicantArgs.KeyParent(ApplicantType, GlobalProperty) & key
        End Function
#End Region

#Region "  Business Rules "
        Protected Overrides Sub AddBusinessRules()
            Me.BusinessRules.AddRule(New IsUSHasValidSSN)
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(DriverLicenseExpiryProperty, "Validation Error - Applicant must have a valid Drivers' License Expiry."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(DriverLicenseProperty, "Validation Error - Applicant must have a valid Drivers' License.", False))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(BirthDateProperty, "Validation Error - Applicant must have BirthDate."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(GivenProperty, "Validation Error - Applicant must have a Given Name.", False))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(FamilyProperty, "Validation Error - Applicant must have a Last Name.", False))
        End Sub
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)
            Dim v As New Rule(New ValidationFormat With {.Message = ApplicantType, .BottomLayer = False})
            AttachPropertyRequirements(v)
            validationRoot.Rules.Add(v)
        End Sub
        Public Class IsUSHasValidSSN
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = NationalIDProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As ApplicantID = context.Target
                If Not ProcessUtility.IsCanadian(t.GlobalProperty) Then
                    If t.NationalID.Length <> 9 Then context.AddErrorResult("Validation Error - Applicant must have a valid Social Insurance Number.")
                End If
            End Sub
        End Class
#End Region
    End Class
End Namespace
