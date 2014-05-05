Public Class Rule 'DataSet or Rule
    Private _ValidationFormat As ValidationFormat
    Private _BrokenRule As Boolean

    Public Property Name As String
        Get
            Return _ValidationFormat.Message
        End Get
        Set(value As String)
            _ValidationFormat.Message = value
        End Set
    End Property
    Public ReadOnly Property Category As Integer
        Get
            Return _ValidationFormat.DisplayCategory
        End Get
    End Property
    Public Property BrokenRule As Boolean
        Get
            Return _ValidationFormat.BottomLayer
        End Get
        Set(value As Boolean)
            _ValidationFormat.BottomLayer = value
        End Set
    End Property
    Public Property Rules As New List(Of Rule)

    Public Sub New(ByVal ruleOrSetName As ValidationFormat)
        _ValidationFormat = ruleOrSetName
    End Sub
    Public Sub Clear()
        Rules.Clear()
    End Sub
End Class
