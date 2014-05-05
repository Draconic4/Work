Namespace ValidationLib
    Public Class VehicleArgs
        Inherits BaseConstructionArgs
        Public Property VehicleType As String
        Public Property VehicleIndex As Integer = 1

        Public Shared Function KeyParent(ByVal vehicleType As String, ByVal index As Integer)
            If index <= 0 Then index = 1
            If vehicleType = "Vehicle" Then Return "VEH" & index.ToString
            If vehicleType = "Trade" Then Return "TRADE" & index.ToString
            Return "VEH1"
        End Function

    End Class
End Namespace
