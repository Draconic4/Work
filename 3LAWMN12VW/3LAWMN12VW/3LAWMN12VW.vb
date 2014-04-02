Imports PBS.Deals.FormsIntegration
Imports Include7.I7

Public Class _3LAWMN12VW
    Inherits FormBase
#Region "Constants - Form Variables "
    Private Const FORMID As String = "LAW 553 MN - 2012 (Development)"
    Private Const NA As String = "N/A"
#End Region

#Region "Form Variables "
    'Prompt Response
    Private _CLSingleSelectionResponse As String = String.Empty
    Private _AHSingleSelectionResponse As String = String.Empty
    Private _FINCHGRESPONSE As Boolean = True
    Private _FINCHGRESPONSE_DATE As Include7.Forms.Critical.DateInc7
    Private _LATECHARGE As Decimal = 0D
    Private _LATEDAYS As Integer = 0
    Private _LATERATE As Decimal = 0D
    Private _GAPPROVIDER As String = NA
    Private _GAPTERM As Integer = 0
    Private _ASSIGNCHOICERESPONSE As String = "Assigned W/Recourse"
    Private _AssigneeResponse As String = NA
    Private _AssigneeTitleResponse As String = NA
    Private _AssigneeTitleReponse As String = NA

    'FED BOX
    Private APR As Decimal = 0D
    Private FINCHG As Decimal = 0D
    Private BALFIN As Decimal = 0D
    Private TOTPMT As Decimal = 0D
    Private DOWNPMT As Decimal = 0D
    Private TOTALSALEPRICE As Decimal = 0D

    'Itemized Variables
    Private L1A As Decimal = 0D
    Private L1 As Decimal = 0D
    Private L2A As Decimal = 0D
    Private L2B As Decimal = 0D
    Private L2C As Decimal = 0D
    Private L2D As Decimal = 0D
    Private L2EDESC As String = NA
    Private L2E As Decimal = 0D
    Private L2 As Decimal = 0D
    Private L3 As Decimal = 0D
    Private L4Ai As Decimal = 0D
    Private L4Aii As Decimal = 0D
    Private L4A As Decimal = 0D
    Private L4B As Decimal = 0D
    Private L4C As Decimal = 0D
    Private L4D As Decimal = 0D
    Private L4E As Decimal = 0D
    Private L4FDESC As String = NA
    Private L4F As Decimal = 0D
    Private L4G As Decimal = 0D
    Private L4HTo1 As String = NA
    Private L4HFor1 As String = NA
    Private L4H1 As Decimal = 0D
    Private L4HTo2 As String = NA
    Private L4HFor2 As String = NA
    Private L4H2 As Decimal = 0D
    Private L4HTo3 As String = NA
    Private L4HFor3 As String = NA
    Private L4H3 As Decimal = 0D
    Private L4HTo4 As String = NA
    Private L4HFor4 As String = NA
    Private L4H4 As Decimal = 0D
    Private L4HTo5 As String = NA
    Private L4HFor5 As String = NA
    Private L4H5 As Decimal = 0D
    Private L4HTo6 As String = NA
    Private L4HFor6 As String = NA
    Private L4H6 As Decimal = 0D
    Private L4HTo7 As String = NA
    Private L4HFor7 As String = NA
    Private L4H7 As Decimal = 0D
    Private L4HTo8 As String = NA
    Private L4HFor8 As String = NA
    Private L4H8 As Decimal = 0D
    Private L4HTo9 As String = NA
    Private L4HFor9 As String = NA
    Private L4H9 As Decimal = 0D
    Private L4HTo10 As String = NA
    Private L4HFor10 As String = NA
    Private L4H10 As Decimal = 0D
    Private L4 As Decimal = 0D
    Private L5 As Decimal = 0D

    'INSCO INFO
    Private INSCO1L1 As String = NA
    Private INSCO1L2 As String = NA
    Private INSCO1_ADDRL1 As String = NA
    Private INSCO1_ADDRL2 As String = NA
    Private OtherIns1Premium As Decimal = 0D
    Private OtherIns1Type As String = NA
    Private OtherIns1Term As Integer = 0
    Private OtherInsCo1L1 As String = NA
    Private OtherInsCo1L2 As String = NA
    Private OtherIns1_AddrL1 As String = NA
    Private OtherIns1_AddrL2 As String = NA
    Private OtherIns2Premium As Decimal = 0D
    Private OtherIns2Type As String = NA
    Private OtherIns2Term As Integer = 0
    Private OtherInsCo2L1 As String = NA
    Private OtherInsCo2L2 As String = NA
    Private OtherIns2_AddrL1 As String = NA
    Private OtherIns2_AddrL2 As String = NA
#End Region

    Public Sub New()
        Me.Id = FORMID
        Me.Info = New _3LAWMN12VWInfo
        _process = New FormEngineAR7()
    End Sub

    Protected Overrides Sub OnAddPrompts()
        Include7.I7.Initialize(Me.Data)
    End Sub
    Protected Overrides Sub OnValidatePrompts()

    End Sub

    Public Overrides Sub Calculate()
        L1A = D.TotalTax(0) + D.TotalTax(1) + D.TotalTax(2) + D.TotalTax(3) + D.TotalTax(4)
        L1 = D.Price + L1A
        L2A = Trade.TotalAllowance
        L2B = Lien.Total
        L2C = L2A - L2B
        L2D = COD.Value
        L2E = Rebate.Total.Value
        Dim TotalDownPayment As Decimal = L2C + L2D + L2E
        If TotalDownPayment < 0 Then
            L2 = 0
            L4HTo1 = T(0).Lien.Name
            L4H1 = -TotalDownPayment
        Else
            L2 = TotalDownPayment
            L4H1 = 0
        End If
        L3 = L1 - L2
        L4Ai = Insurance.CreditLife.Premium
        L4Aii = Insurance.AccidentHealth.Premium
        L4A = L4Ai + L4Aii
        L4B = OtherIns1Premium + OtherIns2Premium
        L4C = Fee.Lic.Total
        L4D = Protection("J").Price
        L4E = Fee.Admin.Total
        L4F = Fee.Reg.Total
        L4G = Fee.Tire.Total
        L4H2 = Protection("A").Price
        If L4H2 <> 0D Then
            L4HTo2 = Protection("A").Name
            'L4HFor2 = Protection("A").Company
        End If
        L4H3 = 0D
        If L4H3 <> 0D Then
            L4HTo3 = String.Empty
            L4HFor3 = String.Empty
        End If
        L4H4 = 0D
        If L4H4 <> 0D Then
            L4HTo4 = String.Empty
            L4HFor4 = String.Empty
        End If
        L4H5 = 0D
        If L4H5 <> 0D Then
            L4HTo5 = String.Empty
            L4HFor5 = String.Empty
        End If
        L4H6 = 0D
        If L4H6 <> 0D Then
            L4HTo6 = String.Empty
            L4HFor6 = String.Empty
        End If
        L4H7 = 0D
        If L4H7 <> 0D Then
            L4HTo7 = String.Empty
            L4HFor7 = String.Empty
        End If
        L4H8 = 0D
        If L4H8 <> 0D Then
            L4HTo8 = String.Empty
            L4HFor8 = String.Empty
        End If
        L4H9 = 0D
        If L4H9 <> 0D Then
            L4HTo9 = String.Empty
            L4HFor9 = String.Empty
        End If
        L4H10 = 0D
        If L4H10 <> 0D Then
            L4HTo10 = String.Empty
            L4HFor10 = String.Empty
        End If
        L4 = L4A + L4B + L4C + L4D + L4E + L4F + L4G
        L4 = L4 + L4H1 + L4H2 + L4H3 + L4H4 + L4H5 + L4H6 + L4H7 + L4H8 + L4H9 + L4H10
        L5 = L3 + L4

    End Sub

    Public Overrides Sub Validate()

    End Sub

    Protected Overrides Function OnExecuteAR7() As GrapeCity.ActiveReports.Document.SectionDocument
        _process.BeginFormExecution(Me)
        Using ar As New _3LAWMN12VW_Layout
            Dim form As New DisplayLibraryAR7(ar, Format.FormatData.GenerateConstantFontTypeAndSize(ar.Buyer.Font), Info.IsFrontLoad, Info.FrontLoadAdjustment)

            form.PrintText(ar.DealerNo, "DealerNum")
            form.PrintText(ar.ContractNo, "ContractNum")
            'Buyer Info
            form.PrintText(ar.Buyer, Buy.Name.LastFirstInit)
            form.PrintText(ar.Buy_USAddr, Buy.Contact.USShortAddress)
            form.PrintText(ar.Buy_C_P_PC, Buy.Contact.CityProvincePostalCode + " " + Buy.Contact.County)
            'Cobuyer Info
            form.PrintText(ar.CoBuyer, Cob(0).Name.LastFirstInit)
            form.PrintText(ar.Cob_USAddr, Cob(0).Contact.USShortAddress)
            form.PrintText(ar.Cob_C_P_PC, Cob(0).Contact.CityProvincePostalCode + " " + Cob(0).Contact.County)
            'Dealer Info
            form.PrintText(ar.Dlr_Name, Dlr.FullName)
            form.PrintText(ar.Dlr_USAddr, Dlr.Contact.Address)
            form.PrintText(ar.Dlr_C_P_PC, Dlr.Contact.CityProvincePostalCode)

            'Vehicle Info
            form.PrintText(ar.V_Type, V(0).Detail.VehicleType)
            form.PrintText(ar.V_Make, V(0).Make)
            form.PrintText(ar.V_Model, V(0).Model)
            form.PrintText(ar.V_Vin, V(0).VIN)
            form.PrintFlag(ar.Business_Tick, True)
            form.PrintFlag(ar.Agriculture_Tick, True)
            form.PrintFlag(ar.Other_Tick, True)
            form.PrintText(ar.OtherUse, "A REALLY GOOD USE")

            'FED BOX
            form.PrintNumber(ar.FED_L1, APR, 2, "-0-")
            form.PrintNumber(ar.FED_L2, FINCHG, 2)
            form.PrintNumber(ar.FED_L3, BALFIN, 2)
            form.PrintNumber(ar.FED_L4, TOTPMT, 2)
            form.PrintNumber(ar.FED_L5A, DOWNPMT, 2, "-0-")
            form.PrintNumber(ar.FED_L5B, TOTALSALEPRICE, 2)

            form.PrintNumber(ar.FED_L6A, D.Term, 2)
            form.PrintNumber(ar.FED_L6B, D.Payment, 2)
            form.PrintText(ar.FED_L6C, D.PaymentDate.MM_DD_Year)

            form.PrintText(ar.FED_L7, "")
            form.PrintText(ar.FED_L8, "")
            form.PrintText(ar.FED_L9, "")

            'ITEMIZATION OF AMOUNTS
            form.PrintNumber(ar.L1A, L1A, 2, "-0-")
            form.PrintNumber(ar.L1, L1, 2)
            If Not String.IsNullOrWhiteSpace(T(0).Year) Then
                form.PrintText(ar.T_Year, T(0).Year)
            Else
                form.PrintText(ar.T_Year, NA)
            End If
            If Not String.IsNullOrWhiteSpace(T(0).Make) Then
                form.PrintText(ar.T_Make, T(0).Make)
            Else
                form.PrintText(ar.T_Make, NA)
            End If
            If Not String.IsNullOrWhiteSpace(T(0).Model) Then
                form.PrintText(ar.T_Model, T(0).Model)
            Else
                form.PrintText(ar.T_Model, NA)
            End If

            form.PrintNumber(ar.L2A, L2A, 2, NA)
            form.PrintNumber(ar.L2B, L2B, 2, NA)
            form.PrintNumber(ar.L2C, L2C, 2, NA)
            form.PrintNumber(ar.L2D, L2D, 2, NA)
            form.PrintText(ar.L2EDESC, L2EDESC)
            form.PrintNumber(ar.L2E, L2E, 2, NA)

            form.PrintNumber(ar.L3, L3, 2, NA)
            form.PrintNumber(ar.L4Ai, L4Ai, 2, NA)
            form.PrintNumber(ar.L4Aii, L4Aii, 2, NA)
            form.PrintNumber(ar.L4A, L4A, 2, NA)
            form.PrintNumber(ar.L4B, L4B, 2, NA)
            form.PrintNumber(ar.L4C, L4C, 2, NA)
            form.PrintNumber(ar.L4D, L4D, 2, NA)
            form.PrintNumber(ar.L4E, L4E, 2, NA)
            form.PrintText(ar.L4FDESC, L4FDESC)
            form.PrintNumber(ar.L4F, L4F, 2, NA)
            form.PrintNumber(ar.L4G, L4G, 2, NA)

            form.PrintText(ar.L4HTo1, L4HTo1)
            form.PrintNumber(ar.L4H1, L4H1, 2, NA)
            form.PrintText(ar.L4HTo2, L4HTo2)
            form.PrintText(ar.L4HFor2, L4HFor2)
            form.PrintNumber(ar.L4H2, L4H2, 2, NA)
            form.PrintText(ar.L4HTo3, L4HTo3)
            form.PrintText(ar.L4HFor3, L4HFor3)
            form.PrintNumber(ar.L4H3, L4H3, 2, NA)
            form.PrintText(ar.L4HTo4, L4HTo4)
            form.PrintText(ar.L4HFor4, L4HFor4)
            form.PrintNumber(ar.L4H4, L4H4, 2, NA)
            form.PrintText(ar.L4HTo5, L4HTo5)
            form.PrintText(ar.L4HFor5, L4HFor5)
            form.PrintNumber(ar.L4H5, L4H5, 2, NA)
            form.PrintText(ar.L4HTo6, L4HTo6)
            form.PrintText(ar.L4HFor6, L4HFor6)
            form.PrintNumber(ar.L4H6, L4H6, 2, NA)
            form.PrintText(ar.L4HTo7, L4HTo7)
            form.PrintText(ar.L4HFor7, L4HFor7)
            form.PrintNumber(ar.L4H7, L4H7, 2, NA)
            form.PrintText(ar.L4HTo8, L4HTo8)
            form.PrintText(ar.L4HFor8, L4HFor8)
            form.PrintNumber(ar.L4H8, L4H8, 2, NA)
            form.PrintText(ar.L4HTo9, L4HTo9)
            form.PrintText(ar.L4HFor9, L4HFor9)
            form.PrintNumber(ar.L4H9, L4H9, 2, NA)
            form.PrintText(ar.L4HTo10, L4HTo10)
            form.PrintText(ar.L4HFor10, L4HFor10)
            form.PrintNumber(ar.L4H10, L4H10, 2, NA)

            form.PrintNumber(ar.L4, L4, 2, NA)
            form.PrintNumber(ar.L5, L5, 2, NA)

            'Insurance
            form.PrintFlag(ar.CreditLife_Tick, (Insurance.CreditLife.Premium <> 0))
            form.PrintFlag(ar.CL_Buy_Tick, Insurance.CreditLife.Status.ToUpper = "BUYER" Or (Insurance.CreditLife.Status.ToUpper = "SINGLE" AndAlso _CLSingleSelectionResponse = "BUYER"))
            form.PrintFlag(ar.CL_Cob_Tick, Insurance.CreditLife.Status.ToUpper = "COBUYER" Or (Insurance.CreditLife.Status.ToUpper = "SINGLE" AndAlso _CLSingleSelectionResponse = "COBUYER"))
            form.PrintFlag(ar.CL_Joint_Tick, Insurance.CreditLife.Status.ToUpper = "JOINT")
            form.PrintFlag(ar.AH_Tick, (Insurance.AccidentHealth.Premium <> 0))
            form.PrintFlag(ar.AH_Buy_Tick, Insurance.AccidentHealth.Status.ToUpper = "BUYER" Or (Insurance.AccidentHealth.Status.ToUpper = "SINGLE" AndAlso _AHSingleSelectionResponse = "BUYER"))
            form.PrintFlag(ar.AH_Cob_Tick, Insurance.AccidentHealth.Status.ToUpper = "COBUYER" Or (Insurance.AccidentHealth.Status.ToUpper = "SINGLE" AndAlso _AHSingleSelectionResponse = "COBUYER"))
            form.PrintFlag(ar.AH_Joint_Tick, Insurance.AccidentHealth.Status.ToUpper = "JOINT")
            form.PrintNumber(ar.CL_Premium, Insurance.CreditLife.Premium, 2, "N/A")
            form.PrintNumber(ar.AH_Premium, Insurance.AccidentHealth.Premium, 2, "N/A")
            'Set based on INSCODE
            form.PrintText(ar.InsCo1L1, INSCO1L1)
            form.PrintText(ar.InsCo1L2, INSCO1L2)
            form.PrintText(ar.InsCo1_AddrL1, INSCO1_ADDRL1)
            form.PrintText(ar.InsCo1_AddrL2, INSCO1_ADDRL2)
            'Other Insurance
            form.PrintFlag(ar.OtherIns1_Tick, (OtherIns1Premium <> 0))
            form.PrintText(ar.OtherIns1Type, OtherIns1Type)
            form.PrintNumber(ar.OtherIns1Term, OtherIns1Term, NA)
            form.PrintNumber(ar.OtherIns1Premium, OtherIns1Premium, 2, NA)
            form.PrintText(ar.OtherInsCo1L1, OtherInsCo1L1)
            form.PrintText(ar.OtherInsCo1L2, OtherInsCo1L2)
            form.PrintText(ar.OtherInsCo1_AddrL1, OtherIns1_AddrL1)
            form.PrintText(ar.OtherInsCo1_AddrL2, OtherIns1_AddrL1)
            form.PrintFlag(ar.OtherIns2_Tick, (OtherIns2Premium <> 0))
            form.PrintText(ar.OtherIns2Type, OtherIns2Type)
            form.PrintNumber(ar.OtherIns2Term, OtherIns2Term, NA)
            form.PrintNumber(ar.OtherIns2Premium, OtherIns2Premium, 2, NA)
            form.PrintText(ar.OtherInsCo2L1, OtherInsCo2L1)
            form.PrintText(ar.OtherInsCo2L2, OtherInsCo2L2)
            form.PrintText(ar.OtherInsCo2_AddrL1, OtherIns2_AddrL1)
            form.PrintText(ar.OtherInsCo2_AddrL2, OtherIns2_AddrL1)
            'Optional Finance Charge
            form.PrintFlag(ar.NoFinanceDate_TICK, _FINCHGRESPONSE)
            If Not _FINCHGRESPONSE_DATE.IsInvalid Then form.PrintText(ar.NoFinanceDate_MMDD, _FINCHGRESPONSE_DATE.MM + " " + _FINCHGRESPONSE_DATE.DD)
            If Not _FINCHGRESPONSE_DATE.IsInvalid Then form.PrintText(ar.NoFinanceDate_YY, _FINCHGRESPONSE_DATE.YY)
            'Late Charge
            form.PrintFlag(ar.BusLateCharge_Tick, Buy.Name.IsBusiness)
            form.PrintNumber(ar.BusLateCharge, _LATECHARGE, 2, NA)
            form.PrintNumber(ar.BusLateDays, _LATEDAYS, "0")
            form.PrintNumber(ar.BusLateRate, _LATERATE, 2, NA)
            'Optional Gap Contract
            form.PrintText(ar.GapProvider, _GAPPROVIDER)
            form.PrintNumber(ar.GapTerm, _GAPTERM, NA)
            'Signatures
            form.PrintText(ar.Buy_Sign_Date, D.Contract.MM_DD_Year)
            If Not String.IsNullOrWhiteSpace(Cob(0).Name.LastFirstInit) Then form.PrintText(ar.Cob_Sign_Date, D.Contract.MM_DD_Year)
            form.PrintText(ar.Dlr_Name_Sign, Dlr.Name)
            form.PrintText(ar.Dlr_Sign_Date, D.Contract.MM_DD_Year)
            If Not String.IsNullOrWhiteSpace(Cob(1).Name.LastFirstInit) Then
                form.PrintText(ar.Cob2_USAddr, Cob(1).Contact.USShortAddress)
            Else
                form.PrintText(ar.Cob2_USAddr, NA)
            End If
            form.PrintText(ar.Dlr_Title, SalesStaff.SalesManager(0).Title)
            'Assignment
            form.PrintText(ar.LenderName, Bank.Name)
            form.PrintFlag(ar.AssignedRecourse_Tick, _ASSIGNCHOICERESPONSE = "Assigned W/Recourse")
            form.PrintFlag(ar.AssignedLimitedRecourse_Tick, _ASSIGNCHOICERESPONSE = "Assigned Limited Recourse")
            form.PrintFlag(ar.AssignedNoRecourse_Tick, _ASSIGNCHOICERESPONSE = "Assigned No Recourse")
            form.PrintText(ar.DlrName_Assigned, Dlr.Name)
            form.PrintText(ar.AssignedName, _AssigneeResponse)
            form.PrintText(ar.AssignedTitle, _AssigneeTitleReponse)

            _process.RegisterLayout(ar)
        End Using
        Return MyBase.OnExecuteAR7()
    End Function

#Region " Required Variables - Form Variables "
    Private _process As FormEngineAR7
#End Region
End Class
