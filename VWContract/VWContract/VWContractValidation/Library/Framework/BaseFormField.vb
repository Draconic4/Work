Imports Csla

Public MustInherit Class BaseFormField
    Inherits BusinessBase(Of BaseFormField)
    Implements IReadFormField, IReplicableFormField, IValidationTarget

    Private _globalProperties As ProcessInfo

    Public Overridable Function OnGenerateKey(ByVal friendlyName As String) As String
        Return friendlyName
    End Function
    Public Overridable Sub Populate(previousRun As Dictionary(Of String, Object)) Implements IReadFormField.Populate
        'Required for Construction that requires previous run information.
    End Sub
    Public Overridable Sub PopulateOverride(currentRun As Dictionary(Of String, Object)) Implements IReadFormField.PopulateOverride
        'Required for Construction of current Aristo Data.
    End Sub
    Public Overridable Sub Calculate(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object)) Implements IReadFormField.Calculate
        If currentRun Is Nothing Then Exit Sub
    End Sub

    Public Overridable Sub CheckRules() Implements IValidationTarget.CheckRules
        Me.BusinessRules.CheckRules()
    End Sub
    'Must Override
    Public MustOverride Sub ReplicateCurrentState(d As Dictionary(Of String, Object)) Implements IReplicableFormField.ReplicateCurrentState
    Public MustOverride Sub Requirement(validationRoot As Rule) Implements IValidationTarget.Requirement

    Public Sub ReadDictionary(args As BaseConstructionArgs)
        FormFieldManager.Populate(Me, args.PreviousRun, args.CurrentRun)
    End Sub
    Public Sub StoreCurrentState(d As Dictionary(Of String, Object))
        ReplicateCurrentState(d)
    End Sub

#Region "  Properties "
    Public ReadOnly Property GlobalProperty As ProcessInfo
        Get
            Return _globalProperties
        End Get
    End Property
#End Region

#Region "  Data Access "
    Public Sub New(ByVal gProp As ProcessInfo)
        _globalProperties = gProp
    End Sub
#End Region

#Region "  CSLA Template Functionality "
    Public Function GenerateKey(Of T)(pi As PropertyInfo(Of T)) As String
        Return OnGenerateKey(pi.FriendlyName)
    End Function
    Public Sub PopulateField(Of T)(ByVal d As Dictionary(Of String, Object), ByVal pi As PropertyInfo(Of T))
        Dim key As String = GenerateKey(pi)
        Dim xVal As T = GenerateDefaultValue(GetType(T))
        If d.TryGetValue(key, xVal) Then LoadProperty(pi, xVal)
    End Sub
    Private Function GenerateDefaultValue(type As Type) As Object
        If type Is GetType(Decimal) Then Return 0D
        If type Is GetType(Integer) Then Return 0
        If type Is GetType(String) Then Return String.Empty
        If type Is GetType(Boolean) Then Return False
        Return Nothing
    End Function
    Public Sub StoreField(Of T)(ByVal pi As PropertyInfo(Of T), d As Dictionary(Of String, Object))
        Dim key As String = GenerateKey(pi)
        If Not d.ContainsKey(key) Then d.Add(key, GetPropertyValue(pi))
    End Sub
    Public Function GetPropertyValue(Of T)(ByVal pi As PropertyInfo(Of T)) As T
        Return Me.ReadProperty(pi)
    End Function
#End Region

    Public Sub AttachPropertyRequirements(validationRoot As Rule)
        For Each vr As Csla.Rules.BrokenRule In Me.BrokenRulesCollection
            validationRoot.Rules.Add(New Rule(vr.Description, True))
        Next
    End Sub
End Class
