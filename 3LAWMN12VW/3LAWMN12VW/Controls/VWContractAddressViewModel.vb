Imports Caliburn.Micro

Public Class VWContractAddressViewModel
    Inherits Screen

    Private _address As Address

    Property Address As Address
        Get
            Return _Address
        End Get
        Set(value As Address)
            _Address = value
        End Set
    End Property
    Public ReadOnly Property ProvinceOrStateText As String
        Get
            If _address.GlobalProperty.IsCanadian Then Return "Province"
            Return "State"
        End Get
    End Property
    Public ReadOnly Property PostalCodeOrZipText As String
        Get
            If _address.GlobalProperty.IsCanadian Then Return "Postal Code"
            Return "Zip Code"
        End Get
    End Property
    Public ReadOnly Property ProvinceOrStateList As List(Of String)
        Get
            Return Utility.GetLocaleList(_address.GlobalProperty.IsCanadian)
        End Get
    End Property
    Public ReadOnly Property CountryList As List(Of String)
        Get
            Return Utility.GetCountryLocationList()
        End Get
    End Property
    Public Sub New(ByVal address As Address)
        _address = address
    End Sub
End Class
