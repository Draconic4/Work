Public Class Utility
    Public Shared Function ProvinceOrStateConverter(ByVal prov As String, ByVal country As String) As String
        If country.StartsWith("U") Then Return StateConverter(prov)
        Return ProvinceConverter(prov)
    End Function
    Public Shared Function StateConverter(ByVal state As String)
        Return "MN"
    End Function
    Public Shared Function ProvinceConverter(ByVal prov As String)
        Return "BC"
    End Function
End Class
