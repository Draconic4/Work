Imports Caliburn.Micro

Public Class ApplicantManagerViewModel
    Inherits Conductor(Of Screen).Collection.OneActive

    Public Property DataContext As ValidationLib.VWCreditProcess
    Public Property _gProp As ValidationLib.ProcessInfo
    Private _applicantSelected As String
    Private _applicantManager As ValidationLib.ApplicantManager
    Private _primaryApplicant As ApplicantViewModel
    Private _businessApplicant As ApplicantViewModel
    Private _contactApplicant As ApplicantViewModel
    Private _guarantorApplicant As ApplicantViewModel
    Private _coApplicant As ApplicantViewModel
    Private _coApplicant2 As ApplicantViewModel

#Region "  Properties "
    Public Property GlobalProperty As ValidationLib.ProcessInfo
        Get
            Return _gProp
        End Get
        Set(value As ValidationLib.ProcessInfo)
            _gProp = value
        End Set
    End Property
    Public ReadOnly Property ApplicantManager As ValidationLib.ApplicantManager
        Get
            Return _applicantManager
        End Get
    End Property
    Public Property ApplicantSelected As String
        Get
            Return _applicantSelected
        End Get
        Set(value As String)
            _applicantSelected = value
        End Set
    End Property
    Public ReadOnly Property ApplicantList As List(Of String)
        Get
            Dim lst As New List(Of String)
            If ProcessUtility.IsBusiness(GlobalProperty) Then
                lst.Add("Business")
                lst.Add("Contact")
                If ProcessUtility.HasGuarantor(GlobalProperty) Then lst.Add("Guarantor")
                If ProcessUtility.HasCoApplicant(GlobalProperty) Then lst.Add("Co-Applicant")
                If ProcessUtility.HasCoApplicant2(GlobalProperty) Then lst.Add("Co-Applicant 2")
            Else
                lst.Add("Primary")
                If ProcessUtility.HasGuarantor(GlobalProperty) Then lst.Add("Guarantor")
                If ProcessUtility.HasCoApplicant(GlobalProperty) Then lst.Add("Co-Applicant")
                If ProcessUtility.HasCoApplicant2(GlobalProperty) Then lst.Add("Co-Applicant 2")
            End If
            Return lst
        End Get
    End Property
    Public ReadOnly Property PrimaryApplicantChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ApplicantViewModel)
        End Get
    End Property
    Public ReadOnly Property BusinessApplicantChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ApplicantViewModel)
        End Get
    End Property
    Public ReadOnly Property ContactApplicantChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ApplicantViewModel)
        End Get
    End Property
    Public ReadOnly Property GuarantorApplicantChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ApplicantViewModel)
        End Get
    End Property
    Public ReadOnly Property CoApplicantChecked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ApplicantViewModel)
        End Get
    End Property
    Public ReadOnly Property CoApplicant2Checked() As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ApplicantViewModel)
        End Get
    End Property
#End Region
#Region "  Events "
    Public Sub ApplicantChanged(ByVal applicant As String)
        Select Case applicant
            Case "Primary"
                ChangeView(_primaryApplicant)
            Case "Business"
                ChangeView(_businessApplicant)
            Case "Contact"
                ChangeView(_contactApplicant)
            Case "Guarantor"
                ChangeView(_guarantorApplicant)
            Case "CoApplicant 1"
                ChangeView(_coApplicant)
            Case "CoApplicant 2"
                ChangeView(_coApplicant2)
            Case Else
                ChangeView(_primaryApplicant)
        End Select
    End Sub
#End Region
    
    Private Sub ChangeView(ByVal newView As Screen)
        Me.ActiveItem = newView
    End Sub
    Public Sub New(ByVal vwCredit As ValidationLib.VWCreditProcess)
        If vwCredit.ApplicantManager Is Nothing Then Exit Sub
        _applicantManager = vwCredit.ApplicantManager
        If ProcessUtility.IsBusiness(vwCredit.GlobalProperty) Then
            _applicantSelected = "Primary"
        Else
            _applicantSelected = "Business"
        End If
        If vwCredit.ApplicantManager.PrimaryApplicant IsNot Nothing Then _primaryApplicant = New ApplicantViewModel(vwCredit.ApplicantManager.PrimaryApplicant, vwCredit.GlobalProperty)
        If vwCredit.ApplicantManager.Guarantor IsNot Nothing Then _guarantorApplicant = New ApplicantViewModel(vwCredit.ApplicantManager.Guarantor, vwCredit.GlobalProperty)
        If vwCredit.ApplicantManager.CoApplicant1 IsNot Nothing Then _coApplicant = New ApplicantViewModel(vwCredit.ApplicantManager.CoApplicant1, vwCredit.GlobalProperty)
        If vwCredit.ApplicantManager.BusinessApplicant IsNot Nothing Then _businessApplicant = New ApplicantViewModel(vwCredit.ApplicantManager.BusinessApplicant, vwCredit.GlobalProperty)
        If vwCredit.ApplicantManager.Contact IsNot Nothing Then _contactApplicant = New ApplicantViewModel(vwCredit.ApplicantManager.Contact, vwCredit.GlobalProperty)
    End Sub
End Class
