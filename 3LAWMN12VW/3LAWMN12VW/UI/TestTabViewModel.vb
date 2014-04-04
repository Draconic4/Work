Imports Caliburn.Micro

Public Class TestTabViewModel
    Inherits Screen

    Private _testContainerVM As VWContractApplicantViewModel

    Public ReadOnly Property TestSurface As Screen
        Get
            Return _testContainerVM
        End Get
    End Property
    Public Sub New(ByVal view As PromptContentViewModel)
        _testContainerVM = New VWContractApplicantViewModel(view.DataContext.PrimaryApplicant, view.DataContext.GlobalProperty)
    End Sub
End Class
