Public Class VWContractRequirementViewModel
    Inherits Caliburn.Micro.Screen

    Private _masterView As PromptContentViewModel
    Private _eventAggregator As Caliburn.Micro.IEventAggregator

    Sub New(promptContentViewModel As PromptContentViewModel, eventAggregator As Caliburn.Micro.IEventAggregator)
        _masterView = promptContentViewModel
        _eventAggregator = eventAggregator
    End Sub

    Public Sub ApplicationTypeChange()
        '_eventAggregator.Publish(New ApplicationTypeChanged)
    End Sub
End Class
