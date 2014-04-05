Imports Caliburn.Micro
Imports System.ComponentModel
Imports System.Windows

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
    Public ReadOnly Property LeaseTypeVisibility As Visibility
        Get
            If _dataContext Is Nothing Then Return Visibility.Collapsed
            If Utility.IsLease(_dataContext) Then Return Visibility.Visible
            Return Visibility.Hidden
        End Get
    End Property
    Public ReadOnly Property ProductTypeText As String
        Get
            If _dataContext Is Nothing Then Return ""
            If Not Utility.IsLease(_dataContext) Or _dataContext.ProductType.StartsWith("M") Then Return _dataContext.ProductType 'Won't be visible
            If Utility.IsCanadian(_dataContext) Then Return "Single Pay"
            Return "Pre-Pay"
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
