Imports Caliburn.Micro
Imports System.Windows

Public Class VWContractCoApplicantViewModel
    Inherits Screen

    Private _View As PromptContentViewModel
    'Private _CoApplicant As VWContractCoApplicantViewModel
    Private _globalProperty As ValidationRuleData.ProcessInfo

    Private ReadOnly Property GlobalProperty() As ValidationRuleData.ProcessInfo
        Get
            Return _globalProperty
        End Get
    End Property
    Public ReadOnly Property CoApplicationToggleButton As Visibility
        Get
            If _View.DataContext Is Nothing OrElse _View.DataContext.GlobalProperty Is Nothing Then Return Visibility.Collapsed
            If Not Utility.HasCoApplicant(GlobalProperty) Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    'Public ReadOnly Property CoApplicant
    '    Get
    '        Return _CoApplicant
    '    End Get
    'End Property

    Public Sub New(ByVal view As PromptContentViewModel)
        _View = view
        '_CoApplicant = 
    End Sub
End Class
