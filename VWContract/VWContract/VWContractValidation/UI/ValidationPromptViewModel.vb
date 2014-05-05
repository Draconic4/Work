Imports Caliburn.Micro
Imports VWContractValidation.ValidationLib

Public Class ValidationPromptViewModel
    Inherits Screen

    Private _dataContext As VWCreditProcess
    Private _VWProcessVM As VWProcessViewModel

#Region "  Properties "
    Public Property VWProcessVM As VWProcessViewModel
        Get
            Return _VWProcessVM
        End Get
        Set(value As VWProcessViewModel)
            _VWProcessVM = value
        End Set
    End Property
    Public ReadOnly Property RuleDisplayVM As RuleSet
        Get
            Return _dataContext.RuleSet
        End Get
    End Property
#End Region

    Sub New(dataContext As VWCreditProcess)
        ' TODO: Complete member initialization 
        _dataContext = dataContext
        _VWProcessVM = New VWProcessViewModel(dataContext)
    End Sub

End Class
