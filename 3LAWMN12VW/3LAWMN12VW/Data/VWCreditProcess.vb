Imports Csla

Public Class VWCreditProcess
    Inherits Csla.BusinessBase(Of VWCreditProcess)

    Private _globalProperties As ProcessInfo
    Private _applicantMgr As ApplicantManager

#Region "  Properties "
    Public ReadOnly Property GlobalProperty As ProcessInfo
        Get
            Return _globalProperties
        End Get
    End Property
    Public ReadOnly Property PrimaryApplicant As Applicant
        Get
            Return _applicantMgr.Primary
        End Get
    End Property
    Public ReadOnly Property ApplicantMgr As ApplicantManager
        Get
            Return _applicantMgr
        End Get
    End Property
#End Region

    Public Sub New()
    End Sub

    Public Shared Function FetchExisting(ByVal formDC As Dictionary(Of String, Object), ByVal aristoDC As Dictionary(Of String, Object)) As VWCreditProcess
        Dim dp As New VWCreditProcess
        dp._globalProperties = ProcessInfo.FetchExisting()
        dp._globalProperties.Populate(formDC)
        dp._globalProperties.Populate(aristoDC)
        dp._applicantMgr = ApplicantManager.Fetch(formDC, aristoDC, dp.GlobalProperty)
        Return dp
    End Function

#Region "  Business Rules "
    Public Sub CheckRules()
        '.CheckRules()
    End Sub
#End Region
End Class
