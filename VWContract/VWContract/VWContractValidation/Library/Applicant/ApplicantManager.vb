' Vocab: 
' ApplicationType: See Process Info for definition. Context and ApplicationType controls most of the ApplicantManager functionality.
' Applicants typing is just a means of slotting them into category. Most share information. Business/Primary are the exceptions both can have more responsibility depending on their role.
' Aristo:Buyer|VW:Primary Applicant - Purchaser of the vehicle (Can require up to 3 Addresses).
' Aristo:Buyer IsBusiness Flagged|VW:Business Applicant - Purchaser of the vehicle.
' Aristo:Cobuyer1|VW:CoApplicant OR Guarantor - Depends on ApplicationType.
' Aristo:Cobuyer2|VW:CoApplicant OR CoApplicant 2 - Depends on ApplicationType.

'Maintains the state of Applicants. Changing an applicant from Guarantor->CoApplicant->Not Applicable.
'Primary/Business Applicants are chosen by the details of the deal. Specifically the IsBusiness Flag on a contact.
'The second variable is the ApplicationType 
Namespace ValidationLib
    Public Class ApplicantManager
        Implements IReplicableFormField, IValidationTarget

        Public Const C_Purchaser As String = "Purchaser"
        Public Const C_Business As String = "Business"
        Public Const C_Contact As String = "Contact"
        Public Const C_Guarantor As String = "Guarantor"
        Public Const C_Cobuyer1 As String = "Cobuyer1"
        Public Const C_Cobuyer2 As String = "Cobuyer2"

        Private _globalProperty As ProcessInfo
        Private _applicantMap As New Dictionary(Of String, Integer)
        Private _applicantList As New List(Of Applicant)

#Region "  Properties "
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperty
            End Get
        End Property
        Public ReadOnly Property PrimaryApplicant As Applicant
            Get
                Return PopulateApplicant(C_Purchaser)
            End Get
        End Property
        Public ReadOnly Property BusinessApplicant As Applicant
            Get
                Return PopulateApplicant(C_Business)
            End Get
        End Property
        Public ReadOnly Property Contact As Applicant
            Get
                Return PopulateApplicant(C_Contact)
            End Get
        End Property
        Public ReadOnly Property Guarantor As Applicant
            Get
                Return PopulateApplicant(C_Guarantor)
            End Get
        End Property
        Public ReadOnly Property CoApplicant1 As Applicant
            Get
                Return PopulateApplicant(C_Cobuyer1)
            End Get
        End Property
        Public ReadOnly Property CoApplicant2 As Applicant
            Get
                Return PopulateApplicant(C_Cobuyer2)
            End Get
        End Property

#End Region

        Public Function PopulateApplicant(ByVal appKey As String) As Applicant
            Dim i As Integer = -1
            _applicantMap.TryGetValue(appKey, i)
            If i = -1 Then Return Nothing
            Return _applicantList(i)
        End Function

        Public Sub New(gProp As ProcessInfo)
            _globalProperty = gProp
        End Sub

        Public Shared Function Fetch(ByVal appArgs As ApplicantArgs) As ApplicantManager
            Dim apm As New ApplicantManager(appArgs.GlobalProperty)
            For i As Integer = 0 To ApplicantArgs.ApplicantTypes.COUNT
                appArgs.applicantType = i
                apm._applicantList.Add(Applicant.Fetch(appArgs))
            Next
            apm.GenerateMapping()
            Return apm
        End Function

        Public Sub GenerateMapping()
            _applicantMap.Clear()
            If ProcessUtility.IsBusiness(GlobalProperty) Then
                Dim businessIdx As Integer = 0
                _applicantMap.Add(C_Business, 0)
                _applicantMap.Add(C_Contact, 1) 'Contact is automatic
                businessIdx = 2
                If ProcessUtility.HasGuarantor(GlobalProperty) Then
                    _applicantMap.Add(C_Guarantor, businessIdx)
                    businessIdx += 1
                End If
                If ProcessUtility.HasCoApplicant(GlobalProperty) Then
                    _applicantMap.Add(C_Cobuyer1, businessIdx)
                    businessIdx += 1
                End If
                If ProcessUtility.HasCoApplicant2(GlobalProperty) Then
                    _applicantMap.Add(C_Cobuyer2, businessIdx)
                    businessIdx += 1
                End If
            Else
                Dim primaryAppIdx As Integer = 1
                _applicantMap.Add(C_Purchaser, 0)
                If ProcessUtility.HasGuarantor(GlobalProperty) Then
                    _applicantMap.Add(C_Guarantor, primaryAppIdx)
                    primaryAppIdx += 1
                End If
                If ProcessUtility.HasCoApplicant(GlobalProperty) Then
                    _applicantMap.Add(C_Cobuyer1, primaryAppIdx)
                    primaryAppIdx += 1
                End If
                If ProcessUtility.HasCoApplicant2(GlobalProperty) Then
                    _applicantMap.Add(C_Cobuyer2, primaryAppIdx)
                    primaryAppIdx += 1
                End If
            End If
        End Sub

        Public Sub ReplicateCurrentState(d As Dictionary(Of String, Object)) Implements IReplicableFormField.ReplicateCurrentState
            For Each i As Integer In _applicantMap.Values
                _applicantList(i).ReplicateCurrentState(d)
            Next
        End Sub

        Public Sub CheckRules() Implements IValidationTarget.CheckRules
            For Each i As Integer In _applicantMap.Values
                _applicantList(i).CheckRules()
            Next
        End Sub
        Public Sub Requirement(previousData As Object, validationRoot As Rule) Implements IValidationTarget.Requirement
            For Each i As KeyValuePair(Of String, Integer) In _applicantMap
                Dim v As New Rule(New ValidationFormat With {.Message = i.Key, .BottomLayer = False})
                _applicantList(i.Value).Requirement(previousData, v)
                validationRoot.Rules.Add(v)
            Next
        End Sub
    End Class
End Namespace
