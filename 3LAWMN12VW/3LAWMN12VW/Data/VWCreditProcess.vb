Imports Csla

Public Class VWCreditProcess
    Inherits Csla.BusinessBase(Of VWCreditProcess)

    Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicationType), "APPLICATION_TYPE", "INDIVIDUAL")
    Public Shared ReadOnly DealTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DealType), "DEAL_STATUS", "0")
    Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "DLR_COUNTRY", "US")
    Public Shared ReadOnly AddressProperty As PropertyInfo(Of Address) = RegisterProperty(Of Address)(Function(c) (c.Buy), "BUY")

#Region "  Properties "
    Public Property ApplicationType As String
        Get
            Return GetProperty(ApplicationTypeProperty)
        End Get
        Set(value As String)
            SetProperty(ApplicationTypeProperty, value)
        End Set
    End Property
    Public ReadOnly Property DealType As String
        Get
            Return GetProperty(DealTypeProperty)
        End Get
    End Property
    Public ReadOnly Property Country As String
        Get
            Return GetProperty(CountryProperty)
        End Get
    End Property
    Public ReadOnly Property Buy As Address
        Get
            Return GetProperty(AddressProperty)
        End Get
    End Property
#End Region

    Public Sub New()
    End Sub

    Public Shared Function FetchExisting(ByVal formDC As Dictionary(Of String, Object), ByVal aristoDC As Dictionary(Of String, Object)) As VWCreditProcess
        Dim dp As New VWCreditProcess
        dp.LoadProperty(AddressProperty, New Address("BUY"))
        dp.LoadFormDataContext(formDC)
        dp.Buy.Populate(formDC)
        dp.LoadFormDataContext(aristoDC)
        dp.Buy.Populate(aristoDC)
        Return dp
    End Function

    Public Sub LoadFormDataContext(ByVal formDC As Dictionary(Of String, Object))
        If formDC.ContainsKey("DLR_COUNTRY") Then Me.LoadProperty(CountryProperty, formDC("DLR_COUNTRY"))
        If formDC.ContainsKey("DEAL_TYPE") Then Me.LoadProperty(DealTypeProperty, formDC("DEAL_TYPE"))
    End Sub

End Class
