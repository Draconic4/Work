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
    Public ReadOnly Property HasPrimaryApplicant As Boolean
        Get
            If _primaryApplicant Is Nothing Then Return False
            Return True
        End Get
    End Property
    Public ReadOnly Property Primary As Applicant
        Get
            Return _primaryApplicant
        End Get
    End Property
    Public ReadOnly Property IsBusiness As Boolean
        Get
            If _businessApplicant Is Nothing Then Return False
            Return True
        End Get
    End Property
    Public ReadOnly Property Business As BusinessApplicant
        Get
            Return _businessApplicant
        End Get
    End Property
    Public ReadOnly Property HasGuarantor As Boolean
        Get
            If GlobalProperty Is Nothing OrElse _applicantList(0) Is Nothing Then Return False
            If GlobalProperty.ApplicationType.Contains("GUARANTOR") Then Return True
            Return False
        End Get
    End Property
    Public ReadOnly Property Guarantor As Applicant
        Get
            If HasGuarantor Then
                Return _applicantList(0)
            End If
            Return Nothing
        End Get
    End Property
    Public ReadOnly Property HasCoApplicant As Boolean
        Get
            Dim coAppIdx As Integer = 0
            If GlobalProperty.ApplicationType.Contains("GUARANTOR") Then coAppIdx = 1
            If _applicantList.Count >= coAppIdx + 1 AndAlso _applicantList(coAppIdx) IsNot Nothing Then Return True
            Return False
        End Get
    End Property
    Public ReadOnly Property CoApplicant As Applicant
        Get
            If HasCoApplicant Then
                If GlobalProperty.ApplicationType.Contains("GUARANTOR") Then Return _applicantList(1)
                Return _applicantList(0)
            End If
            Return Nothing
        End Get
    End Property
    Public ReadOnly Property HasCoApplicant2 As Boolean
        Get
            If GlobalProperty Is Nothing Then Return False
            If GlobalProperty.ApplicationType.Contains("TWO") Then Return True
            Return False
        End Get
    End Property
    Public ReadOnly Property CoApplicant2 As Applicant
        Get
            If HasCoApplicant2 Then
                If GlobalProperty.ApplicationType.Contains("GUARANTOR") Then Return _applicantList(2)
                Return _applicantList(1)
            End If
            Return Nothing
        End Get
    End Property
#End Region

#Region "  Data Access "
    Public Sub New(gProp As ProcessInfo)
        _globalProperty = gProp
        _applicantList = New List(Of Applicant)
    End Sub
    Public Sub Populate(ByVal p As Dictionary(Of String, Object), ByVal d As Dictionary(Of String, Object))
        If d.ContainsKey("BUY_ISBUSINESS") AndAlso d("BUY_ISBUSINESS") Then
            _businessApplicant = BusinessApplicant.FetchExisting("BUY", _globalProperty)
            _businessApplicant.Populate(p)
            _businessApplicant.Populate(d)
        Else
            _primaryApplicant = Applicant.FetchExisting("BUY", _globalProperty)
            _primaryApplicant.Populate(p)
            _primaryApplicant.Populate(d)
        End If
        If d.ContainsKey("COBUYER3_CODE") AndAlso Not String.IsNullOrWhiteSpace(d("COBUYER3_CODE")) Then
            Dim cob3 As Applicant = Applicant.FetchExisting("COBUYER3", _globalProperty)
            If p.ContainsKey("COBUYER3_CODE") Then cob3.Populate(p)
            cob3.Populate(d)
            _applicantList.Add(cob3)
        End If
        If d.ContainsKey("COBUYER2_CODE") AndAlso Not String.IsNullOrWhiteSpace(d("COBUYER2_CODE")) Then
            Dim cob2 As Applicant = Applicant.FetchExisting("COBUYER2", _globalProperty)
            If p.ContainsKey("COBUYER3_CODE") Then cob2.Populate(p)
            cob2.Populate(d)
            _applicantList.Insert(0, cob2)
        End If
        If d.ContainsKey("COBUYER1_CODE") AndAlso Not String.IsNullOrWhiteSpace(d("COBUYER2_CODE")) Then
            Dim cob1 As Applicant = Applicant.FetchExisting("COBUYER1", _globalProperty)
            If p.ContainsKey("COBUYER1_CODE") Then cob1.Populate(p)
            cob1.Populate(d)
            _applicantList.Insert(0, cob1)
        End If
    End Sub
#End Region

    Shared Function Fetch(formDC As Dictionary(Of String, Object), aristoDC As Dictionary(Of String, Object), globalProperty As ProcessInfo) As ApplicantManager
        Return New ApplicantManager(globalProperty)
    End Function

End Class
