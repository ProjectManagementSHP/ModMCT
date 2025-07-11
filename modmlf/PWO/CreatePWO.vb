﻿Imports System.Data.SqlClient

Public Class CreatePWO
    Dim AU As Integer = 0
    Dim InfoTablas As New List(Of ChargeInfo)
    Dim PartNumber As String = ""

    Private Sub GetDataByUser()
        Try
            AU = 0
            If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
                Dim res = MessageBox.Show("Ya se estan visualizando terminales en por procesar, si deseas dejar de visualizar las terminales seleccionadas, pulsa Yes, si deseas continuar pulsa no.", "Recargando informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If res = 6 Then
                    dgvDetalleTerminales.DataSource = Nothing
                    InfoTablas.Clear()
                    If TextBox1.Text <> "" Then TextBox1.Clear()
                    'stateRes = Await Task.Run(Function() ChargeGrid())
                    ChargeGrid()
                    CleanningState()
                End If
            Else
                If TextBox1.Text <> "" Then TextBox1.Clear()
                'stateRes = Await Task.Run(Function() ChargeGrid())
                ChargeGrid()
                CleanningState()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub ChargeGrid()
        Try
            'Al query principal, falta agregar que PWO sea nulo o que no tenga PWO
            Dim consulta As String = $"WITH CombinedResults AS (
    SELECT 
        tblwipdet.terma AS Terminal, 
        tblwipdet.AU,
        (SELECT qty FROM tblwip WHERE wip = tblwipdet.wip) AS Qty,
        CASE 
            WHEN tblcwoserialnumbers.numericalpath NOT LIKE '%2%' THEN 0
            ELSE (SELECT qty FROM tblwip WHERE wip = tblwipdet.wip)
        END AS QtyCorte,
        tblwipdet.wip 
    FROM tblwipdet
    INNER JOIN tblcwoserialnumbers ON tblwipdet.wireid = tblcwoserialnumbers.wireid
    WHERE tblwipdet.cwo <> '0' 
      AND tblwipdet.wip IN (SELECT wip FROM tblwip WHERE status = 'Open' AND (wsort > 20 AND wsort < 75)) 
      AND MaqA = 'MM' 
      AND tblwipdet.PWOA IS NULL

    UNION ALL

    SELECT 
        tblwipdet.termb AS Terminal, 
        tblwipdet.AU,
        (SELECT qty FROM tblwip WHERE wip = tblwipdet.wip) AS Qty,
        CASE 
            WHEN tblcwoserialnumbers.numericalpath NOT LIKE '%2%' THEN 0
            ELSE (SELECT qty FROM tblwip WHERE wip = tblwipdet.wip)
        END AS QtyCorte,
        tblwipdet.wip 
    FROM tblwipdet
    INNER JOIN tblcwoserialnumbers ON tblwipdet.wireid = tblcwoserialnumbers.wireid
    WHERE tblwipdet.cwo <> '0' 
      AND tblwipdet.wip IN (SELECT wip FROM tblwip WHERE status = 'Open' AND (wsort > 20 AND wsort < 75)) 
      AND Maqb = 'MM' 
      AND tblwipdet.PWOb IS NULL
),
ResultadoTerminales AS (
    SELECT 
        Terminal, 
        AU, 
        wip,
        SUM(Qty) AS TotalQty,
        SUM(QtyCorte) AS TotalQtyCorte
    FROM CombinedResults
    GROUP BY Terminal, wip, AU
)
-- Consulta principal
SELECT 
    Terminal,  
    SUM(TotalQty) AS [TotalQty],
    SUM(TotalQtyCorte) AS [QtySinCortar],
    '' AS [Pagodas],
    (
        SELECT TOP 1 cell 
        FROM tblToolCribAplicators App 
        INNER JOIN tblToolCribAplicatorsRelationsWithTerminals AppTerm 
            ON App.AplPn = AppTerm.Serie 
        WHERE AppTerm.TerminalPN = Terminal
    ) AS [Celda]
FROM ResultadoTerminales
GROUP BY Terminal
ORDER BY TotalQty DESC;
;

"
            'select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 20 {If(AU = 0, " ", $" and AU={AU}")}
            Dim len = consulta.Length
            Dim dr As SqlDataReader
            Dim aTable As New DataTable
            Dim cmd As SqlCommand = New SqlCommand(consulta, cnn)
            cmd.CommandTimeout = 480000
            cnn.Open()
            dr = cmd.ExecuteReader
            aTable.Load(dr)
            cnn.Close()
            If aTable.Rows.Count > 0 Then
                With dgvTerminalesXProcesar
                    .DataSource = aTable
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .ClearSelection()
                End With
            Else
                dgvTerminalesXProcesar.DataSource = Nothing
                MessageBox.Show("No hay terminales por procesar.")
            End If
            Label6.Text = "Records: " + dgvTerminalesXProcesar.Rows.Count.ToString
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub CleanningState()
        dgvPNTermsProcess.DataSource = Nothing
        Label8.Text = "Number of terminals 
selected: " + InfoTablas.Count.ToString
        Label5.Text = "Records:"
        Label10.Text = "-"
        Label9.Text = "-"
        Label3.Text = "-"
        Label4.Text = "-"
        ChPushToFirstPlace.Checked = False
    End Sub
    Private Sub MainRecordClean()
        Label6.Text = "Records:"
        dgvTerminalesXProcesar.DataSource = Nothing
        dgvDetalleTerminales.DataSource = Nothing
        InfoTablas.Clear()
    End Sub
    Private Sub SelectedTermProcess(PN As String, Cell As String, Balance As Integer)
        Try
            If Cell = "AMARILLO" Or Cell = "YEL" Then
                Cell = "YEL"
            ElseIf Cell = "AZUL" Then
                Cell = "BLU"
            ElseIf Cell = "VERDE" Then
                Cell = "GRN"
            ElseIf Cell = "PURPURA" Then
                Cell = "PUR"
            End If
            Dim aConsulta As String = $"select AU,WIP,Wire,TermA,TABalance,TermB,TBBalance,CONVERT(float,ROUND(CAST(((case when TermA = '{PN}' and MaqA='MM' then TABalance else 0 end + case when TermB = '{PN}' and MaqB='MM' then TBBalance else 0 end) * 7) AS DECIMAL(18,0)) / 60,2,1)) [Test],'{Cell}' [Celda],WireID,MaqA,MaqB from tblWipDet" &
                                      $" where ((TermA = '{PN}' and MaqA='MM') or (TermB='{PN}' and MaqB='MM')) and" &
                                      $" WIP in (select WIP from tblWIP where Status='OPEN'  {If(AU = 0, " ", $" and AU={AU}")}) 
                                      and ((PWOA is null and MaqA='MM') or (PWOB is null and MaqB='MM'))" &
                                      "order by IDSort asc"
            Dim cmd As SqlCommand = New SqlCommand(aConsulta, cnn)
            Dim aTable As DataTable = New DataTable, tbAux As DataTable = New DataTable
            Dim dr As SqlDataReader
            cnn.Open()
            dr = cmd.ExecuteReader
            aTable.Load(dr)
            cnn.Close()
            If aTable.Rows.Count > 0 Then
                If dgvDetalleTerminales.Rows.Count > 0 Then
                    If InfoTablas.Count > 0 Then
                        tbAux = MergedObjTables(aTable, dgvDetalleTerminales)
                        dgvDetalleTerminales.DataSource = tbAux
                        dgvDetalleTerminales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                        dgvDetalleTerminales.AutoResizeColumns()
                        dgvDetalleTerminales.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        dgvDetalleTerminales.ClearSelection()
                        Dim last = (From d In InfoTablas Order By d.Rows Descending Select d.Rows).First()
                        FillListInfoTablas(PN, last + aTable.Rows.Count, Cell, Balance)
                        SumQtyAndTest(tbAux)
                        If dgvDetalleTerminales.Rows.Count - 1 >= 0 Then dgvDetalleTerminales.FirstDisplayedScrollingRowIndex = dgvDetalleTerminales.Rows.Count - 1
                    End If
                Else
                    With dgvDetalleTerminales
                        .DataSource = Nothing
                        .DataSource = aTable
                        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .ClearSelection()
                        FillListInfoTablas(PN, aTable.Rows.Count, Cell, Balance)
                        SumQtyAndTest(aTable)
                        If .Rows.Count - 1 >= 0 Then .FirstDisplayedScrollingRowIndex = .Rows.Count - 1
                    End With
                End If
            Else
                dgvDetalleTerminales.DataSource = Nothing
            End If
            DisableButton()
            FillColorGrid()
            ChargeSpecsTerms()
            Label5.Text = "Records: " + dgvDetalleTerminales.Rows.Count.ToString
            Label8.Text = "Number of terminals 
selected: " + InfoTablas.Count.ToString
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub ChargeSpecsTerms()
        Try
            With dgvPNTermsProcess
                Dim auxList = InfoTablas.[Select](Function(i) New With {
                                                      i.PN,
                                                      i.Balance,
                                                      i.RunTime
                                                   }).ToList()
                .DataSource = auxList
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ClearSelection()
                Dim SumRunTime = InfoTablas.[Select](Function(i) i.RunTime).Sum(Function(a) a)
                Dim SetupTime As Integer = InfoTablas.Count * 5
                Label10.Text = SumRunTime
                Label9.Text = SetupTime
                Label4.Text = SumRunTime + SetupTime
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub

    Private MergedObjTables As Func(Of DataTable, DataGridView, DataTable) = Function(x, y)
                                                                                 Dim dt As New DataTable
                                                                                 dt = DirectCast(y.DataSource, DataTable)
                                                                                 dt.Merge(x, True, MissingSchemaAction.AddWithKey)
                                                                                 Return dt
                                                                             End Function
    Private SumQtyAndTest As Action(Of DataTable) = Function(a)
                                                        Try
                                                            Dim Test = 0
                                                            Dim objT = a.AsEnumerable()
                                                            Test = (From row In a.AsEnumerable() Select row("Test")).ToList().Sum(Function(i) CInt(i.ToString()))
                                                            Label3.Text = InfoTablas.Sum(Function(o) o.Balance)
                                                            Label4.Text = Test.ToString
                                                        Catch ex As Exception
                                                            MessageBox.Show(ex.ToString)
                                                        End Try
                                                        Return Nothing
                                                    End Function
    Private FillListInfoTablas As Action(Of String, Integer, String, Integer) = Function(b, c, d, e)
                                                                                    Dim oCreate As New ChargeInfo With {
                                                                                        .PN = b,
                                                                                        .Rows = c,
                                                                                        .Cell = d,
                                                                                        .Balance = e,
                                                                                        .RunTime = e * 7 / 60
                                                                                    }
                                                                                    InfoTablas.Add(oCreate)
                                                                                    Return Nothing
                                                                                End Function
    Private Sub FillColorGrid()
        If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
            Dim auxRow As Integer = 0
            Dim First = (From d In InfoTablas  Order By d.Rows Ascending  Select d.Rows).First()
            InfoTablas.ForEach(
                Function(Term)
                    Dim CountInteraction As Integer = 0
                    For Each row As DataGridViewRow In dgvDetalleTerminales.Rows
                        CountInteraction += 1
                        If First = Term.Rows Then
                            auxRow = Term.Rows
                            If CountInteraction >= 1 And CountInteraction <= Term.Rows Then
                                If Term.PN.Equals(row.Cells(3).Value) Then
                                    row.Cells(3).Style.BackColor = Color.LightBlue
                                End If
                                If Term.PN.Equals(row.Cells(5).Value) Then
                                    row.Cells(5).Style.BackColor = Color.LightBlue
                                End If
                                If CountInteraction = Term.Rows Then
                                    Exit For
                                Else
                                    Continue For
                                End If
                            End If
                        ElseIf auxRow < Term.Rows Then
                            If CountInteraction > auxRow And CountInteraction <= Term.Rows Then
                                If Term.PN.Equals(row.Cells(3).Value) Then
                                    row.Cells(3).Style.BackColor = Color.LightBlue
                                End If
                                If Term.PN.Equals(row.Cells(5).Value) Then
                                    row.Cells(5).Style.BackColor = Color.LightBlue
                                End If
                                If CountInteraction = Term.Rows Then
                                    auxRow = Term.Rows
                                    Exit For
                                Else
                                    Continue For
                                End If
                            End If
                        End If
                    Next
                    Return Nothing
                End Function
                )
        End If
    End Sub
    Private Sub DisableButton()
        Button3.Visible = dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 1
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Cursor.Current = Cursors.WaitCursor
            If IsNumeric(TextBox1.Text) Then
                AU = TextBox1.Text
                If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
                    Dim res = MessageBox.Show("Ya se estan visualizando terminales en por procesar, si deseas dejar de visualizar las terminales seleccionadas, pulsa Yes, si deseas continuar pulsa no.", "Recargando informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If res = 6 Then
                        dgvDetalleTerminales.DataSource = Nothing
                        InfoTablas.Clear()
                        ChargeGrid()
                        CleanningState()
                    End If
                Else
                    ChargeGrid()
                    CleanningState()
                End If
            Else
                MessageBox.Show("El AU ingresado no es valido.")
                dgvTerminalesXProcesar.DataSource = Nothing
            End If
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub dgvTerminalesXProcesar_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTerminalesXProcesar.CellDoubleClick
        If e.RowIndex >= 0 Then
            Cursor.Current = Cursors.WaitCursor
            If e.ColumnIndex = 0 Then
                Dim auxTerm As String = dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("Terminal").Value.ToString
                Dim auxCell As String = dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("Celda").Value.ToString
                Dim auxBalance As Integer = dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("TotalQty").Value.ToString
                If InfoTablas.Where(Function(Terminal) Terminal.PN.Equals(auxTerm)).Count > 0 Then
                    MessageBox.Show($"Esta terminal: {auxTerm} ya esta en la lista en por procesar, no es posible agregarla de nuevo.")
                Else
                    SelectedTermProcess(auxTerm, auxCell, auxBalance)
                End If
            ElseIf e.ColumnIndex = 3 Then
                Dim lbmTermsCheck As Action(Of String, Boolean) = Function(Cell, flag)
                                                                      For Each rows As DataGridViewRow In dgvTerminalesXProcesar.Rows
                                                                          If rows.Cells("Celda").Value.ToString.Equals(Cell) Then
                                                                              If flag Then
                                                                                  If Not InfoTablas.Where(Function(Terminal) Terminal.PN.Equals(rows.Cells("Term").Value.ToString)).Count > 0 Then
                                                                                      SelectedTermProcess(rows.Cells("Term").Value.ToString, rows.Cells("Celda").Value.ToString, rows.Cells("Qty").Value.ToString)
                                                                                  End If
                                                                              Else
                                                                                  SelectedTermProcess(rows.Cells("Term").Value.ToString, rows.Cells("Celda").Value.ToString, rows.Cells("Qty").Value.ToString)
                                                                              End If
                                                                          End If
                                                                      Next
                                                                      Return Nothing
                                                                  End Function
                Dim resp As Integer
                Dim CellSelected As String = dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("Celda").Value.ToString
                If dgvDetalleTerminales.Rows.Count > 0 Then
                    resp = MessageBox.Show($"Seleccionaste terminales agrupadas por celda, pero ya existen terminales en consultas, deseas agregar las ya seleccionadas? si es asi pulsa Yes, de lo contrario, pulsa No", "Agrupacion por Celda", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If Not resp = 6 Then
                        dgvDetalleTerminales.DataSource = Nothing
                        dgvPNTermsProcess.DataSource = Nothing
                        dgvPNTermsProcess.ClearSelection()
                        InfoTablas.Clear()
                        Label5.Text = "Records: " + dgvDetalleTerminales.Rows.Count.ToString
                        Label8.Text = "Number of terminals 
selected: " + InfoTablas.Count.ToString
                        DisableButton()
                    End If
                End If
                lbmTermsCheck(CellSelected, resp = 6)
            End If
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Eliminar ultima terminal seleccionada
        Cursor.Current = Cursors.WaitCursor
        Dim changesInGrid As Boolean = False
        Dim dt As New DataTable
        dt = DirectCast(dgvDetalleTerminales.DataSource, DataTable)
        Dim countLastRow As Integer = 0

        Dim CheckDt As Func(Of String, Integer) = Function(PnItem)
                                                      For i = 0 To dt.Rows.Count - 1
                                                          If (dt.Rows(i).Item("TermA").ToString = PnItem And dt.Rows(i).Item("MaqA").ToString = "MM") Or (dt.Rows(i).Item("TermB").ToString = PnItem And dt.Rows(i).Item("MaqB").ToString = "MM") Then
                                                              countLastRow += 1
                                                          End If
                                                      Next
                                                      Return countLastRow
                                                  End Function
        Dim FillGrid As Action(Of DataTable) = Function(dtObj)
                                                   With dgvDetalleTerminales
                                                       .DataSource = Nothing
                                                       .DataSource = dtObj
                                                       .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                                                       .AutoResizeColumns()
                                                       .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                                       .ClearSelection()
                                                       If .Rows.Count - 1 >= 0 Then .FirstDisplayedScrollingRowIndex = .Rows.Count - 1
                                                   End With
                                                   Return Nothing
                                               End Function

        If PartNumber = "" Then
            Dim last = (From d In InfoTablas Order By d.Rows Descending Select d.Rows).First()
            InfoTablas.RemoveAt(InfoTablas.FindIndex(Function(d) d.Rows.Equals(last)))
            last = (From d In InfoTablas Order By d.Rows Descending Select d.Rows).First()
            For i = 0 To dt.Rows.Count - 1
                If i >= last Then
                    dt.Rows(i).Delete()
                End If
            Next
            dt.AcceptChanges()
        Else
            dt.Clear()
            dt.AcceptChanges()
            InfoTablas.RemoveAt(InfoTablas.FindIndex(Function(d) d.PN.Equals(PartNumber)))
            Dim interactionCount As Integer = 0
            InfoTablas.ForEach(Function(Term)
                                   Dim aConsulta As String = $"select AU,WIP,Wire,TermA,TABalance,TermB,TBBalance,CONVERT(float,ROUND(CAST(((case when TermA = '{Term.PN}' and MaqA='MM' then TABalance else 0 end + case when TermB = '{Term.PN}' and MaqB='MM' then TBBalance else 0 end) * 7) AS DECIMAL(18,0)) / 60,2,1)) [Test],'{Term.Cell}' [Celda],WireID,MaqA,MaqB from tblWipDet" &
                                                                 $" where ((TermA = '{Term.PN}' and MaqA='MM') or (TermB='{Term.PN}' and MaqB='MM')) and" &
                                                                 $" WIP in (select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 {If(AU = 0, " ", $" and AU={AU}")}) 
                                                                 and ((PWOA is null and MaqA='MM') or (PWOB is null and MaqB='MM'))" &
                                                                 "order by IDSort asc"
                                   Dim cmd As SqlCommand = New SqlCommand(aConsulta, cnn)
                                   Dim aTable As DataTable = New DataTable, tbAux As DataTable = New DataTable
                                   Dim dr As SqlDataReader
                                   cnn.Open()
                                   dr = cmd.ExecuteReader
                                   aTable.Load(dr)
                                   cnn.Close()
                                   If aTable.Rows.Count > 0 Then
                                       If dt.Rows.Count > 0 Then
                                           dt.Merge(aTable, True, MissingSchemaAction.AddWithKey)
                                       Else
                                           dt = aTable.Copy()
                                       End If
                                       interactionCount += 1
                                   End If
                                   Return Nothing
                               End Function)

            InfoTablas.ForEach(Function(item)
                                   item.Rows = CheckDt(item.PN)
                                   Return Nothing
                               End Function)

            PartNumber = ""
        End If

        FillGrid(dt)
        SumQtyAndTest(dt)

        DisableButton()
        FillColorGrid()
        ChargeSpecsTerms()
        Label5.Text = "Records: " + dgvDetalleTerminales.Rows.Count.ToString
        Label8.Text = "Number of terminals 
selected: " + InfoTablas.Count.ToString
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click 'Create PWO
        Cursor.Current = Cursors.WaitCursor
        If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
            Dim NewPwo As New CreateWorkOrder(dgvDetalleTerminales, InfoTablas) With {
                ._PushFirstPlace = ChPushToFirstPlace.Checked
            }
            NewPwo.MaterialReserve()
            If NewPwo.GetSetWorkOrder() Then
                Dim PWO = NewPwo.SerialPWO
                MessageBox.Show($"Se a creado con exito el PWO: {PWO}")
                Dim Report As New ReportePWO(PWO.ToString) With {
                    .Text = PWO.ToString
                }
                Report.ShowDialog()
                NewPwo.MaterialReserve(False)
                MainRecordClean()
                DisableButton()
                CleanningState()
            Else
                MessageBox.Show("Hubo un problema al crear PWO, contacta con ingenieria o sistemas para resolver el problema.")
            End If
        Else
            MessageBox.Show("Seleccione primero una terminal para poder crear PWO.")
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Button3_MouseHover(sender As Object, e As EventArgs) Handles Button3.MouseHover
        Cursor.Current = Cursors.Hand
    End Sub
    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles Button3.MouseLeave
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Button1_MouseHover(sender As Object, e As EventArgs)
        Cursor.Current = Cursors.Hand
    End Sub
    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs)
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub dgvDetalleTerminales_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles dgvDetalleTerminales.ColumnAdded
        e.Column.SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Report As New ReportePWO(TextBox2.Text) With {
                    .Text = TextBox2.Text.ToString
                }
        Report.ShowDialog()
    End Sub
    Private Sub CreatePWO_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If dgvPNTermsProcess.Rows.Count > 0 Then
            Dim res = MessageBox.Show("Ya se estan visualizando terminales en por procesar, si deseas salir, pulsa Yes, si deseas continuar pulsa no.", "Cerrando Creacion PWO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If Not res = 6 Then
                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub dgvTerminalesXProcesar_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvTerminalesXProcesar.CellMouseMove
        Dim Encabezado As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            Encabezado = dgvTerminalesXProcesar.Columns(cdx).HeaderText
        End If
        If Encabezado = "Term" Or Encabezado = "Celda" Then
            Cursor.Current = Cursors.Hand
        End If
    End Sub
    Private Sub dgvTerminalesXProcesar_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTerminalesXProcesar.CellMouseLeave
        Dim Encabezado As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            Encabezado = dgvTerminalesXProcesar.Columns(cdx).HeaderText
        End If
        If Encabezado = "Term" Or Encabezado = "Celda" Then
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub dgvPNTermsProcess_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNTermsProcess.CellDoubleClick
        If e.RowIndex >= 0 Then
            PartNumber = dgvPNTermsProcess.Rows(e.RowIndex).Cells(0).Value.ToString
            dgvPNTermsProcess.Rows(e.RowIndex).Selected = True
            Button3.Visible = True
        End If
    End Sub

    Private Sub CreatePWO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor
        GetDataByUser()
        Cursor.Current = Cursors.Default
    End Sub
End Class