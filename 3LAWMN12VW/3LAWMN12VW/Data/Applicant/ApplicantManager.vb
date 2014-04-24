Imports System.Collections.ObjectModel

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
        Public Sub CheckRules(ByVal aristo As Boolean)
            If _primaryApplicant IsNot Nothing Then _primaryApplicant.CheckRules(aristo)
            'If _businessApplicant IsNot Nothing Then _businessApplicant.CheckRules()
            For Each app As Applicant In _applicantList
                app.CheckRules(aristo)
            Next
        End Sub
        Public Sub RequirementList(vroot As ValidationRuleOrSet)
            Dim vrs As New ValidationRuleOrSet()
            vrs.RuleOrSetName = "Primary Applicant"
            For i As Integer = 0 To 32
                Dim temp As New ValidationRuleOrSet()
                temp.RuleOrSetName = "Broken Rule 000" & i
                vrs.Rules.Add(temp)
            Next
            vroot.Rules.Add(vrs)
            'Dim vs As New ValidSection("Applicants")
            'If Not Utility.IsBusiness(GlobalProperty) Then
            '    Dim primary As New ValidSection("Primary Applicant")
            '    _primaryApplicant.RequirementList(primary)
            '    vs.SubSections.Add(primary)
            'Else
            '    Dim business As New ValidSection("Business")
            '    _businessApplicant.RequirementList(business)
            '    vs.SubSections.Add(business)
            'End If
            'If Utility.HasGuarantor(GlobalProperty) Then
            '    Dim guarantor As New ValidSection("Guarantor")
            '    _applicantList(0).RequirementList(guarantor)
            '    vs.SubSections.Add(guarantor)
            '    If Utility.HasCoApplicant(GlobalProperty) Then
            '        Dim coApplicant As New ValidSection("CoApplicant")
            '        _applicantList(1).RequirementList(coApplicant)
            '        vs.SubSections.Add(coApplicant)
            '    End If
            'ElseIf Utility.HasCoApplicant(GlobalProperty) Then
            '    Dim coApplicant As New ValidSection("CoApplicant")
            '    _applicantList(0).RequirementList(coApplicant)
            '    vs.SubSections.Add(coApplicant)
            '    If Utility.HasCoApplicant2(GlobalProperty) Then
            '        Dim coApplicant2 As New ValidSection("CoApplicant 2")
            '        _applicantList(0).RequirementList(coApplicant2)
            '        vs.SubSections.Add(coApplicant2)
            '    End If
            'End If
            'vroot.SubSections.Add(vs)
        End Sub
#End Region
    End Class
End Namespace