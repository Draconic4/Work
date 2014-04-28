Public Class VWCreditProcess
    Implements IReplicableFormField, IValidationTarget

    Private _isValidated As Boolean = False
    Private _validationNumber As String = String.Empty
    Private _gProp As ProcessInfo


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

    Property Vehicle As Vehicle

    Property InsuranceProducts As Insurance

    Property Protection As Object
    Property PaymentSchedule As PaymentSchedule
    'Private _applicantMgr As ApplicantManager
    'Private _veh As Vehicle
    'Private _finance As Finance
    'Private _lease As Lease

    Public Shared Function Fetch(ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As VWCreditProcess
        Dim vwCP As New VWCreditProcess()
        vwCP._gProp = ProcessInfo.Fetch(previousRun, currentRun)
        Dim vArgs As New VehicleArgs With {.GlobalProperty = vwCP._gProp, .PreviousRun = previousRun, .CurrentRun = currentRun}
        vwCP.Vehicle = Vehicle.Fetch(vArgs)
        Dim sArgs As New ScheduleArguments With {.GlobalProperty = vwCP._gProp, .PreviousRun = previousRun, .CurrentRun = currentRun}
        vwCP.PaymentSchedule = PaymentSchedule.Fetch(sArgs)
        Return vwCP
    End Function

    Public Sub ReplicateCurrentState(d As Dictionary(Of String, Object)) Implements IReplicableFormField.ReplicateCurrentState

    End Sub

    Public Sub CheckRules() Implements IValidationTarget.CheckRules

    End Sub

    Public Sub Requirement(validationRoot As Rule) Implements IValidationTarget.Requirement

    End Sub
End Class
