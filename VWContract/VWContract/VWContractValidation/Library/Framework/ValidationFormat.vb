Imports System.Text.RegularExpressions

Public Class ValidationFormat
    Public Enum RuleDisplay
        NoCategory = 0
        CodingRequirement = 20
        AristoMissingInformation = 40
        RequiredResponse = 60
    End Enum

    Private _category As Integer
    Private _order As Integer
    Private _message As String
    Private _bottomLayer As Boolean

#Region "  Properties "
    Public Property DisplayCategory As Integer
        Get
            Return _category
        End Get
        Set(value As Integer)
            _category = value
        End Set
    End Property
    Public Property DisplayOrder As Integer
        Get
            Return _order
        End Get
        Set(value As Integer)
            _order = value
        End Set
    End Property
    Public Property Message As String
        Get
            Return _message
        End Get
        Set(value As String)
            _message = value
        End Set
    End Property
    Public Property BottomLayer As Boolean
        Get
            Return _bottomLayer
        End Get
        Set(value As Boolean)
            _bottomLayer = value
        End Set
    End Property
#End Region
    Public Shared Function ConvertBrokenRule(br As Csla.Rules.BrokenRule) As ValidationFormat
        Return ValidationFormat.ConvertMessage(br.Description)
    End Function
    Public Shared Function ConvertMessage(errorOrWarningWithFormat As String) As ValidationFormat
        Dim reg As New Regex("^(?<category>\d+)\s+(?<message>.*)")
        Dim m As Match = reg.Match(errorOrWarningWithFormat)
        If m.Success Then
            Dim category As Integer = 0
            Dim order As Integer = 0
            Integer.TryParse(m.Groups("category").Value, category)
            FormatCategoryAndOrder(category, order)
            Dim msg As String = m.Groups("message").Value

            Return New ValidationFormat With {.DisplayCategory = category, .DisplayOrder = order, .Message = msg}
        Else
            Return New ValidationFormat With {.DisplayCategory = 0, .DisplayOrder = 0, .Message = errorOrWarningWithFormat}
        End If
    End Function
    Public Shared Sub FormatCategoryAndOrder(category, order)
        If category > RuleDisplay.RequiredResponse Then
            order = category - RuleDisplay.RequiredResponse
            category = RuleDisplay.RequiredResponse
        ElseIf category > RuleDisplay.AristoMissingInformation Then
            order = category - RuleDisplay.AristoMissingInformation
            category = RuleDisplay.AristoMissingInformation
        ElseIf category > RuleDisplay.CodingRequirement Then
            order = category - RuleDisplay.CodingRequirement
            category = RuleDisplay.CodingRequirement
        Else
            order = category
            category = RuleDisplay.NoCategory
        End If
    End Sub
    Public Shared Sub GenerateAristoMessage(currentCategory As String, v As Rule, lst As List(Of String))
        Dim cRun As String = currentCategory
        For Each r As Rule In v.Rules
            cRun = cRun & vbTab & r.Name
            If r.Category = ValidationFormat.RuleDisplay.NoCategory Then
                Dim children As New List(Of String)
                GenerateAristoMessage(cRun, r, children)
                lst.AddRange(children)
            ElseIf r.Category <= ValidationFormat.RuleDisplay.AristoMissingInformation Then
                lst.Add(cRun)
                cRun = currentCategory
            Else
                cRun = currentCategory
            End If
        Next
    End Sub
End Class
Public Class ValidationFormatOrderFirst
    Inherits Comparer(Of ValidationFormat)
    Public Overrides Function Compare(x As ValidationFormat, y As ValidationFormat) As Integer
        If x.DisplayCategory > y.DisplayCategory Then
            Return 1
        ElseIf x.DisplayCategory < y.DisplayCategory Then
            Return -1
        Else
            Return x.DisplayOrder - y.DisplayOrder
        End If
    End Function
End Class