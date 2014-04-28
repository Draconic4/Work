Public Class PaymentScheduleLine
    Public Property IsValid As Boolean
    Public Property PaymentPeriod As Integer
    Public Property Payment As Decimal
    Public Property PaymentStartDate As String
    Public Property PaymentEndDate As String
    Public Property PaymentLineFunc As Func(Of String, String, String, String, String)

    Public Sub New()
        PaymentStartDate = String.Empty
        PaymentEndDate = String.Empty
    End Sub

    Public Function PaymentLine() As String
        If Not IsValid Then Return String.Empty
        If PaymentLineFunc Is Nothing Then Return String.Empty
        Return PaymentLineFunc(PaymentPeriod, Payment, PaymentStartDate, PaymentEndDate)
    End Function
End Class
