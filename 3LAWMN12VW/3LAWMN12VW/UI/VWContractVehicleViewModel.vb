Imports Caliburn.Micro

Public Class VWContractVehicleViewModel
    Inherits Screen

    Private _View As PromptContentViewModel 'Parent View
    Private _Vehicle As VWContractCreditVehicleViewModel 'Pass Through View
    Private Property _globalProperty As ValidationRuleData.ProcessInfo 'Global Processing Information

    Private ReadOnly Property GlobalProperty() As ValidationRuleData.ProcessInfo
        Get
            Return _globalProperty
        End Get
    End Property
    Public ReadOnly Property Vehicle
        Get
            Return _Vehicle
        End Get
    End Property
    Public Sub New(ByVal view As PromptContentViewModel, evA As IEventAggregator)
        _View = view
        _Vehicle = New VWContractCreditVehicleViewModel(view.DataContext.Vehicle, view.DataContext.GlobalProperty)
    End Sub
    Public Sub Validate()
        If Utility.IsBusiness(GlobalProperty) Then Return
        _Vehicle.Validate()
        NotifyOfPropertyChange("")
    End Sub
End Class
