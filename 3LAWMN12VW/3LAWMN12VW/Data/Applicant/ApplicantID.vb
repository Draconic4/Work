﻿Imports Csla

Namespace ValidationRuleData
    Public Class ApplicantID
        Inherits BusinessBase(Of ApplicantID)

        Private _globalProperties As ProcessInfo

        Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of KeyBindInfo) = RegisterProperty(Of KeyBindInfo)(Function(c) (c.ApplicantType))
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
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperties
            End Get
        End Property
        Public ReadOnly Property FullName As String
            Get
                Return Given & " " & Family
            End Get
        End Property
        Public ReadOnly Property ApplicantType As KeyBindInfo
            Get
                Return GetProperty(ApplicantTypeProperty)
            End Get
        End Property
        Public ReadOnly Property Family As String
            Get
                Return GetProperty(FamilyProperty)
            End Get
        End Property
        Public ReadOnly Property Middle As String
            Get
                Return GetProperty(MiddleProperty)
            End Get
        End Property
        Public ReadOnly Property Given As String
            Get
                Return GetProperty(GivenProperty)
            End Get
        End Property
        Public Property Suffix As String
            Get
                Return GetProperty(SuffixProperty)
            End Get
            Set(value As String)
                SetProperty(SuffixProperty, value)
            End Set
        End Property
        Public ReadOnly Property BirthDate As String
            Get
                Return GetProperty(BirthDateProperty)
            End Get
        End Property
        Public ReadOnly Property Age As String
            Get
                Return GetProperty(AgeProperty)
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
        Public Shared Function Fetch(ByVal keyParent As String, gProp As ProcessInfo, ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As ApplicantID
            Dim aid As New ApplicantID(keyParent, gProp)
            If currentRun Is Nothing Then Return aid
            aid.Populate(previousRun)
            aid.PopulateOverride(currentRun)
            aid.Calculate(previousRun, currentRun)
            Return aid
        End Function
        Public Sub Populate(ByVal pRun As Dictionary(Of String, Object))
            If pRun Is Nothing Then Exit Sub
            PopulateField(SuffixProperty, pRun)
        End Sub
        Public Sub PopulateOverride(ByVal cRun As Dictionary(Of String, Object))
            If cRun Is Nothing Then Exit Sub
            PopulateField(FamilyProperty, cRun)
            PopulateField(MiddleProperty, cRun)
            PopulateField(GivenProperty, cRun)
            PopulateField(BirthDateProperty, cRun)
            PopulateField(AgeProperty, cRun)
            PopulateField(NationalIDProperty, cRun)
            PopulateField(IssuingStateProperty, cRun)
            PopulateField(DriverLicenseProperty, cRun)
            PopulateField(DriverLicenseExpiryProperty, cRun)
        End Sub
        Public Sub Calculate(ByVal pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            'Do Nothing.... Nothing to Calculate
        End Sub
        Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal d As Dictionary(Of String, Object))
            Dim key As String = ApplicantType.KeyValue & pi.FriendlyName
            Dim xVal As String = String.Empty
            If d.TryGetValue(key, xVal) Then LoadProperty(pi, xVal)
        End Sub
        Function SaveData() As Dictionary(Of String, Object)
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
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(FamilyProperty, "Validation Error - Applicant must have a Last Name."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(GivenProperty, "Validation Error - Applicant must have a Given Name."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(BirthDateProperty, "Validation Error - Applicant must have BirthDate."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueString(DriverLicenseProperty, "Validation Error - Applicant must have a valid Drivers' License."))
            Me.BusinessRules.AddRule(New Utility.HasRequiredValueDate(DriverLicenseExpiryProperty, "Validation Error - Applicant must have a valid Drivers' License Expiry."))
            Me.BusinessRules.AddRule(New IsUSHasValidSSN)
        End Sub
        Public Sub CheckRules()
            Me.BusinessRules.CheckRules()
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
