Public Class CambioMaquina
    Public Categoria As Integer
    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click

    End Sub
    Private Sub CambioMaquina_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Text = ""
        txbNotas.Text = Nothing
    End Sub
End Class