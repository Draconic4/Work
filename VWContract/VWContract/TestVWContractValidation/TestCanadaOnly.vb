Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VWContractValidation.ValidationLib

<TestClass()> Public Class TestCanadaOnly

    <TestMethod()> Public Sub TestDefaultConstructor()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object)
        Dim bArgs As New BaseConstructionArgs With {.PreviousRun = pRun, .CurrentRun = cRun}
        Dim tst As CanadaOnly = CanadaOnly.Fetch(bArgs)
        Assert.AreEqual(False, tst.IsActive)
        tst.AggregateCashPrice = 250D
        Assert.AreEqual(0D, tst.AggregateCashPrice)
    End Sub

    <TestMethod()> Public Sub TestCanadaConstructor()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object) From {{"DLR_COUNTRY", "CANADA"}}
        Dim bArgs As New BaseConstructionArgs With {.GlobalProperty = Nothing, .PreviousRun = pRun, .CurrentRun = cRun}
        Dim pArgs As New BaseConstructionArgs With {.GlobalProperty = ProcessInfo.Fetch(bArgs)}
        Dim tst As CanadaOnly = CanadaOnly.Fetch(pArgs)
        Assert.AreEqual(True, tst.IsActive)
        tst.AggregateCashPrice = 250D
        Assert.AreEqual(250D, tst.AggregateCashPrice)
    End Sub

End Class