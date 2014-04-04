Imports Csla

Public Class ProcessInfo
    Inherits BusinessBase(Of ProcessInfo)

    'Global Business Rule Dependencies
    Public Shared ReadOnly ApplicationTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ApplicationType), "DEAL_APPLICANTTYPE", "INDIV")
    Public Shared ReadOnly DealTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DealType), "DEAL_STATUS", "FINANCE")
    Public Shared ReadOnly CountryProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Country), "DLR_COUNTRY", "US")

#Region "  Properties "
    Public ReadOnly Property IsCanadian As Boolean
        Get
            Return Country.StartsWith("C")
        End Get
    End Property
    Public ReadOnly Property IsLease As Boolean
        Get
            Return DealType.StartsWith("L")
        End Get
    End Property
    Public ReadOnly Property Country As String
        Get
            Return GetProperty(CountryProperty)
        End Get
    End Property
    Public Property ApplicationType As String
        Get
            Return GetProperty(ApplicationTypeProperty)
        End Get
        Set(value As String)
            SetProperty(ApplicationTypeProperty, ApplicationType)
        End Set
    End Property
    Public Property DealType As String
        Get
            Return GetProperty(DealTypeProperty)
        End Get
        Set(value As String)
            SetProperty(DealTypeProperty, value)
        End Set
    End Property
#End Region

#Region "  Data Access "
    Public Sub New()
    End Sub
    Public Shared Function FetchExisting() As ProcessInfo
        Return New ProcessInfo
    End Function
    Public Sub Populate(ByVal d As Dictionary(Of String, Object))
        If d Is Nothing Then Exit Sub
        PopulateField(ApplicationTypeProperty, d)
        PopulateField(DealTypeProperty, d)
        PopulateField(CountryProperty, d)
    End Sub
    Public Sub PopulateField(ByVal pi As PropertyInfo(Of String), ByVal d As Dictionary(Of String, Object))
        Dim key As String = Utility.SimplePopulateKey(pi)
        If d.ContainsKey(key) Then LoadProperty(pi, d(key))
    End Sub
#End Region
End Class
