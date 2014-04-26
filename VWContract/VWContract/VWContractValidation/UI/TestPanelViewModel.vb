Imports Caliburn.Micro

Public Class TestPanelViewModel
    Inherits Screen

    Private _parentView As PromptContentViewModel
    Private _eventAggregator As IEventAggregator

    Public Sub New(ByVal parent As PromptContentViewModel, ByVal evtAggro As IEventAggregator)
        _parentView = parent
        _eventAggregator = evtAggro
    End Sub
End Class
