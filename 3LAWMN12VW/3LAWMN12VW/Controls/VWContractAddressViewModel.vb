Imports Caliburn.Micro

Public Class VWContractAddressViewModel
    Inherits Screen

    Private _address As Address
    Private _gInfo As ProcessInfo

    Public Property Address As Address
        Get
            Return _address
        End Get
        Set(value As Address)
            _address = value
        End Set
    End Property
    Public ReadOnly Property GlobalProperty As ProcessInfo
        Get
            Return _gInfo
        End Get
    End Property
    Public ReadOnly Property ProvinceOrStateText As String
        Get
            If GlobalProperty Is Nothing Then Return "INVALID"
            If GlobalProperty.IsCanadian Then Return "Province"
            Return "State"
        End Get
    End Property
    Public ReadOnly Property PostalCodeOrZipText As String
        Get
            If GlobalProperty Is Nothing Then Return "INVALID"
            If GlobalProperty.IsCanadian Then Return "Postal Code"
            Return "Zip Code"
        End Get
    End Property
    Public ReadOnly Property ProvinceOrStateList As List(Of String)
        Get
            If GlobalProperty Is Nothing Then Return New List(Of String) From {"INVALID"}
            Return Utility.GetLocaleList(GlobalProperty.IsCanadian)
        End Get
    End Property
    Public ReadOnly Property CountryList As List(Of String)
        Get
            If GlobalProperty Is Nothing Then Return New List(Of String) From {"INVALID"}
            Return Utility.GetCountryLocationList()
        End Get
    End Property
    Public Sub New(ByVal address As Address, ByVal gProp As ProcessInfo)
        _address = address
        _gInfo = gProp
    End Sub
End Class
