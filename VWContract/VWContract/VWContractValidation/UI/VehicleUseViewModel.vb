Imports Caliburn.Micro

Public Class VehicleUseViewModel
    Inherits Screen

    Public _vehicle As Vehicle

    Public ReadOnly Property VehicleData As Vehicle
        Get
            Return _vehicle
        End Get
    End Property

    Dim _useGridVisilibity As Windows.Visibility
    Dim _useOtherGridVisibility As Windows.Visibility
    Dim _useBusinessGridVisibility As Windows.Visibility

    Public ReadOnly Property UseTypes As List(Of String)
        Get
            Return New List(Of String) From {VehicleUtility.C_VEHICLEUSE_PERSONAL, VehicleUtility.C_VEHICLEUSE_BUSINESS, VehicleUtility.C_VEHICLEUSE_AGRICULTURAL, VehicleUtility.C_VEHICLEUSE_OTHER}
        End Get
    End Property

    Public Property UseGridVisibility As Windows.Visibility
        Get
            If VehicleUtility.IsPersonalUse(VehicleData) Then Return Windows.Visibility.Collapsed
            Return Windows.Visibility.Visible
        End Get
        Set(value As Windows.Visibility)
            _useGridVisilibity = value
        End Set
    End Property
    Public Property OtherUseVisibility As Windows.Visibility
        Get
            If Not VehicleUtility.IsOtherUse(VehicleData) Then Return Windows.Visibility.Collapsed
            Return Windows.Visibility.Visible
        End Get
        Set(value As Windows.Visibility)
            _useOtherGridVisibility = value
        End Set
    End Property
    Public Property BusinessAgricultureUseVisibility As Windows.Visibility
        Get
            If VehicleUtility.IsOtherUse(VehicleData) OrElse VehicleUtility.IsPersonalUse(VehicleData) Then Return Windows.Visibility.Collapsed
            Return Windows.Visibility.Visible
        End Get
        Set(value As Windows.Visibility)
            _useBusinessGridVisibility = value
        End Set
    End Property

    Public Sub VehicleUseChanged(itm As String)
        NotifyOfPropertyChange("UseGridVisibility")
        NotifyOfPropertyChange("OtherUseVisibility")
        NotifyOfPropertyChange("BusinessAgricultureUseVisibility")
    End Sub

    Public Sub New(ByVal veh As Vehicle)
        _vehicle = veh
    End Sub
End Class
