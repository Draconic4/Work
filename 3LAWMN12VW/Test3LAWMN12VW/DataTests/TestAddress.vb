Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports _3LAWMN12VW

<TestClass()> Public Class TestAddress

    Public Shared Function GenerateAristoTestData() As Dictionary(Of String, Object)

        Dim d As New Dictionary(Of String, Object)
        ''Process Info
        'd.Add("", )
        'd.Add("", )
        'd.Add("", )

        ''Address Info
        'd.Add("", )
        'd.Add("", )
        'd.Add("", )
        'd.Add("", )
        'd.Add("", )
        Return d
    End Function

    <TestMethod()> Public Sub TestConstruction_GoodAristoData()
        Dim pi As ProcessInfo = ProcessInfo.FetchExisting()
        pi.Populate(GenerateAristoTestData)
        'Dim a As Address = Address.FetchExisting("", 
    End Sub
    <TestMethod()> Public Sub TestConstructor_MissingData()
    End Sub
    <TestMethod()> Public Sub TestConstructor_FailThroughData()
    End Sub

End Class