Public Class Asignar
    Private Sub Asignar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cargaelcombo()
        dtfechaasignada.Value = Now
    End Sub

    'Private Sub txbhora_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Not IsNumeric(e.KeyChar) Then
    '        e.Handled = True
    '    End If
    '    If e.KeyChar = Chr(13) Then
    '        ComboBox1.Focus()
    '    End If
    'End Sub

    'Private Sub txbminutos_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Not IsNumeric(e.KeyChar) Then
    '        e.Handled = True
    '    End If
    'End Sub
    Private Sub cargaelcombo()
        Dim cargacombo() As String = {"06:00 AM", "07:00 AM", "08:00 AM", "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM", "07:00 PM", "08:00 PM", "09:00 PM", "10:00 PM", "11:00 PM", "12:00 AM", "01:00 AM",
            "02:00 AM", "03:00 AM", "04:00 AM", "05:00 AM"}
        ComboBox1.Items.AddRange(cargacombo)
    End Sub
    Private Sub btnenviarsolicitud_Click(sender As Object, e As EventArgs) Handles btnenviarsolicitud.Click
        If ComboBox1.Text <> "" Then
            Dim fecha As String = dtfechaasignada.Text
            Dim horarequerida As String = ComboBox1.Text
            campocortesolicitud = fecha & " " & horarequerida
            Dim fechafinal As DateTime = campocortesolicitud
            Dim respuesta As String
            respuesta = MessageBox.Show("Seguro que desea asignar el CWO: " + lblcwoporsolicitar.Text + " para la fecha y hora: " + fechafinal.ToString + " ?", "Verificacion", MessageBoxButtons.YesNo)
            If respuesta = vbYes Then
                'If campocortesolicitud <= DateTime.Now.ToString("hh:mm tt") Then
                If campocortesolicitud <= Now() Then
                    respuesta = MessageBox.Show("No es posible enviar la solicitud, debido a que no puedes asignar la hora de solicitud por que debe ser con antelacion de minimo 30 minutos " + vbNewLine + " Desea asignarlo a la hora indicada?", "Verificacion", MessageBoxButtons.YesNo)
                    If respuesta = vbYes Then
                        Principal.actualizafecharequerimiento(lblcwoporsolicitar.Text, fechafinal, lblwipporsolicitar.Text)
                    ElseIf respuesta = vbNo Then
                        ComboBox1.Text = ""
                        campocortesolicitud = ""
                    End If
                Else
                    Principal.actualizafecharequerimiento(lblcwoporsolicitar.Text, fechafinal, lblwipporsolicitar.Text)
                End If
                Me.Hide()
            ElseIf respuesta = vbNo Then
                ComboBox1.Text = ""
                campocortesolicitud = ""
            End If
        Else
            MsgBox("Debe agregar la hora de asignacion")
        End If
    End Sub
End Class