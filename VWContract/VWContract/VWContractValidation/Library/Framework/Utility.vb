Imports Csla

Public Class Utility

#Region "  Global Business Rules "
    Public Class HasRequiredValueString
        Inherits Csla.Rules.BusinessRule

        Public OverrideMessage As String
        Public IsWarning As Boolean

        Public Sub New(ByVal pi As PropertyInfo(Of String), ByVal overrideMessage As String, ByVal warning As Boolean)
            Me.PrimaryProperty = pi
            Me.OverrideMessage = overrideMessage
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Object = context.Target
            Dim x As String = t.GetPropertyValue(Me.PrimaryProperty)
            If IsInvalidValue(x) Then
                context.AddErrorResult(Utility.FormatWarningOrErrorMessage(Me, OverrideMessage, IsWarning))
            End If
        End Sub
        Private Function IsInvalidValue(ByVal val As String) As Boolean
            Return String.IsNullOrWhiteSpace(val)
        End Function
    End Class
    Public Class HasRequiredValueDate
        Inherits Csla.Rules.BusinessRule

        Public Property OverrideMessage As String
        Public Property IsWarning As Boolean

        Public Sub New(ByVal pi As PropertyInfo(Of String), ByVal overrideMessage As String)
            Me.PrimaryProperty = pi
            Me._overrideMessage = overrideMessage
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As Object = context.Target
            Dim x As String = t.GetPropertyValue(Me.PrimaryProperty)
            If IsInvalidValue(x) Then
                context.AddErrorResult(FormatWarningOrErrorMessage(Me, OverrideMessage, IsWarning))
            End If
        End Sub
        Private Function IsInvalidValue(ByVal val As String) As Boolean
            Return Not IsDate(val)
        End Function
    End Class
    Public Shared Function FormatWarningOrErrorMessage(ByVal r As Rules.BusinessRule, ByVal overrideMessage As String, ByVal warning As Boolean) As String
        Dim msg As String = r.Priority & " " & IIf(warning, "Validation Warning - ", "Validation Error - ")
        If Not String.IsNullOrWhiteSpace(overrideMessage) Then
            msg = msg & overrideMessage
        Else
            msg = msg & r.PrimaryProperty.Name & " is required."
        End If
        Return msg
    End Function
#End Region
End Class
