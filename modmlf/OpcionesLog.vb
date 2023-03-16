Public Class OpcionesLog
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            opcion = 6
            Close()
        End If
    End Sub
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            opcion = 7
            Close()
        End If
    End Sub
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then
            opcion = 2
            Close()
        End If
    End Sub
    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked Then
            opcion = 3
            Close()
        End If
    End Sub
    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked Then
            opcion = 5
            Close()
        End If
    End Sub
    Public Sub CheckOpcionesVisible(dep As String)
        RadioButton1.Visible = False
        RadioButton2.Visible = False
        RadioButton3.Visible = False
        RadioButton4.Visible = False
        RadioButton5.Visible = False
        RadioButton7.Visible = False
        RadioButton6.Visible = False
        Dim opciones As String() = dep.Split(",")
        If opciones IsNot Nothing Then
            For Each _Option In opciones
                If _Option = "3" Then 'Aplicadores
                    RadioButton4.Visible = True
                ElseIf _Option = "2" Then 'Almacen
                    RadioButton3.Visible = True
                ElseIf _Option = "5" Then 'Compras
                    RadioButton5.Visible = True
                ElseIf _Option = "6" Then 'PlanCorte
                    RadioButton1.Visible = True
                ElseIf _Option = "7" Then 'PlanXP
                    RadioButton2.Visible = True
                ElseIf _Option = "8" Then 'PlanPWO
                    RadioButton7.Visible = True
                End If
            Next
        End If
    End Sub
    Public Sub AllVisible()
        RadioButton1.Visible = True
        RadioButton2.Visible = True
        RadioButton3.Visible = True
        RadioButton4.Visible = True
        RadioButton5.Visible = True
        RadioButton7.Visible = True
        RadioButton6.Visible = True
    End Sub
    Private Sub OpcionesLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton4.Checked = False
        RadioButton5.Checked = False
        RadioButton7.Checked = False
        RadioButton6.Checked = True
    End Sub
    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked Then
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton4.Checked = False
            RadioButton5.Checked = False
            RadioButton7.Checked = False
        End If
    End Sub
    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked Then
            opcion = 8
            Close()
        End If
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Application.Exit()
        End
    End Sub
    'Private Sub OpcionesLog_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    '    Application.Exit()
    '    End
    'End Sub
End Class