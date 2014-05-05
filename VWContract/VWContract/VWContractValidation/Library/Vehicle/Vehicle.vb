Imports Csla

Namespace ValidationLib
    Public Class Vehicle
        Inherits BaseFormField

        Public Shared ReadOnly BaseVehicleProperty As PropertyInfo(Of BaseVehicle) = RegisterPropertyLocal(Of BaseVehicle)(Function(c) (c.Base), "", Nothing)
        'USAGE
        Public Shared ReadOnly UseProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.Use), "_USAGE", VehicleUtility.C_VEHICLEUSE_PERSONAL)
        Public Shared ReadOnly OtherUseDescriptionProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.OtherUseDescription), "_USAGE_OTHERDESCRIPTION", String.Empty)
        Public Shared ReadOnly NumberDaysGraceProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer)(Function(c) (c.NumberDaysGrace), "_USAGE_BUSINESSGRACEPERIOD", 0)
        Public Shared ReadOnly LateChargeFlatProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal)(Function(c) (c.LateChargeFlat), "_USAGE_BUSINESS_LATECHARGE_FLAT", 0D)
        Public Shared ReadOnly LateChargeRateProperty As PropertyInfo(Of Decimal) = RegisterPropertyLocal(Of Decimal)(Function(c) (c.LateChargeRate), "_USAGE_BUSINESS_LATECHARGE_RATE", 0D)

        Public Shared ReadOnly ExteriorColorProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.ExteriorColor), "_COLOR", String.Empty)
        Public Shared ReadOnly NumberOfEngineCylindersProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer)(Function(c) (c.EngineCylinder), "_CYL", 0)
        Public Shared ReadOnly TransmissionProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.Transmission), "_TRANSMISSION", String.Empty)
        Public Shared ReadOnly WeightProperty As PropertyInfo(Of Integer) = RegisterPropertyLocal(Of Integer)(Function(c) (c.Weight), "_WEIGHT", 0)
        Public Shared ReadOnly LicenseNumberProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.LicenseNumber), "_LICENSE", String.Empty)
        Public Shared ReadOnly SaleClassProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.SaleClass), "_STATUS", "Vehicle")
        Public Shared ReadOnly CPOConditionProperty As PropertyInfo(Of Boolean) = RegisterPropertyLocal(Of Boolean)(Function(c) (c.CPOCondition), "_CPOCONDITION", False)
        Public Shared ReadOnly CertifiedPreOwnedProperty As PropertyInfo(Of Boolean) = RegisterPropertyLocal(Of Boolean)(Function(c) (c.CertifiedPreOwned), "_ISCERTIFIED", False)
        Public Shared ReadOnly VehicleUseProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.VehicleUse), "_VEHICLEUSE", "PERSONAL")
        Public Shared ReadOnly FuelTypeProperty As PropertyInfo(Of String) = RegisterPropertyLocal(Of String)(Function(c) (c.FuelType), "_FUELTYPE", "GAS")
        ' OPTIONS
        'Public Shared ReadOnly OptionListProperty As PropertyInfo(Of VehicleOptionCollection) = RegisterPropertyLocal(Of VehicleOptionCollection)(Function(c) (c.Options), "_OPTION", Nothing)
        ' PRICING
        'Public Shared ReadOnly VehiclePricingProperty As PropertyInfo(Of VehiclePricingManager) = RegisterPropertyLocal(Of VehiclePricingManager)(Function(c) (c.VehiclePricing), "_PRICING", Nothing)

#Region "  Properties "
        Public Property Base As BaseVehicle
            Get
                Return GetProperty(BaseVehicleProperty)
            End Get
            Set(value As BaseVehicle)
                SetProperty(BaseVehicleProperty, value)
            End Set
        End Property
        'USAGE
        Public Property Use As String
            Get
                Return GetProperty(UseProperty)
            End Get
            Set(value As String)
                SetProperty(UseProperty, value)
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
        Public Property ExteriorColor As String
            Get
                Return GetProperty(ExteriorColorProperty)
            End Get
            Set(value As String)
                SetProperty(ExteriorColorProperty, value)
            End Set
        End Property
        Public Property Transmission As String
            Get
                Return GetProperty(TransmissionProperty)
            End Get
            Set(value As String)
                SetProperty(TransmissionProperty, value)
            End Set
        End Property
        Public Property EngineCylinder As Integer
            Get
                Return GetProperty(NumberOfEngineCylindersProperty)
            End Get
            Set(value As Integer)
                SetProperty(NumberOfEngineCylindersProperty, value)
            End Set
        End Property
        Public Property Weight As Integer
            Get
                Return GetProperty(WeightProperty)
            End Get
            Set(value As Integer)
                SetProperty(WeightProperty, value)
            End Set
        End Property
        Public Property LicenseNumber As String
            Get
                Return GetProperty(LicenseNumberProperty)
            End Get
            Set(value As String)
                SetProperty(LicenseNumberProperty, value)
            End Set
        End Property
        Public Property SaleClass As String
            Get
                Return GetProperty(SaleClassProperty)
            End Get
            Set(value As String)
                SetProperty(SaleClassProperty, value)
            End Set
        End Property
        Public Property CPOCondition As Boolean
            Get
                Return GetProperty(CPOConditionProperty)
            End Get
            Set(value As Boolean)
                SetProperty(CPOConditionProperty, value)
            End Set
        End Property
        Public Property CertifiedPreOwned As Boolean
            Get
                Return GetProperty(CertifiedPreOwnedProperty)
            End Get
            Set(value As Boolean)
                SetProperty(CertifiedPreOwnedProperty, value)
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
        Public Property FuelType As String
            Get
                Return GetProperty(FuelTypeProperty)
            End Get
            Set(value As String)
                SetProperty(FuelTypeProperty, value)
            End Set
        End Property
        'Public Property Options As VehicleOptionCollection
        '    Get
        '        Return GetProperty(OptionListProperty)
        '    End Get
        '    Set(value As VehicleOptionCollection)
        '        SetProperty(OptionListProperty, value)
        '    End Set
        'End Property
        'Public Property VehiclePricing As VehiclePricingManager
        '    Get
        '        Return GetProperty(VehiclePricingProperty)
        '    End Get
        '    Set(value As VehiclePricingManager)
        '        SetProperty(VehiclePricingProperty, value)
        '    End Set
        'End Property
#End Region

#Region "  Data Access "
        Private Sub New(vArgs As VehicleArgs)
            MyBase.New(vArgs.GlobalProperty)
            LoadProperty(BaseVehicleProperty, BaseVehicle.Fetch(vArgs))
        End Sub
        Public Shared Function Fetch(vArgs As VehicleArgs) As Vehicle
            Dim v As New Vehicle(vArgs)
            v.ReadDictionary(vArgs)
            Return v
        End Function
        Public Overrides Sub Populate(pRun As Dictionary(Of String, Object))
            PopulateField(pRun, UseProperty)
            PopulateField(pRun, OtherUseDescriptionProperty)
            PopulateField(pRun, NumberDaysGraceProperty)
            PopulateField(pRun, LateChargeFlatProperty)
            PopulateField(pRun, LateChargeRateProperty)
        End Sub
        Public Overrides Sub PopulateOverride(currentRun As Dictionary(Of String, Object))
            PopulateField(currentRun, ExteriorColorProperty)
            PopulateField(currentRun, NumberOfEngineCylindersProperty)
            PopulateField(currentRun, TransmissionProperty)
            PopulateField(currentRun, WeightProperty)
            PopulateField(currentRun, LicenseNumberProperty)
            PopulateField(currentRun, SaleClassProperty)
            PopulateField(currentRun, CPOConditionProperty)
            PopulateField(currentRun, CertifiedPreOwnedProperty)
            PopulateField(currentRun, FuelTypeProperty)
            'OPTIONS/VEHICLE PRICING
        End Sub
        Public Overrides Sub Calculate(previousRun As Dictionary(Of String, Object), currentRun As Dictionary(Of String, Object))
            If ProcessUtility.IsBusiness(GlobalProperty) Then
                LoadProperty(UseProperty, VehicleUtility.C_VEHICLEUSE_BUSINESS)
            End If
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))
            Base.ReplicateCurrentState(d)
            StoreField(UseProperty, d)
            StoreField(OtherUseDescriptionProperty, d)
            StoreField(NumberDaysGraceProperty, d)
            StoreField(LateChargeFlatProperty, d)
            StoreField(LateChargeRateProperty, d)
            StoreField(ExteriorColorProperty, d)
            StoreField(NumberOfEngineCylindersProperty, d)
            StoreField(TransmissionProperty, d)
            StoreField(WeightProperty, d)
            StoreField(LicenseNumberProperty, d)
            StoreField(SaleClassProperty, d)
            StoreField(CPOConditionProperty, d)
            StoreField(CertifiedPreOwnedProperty, d)
            StoreField(FuelTypeProperty, d)
            'OPTIONS/VEHICLE PRICING
        End Sub
        Public Overrides Function OnGenerateKey(friendlyName As String) As String
            Return VehicleArgs.KeyParent(Base.VehicleType, Base.VehicleIndex) & friendlyName
        End Function
#End Region
#Region "  Business Rules "
        Public Overrides Sub CheckRules()
            Base.CheckRules()
            MyBase.CheckRules()
            'OPTIONS/VEHICLE PRICING
        End Sub
        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

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
End Namespace
