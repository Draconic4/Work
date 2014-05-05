Namespace ValidationLib
    Public Class LeaseBalloonFields
        Inherits BaseFormField
        Private _IsActive As Boolean

        Public ReadOnly Property Active As Boolean
            Get
                Return _IsActive
            End Get
        End Property

        Public Sub New()
            MyBase.New(Nothing)
        End Sub
        Public Overrides Sub ReplicateCurrentState(d As Dictionary(Of String, Object))

        End Sub

        Public Overrides Sub Requirement(previousData As Object, validationRoot As Rule)

        End Sub
    End Class
End Namespace
