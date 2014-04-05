Imports Caliburn.Micro
Imports System.Windows

Public Class VWContractApplicantViewModel
    Inherits Screen

    Private _Person As Applicant
    Private _UseHomeAddressForBilling As Boolean = True
    Private _UseHomeAddressForGarage As Boolean = True
    Private _Id As VWContractApplicantIDViewModel
    Private _HomeAddress As VWContractAddressViewModel
    Private _BillingAddress As VWContractAddressViewModel
    Private _GarageAddress As VWContractAddressViewModel

    Public ReadOnly Property MyCaption As String
        Get
            If _Person Is Nothing OrElse _Person.ApplicantName Is Nothing Then Return String.Empty
            Return _Person.ApplicantName.FullName
        End Get
    End Property
    Public ReadOnly Property ApplicantName As VWContractApplicantIDViewModel
        Get
            Return _Id
        End Get
    End Property
    Public ReadOnly Property HomeAddress As VWContractAddressViewModel
        Get
            Return _HomeAddress
        End Get
    End Property
    Public Property UseHomeAddressForBilling As Boolean
        Get
            Return _UseHomeAddressForBilling
        End Get
        Set(value As Boolean)
            _UseHomeAddressForBilling = value
            NotifyOfPropertyChange(BillingAddressVisibility)
        End Set
    End Property
    Public ReadOnly Property BillingAddressVisibility As Visibility
        Get
            If UseHomeAddressForBilling Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public ReadOnly Property BillingAddress As VWContractAddressViewModel
        Get
            Return _BillingAddress
        End Get
    End Property
    Public Property UseHomeAddressForGarage As Boolean
        Get
            Return _UseHomeAddressForGarage
        End Get
        Set(value As Boolean)
            _UseHomeAddressForGarage = value
            NotifyOfPropertyChange(GarageAddressVisibility)
        End Set
    End Property
    Public ReadOnly Property GarageAddressVisibility As Visibility
        Get
            If UseHomeAddressForGarage Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public ReadOnly Property GarageAddress As VWContractAddressViewModel
        Get
            Return _GarageAddress
        End Get
    End Property

    Public Sub New(ByVal app As Applicant, gProp As ProcessInfo)
        _Person = app
        _Id = New VWContractApplicantIDViewModel(_Person.ApplicantName, gProp)
        _HomeAddress = New VWContractAddressViewModel(_Person.HomeAddress, gProp)
        _BillingAddress = New VWContractAddressViewModel(_Person.BillingAddress, gProp)
        _GarageAddress = New VWContractAddressViewModel(_Person.GarageAddress, gProp)
    End Sub

    Public Sub Validate()
    End Sub
End Class
