Imports Csla

Namespace ValidationRuleData
    Public Class Vehicle
        Inherits BusinessBase(Of Vehicle)

        Private _globalProperties As ProcessInfo

        'Global Business Rule Dependency
        Public Shared ReadOnly GlobalRuleProperty As PropertyInfo(Of ProcessInfo) = RegisterProperty(Of ProcessInfo)(Function(c) (c.GlobalProperty), RelationshipTypes.PrivateField)
        Public Shared ReadOnly ModelProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Model), "_MODEL", String.Empty)
        Public Shared ReadOnly ModelYearProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Year), "_YEAR", String.Empty)
        Public Shared ReadOnly MakeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Make), "_MAKE", String.Empty)
        Public Shared ReadOnly SaleClassProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.SaleClass), "_STATUS", String.Empty)
        Public Shared ReadOnly ConditionProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.Condition), "_ADDRESS", String.Empty)
        Public Shared ReadOnly IsCertifiedProperty As PropertyInfo(Of Boolean) = RegisterProperty(Of Boolean)(Function(c) (c.IsCertified), "_ISCERTIFIED", False)
        Public Shared ReadOnly VINProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.VIN), "_VIN", String.Empty)
        Public Shared ReadOnly DeliveredMileageProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.DeliveredMileage), "DEAL_ODOMETER", String.Empty)
        Public Shared ReadOnly OdometerMeasureProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.OdometerMeasure))
        Public Shared ReadOnly BodyStyleProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.BodyStyle), "_BODY", String.Empty)
        Public Shared ReadOnly ExteriorColorProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.ExteriorColor), "_COLOR", String.Empty)
        Public Shared ReadOnly TransmissionTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.TransmissionType), "_TRANSMISSION", String.Empty)
        Public Shared ReadOnly LicenseNumberProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.LicenseNumber), "_LICENSE", String.Empty)
        Public Shared ReadOnly OptionListProperty As PropertyInfo(Of OptionList) = RegisterProperty(Of OptionList)(Function(c) (c.Options), "_OPTION")
        Public Shared ReadOnly VehiclePriceProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) (c.VehiclePrice), "_PRICE", 0D)
        Public Shared ReadOnly GrossCapCostProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) (c.GrossCapCost), "_GROSSCAPCOST", 0D)
        Public Shared ReadOnly NetCapCostProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) (c.NetCapCost), "_NETCAPCOST", 0D)
        Public Shared ReadOnly TaxableSellPriceProperty As PropertyInfo(Of Decimal) = RegisterProperty(Of Decimal)(Function(c) (c.TaxableSellPrice), "_TAXABLESELLPRICE", 0D)
        Public Shared ReadOnly NumberOfEngineCylindersProperty As PropertyInfo(Of Integer) = RegisterProperty(Of Integer)(Function(c) (c.NumberOfCylinders), "_CYLINDERS", 0)
        Public Shared ReadOnly AuctionIndicatorProperty As PropertyInfo(Of Boolean) = RegisterProperty(Of Boolean)(Function(c) (c.MarkAuctioned), "_WASAUCTIONED", False)
        Public Shared ReadOnly VehicleWeightProperty As PropertyInfo(Of Integer) = RegisterProperty(Of Integer)(Function(c) (c.VehicleWeight), "_WEIGHT", 0)
        Public Shared ReadOnly VehicleUseProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.VehicleUse), "_USAGE", "PERSONAL")
        Public Shared ReadOnly EngineTypeProperty As PropertyInfo(Of String) = RegisterProperty(Of String)(Function(c) (c.EngineType), "_ENGINETYPE", "GAS")

#Region "  Properties "
        Public ReadOnly Property GlobalProperty As ProcessInfo
            Get
                Return _globalProperties
            End Get
        End Property
        Public ReadOnly Property Model As String
            Get
                Return GetProperty(ModelProperty)
            End Get
        End Property
        Public ReadOnly Property Year As String
            Get
                Return GetProperty(ModelYearProperty)
            End Get
        End Property
        Public ReadOnly Property Make As String
            Get
                Return GetProperty(MakeProperty)
            End Get
        End Property
        Public ReadOnly Property SaleClass As String
            Get
                Return GetProperty(SaleClassProperty)
            End Get
        End Property
        Public ReadOnly Property Condition As String
            Get
                Return GetProperty(ConditionProperty)
            End Get
        End Property
        Public ReadOnly Property IsCertified As Boolean
            Get
                Return GetProperty(IsCertifiedProperty)
            End Get
        End Property
        Public ReadOnly Property VIN As String
            Get
                Return GetProperty(VINProperty)
            End Get
        End Property
        Public ReadOnly Property DeliveredMileage As String
            Get
                Return GetProperty(DeliveredMileageProperty)
            End Get
        End Property
        Public ReadOnly Property OdometerMeasure As String
            Get
                Return GetProperty(OdometerMeasureProperty)
            End Get
        End Property
        Public ReadOnly Property BodyStyle As String
            Get
                Return GetProperty(BodyStyleProperty)
            End Get
        End Property
        Public ReadOnly Property ExteriorColor As String
            Get
                Return GetProperty(ExteriorColorProperty)
            End Get
        End Property
        Public Property TransmissionType As String
            Get
                Return GetProperty(TransmissionTypeProperty)
            End Get
            Set(value As String)
                SetProperty(TransmissionTypeProperty, value)
            End Set
        End Property
        Public ReadOnly Property LicenseNumber As String
            Get
                Return GetProperty(LicenseNumberProperty)
            End Get
        End Property
        Public ReadOnly Property Options As OptionList
            Get
                Return GetProperty(OptionListProperty)
            End Get
        End Property
        Public ReadOnly Property VehiclePrice As Decimal
            Get
                Return GetProperty(VehiclePriceProperty)
            End Get
        End Property
        Public Property GrossCapCost As Decimal
            Get
                Return GetProperty(GrossCapCostProperty)
            End Get
            Set(value As Decimal)
                SetProperty(GrossCapCostProperty, value)
            End Set
        End Property
        Public Property NetCapCost As Decimal
            Get
                Return GetProperty(NetCapCostProperty)
            End Get
            Set(value As Decimal)
                SetProperty(NetCapCostProperty, value)
            End Set
        End Property
        Public Property TaxableSellPrice As Decimal
            Get
                Return GetProperty(TaxableSellPriceProperty)
            End Get
            Set(value As Decimal)
                SetProperty(TaxableSellPriceProperty, value)
            End Set
        End Property
        Public Property NumberOfCylinders As Decimal
            Get
                Return GetProperty(NumberOfEngineCylindersProperty)
            End Get
            Set(value As Decimal)
                SetProperty(NumberOfEngineCylindersProperty, value)
            End Set
        End Property
        Public Property MarkAuctioned As Boolean
            Get
                Return GetProperty(AuctionIndicatorProperty)
            End Get
            Set(value As Boolean)
                SetProperty(AuctionIndicatorProperty, value)
            End Set
        End Property
        Public Property VehicleWeight As Decimal
            Get
                Return GetProperty(VehicleWeightProperty)
            End Get
            Set(value As Decimal)
                SetProperty(VehicleWeightProperty, value)
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
        Public Property EngineType As String
            Get
                Return GetProperty(EngineTypeProperty)
            End Get
            Set(value As String)
                SetProperty(EngineTypeProperty, value)
            End Set
        End Property
#End Region

#Region " Data Access "
        Public Sub New(ByVal gProp As ProcessInfo)
            _globalProperties = gProp
        End Sub
        Public Shared Function FetchExisting(formDC As Dictionary(Of String, Object), aristoDC As Dictionary(Of String, Object), globalProperty As ProcessInfo) As Vehicle
            Dim veh As Vehicle = New Vehicle(globalProperty)
            veh.Populate(formDC, aristoDC)
            Return veh
        End Function
        Public Sub Populate(ByVal p As Dictionary(Of String, Object), ByVal d As Dictionary(Of String, Object))

        End Sub
#End Region

#Region "  Business Rules "
        Public Sub CheckRules()

        End Sub
#End Region
    End Class
End Namespace
