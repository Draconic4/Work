Imports Caliburn.Micro
Public Class VWContractRequiredViewModel
    Inherits Screen

    Private _masterView As PromptContentViewModel

#Region "  Properties "
    Public Property ApplicationType As String
        Get
            Return _masterView.DataContext.ApplicationType
        End Get
        Set(value As String)
            _masterView.DataContext.ApplicationType = value
        End Set
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
    End Sub

End Class
