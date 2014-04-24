Module Module1
    'Previous Runs
    Dim usfprun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"VW_EXECUTIONSTATEORPROVINCE", "MN"}, {"VW_APPLICANTTYPE", "INDIVIDUAL"}, {"DEAL_FCALC", 0}, {"VW_FINANCETYPE", "RETAIL"}, {"VW_PRODUCTTYPE", "SIMPLE INTEREST METHOD"}}
    Dim usbprun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"VW_EXECUTIONSTATEORPROVINCE", "MN"}, {"VW_APPLICANTTYPE", "INDIVIDUAL"}, {"DEAL_FCALC", 0}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}, {"VW_FINANCETYPE", "BALOON"}, {"VW_PRODUCTTYPE", "SIMPLE INTEREST METHOD"}}
    Dim uslmprun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"VW_EXECUTIONSTATEORPROVINCE", "MN"}, {"VW_APPLICANTTYPE", "INDIVIDUAL"}, {"DEAL_FCALC", 1}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 60}, {"VW_FINANCETYPE", "LEASE"}, {"VW_PRODUCTTYPE", "MONTHLY"}}
    Dim uslsprun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"VW_EXECUTIONSTATEORPROVINCE", "MN"}, {"VW_APPLICANTTYPE", "INDIVIDUAL"}, {"DEAL_FCALC", 1}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 1}, {"VW_FINANCETYPE", "LEASE"}, {"VW_PRODUCTTYPE", "SINGLE"}}
    Dim cafprun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}, {"VW_EXECUTIONSTATEORPROVINCE", "AB"}, {"VW_APPLICANTTYPE", "INDIVIDUAL"}, {"DEAL_FCALC", 0}, {"VW_FINANCETYPE", "RETAIL"}, {"VW_PRODUCTTYPE", "SIMPLE INTEREST METHOD"}}
    Dim cabprun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}, {"VW_EXECUTIONSTATEORPROVINCE", "AB"}, {"VW_APPLICANTTYPE", "INDIVIDUAL"}, {"DEAL_FCALC", 0}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}, {"VW_FINANCETYPE", "BALOON"}, {"VW_PRODUCTTYPE", "SIMPLE INTEREST METHOD"}}
    Dim calmprun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}, {"VW_EXECUTIONSTATEORPROVINCE", "AB"}, {"VW_APPLICANTTYPE", "INDIVIDUAL"}, {"DEAL_FCALC", 1}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 60}, {"VW_FINANCETYPE", "RETAIL"}, {"VW_PRODUCTTYPE", "SIMPLE INTEREST METHOD"}}
    'Current Runs
    Dim usfcrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"DEAL_FCALC", 0}}
    Dim usbcrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"DEAL_FCALC", 0}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}}
    Dim uslmcrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"DEAL_FCALC", 1}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 60}}
    Dim uslscrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "US"}, {"DEAL_FCALC", 1}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 1}}
    Dim cafcrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}, {"DEAL_FCALC", 0}}
    Dim cabcrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}, {"DEAL_FCALC", 0}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 120}}
    Dim calmcrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}, {"DEAL_FCALC", 1}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 60}}
    Dim calscrun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}, {"DEAL_FCALC", 1}, {"DEAL_TERM", 60}, {"DEAL_AMORTTERM", 1}}


    Sub Main()
        TestProcessInfoDefaultCountry()
        TestProcessInfoCAFinanceDefault()
        Dim prun As New Dictionary(Of String, Object)
        Dim crun As New Dictionary(Of String, Object)
        prun.Add("BUY_SUFFIX", "Jr.")
        crun.Add("BUY_LAST", "Test")
        crun.Add("BUY_FIRST", "PBS")
        Dim gProp As VWContractValidationPrompt.ProcessInfo = VWContractValidationPrompt.ProcessInfo.Fetch(prun, crun)
        Dim appArgs As New VWContractValidationPrompt.ApplicantArgs With {.applicantType = "Primary", .customer = True, .globalProperty = gProp, .previousRun = prun, .currentRun = crun}
        Dim x As VWContractValidationPrompt.ApplicantID = VWContractValidationPrompt.ApplicantID.Fetch(appArgs)
        Console.WriteLine("I AM BUYER " & x.Family)
        Console.WriteLine("I AM FUNNY " & x.Given)
        Console.WriteLine("I AM SUFFIX " & x.Suffix)
        x.CheckRules()
        Dim r As New VWContractValidationPrompt.Rule("Rule Test", False)
        x.Requirement(r)
        Console.WriteLine("Press any key to continue....")
        Console.ReadKey()
    End Sub

    Public Sub TestProcessInfoDefaultCountry()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object)
        Dim gProp As VWContractValidationPrompt.ProcessInfo = VWContractValidationPrompt.ProcessInfo.Fetch(pRun, cRun)
        Console.WriteLine("US|{0}", gProp.Country)
    End Sub
    Public Sub TestProcessInfoUSCountry()
        Dim pRun As New Dictionary(Of String, Object)
    End Sub

    Public Sub TestProcessInfoCAFinanceDefault()
        Dim gProp As VWContractValidationPrompt.ProcessInfo = VWContractValidationPrompt.ProcessInfo.Fetch(cafprun, cafcrun)
        Console.WriteLine("CA|{0}", gProp.Country)
        Console.WriteLine("AB|{0}", gProp.StateOrProvinceExecution)
        Console.WriteLine("INDIVIDUAL|{0}", gProp.ApplicationType)
        Console.WriteLine("RETAIL|{0}", gProp.FinanceType)
        Console.WriteLine("SIMPLE INTEREST METHOD|{0}", gProp.ProductType)
        Console.WriteLine("Press any key to continue....")
        Console.ReadKey()
    End Sub

    Public Function WriteRules(ByVal depth As Integer, ByVal r As VWContractValidationPrompt.Rule) As String

    End Function


End Module
