Public Class CloseCWO
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Close()
	End Sub
	Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
		Dim CloseCwo As New Principal
		If CloseCwo.ClosingCWO(txbNotas.Text.Trim) Then
			MessageBox.Show("Se cerro con exito el CWO.")
			Close()
		Else
			MessageBox.Show("No se logro cerrar este CWO, revisalo.")
			Close()
		End If
	End Sub
End Class