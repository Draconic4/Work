Imports Caliburn.Micro
Imports System.Windows

Public Class VWContractPrimaryApplicantViewModel
    Inherits Screen

    Private _View As PromptContentViewModel
    Private _Person As Applicant
    Private _UseAddress1For2 As Boolean = True
    Private _UseAddress1For3 As Boolean = True
    Private _Id As VWContractApplicantIDViewModel
    Private _Address1 As VWContractAddressViewModel
    Private _Address2 As VWContractAddressViewModel
    Private _Address3 As VWContractAddressViewModel

    Public ReadOnly Property MyCaption As String
        Get
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
            Return _Address1
        End Get
    End Property
    Public Property UseAddress1For2 As Boolean
        Get
            Return _UseAddress1For2
        End Get
        Set(value As Boolean)
            _UseAddress1For2 = value
            NotifyOfPropertyChange(Address2Visibility)
        End Set
    End Property
    Public ReadOnly Property Address2Visibility As Visibility
        Get
            If _UseAddress1For2 Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public ReadOnly Property Address2 As VWContractAddressViewModel
        Get
            Return _Address2
        End Get
    End Property
    Public Property UseAddress1For3 As Boolean
        Get
            Return _UseAddress1For3
        End Get
        Set(value As Boolean)
            _UseAddress1For3 = value
            NotifyOfPropertyChange(Address3Visibility)
        End Set
    End Property
    Public ReadOnly Property Address3Visibility As Visibility
        Get
            If UseAddress1For3 Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public ReadOnly Property Address3 As VWContractAddressViewModel
        Get
            Return _Address3
        End Get
    End Property
    Public Sub New(ByVal view As PromptContentViewModel)
        _View = view
        _Person = _View.DataContext.PrimaryApplicant
        _Id = New VWContractApplicantIDViewModel(_Person.ApplicantName, _View.DataContext.GlobalProperty)
        _Address1 = New VWContractAddressViewModel(_Person.HomeAddress, _View.DataContext.GlobalProperty)
        _Address2 = New VWContractAddressViewModel(_Person.BillingAddress, _View.DataContext.GlobalProperty)
        _Address3 = New VWContractAddressViewModel(_Person.GarageAddress, _View.DataContext.GlobalProperty)
    End Sub
    Public Sub Validate()
        _Id.Validate()
        NotifyOfPropertyChange("")
    End Sub
End Class
