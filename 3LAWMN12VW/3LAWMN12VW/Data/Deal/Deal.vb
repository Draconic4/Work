Imports Csla
Namespace ValidationRuleData
    Public Class Deal
        Inherits BusinessBase(Of Deal)

        Public Shared ReadOnly ContractDateProperty As PropertyInfo(Of Csla.SmartDate) = RegisterProperty(Of Csla.SmartDate)(Function(c) c.ContractDate)
        Public Shared ReadOnly TermProperty As PropertyInfo(Of Integer) = RegisterProperty(Of Integer)(Function(c) c.Term)
        Public Shared ReadOnly BaseVehiclePriceProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.BaseVehiclePrice)
        Public Shared ReadOnly HardAddSellingPriceProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.HardAddSellingPrice)
        Public Shared ReadOnly TotalVehicleCashPriceProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.TotalVehicleCashPrice)
        Public Shared ReadOnly TotalDownPaymentProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.TotalDownPayment)
        Public Shared ReadOnly TotalDeferredDownPaymentProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.TotalDeferredDownPayment)
        Public Shared ReadOnly DeferredUntilDateProperty As PropertyInfo(Of Csla.SmartDate) = RegisterProperty(Of Csla.SmartDate)(Function(c) c.DeferredUntilDate)
        Public Shared ReadOnly CashDownPaymentProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.CashDownPayment)
        Public Shared ReadOnly OEMRebateProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.OEMRebate)
        Public Shared ReadOnly OtherDownPaymentDescriptionProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.OtherDownPaymentDescription)
        Public Shared ReadOnly OtherDownPaymentAmountProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.OtherDownPaymentAmount)
        Public Shared ReadOnly UnpaidBalanceProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.UnpaidBalance)
        Public Shared ReadOnly TotalAmountPaidOnYourBehalfProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.TotalAmountPaidOnYourBehalf)
        Public Shared ReadOnly AmountFinancedProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.AmountFinanced)
        Public Shared ReadOnly AnnualMilesAllowedProperty As PropertyInfo(Of Integer) = RegisterProperty(Of Integer)(Function(c) c.AnnualMilesAllowed)
        Public Shared ReadOnly ExcessMileageRateProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.ExcessMileageRate)
        Public Shared ReadOnly AnnualPercentageRateProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.AnnualPercentageRate)
        Public Shared ReadOnly CanadaACRPercentageProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.CanadaACRPercentage)
        Public Shared ReadOnly FinanceChargeProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.FinanceCharge)
        Public Shared ReadOnly TotalOfPaymentsProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.TotalOfPayments)
        Public Shared ReadOnly TotalSalePriceProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.TotalSalePrice)
        Public Shared ReadOnly FirstPaymentDateProperty As PropertyInfo(Of Csla.SmartDate) = RegisterProperty(Of Csla.SmartDate)(Function(c) c.FirstPaymentDate)
        Public Shared ReadOnly PaymentScheduleProperty As PropertyInfo(Of PaymentSchedule) = RegisterProperty(Of PaymentSchedule)(Function(c) c.PaymentSchedule)
        Public Shared ReadOnly LeaseRateMoneyFactorProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.LeaseRateMoneyFactor)
        Public Shared ReadOnly ProgramsRebatesProperty As PropertyInfo(Of ProgramsRebates) = RegisterProperty(Of ProgramsRebates)(Function(c) c.ProgramsRebates)
        Public Shared ReadOnly TaxProperty As PropertyInfo(Of TaxValue) = RegisterProperty(Of TaxValue)(Function(c) c.Tax)
        Public Shared ReadOnly FeeProperty As PropertyInfo(Of Fee) = RegisterProperty(Of Fee)(Function(c) c.Fee)
        Public Shared ReadOnly OtherChargesProperty As PropertyInfo(Of OtherCharges) = RegisterProperty(Of OtherCharges)(Function(c) c.OtherCharges)
        Public Shared ReadOnly VehicleInsuranceProperty As PropertyInfo(Of VehicleInsurance) = RegisterProperty(Of VehicleInsurance)(Function(c) c.VehicleInsurance)
        Public Shared ReadOnly NonVehicleInsuranceProperty As PropertyInfo(Of NonVehicleInsurance) = RegisterProperty(Of NonVehicleInsurance)(Function(c) c.NonVehicleInsurance)
        Public Shared ReadOnly ServiceContractProperty As PropertyInfo(Of ServiceContract) = RegisterProperty(Of ServiceContract)(Function(c) c.ServiceContract)
        Public Shared ReadOnly TradeInProperty As PropertyInfo(Of TradeIn) = RegisterProperty(Of TradeIn)(Function(c) c.TradeIn)
        Public Shared ReadOnly AutomaticPaymentProperty As PropertyInfo(Of AutomatedPayment) = RegisterProperty(Of AutomatedPayment)(Function(c) c.AutomaticPayment)
        Public Shared ReadOnly FinancePlanProcessingDateProperty As PropertyInfo(Of Csla.SmartDate) = RegisterProperty(Of Csla.SmartDate)(Function(c) c.FinancePlanProcessingDate)
        Public Shared ReadOnly TaxExemptProperty As PropertyInfo(Of Boolean) = RegisterProperty(Of Boolean)(Function(c) c.TaxExempt)
        Public Property TaxExempt() As Boolean
            Get
                Return GetProperty(TaxExemptProperty)
            End Get
            Private Set(ByVal value As Boolean)
                LoadProperty(TaxExemptProperty, value)
            End Set
        End Property
        Public Property FinancePlanProcessingDate() As Csla.SmartDate
            Get
                Return GetProperty(FinancePlanProcessingDateProperty)
            End Get
            Private Set(ByVal value As Csla.SmartDate)
                LoadProperty(FinancePlanProcessingDateProperty, value)
            End Set
        End Property
        Public Property AutomaticPayment() As AutomatedPayment
            Get
                Return GetProperty(AutomaticPaymentProperty)
            End Get
            Private Set(ByVal value As AutomatedPayment)
                LoadProperty(AutomaticPaymentProperty, value)
            End Set
        End Property
        Public Property TradeIn() As TradeIn
            Get
                Return GetProperty(TradeInProperty)
            End Get
            Private Set(ByVal value As TradeIn)
                LoadProperty(TradeInProperty, value)
            End Set
        End Property
        Public Property ServiceContract() As ServiceContract
            Get
                Return GetProperty(ServiceContractProperty)
            End Get
            Private Set(ByVal value As ServiceContract)
                LoadProperty(ServiceContractProperty, value)
            End Set
        End Property
        Public Property NonVehicleInsurance() As NonVehicleInsurance
            Get
                Return GetProperty(NonVehicleInsuranceProperty)
            End Get
            Private Set(ByVal value As NonVehicleInsurance)
                LoadProperty(NonVehicleInsuranceProperty, value)
            End Set
        End Property
        Public Property VehicleInsurance() As VehicleInsurance
            Get
                Return GetProperty(VehicleInsuranceProperty)
            End Get
            Private Set(ByVal value As VehicleInsurance)
                LoadProperty(VehicleInsuranceProperty, value)
            End Set
        End Property
        Public Property OtherCharges() As OtherCharges
            Get
                Return GetProperty(OtherChargesProperty)
            End Get
            Private Set(ByVal value As OtherCharges)
                LoadProperty(OtherChargesProperty, value)
            End Set
        End Property
        Public Property Fee() As Fee
            Get
                Return GetProperty(FeeProperty)
            End Get
            Private Set(ByVal value As Fee)
                LoadProperty(FeeProperty, value)
            End Set
        End Property
        Public Property Tax() As TaxValue
            Get
                Return GetProperty(TaxProperty)
            End Get
            Private Set(ByVal value As TaxValue)
                LoadProperty(TaxProperty, value)
            End Set
        End Property
        Public Property ProgramsRebates() As ProgramsRebates
            Get
                Return GetProperty(ProgramsRebatesProperty)
            End Get
            Private Set(ByVal value As ProgramsRebates)
                LoadProperty(ProgramsRebatesProperty, value)
            End Set
        End Property
        Public Property LeaseRateMoneyFactor() As Decimal
            Get
                Return GetProperty(LeaseRateMoneyFactorProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(LeaseRateMoneyFactorProperty, value)
            End Set
        End Property
        Public Property PaymentSchedule() As PaymentSchedule
            Get
                Return GetProperty(PaymentScheduleProperty)
            End Get
            Private Set(ByVal value As PaymentSchedule)
                LoadProperty(PaymentScheduleProperty, value)
            End Set
        End Property
        Public Property FirstPaymentDate() As Csla.SmartDate
            Get
                Return GetProperty(FirstPaymentDateProperty)
            End Get
            Private Set(ByVal value As Csla.SmartDate)
                LoadProperty(FirstPaymentDateProperty, value)
            End Set
        End Property
        Public Property TotalSalePrice() As Decimal
            Get
                Return GetProperty(TotalSalePriceProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(TotalSalePriceProperty, value)
            End Set
        End Property
        Public Property TotalOfPayments() As Decimal
            Get
                Return GetProperty(TotalOfPaymentsProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(TotalOfPaymentsProperty, value)
            End Set
        End Property
        Public Property FinanceCharge() As Decimal
            Get
                Return GetProperty(FinanceChargeProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(FinanceChargeProperty, value)
            End Set
        End Property
        Public Property CanadaACRPercentage() As Decimal
            Get
                Return GetProperty(CanadaACRPercentageProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(CanadaACRPercentageProperty, value)
            End Set
        End Property
        Public Property AnnualPercentageRate() As Decimal
            Get
                Return GetProperty(AnnualPercentageRateProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(AnnualPercentageRateProperty, value)
            End Set
        End Property
        Public Property ExcessMileageRate() As Decimal
            Get
                Return GetProperty(ExcessMileageRateProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(ExcessMileageRateProperty, value)
            End Set
        End Property
        Public Property AnnualMilesAllowed() As Integer
            Get
                Return GetProperty(AnnualMilesAllowedProperty)
            End Get
            Private Set(ByVal value As Integer)
                LoadProperty(AnnualMilesAllowedProperty, value)
            End Set
        End Property
        Public Property AmountFinanced() As Decimal
            Get
                Return GetProperty(AmountFinancedProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(AmountFinancedProperty, value)
            End Set
        End Property
        Public Property TotalAmountPaidOnYourBehalf() As Decimal
            Get
                Return GetProperty(TotalAmountPaidOnYourBehalfProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(TotalAmountPaidOnYourBehalfProperty, value)
            End Set
        End Property
        Public Property UnpaidBalance() As Decimal
            Get
                Return GetProperty(UnpaidBalanceProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(UnpaidBalanceProperty, value)
            End Set
        End Property
        Public Property OtherDownPaymentAmount() As Decimal
            Get
                Return GetProperty(OtherDownPaymentAmountProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(OtherDownPaymentAmountProperty, value)
            End Set
        End Property
        Public Property OtherDownPaymentDescription() As Integer
            Get
                Return GetProperty(OtherDownPaymentDescriptionProperty)
            End Get
            Private Set(ByVal value As Integer)
                LoadProperty(OtherDownPaymentDescriptionProperty, value)
            End Set
        End Property
        Public Property OEMRebate() As Decimal
            Get
                Return GetProperty(OEMRebateProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(OEMRebateProperty, value)
            End Set
        End Property
        Public Property CashDownPayment() As Decimal
            Get
                Return GetProperty(CashDownPaymentProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(CashDownPaymentProperty, value)
            End Set
        End Property
        Public Property DeferredUntilDate() As Csla.SmartDate
            Get
                Return GetProperty(DeferredUntilDateProperty)
            End Get
            Private Set(ByVal value As Csla.SmartDate)
                LoadProperty(DeferredUntilDateProperty, value)
            End Set
        End Property
        Public Property TotalDeferredDownPayment() As Decimal
            Get
                Return GetProperty(TotalDeferredDownPaymentProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(TotalDeferredDownPaymentProperty, value)
            End Set
        End Property
        Public Property TotalDownPayment() As Decimal
            Get
                Return GetProperty(TotalDownPaymentProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(TotalDownPaymentProperty, value)
            End Set
        End Property
        Public Property TotalVehicleCashPrice() As Decimal
            Get
                Return GetProperty(TotalVehicleCashPriceProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(TotalVehicleCashPriceProperty, value)
            End Set
        End Property
        Public Property HardAddSellingPrice() As Decimal
            Get
                Return GetProperty(HardAddSellingPriceProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(HardAddSellingPriceProperty, value)
            End Set
        End Property
        Public Property BaseVehiclePrice() As Decimal
            Get
                Return GetProperty(BaseVehiclePriceProperty)
            End Get
            Private Set(ByVal value As Decimal)
                LoadProperty(BaseVehiclePriceProperty, value)
            End Set
        End Property
        Public Property Term() As Integer
            Get
                Return GetProperty(TermProperty)
            End Get
            Private Set(ByVal value As Integer)
                LoadProperty(TermProperty, value)
            End Set
        End Property
        Public Property ContractDate() As Csla.SmartDate
            Get
                Return GetProperty(ContractDateProperty)
            End Get
            Private Set(ByVal value As Csla.SmartDate)
                LoadProperty(ContractDateProperty, value)
            End Set
        End Property


    End Class
End Namespace