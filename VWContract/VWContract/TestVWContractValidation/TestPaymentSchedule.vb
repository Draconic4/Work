Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VWContractValidation

<TestClass()> Public Class TestPaymentSchedule

    <TestMethod()> Public Sub TestDefaultConstrutor()
        Dim pRun As New Dictionary(Of String, Object)
        Dim cRun As New Dictionary(Of String, Object)
        Dim scheduleArgs As New ScheduleArguments With {.GlobalProperty = ProcessInfo.Fetch(pRun, cRun), .PreviousRun = pRun, .CurrentRun = cRun}
        Dim tst As PaymentSchedule = PaymentSchedule.Fetch(scheduleArgs)
        Assert.AreEqual(True, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).IsValid)
        Assert.AreEqual(0, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentPeriod)
        Assert.AreEqual(0D, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).Payment)
        Assert.AreEqual(String.Empty, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentLine)
        Assert.AreEqual(String.Empty, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentStartDate)
        Assert.AreEqual(String.Empty, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentEndDate)
        Assert.AreEqual(False, tst.PaymentDictionary(PaymentSchedule.SECONDLINE).IsValid)
        Assert.AreEqual(False, tst.PaymentDictionary(PaymentSchedule.THIRDLINE).IsValid)
        Assert.AreEqual(False, tst.PaymentDictionary(PaymentSchedule.FOURTHLINE).IsValid)
        Assert.AreEqual(False, tst.PaymentDictionary(PaymentSchedule.FINALLINE).IsValid)
    End Sub
    <TestMethod()> Public Sub TestPayment()
        Dim cRun As New Dictionary(Of String, Object) From {{"DEAL_PAYMENT", 256.93D}}
        Dim schArgs As New ScheduleArguments With {.GlobalProperty = ProcessInfo.Fetch(Nothing, cRun), .PreviousRun = Nothing, .CurrentRun = cRun}
        Dim tst As PaymentSchedule = PaymentSchedule.Fetch(schArgs)
        Assert.AreEqual(True, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).IsValid)
        Assert.AreEqual(256.93D, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).Payment)
        Assert.AreEqual(False, tst.PaymentDictionary(PaymentSchedule.SECONDLINE).IsValid)
    End Sub
    <TestMethod()> Public Sub TestPeriod()
        Dim cRun As New Dictionary(Of String, Object) From {{"DEAL_TERM", 36}}
        Dim schArgs As New ScheduleArguments With {.GlobalProperty = ProcessInfo.Fetch(Nothing, cRun), .PreviousRun = Nothing, .CurrentRun = cRun}
        Dim tst As PaymentSchedule = PaymentSchedule.Fetch(schArgs)
        Assert.AreEqual(True, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).IsValid)
        Assert.AreEqual(36, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentPeriod)
    End Sub
    <TestMethod()> Public Sub TestStartDate()
        Dim cRun As New Dictionary(Of String, Object) From {{"DEAL_TERM", 36}, {"DEAL_PAYMENTDATE", "05/29/2014"}}
        Dim schArgs As New ScheduleArguments With {.GlobalProperty = ProcessInfo.Fetch(Nothing, cRun), .PreviousRun = Nothing, .CurrentRun = cRun}
        Dim tst As PaymentSchedule = PaymentSchedule.Fetch(schArgs)
        Assert.AreEqual(True, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).IsValid)
        Assert.AreEqual("05/29/2014", tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentStartDate, True)
    End Sub
    <TestMethod()> Public Sub TestEndDate()
        Dim cRun As New Dictionary(Of String, Object) From {{"DEAL_TERM", 36}, {"DEAL_PAYMENTDATE", "05/29/2014"}}
        Dim schArgs As New ScheduleArguments With {.GlobalProperty = ProcessInfo.Fetch(Nothing, cRun), .PreviousRun = Nothing, .CurrentRun = cRun}
        Dim tst As PaymentSchedule = PaymentSchedule.Fetch(schArgs)
        Assert.AreEqual(True, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).IsValid)
        Assert.AreEqual("05/29/2017", tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentEndDate, True)
    End Sub
    <TestMethod()> Public Sub TestBalloonDType()
        Dim cRun As New Dictionary(Of String, Object) From {{"DEAL_FCALC", 0}, {"DEAL_BALANCEDUE", 19021.68}, {"DEAL_APR", 2.34D}, {"DEAL_TERM", 36}, {"DEAL_PAYMENT", 235D}, {"DEAL_PAYMENTDATE", "05/29/2014"}, {"DEAL_BALOON", 2345.05D}, {"BANK_BALOONTYPE", "D"}}
        Dim schArgs As New ScheduleArguments With {.GlobalProperty = ProcessInfo.Fetch(Nothing, cRun), .PreviousRun = Nothing, .CurrentRun = cRun}
        Dim tst As PaymentSchedule = PaymentSchedule.Fetch(schArgs)
        Assert.AreEqual(True, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).IsValid)
        Assert.AreEqual(235D, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).Payment)
        Assert.AreEqual(35, tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentPeriod)
        Assert.AreEqual("05/29/2014", tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentStartDate, True)
        'Assert.AreEqual("04/29/2014", tst.PaymentDictionary(PaymentSchedule.FIRSTLINE).PaymentEndDate, True)
        Assert.AreEqual(True, tst.PaymentDictionary(PaymentSchedule.SECONDLINE).IsValid)
        Assert.AreEqual(1, tst.PaymentDictionary(PaymentSchedule.SECONDLINE).PaymentPeriod)
        Assert.AreEqual(2580.05D, tst.PaymentDictionary(PaymentSchedule.SECONDLINE).Payment)
        Assert.AreEqual("05/29/2017", tst.PaymentDictionary(PaymentSchedule.SECONDLINE).PaymentEndDate, True)
    End Sub
    <TestMethod()> Public Sub TestBalloonNonDType()

    End Sub
End Class