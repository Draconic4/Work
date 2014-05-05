Imports Caliburn.Micro
Imports VWContractValidation.ValidationLib

Public Class ContractPromptViewModel
    Inherits Screen

    Public _dataContext As VWCreditProcess
    Private _vehicleUseVM As VehicleUseViewModel

#Region "  Properties "
    Public ReadOnly Property VehicleData As Vehicle
        Get
            Return _dataContext.Vehicle
        End Get
    End Property
    Public Property VehicleUseVM As VehicleUseViewModel
        Get
            Return _vehicleUseVM
        End Get
        Set(value As VehicleUseViewModel)
            _vehicleUseVM = value
        End Set
    End Property
#End Region

    Public Sub New(ByVal processInfo As VWCreditProcess)
        _dataContext = processInfo
        _vehicleUseVM = New VehicleUseViewModel(processInfo)
    End Sub
End Class
