Public Class PaymentSchedule
    Inherits BaseFormField

    Public Const FIRSTLINE As String = "FIRST"
    Public Const SECONDLINE As String = "SECOND"
    Public Const THIRDLINE As String = "THIRD"
    Public Const FOURTHLINE As String = "FOURTH"
    Public Const FINALLINE As String = "FINAL"

    Public PaymentDictionary As Dictionary(Of String, PaymentScheduleLine)

    Private Sub New(ByVal GlobalProperty As ProcessInfo)
        MyBase.New(GlobalProperty)
        GenerateScheduleLines()
    End Sub
    Public Sub GenerateScheduleLines()
        PaymentDictionary = New Dictionary(Of String, PaymentScheduleLine)
        PaymentDictionary.Add(FIRSTLINE, New PaymentScheduleLine)
        PaymentDictionary.Add(SECONDLINE, New PaymentScheduleLine)
        PaymentDictionary.Add(THIRDLINE, New PaymentScheduleLine)
        PaymentDictionary.Add(FOURTHLINE, New PaymentScheduleLine)
        PaymentDictionary.Add(FINALLINE, New PaymentScheduleLine)
    End Sub
    Public Shared Function Fetch(ByVal scheduleArgs As ScheduleArguments) As PaymentSchedule
        Dim sp As New PaymentSchedule(scheduleArgs.GlobalProperty)
        sp.ReadDictionary(scheduleArgs)
        Return sp
    End Function
    Public Overrides Sub Calculate(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object))
        If String.Compare(GlobalProperty.FinanceType, ProcessUtility.FINANCE_TYPE_RETAIL, StringComparison.InvariantCultureIgnoreCase) = 0 Then CalculateFinancePaymentSchedule(previousRun, currentRun)
        If String.Compare(GlobalProperty.FinanceType, ProcessUtility.FINANCE_TYPE_LEASE, StringComparison.InvariantCultureIgnoreCase) = 0 Then CalculateLeasePaymentSchedule(previousRun, currentRun)
        If String.Compare(GlobalProperty.FinanceType, ProcessUtility.FINANCE_TYPE_BALLOON, StringComparison.InvariantCultureIgnoreCase) = 0 Then CalculateBalloonPaymentSchedule(previousRun, currentRun)
    End Sub
    Private Sub CalculateFinancePaymentSchedule(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object))
        LoadFinanceFirstLine(currentRun)
        APRAdjustment(currentRun, Nothing)
    End Sub
    Private Function LoadDictionaryStr(d As Dictionary(Of String, Object), key As String) As String
        Dim strVal As String = String.Empty
        If Not d.TryGetValue(key, strVal) Then
            Return String.Empty
        End If
        Return strVal
    End Function
    Private Function LoadDictionaryDecimal(d As Dictionary(Of String, Object), key As String) As Decimal
        Dim decimalVal As Decimal = 0D
        d.TryGetValue(key, decimalVal)
        Return decimalVal
    End Function
    Private Function LoadDictionaryInt(d As Dictionary(Of String, Object), key As String) As Integer
        Dim intVal As Integer = 0
        d.TryGetValue(key, intVal)
        Return intVal
    End Function
    Private Sub CalculateLeasePaymentSchedule(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object))

    End Sub
    Private Sub CalculateBalloonPaymentSchedule(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object))
        LoadFinanceFirstLine(currentRun)
        APRAdjustment(currentRun, AddressOf BalloonAdjustment)
    End Sub

    Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

    End Sub

    Public Overrides Sub Requirement(validationRoot As Rule)

    End Sub

    Private Function GetDefaultValue(type As Type) As Object
        Throw New NotImplementedException
    End Function

    Private Sub LoadFinanceFirstLine(currentRun As Dictionary(Of String, Object))
        PaymentDictionary(FIRSTLINE).IsValid = True
        PaymentDictionary(FIRSTLINE).PaymentPeriod = LoadDictionaryInt(currentRun, "DEAL_TERM")
        PaymentDictionary(FIRSTLINE).Payment = LoadDictionaryDecimal(currentRun, "DEAL_PAYMENT")
        PaymentDictionary(FIRSTLINE).PaymentStartDate = LoadDictionaryStr(currentRun, "DEAL_PAYMENTDATE")
        If IsDate(PaymentDictionary(FIRSTLINE).PaymentStartDate) Then
            Dim term As Integer = PaymentDictionary(FIRSTLINE).PaymentPeriod
            Dim endDate As Date = DateAdd(DateInterval.Month, term, Date.Parse(PaymentDictionary(FIRSTLINE).PaymentStartDate))
            PaymentDictionary(FIRSTLINE).PaymentEndDate = endDate.ToString("MM/dd/yyyy")
        End If
    End Sub
    Private Sub APRAdjustment(currentRun As Dictionary(Of String, Object), balloonAdjustment As Action(Of Boolean, Dictionary(Of String, Object)))
        Dim APR As Decimal = LoadDictionaryDecimal(currentRun, "DEAL_APR")
        If APR <= 0.01 Then ' APR = 0, Last Payment may need adjustment, due to Artifical Finance Charge/Rounding.
            Dim BalanceFinanced As Decimal = LoadDictionaryDecimal(currentRun, "DEAL_BALANCEFINANCED")
            Dim pmt As Decimal = PaymentDictionary(FIRSTLINE).Payment
            Dim term As Integer = PaymentDictionary(FIRSTLINE).PaymentPeriod
            If term - 1 < 0 Then Exit Sub
            Dim Adjusted_Last_Payment = pmt + BalanceFinanced - (pmt * (term - 1))
            If Adjusted_Last_Payment <> pmt Then ' Payments are not uniform from first to last payment.
                PaymentDictionary(FIRSTLINE).PaymentPeriod = term - 1
                PaymentDictionary(SECONDLINE).IsValid = True 'Last Payment requires a line on Payment Schedule.
                PaymentDictionary(SECONDLINE).Payment = Adjusted_Last_Payment
                PaymentDictionary(SECONDLINE).PaymentPeriod = 1
                If IsDate(PaymentDictionary(FIRSTLINE).PaymentStartDate) Then
                    PaymentDictionary(SECONDLINE).PaymentStartDate = PaymentDictionary(FIRSTLINE).PaymentEndDate 'Last Payment Date Start and End are same as Last Payment Date of Uniform Term
                    PaymentDictionary(SECONDLINE).PaymentEndDate = PaymentDictionary(SECONDLINE).PaymentStartDate
                    'Recalculate Last Payment Date for First Line of schedule...
                    PaymentDictionary(FIRSTLINE).PaymentEndDate = DateAdd(DateInterval.Month, -1, Date.Parse(PaymentDictionary(SECONDLINE).PaymentStartDate)).ToString("MM/dd/yyyy")
                End If
                If balloonAdjustment IsNot Nothing Then balloonAdjustment(True, currentRun)
            Else
                If balloonAdjustment IsNot Nothing Then balloonAdjustment(False, currentRun)
            End If
        Else
            If balloonAdjustment IsNot Nothing Then balloonAdjustment(False, currentRun)
        End If
    End Sub
    Private Sub BalloonAdjustment(LastPaymentAdjustment As Boolean, currentRun As Dictionary(Of String, Object))
        Dim BalloonType As String = LoadDictionaryStr(currentRun, "BANK_BALOONTYPE")
        Dim Balloon As Decimal = LoadDictionaryDecimal(currentRun, "DEAL_BALOON")
        PaymentDictionary(FIRSTLINE).PaymentPeriod -= 1
        If IsDate(PaymentDictionary(FIRSTLINE).PaymentEndDate) Then
            Dim startDateAdjustmentFinal As Date = Date.Parse(PaymentDictionary(FIRSTLINE).PaymentEndDate)
            Dim endDateAdjustment As Date = DateAdd(DateInterval.Month, -1, startDateAdjustmentFinal)
            PaymentDictionary(FIRSTLINE).PaymentEndDate = endDateAdjustment.ToString("MM/dd/yyyy")
            If IsDate(PaymentDictionary(SECONDLINE).PaymentStartDate) Then
                startDateAdjustmentFinal = DateAdd(DateInterval.Month, -1, Date.Parse(PaymentDictionary(SECONDLINE).PaymentStartDate))
            End If
            PaymentDictionary(SECONDLINE).PaymentStartDate = startDateAdjustmentFinal.ToString("MM/dd/yyyy")
            PaymentDictionary(SECONDLINE).PaymentEndDate = startDateAdjustmentFinal.ToString("MM/dd/yyyy")
        End If
        If LastPaymentAdjustment Then
            If BalloonType.StartsWith("D") Then
                PaymentDictionary(SECONDLINE).Payment = PaymentDictionary(SECONDLINE).Payment + Balloon
            Else
                PaymentDictionary(THIRDLINE).IsValid = True
                PaymentDictionary(THIRDLINE).Payment = Balloon
                PaymentDictionary(THIRDLINE).PaymentPeriod = 1
                If IsDate(PaymentDictionary(SECONDLINE).PaymentStartDate) Then
                    Dim startDateFinal As Date = DateAdd(DateInterval.Month, 1, Date.Parse(PaymentDictionary(SECONDLINE).PaymentEndDate))
                    PaymentDictionary(THIRDLINE).PaymentStartDate = startDateFinal.ToString("MM/dd/yyyy")
                    PaymentDictionary(THIRDLINE).PaymentEndDate = startDateFinal.ToString("MM/dd/yyyy")
                End If
            End If
        Else 'Operate on FIRST LINE/STARTDATE/ENDDATE
            PaymentDictionary(SECONDLINE).IsValid = True
            PaymentDictionary(SECONDLINE).PaymentPeriod = 1
            If IsDate(PaymentDictionary(FIRSTLINE).PaymentEndDate) Then
                Dim dateFinal As Date = DateAdd(DateInterval.Month, 1, Date.Parse(PaymentDictionary(FIRSTLINE).PaymentEndDate))
                PaymentDictionary(SECONDLINE).PaymentStartDate = dateFinal.ToString("MM/dd/yyyy")
                PaymentDictionary(SECONDLINE).PaymentEndDate = dateFinal.ToString("MM/dd/yyyy")
            End If
            If BalloonType.StartsWith("D") Then
                PaymentDictionary(SECONDLINE).Payment = PaymentDictionary(FIRSTLINE).Payment + Balloon
            Else
                PaymentDictionary(SECONDLINE).Payment = Balloon
            End If
        End If
    End Sub
End Class
