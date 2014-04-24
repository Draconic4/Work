Imports System.Linq
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace ValidationRuleData

    Public Class ValidationRuleOrSet
        Public Property Name As String
        Public Property Rules As New List(Of ValidationRuleOrSet)

        Public Sub New(ByVal ruleOrSetName As String)
            Name = ruleOrSetName
        End Sub
    End Class

    Public Class ValidationRuleUIModel
        Private _parent As ValidationRuleUIModel
        Private _ruleOrSet As ValidationRuleOrSet
        Private _rules As ObservableCollection(Of ValidationRuleUIModel)

        Public ReadOnly Property RuleOrSetName As String
            Get
                Return _ruleOrSet.RuleOrSetName
            End Get
        End Property

        Public ReadOnly Property Rules As ObservableCollection(Of ValidationRuleUIModel)
            Get
                Return _rules
            End Get
        End Property

        Public Sub New(vros As ValidationRuleOrSet, ByVal parentVRVM As ValidationRuleUIModel)
            _ruleOrSet = vros
            _parent = parentVRVM
            _rules = New ObservableCollection(Of ValidationRuleUIModel)(From rule As ValidationRuleOrSet In _ruleOrSet.Rules
                     Select New ValidationRuleUIModel(rule, Me))
        End Sub
    End Class

    Public Class ValidationRuleSetUIModel
        Private _root As ValidationRuleUIModel
        Public ReadOnly Property Root As ValidationRuleUIModel
            Get
                Return _root
            End Get
        End Property
        Public Sub New(rootViewModel As ValidationRuleOrSet)
            _root = New ValidationRuleUIModel(rootViewModel, Nothing)
        End Sub
    End Class
End Namespace
