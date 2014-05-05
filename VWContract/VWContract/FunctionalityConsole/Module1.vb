Imports VWContractValidation.ValidationLib
Module Module1

    Sub Main()
        Dim bArgs As New BaseConstructionArgs With {.CurrentRun = Nothing, .PreviousRun = Nothing}
        Dim pi As ProcessInfo = ProcessInfo.Fetch(bArgs)

    End Sub

End Module
