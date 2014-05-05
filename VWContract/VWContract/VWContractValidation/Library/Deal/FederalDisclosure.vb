Namespace ValidationLib
    Public Class FederalDisclosure
        Inherits BaseFormField

        Public Property APR As Decimal
        Public Property FinanceCharge As Decimal
        Public Property TotalOfPayments As Decimal
        Public Property TotalSalePrice As Decimal
        Public Property FirstPaymentDate As Decimal
        Public Property BasePaymentAmount As Decimal
        Public Property LeaseRateMoneyFactor As Decimal

        Public Sub New()
            MyBase.New(Nothing)
        End Sub

        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub

        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
    End Class
End Namespace
