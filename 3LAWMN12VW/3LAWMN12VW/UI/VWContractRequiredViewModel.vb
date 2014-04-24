Imports Caliburn.Micro
Imports System.ComponentModel
Imports System.Windows
Imports _3LAWMN12VW.ValidationRuleData
Imports System.Collections.ObjectModel

Public Class VWContractRequiredViewModel
    Inherits Screen

    Private _masterView As PromptContentViewModel
    Private _applicantID As VWContractApplicantIDViewModel
    Private _eventAggregator As IEventAggregator
    Private _dataContext As ValidationRuleData.ProcessInfo
    Private _validationTree As ValidationRuleSetUIModel

#Region "  Properties "
    Public ReadOnly Property DataContext As ValidationRuleData.ProcessInfo
        Get
            Return _dataContext
        End Get
    End Property
    Public ReadOnly Property ValidationTree As ValidationRuleSetUIModel
        Get
            Return _validationTree
        End Get
    End Property
    Public ReadOnly Property ApplicantID
        Get
            Return _applicantID
        End Get
    End Property
    Public ReadOnly Property LeaseTypeVisibility As Visibility
        Get
            If _dataContext Is Nothing Then Return Visibility.Collapsed
            If Utility.IsLease(_dataContext) Then Return Visibility.Visible
            Return Visibility.Hidden
        End Get
    End Property
    Public ReadOnly Property ProductTypeText As String
        Get
            If _dataContext Is Nothing Then Return ""
            Return _dataContext.ProductType
        End Get
    End Property
    Public ReadOnly Property ApplicationTypes As List(Of String)
        Get
            Return _dataContext.ApplicationTypeList
        End Get
    End Property
#End Region

    Public Sub New(ByVal mv As PromptContentViewModel, evA As IEventAggregator)
        _masterView = mv
        _dataContext = mv.DataContext.GlobalProperty
        _applicantID = New VWContractApplicantIDViewModel(mv.DataContext.ApplicantMgr.Primary.ApplicantName, _dataContext)
        _eventAggregator = evA
    End Sub

    Public Sub ApplicationTypeChange()
        _eventAggregator.Publish(New ApplicationTypeChanged)
    End Sub

    Public Sub Validate(ByVal vt As ValidationRuleOrSet)
        Dim mockVT As New ValidationRuleOrSet()
        mockVT.RuleOrSetName = "HMMM"
        mockVT.Rules.Add(New ValidationRuleOrSet())
        mockVT.Rules(0).RuleOrSetName = "HA HA HA"
        mockVT.Rules(0).Rules.Add(New ValidationRuleOrSet())
        mockVT.Rules(0).Rules(0).RuleOrSetName = "DEPTH!"
        mockVT.Rules(0).Rules.Add(New ValidationRuleOrSet())
        mockVT.Rules(0).Rules(1).RuleOrSetName = "MORE DEPTH!"
        _validationTree = New ValidationRuleSetUIModel(mockVT)
        '_validationTree = New ValidationRuleViewModel(vt, Nothing)
        NotifyOfPropertyChange("")
    End Sub
End Class
