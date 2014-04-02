Imports Csla

Public Class Applicant
    Inherits BusinessBase(Of Applicant)

    Public Shared ReadOnly ApplicantNameProperty As PropertyInfo(Of ApplicantID) = RegisterProperty(Of ApplicantID)(Function(c) c.ApplicantName)

#Region "  Properties "
    Public Property ApplicantName() As ApplicantID
        Get
            Return GetProperty(ApplicantNameProperty)
        End Get
        Set(ByVal value As ApplicantID)
            SetProperty(ApplicantNameProperty, value)
        End Set
    End Property
#End Region

#Region "  Data Access "

#End Region

End Class
