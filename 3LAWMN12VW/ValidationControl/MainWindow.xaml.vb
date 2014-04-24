Class MainWindow 

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim vr As New Rule("The Ten Commandments", False)
        vr.Rules.Add(New Rule("Commandment 1", False))
        vr.Rules(0).Rules.Add(New Rule("Thou Shall Not Kill", False))
        vr.Rules(0).Rules(0).Rules.Add(New Rule("-A Unless Thy Victim is a member of a different religion", True))
        vr.Rules(0).Rules(0).Rules.Add(New Rule("-B Unless Thy Victim is a member of a different race", False))
        vr.Rules(0).Rules(0).Rules.Add(New Rule("-C Unless Thy Victim is a witch", False))
        vr.Rules.Add(New Rule("Commandment 2", False))
        vr.Rules(1).Rules.Add(New Rule("Thou Shall Not Steal", False))
        vr.Rules(1).Rules(0).Rules.Add(New Rule("-A Unless Thou was a victim of Theft first", True))
        vr.Rules(1).Rules(0).Rules.Add(New Rule("-B It is really really funny", True))
        vr.Rules.Add(New Rule("Commandment 3", False))
        MyBase.DataContext = New RuleSet(vr)
    End Sub

End Class
