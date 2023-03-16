Imports System.Deployment.Application
Public Class ActualizacionRequerida
    Dim Oldversion As String
    Sub New(versionOld As String)

        ' This call is required by the designer.
        InitializeComponent()
        Oldversion = versionOld
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub ActualizacionRequerida_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (ApplicationDeployment.IsNetworkDeployed) Then
            With ApplicationDeployment.CurrentDeployment.CurrentVersion
                Label4.Text = $"{ .Major}.{ .Minor}.{ .Build}.{ .Revision}"
            End With
        Else
            Label4.Text = $"{Application.ProductVersion}"
        End If
        Label5.Text = Oldversion
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click ' Actualizar
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim use As String = Environment.UserName
            Dim path As String = "C:\Users\" + use.ToString + "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\SEA\MLF.appref-ms"
            Process.Start(path)
            flagActualizacion = True
            Close()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub
End Class