Imports Caliburn.Micro
Imports VWContractValidation.ValidationLib

Public Class VehicleUseViewModel
    Inherits Screen

    Public _vehicle As Vehicle

#Region "  Properties "
    Public ReadOnly Property VehicleData As Vehicle
        Get
            Return _vehicle
        End Get
    End Property
    Public ReadOnly Property UseTypes As List(Of String)
        Get
            Return New List(Of String) From {VehicleUtility.C_VEHICLEUSE_PERSONAL, VehicleUtility.C_VEHICLEUSE_BUSINESS, VehicleUtility.C_VEHICLEUSE_AGRICULTURAL, VehicleUtility.C_VEHICLEUSE_OTHER}
        End Get
    End Property
    Public ReadOnly Property UseGrid As Double
        Get
            If VehicleUtility.IsPersonalUse(VehicleData) Then Return 0
            Return Double.NaN
        End Get
    End Property
    Public ReadOnly Property OtherUse As Double
        Get
            If Not VehicleUtility.IsOtherUse(VehicleData) Then Return 0
            Return Double.NaN
        End Get
    End Property
    Public ReadOnly Property BusinessUse As Double
        Get
            If VehicleUtility.IsOtherUse(VehicleData) OrElse VehicleUtility.IsPersonalUse(VehicleData) Then Return 0
            Return Double.NaN
        End Get
    End Property
#End Region

    Public Sub VehicleUseChanged(itm As String)
        NotifyOfPropertyChange("UseGrid")
        NotifyOfPropertyChange("OtherUse")
        NotifyOfPropertyChange("BusinessUse")
    End Sub
    Public Sub New(ByVal vwCP As VWCreditProcess)
        _vehicle = vwCP.Vehicle
    End Sub
End Class
