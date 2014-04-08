Imports Caliburn.Micro
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.OneActive
    Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage), IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage), IHandle(Of ApplicationTypeChanged)

    Private ReadOnly _eventAggregator As IEventAggregator
    Private _menuOptions As List(Of ToggleButtonViewModel)

    Private _dataContext As ValidationRuleData.VWCreditProcess
    Public Property DataContext As ValidationRuleData.VWCreditProcess
        Get
            Return _dataContext
        End Get
        Set(value As ValidationRuleData.VWCreditProcess)
            _dataContext = value
        End Set
    End Property

    Private _detailView As VWContractRequiredViewModel
    Public Sub RequirementClicked()
        ChangeView(_detailView)
    End Sub
    Public ReadOnly Property RequirementChecked()
        Get
            Return Me.ActiveItem.GetType() Is GetType(VWContractRequiredViewModel)
        End Get
    End Property

    Private _primaryApplicant As VWContractPrimaryApplicantViewModel
    Public ReadOnly Property PrimaryApplicantVisibility As Visibility
        Get
            If Utility.IsBusiness(_dataContext.GlobalProperty) Then Return Visibility.Collapsed
            Return Visibility.Visible
        End Get
    End Property
    Public Sub PrimaryApplicantClicked()
        ChangeView(_primaryApplicant)
    End Sub
    Public ReadOnly Property PrimaryApplicantChecked()
        Get
            Return Me.ActiveItem.GetType() Is GetType(VWContractPrimaryApplicantViewModel)
        End Get
    End Property

    Private _vehicleView As VWContractVehicleViewModel
    Public Sub VehicleClicked()
        ChangeView(_vehicleView)
    End Sub
    Public ReadOnly Property VehicleChecked()
        Get
            Return Me.ActiveItem.GetType() Is GetType(VWContractVehicleViewModel)
        End Get
    End Property
    Private Sub ChangeView(ByVal newView As Screen)
        'If Me.ActiveItem Is VWContractRequiredViewModel Then
        '    'Warn and require user to select these options.
        'End If
        Me.ActiveItem = newView
        Me.NotifyOfPropertyChange("RequirementChecked")
        Me.NotifyOfPropertyChange("PrimaryApplicantChecked")
        Me.NotifyOfPropertyChange("VehicleChecked")
        'Me.NotifyOfPropertyChange("TestChecked")
    End Sub
    Public Sub New()
    End Sub
    Public Sub New(ByVal previousDC As Dictionary(Of String, Object), ByVal arDC As Dictionary(Of String, Object), eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        _dataContext = ValidationRuleData.VWCreditProcess.FetchExisting(previousDC, arDC)
        '_menuOptions = GenerateButtonMenu()
        _detailView = New VWContractRequiredViewModel(Me, _eventAggregator)
        _primaryApplicant = New VWContractPrimaryApplicantViewModel(Me)
        _vehicleView = New VWContractVehicleViewModel(Me, _eventAggregator)
        '_testTab = New TestTabViewModel(Me)
        _eventAggregator.Subscribe(Me)
        Me.ActiveItem = _detailView
    End Sub

    Public Sub Handle_BeginValidationMessage(message As PBS.Deals.FormsIntegration.BeginValidationMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage).Handle
        _dataContext.CheckRules()
    End Sub

    Public Sub Handle_BeginDataCollectMessage(message As PBS.Deals.FormsIntegration.BeginDataCollectMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage).Handle
    End Sub

    Public Sub Handle_ApplicationTypeChanged(message As ApplicationTypeChanged) Implements IHandle(Of ApplicationTypeChanged).Handle
        NotifyOfPropertyChange("")
    End Sub

    'Public Function GenerateButtonMenu() As List(Of ToggleButtonViewModel)
    '    Dim l As New List(Of ToggleButtonViewModel)
    '    l.Add(New ToggleButtonViewModel With {.ButtonText = "Applicant", .EventFunc = AddressOf RequirementChecked, .ActionFunc = ""}

    'End Function

    'Private _testTab As TestTabViewModel

    'Public ReadOnly Property BusinessApplicantVisibility As Visibility
    '    Get
    '        If Not Utility.IsBusiness(_dataContext.GlobalProperty) Then Return Visibility.Collapsed
    '        Return Visibility.Visible
    '    End Get
    'End Property
    'Public ReadOnly Property CoApplicantVisibility As Visibility
    '    Get
    '        If Utility.HasCoApplicant(_dataContext.GlobalProperty) AndAlso _dataContext.ApplicantMgr IsNot Nothing AndAlso _dataContext.ApplicantMgr.HasCoApplicant Then Return Visibility.Visible
    '        Return Visibility.Collapsed
    '    End Get
    'End Property
    'Public Sub TestClicked()
    '    ChangeView(_testTab)
    'End Sub
    'Public ReadOnly Property TestChecked()
    '    Get
    '        Return Me.ActiveItem.GetType() Is GetType(TestTabViewModel)
    '    End Get
    'End Property
End Class

Public Class ToggleButtonViewModel
    Public Property ButtonText As String
    Public Property EventFunc As Action(Of Object)
    Public Property ActionFunc As String
End Class
