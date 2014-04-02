Public Class Launcher
    Shared Sub Main()
        System.Windows.Forms.Application.EnableVisualStyles()
        PBS.Framework.Core.Library.ConfigurationManager.IsDevelopmentMode = True
        PBS.Framework.SOA.Requests.Application.ConfigureApp()
        PBS.Framework.SOA.Requests.Application.ConfigureLogin("PBS")

        Dim pv As New FormPromptViewModel()
        PBS.Framework.ControlLibrary.Wpf.AristoShell.ShowModalView(pv)
    End Sub

End Class
