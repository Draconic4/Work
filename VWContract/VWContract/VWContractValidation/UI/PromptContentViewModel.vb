Imports Caliburn.Micro
Imports System.Windows
Imports VWContractValidation.ValidationLib

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.OneActive
    Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage), IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage)

    Private _dataContext As VWCreditProcess
    Private _eventAggregator As IEventAggregator

    Private _validationBegun As Boolean = False
    Private _validated As Boolean = False

    Private _promptContent As ContractPromptViewModel
    Private _validationContent As ValidationPromptViewModel

#Region "  Properties "
    Public ReadOnly Property ContractStage As String
        Get
            If _validated Then Return "Contract Validated"
            If _validationBegun Then Return "Contract Validation"
            Return "Contract"
        End Get
    End Property
    Public Property PromptContent As ContractPromptViewModel
        Get
            Return _promptContent
        End Get
        Set(value As ContractPromptViewModel)
            _promptContent = value
        End Set
    End Property
    Public Property ValidationContent As ValidationPromptViewModel
        Get
            Return _validationContent
        End Get
        Set(value As ValidationPromptViewModel)
            _validationContent = value
        End Set
    End Property
#End Region

    Private Sub ChangeView(ByVal newView As Screen)
        Me.ActiveItem = newView
    End Sub

    Public Sub New(dc As VWCreditProcess, eAggr As IEventAggregator)
        _eventAggregator = eAggr
        _dataContext = dc
        _validated = dc.IsValidated
        _eventAggregator.Subscribe(Me)
        GenerateScreens()
    End Sub
    Public Sub GenerateScreens()
        _promptContent = New ContractPromptViewModel(Me._dataContext)
        _validationContent = New ValidationPromptViewModel(Me._dataContext)
        ChangeView(_promptContent)
    End Sub
    Public Sub Handle_BeginDataCollectionMessage(message As PBS.Deals.FormsIntegration.BeginDataCollectMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage).Handle
        If message.ActionInitiateCollect Then
            Dim d As New Dictionary(Of String, Object)
            _dataContext.ReplicateCurrentState(d)
            _eventAggregator.Publish(New PBS.Deals.FormsIntegration.DataCollectionCompleteMessage() With {.ActionComplete = True, .CollectedResults = d})
        End If
    End Sub
    Public Sub Handle_BeginValidationMessage(message As PBS.Deals.FormsIntegration.BeginValidationMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage).Handle
        If message.BeginValidation Then
            _dataContext.CheckRules()
            _dataContext.GenerateRequirement()
            _validationBegun = True
            ChangeView(_validationContent)
        End If
    End Sub

End Class
