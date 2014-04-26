Imports Caliburn.Micro

Public Class ContractPromptViewModel
    Inherits Screen

    Public _dataContext As VWCreditProcess
    Public Property VehicleUseVM As VehicleUseViewModel
    Dim _useGridVisilibity As Windows.Visibility
    Dim _useOtherGridVisibility As Windows.Visibility
    Dim _useBusinessGridVisibility As Windows.Visibility
    Dim _CL_AH_GridVisibility As Windows.Visibility
    Dim _VSI_Visibility As Windows.Visibility

    Public ReadOnly Property VehicleData As Vehicle
        Get
            Return _dataContext.Vehicle
        End Get
    End Property
    Public ReadOnly Property CL_AH_Insurance As Insurance
        Get
            Return _dataContext.InsuranceProducts
        End Get
    End Property
    Public ReadOnly Property VSI_Insurance As Protection
        Get
            Return _dataContext.Protection.B
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
            If Not VehicleUtility.IsOtherUse(VehicleData) OrElse VehicleUtility.IsPersonalUse(VehicleData) Then Return Windows.Visibility.Collapsed
            Return Windows.Visibility.Visible
        End Get
        Set(value As Windows.Visibility)
            _useBusinessGridVisibility = value
        End Set
    End Property
    Public Property CL_AH_GridVisibility As Windows.Visibility
        Get
            'If Not CL_AH_Insurance.IsSelected Then Return Windows.Visibility.Collapsed
            Return Windows.Visibility.Visible
        End Get
        Set(value As Windows.Visibility)
            _CL_AH_GridVisibility = value
        End Set
    End Property
    Public Property VSI_GridVisiblity As Windows.Visibility
        Get
            'If Not VSI_Insurance.HasValue Then Return Windows.Visibility.Collapsed
            Return Windows.Visibility.Visible
        End Get
        Set(value As Windows.Visibility)
            _VSI_Visibility = value
        End Set
    End Property

    Public Sub New(ByVal processInfo As VWCreditProcess)
        _dataContext = processInfo
        VehicleUseVM = New VehicleUseViewModel(processInfo.Vehicle)
    End Sub

End Class
