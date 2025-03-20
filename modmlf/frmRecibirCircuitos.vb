Imports System.Data.SqlClient

Public Class frmRecibirCircuitos
    Private dataTable As New DataTable()
    Private Sub txtCelda_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCelda.KeyPress
        If Asc(e.KeyChar) = 13 Then
            VerificaCelda()
        End If
    End Sub

    Private Sub txtIdAtado_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdAtado.KeyPress
        If Asc(e.KeyChar) = 13 Then
            AgregarResultadosAlDataGridView(txtIdAtado.Text)
            txtIdAtado.Text = ""
            txtIdAtado.Focus()
        End If
    End Sub

    Public Sub VerificaCelda()

        Dim query As String = $"SELECT COUNT(*) FROM tblToolCribAplicators WHERE cell = '{txtCelda.Text}'"

        Dim result As Integer = 0

        Using connection As New SqlConnection(strconexion)
            Using command As New SqlCommand(query, connection)
                connection.Open()
                result = Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using

        If result > 0 Then
            lblCelda.Text = txtCelda.Text
            txtIdAtado.Focus()
        Else
            Throw New Exception($"El valor '{txtCelda}' no se encuentra en la tabla 'tblToolCribAplicators'.")
        End If
    End Sub



    Private Sub AgregarResultadosAlDataGridView(id As String)

        Dim query As String = "SELECT idAtado,B.TermA, B.TermB, A.IDCWO, Qty, A.Route, B.CWO, B.WIP, A.CellA, A.CellB FROM tblatados A inner join tblCWOSerialNumbers B on A.IDCWO=B.IDCWO  WHERE idatado = @idatado and isReceived = 0 "

        Using connection As New SqlConnection(strconexion)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@idatado", id)
                Dim adapter As New SqlDataAdapter(command)
                Dim newDataTable As New DataTable()
                adapter.Fill(newDataTable)


                If dataTable.Columns.Count = 0 Then
                    ' Copiar la estructura de la tabla
                    dataTable = newDataTable.Clone()
                End If

                ' Agregar las filas nuevas al DataTable existente
                For Each row As DataRow In newDataTable.Rows
                    If row("CellA").ToString() = lblCelda.Text OrElse row("CellB").ToString() = lblCelda.Text Then
                        dataTable.ImportRow(row)
                    Else
                        MsgBox("Este circuito no pertenece a esta Celda")
                    End If
                Next


                DataGridView1.DataSource = dataTable ' Suponiendo que tu DataGridView se llama DataGridView1
            End Using
        End Using
    End Sub

    Private Sub ActualizarIsReceived()


        Using connection As New SqlConnection(strconexion)
            connection.Open()

            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    Dim idAtado As String = row.Cells("idAtado").Value.ToString()

                    Dim query As String = $"UPDATE tblAtados SET isReceived = 1, loc='{lblCelda.Text}' WHERE idAtado = @idAtado"
                    Using command As New SqlCommand(query, connection)
                        command.Parameters.AddWithValue("@idAtado", idAtado)
                        command.ExecuteNonQuery()
                    End Using
                End If
            Next

            MessageBox.Show("Circuitos recibidos")

            ' Limpiar el DataGridView
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()

            ' Limpiar el DataTable
            dataTable.Clear()
        End Using
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        ActualizarIsReceived()
        txtIdAtado.Text = ""
        txtCelda.Text = ""
    End Sub


End Class