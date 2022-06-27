Public Class Hold
    Public flag As Integer = 0
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txbNotas.Text <> "" Then
            If flag = 1 Then
                If Principal.lbldept.Text = "Compras" Then
                    If dtpFProm.Value = "2021-01-01" Then
                        MsgBox("Debe agregar fecha promesa", MessageBoxIcon.Warning)
                    Else
                        flag = 0
                        Principal.notas(lblcwoporsolicitar.Text, dtpFProm.Text, txbNotas.Text)
                        Me.Close()
                    End If
                Else
                    Principal.notas(lblcwoporsolicitar.Text, dtpFProm.Text, txbNotas.Text)
                    'If op = 1 Then
                    '    Principal.notesWIPandCWOquitandoOnHold(txbNotas.Text)
                    Me.Close()
                End If
                'Else
            ElseIf op = 3 Then
                Principal.notesWIPandCWOquitaOnHoldde26(lblcwoporsolicitar.Text, txbNotas.Text, lblwipporsolicitar.Text)
                Me.Close()
            ElseIf flag = 12 Then
                flag = 0
                Principal.notesWIPandCWOOnHold(lblcwoporsolicitar.Text, dtpFProm.Text, txbNotas.Text)
                Me.Close()
            Else
                If dtpFProm.Value = "2021-01-01" Then
                    MsgBox("Debe agregar fecha promesa", MessageBoxIcon.Warning)
                Else
                    Principal.notesWIPandCWOOnHold(lblcwoporsolicitar.Text, dtpFProm.Text, txbNotas.Text)
                    Me.Close()
                End If
            End If
        Else
            MsgBox("Debe añadir su nota", MessageBoxIcon.Warning)
            txbNotas.Text = ""
        End If
    End Sub
    Private Sub dtpFProm_MouseDown(sender As Object, e As MouseEventArgs) Handles dtpFProm.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.dtpFProm.Format = DateTimePickerFormat.Short
            If dtpFProm.Value = "2021-01-01" Then
                Me.dtpFProm.Value = Date.Today
            End If
        End If
    End Sub
    Private Sub Hold_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txbNotas.Text = ""
        If flag = 12 Then dtpFProm.Value = "2022-12-01"
        dtpFProm.Value = "2021-01-01"
        If opcion = 2 Then
            btnGuardar.BackColor = Color.LightBlue
        ElseIf opcion = 3 Then
            btnGuardar.BackColor = Color.LightGray
        End If
    End Sub
End Class