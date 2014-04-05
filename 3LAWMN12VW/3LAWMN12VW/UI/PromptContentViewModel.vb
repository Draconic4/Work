Imports Caliburn.Micro
Imports System.ComponentModel

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.OneActive
    Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage), IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage)

    Private ReadOnly _eventAggregator As IEventAggregator

    Private _dataContext As VWCreditProcess
    Private _detailView As VWContractRequiredViewModel
    Private _primaryApplicant As VWContractPrimaryApplicantViewModel
    Private _testTab As TestTabViewModel

    Public Property DataContext As VWCreditProcess
        Get
            Return _dataContext
        End Get
        Set(value As VWCreditProcess)
            _dataContext = value
        End Set
    End Property

    Public Sub RequiredClicked()
        ChangeView(_detailView)
    End Sub
    Public Sub ApplicantClicked()
        ChangeView(_primaryApplicant)
    End Sub
    Public Sub TestClicked()
        ChangeView(_testTab)
    End Sub
    Private Sub ChangeView(ByVal newView As Screen)
        'If Me.ActiveItem Is VWContractRequiredViewModel Then
        '    'Warn and require user to select these options.
        'End If
        Me.ActiveItem = newView
        Me.NotifyOfPropertyChange("RequiredChecked")
        Me.NotifyOfPropertyChange("ApplicantChecked")
        Me.NotifyOfPropertyChange("TestChecked")
    End Sub
    Public ReadOnly Property RequiredChecked()
        Get
            Return Me.ActiveItem.GetType() Is GetType(VWContractRequiredViewModel)
        End Get
    End Property
    Public ReadOnly Property ApplicantChecked()
        Get
            Return Me.ActiveItem.GetType() Is GetType(VWContractPrimaryApplicantViewModel)
        End Get
    End Property
    Public ReadOnly Property TestChecked()
        Get
            Return Me.ActiveItem.GetType() Is GetType(TestTabViewModel)
        End Get
    End Property
    Public Sub New(ByVal previousDC As Dictionary(Of String, Object), ByVal arDC As Dictionary(Of String, Object), eventAggregator As IEventAggregator)
        _dataContext = VWCreditProcess.FetchExisting(previousDC, arDC)
        _detailView = New VWContractRequiredViewModel(Me)
        _primaryApplicant = New VWContractPrimaryApplicantViewModel(Me)
        _testTab = New TestTabViewModel(Me)
        _eventAggregator = eventAggregator
        _eventAggregator.Subscribe(Me)
        Me.ActiveItem = _detailView
    End Sub

    Public Sub Handle(message As PBS.Deals.FormsIntegration.BeginValidationMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage).Handle
        _dataContext.CheckRules()
    End Sub

    Public Sub Handle1(message As PBS.Deals.FormsIntegration.BeginDataCollectMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage).Handle
    End Sub
End Class
