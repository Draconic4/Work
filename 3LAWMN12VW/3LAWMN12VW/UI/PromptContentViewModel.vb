Imports Caliburn.Micro
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Public Class PromptContentViewModel
    Inherits Conductor(Of Screen).Collection.AllActive
    Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage), IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage), IHandle(Of ApplicationTypeChanged)

    Public Const C_Requirement As String = "Requirement"
    Public Const C_PrimaryApplicant As String = "PrimaryApplicant"
    Public Const C_BusinessApplicant As String = "BusinessApplicant"
    Public Const C_CoApplicant As String = "CoApplicant"
    Public Const C_Vehicle As String = "Vehicle"
    Public Const C_Finance As String = "Finance"
    Public Const C_Lease As String = "Lease"
    Public Const C_TestTab As String = "TestTab"

    Private _conduct As New Dictionary(Of String, Integer)
    Private _currentDisplay As Screen

    Private _dataContext As ValidationRuleData.VWCreditProcess

    Private ReadOnly _eventAggregator As IEventAggregator

#Region "  Properties "
    Public Property CurrentDisplay As Screen
        Get
            Return _currentDisplay
        End Get
        Set(value As Screen)
            _currentDisplay = value
        End Set
    End Property
    Public Property DataContext As ValidationRuleData.VWCreditProcess
        Get
            Return _dataContext
        End Get
        Set(value As ValidationRuleData.VWCreditProcess)
            _dataContext = value
        End Set
    End Property
    Public ReadOnly Property RequirementChecked() As Boolean
        Get
            Return Me.CurrentDisplay.GetType() Is GetType(VWContractRequiredViewModel)
        End Get
    End Property
    Public ReadOnly Property PrimaryApplicantChecked() As Boolean
        Get
            Return Me.CurrentDisplay.GetType() Is GetType(VWContractPrimaryApplicantViewModel)
        End Get
    End Property
    'Public ReadOnly Property BusinessApplicantChecked() As Boolean
    '    Get
    '        Return Me.CurrentDisplay.GetType() Is GetType(VWContractBusinessApplicantViewModel)
    '    End Get
    'End Property
    Public ReadOnly Property CoApplicantChecked() As Boolean
        Get
            Return Me.CurrentDisplay.GetType() Is GetType(VWContractCoApplicantViewModel)
        End Get
    End Property
    'Public ReadOnly Property FinanceChecked() As Boolean
    '    Get
    '        Return Me.CurrentDisplay.GetType() Is GetType(VWContractFinanceViewModel)
    '    End Get
    'End Property
    'Public ReadOnly Property LeaseChecked() As Boolean
    '    Get
    '        Return Me.CurrentDisplay.GetType() Is GetType(VWContractLeaseViewModel)
    '    End Get
    'End Property
    Public ReadOnly Property VehicleChecked() As Boolean
        Get
            Return Me.CurrentDisplay.GetType() Is GetType(VWContractVehicleViewModel)
        End Get
    End Property
#End Region

    'Events
    Public Sub RequirementClicked()
        ChangeView(_conduct(C_Requirement))
    End Sub
    Public Sub PrimaryApplicantClicked()
        ChangeView(_conduct("PrimaryApplicant"))
    End Sub
    Public Sub VehicleClicked()
        ChangeView(_conduct("Vehicle"))
    End Sub

    Private Sub ChangeView(ByVal newIdx As Integer)
        Me.CurrentDisplay = Items(newIdx)
        Me.NotifyOfPropertyChange("RequirementChecked")
        'Me.NotifyOfPropertyChange("PrimaryApplicantChecked")
        Me.NotifyOfPropertyChange("VehicleChecked")
        Me.NotifyOfPropertyChange("TestChecked")
    End Sub
    Public Sub New()
    End Sub
    Public Sub New(ByVal previousDC As Dictionary(Of String, Object), ByVal arDC As Dictionary(Of String, Object), eventAggregator As IEventAggregator)
        _eventAggregator = eventAggregator
        _dataContext = ValidationRuleData.VWCreditProcess.Fetch(previousDC, arDC)
        GenerateScreens()
        _eventAggregator.Subscribe(Me)
    End Sub

    Public Sub Handle_BeginValidationMessage(message As PBS.Deals.FormsIntegration.BeginValidationMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginValidationMessage).Handle
        _dataContext.CheckRules()
    End Sub

    Public Sub Handle_BeginDataCollectMessage(message As PBS.Deals.FormsIntegration.BeginDataCollectMessage) Implements IHandle(Of PBS.Deals.FormsIntegration.BeginDataCollectMessage).Handle
    End Sub

    Public Sub Handle_ApplicationTypeChanged(message As ApplicationTypeChanged) Implements IHandle(Of ApplicationTypeChanged).Handle
        NotifyOfPropertyChange("")
    End Sub

    Public Sub GenerateScreens()
        Items.Clear()
        GenerateScreen(New VWContractRequiredViewModel(Me, _eventAggregator), "Requirement")
        GenerateScreen(New VWContractPrimaryApplicantViewModel(Me), "PrimaryApplicant")
        GenerateScreen(New VWContractVehicleViewModel(Me, _eventAggregator), "Vehicle")
        ChangeView(_conduct("Requirement"))
    End Sub

    Public Sub GenerateScreen(ByVal screen As Screen, ByVal desc As String)
        Items.Add(screen)
        _conduct.Add(desc, Items.Count - 1)
    End Sub
End Class
