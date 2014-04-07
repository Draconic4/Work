Imports Csla
Namespace ValidationRuleData
    Public Class DealerProduct
        Inherits BusinessBase(Of DealerProduct)

        Private _globalProperties As ProcessInfo
        Public Shared ReadOnly ProductTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.ProductType)
        Public Shared ReadOnly ProviderProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) c.Provider)
        Public Shared ReadOnly AmountProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) c.Amount)
        Public Shared ReadOnly CapitalizedProperty As PropertyInfo(Of Boolean) = RegisterProperty(Of Boolean)(Function(c) c.Capitalized)
        Public Shared ReadOnly OEMPreferredProperty As PropertyInfo(Of Boolean) = RegisterProperty(Of Boolean)(Function(c) c.OEMPreferred)

#Region "  Properties "
        Public ReadOnly Property ProductType() As String
            Get
                Return GetProperty(ProductTypeProperty)
            End Get
        End Property
        Public ReadOnly Property Provider() As String
            Get
                Return GetProperty(ProviderProperty)
            End Get
        End Property
        Public ReadOnly Property Amount() As Decimal
            Get
                Return GetProperty(AmountProperty)
            End Get
        End Property
        Public ReadOnly Property Capitalized() As Boolean
            Get
                Return GetProperty(CapitalizedProperty)
            End Get
        End Property
        Public ReadOnly Property OEMPreferred() As Boolean
            Get
                Return GetProperty(OEMPreferredProperty)
            End Get
        End Property
#End Region
    End Class
End Namespace
