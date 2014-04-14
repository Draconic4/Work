Imports Csla
Public Class Utility

#Region "  VW Application Type "
    Public Const C_APPTYPE_BUSINESS As String = "BUSINESS"
    Public Const C_APPTYPE_BUSINESSCOAPP As String = "BUSINESS WITH COAPPLICANT"
    Public Const C_APPTYPE_BUSINESSGUARANTOR As String = "BUSINESS WITH GUARANTOR"
    Public Const C_APPTYPE_BUSINESSTWOCOAPP As String = "BUSINESS WITH TWO COAPPLICANTS"
    Public Const C_APPTYPE_BUSINESSGUARANTORCOAPP As String = "BUSINESS WITH GUARANTOR AND COAPPLICANT"
    Public Const C_APPTYPE_PRIM As String = "INDIVIDUAL"
    Public Const C_APPTYPE_PRIMCOAPP As String = "INDIVIDUAL AND COAPPLICANT"
    Public Const C_APPTYPE_PRIMGUARANTOR As String = "INDIVIDUAL WITH GUARANTOR"
    Public Const C_APPTYPE_PRIMTWOCOAPP As String = "INDIVIDUAL AND TWO COAPPLICANTS"
#End Region
#Region "  VW Finance Type"
    Public Const FINANCE_TYPE_RETAIL As String = "Retail"
    Public Const FINANCE_TYPE_LEASE As String = "Lease"
    Public Const FINANCE_TYPE_BALLOON As String = "Balloon"
#End Region
#Region "  VW Product Type"
    Public Const PRODUCT_TYPE_SIMPLE_INTEREST As String = "Simple Interest Method"
    Public Const PRODUCT_TYPE_SINGLE As String = "Single"
    Public Const PRODUCT_TYPE_MONTHLY As String = "Monthly"
#End Region

#Region "  Country "
    Public Shared Function IsCanadian(pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        Return pi.Country.StartsWith("CA")
    End Function
    Public Shared Function GetCountryLocationList() As List(Of String)
        Dim result As New List(Of String)
        result.Add("")
        result.Add("US")
        result.Add("CA")
        Return result
    End Function
#End Region
#Region "  Province Or State "
    Public Shared Function GetLocaleList(ByVal pi As ValidationRuleData.ProcessInfo) As List(Of String)
        If pi Is Nothing Then Return New List(Of String)
        If IsCanadian(pi) Then Return GetProvinceList()
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
    Public Shared Function ConvertToAbbreviatedStateOrProvince(pi As ValidationRuleData.ProcessInfo, state As String) As String
        If state.Length = 2 Then Return state
        If IsCanadian(pi) Then Return ConvertToAbbreviatedProvince(state)
        Return ConvertToAbbreviatedState(state)
    End Function
    Public Shared Function ConvertToAbbreviatedProvince(prov As String) As String
        prov = prov.ToUpper
        Select Case prov
            Case prov.StartsWith("A")
                Return "AB"
            Case prov.StartsWith("B")
                Return "BC"
            Case prov.StartsWith("M")
                Return "MB"
            Case prov.StartsWith("O")
                Return "ON"
            Case prov.StartsWith("P")
                Return "PE"
            Case prov.StartsWith("Q")
                Return "QC"
            Case prov.StartsWith("S")
                Return "SK"
            Case prov.StartsWith("Y")
                Return "YT"
            Case Else
                If prov.StartsWith("NU") Then
                    Return "NO"
                ElseIf prov.StartsWith("NOV") Then
                    Return "NS"
                ElseIf prov.StartsWith("NOR") Then
                    Return "NT"
                ElseIf prov.StartsWith("NEW B") Then
                    Return "NB"
                Else
                    Return "NL"
                End If
        End Select
        Return prov
    End Function
    Public Shared Function ConvertToAbbreviatedState(state As String) As String
        state = state.ToUpper
        Select Case state
            Case state.StartsWith("AL")
                If state.StartsWith("ALAB") Then Return "AL"
                Return "AK"
            Case state.StartsWith("AM")
                Return "AS"
            Case state.StartsWith("AR")
                If state.StartsWith("ARIZ") Then Return "AZ"
                Return "AR"
            Case state.StartsWith("CA")
                Return "CA"
            Case state.StartsWith("CO")
                If state.StartsWith("COLO") Then Return "CO"
                Return "CT"
            Case state.StartsWith("DE")
                Return "DE"
            Case state.StartsWith("DI")
                Return "DC"
            Case state.StartsWith("FE")
                Return "FM"
            Case state.StartsWith("FL")
                Return "FL"
            Case state.StartsWith("G")
                Return "G"
            Case state.StartsWith("H")
                Return "H"
            Case state.StartsWith("IL")
                Return "IL"
            Case state.StartsWith("IN")
                Return "ID"
            Case state.StartsWith("IO")
                Return "IA"
            Case state.StartsWith("KA")
                Return "KS"
            Case state.StartsWith("KE")
                Return "KY"
            Case state.StartsWith("L")
                Return "LA"
            Case state.StartsWith("MA")
                If state.StartsWith("MAI") Then
                    Return "ME"
                ElseIf state.StartsWith("MAS") Then
                    Return "MA"
                ElseIf state.StartsWith("MARS") Then
                    Return "MH"
                Else
                    Return "MD"
                End If
            Case state.StartsWith("MI")
                If state.StartsWith("MIC") Then
                    Return "MI"
                ElseIf state.StartsWith("MIN") Then
                    Return "MN"
                ElseIf state.StartsWith("MISSO") Then
                    Return "MO"
                Else
                    Return "MS"
                End If
            Case state.StartsWith("MO")
                Return "MO"
            Case state.StartsWith("NE")
                If state.StartsWith("NEB") Then
                    Return "NE"
                ElseIf state.StartsWith("NEV") Then
                    Return "NV"
                ElseIf state.StartsWith("NEW H") Then
                    Return "NH"
                ElseIf state.StartsWith("NEW J") Then
                    Return "NJ"
                ElseIf state.StartsWith("NEW M") Then
                    Return "NM"
                Else
                    Return "NY"
                End If
            Case state.StartsWith("NO")
                If state.StartsWith("NORTH C") Then
                    Return "NC"
                ElseIf state.StartsWith("NORTH D") Then
                    Return "ND"
                Else
                    Return "MP"
                End If
            Case state.StartsWith("OH")
                Return "OH"
            Case state.StartsWith("OK")
                Return "OK"
            Case state.StartsWith("OR")
                Return "OR"
            Case state.StartsWith("PA")
                Return "PW"
            Case state.StartsWith("PE")
                Return "PA"
            Case state.StartsWith("PU")
                Return "PR"
            Case state.StartsWith("RH")
                Return "RI"
            Case state.StartsWith("SO")
                If state.StartsWith("SOUTH C") Then
                    Return "SC"
                Else
                    Return "SD"
                End If
            Case state.StartsWith("TE")
                If state.StartsWith("TEN") Then
                    Return "TN"
                Else
                    Return "TX"
                End If
            Case state.StartsWith("U")
                Return "UT"
            Case state.StartsWith("VE")
                Return "VT"
            Case state.StartsWith("VI")
                If state.StartsWith("VIRGIN I") Then
                    Return "VI"
                Else
                    Return "VA"
                End If
            Case state.StartsWith("WA")
                Return "WA"
            Case state.StartsWith("WE")
                Return "WV"
            Case state.StartsWith("WI")
                Return "WI"
            Case Else
                Return "WY"
        End Select
        Return state
    End Function
#End Region

    Public Shared Function IsBusiness(ByVal pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        Return pi.ApplicationType.StartsWith("BUS", StringComparison.InvariantCultureIgnoreCase)
    End Function
    Public Shared Function IsLease(pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        Return pi.DealType = 1
    End Function

#Region "  CSLA Helpers "
    Public Shared Function SimplePopulateKey(pi As PropertyInfo(Of String)) As String
        Return pi.FriendlyName
    End Function
#End Region

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