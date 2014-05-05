Imports VWContractValidation.ValidationLib

Public Class VehicleUtility

#Region " LAW Vehicle Usage Types "
    Public Const C_VEHICLEUSE_PERSONAL As String = "PERSONAL/FAMILY/HOUSEHOLD"
    Public Const C_VEHICLEUSE_BUSINESS As String = "BUSINESS"
    Public Const C_VEHICLEUSE_AGRICULTURAL As String = "AGRICULTURE"
    Public Const C_VEHICLEUSE_OTHER As String = "OTHER"
#End Region

    Public Shared Function IsPersonalUse(vehicle As Vehicle) As Boolean
        If vehicle Is Nothing Then Return True
        Return vehicle.Use.StartsWith("P")
    End Function
    Public Shared Function IsOtherUse(vehicle As Vehicle) As Boolean
        If vehicle Is Nothing Then Return False
        Return vehicle.Use.StartsWith("O")
    End Function
    Public Shared Function IsBusinessUse(vehicle As Vehicle) As Boolean
        If IsPersonalUse(vehicle) OrElse IsOtherUse(vehicle) Then Return False
        Return vehicle.Use.StartsWith("B")
    End Function
    Public Shared Function IsAgricultureUse(vehicle As Vehicle) As Boolean
        If IsPersonalUse(vehicle) OrElse IsOtherUse(vehicle) Then Return False
        Return vehicle.Use.StartsWith("A")
    End Function
End Class
