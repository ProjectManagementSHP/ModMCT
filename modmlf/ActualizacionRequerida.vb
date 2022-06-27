Imports System.Deployment.Application
Public Class ActualizacionRequerida
    Private Sub ActualizacionRequerida_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (ApplicationDeployment.IsNetworkDeployed) Then
            With ApplicationDeployment.CurrentDeployment.CurrentVersion
                Label4.Text = "" & .Major & "." & .Minor & "." & .Build & "." & .Revision & ""
            End With
        Else
            Label4.Text = "" & System.Windows.Forms.Application.ProductVersion & ""
        End If
        Label5.Text = ver
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click ' Actualizar
        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim use As String = Environment.UserName
            Dim path As String = "C:\Users\" + use.ToString + "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\SEA\MLF.appref-ms"
            Process.Start(path)
            flagActualizacion = 1
            Me.Close()
            Cursor.Current = System.Windows.Forms.Cursors.Default
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class