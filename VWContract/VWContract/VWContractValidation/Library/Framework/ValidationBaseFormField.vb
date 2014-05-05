Imports Csla

Namespace ValidationLib
    Public MustInherit Class ValidationBaseFormField
        Inherits BaseFormField
        Public validationFunc As Func(Of ProcessInfo, Boolean)
        Public ReadOnly Property IsActive As Boolean
            Get
                If validationFunc Is Nothing OrElse GlobalProperty Is Nothing Then Return False
                Return validationFunc.Invoke(GlobalProperty)
            End Get
        End Property
        Public Sub New(gProp As ProcessInfo)
            MyBase.New(gProp)
        End Sub
        Public Sub SetPropertyLocal(Of T)(pi As PropertyInfo(Of T), val As T)
            If validationFunc IsNot Nothing AndAlso GlobalProperty IsNot Nothing Then
                If validationFunc.Invoke(GlobalProperty) Then
                    LoadProperty(pi, val)
                End If
            End If
        End Sub
    End Class
End Namespace
