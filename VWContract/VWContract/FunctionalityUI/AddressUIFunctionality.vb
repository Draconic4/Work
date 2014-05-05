Public Class AddressUIFunctionality

    Public Shared Function LoadSingleAddress(testTemplate As String) As VWContractValidation.AddressViewModel
        Select Case testTemplate
            Case "Nothing"
                Return New VWContractValidation.AddressViewModel(Nothing, Nothing)
            Case "EmptyDefault"
                Dim base As New VWContractValidation.ValidationLib.BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = New Dictionary(Of String, Object), .CurrentRun = New Dictionary(Of String, Object)}
                Dim gProp As VWContractValidation.ValidationLib.ProcessInfo = VWContractValidation.ValidationLib.ProcessInfo.Fetch(base)
                Dim aArgs As New VWContractValidation.ValidationLib.ApplicantArgs With {.GlobalProperty = base.GlobalProperty, .PreviousRun = base.PreviousRun, .CurrentRun = base.CurrentRun, .applicantType = 0}
                Dim addr As VWContractValidation.ValidationLib.Address = VWContractValidation.ValidationLib.Address.Fetch("Header Text", aArgs)
                Return New VWContractValidation.AddressViewModel(addr, gProp)
            Case "USDefault"
                Dim base As New VWContractValidation.ValidationLib.BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = New Dictionary(Of String, Object), .CurrentRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}}}
                Dim gProp As VWContractValidation.ValidationLib.ProcessInfo = VWContractValidation.ValidationLib.ProcessInfo.Fetch(base)
                Dim aArgs As New VWContractValidation.ValidationLib.ApplicantArgs With {.GlobalProperty = gProp, .PreviousRun = base.PreviousRun, .CurrentRun = base.CurrentRun, .applicantType = 0}
                Dim addr As VWContractValidation.ValidationLib.Address = VWContractValidation.ValidationLib.Address.Fetch("Home Address", aArgs)
                Return New VWContractValidation.AddressViewModel(addr, gProp)
            Case "CANDefault"
                Dim base As New VWContractValidation.ValidationLib.BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = New Dictionary(Of String, Object), .CurrentRun = New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}}
                Dim gProp As VWContractValidation.ValidationLib.ProcessInfo = VWContractValidation.ValidationLib.ProcessInfo.Fetch(base)
                Dim aArgs As New VWContractValidation.ValidationLib.ApplicantArgs With {.GlobalProperty = gProp, .PreviousRun = base.PreviousRun, .CurrentRun = base.CurrentRun, .applicantType = 0}
                Dim addr As VWContractValidation.ValidationLib.Address = VWContractValidation.ValidationLib.Address.Fetch("Billing Address", aArgs)
                Return New VWContractValidation.AddressViewModel(addr, gProp)
            Case Else
                Return New VWContractValidation.AddressViewModel(Nothing, Nothing)
        End Select
    End Function
End Class
