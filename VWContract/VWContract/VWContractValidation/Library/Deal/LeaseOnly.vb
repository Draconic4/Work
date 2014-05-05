Namespace ValidationLib
    Public Class LeaseOnly
        Inherits BaseFormField
        Private _IsActive As Boolean

        Public ReadOnly Property Active As Boolean
            Get
                Return _IsActive
            End Get
        End Property

        Public Property TaxExempt As Boolean
        Public Property TotalOfMonthlyPaymentsAmount As Decimal
        Public Property TotalDueAtSigningAmount As Decimal
        Public Property PurchaseOptionPrice As Decimal
        Public Property ServiceChargeAmount As Decimal


        Public Sub New()
            MyBase.New(Nothing)
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub

        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
    End Class
End Namespace
