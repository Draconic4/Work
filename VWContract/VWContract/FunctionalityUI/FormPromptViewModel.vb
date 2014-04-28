Imports Caliburn.Micro
Imports System.Reflection

Public Class FormPromptViewModel
    Inherits Screen

    Private ReadOnly _eventAggregator As IEventAggregator

    Public Property TheHammer As PropertyChangedBase
    Public Property Title As String = "Hello World"
    Public Sub CancelClicked()
        TryClose()
    End Sub
    Public Sub ValidateClicked()
        '_eventAggregator.Publish(New PBS.Deals.FormsIntegration.BeginValidationMessage With {.BeginValidation = True})
    End Sub
    Public Sub ContinueClicked()
        '_eventAggregator.Publish(New PBS.Deals.FormsIntegration.BeginDataCollectMessage With {.ActionInitiateCollect = True})
    End Sub

    Public Sub New(eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        Dim lastData As New Dictionary(Of String, Object)
        Dim dat As New Dictionary(Of String, Object)
        'For Each line In IO.File.ReadLines("../../ELW1121212B.txt") 'DANDICT.dat
        '    dat.Add(line.Split("|")(0), line.Split("|")(2))
        'Next
        AssemblySource.Instance.Add(Assembly.LoadFile("C:\Project\Work\Work\VWContract\VWContract\VWContractValidation\bin\Debug\VWContractValidation.dll"))
        Dim vwPI As VWContractValidation.VWCreditProcess = VWContractValidation.VWCreditProcess.Fetch(lastData, dat)
        TheHammer = New VWContractValidation.PromptContentViewModel(vwPI, _eventAggregator) 'New _3LAWMN12VW.VWContractRequiredViewModel(x)
    End Sub

    'Public Sub Handle(message As PBS.Deals.FormsIntegration.DataCollectMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.DataCollectMessage).Handle

    'End Sub

    'Public Sub Handle1(message As PBS.Deals.FormsIntegration.ValidationCompleteMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.ValidationCompleteMessage).Handle

    'End Sub
End Class

