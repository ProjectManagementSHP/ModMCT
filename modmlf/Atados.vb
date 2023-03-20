Imports System.Data.SqlClient
Public Class Atados
	Public execQuery As Func(Of String, SqlCommand) = Function(sentence)
														  If cnn.State.ToString = "Open" Then cnn.Close()
														  Dim cmd As SqlCommand
														  cmd = New SqlCommand(sentence, cnn)
														  cmd.CommandType = CommandType.Text
														  cmd.CommandTimeout = 18000000
														  If cnn.State.ToString = "Closed" Then cnn.Open()
														  Return cmd
													  End Function
	Private Sub Load_Aus(Optional AU As Integer = 0)
		Try
			Dim query As String = "select AU,Rev,Atado from tblMaster where Active = 1"
			If AU > 0 Then
				query += $" and AU like '%{AU}%'"
			End If
			Dim read As SqlDataReader
			Dim tb As New DataTable
			read = execQuery(query).ExecuteReader
			tb.Load(read)
			With DgvAus
				.DataSource = tb
				.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
				.AutoResizeColumns()
				.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
				Label5.Text = $"Items: { .RowCount}"
				.ClearSelection()
			End With
			cnn.Close()
		Catch ex As Exception
			cnn.Close()
			MessageBox.Show(ex.ToString)
		End Try
	End Sub
	Private Sub AsignarQtyAtado(QtyAtado As Integer)
		Try
			execQuery($"update tblMaster set Atado = {QtyAtado} where AU = {LblAu.Text} and Rev = '{LblRev.Text}'").ExecuteNonQuery()
			MessageBox.Show($"Se actualizo la cantidad de Atado para este AU {LblAu.Text} con Rev {LblRev.Text}.")
			Load_Aus(If(TextBox1.Text <> "", CInt(TextBox1.Text), 0))
			TxbQtyAtado.Text = ""
			LblAu.Text = ""
			LblRev.Text = ""
			cnn.Close()
		Catch ex As Exception
			cnn.Close()
			MessageBox.Show(ex.ToString)
		End Try
	End Sub

	'/*Events*/
	Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
		Cursor.Current = Cursors.WaitCursor
		Dim filtro As String = CType(sender, TextBox).Text
		If filtro.Trim() <> String.Empty Then
			Load_Aus(filtro)
		End If
		Cursor.Current = Cursors.Default
	End Sub
	Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
		If e.KeyChar = Chr(13) AndAlso TextBox1.Text <> "" Then
			Load_Aus(TextBox1.Text)
			TextBox1.Text = ""
		End If
	End Sub
	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Load_Aus()
		TextBox1.Text = ""
		TxbQtyAtado.Text = ""
		LblAu.Text = ""
		LblRev.Text = ""
	End Sub
	Private Sub Atados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Load_Aus()
	End Sub
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		If IsNumeric(TxbQtyAtado.Text) AndAlso CInt(TxbQtyAtado.Text) > 1 Then
			AsignarQtyAtado(CInt(TxbQtyAtado.Text))
		Else
			MessageBox.Show("Coloque una cantidad valida")
		End If
	End Sub
	Private Sub TxbQtyAtado_TextChanged(sender As Object, e As EventArgs) Handles TxbQtyAtado.TextChanged
		Button2.Visible = (LblAu.Text <> "-" And LblRev.Text <> "-") AndAlso TxbQtyAtado.Text.Length > 0 AndAlso IsNumeric(TxbQtyAtado.Text) AndAlso CInt(TxbQtyAtado.Text) > 1
	End Sub
	Private Sub DgvAus_CellClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAus.CellClick
		If e.RowIndex >= 0 Then
			LblAu.Text = DgvAus.Item(0, e.RowIndex).Value()
			LblRev.Text = DgvAus.Item(1, e.RowIndex).Value()
		End If
	End Sub
End Class