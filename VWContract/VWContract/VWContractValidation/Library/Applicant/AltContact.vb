Imports Csla
Imports VWContractValidation.ValidationLib

Public Class AltContact
    Inherits BaseFormField
    Implements IReplicableFormField, IValidationTarget

    Public Shared ReadOnly ApplicantTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, AltContact)(Function(c) (c.ApplicantType), String.Empty, "Primary")
    Public Shared ReadOnly HomePhoneProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, AltContact)(Function(c) (c.HomePhone), "_HOMEPHONE", String.Empty)
    Public Shared ReadOnly WorkPhoneProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, AltContact)(Function(c) (c.WorkPhone), "_WORKPHONE", String.Empty)
    Public Shared ReadOnly CellPhoneProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, AltContact)(Function(c) (c.CellPhone), "_CELLPHONE", String.Empty)
    Public Shared ReadOnly FaxPhoneProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, AltContact)(Function(c) (c.FaxPhone), "_FAX", String.Empty)
    Public Shared ReadOnly EMailProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String, AltContact)(Function(c) (c.EMail), "_EMAIL", String.Empty)

#Region "  Properties "
    Public ReadOnly Property ApplicantType As String
        Get
            Return GetProperty(ApplicantTypeProperty)
        End Get
    End Property
    Public Property HomePhone As String
        Get
            Return GetProperty(HomePhoneProperty)
        End Get
        Set(value As String)
            SetProperty(HomePhoneProperty, value)
        End Set
    End Property
    Public Property WorkPhone As String
        Get
            Return GetProperty(WorkPhoneProperty)
        End Get
        Set(value As String)
            SetProperty(WorkPhoneProperty, value)
        End Set
    End Property
    Public Property CellPhone As String
        Get
            Return GetProperty(CellPhoneProperty)
        End Get
        Set(value As String)
            SetProperty(CellPhoneProperty, value)
        End Set
    End Property
    Public Property FaxPhone As String
        Get
            Return GetProperty(FaxPhoneProperty)
        End Get
        Set(value As String)
            SetProperty(FaxPhoneProperty, value)
        End Set
    End Property
    Public Property EMail As String
        Get
            Return GetProperty(EMailProperty)
        End Get
        Set(value As String)
            SetProperty(EMailProperty, value)
        End Set
    End Property
#End Region

#Region "  Data Access "
    Public Sub New(ByVal gProp As ProcessInfo, ByVal parent As String)
        MyBase.New(gProp)
        LoadProperty(ApplicantTypeProperty, parent)
    End Sub
    Public Shared Function Fetch(ByVal appArgs As ApplicantArgs) As AltContact
        Dim ac As New AltContact(appArgs.globalProperty, appArgs.applicantType)
        ac.ReadDictionary(appArgs)
        Return ac
    End Function
    Public Overrides Sub PopulateOverride(currentRun As Dictionary(Of String, Object))
        If currentRun Is Nothing Then Exit Sub
        PopulateField(currentRun, HomePhoneProperty)
        PopulateField(currentRun, WorkPhoneProperty)
        PopulateField(currentRun, CellPhoneProperty)
        PopulateField(currentRun, FaxPhoneProperty)
        PopulateField(currentRun, EMailProperty)
    End Sub
    Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
        StoreField(HomePhoneProperty, d)
        StoreField(WorkPhoneProperty, d)
        StoreField(CellPhoneProperty, d)
        StoreField(FaxPhoneProperty, d)
        StoreField(EMailProperty, d)
    End Sub
    Public Overrides Function OnGenerateKey(friendlyName As String) As String
        Return ApplicantArgs.KeyParent(ApplicantType, GlobalProperty) & friendlyName
    End Function
#End Region

#Region "  Business Rules "
    Protected Overrides Sub AddBusinessRules()
        Me.BusinessRules.AddRule(New HasUSRequiredPhone)
        Me.BusinessRules.AddRule(New HasCanadaRequiredPhone)
    End Sub
    Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)
        Dim attach As Boolean = False
        Dim v As Rule = Nothing
        For Each appRoot As Rule In validationRoot.Rules
            If appRoot.Name = ApplicantType Then
                v = appRoot
                Exit For
            End If
        Next
        If v Is Nothing Then
            v = New Rule(New ValidationFormat With {.Message = ApplicantType, .BottomLayer = False})
            attach = True
        End If
        AttachPropertyRequirements(v)
        If attach Then validationRoot.Rules.Add(v)
    End Sub
    Public Class HasUSRequiredPhone
        Inherits Csla.Rules.BusinessRule

        Public Sub New()
            Me.PrimaryProperty = HomePhoneProperty
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As AltContact = context.Target
            If Not ProcessUtility.IsCanadian(t.GlobalProperty) Then
                If String.IsNullOrWhiteSpace(t.HomePhone) OrElse t.HomePhone.Length <> 10 AndAlso _
                   String.IsNullOrWhiteSpace(t.WorkPhone) OrElse t.WorkPhone.Length <> 10 AndAlso _
                   String.IsNullOrWhiteSpace(t.CellPhone) OrElse t.CellPhone.Length <> 10 AndAlso _
                   String.IsNullOrWhiteSpace(t.FaxPhone) OrElse t.FaxPhone.Length <> 10 Then
                    context.AddErrorResult("Validation Error - US Contract requires one phone number")
                End If
            End If
        End Sub
    End Class
    Public Class HasCanadaRequiredPhone
        Inherits Csla.Rules.BusinessRule
        Public Sub New()
            Me.PrimaryProperty = HomePhoneProperty
        End Sub
        Protected Overrides Sub Execute(context As Rules.RuleContext)
            Dim t As AltContact = context.Target
            If ProcessUtility.IsCanadian(t.GlobalProperty) Then
                Dim count As Integer = 0
                If Not String.IsNullOrWhiteSpace(t.HomePhone) AndAlso t.HomePhone.Length <= 10 Then count = count + 1
                If Not String.IsNullOrWhiteSpace(t.WorkPhone) AndAlso t.WorkPhone.Length <= 10 Then count = count + 1
                If Not String.IsNullOrWhiteSpace(t.CellPhone) AndAlso t.CellPhone.Length <= 10 Then count = count + 1
                If Not String.IsNullOrWhiteSpace(t.FaxPhone) AndAlso t.FaxPhone.Length <= 10 Then count = count + 1
                If count < 2 Then context.AddErrorResult("Validation Error - Canada Contract requires a minimum of two phone numbers")
            End If
        End Sub
    End Class
#End Region
End Class
