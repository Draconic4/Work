Imports Caliburn.Micro

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.OneActive

    Private _dataContext As VWCreditProcess
    Private _eventAggregator As IEventAggregator

    Public Const C_Requirement As String = "Requirement"
    Private _requirement As VWContractRequirementViewModel

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
#End Region

    Public Sub RequirementClicked()
        ChangeView(_requirement)
    End Sub

    Public Sub ChangeView(ByVal newView As Screen)
        Me.ActiveItem = newView
        Me.NotifyOfPropertyChange(C_Requirement & "Checked")
    End Sub

    Public Sub New(ByVal pDC As Dictionary(Of String, Object), ByVal cDC As Dictionary(Of String, Object), eAggr As IEventAggregator)
        _eventAggregator = eAggr
        _dataContext = VWCreditProcess.Fetch(pDC, cDC)
        _eventAggregator.Subscribe(Me)
        GenerateScreens()
    End Sub
    Public Sub GenerateScreens()
        Items.Clear()
        _requirement = New VWContractRequirementViewModel(Me, _eventAggregator)
        ChangeView(_requirement)
    End Sub
End Class
