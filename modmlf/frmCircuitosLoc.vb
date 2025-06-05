Imports System.Data.SqlClient
Imports System.Globalization

Public Class frmCircuitosLoc
    Private Sub frmCircuitosLoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim nsemana As Integer = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(Date.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
        Label8.Text = "Semana Actual: " & nsemana
        LoadTermsData()
    End Sub



    Private Sub ObtenerCeldas()
        cmbCelda.Items.Clear()
        Dim query As String = "SELECT DISTINCT cell FROM tblToolCribAplicators"

        Using connection As New SqlConnection(strconexion)
            connection.Open()
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        cmbCelda.Items.Add(reader("cell").ToString())
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub cmbSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSelector.SelectedIndexChanged
        If cmbSelector.Text = "PRESS" Then
            cmbCelda.Visible = True
            ObtenerCeldas()
            LoadTermsData()
        ElseIf cmbSelector.Text = "SPLICE" Then
            cmbCelda.Visible = True
        End If
    End Sub

    Private Sub LlenarDataGridTerms(valor As String)

        Dim query As String = "SELECT 
                                Term,
                                SUM(Qty) AS TotalQty,
                                NULL AS EnCortebasket,
                                NULL AS EnCorte
                            FROM (
                                SELECT TermA AS Term, Qty
                                FROM tblatados A 
                                INNER JOIN tblCWOSerialNumbers B ON A.IDCWO = B.IDCWO 
                                WHERE TermA IS NOT NULL AND TermA != ''

                                UNION ALL

                                SELECT TermB AS Term, Qty
                                FROM tblatados A 
                                INNER JOIN tblCWOSerialNumbers B ON A.IDCWO = B.IDCWO 
                                WHERE TermB IS NOT NULL AND TermB != ''
                            ) AS CombinedTerms
                            WHERE Term NOT LIKE '%/MA%'
                            GROUP BY Term
                            ;"

        Using connection As New SqlConnection(strconexion)
            connection.Open()
            Dim adapter As New SqlDataAdapter()
            Dim dataTable As New DataTable()

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@valor", valor)
                adapter.SelectCommand = command
                adapter.Fill(dataTable)
            End Using

            dgvTerms.DataSource = dataTable
        End Using
    End Sub
    Private Sub LlenarDataGridTermsDet(terminal As String)
        terminal = Trim(terminal)
        Dim query As String = $"SELECT idAtado,B.TermA, B.TermB, A.IDCWO, Qty, A.Route, B.CWO, B.WIP FROM tblatados A inner join tblCWOSerialNumbers B on A.IDCWO=B.IDCWO  WHERE B.TermA like '%{terminal}%' or  B.TermB like '%{terminal}%'"

        Using connection As New SqlConnection(strconexion)
            connection.Open()
            Dim adapter As New SqlDataAdapter()
            Dim dataTable As New DataTable()

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@terminal", terminal)
                adapter.SelectCommand = command
                adapter.Fill(dataTable)
            End Using

            dgvTermsDet.DataSource = dataTable
        End Using
    End Sub

    Private Sub cmbCelda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCelda.SelectedIndexChanged
        If cmbCelda.Text <> "" And cmbSelector.Text = "PRESS" Then
            '  LlenarDataGridTerms(cmbCelda.Text)
        ElseIf cmbCelda.Text <> "" And cmbSelector.Text = "SPLICE" Then
            '  LlenarDataGridSplice(cmbCelda.Text)
        End If
    End Sub



    Private Sub dgvTerms_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTerms.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim terminal As String = dgvTerms.Rows(e.RowIndex).Cells(0).Value.ToString()
            If cmbCelda.Text <> "" And cmbSelector.Text = "PRESS" Then
                LlenarDataGridTermsDet(terminal)
            ElseIf cmbCelda.Text <> "" And cmbSelector.Text = "SPLICE" Then
                LlenarDataGridTermsDet(terminal)
            End If


        End If
    End Sub

    Private Sub LlenarDataGridSplice(celda As String)

        Dim query As String = "select idcwo,(select WIP from tblcwoserialnumbers where idcwo = tblatados.idcwo) WIP, sum(qty) Qty, (select wirebalance from tblcwoserialnumbers where idcwo = tblatados.idcwo) - sum(qty) as SinRecibir, CellSplice as Celda from tblatados where CellSpliceReceived = 1  and cellsplice = @celda group by idcwo, CellSplice, tblAtados.qty"

        Using connection As New SqlConnection(strconexion)
            connection.Open()
            Dim adapter As New SqlDataAdapter()
            Dim dataTable As New DataTable()

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@celda", celda)
                adapter.SelectCommand = command
                adapter.Fill(dataTable)
            End Using

            dgvTerms.DataSource = dataTable
        End Using


    End Sub





    Public Sub LoadTermsData()
        Dim query As String = "
        SELECT 
            term, 
            '' AS QtyMP,
            SUM(qty) AS EnCorte,
            SUM(qty) AS Total,
            '' AS Celda,
            '' AS PWO,
            '' AS FechaPWO,
            '' AS QtyInicio,
            '' AS BalXcortadora,
            '' AS HraProgramada,
            '' AS Dado,
            '' AS Term
        FROM (
            SELECT 
                termA AS term, 
                tblwip.qty
            FROM 
                tblwip 
            INNER JOIN 
                tblwipdet 
            ON 
                tblwip.wip = tblwipdet.wip
            WHERE 
                wsort >= 20 AND tblwipdet.MaqA = 'MM' AND (pwoA IS NULL OR pwoA = '') AND tblWIP.Status = 'Open'

            UNION ALL

            SELECT 
                termB AS term, 
                tblwip.qty
            FROM 
                tblwip 
            INNER JOIN 
                tblwipdet 
            ON 
                tblwip.wip = tblwipdet.wip
            WHERE 
                wsort >= 20 AND tblwipdet.MaqB = 'MM' AND (pwoB IS NULL OR pwoB = '') AND tblWIP.Status = 'Open'
        ) AS combined
        GROUP BY 
            term
        ORDER BY 
            SUM(qty) DESC"

        Try
            Using conn As New SqlConnection(strconexion)
                conn.Open()
                Using cmd As New SqlCommand(query, conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)

                        ' Asigna los datos al DataGridView antes de agregar botones
                        dgvTerms.DataSource = dt
                    End Using
                End Using
            End Using

            ' Eliminar botones si ya existen (para evitar duplicados)
            RemoveButtonIfExists("btnCrearPWO")
            RemoveButtonIfExists("btnSolicitarMasTerm")
            RemoveButtonIfExists("btnCerrarPWO")

            ' Guardar el índice de la última columna de datos
            Dim lastColumnIndex As Integer = dgvTerms.Columns.Count

            ' Agregar botones al final del DataGridView
            AddButtonToDataGridView("btnCrearPWO", "Acción", "Crear PWO")
            AddButtonToDataGridView("btnSolicitarMasTerm", "Solicitar", "Solicitar Más Term")
            AddButtonToDataGridView("btnCerrarPWO", "Cerrar", "Cerrar PWO")

            ' Recalcular los índices de las columnas de botones
            If dgvTerms.Columns.Contains("btnCrearPWO") Then dgvTerms.Columns("btnCrearPWO").DisplayIndex = lastColumnIndex
            If dgvTerms.Columns.Contains("btnSolicitarMasTerm") Then dgvTerms.Columns("btnSolicitarMasTerm").DisplayIndex = lastColumnIndex + 1
            If dgvTerms.Columns.Contains("btnCerrarPWO") Then dgvTerms.Columns("btnCerrarPWO").DisplayIndex = lastColumnIndex + 2

        Catch ex As Exception
            MessageBox.Show("Error al cargar datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Función para agregar botones dinámicamente al final del DataGridView
    Private Sub AddButtonToDataGridView(buttonName As String, headerText As String, buttonText As String)
        If Not dgvTerms.Columns.Contains(buttonName) Then
            Dim btnColumn As New DataGridViewButtonColumn()
            btnColumn.Name = buttonName
            btnColumn.HeaderText = headerText
            btnColumn.Text = buttonText
            btnColumn.UseColumnTextForButtonValue = True
            dgvTerms.Columns.Add(btnColumn)
        End If
    End Sub

    ' Función para eliminar botones si ya existen
    Private Sub RemoveButtonIfExists(columnName As String)
        If dgvTerms.Columns.Contains(columnName) Then
            dgvTerms.Columns.Remove(columnName)
        End If
    End Sub





End Class
