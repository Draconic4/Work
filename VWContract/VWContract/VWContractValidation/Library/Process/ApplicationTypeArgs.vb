Public Class ApplicationTypeArgs
    Private _hasCoApplicantOrGuarantor As Boolean = False
    Private _hasCoApplicant2OrCoApplicantAndAlsoGuarantor = False

    Property IsBusiness As Boolean
    Property HasCoApplicantOrGuarantor As Boolean
        Get
            Return _hasCoApplicantOrGuarantor
        End Get
        Set(value As Boolean)
            If value <> _hasCoApplicantOrGuarantor Then 'Lock Step hasCoApp and hasCoApp2
                If _hasCoApplicantOrGuarantor Then HasCoApplicant2OrCoApplicantAndAlsoGuarantor = False
                _hasCoApplicantOrGuarantor = value
            End If
        End Set
    End Property
    Property HasCoApplicant2OrCoApplicantAndAlsoGuarantor As Boolean
        Get
            Return _hasCoApplicant2OrCoApplicantAndAlsoGuarantor
        End Get
        Set(value As Boolean)
            If _hasCoApplicantOrGuarantor Then
                _hasCoApplicant2OrCoApplicantAndAlsoGuarantor = value
            End If
        End Set
    End Property

    Public Sub New(ByVal isBusiness As Boolean, ByVal hasCoApplicantOrGuarantor As Boolean, ByVal hasCoApplicant2OrCoApplicantAndAlsoGuarantor As Boolean)
        Me.IsBusiness = isBusiness
        Me._hasCoApplicantOrGuarantor = hasCoApplicantOrGuarantor 'Lock Step hasCoApp and hasCoApp2.
        If hasCoApplicantOrGuarantor Then
            _hasCoApplicant2OrCoApplicantAndAlsoGuarantor = hasCoApplicant2OrCoApplicantAndAlsoGuarantor
        End If
    End Sub
End Class