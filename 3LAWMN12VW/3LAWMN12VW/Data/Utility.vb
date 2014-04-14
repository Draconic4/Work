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
#End Region

    Public Shared Function IsBusiness(ByVal pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        Return pi.ApplicationType.StartsWith("BUS", StringComparison.InvariantCultureIgnoreCase)
    End Function
    Public Shared Function IsLease(pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        Return pi.DealType = 1
    End Function
    Public Shared Function HasGuarantor(pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        If pi.ApplicationType = Utility.C_APPTYPE_BUSINESSGUARANTOR Or pi.ApplicationType = Utility.C_APPTYPE_BUSINESSGUARANTORCOAPP Or _
           pi.ApplicationType = Utility.C_APPTYPE_PRIMGUARANTOR Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function HasCoApplicant(pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        If pi.ApplicationType = Utility.C_APPTYPE_BUSINESSCOAPP OrElse pi.ApplicationType = Utility.C_APPTYPE_BUSINESSGUARANTORCOAPP OrElse pi.ApplicationType = Utility.C_APPTYPE_BUSINESSTWOCOAPP OrElse _
           pi.ApplicationType = Utility.C_APPTYPE_PRIMCOAPP OrElse pi.ApplicationType = Utility.C_APPTYPE_PRIMTWOCOAPP Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function HasCoApplicant2(pi As ValidationRuleData.ProcessInfo) As Boolean
        If pi Is Nothing Then Return False
        If pi.ApplicationType = Utility.C_APPTYPE_BUSINESSTWOCOAPP OrElse pi.ApplicationType = Utility.C_APPTYPE_PRIMTWOCOAPP Then
            Return True
        End If
        Return False
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