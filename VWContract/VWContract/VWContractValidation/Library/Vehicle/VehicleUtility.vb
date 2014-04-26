Class VehicleUtility

#Region " LAW Vehicle Usage Types "
    Public Const C_VEHICLEUSE_PERSONAL As String = "PERSONAL/FAMILY/HOUSEHOLD"
    Public Const C_VEHICLEUSE_BUSINESS As String = "BUSINESS"
    Public Const C_VEHICLEUSE_AGRICULTURAL As String = "AGRICULTURE"
    Public Const C_VEHICLEUSE_OTHER As String = "OTHER"
#End Region
    Public Shared Function IsPersonalUse(vehicle As Vehicle) As Boolean
        If vehicle Is Nothing Then Return True
        If Not vehicle.VehicleUse.StartsWith(C_VEHICLEUSE_PERSONAL, StringComparison.InvariantCultureIgnoreCase) Then Return False
        Return True
    End Function
    Public Shared Function IsOtherUse(vehicle As Vehicle) As Boolean
        If vehicle Is Nothing Then Return False
        If Not vehicle.VehicleUse.StartsWith(C_VEHICLEUSE_OTHER, StringComparison.InvariantCultureIgnoreCase) Then Return False
        Return True
    End Function
End Class
