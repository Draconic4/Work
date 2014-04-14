Namespace ValidationRuleData
    Public Class ApplicantManager

        Private _globalProperty As ProcessInfo
        Private _primaryApplicant As Applicant
        Private _businessApplicant As BusinessApplicant
        Private _applicantList As List(Of Applicant)

#Region "  Properties "
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperty
            End Get
        End Property
        Public ReadOnly Property Primary As Applicant
            Get
                Return _primaryApplicant
            End Get
        End Property
        Public ReadOnly Property Business As BusinessApplicant
            Get
                Return _businessApplicant
            End Get
        End Property
        Public ReadOnly Property Guarantor As Applicant
            Get
                Return _applicantList(0)
            End Get
        End Property
        Public ReadOnly Property CoApplicant As Applicant
            Get
                If Utility.HasGuarantor(GlobalProperty) Then Return _applicantList(1)
                Return _applicantList(0)
            End Get
        End Property
        Public ReadOnly Property CoApplicant2 As Applicant
            Get
                If Utility.HasCoApplicant2(GlobalProperty) Then Return _applicantList(1)
                Return Nothing
            End Get
        End Property
#End Region

#Region "  Data Access "
        Public Sub New(gProp As ProcessInfo)
            _globalProperty = gProp
            _applicantList = New List(Of Applicant)
        End Sub
        Public Shared Function Fetch(ByVal gProp As ProcessInfo, ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As ApplicantManager
            Dim am As New ApplicantManager(gProp)
            If currentRun Is Nothing Then Return am
            am.GenerateApplicants(previousRun, currentRun)
            Return am
        End Function
        Public Sub GenerateApplicants(pRun As Dictionary(Of String, Object), ByVal cRun As Dictionary(Of String, Object))
            If Utility.IsBusiness(GlobalProperty) Then 'Set up according to Global Properties
                '_businessApplicant = BusinessApplicant.Fetch("BUY", GlobalProperty)
            Else
                _primaryApplicant = Applicant.Fetch("BUY", GlobalProperty, pRun, cRun)
            End If
        End Sub
#End Region
    End Class
End Namespace

'If pRun.ContainsKey("COBUYER2_CODE") AndAlso Not String.IsNullOrWhiteSpace(d("COBUYER2_CODE")) Then
'    Dim cob3 As Applicant = Applicant.FetchExisting(New KeyBindInfo With {.KeyValue = "COBUYER2", .HumanReadable = "CoApplicant 2"}, _globalProperty)
'    If p.ContainsKey("COBUYER3_CODE") Then cob3.Populate(p)
'    cob3.Populate(d)
'    _applicantList.Add(cob3)
'End If
'If d.ContainsKey("COBUYER2_CODE") AndAlso Not String.IsNullOrWhiteSpace(d("COBUYER2_CODE")) Then
'    Dim cob2 As Applicant = Applicant.FetchExisting(New KeyBindInfo With {.KeyValue = "COBUYER2", .HumanReadable = "CoApplicant 1 or CoApplicant 2"}, _globalProperty)
'    If p.ContainsKey("COBUYER2_CODE") Then cob2.Populate(p)
'    cob2.Populate(d)
'    _applicantList.Insert(0, cob2)
'End If
'If d.ContainsKey("COBUYER1_CODE") AndAlso Not String.IsNullOrWhiteSpace(d("COBUYER1_CODE")) Then
'    Dim cob1 As Applicant = Applicant.FetchExisting(New KeyBindInfo With {.KeyValue = "COBUYER1", .HumanReadable = "CoApplicant 1 or Guarantor"}, _globalProperty)
'    If p.ContainsKey("COBUYER1_CODE") Then cob1.Populate(p)
'    cob1.Populate(d)
'    _applicantList.Insert(0, cob1)
'End If