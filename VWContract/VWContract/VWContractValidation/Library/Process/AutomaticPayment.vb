Namespace ValidationLib
    Public Class AutomaticPayment
        Inherits BaseFormField

        Public Property TransitRoutingNumber As String
            Get
                Return ""
            End Get
            Set(value As String)

            End Set
        End Property
        Public Property AccountID As String
            Get
                Return ""
            End Get
            Set(value As String)

            End Set
        End Property
        Public Property AccountType As String
            Get
                Return ""
            End Get
            Set(value As String)

            End Set
        End Property
        Public Property TransferFrequency As String
            Get
                Return ""
            End Get
            Set(value As String)

            End Set
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
