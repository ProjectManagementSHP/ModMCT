Public Class CambioMaquina
    Public Categoria As Integer
    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        If Not ComboBox1.Text = "" Then
            If Categoria = 1 Then
                Principal.cambioMaquinaXCWO(ComboBox1.Text, txbNotas.Text, lblcwoporsolicitar.Text, Categoria)
            Else
                Principal.cambioMaquinaXCWO(ComboBox1.Text, txbNotas.Text, lblcwoporsolicitar.Text)
            End If
            Me.Dispose()
            Me.Close()
        Else
            MsgBox("Favor de seleccionar una maquina para continuar")
            ComboBox1.SelectAll()
            ComboBox1.Focus()
        End If
    End Sub
    Private Sub CambioMaquina_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Text = ""
        txbNotas.Text = Nothing
    End Sub
End Class