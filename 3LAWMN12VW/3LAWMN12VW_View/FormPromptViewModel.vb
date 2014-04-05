Imports Caliburn.Micro
Imports System.Reflection

Public Class FormPromptViewModel
    Inherits Screen
    Implements IHandle(Of PBS.Deals.FormsIntegration.DataCollectMessage), IHandle(Of PBS.Deals.FormsIntegration.ValidationCompleteMessage)

    Private ReadOnly _eventAggregator As IEventAggregator

    Public Property TheHammer As PropertyChangedBase

    Public Sub CancelClicked()
        TryClose()
    End Sub
    Public Sub ValidateClicked()
        _eventAggregator.Publish(New PBS.Deals.FormsIntegration.BeginValidationMessage With {.BeginValidation = True})
    End Sub
    Public Sub ContinueClicked()
        _eventAggregator.Publish(New PBS.Deals.FormsIntegration.BeginDataCollectMessage With {.ActionInitiateCollect = True})
    End Sub

    Public Sub New(eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        Dim lastData As New Dictionary(Of String, Object)
        Dim dat As New Dictionary(Of String, Object)
        dat.Add("DLR_COUNTRY", "US")
        dat.Add("DEAL_FCALC", "0")
        dat.Add("DEAL_TERM", 60)
        dat.Add("DEAL_AMORTTERM", 1)
        AssemblySource.Instance.Add(Assembly.LoadFile("C:\Projects\GitHubProjects\Work\3LAWMN12VW\3LAWMN12VW\bin\Debug\3LAWMN12VW.dll"))
        TheHammer = New _3LAWMN12VW.PromptContentViewModel(lastData, dat, _eventAggregator) 'New _3LAWMN12VW.VWContractRequiredViewModel(x)
    End Sub

    Public Sub Handle(message As PBS.Deals.FormsIntegration.DataCollectMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.DataCollectMessage).Handle

    End Sub

    Public Sub Handle1(message As PBS.Deals.FormsIntegration.ValidationCompleteMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.ValidationCompleteMessage).Handle

    End Sub
End Class
