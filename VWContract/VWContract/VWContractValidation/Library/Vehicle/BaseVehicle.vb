Imports Csla

Namespace ValidationLib
    Public Class BaseVehicle
        Inherits BaseFormField

        Public Shared ReadOnly VehicleTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.VehicleType), String.Empty, "Vehicle")
        Public Shared ReadOnly VehicleIndexProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer)(Function(c) (c.VehicleIndex), String.Empty, 1)
        Public Shared ReadOnly ModelProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.Model), "_MODEL", "Vehicle")
        Public Shared ReadOnly ModelYearProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.ModelYear), "_YEAR", "Vehicle")
        Public Shared ReadOnly MakeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.Make), "_MAKE", "Vehicle")
        Public Shared ReadOnly VINProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.VIN), "_VIN", String.Empty)
        Public Shared ReadOnly OdometerProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer)(Function(c) (c.Odometer), "_ODOM", 0)
        Public Shared ReadOnly BodyProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.Body), "_BODY", String.Empty)

#Region "  Properties "
        Public Property VehicleType As String
            Get
                Return GetProperty(VehicleTypeProperty)
            End Get
            Set(value As String)
                SetProperty(VehicleTypeProperty, value)
            End Set
        End Property
        Public Property VehicleIndex As Integer
            Get
                Return GetProperty(VehicleIndexProperty)
            End Get
            Set(value As Integer)
                SetProperty(VehicleIndexProperty, value)
            End Set
        End Property
        Public Property Model As String
            Get
                Return GetProperty(ModelProperty)
            End Get
            Set(value As String)
                SetProperty(ModelProperty, value)
            End Set
        End Property
        Public Property ModelYear As String
            Get
                Return GetProperty(ModelYearProperty)
            End Get
            Set(value As String)
                SetProperty(ModelYearProperty, value)
            End Set
        End Property
        Public Property Make As String
            Get
                Return GetProperty(MakeProperty)
            End Get
            Set(value As String)
                SetProperty(MakeProperty, value)
            End Set
        End Property
        Public Property VIN As String
            Get
                Return GetProperty(VINProperty)
            End Get
            Set(value As String)
                SetProperty(VINProperty, value)
            End Set
        End Property
        Public Property Odometer As Integer
            Get
                Return GetProperty(OdometerProperty)
            End Get
            Set(value As Integer)
                SetProperty(OdometerProperty, value)
            End Set
        End Property
        Public Property Body As String
            Get
                Return GetProperty(BodyProperty)
            End Get
            Set(value As String)
                SetProperty(BodyProperty, value)
            End Set
        End Property
#End Region

#Region "  Data Access "
        Public Sub New(ByVal vehicleArgs As VehicleArgs)
            MyBase.New(vehicleArgs.GlobalProperty)
            LoadProperty(VehicleTypeProperty, vehicleArgs.VehicleType)
            LoadProperty(VehicleIndexProperty, vehicleArgs.VehicleIndex)
        End Sub
        Public Shared Function Fetch(ByVal vehicleArgs As VehicleArgs) As BaseVehicle
            Dim vb As New BaseVehicle(vehicleArgs)
            vb.ReadDictionary(vehicleArgs)
            Return vb
        End Function
        Public Overrides Sub PopulateOverride(currentRun As Dictionary(Of String, Object))
            PopulateField(currentRun, ModelProperty)
            PopulateField(currentRun, ModelYearProperty)
            PopulateField(currentRun, MakeProperty)
            PopulateField(currentRun, VINProperty)
            'PopulateField(currentRun, OdometerProperty)
            PopulateField(currentRun, BodyProperty)
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
            d.Add(GenerateKey(ModelProperty), Model)
            d.Add(GenerateKey(ModelYearProperty), ModelYear)
            d.Add(GenerateKey(MakeProperty), Make)
            d.Add(GenerateKey(VINProperty), VIN)
            d.Add(GenerateKey(OdometerProperty), Odometer)
            d.Add(GenerateKey(BodyProperty), Body)
        End Sub
        Public Overrides Function OnGenerateKey(friendlyName As String) As String
            Return VehicleArgs.KeyParent(VehicleType, VehicleIndex) & friendlyName
        End Function
#End Region

#Region "  Business Rules "
        Protected Overrides Sub AddBusinessRules()
            MyBase.AddBusinessRules()
        End Sub
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
#End Region
        'Inheritance Worker...
        Private Shared Function RegisterPropertyLocal(Of T)(propertyLambdaExpression As Linq.Expressions.Expression(Of Func(Of BaseVehicle, T)), friendlyName As String, defaultValue As T) As PropertyInfo(Of T)
            Dim reflectedPropertyInfo = Reflection.Reflect(Of BaseVehicle).GetProperty(propertyLambdaExpression)
            Return RegisterProperty(GetType(BaseVehicle), Csla.Core.FieldManager.PropertyInfoFactory.Factory.Create(Of T)(GetType(BaseVehicle), reflectedPropertyInfo.Name, friendlyName, defaultValue))
        End Function
    End Class
End Namespace