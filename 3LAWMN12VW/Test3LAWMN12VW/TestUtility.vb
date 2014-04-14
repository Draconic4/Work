Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports _3LAWMN12VW.ValidationRuleData

<TestClass()> Public Class TestUtility

    <TestMethod()> Public Sub TestIsBusiness()
        Dim pi As ProcessInfo = ProcessInfo.Fetch(Nothing, Nothing)
        Assert.IsFalse(_3LAWMN12VW.Utility.IsBusiness(pi))
    End Sub

End Class