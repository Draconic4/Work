Namespace ValidationLib
    Public Class VWCreditProcess
        Implements IReplicableFormField, IValidationTarget

        Private _isValidated As Boolean = False
        Private _validationNumber As String = String.Empty
        Private _gProp As ProcessInfo
        Private _applicantMgr As ApplicantManager
        Private _vehicle As Vehicle
        Private _paymentSchedule As PaymentSchedule

        Private _rule As RuleSet

#Region "  Properties "
        Public Property IsValidated As Boolean
            Get
                Return _isValidated
            End Get
            Set(value As Boolean)
                If _isValidated AndAlso _isValidated <> value Then
                    _isValidated = value
                    _validationNumber = String.Empty
                End If
            End Set
        End Property
        Public Property ValidationNumber As String
            Get
                Return _validationNumber
            End Get
            Set(value As String)
                If _isValidated Then
                    _validationNumber = value
                End If
            End Set
        End Property
        Public Property GlobalProperty As ProcessInfo
            Get
                Return _gProp
            End Get
            Set(value As ProcessInfo)
                _gProp = value
            End Set
        End Property
        Public Property ApplicantManager As ApplicantManager
            Get
                Return _applicantMgr
            End Get
            Set(value As ApplicantManager)
                _applicantMgr = value
            End Set
        End Property
        Public Property Vehicle As Vehicle
            Get
                Return _vehicle
            End Get
            Set(value As Vehicle)
                _vehicle = value
            End Set
        End Property
        Public Property PaymentSchedule As PaymentSchedule
            Get
                Return _paymentSchedule
            End Get
            Set(value As PaymentSchedule)
                _paymentSchedule = value
            End Set
        End Property
        Public ReadOnly Property RuleSet As RuleSet
            Get
                Return _rule
            End Get
        End Property
#End Region
#Region "  Data Accesss "
        Public Shared Function Fetch(ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As VWCreditProcess
            Dim vwCP As New VWCreditProcess()
            Dim bArgs As New BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = previousRun, .CurrentRun = currentRun}
            vwCP._gProp = ProcessInfo.Fetch(bArgs)
            Dim vArgs As New VehicleArgs With {.VehicleType = "Vehicle", .VehicleIndex = 0, .GlobalProperty = vwCP.GlobalProperty, .PreviousRun = previousRun, .CurrentRun = currentRun}
            vwCP.Vehicle = Vehicle.Fetch(vArgs)
            Dim aArgs As New ApplicantArgs With {.GlobalProperty = vwCP.GlobalProperty, .PreviousRun = previousRun, .CurrentRun = currentRun}
            vwCP.ApplicantManager = ApplicantManager.Fetch(aArgs)
            Dim sArgs As New ScheduleArguments With {.GlobalProperty = vwCP._gProp, .PreviousRun = previousRun, .CurrentRun = currentRun}
            vwCP.PaymentSchedule = PaymentSchedule.Fetch(sArgs)
            Return vwCP
        End Function
        Public Sub ReplicateCurrentState(d As Dictionary(Of String, Object)) Implements IReplicableFormField.ReplicateCurrentState
            d.Add("VW_ISVALIDATED", IsValidated)
            d.Add("VW_VALIDATIONNUMBER", ValidationNumber)
            GlobalProperty.ReplicateCurrentState(d)
            ApplicantManager.ReplicateCurrentState(d)
            Vehicle.ReplicateCurrentState(d)
            PaymentSchedule.ReplicateCurrentState(d)
        End Sub
#End Region
#Region "  Business Rules "
        Public Sub CheckRules() Implements IValidationTarget.CheckRules
            GlobalProperty.CheckRules()
            ApplicantManager.CheckRules()
            Vehicle.CheckRules()
            PaymentSchedule.CheckRules()
        End Sub
        Public Sub Requirement(previousData As Object, validationRoot As Rule) Implements IValidationTarget.Requirement
            If validationRoot IsNot Nothing Then
                Dim v As New Rule(New ValidationFormat With {.Message = "Root", .BottomLayer = False, .DisplayCategory = ValidationFormat.RuleDisplay.NoCategory, .DisplayOrder = 0})
                GlobalProperty.Requirement(Nothing, v)
                ApplicantManager.Requirement(Nothing, v)
                Vehicle.Requirement(Nothing, v)
                PaymentSchedule.Requirement(Nothing, v)
                validationRoot.Rules.Add(v)
                _rule = New RuleSet(v)
            End If

        End Sub
        Sub GenerateRequirement()
            Dim root As New Rule(New ValidationFormat With {.Message = "", .BottomLayer = False, .DisplayCategory = ValidationFormat.RuleDisplay.NoCategory, .DisplayOrder = 0})
            Me.Requirement(Nothing, root)
            Dim lst As New List(Of String)
            ValidationFormat.GenerateAristoMessage(String.Empty, root, lst)
            SendAristoErrorsToNotepadInterOp.SendErrors(String.Empty, lst)
        End Sub
#End Region
    End Class
End Namespace
