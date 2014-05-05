Public Class ApplicantUIFunctionality
    Public Shared Function LoadApplicant(testTemplate As String) As VWContractValidation.ApplicantViewModel
        Select Case testTemplate
            Case "Nothing"
                Return New VWContractValidation.ApplicantViewModel(Nothing, Nothing)
            Case "EmptyDefault"
                Dim base As New VWContractValidation.ValidationLib.BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = New Dictionary(Of String, Object), .CurrentRun = New Dictionary(Of String, Object)}
                Dim gProp As VWContractValidation.ValidationLib.ProcessInfo = VWContractValidation.ValidationLib.ProcessInfo.Fetch(base)
                Dim aArgs As New VWContractValidation.ValidationLib.ApplicantArgs With {.GlobalProperty = base.GlobalProperty, .PreviousRun = base.PreviousRun, .CurrentRun = base.CurrentRun, .applicantType = VWContractValidation.ValidationLib.ApplicantArgs.ApplicantTypes.BUYER1}
                Dim app As VWContractValidation.ValidationLib.Applicant = VWContractValidation.ValidationLib.Applicant.Fetch(aArgs)
                Return New VWContractValidation.ApplicantViewModel(app, gProp)
            Case "USDefault"
                Dim base As New VWContractValidation.ValidationLib.BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = New Dictionary(Of String, Object), .CurrentRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}}}
                Dim gProp As VWContractValidation.ValidationLib.ProcessInfo = VWContractValidation.ValidationLib.ProcessInfo.Fetch(base)
                Dim aArgs As New VWContractValidation.ValidationLib.ApplicantArgs With {.GlobalProperty = gProp, .PreviousRun = base.PreviousRun, .CurrentRun = base.CurrentRun, .applicantType = VWContractValidation.ValidationLib.ApplicantArgs.ApplicantTypes.BUYER1}
                Dim app As VWContractValidation.ValidationLib.Applicant = VWContractValidation.ValidationLib.Applicant.Fetch(aArgs)
                Return New VWContractValidation.ApplicantViewModel(app, gProp)
            Case "CANDefault"
                Dim base As New VWContractValidation.ValidationLib.BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = New Dictionary(Of String, Object), .CurrentRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}}
                Dim gProp As VWContractValidation.ValidationLib.ProcessInfo = VWContractValidation.ValidationLib.ProcessInfo.Fetch(base)
                Dim aArgs As New VWContractValidation.ValidationLib.ApplicantArgs With {.GlobalProperty = gProp, .PreviousRun = base.PreviousRun, .CurrentRun = base.CurrentRun, .applicantType = VWContractValidation.ValidationLib.ApplicantArgs.ApplicantTypes.BUYER2}
                Dim app As VWContractValidation.ValidationLib.Applicant = VWContractValidation.ValidationLib.Applicant.Fetch(aArgs)
                Return New VWContractValidation.ApplicantViewModel(app, gProp)
            Case Else
                Return New VWContractValidation.ApplicantViewModel(Nothing, Nothing)
        End Select
    End Function
End Class
