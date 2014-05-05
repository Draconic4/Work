Imports Csla

Namespace ValidationLib
    Public Class Pricing
        Inherits BaseFormField

        Enum PriceType
            None
            Vehicle
        End Enum
        Enum PriceCategoryType
            None
            GrossCapCost
            NetCapCost
            TaxableSellingPrice
        End Enum

        Public Shared ReadOnly PriceTypeProperty As PropertyInfo(Of PriceType) = RegisterPropertyLocal(Of PriceType)(Function(c) (c.Type), "PriceType", PriceType.None)
        Public Shared ReadOnly CategoryTypeProperty As PropertyInfo(Of PriceCategoryType) = RegisterPropertyLocal(Of PriceCategoryType)(Function(c) (c.PriceCategory), "PriceCategory", PriceCategoryType.None)
        Public Shared ReadOnly KeyProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.Key), String.Empty, String.Empty)
        Public Shared ReadOnly ValueProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal)(Function(c) (c.Value), "_PRICE", -1D)
#Region "  Properties "
        Public Property [Type] As PriceType
            Get
                Return GetProperty(PriceTypeProperty)
            End Get
            Set(value As PriceType)
                SetProperty(PriceTypeProperty, value)
            End Set
        End Property
        Public Property Key As String
            Get
                Return GetProperty(KeyProperty)
            End Get
            Set(value As String)
                SetProperty(KeyProperty, value)
            End Set
        End Property
        Public Property PriceCategory As PriceCategoryType
            Get
                Return GetProperty(CategoryTypeProperty)
            End Get
            Set(value As PriceCategoryType)
                SetProperty(CategoryTypeProperty, value)
            End Set
        End Property
        Public Property Value As Decimal
            Get
                Return GetProperty(ValueProperty)
            End Get
            Set(value As Decimal)
                SetProperty(ValueProperty, value)
            End Set
        End Property
#End Region

#Region "  Data Access "
        Public Sub New()
            MyBase.New(Nothing)
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub
#End Region

#Region "  Business Rules "
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
#End Region

        Private Shared Function RegisterPropertyLocal(Of T)(propertyLambdaExpression As Linq.Expressions.Expression(Of Func(Of Pricing, T)), friendlyName As String, defaultValue As T) As PropertyInfo(Of T)
            Dim reflectedPropertyInfo = Reflection.Reflect(Of Pricing).GetProperty(propertyLambdaExpression)
            Return RegisterProperty(GetType(Pricing), Csla.Core.FieldManager.PropertyInfoFactory.Factory.Create(Of T)(GetType(Pricing), reflectedPropertyInfo.Name, friendlyName, defaultValue))
        End Function
    End Class
End Namespace
