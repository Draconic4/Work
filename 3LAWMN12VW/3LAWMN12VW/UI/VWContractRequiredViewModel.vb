Imports Caliburn.Micro
Imports System.ComponentModel

Public Class VWContractRequiredViewModel
    Inherits Screen

    Private _masterView As PromptContentViewModel
    Private _dataContext As ProcessInfo

#Region "  Properties "
    Public ReadOnly Property DataContext As ProcessInfo
        Get
            Return _dataContext
        End Get
    End Property
    Public ReadOnly Property ApplicationTypes As List(Of String)
        Get
            Dim appTypes As New List(Of String)
            appTypes.Add("INDIVIDUAL")
            appTypes.Add("INDIVIDUAL AND COAPPLICANT")
            appTypes.Add("INDIVIDUAL AND TWO COAPPLICANTS")
            appTypes.Add("INDIVIDUAL WITH GUARANTOR")
            appTypes.Add("BUSINESS")
            appTypes.Add("BUSINESS WITH COAPPLICANT")
            appTypes.Add("BUSINESS WITH GUARANTOR")
            appTypes.Add("BUSINESS WITH TWO COAPPLICANTS")
            appTypes.Add("BUSINESS WITH COAPPLICANT AND GUARANTOR")
            Return appTypes
        End Get
    End Property
#End Region

    Public Sub New(ByVal mv As PromptContentViewModel)
        _masterView = mv
        _dataContext = mv.DataContext.GlobalProperty
    End Sub

    Public Sub Validate()
        NotifyOfPropertyChange("")
        '_dataContext.CheckRules()
    End Sub
End Class
