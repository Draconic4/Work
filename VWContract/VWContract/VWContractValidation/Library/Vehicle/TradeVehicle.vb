Imports Csla

Namespace ValidationLib
    Public Class TradeVehicle
        Inherits BaseFormField

        Public Shared ReadOnly BaseVehicleProperty As PropertyInfo(Of BaseVehicle) = RegisterPropertyLocal(Of BaseVehicle)(Function(c) (c.Base), "", Nothing)

#Region "  Properties "
        Public Property Base As BaseVehicle
            Get
                Return GetProperty(BaseVehicleProperty)
            End Get
            Set(value As BaseVehicle)
                SetProperty(BaseVehicleProperty, value)
            End Set
        End Property
        Public Property BalanceAmount As Decimal
            Get
                Return 0D
            End Get
            Set(value As Decimal)

            End Set
        End Property
        Public Property LienCompany As String
            Get
                Return ""
            End Get
            Set(value As String)

            End Set
        End Property
        Public Property NetTradeAllowanceAmount As Decimal
            Get
                Return 0D
            End Get
            Set(value As Decimal)

            End Set
        End Property
        Public Property GrossTradeIn As Decimal
            Get
                Return 0D
            End Get
            Set(value As Decimal)

            End Set
        End Property
        Public Property LienAmount As Decimal
            Get
                Return 0D
            End Get
            Set(value As Decimal)

            End Set
        End Property
        Public Property TradeInSalesTaxCredit As Decimal
            Get
                Return 0D
            End Get
            Set(value As Decimal)

            End Set
        End Property
#End Region

#Region "  Data Access "
        Public Sub New(vehArgs As VehicleArgs)
            MyBase.New(vehArgs.GlobalProperty)
            LoadProperty(BaseVehicleProperty, BaseVehicle.Fetch(vehArgs))
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
            Base.ReplicateCurrentState(d)
        End Sub
#End Region

#Region "  Business Rules "
        Public Overrides Sub CheckRules()
            Me.BusinessRules.CheckRules()
        End Sub
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)
        End Sub
#End Region
        'Inheritance Worker...
        Private Shared Function RegisterPropertyLocal(Of T)(propertyLambdaExpression As Linq.Expressions.Expression(Of Func(Of TradeVehicle, T)), friendlyName As String, defaultValue As T) As PropertyInfo(Of T)
            Dim reflectedPropertyInfo = Reflection.Reflect(Of TradeVehicle).GetProperty(propertyLambdaExpression)
            Return RegisterProperty(GetType(Vehicle), Csla.Core.FieldManager.PropertyInfoFactory.Factory.Create(Of T)(GetType(TradeVehicle), reflectedPropertyInfo.Name, friendlyName, defaultValue))
        End Function
    End Class
End Namespace
