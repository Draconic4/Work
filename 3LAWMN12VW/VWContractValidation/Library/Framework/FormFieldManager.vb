Public Class FormFieldManager
    Public Shared Sub Populate(obj As IReadFormField, ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object))
        If previousRun IsNot Nothing Then obj.Populate(previousRun)
        If currentRun IsNot Nothing Then
            obj.PopulateOverride(currentRun)
            obj.Calculate(previousRun, currentRun)
        End If
    End Sub
    Public Shared Sub ReplicateCurrentState(obj As IReplicableFormField, d As Dictionary(Of String, Object))
        obj.ReplicateCurrentState(d)
    End Sub
End Class
