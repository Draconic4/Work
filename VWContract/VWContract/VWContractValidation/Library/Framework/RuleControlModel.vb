Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows

Public Class RuleControlModel
    Implements INotifyPropertyChanged

    Private _isSelected As Boolean
    Private _invalidBranch As Boolean
    Private _vr As Rule
    Private _parent As RuleControlModel
    Private _children As ObservableCollection(Of RuleControlModel)

    Public ReadOnly Property BrokenRule As Visibility
        Get
            If _invalidBranch Then Return Visibility.Visible
            Return Visibility.Collapsed
        End Get
    End Property
    Public ReadOnly Property ValidSection As Visibility
        Get
            If Not _invalidBranch Then Return Visibility.Visible
            Return Visibility.Collapsed
        End Get
    End Property

    Public Property IsSelected As Boolean
        Get
            Return _isSelected
        End Get
        Set(value As Boolean)
            If value <> _isSelected Then
                _isSelected = value
                OnPropertyChanged("IsSelected")
            End If
        End Set
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _vr.Name
        End Get
    End Property
    Public ReadOnly Property Children As ObservableCollection(Of RuleControlModel)
        Get
            Return _children
        End Get
    End Property

    Public Sub New(ByVal vr As Rule, ByVal parent As RuleControlModel)
        _vr = vr
        _invalidBranch = _vr.BrokenRule
        _parent = parent
        Dim l = (From r In _vr.Rules
                Select New RuleControlModel(r, Me)).ToList
        _children = New ObservableCollection(Of RuleControlModel)(l)

        If _invalidBranch Then
            Dim p As RuleControlModel = _parent
            While p IsNot Nothing AndAlso Not p._invalidBranch 'Recurse until we get to invalid branching
                p._invalidBranch = True
                p = p._parent
            End While
        End If
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    Public Sub OnPropertyChanged(ByVal prop As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(prop))
    End Sub
End Class
