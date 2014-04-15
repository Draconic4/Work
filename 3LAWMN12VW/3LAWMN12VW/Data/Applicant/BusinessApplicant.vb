Imports Csla

Namespace ValidationRuleData
    Public Class BusinessApplicant
        Inherits BusinessBase(Of Applicant)
        Public Const C_HOMEADDRESS As String = "Home Address"
        Public Const C_BILLINGADDRESS As String = "Billing Address"
        Public Const C_GARAGEADDRESS As String = "Garage Address"

        Private _globalProperty As ProcessInfo

        Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicantType))
        Public Shared ReadOnly CompanyNameProperty As PropertyInfo(Of BusinessApplicantID) = RegisterProperty(Of BusinessApplicantID)(Function(c) c.ApplicantName)
        Public Shared ReadOnly HomeAddressProperty As PropertyInfo(Of Address) = RegisterProperty(Of Address)(Function(c) c.HomeAddress)
        Public Shared ReadOnly BillingAddressProperty As PropertyInfo(Of Address) = RegisterProperty(Of Address)(Function(c) c.BillingAddress)
        Public Shared ReadOnly GarageAddressProperty As PropertyInfo(Of Address) = RegisterProperty(Of Address)(Function(c) c.GarageAddress)

#Region "  Properties "
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperty
            End Get
        End Property
        Public ReadOnly Property ApplicantType As String
            Get
                Return GetProperty(ApplicantTypeProperty)
            End Get
        End Property
        Public ReadOnly Property CompanyName As BusinessApplicantID
            Get
                Return GetProperty(CompanyNameProperty)
            End Get
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
        Public Sub New(ByVal parent As String, gProp As ProcessInfo)
            _globalProperty = gProp
            LoadProperty(ApplicantTypeProperty, parent)
        End Sub
        Public Shared Function Fetch(ByVal keyParent As String, gProp As ProcessInfo, ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As Applicant
            Dim a As New Applicant(keyParent, gProp)
            If currentRun Is Nothing Then Return a

            a.HomeAddress = Address.Fetch(keyParent, C_HOMEADDRESS, gProp, previousRun, currentRun)
            a.BillingAddress = Address.Fetch(keyParent, C_BILLINGADDRESS, gProp, previousRun, currentRun)
            a.GarageAddress = Address.Fetch(keyParent, C_GARAGEADDRESS, gProp, previousRun, currentRun)
            Return a
        End Function
        Public Function SaveData() As Dictionary(Of String, Object)
            Dim d As New Dictionary(Of String, Object)
            For Each elem As KeyValuePair(Of String, Object) In CompanyName.SaveData()
                d.Add(elem.Key, elem.Value)
            Next
            For Each elem As KeyValuePair(Of String, Object) In HomeAddress.SaveData()
                d.Add(elem.Key, elem.Value)
            Next
            For Each elem As KeyValuePair(Of String, Object) In BillingAddress.SaveData()
                d.Add(elem.Key, elem.Value)
            Next
            For Each elem As KeyValuePair(Of String, Object) In GarageAddress.SaveData()
                d.Add(elem.Key, elem.Value)
            Next
            Return d
        End Function
#End Region

#Region "  Business Rules "
        Public Sub CheckRules()
            CompanyName.CheckRules()
            HomeAddress.CheckRules()
            BillingAddress.CheckRules()
            GarageAddress.CheckRules()
        End Sub
#End Region
    End Class
End Namespace
