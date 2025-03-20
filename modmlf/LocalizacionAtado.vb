Imports System.Data.SqlClient
Public Class LocalizacionAtado
    Private dataTable As New DataTable()
    Private Sub txtIdAtado_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdAtado.KeyPress
        If Asc(e.KeyChar) = 13 Then
            'AgregarResultadosAlDataGridView(txtIdAtado.Text)
            ObtenerInfoAtado(txtIdAtado.Text)

            'txtIdAtado.Text = ""
            'txtIdAtado.Focus()
        End If
    End Sub





    Private Sub AgregarResultadosAlDataGridView(id As String)

        Dim query As String = "SELECT idAtado,B.TermA, B.TermB, A.IDCWO, Qty, A.Route, B.CWO, B.WIP, B.JoinA, B.JoinB FROM tblatados A inner join tblCWOSerialNumbers B on A.IDCWO=B.IDCWO  WHERE idatado = @idatado and CellSpliceReceived = 0 "

        Using connection As New SqlConnection(strconexion)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@idatado", id)
                Dim adapter As New SqlDataAdapter(command)
                Dim newDataTable As New DataTable()
                adapter.Fill(newDataTable)


                If DataTable.Columns.Count = 0 Then
                    ' Copiar la estructura de la tabla
                    DataTable = newDataTable.Clone()
                End If

                ' Agregar las filas nuevas al DataTable existente
                For Each row As DataRow In newDataTable.Rows
                    'If row("JoinA").ToString() = lblCelda.Text OrElse row("JoinB").ToString() = lblCelda.Text Then
                    '    dataTable.ImportRow(row)
                    'Else
                    '    MsgBox("Este circuito no pertenece a esta Celda")
                    'End If
                    DataTable.ImportRow(row)
                Next



                '  DataGridView1.DataSource = dataTable ' Suponiendo que tu DataGridView se llama DataGridView1
            End Using
        End Using
    End Sub


    Private Sub ObtenerInfoAtado(id As String)
        Dim query As String = "SELECT idAtado,B.TermA, B.TermB, A.IDCWO, Qty, A.Route, B.CWO, B.WIP, B.JoinA, B.JoinB, B.Wire, B.WID, a.loc FROM tblatados A inner join tblCWOSerialNumbers B on A.IDCWO=B.IDCWO  WHERE idatado = @idatado and CellSpliceReceived = 0 and (joinA like '%SP%' or joinB like '%SP%' ) "

        cnn.Open()
        Using cmd As New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@idatado", id) ' Asegúrate de agregar el parámetro idatado
            Dim tb As New DataTable
            Dim dr As SqlDataReader
            cmd.CommandTimeout = 1800000
            dr = cmd.ExecuteReader
            tb.Load(dr)

            ' Verificar si se obtuvieron resultados
            If tb.Rows.Count > 0 Then
                ' Obtener la primera fila (ya que esperas solo una línea)
                Dim row As DataRow = tb.Rows(0)

                ' Asignar valores a los TextBox
                txtIdAtado.Text = row("idAtado").ToString()

                txtTraveler.Text = row("IDCWO").ToString()
                txtQty.Text = row("Qty").ToString()
                'txtRoute.Text = row("Route").ToString()
                'txtCWO.Text = row("CWO").ToString()
                txtWIP.Text = row("WIP").ToString()
                txtJoinA.Text = row("JoinA").ToString()
                txtJoinB.Text = row("JoinB").ToString()
                txtWire.Text = row("Wire").ToString()
                txtWID.Text = row("WID").ToString()
                txtLocacion.Text = row("Loc").ToString()
            Else
                MsgBox("Este atado no cuenta con Splices o ya fue recibido")
                ' Limpiar los TextBox si no se encontraron resultados
                LimpiarTextBox()
            End If
        End Using
        cnn.Close()
    End Sub

    Private Sub LimpiarTextBox()
        ' Método para limpiar los TextBox
        txtIdAtado.Text = ""
        txtWID.Text = ""
        txtWire.Text = ""
        txtTraveler.Text = ""
        txtQty.Text = ""
        ' txtRoute.Text = ""

        txtWIP.Text = ""
        txtJoinA.Text = ""
        txtJoinB.Text = ""
    End Sub
    Private Sub VerificaAtadosAnteriores(wip As String, SP As String)
        Dim query As String = $"SELECT A.CellSpliceA, A.CellSpliceB FROM tblatados A inner join tblCWOSerialNumbers B on A.IDCWO=B.IDCWO  WHERE  b.wip = @WIP and CellSpliceReceived = 1 and (joinA like '%{SP}%' or joinB like '%{SP}%' ) "

        cnn.Open()
        Using cmd As New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@WIP", wip) ' Asegúrate de agregar el parámetro idatado
            Dim tb As New DataTable
            Dim dr As SqlDataReader
            cmd.CommandTimeout = 1800000
            dr = cmd.ExecuteReader
            tb.Load(dr)

            Dim SpliceA As String = ""
            Dim SpliceB As String = ""

            ' Verificar si se obtuvieron resultados
            If tb.Rows.Count > 0 Then
                ' Obtener la primera fila (ya que esperas solo una línea)
                Dim row As DataRow = tb.Rows(0)
                ' Asignar valores a los TextBox
                SpliceA = If(row("CellSpliceA") IsNot Nothing, row("CellSpliceA").ToString(), "")
                SpliceB = If(row("CellSpliceB") IsNot Nothing, row("CellSpliceB").ToString(), "")

                ' Comparar para o asignar locacion si hay alguna anterior


            Else
                '  MsgBox("Este atado no cuenta con Splices")
                ' Limpiar los TextBox si no se encontraron resultados
                '  LimpiarTextBox()
            End If
        End Using
        cnn.Close()
    End Sub

End Class