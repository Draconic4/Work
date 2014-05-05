Namespace ValidationLib
    Public Class DealDetails
        Inherits BaseFormField


#Region "  Properties "
        Public Property BaseVehicleCashPrice As Decimal
        Public Property HardAddSellingPrice As Decimal
        Public Property TotalVehicleCashPrice As Decimal
        Public Property TotalDownPaymentAmount As Decimal
        Public Property CashDownPayment As Decimal
        Public Property ManufacturerRebateAmount As Decimal
        Public Property OtherDownPaymentDescription As String
        Public Property OtherDownPaymentAmount As Decimal
        Public Property UnPaidBalance As Decimal
        Public Property TotalAmountPaidOnYourBehalf As Decimal
        Public Property AmountFinanced As Decimal
        Public Property DeferredDownPaymentAmount As Decimal
        Public Property DeferredUntilDate As String
        Public Property AnnualMilesAllowed As String
        Public Property ExcessMileageRate As Decimal
        Public Property SecurityDepositAmount As Decimal
        Public Property TotalAmtOfBaseMonthlyPayment As Decimal
        Public Property TotalEstimatedFeesAndTaxesAmount As Decimal
        Public Property ResidualAmount As Decimal
        Public Property DisabilityAndLifeSubtotalAmount As Decimal
        Public Property FinancePlanProcessingDate As String
        

#End Region
        Public Sub New()
            MyBase.New(Nothing)
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub

        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
    End Class
End Namespace
