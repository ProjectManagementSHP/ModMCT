Public Class CloseCWO
	Dim FlagCancelOrClose As Boolean
	Public Sub New(Flag As Boolean)

		' This call is required by the designer.
		FlagCancelOrClose = Flag
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

	End Sub
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Close()
	End Sub
	Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
		Dim ChangeCwo As New Principal
		If FlagCancelOrClose Then
			If ChangeCwo.ClosingCWO(txbNotas.Text.Trim) Then
				MessageBox.Show("Se cerro con exito el CWO.")
				Close()
			Else
				MessageBox.Show("No se logro cerrar este CWO, revisalo.")
				Close()
			End If
		Else
			If ChangeCwo.CancelCWO(txbNotas.Text.Trim) Then
				MessageBox.Show("Se cancelo con exito el CWO.")
				Close()
			Else
				MessageBox.Show("No se logro cancelar este CWO, revisalo.")
				Close()
			End If
		End If
	End Sub
End Class