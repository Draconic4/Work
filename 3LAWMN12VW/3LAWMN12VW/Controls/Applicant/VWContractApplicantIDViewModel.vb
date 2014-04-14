Imports Caliburn.Micro

Public Class VWContractApplicantIDViewModel
    Inherits Screen

    Private _Applicant As ValidationRuleData.ApplicantID
    Private _globalProperties As ValidationRuleData.ProcessInfo

    Public ReadOnly Property Applicant As ValidationRuleData.ApplicantID
        Get
            Return _Applicant
        End Get
    End Property
    Public ReadOnly Property GlobalProperty As ValidationRuleData.ProcessInfo
        Get
            Return _globalProperties
        End Get
    End Property
    Public ReadOnly Property SuffixList As List(Of String)
        Get
            Dim result As New List(Of String)
            result.Add("")
            result.Add("JR")
            result.Add("SR")
            result.Add("I")
            result.Add("II")
            result.Add("III")
            result.Add("IV")
            result.Add("V")
            Return result
        End Get
    End Property
    Public ReadOnly Property NationalIdText As String
        Get
            If Utility.IsCanadian(GlobalProperty) Then Return "SIN"
            Return "SSN"
        End Get
    End Property
    Public ReadOnly Property DriversIssuingProvinceText As String
        Get
            If Utility.IsCanadian(GlobalProperty) Then Return "Issuing Province"
            Return "Issuing State"
        End Get
    End Property
    Public ReadOnly Property ProvinceOrStateList() As List(Of String)
        Get
            Return Utility.GetLocaleList(GlobalProperty)
        End Get
    End Property
    Public Sub New(ByVal appId As ValidationRuleData.ApplicantID, globalProperties As ValidationRuleData.ProcessInfo)
        _Applicant = appId
        _globalProperties = globalProperties
    End Sub
    Public Sub Validate()
        _Applicant.CheckRules()
        NotifyOfPropertyChange("")
    End Sub
End Class
