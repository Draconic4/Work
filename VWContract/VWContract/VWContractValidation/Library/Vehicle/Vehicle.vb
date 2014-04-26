Imports Csla

Public Class Vehicle
    Inherits BaseFormField

    Private C_VEHICLE As String = "VEH"
    Public Shared ReadOnly VehicleIndexProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.VehicleIndex), "", "0")
    Public Shared ReadOnly VehicleUseProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.VehicleUse), "_USAGE", VehicleUtility.C_VEHICLEUSE_PERSONAL)
    Public Shared ReadOnly OtherUseDescriptionProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.OtherUseDescription), "_USAGE_OTHERDESCRIPTION", String.Empty)
    Public Shared ReadOnly NumberDaysGraceProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer)(Function(c) (c.NumberDaysGrace), "_USAGE_BUSINESSGRACEPERIOD", 0)
    Public Shared ReadOnly LateChargeFlatProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal)(Function(c) (c.LateChargeFlat), "_USAGE_BUSINESS_LATECHARGE_FLAT", 0D)
    Public Shared ReadOnly LateChargeRateProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal)(Function(c) (c.LateChargeRate), "_USAGE_BUSINESS_LATECHARGE_RATE", 0D)

#Region "  Properties "
    Public Property VehicleIndex As Integer
        Get
            Return GetProperty(VehicleIndexProperty)
        End Get
        Set(value As Integer)
            SetProperty(VehicleIndexProperty, value)
        End Set
    End Property
    Public Property VehicleUse As String
        Get
            Return GetProperty(VehicleUseProperty)
        End Get
        Set(value As String)
            SetProperty(VehicleUseProperty, value)
        End Set
    End Property
    Property OtherUseDescription As String
        Get
            Return GetProperty(OtherUseDescriptionProperty)
        End Get
        Set(value As String)
            SetProperty(OtherUseDescriptionProperty, value)
        End Set
    End Property
    Property NumberDaysGrace As Integer
        Get
            Return GetProperty(NumberDaysGraceProperty)
        End Get
        Set(value As Integer)
            SetProperty(NumberDaysGraceProperty, value)
        End Set
    End Property
    Property LateChargeFlat As Decimal
        Get
            Return GetProperty(LateChargeFlatProperty)
        End Get
        Set(value As Decimal)
            SetProperty(LateChargeFlatProperty, value)
        End Set
    End Property
    Property LateChargeRate As Decimal
        Get
            Return GetProperty(LateChargeRateProperty)
        End Get
        Set(value As Decimal)
            SetProperty(LateChargeRateProperty, value)
        End Set
    End Property
#End Region

#Region "  Data Access "
    Private Sub New(vArgs As VehicleArgs)
        MyBase.New(vArgs.GlobalProperty)
    End Sub
    Public Shared Function Fetch(vArgs As VehicleArgs) As Vehicle
        Dim v As New Vehicle(vArgs)
        v.ReadDictionary(vArgs)
        Return v
    End Function
    Public Overrides Sub Populate(pRun As Dictionary(Of String, Object))
        PopulateField(pRun, VehicleUseProperty)
        PopulateField(pRun, OtherUseDescriptionProperty)
        PopulateField(pRun, NumberDaysGraceProperty)
        PopulateField(pRun, LateChargeFlatProperty)
        PopulateField(pRun, LateChargeRateProperty)
    End Sub
    Public Overrides Sub Calculate(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object))
        If ProcessUtility.IsBusiness(GlobalProperty) Then
            LoadProperty(VehicleUseProperty, VehicleUtility.C_VEHICLEUSE_BUSINESS)
        End If
    End Sub
    Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
        StoreField(VehicleUseProperty, d)
        StoreField(OtherUseDescriptionProperty, d)
        StoreField(NumberDaysGraceProperty, d)
        StoreField(LateChargeFlatProperty, d)
        StoreField(LateChargeRateProperty, d)
    End Sub
    Public Overrides Function OnGenerateKey(friendlyName As String) As String
        Return C_VEHICLE & VehicleIndex & friendlyName
    End Function
#End Region
#Region "  Business Rules "
    Public Overrides Sub Requirement(validationRoot As Rule)

    End Sub
#End Region

    'Inheritance Worker
    Private Shared Function RegisterPropertyLocal(Of T)(propertyLambdaExpression As System.Linq.Expressions.Expression(Of Func(Of Vehicle, Object)), friendlyName As String, defaultValue As T) As PropertyInfo(Of T)
        Dim reflectedPropertyInfo = Reflection.Reflect(Of Vehicle).GetProperty(propertyLambdaExpression)
        Return RegisterProperty(GetType(Vehicle), Csla.Core.FieldManager.PropertyInfoFactory.Factory.Create(Of T)(
                GetType(Vehicle),
                reflectedPropertyInfo.Name, friendlyName, defaultValue))
    End Function
End Class
