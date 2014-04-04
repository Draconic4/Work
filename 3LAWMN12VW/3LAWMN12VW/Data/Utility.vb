Imports Csla

Public Class Utility
    Public Shared Function ProvinceOrStateConverter(ByVal prov As String, ByVal isCanadian As Boolean) As String
        If isCanadian Then Return ProvinceConverter(prov)
        Return StateConverter(prov)
    End Function
    Public Shared Function StateConverter(ByVal state As String)
        Return "MN"
    End Function
    Public Shared Function ProvinceConverter(ByVal prov As String)
        Return "BC"
    End Function
    Public Shared Function GetLocaleList(ByVal isCanadian As Boolean) As List(Of String)
        If isCanadian Then Return GetProvinceList()
        Return GetStateList()
    End Function
    Public Shared Function GetProvinceList() As List(Of String)
        Dim result As New List(Of String)
        result.Add("")

        result.Add("AB")
        result.Add("BC")
        result.Add("MB")
        result.Add("NB")
        result.Add("NL")
        result.Add("NT")
        result.Add("NS")
        result.Add("NU")
        result.Add("ON")
        result.Add("PE")
        result.Add("QC")
        result.Add("SK")
        result.Add("YT")
        Return result
    End Function
    Public Shared Function GetStateList() As List(Of String)
        Dim result As New List(Of String)
        result.Add("")
        result.Add("AL")
        result.Add("AK")
        result.Add("AZ")
        result.Add("AR")
        result.Add("CA")
        result.Add("CO")
        result.Add("CT")
        result.Add("DE")
        result.Add("FL")
        result.Add("GA")
        result.Add("HI")
        result.Add("ID")
        result.Add("IL")
        result.Add("IN")
        result.Add("IA")
        result.Add("KS")
        result.Add("KY")
        result.Add("LA")
        result.Add("ME")
        result.Add("MD")
        result.Add("MA")
        result.Add("MI")
        result.Add("MN")
        result.Add("MS")
        result.Add("MO")
        result.Add("MT")
        result.Add("NE")
        result.Add("NV")
        result.Add("NH")
        result.Add("NJ")
        result.Add("NM")
        result.Add("NY")
        result.Add("NC")
        result.Add("ND")
        result.Add("OH")
        result.Add("OK")
        result.Add("OR")
        result.Add("PA")
        result.Add("RI")
        result.Add("SC")
        result.Add("SD")
        result.Add("TN")
        result.Add("TX")
        result.Add("UT")
        result.Add("VT")
        result.Add("VA")
        result.Add("WA")
        result.Add("WV")
        result.Add("WI")
        result.Add("WY")
        Return result
    End Function
    Public Shared Function GetCountryLocationList() As List(Of String)
        Dim result As New List(Of String)
        result.Add("")
        result.Add("US")
        result.Add("CA")
        Return result
    End Function
    Public Shared Function SimplePopulateKey(pi As PropertyInfo(Of String)) As String
        Return pi.FriendlyName
    End Function

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
End Class
