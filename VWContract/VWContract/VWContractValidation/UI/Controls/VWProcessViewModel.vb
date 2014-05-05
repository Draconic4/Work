Imports Caliburn.Micro

Public Class VWProcessViewModel
    Inherits Conductor(Of Screen).Collection.OneActive

    Private _processInfoVM As ProcessInfoViewModel
    Private _applicantVM As ApplicantManagerViewModel
    Private _vehicleVM As VehicleUseViewModel
    'Private _dealVM As DealViewModel
#Region "  Properties "
    Public ReadOnly Property ProcessInfoVM As ProcessInfoViewModel
        Get
            Return _processInfoVM
        End Get
    End Property
    Public ReadOnly Property ApplicantManagerVM As ApplicantManagerViewModel
        Get
            Return _applicantVM
        End Get
    End Property
    Public ReadOnly Property VehicleUseVM As VehicleUseViewModel
        Get
            Return _vehicleVM
        End Get
    End Property
    Public ReadOnly Property ProcessInfoVMChecked As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ProcessInfoViewModel)
        End Get
    End Property
    Public ReadOnly Property ApplicantManagerVMChecked As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(ApplicantManagerViewModel)
        End Get
    End Property
    Public ReadOnly Property VehicleUseVMChecked As Boolean
        Get
            Return Me.ActiveItem.GetType() Is GetType(VehicleUseViewModel)
        End Get
    End Property
#End Region
#Region "  Events "
    Public Sub ProcessInfoVMClicked()
        ChangeView(_processInfoVM)
    End Sub
    Public Sub ApplicantManagerVMClicked()
        ChangeView(_applicantVM)
    End Sub
    Public Sub VehicleUseVMClicked()
        ChangeView(_vehicleVM)
    End Sub
#End Region
    Private Sub ChangeView(ByVal newView As Screen)
        Me.ActiveItem = newView
        Me.NotifyOfPropertyChange("ProcessInfoVMChecked")
        Me.NotifyOfPropertyChange("ApplicantManagerVMChecked")
        Me.NotifyOfPropertyChange("VehicleUseVMChecked")
    End Sub
    Public Sub New(ByVal dataContext As ValidationLib.VWCreditProcess)
        _processInfoVM = New ProcessInfoViewModel(dataContext)
        _applicantVM = New ApplicantManagerViewModel(dataContext)
        _vehicleVM = New VehicleUseViewModel(dataContext)
        ChangeView(_processInfoVM)
    End Sub
End Class
