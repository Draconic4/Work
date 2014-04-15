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
            Dim cobCode As String = String.Empty
            Dim cob2Code As String = String.Empty
            If cRun.TryGetValue("COBUYER1_CODE", cobCode) Then
                _applicantList.Add(Applicant.Fetch("COBUYER1", GlobalProperty, pRun, cRun))
            ElseIf cRun.TryGetValue("COBUYER2_CODE", cob2Code) Then
                _applicantList.Add(Applicant.Fetch("COBUYER2", GlobalProperty, pRun, cRun))
            End If
        End Sub
#End Region

#Region "  Business Rules "
        Public Sub CheckRules()
            If _primaryApplicant IsNot Nothing Then _primaryApplicant.CheckRules()
            'If _businessApplicant IsNot Nothing Then _businessApplicant.CheckRules()
            For Each app As Applicant In _applicantList
                app.CheckRules()
            Next
        End Sub
#End Region
    End Class
End Namespace