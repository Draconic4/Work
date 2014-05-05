Namespace ValidationLib
    Public Class ApplicantArgs
        Inherits BaseConstructionArgs

        Enum ApplicantTypes
            BUYER1
            BUYER2
            BUYER3
            BUYER4
            BUYER5
            COUNT
        End Enum

        Public applicantType As ApplicantTypes

        Public Shared Function KeyParent(ByVal index As ApplicantTypes, ByVal gProp As ProcessInfo) As String
            Select Case index
                Case ApplicantTypes.BUYER1
                    Return "BUY"
                Case ApplicantTypes.BUYER2
                    Return "COBUYER1"
                Case ApplicantTypes.BUYER3
                    Return "COBUYER2"
                Case ApplicantTypes.BUYER4
                    Return "COBUYER3"
                Case ApplicantTypes.BUYER5
                    Return "COBUYER4"
                Case Else
                    Return "BUY"
            End Select
        End Function
    End Class
End Namespace
