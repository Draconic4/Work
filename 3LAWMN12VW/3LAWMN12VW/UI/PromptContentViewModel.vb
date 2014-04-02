Imports Caliburn.Micro

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.OneActive

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
    Public Sub New(ByVal previousDC As Dictionary(Of String, Object), ByVal arDC As Dictionary(Of String, Object))
        _dataContext = VWCreditProcess.FetchExisting(previousDC, arDC)
        _detailView = New VWContractRequiredViewModel(Me) 'Me.Items.Add(New VWContractRequiredViewModel(Me))
        Me.ActiveItem = _detailView
    End Sub
End Class
