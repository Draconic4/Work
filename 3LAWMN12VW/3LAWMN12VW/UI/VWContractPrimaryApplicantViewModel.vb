Imports Caliburn.Micro
Imports System.Windows

Public Class VWContractPrimaryApplicantViewModel
    Inherits Screen

    Private _View As PromptContentViewModel 'Parent View
    Private _Primary As VWContractApplicantViewModel 'Pass Through View
    Private Property _globalProperty As ValidationRuleData.ProcessInfo 'Global Processing Information

    Private ReadOnly Property GlobalProperty() As ValidationRuleData.ProcessInfo
        Get
            Return _globalProperty
        End Get
    End Property
    Public ReadOnly Property PrimaryApplicantToggleButton As Visibility
        Get
            If _View.DataContext Is Nothing OrElse _View.DataContext.GlobalProperty Is Nothing Then Return Visibility.Collapsed
            If Utility.IsBusiness(GlobalProperty) Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public ReadOnly Property PrimaryApplicant
        Get
            Return _Primary
        End Get
    End Property

    Public Sub New(ByVal view As PromptContentViewModel)
        _View = view
        _Primary = New VWContractApplicantViewModel(view.DataContext.ApplicantMgr.Primary, view.DataContext.GlobalProperty)
    End Sub
    Public Sub Validate()
        If Utility.IsBusiness(GlobalProperty) Then Return
        _Primary.Validate()
        NotifyOfPropertyChange("")
    End Sub
End Class
