Imports Csla
Namespace ValidationLib
    Public Class DealerProduct
        Inherits BaseFormField

#Region "  Properties "
        Public Property [Type] As Integer
            Get
                Return -1
            End Get
            Set(value As Integer)

            End Set
        End Property
        Public Property Amount As Decimal
            Get
                Return 0D
            End Get
            Set(value As Decimal)

            End Set
        End Property
        Public Property PaidTo As String
            Get
                Return String.Empty
            End Get
            Set(value As String)

            End Set
        End Property
        Public Property Capitalized As Boolean
            Get
                Return False
            End Get
            Set(value As Boolean)

            End Set
        End Property
        Public Property PreferredProviderIndication As Boolean
            Get
                Return False
            End Get
            Set(value As Boolean)

            End Set
        End Property
#End Region

        Public Sub New()
            MyBase.New(Nothing)
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub

        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
        Private Shared Function RegisterPropertyLocal(Of T)(propertyLambdaExpression As Linq.Expressions.Expression(Of Func(Of DealerProduct, T)), friendlyName As String, defaultValue As T) As PropertyInfo(Of T)
            Dim reflectedPropertyInfo = Reflection.Reflect(Of DealerProduct).GetProperty(propertyLambdaExpression)
            Return RegisterProperty(GetType(DealerProduct), Csla.Core.FieldManager.PropertyInfoFactory.Factory.Create(Of T)(GetType(DealerProduct), reflectedPropertyInfo.Name, friendlyName, defaultValue))
        End Function
    End Class
End Namespace
