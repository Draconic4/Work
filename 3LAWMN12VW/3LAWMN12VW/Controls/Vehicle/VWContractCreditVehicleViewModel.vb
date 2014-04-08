Public Class VWContractCreditVehicleViewModel
    Inherits Caliburn.Micro.Screen

    Private _globalProperty As ValidationRuleData.ProcessInfo
    Private _Vehicle As ValidationRuleData.Vehicle

    Public ReadOnly Property Vehicle As ValidationRuleData.Vehicle
        Get
            Return _Vehicle
        End Get
    End Property
    'Public ReadOnly Property CapCostVisibility As Windows.Visibility
    '    Get
    '        If _Contract.XML.DataArea.CreditContract.Header.FinanceType = 2 Then
    '            Return Windows.Visibility.Visible
    '        Else
    '            Return Windows.Visibility.Hidden
    '        End If
    '    End Get
    'End Property
    'Public ReadOnly Property NetCapCost As String
    '    Get
    '        If _Vehicle.Pricing IsNot Nothing Then
    '            For Each p As ProcessCreditContractXML.VehiclePricingNode In _Vehicle.Pricing
    '                If p.VehiclePricingType = "Net Cap Cost" Then Return p.VehiclePrice.Price
    '            Next
    '        End If

    '        Return ""
    '    End Get
    'End Property
    'Public ReadOnly Property GrossCapCost As String
    '    Get
    '        If _Vehicle.Pricing IsNot Nothing Then
    '            For Each p As ProcessCreditContractXML.VehiclePricingNode In _Vehicle.Pricing
    '                If p.VehiclePricingType = "Gross Cap Cost" Then Return p.VehiclePrice.Price
    '            Next
    '        End If
    '        Return ""
    '    End Get
    'End Property
    'Public Property AuctionIndicator As Boolean
    '    Get
    '        If _Vehicle.AuctionInd IsNot Nothing AndAlso _Vehicle.AuctionInd = "1" Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Get
    '    Set(ByVal value As Boolean)
    '        If value Then
    '            _Vehicle.AuctionInd = "1"
    '        Else
    '            _Vehicle.AuctionInd = "0"
    '        End If
    '    End Set
    'End Property
    'Public ReadOnly Property PreOwnedIndicator As Boolean
    '    Get
    '        If _Vehicle.CertifiedPrownedInd IsNot Nothing AndAlso _Vehicle.CertifiedPrownedInd = "1" Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Get
    'End Property
    Public ReadOnly Property VehicleUseList As List(Of String)
        Get
            Dim result As New List(Of String)
            result.Add("Personal")
            result.Add("Business")
            result.Add("Agricultural")
            If Not Utility.IsCanadian(_globalProperty) Then
                result.Add("Commercial")
                result.Add("Other")
            End If

            Return result
        End Get
    End Property
    Public ReadOnly Property EngineTypeList As List(Of String)
        Get
            Dim result As New List(Of String)
            result.Add("Diesel")
            result.Add("Electric")
            result.Add("Gas")
            result.Add("Hybrid")
            result.Add("Hydrogen")
            result.Add("Natural Gas")
            result.Add("Other")
            Return result
        End Get
    End Property
    Public ReadOnly Property TransmissionTypeList As List(Of String)
        Get
            Dim result As New List(Of String)
            result.Add("3")
            result.Add("4")
            result.Add("5")
            result.Add("6")
            result.Add("A")
            Return result
        End Get
    End Property
    Public Sub New(ByVal veh As ValidationRuleData.Vehicle, gProp As ValidationRuleData.ProcessInfo)
        If veh Is Nothing Then Exit Sub
        _Vehicle = veh
        _globalProperty = gProp
    End Sub
    Public Sub Validate()
    End Sub
End Class
