Public Class VWCreditProcess
    Implements IReplicableFormField, IValidationTarget

    Private _gProp As ProcessInfo
    'Private _applicantMgr As ApplicantManager
    'Private _veh As Vehicle
    'Private _finance As Finance
    'Private _lease As Lease

    Public Shared Function Fetch(ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object)) As VWCreditProcess
        Dim vwCP As New VWCreditProcess()
        vwCP._gProp = ProcessInfo.Fetch(previousRun, currentRun)
        Return vwCP
    End Function

    Public Sub ReplicateCurrentState(d As Dictionary(Of String, Object)) Implements IReplicableFormField.ReplicateCurrentState

    End Sub

    Public Sub CheckRules() Implements IValidationTarget.CheckRules

    End Sub

    Public Sub Requirement(validationRoot As Rule) Implements IValidationTarget.Requirement

    End Sub
End Class
