Imports System.Runtime.InteropServices

Public Class SendAristoErrorsToNotepadInterOp
    Const WM_SETTEXT = &HC

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal iclassName As String, ByVal windowTitle As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As UInteger, ByVal wParam As Int32, ByVal lParam As String) As Int32
    End Function

    Public Shared Sub SendErrors(ByVal fileName As String, ByVal content As List(Of String))
        Dim hParent As IntPtr = FindWindowEx(IntPtr.Zero, hParent, "Notepad", "Untitled - Notepad")
        If hParent.Equals(IntPtr.Zero) Then
            System.Diagnostics.Process.Start("notepad.exe")
            hParent = FindWindowEx(IntPtr.Zero, hParent, "Notepad", "Untitled - Notepad")
        End If
        If Not hParent.Equals(IntPtr.Zero) Then
            Dim hChild As IntPtr = FindWindowEx(hParent, hChild, "Edit", Nothing)
            If Not hChild.Equals(IntPtr.Zero) Then
                Dim msg As String = String.Empty
                For Each Str As String In content
                    msg = msg & Str & vbNewLine
                Next
                SendMessage(hChild, WM_SETTEXT, 0, msg)
            Else
            End If
        End If
    End Sub

End Class
