Namespace ValidationLib
    Public Class Applicant
        Implements IReplicableFormField, IValidationTarget

        Private _applicantType As String
        Private _customerID As ApplicantID
        Private _businessID As BusinessApplicantID
        Private _Address As Dictionary(Of String, Address)
        Private _altContact As AltContact

#Region "  Properties "
        Public ReadOnly Property CustomerName As ApplicantID
            Get
                Return _customerID
            End Get
        End Property
        Public ReadOnly Property BusinessName As BusinessApplicantID
            Get
                Return _businessID
            End Get
        End Property
        Public ReadOnly Property Addresses As Dictionary(Of String, Address)
            Get
                Return _Address
            End Get
        End Property
        Public ReadOnly Property AltContact As AltContact
            Get
                Return _altContact
            End Get
        End Property
#End Region

#Region "  Data Access "
        Public Sub New()
        End Sub
        Public Shared Function Fetch(appArgs As ApplicantArgs) As Applicant
            Dim app As New Applicant()
            If ProcessUtility.IsBusiness(appArgs.GlobalProperty) Then
                app._businessID = BusinessApplicantID.Fetch(appArgs)
                app._Address = New Dictionary(Of String, Address) From {{Address.C_WORKADDRESS, Address.Fetch(Address.C_WORKADDRESS, appArgs)}}
                app._altContact = AltContact.Fetch(appArgs)
            ElseIf appArgs.applicantType = ApplicantArgs.ApplicantTypes.BUYER1 Then
                app._customerID = ApplicantID.Fetch(appArgs)
                app._Address = New Dictionary(Of String, Address) From {{Address.C_HOMEADDRESS, Address.Fetch(Address.C_HOMEADDRESS, appArgs)},
                                                                        {Address.C_BILLINGADDRESS, Address.Fetch(Address.C_BILLINGADDRESS, appArgs)},
                                                                        {Address.C_GARAGEADDRESS, Address.Fetch(Address.C_GARAGEADDRESS, appArgs)}}
                app._altContact = AltContact.Fetch(appArgs)
            Else
                app._customerID = ApplicantID.Fetch(appArgs)
                app._Address = New Dictionary(Of String, Address) From {{Address.C_HOMEADDRESS, Address.Fetch(Address.C_HOMEADDRESS, appArgs)}}
                app._altContact = AltContact.Fetch(appArgs)
            End If
            Return app
        End Function
        Public Sub ReplicateCurrentState(d As Dictionary(Of String, Object)) Implements IReplicableFormField.ReplicateCurrentState
            If _customerID IsNot Nothing Then CustomerName.ReplicateCurrentState(d)
            If _businessID IsNot Nothing Then BusinessName.ReplicateCurrentState(d)
            For Each addr As Address In _Address.Values
                addr.ReplicateCurrentState(d)
            Next
            If _altContact IsNot Nothing Then AltContact.ReplicateCurrentState(d)
        End Sub
#End Region
#Region "  Business Rules "
        Public Sub CheckRules() Implements IValidationTarget.CheckRules
            If _customerID IsNot Nothing Then CustomerName.CheckRules()
            If _businessID IsNot Nothing Then BusinessName.CheckRules()
            For Each addr As Address In _Address.Values
                addr.CheckRules()
            Next
            If _altContact IsNot Nothing Then AltContact.CheckRules()
        End Sub
        Public Sub Requirement(previousData As Object, validationRoot As Rule) Implements IValidationTarget.Requirement
            If Not TypeOf previousData Is ValidationTargetData Then Exit Sub
            Dim pData As ValidationTargetData = DirectCast(previousData, ValidationTargetData)
            Dim v As New Rule(New ValidationFormat With {.Message = pData.GetTypeName, .BottomLayer = False})
            If _customerID IsNot Nothing Then CustomerName.Requirement(Nothing, v)
            If _businessID IsNot Nothing Then BusinessName.Requirement(Nothing, v)
            For Each addr As KeyValuePair(Of String, Address) In _Address
                addr.Value.Requirement(New ValidationTargetData With {.GetTypeName = addr.Key}, v)
            Next
            If _altContact IsNot Nothing Then AltContact.Requirement(Nothing, v)
            validationRoot.Rules.Add(v)
        End Sub
#End Region
    End Class
End Namespace