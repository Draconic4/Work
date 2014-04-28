Imports Caliburn.Micro

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.OneActive

    Private _dataContext As VWCreditProcess
    Private _eventAggregator As IEventAggregator

    Public Const C_Requirement As String = "Requirement"
    Private _requirement As VWContractRequirementViewModel
    Public Const C_TestPanel As String = "TestPanel"
    Private _testPanel As TestPanelViewModel
    Public Const C_ContractPrompt As String = "ContractPrompt"
    Private _contractPrompt As ContractPromptViewModel

#Region "  Properties "
    Public Property DataContext As VWCreditProcess
        Get
            Return _dataContext
        End Get
        Set(value As VWCreditProcess)
            _dataContext = value
        End Set
    End Property
    Public ReadOnly Property RequirementChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(VWContractRequirementViewModel)
        End Get
    End Property
    Public ReadOnly Property TestPanelChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(TestPanelViewModel)
        End Get
    End Property
    Public ReadOnly Property ContractPromptChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ContractPromptViewModel)
        End Get
    End Property
#End Region

    Public Sub RequirementClicked()
        'ChangeView(_requirement)
    End Sub
    Public Sub TestPanelClicked()
        'ChangeView(_testPanel)
    End Sub

    Public Sub ChangeView(ByVal newView As Screen)
        Me.ActiveItem = newView
        Me.NotifyOfPropertyChange(C_Requirement & "Checked")
        Me.NotifyOfPropertyChange(C_TestPanel & "Checked")
        Me.NotifyOfPropertyChange(C_ContractPrompt & "Checked")
    End Sub

    Public Sub New(dc As VWCreditProcess, eAggr As IEventAggregator)
        _eventAggregator = eAggr
        _dataContext = dc
        _eventAggregator.Subscribe(Me)
        GenerateScreens()
    End Sub
    Public Sub GenerateScreens()
        Items.Clear()
        _requirement = New VWContractRequirementViewModel(Me, _eventAggregator)
        _testPanel = New TestPanelViewModel(Me, _eventAggregator)
        _contractPrompt = New ContractPromptViewModel(Me.DataContext)
        ChangeView(_contractPrompt)
    End Sub
End Class
