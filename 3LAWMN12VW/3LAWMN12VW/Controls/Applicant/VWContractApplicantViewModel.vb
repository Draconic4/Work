Imports Caliburn.Micro
Imports System.Windows

Public Class VWContractApplicantViewModel
    Inherits Screen

    Private _Person As ValidationRuleData.Applicant
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
    Public Sub New()
        _Id = New VWContractApplicantIDViewModel(Nothing, Nothing)
        _HomeAddress = New VWContractAddressViewModel(Nothing, Nothing)
        _BillingAddress = New VWContractAddressViewModel(Nothing, Nothing)
        _GarageAddress = New VWContractAddressViewModel(Nothing, Nothing)
    End Sub
    Public Sub New(ByVal app As ValidationRuleData.Applicant, gProp As ValidationRuleData.ProcessInfo)
        If app Is Nothing Then Exit Sub
        _Person = app
        _Id = New VWContractApplicantIDViewModel(_Person.ApplicantName, gProp)
        _HomeAddress = New VWContractAddressViewModel(_Person.HomeAddress, gProp)
        _BillingAddress = New VWContractAddressViewModel(_Person.BillingAddress, gProp)
        _GarageAddress = New VWContractAddressViewModel(_Person.GarageAddress, gProp)
    End Sub
    Public Sub Validate()
        Dim x As New Csla.Rules.BrokenRulesCollection
        _Person.ApplicantName.CheckRules()
        x.AddRange(_Person.ApplicantName.BrokenRulesCollection.Reverse)
        _Person.HomeAddress.CheckRules()
        x.AddRange(_Person.HomeAddress.BrokenRulesCollection.Reverse)
        If Not _Person.BillingAddress.SameAsHomeAddress Then
            _Person.BillingAddress.CheckRules()
            x.AddRange(_Person.BillingAddress.BrokenRulesCollection.Reverse)
        End If
        If Not _Person.GarageAddress.SameAsHomeAddress Then
            _Person.GarageAddress.CheckRules()
            x.AddRange(_Person.GarageAddress.BrokenRulesCollection.Reverse)
        End If
        NotifyOfPropertyChange("")
    End Sub
End Class
