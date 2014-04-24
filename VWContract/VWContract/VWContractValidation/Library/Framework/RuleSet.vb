Imports System.Collections.ObjectModel

Public Class RuleSet
    Private _root As RuleControlModel
    Private _rules As ObservableCollection(Of RuleControlModel)

    Public ReadOnly Property Rules As ObservableCollection(Of RuleControlModel)
        Get
            Return _rules
        End Get
    End Property

    Public Sub New(ByVal vr As Rule)
        _root = New RuleControlModel(vr, Nothing)
        _rules = New ObservableCollection(Of RuleControlModel)(New RuleControlModel() {_root})
    End Sub
End Class
