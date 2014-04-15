Imports Caliburn.Micro
Imports System.ComponentModel
Imports System.Windows

Public Class VWContractRequiredViewModel
    Inherits Screen

    Private _masterView As PromptContentViewModel
    Private _eventAggregator As IEventAggregator
    Private _dataContext As ValidationRuleData.ProcessInfo

#Region "  Properties "
    Public ReadOnly Property DataContext As ValidationRuleData.ProcessInfo
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
            Return _dataContext.ProductType 
        End Get
    End Property
    Public ReadOnly Property ApplicationTypes As List(Of String)
        Get
            Return _dataContext.ApplicationTypeList
        End Get
    End Property
#End Region

    Public Sub New(ByVal mv As PromptContentViewModel, evA As IEventAggregator)
        _masterView = mv
        _dataContext = mv.DataContext.GlobalProperty
        _eventAggregator = evA
    End Sub

    Public Sub ApplicationTypeChange()
        _eventAggregator.Publish(New ApplicationTypeChanged)
    End Sub

    Public Sub Validate()
        NotifyOfPropertyChange("")
    End Sub
End Class
