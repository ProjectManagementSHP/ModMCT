Imports System.Data.SqlClient
Imports System.Globalization

Public Class SplicePlanning
    Private Sub PressPlanning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim nsemana As Integer = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(Date.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
        cmbSem.Items.Add(nsemana)
        cmbSem.Items.Add(nsemana + 1)
        cmbSem.Items.Add(nsemana + 2)
        cmbSem.Items.Add(nsemana + 3)
        cmbSem.Text = nsemana
        GetWips(nsemana)
    End Sub

    Private Sub GetWips(semana As Integer)
        Try


            Dim query As String = "SELECT B.wip, B.AU, A.Qty, " &
                                  "(SELECT COUNT(*) FROM tblwipdet WHERE wip = B.wip AND (JoinA LIKE '%SP%' OR JoinB LIKE '%SP%')) AS TlSplices " &
                                  "FROM tblwip A " &
                                  "INNER JOIN tblwipdet B ON A.wip = B.wip " &
                                  "WHERE A.status = 'Open' AND A.sem >= @Semana AND (B.JoinA LIKE '%SP%' OR B.JoinB LIKE '%SP%') and A.CeldaSplice is null " &
                                  "GROUP BY B.wip, B.AU, A.Qty"

            Using connection As New SqlConnection(strconexion)
                connection.Open()

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Semana", semana)

                    Using adapter As New SqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)

                        dgvWipsSplices.DataSource = dataTable
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al recuperar los datos: " & ex.Message)
        End Try
    End Sub


    Private Sub cmbSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSem.SelectedIndexChanged

        dgvWipsSplices.Rows.Clear()
        GetWips(cmbSem.Text)

    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        AgregarSeleccionados()
        GetWips(cmbSem.Text)
    End Sub

    Private Sub AgregarSeleccionados()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try


            'Recorre el datagrid
            For i = 0 To dgvWipsSplices.Rows.Count - 1
                Dim selWIP As Boolean = dgvWipsSplices.Rows(i).Cells("check").Value
                Dim WIP As String = dgvWipsSplices.Rows(i).DataBoundItem("WIP").ToString


                If selWIP = True Then
                    'Ingresa el WIP a la tabla tblTempModMM
                    UpdateCeldaSplice(WIP, cmbCeldaPrensa.Text)

                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error Actualizacion de Celda de WIPs")
        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub UpdateCeldaSplice(WIP As String, Celda As String)
        Dim query As String = "UPDATE tblwip SET celdaSplice = @NuevoValor WHERE wip = @WIP"

        Try
            Using conexion As New SqlConnection(strconexion)
                Using comando As New SqlCommand(query, conexion)
                    ' Agregar parámetros
                    comando.Parameters.AddWithValue("@Celda", Celda)
                    comando.Parameters.AddWithValue("@WIP", WIP)

                    ' Abrir la conexión y ejecutar la consulta
                    conexion.Open()
                    comando.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error al actualizar: " & ex.Message)
        End Try
    End Sub

End Class