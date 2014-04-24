Public Interface IReadFormField
    Sub Populate(ByVal previousRun As Dictionary(Of String, Object))
    Sub PopulateOverride(ByVal currentRun As Dictionary(Of String, Object))
    Sub Calculate(ByVal previousRun As Dictionary(Of String, Object), ByVal currentRun As Dictionary(Of String, Object))
End Interface
