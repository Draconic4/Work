Imports Csla
Imports System.Collections.ObjectModel

Namespace ValidationRuleData
    Public Class VWCreditProcess
        Inherits Csla.BusinessBase(Of VWCreditProcess)

        Private _globalProperties As ProcessInfo
        Private _validationTree As ValidationRuleOrSet
        Private _applicantMgr As ApplicantManager
        Private _vehicle As Vehicle
        Private _deal As Deal

#Region "  Properties "
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperties
            End Get
        End Property
        Public ReadOnly Property ValidationTree As ValidationRuleOrSet
            Get
                Return _validationTree
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
        Public ReadOnly Property Details As Deal
            Get
                Return _deal
            End Get
        End Property
#End Region

        Public Sub New()
        End Sub

        Public Shared Function Fetch(ByVal formDC As Dictionary(Of String, Object), ByVal aristoDC As Dictionary(Of String, Object)) As VWCreditProcess
            Dim dp As New VWCreditProcess
            dp._globalProperties = ProcessInfo.Fetch(formDC, aristoDC)
            dp._applicantMgr = ApplicantManager.Fetch(dp.GlobalProperty, formDC, aristoDC)
            dp._vehicle = Vehicle.Fetch(dp.GlobalProperty, formDC, aristoDC)
            dp._deal = Deal.Fetch(dp.GlobalProperty, formDC, aristoDC)
            Return dp
        End Function

#Region "  Business Rules "
        Public Sub CheckRules(ByVal aristo As Boolean)
            _applicantMgr.CheckRules(aristo)
            '_vehicle.CheckRules(aristo)
            '_deal.CheckRules(aristo)
            RequirementList()
        End Sub
        Public Sub RequirementList()
            Dim x As New ValidationRuleOrSet()
            _applicantMgr.RequirementList(x)
            _validationTree = x
        End Sub
#End Region
    End Class
End Namespace