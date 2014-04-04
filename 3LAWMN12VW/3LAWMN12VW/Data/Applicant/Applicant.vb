Imports Csla

Public Class Applicant
    Inherits BusinessBase(Of Applicant)

    Private _globalProperties As ProcessInfo

    Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicantType))

    Public Shared ReadOnly ApplicantNameProperty As PropertyInfo(Of ApplicantID) = RegisterProperty(Of ApplicantID)(Function(c) c.ApplicantName)
    Public Shared ReadOnly HomeAddressProperty As PropertyInfo(Of Address) = RegisterProperty(Of Address)(Function(c) c.HomeAddress)
    Public Shared ReadOnly BillingAddressProperty As PropertyInfo(Of Address) = RegisterProperty(Of Address)(Function(c) c.BillingAddress)
    Public Shared ReadOnly GarageAddressProperty As PropertyInfo(Of Address) = RegisterProperty(Of Address)(Function(c) c.GarageAddress)

#Region "  Properties "
    Public ReadOnly Property ApplicantType As String
        Get
            Return GetProperty(ApplicantTypeProperty)
        End Get
    End Property
    Public Property ApplicantName() As ApplicantID
        Get
            Return GetProperty(ApplicantNameProperty)
        End Get
        Set(ByVal value As ApplicantID)
            SetProperty(ApplicantNameProperty, value)
        End Set
    End Property
    Public Property HomeAddress As Address
        Get
            Return GetProperty(HomeAddressProperty)
        End Get
        Set(value As Address)
            SetProperty(HomeAddressProperty, value)
        End Set
    End Property
    Public Property BillingAddress As Address
        Get
            Return GetProperty(BillingAddressProperty)
        End Get
        Set(value As Address)
            SetProperty(BillingAddressProperty, value)
        End Set
    End Property
    Public Property GarageAddress As Address
        Get
            Return GetProperty(GarageAddressProperty)
        End Get
        Set(value As Address)
            SetProperty(GarageAddressProperty, value)
        End Set
    End Property
#End Region

#Region "  Data Access "
    Public Sub New(ByVal prefix As String, pInfo As ProcessInfo)
        LoadProperty(ApplicantTypeProperty, prefix)
        _globalProperties = pInfo
    End Sub
    Public Shared Function FetchExisting(ByVal prefix As String, pInfo As ProcessInfo) As Applicant
        Return New Applicant(prefix, pInfo)
    End Function
    Public Sub Populate(ByVal d As Dictionary(Of String, Object))
        PopulateId(d)
        PopulateAddress(d)
    End Sub
    Public Sub PopulateId(ByVal d As Dictionary(Of String, Object))
        If ApplicantName Is Nothing Then LoadProperty(ApplicantNameProperty, ApplicantID.FetchExisting(ApplicantType))
        ApplicantName.Populate(d)
    End Sub
    Public Sub PopulateAddress(ByVal d As Dictionary(Of String, Object))
        If HomeAddress Is Nothing Then LoadProperty(HomeAddressProperty, Address.FetchExisting(ApplicantType, _globalProperties))
        If BillingAddress Is Nothing Then LoadProperty(BillingAddressProperty, Address.FetchExisting(ApplicantType, _globalProperties))
        If GarageAddress Is Nothing Then LoadProperty(GarageAddressProperty, Address.FetchExisting(ApplicantType, _globalProperties))
        HomeAddress.Populate(d)
        BillingAddress.Populate(d)
        GarageAddress.Populate(d)
    End Sub
#End Region

#Region "  Business Rules "
    Public Sub CheckRules()
    End Sub
#End Region

End Class
