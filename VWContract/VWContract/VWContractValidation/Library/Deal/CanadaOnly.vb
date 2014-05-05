Imports Csla

Namespace ValidationLib
    Public Class CanadaOnly
        Inherits ValidationBaseFormField

        Public Shared ReadOnly AggregateCashPriceProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.AggregateCashPrice), String.Empty, 0D)
        Public Shared ReadOnly CostOfBorrowingProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.CostOfBorrowingAmount), String.Empty, 0D)
        Public Shared ReadOnly ValueGivenByYouProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.ValueGivenByYouAmount), String.Empty, 0D)
        Public Shared ReadOnly ValueReceivedByYouProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.ValueReceivedByYouAmount), String.Empty, 0D)
        Public Shared ReadOnly CashValueOfLeasedGoodsProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.CashValueOfLeasedGoodsAmount), String.Empty, 0D)
        Public Shared ReadOnly LeaseCapitalizedProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.LeaseCapitalizedAmount), String.Empty, 0D)
        Public Shared ReadOnly ImplicitFinanceChargeProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.ImplicitFinanceChargesAmount), String.Empty, 0D)
        Public Shared ReadOnly TotalCostOfTermProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.TotalCostOfTerm), String.Empty, 0D)
        Public Shared ReadOnly TotalCostOfLeaseProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.TotalCostOfLease), String.Empty, 0D)
        Public Shared ReadOnly UpfrontCashDownPaymentProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.UpfrontCashDownPaymentAmount), String.Empty, 0D)
        Public Shared ReadOnly UpfrontMfgRebateProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.UpfrontMfgRebateAmount), String.Empty, 0D)
        Public Shared ReadOnly UpfrontNetTradeProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal, CanadaOnly)(Function(c) (c.UpfrontNetTradeAmount), String.Empty, 0D)

#Region "  Properties "
        Public Property AggregateCashPrice As Decimal
            Get
                Return GetProperty(AggregateCashPriceProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(AggregateCashPriceProperty, value)
            End Set
        End Property
        Public Property CostOfBorrowingAmount As Decimal
            Get
                Return GetProperty(CostOfBorrowingProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(CostOfBorrowingProperty, value)
            End Set
        End Property
        Public Property ValueGivenByYouAmount As Decimal
            Get
                Return GetProperty(ValueGivenByYouProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(ValueGivenByYouProperty, value)
            End Set
        End Property
        Public Property ValueReceivedByYouAmount As Decimal
            Get
                Return GetProperty(ValueReceivedByYouProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(ValueReceivedByYouProperty, value)
            End Set
        End Property
        Public Property CashValueOfLeasedGoodsAmount As Decimal
            Get
                Return GetProperty(CashValueOfLeasedGoodsProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(CashValueOfLeasedGoodsProperty, value)
            End Set
        End Property
        Public Property LeaseCapitalizedAmount As Decimal
            Get
                Return GetProperty(LeaseCapitalizedProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(LeaseCapitalizedProperty, value)
            End Set
        End Property
        Public Property ImplicitFinanceChargesAmount As Decimal
            Get
                Return GetProperty(ImplicitFinanceChargeProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(ImplicitFinanceChargeProperty, value)
            End Set
        End Property
        Public Property TotalCostOfTerm As Decimal
            Get
                Return GetProperty(TotalCostOfTermProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(TotalCostOfTermProperty, value)
            End Set
        End Property
        Public Property TotalCostOfLease As Decimal
            Get
                Return GetProperty(TotalCostOfLeaseProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(TotalCostOfLeaseProperty, value)
            End Set
        End Property
        Public Property UpfrontCashDownPaymentAmount As Decimal
            Get
                Return GetProperty(UpfrontCashDownPaymentProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(UpfrontCashDownPaymentProperty, value)
            End Set
        End Property
        Public Property UpfrontMfgRebateAmount As Decimal
            Get
                Return GetProperty(UpfrontMfgRebateProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(UpfrontMfgRebateProperty, value)
            End Set
        End Property
        Public Property UpfrontNetTradeAmount As Decimal
            Get
                Return GetProperty(UpfrontNetTradeProperty)
            End Get
            Set(value As Decimal)
                SetPropertyLocal(UpfrontNetTradeProperty, value)
            End Set
        End Property
#End Region

#Region "  Data Access "
        Public Sub New(ByVal gProp As ProcessInfo)
            MyBase.New(gProp)
            MyBase.validationFunc = AddressOf ProcessUtility.IsCanadian
        End Sub
        Public Shared Function Fetch(ByVal DealArgs As BaseConstructionArgs) As CanadaOnly
            Dim co As New CanadaOnly(DealArgs.GlobalProperty)
            Return co
        End Function
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub
#End Region

#Region "  Business Rules "
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
#End Region
    End Class
End Namespace
