Imports Csla
Namespace ValidationRuleData
    Public Class VWCreditProcess
        Inherits Csla.BusinessBase(Of VWCreditProcess)

        Private _globalProperties As ProcessInfo
        Private _applicantMgr As ApplicantManager
        Private _vehicle As Vehicle

#Region "  Properties "
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperties
            End Get
        End Property
        Public ReadOnly Property ApplicantMgr As ApplicantManager
            Get
                Return _applicantMgr
            End Get
        End Property
        Public ReadOnly Property Vehicle As Vehicle
            Get
                Return _vehicle
            End Get
        End Property
#End Region

        Public Sub New()
        End Sub

        Public Shared Function Fetch(ByVal formDC As Dictionary(Of String, Object), ByVal aristoDC As Dictionary(Of String, Object)) As VWCreditProcess
            Dim dp As New VWCreditProcess
            dp._globalProperties = ProcessInfo.Fetch(formDC, aristoDC)
            dp._applicantMgr = ApplicantManager.Fetch(dp.GlobalProperty, formDC, aristoDC)
            dp._vehicle = Vehicle.FetchExisting(formDC, aristoDC, dp.GlobalProperty)
            Return dp
        End Function

#Region "  Business Rules "
        Public Sub CheckRules()
            _applicantMgr.CheckRules()
            _vehicle.CheckRules()
        End Sub
#End Region
    End Class
End Namespace