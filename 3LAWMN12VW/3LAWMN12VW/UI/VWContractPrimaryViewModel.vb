Imports Caliburn.Micro
Imports System.Windows

Public Class VWContractPrimaryViewModel
    Inherits Screen

    Private _Primary As VWContractApplicantViewModel
    Private _View As PromptContentViewModel

    Public ReadOnly Property PrimaryApplicant As VWContractApplicantViewModel
        Get
            'If _Primary Is Nothing AndAlso Not _View.DataContext.GlobalProperty.ApplicationType.IsBusinessApplicant Then
            '    _Primary = New VWContractPrimaryApplicantViewModel(_View.DataContext.Buy)
            'End If
            Return _Primary
        End Get
    End Property
    Public ReadOnly Property PrimaryApplicantVisibility As Visibility
        Get
            'If _View.DataContext.GlobalProperty.ApplicationType.IsBusinessApplicant Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public Sub New(ByVal view As PromptContentViewModel)
        _View = view
    End Sub
End Class
