Imports Csla

Public Class Utility
#Region "  Global Business Rules "

    Public Class HasRequiredValueString
        Inherits Csla.Rules.BusinessRule

        Public _overrideMessage As String

        Public Sub New(ByVal pi As PropertyInfo(Of String), ByVal overrideMessage As String)
            Me.PrimaryProperty = pi
            Me._overrideMessage = overrideMessage
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Object = context.Target
            Dim x As String = t.GetPropertyValue(Me.PrimaryProperty)
            If IsInvalidValue(x) Then
                Dim msg As String = _overrideMessage
                If String.IsNullOrWhiteSpace(msg) Then msg = "Validation Error - " & PrimaryProperty.Name & " is required."
                context.AddErrorResult(msg)
            End If
        End Sub
        Private Function IsInvalidValue(ByVal val As String) As Boolean
            Return String.IsNullOrWhiteSpace(val)
        End Function
    End Class
    Public Class HasRequiredValueDate
        Inherits Csla.Rules.BusinessRule

        Public _overrideMessage As String

        Public Sub New(ByVal pi As PropertyInfo(Of String), ByVal overrideMessage As String)
            Me.PrimaryProperty = pi
            Me._overrideMessage = overrideMessage
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Object = context.Target
            Dim x As String = t.GetPropertyValue(Me.PrimaryProperty)
            If IsInvalidValue(x) Then
                Dim msg As String = _overrideMessage
                If String.IsNullOrWhiteSpace(msg) Then msg = "Validation Error - " & PrimaryProperty.Name & " is required."
                context.AddErrorResult(msg)
            End If
        End Sub
        Private Function IsInvalidValue(ByVal val As String) As Boolean
            Return Not IsDate(val)
        End Function
    End Class
#End Region
End Class
