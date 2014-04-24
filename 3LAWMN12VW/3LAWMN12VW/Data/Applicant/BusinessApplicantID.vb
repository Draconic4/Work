Imports Csla

Namespace ValidationRuleData
    Public Class BusinessApplicantID
        Inherits BusinessBase(Of BusinessApplicantID)

        Private _globalProperties As ProcessInfo

        Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicantType))
        Public Shared ReadOnly CompanyProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.CompanyName), "_COMPANY", String.Empty)
        Public Shared ReadOnly NationalIDProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.NationalID), "_GSTNUMBER", String.Empty)
        Public Shared ReadOnly IssuingStateProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.IssuingState), "_DLIC_STATE", String.Empty)
        Public Shared ReadOnly DriverLicenseProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DriverLicense), "_DLIC_NO", String.Empty)
        Public Shared ReadOnly DriverLicenseExpiryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DriverLicenseExpiry), "_DLIC_EXP", String.Empty)

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
        Public ReadOnly Property CompanyName As String
            Get
                Return GetProperty(CompanyProperty)
            End Get
        End Property
        Public ReadOnly Property NationalID As String
            Get
                Return GetProperty(NationalIDProperty)
            End Get
        End Property
        Public ReadOnly Property DriverLicense As String
            Get
                Return GetProperty(DriverLicenseProperty)
            End Get
        End Property
        Public ReadOnly Property DriverLicenseExpiry As String
            Get
                Return GetProperty(DriverLicenseExpiryProperty)
            End Get
        End Property
        Public ReadOnly Property IssuingState As String
            Get
                Return GetProperty(IssuingStateProperty)
            End Get
        End Property
#End Region

#Region "  Data Access "
        Public Sub New(ByVal parent As String, gProp As ProcessInfo)
            LoadProperty(ApplicantTypeProperty, parent)
        End Sub
        Public Shared Function Fetch(ByVal keyParent As String, gProp As ProcessInfo, ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As BusinessApplicantID
            Dim aid As New BusinessApplicantID(keyParent, gProp)
            If currentRun Is Nothing Then Return aid
            aid.Populate(previousRun)
            aid.PopulateOverride(currentRun)
            aid.Calculate(previousRun, currentRun)
            Return aid
        End Function
        Public Sub Populate(ByVal pRun As Dictionary(Of String, Object))
            If pRun Is Nothing Then Exit Sub
        End Sub
        Public Sub PopulateOverride(ByVal cRun As Dictionary(Of String, Object))
            If cRun Is Nothing Then Exit Sub
            PopulateField(CompanyProperty, cRun)
            PopulateField(NationalIDProperty, cRun)
            PopulateField(IssuingStateProperty, cRun)
            PopulateField(DriverLicenseProperty, cRun)
            PopulateField(DriverLicenseExpiryProperty, cRun)
        End Sub
        Public Sub Calculate(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            'Do Nothing.... Nothing to Calculate
        End Sub
        Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal d As Dictionary(Of String, Object))
            Dim key As String = ApplicantType & pi.FriendlyName
            Dim xVal As String = String.Empty
            If d.TryGetValue(key, xVal) Then LoadProperty(pi, xVal)
        End Sub
        Public Sub PopulateField(ByVal pi As PropertyInfo(Of Integer), ByVal d As Dictionary(Of String, Object))
            Dim key As String = ApplicantType & pi.FriendlyName
            Dim xVal As Integer = 0
            If d.TryGetValue(key, xVal) Then LoadProperty(pi, xVal)
        End Sub
        Function SaveData() As Dictionary(Of String, Object)
            Dim d As New Dictionary(Of String, Object)

            d.Add(ApplicantType & CompanyProperty.FriendlyName, CompanyName)
            d.Add(ApplicantType & NationalIDProperty.FriendlyName, NationalID)
            d.Add(ApplicantType & IssuingStateProperty.FriendlyName, IssuingState)
            d.Add(ApplicantType & DriverLicenseProperty.FriendlyName, DriverLicense)
            d.Add(ApplicantType & DriverLicenseExpiryProperty.FriendlyName, DriverLicenseExpiry)

            Return d
        End Function

        Public Function GetPropertyValue(ByVal pi As PropertyInfo(Of String)) As String
            Return Me.ReadProperty(pi)
        End Function
#End Region

#Region "  Business Rules "
        Protected Overrides Sub AddBusinessRules()
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(CompanyProperty, "Validation Error - Business Applicant must have a Name."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(DriverLicenseProperty, "Validation Error - Applicant must have a valid Drivers' License."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(DriverLicenseExpiryProperty, "Validation Error - Applicant must have a valid Drivers' License Expiry."))
            Me.BusinessRules.AddRule(New IsUSHasValidSSN)
        End Sub
        Public Sub CheckRules()
            Me.BusinessRules.CheckRules()
        End Sub
        Public Sub RequirementList(vroot As ValidationRuleOrSet)
            'For Each vr As Csla.Rules.BrokenRule In Me.BrokenRulesCollection
            '    vroot.Rules.Add(New ValidationRule(vr.Description))
            'Next
        End Sub
        Public Class IsUSHasValidSSN
            Inherits Csla.Rules.BusinessRule

            Public Sub New()
                Me.PrimaryProperty = NationalIDProperty
            End Sub
            Protected Overrides Sub Execute(context As Rules.RuleContext)
                Dim t As ApplicantID = context.Target
                If Not Utility.IsCanadian(t.GlobalProperty) Then
                    If t.NationalID.Length >= 9 Then context.AddErrorResult("Validation Error - Applicant must have a valid Social Insurance Number.")
                End If
            End Sub
        End Class
#End Region
    End Class
End Namespace
