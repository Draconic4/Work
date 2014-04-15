Imports Caliburn.Micro
Imports System.Windows

Public Class VWContractAddressViewModel
    Inherits Screen

    Private _address As ValidationRuleData.Address
    Private _gInfo As ValidationRuleData.ProcessInfo

    Public Property Address As ValidationRuleData.Address
        Get
            Return _address
        End Get
        Set(value As ValidationRuleData.Address)
            _address = value
        End Set
    End Property
    Public ReadOnly Property HomeAddressVisibility As Visibility
        Get
            If _address Is Nothing Then Return Visibility.Hidden
            If _address.AddressType = ValidationRuleData.Applicant.C_HOMEADDRESS Then Return Visibility.Hidden
            Return Visibility.Visible
        End Get
    End Property
    Public ReadOnly Property AddressFieldVisibility As Visibility
        Get
            If _address Is Nothing Then Return Visibility.Collapsed
            If IsHomeAddress Then Return Visibility.Visible
            If _address.SameAsHomeAddress Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public Sub AddressChanged()
        NotifyOfPropertyChange("HomeAddressVisibility")
        NotifyOfPropertyChange("AddressFieldVisibility")
    End Sub
    Public ReadOnly Property GlobalProperty As ValidationRuleData.ProcessInfo
        Get
            Return _gInfo
        End Get
    End Property
    Public ReadOnly Property ProvinceOrStateText As String
        Get
            If GlobalProperty Is Nothing Then Return "INVALID"
            If Utility.IsCanadian(GlobalProperty) Then Return "Province"
            Return "State"
        End Get
    End Property
    Public ReadOnly Property PostalCodeOrZipText As String
        Get
            If GlobalProperty Is Nothing Then Return "INVALID"
            If Utility.IsCanadian(GlobalProperty) Then Return "Postal Code"
            Return "Zip Code"
        End Get
    End Property
    Public ReadOnly Property ProvinceOrStateList As List(Of String)
        Get
            If GlobalProperty Is Nothing Then Return New List(Of String) From {"INVALID"}
            Return Utility.GetLocaleList(GlobalProperty)
        End Get
    End Property
    Public ReadOnly Property CountryList As List(Of String)
        Get
            If GlobalProperty Is Nothing Then Return New List(Of String) From {"INVALID"}
            Return Utility.GetCountryLocationList()
        End Get
    End Property

    Public ReadOnly Property IsHomeAddress As Boolean
        Get
            If _address Is Nothing Then Return False
            If _address.AddressType = ValidationRuleData.Applicant.C_HOMEADDRESS Then Return True
            Return False
        End Get
    End Property
    Public Sub New(ByVal address As ValidationRuleData.Address, ByVal gProp As ValidationRuleData.ProcessInfo)
        _address = address
        _gInfo = gProp
    End Sub
End Class
