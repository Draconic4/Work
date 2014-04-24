Public Class Rule 'DataSet or Rule
    Private _Name As String
    Private _BrokenRule As Boolean

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property
    Public Property BrokenRule As Boolean
        Get
            Return _BrokenRule
        End Get
        Set(value As Boolean)
            _BrokenRule = value
        End Set
    End Property
    Public Property Rules As New List(Of Rule)

    Public Sub New(ByVal ruleOrSetName As String, ByVal ruleLayer As Boolean)
        _Name = ruleOrSetName
        _BrokenRule = ruleLayer
    End Sub
End Class
