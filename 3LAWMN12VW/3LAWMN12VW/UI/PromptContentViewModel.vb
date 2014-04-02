Imports Caliburn.Micro
Imports System.ComponentModel

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.OneActive
    Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage), IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage)

    Private ReadOnly _eventAggregator As IEventAggregator

    Private _yahtz As String = "Go Home"
    Private _dataContext As VWCreditProcess
    Private _detailView As VWContractRequiredViewModel
    Public Property DataContext As VWCreditProcess
        Get
            Return _dataContext
        End Get
        Set(value As VWCreditProcess)
            _dataContext = value
        End Set
    End Property
    Public Property Yahtzee As String
        Get
            Return _yahtz
        End Get
        Set(value As String)
            _yahtz = value
            Me.NotifyOfPropertyChange("Yahtzee")
        End Set
    End Property

    Public Sub RequiredClicked()
        ChangeView(_detailView)
    End Sub
    Private Sub ChangeView(ByVal newView As Screen)
        'If Me.ActiveItem Is VWContractRequiredViewModel Then
        '    'Warn and require user to select these options.
        'End If
        Me.ActiveItem = newView
        Me.NotifyOfPropertyChange("RequiredChecked")
    End Sub
    Public ReadOnly Property RequiredChecked()
        Get
            Return Me.ActiveItem.GetType() Is GetType(VWContractRequiredViewModel)
        End Get
    End Property
    Public Sub New(ByVal previousDC As Dictionary(Of String, Object), ByVal arDC As Dictionary(Of String, Object), eventAggregator As IEventAggregator)
        _dataContext = VWCreditProcess.FetchExisting(previousDC, arDC)
        _detailView = New VWContractRequiredViewModel(Me) 'Me.Items.Add(New VWContractRequiredViewModel(Me))
        _eventAggregator = eventAggregator
        _eventAggregator.Subscribe(Me)
        Me.ActiveItem = _detailView
    End Sub

    Public Sub Handle(message As PBS.Deals.FormsIntegration.BeginValidationMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage).Handle
        Yahtzee = "Hello Nurse"
    End Sub

    Public Sub Handle1(message As PBS.Deals.FormsIntegration.BeginDataCollectMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage).Handle
        Yahtzee = "HAHAHA I AM ALL POWERFUL"
    End Sub
End Class
