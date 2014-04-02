Imports Caliburn.Micro
Imports System.Reflection

Public Class FormPromptViewModel
    Inherits PropertyChangedBase

    Public Property TheHammer As PropertyChangedBase

    Public Sub New()
        Dim lastData As New Dictionary(Of String, Object)
        Dim dat As New Dictionary(Of String, Object)
        AssemblySource.Instance.Add(Assembly.LoadFile("C:\Projects\GitHubProjects\Work\3LAWMN12VW\3LAWMN12VW\bin\Debug\3LAWMN12VW.dll"))
        'Dim x As _3LAWMN12VW.PromptContentViewModel =
        TheHammer = New _3LAWMN12VW.PromptContentViewModel(lastData, dat) 'New _3LAWMN12VW.VWContractRequiredViewModel(x)

    End Sub

End Class
