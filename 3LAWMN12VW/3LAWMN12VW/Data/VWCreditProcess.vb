Imports Csla

Public Class VWCreditProcess
    Inherits Csla.BusinessBase(Of VWCreditProcess)

    Public Shared ReadOnly DealTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DealType), "DEAL_STATUS", "0")
    Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "DLR_COUNTRY", "US")

#Region "  Properties "
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
#End Region

    Public Sub New()
    End Sub

    Public Shared Function FetchExisting(ByVal formDC As Dictionary(Of String, Object), ByVal aristoDC As Dictionary(Of String, Object)) As VWCreditProcess
        Dim dp As New VWCreditProcess
        dp.LoadFormDataContext(formDC)
        dp.LoadFormDataContext(aristoDC)
        Return dp
    End Function

    Public Sub LoadFormDataContext(ByVal formDC As Dictionary(Of String, Object))
        If formDC.ContainsKey("DLR_COUNTRY") Then Me.LoadProperty(CountryProperty, formDC("DLR_COUNTRY"))
        If formDC.ContainsKey("DEAL_TYPE") Then Me.LoadProperty(DealTypeProperty, formDC("DEAL_TYPE"))
    End Sub

End Class
