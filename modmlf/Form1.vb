Imports System.Data.SqlClient
Imports System.Reflection
Imports Microsoft.Office.Interop
'Imports System.Reflection
'Imports System.Data.OleDb
'Imports System.Data
'Imports System
'Imports System.IO
''Imports System.Deployment.Application
'Imports Telerik.WinControls.UI
'Imports Telerik.WinControls
'Imports System.Net
'Imports System.DirectoryServices
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class Principal
    Public tblasig As New DataTable
    Private tblUsersPriv As New DataTable
    Dim oh, alm, piso, rework As Decimal
    'Public WithEvents FSW As New System.IO.FileSystemWatcher
    Private bgWorker As New BackgroundWorker
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor
        LoadStart()
        Inicio()
        Cursor.Current = Cursors.Default
    End Sub
    Sub Inicio()
        If opcion = 1 Then
            pnluserandtitle.BackColor = Color.LightGreen
            dgvWips.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvwSorts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvMatSinStockCompras.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvAfectados.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            lbldept.Text = "Corte"
            rbsolicitar.Checked = True
            Button2.BackColor = Color.LightGreen
            ToolStripMenuItem10.Visible = False
            TabPage2.Visible = False
            TabPage2.Parent = Nothing
            btnCortosPN.Visible = False
        ElseIf opcion = 2 Then
            pnluserandtitle.BackColor = Color.LightBlue
            dgvWips.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue
            dgvwSorts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue
            dgvMatSinStockCompras.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue
            dgvAfectados.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue
            lbldept.Text = "Almacen"
            rbListosParaEntrar.Checked = True
            ToolStripMenuItem10.Visible = False
            TabPage2.Visible = True
            TabPage2.Parent = TabControl1
            Button2.BackColor = Color.LightBlue
            cargadatosCompras()
        ElseIf opcion = 3 Then
            pnluserandtitle.BackColor = Color.LightGray
            dgvWips.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
            dgvwSorts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
            lbldept.Text = "Aplicadores"
            rbListosParaEntrar.Checked = True
            ToolStripMenuItem10.Visible = False
            TabPage2.Visible = False
            TabPage2.Parent = Nothing
            Button2.BackColor = Color.LightGray
            btnCortosPN.Visible = False
        ElseIf opcion = 4 Then
            pnluserandtitle.BackColor = Color.LightGreen
            dgvWips.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvwSorts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            lbldept.Text = "XP"
            rbsolicitar.Checked = True
            ToolStripMenuItem10.Visible = False
            TabPage2.Visible = False
            TabPage2.Parent = Nothing
            Button2.BackColor = Color.LightGreen
            btnCortosPN.Visible = False
        ElseIf opcion = 5 Then
            pnluserandtitle.BackColor = Color.Bisque
            dgvWips.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque
            dgvwSorts.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque
            dgvMatSinStockCompras.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque
            dgvAfectados.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque
            lbldept.Text = "Compras"
            dgvMatSinStockCompras.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque
            dgvMatSinStockCompras.Visible = True
            ToolStripMenuItem10.Visible = True
            TabPage2.Visible = True
            TabPage2.Parent = TabControl1
            TabControl1.SelectedTab = TabPage2
            rdbOnHold.Checked = True
            Button2.BackColor = Color.Bisque
            btnCortosPN.Visible = False
            cargadatosCompras()
            Timer3.Enabled = True
            Timer3.Interval = 1800000
        ElseIf opcion = 6 Then
            pnluserandtitle.BackColor = Color.LightGreen
            dgvWips.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvwSorts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvMatSinStockCompras.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvAfectados.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            lbldept.Text = "Planeacion Corte"
            ToolStripMenuItem10.Visible = True
            rbListosParaEntrar.Checked = True
            TabPage2.Visible = True
            TabPage2.Parent = TabControl1
            Button2.BackColor = Color.LightGreen
            btnCortosPN.Visible = False
            cargadatosCompras()
        ElseIf opcion = 7 Then
            pnluserandtitle.BackColor = Color.LightGreen
            dgvWips.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvwSorts.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvMatSinStockCompras.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            dgvAfectados.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen
            lbldept.Text = "Planeacion XP"
            ToolStripMenuItem10.Visible = True
            rbListosParaEntrar.Checked = True
            TabPage2.Visible = True
            TabPage2.Parent = TabControl1
            Button2.BackColor = Color.LightGreen
            btnCortosPN.Visible = False
            cargadatosCompras()
        End If
    End Sub
    Private Function cargausername(user As String) As String
        Try
            user = LTrim(RTrim(user))
            user = user.Replace(".", " ")
            Return user
        Catch ex As Exception
            CorreoFalla.EnviaCorreoFalla("cargausername", host, UserName)
            Return Nothing
        End Try
    End Function
    Sub LoadStart()
        lblwelcome.Text = "Bienvenido: " & cargausername(UserName.ToString)
        Label4.Text = "Semana Actual: " & nsemana
        ToolStripMenuItem6.Visible = False
        GroupBox2.Visible = False
        GroupBox3.Visible = False
        Timer1.Enabled = True
        Timer1.Interval = 3000
        cargachart()
        GridCharge()
        'AutoUpdate()
    End Sub
    Private Sub llenagrid(query As String)
        Dim tabla As New DataTable
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 600000
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If tabla.Rows.Count > 0 Then
                With dgvWips
                    .DataSource = tabla
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("DueDateProcess").DefaultCellStyle.Format = ("dd-MMM-yy")
                    .Columns("DateCreatedWIP").DefaultCellStyle.Format = ("dd-MMM-yy")
                    If opcion = 5 Then
                        .Columns("Fecha Materiales despues de Hold").DefaultCellStyle.Format = ("dd-MMM-yy")
                        .Columns("Fecha Materiales").DefaultCellStyle.Format = ("dd-MMM-yy")
                    End If
                    .Columns(0).Frozen = True
                    .Columns(1).Frozen = True
                    Label3.Text = "Items: " & dgvWips.Rows.Count
                    btnRefrescaGrid.Visible = True
                End With
                pintaceldas()
            Else
                dgvWips.DataSource = Nothing
                Label3.Text = "Items: " & dgvWips.Rows.Count
            End If
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias", "Error llenagrid")
            CorreoFalla.EnviaCorreoFalla("llenagrid", host, UserName)
        End Try
    End Sub
    Private Sub GridCharge()
        Dim systemType As Type
        Dim propertyInfo As PropertyInfo
        systemType = dgvWips.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvWips, True, Nothing)
        '--------------------------------------------
        systemType = dgvMatSinStockCompras.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvMatSinStockCompras, True, Nothing)
        '--------------------------------------------
        systemType = DataGridView1.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(DataGridView1, True, Nothing)
        '--------------------------------------------
        systemType = dgvwSorts.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvwSorts, True, Nothing)
        '--------------------------------------------
        systemType = dgvAfectados.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvAfectados, True, Nothing)
    End Sub
    Private Sub cargadatosCompras()
        GroupBox3.Visible = False
        GroupBox2.Visible = False
        Dim i As Integer
        Dim Cuenta As Integer = 1
        Dim QtyTerm, QtySello, QtyWire As Integer
        Dim Allocated, InTransit, AllocatedFiltro As Decimal
        Dim termBuscar As String
        Dim wip As String = "(select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) and (ConfirmacionAlm='OnHold'))"
        Dim Query As String = "SELECT tblBOMCWO.PN AS ComponentPN, 
                               0 AS QTY, 
                               0 AS OH, '' AS ' ', 
                               0 AS ALM, 
                               0 AS PISO, 
                               0 AS [Re-Work], '' AS ' ', 
                               0 AS 'Allocated', 
                               0 AS Diff, 
                               0 AS DifIncComp, 
                               (SELECT SUM(QtyBalance) FROM tblItemsPOsDet AS A INNER JOIN tblItemsPOs AS B ON A.IDPO = B.IDPO WHERE A.PN = tblBOMCWO.PN AND A.QtyBalance > 0 AND B.Status = 'OPEN') AS InTransit,
                               (SELECT TOP(1) JuarezDueDate FROM tblItemsPOsDet AS A INNER JOIN tblItemsPOs AS B ON A.IDPO = B.IDPO WHERE A.PN = tblBOMCWO.PN AND A.QtyBalance > 0 AND B.Status = 'OPEN'  
                               ORDER BY A.JuarezDueDate) AS NextFReciboMat, 
                               tblBOMCWO.WIP,AU,(select Sem from tblWIP where tblWIP.WIP=tblBOMCWO.WIP) [Sem],CWO,(select MAX(ProcFDisMat) from tblBOMWIP where tblBOMWIP.WIP = tblBOMCWO.WIP and tblBOMWIP.PN=tblBOMCWO.PN) [Fecha promesa PN] 
                               FROM tblBOMCWO 
                               WHERE 
                               tblBOMCWO.WIP IN (select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO 
                               where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) and (ConfirmacionAlm='OnHold')) and tblBOMCWO.Hold=1 ORDER BY ComponentPN"

        Using DTtermDist As New DataTable
            Try
                cmd = New SqlCommand(Query, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                DTtermDist.Load(dr)
                edo = cnn.State.ToString
                If edo = "Open" Then cnn.Close()
                Try
                    Dim diff As Integer = 0
                    wip = "(WIP in " & wip & ")"
                    ProgressBar1.Maximum = DTtermDist.Rows.Count
                    ProgressBar1.Visible = True
                    ProgressBar1.BringToFront()
                    For i = 0 To DTtermDist.Rows.Count - 1
                        QtyTerm = 0
                        QtySello = 0
                        QtyWire = 0
                        oh = 0
                        alm = 0
                        piso = 0
                        rework = 0
                        Allocated = 0
                        AllocatedFiltro = 0
                        InTransit = 0
                        diff = 0
                        termBuscar = ""
                        termBuscar = DTtermDist.Rows(i).Item("ComponentPN").ToString
                        InTransit = CDec(Val(DTtermDist.Rows(i).Item("InTransit").ToString))
                        oHQty(termBuscar)
                        If Microsoft.VisualBasic.Left(termBuscar, 1) = "T" Then
                            QtyTerm += TermAQty(termBuscar, wip)
                            QtyTerm += TermBQty(termBuscar, wip)
                            Allocated = AllocatedAQty("select SUM(TABalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND TABalance > 0  AND TermA = '" & termBuscar & "' and CWO <> '0'")
                            Allocated += AllocatedAQty("select SUM(TBBalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND TBBalance > 0  AND TermB = '" & termBuscar & "' and CWO <> '0'")

                            AllocatedFiltro = AllocatedAQty("select SUM(TABalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND  " & wip & " AND TABalance > 0  AND TermA = '" & termBuscar & "' and CWO <> '0'")
                            AllocatedFiltro += AllocatedAQty("select SUM(TBBalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND  " & wip & " AND TBBalance > 0  AND TermB = '" & termBuscar & "' and CWO <> '0'")
                        ElseIf Microsoft.VisualBasic.Left(termBuscar, 1) = "W" Or Microsoft.VisualBasic.Left(termBuscar, 1) = "C" Then
                            QtyWire += WireQty(termBuscar, wip)
                            Allocated = AllocatedAQty("select SUM(WireBalance * LengthWire) * 0.0032808 AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (2,3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND WireBalance > 0 AND CWO <> '0' AND Wire =  '" & termBuscar & "'")
                            AllocatedFiltro = AllocatedAQty("select SUM(WireBalance * LengthWire) * 0.0032808 AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND " & wip & " AND WireBalance > 0  AND Wire =  '" & termBuscar & "' and CWO <> '0'")
                        End If
                        diff = (oh - (QtyTerm + QtyWire) - Allocated) + AllocatedFiltro
                        DTtermDist.Columns(1).ReadOnly = False
                        DTtermDist.Rows(i).Item(1) = QtyTerm + QtyWire 'Qty
                        DTtermDist.Columns(2).ReadOnly = False
                        DTtermDist.Rows(i).Item(2) = oh 'OnHand
                        DTtermDist.Columns(4).ReadOnly = False
                        DTtermDist.Rows(i).Item(4) = alm 'Almacen
                        DTtermDist.Columns(5).ReadOnly = False
                        DTtermDist.Rows(i).Item(5) = piso 'En Piso
                        DTtermDist.Columns(6).ReadOnly = False
                        DTtermDist.Rows(i).Item(6) = rework 'Rework
                        DTtermDist.Columns(8).ReadOnly = False
                        DTtermDist.Rows(i).Item(8) = Allocated 'Allocated
                        DTtermDist.Columns(9).ReadOnly = False
                        DTtermDist.Rows(i).Item(9) = diff
                        DTtermDist.Columns(10).ReadOnly = False
                        DTtermDist.Rows(i).Item(10) = diff + InTransit 'DifIncComp
                        ProgressBar1.Value = Cuenta
                        Application.DoEvents()
                        Cuenta += 1
                    Next

                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
                tblasig.Clear()
                tblasig = DTtermDist
                dgvMatSinStockCompras.DataSource = DTtermDist
                If dgvMatSinStockCompras.RowCount > 0 Then
                    dgvMatSinStockCompras.Columns("NextFReciboMat").DefaultCellStyle.Format = ("dd-MMM-yy")
                    dgvMatSinStockCompras.Columns("Fecha promesa PN").DefaultCellStyle.Format = ("dd-MMM-yy")
                    dgvMatSinStockCompras.AutoResizeColumns() 'ajustamos el tamaño de las columnas
                    dgvMatSinStockCompras.Columns(4).Width = 5
                    dgvMatSinStockCompras.Columns(8).Width = 5
                    dgvMatSinStockCompras.Columns(1).Frozen = True
                    If opcion = 5 Then
                        dgvMatSinStockCompras.Columns("Chk").Visible = True
                        btnAgregaFecha.Visible = True
                    Else
                        dgvMatSinStockCompras.Columns("Chk").Visible = False
                    End If
                    dgvMatSinStockCompras.ClearSelection()
                    PintarMateriales()
                    lblitemscortos.Text = "Items: " + dgvMatSinStockCompras.Rows.Count.ToString
                    btnexportaeficc.Visible = True
                Else
                    dgvMatSinStockCompras.DataSource = Nothing
                    lblitemscortos.Text = "Items: " + dgvMatSinStockCompras.Rows.Count.ToString
                    If opcion = 5 Or opcion = 2 Then MessageBox.Show("No hay materiales con faltantes", "Materiales sin stock")
                End If
                ProgressBar1.Visible = False
            Catch ex As Exception
                MessageBox.Show("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias", "Error Loading Terminales")
                CorreoFalla.EnviaCorreoFalla("cargadatosCompras", host, UserName)
            End Try
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
        End Using
    End Sub
    Private Function TermAQty(TermA As String, subQuery As String) As Integer
        Dim Qty As Integer = 0
        Dim Query As String = "SELECT SUM(TABalance) As Qty FROM tblWipDet where TABalance > 0 AND TermA = @TermA AND " & subQuery
        Try
            cmd = New SqlCommand(Query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@TermA", SqlDbType.NVarChar).Value = TermA
            cnn.Open()
            Qty = If(IsDBNull(cmd.ExecuteScalar()), 0, CInt(Val(cmd.ExecuteScalar())))
            Return Qty
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias", "Error Loading TermA")
            CorreoFalla.EnviaCorreoFalla("TermAQty", host, UserName)
            Return 0
        Finally
            cnn.Close()
        End Try
    End Function
    Private Function TermBQty(TermB As String, subQuery As String) As Integer
        Dim Qty As Integer = 0
        Dim Query As String = "SELECT SUM(TBBalance) As Qty FROM tblWipDet where TBBalance > 0 AND TermB = @TermB AND " & subQuery
        Try
            cmd = New SqlCommand(Query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@TermB", SqlDbType.NVarChar).Value = TermB
            cnn.Open()
            Qty = If(IsDBNull(cmd.ExecuteScalar()), 0, CInt(Val(cmd.ExecuteScalar())))
            Return Qty
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias", "Error Loading TermB")
            CorreoFalla.EnviaCorreoFalla("TermBQty", host, UserName)
            Return 0
        Finally
            cnn.Close()
        End Try
    End Function
    Public Function AllocatedAQty(Query As String) As Integer
        Dim Qty As Integer = 0
        Try
            cmd = New SqlCommand(Query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            Qty = If(IsDBNull(cmd.ExecuteScalar()), 0, CInt(Val(cmd.ExecuteScalar())))
            Return Qty
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias", "Error Loading Allocated")
            CorreoFalla.EnviaCorreoFalla("AllocatedAQty", host, UserName)
            Return 0
        Finally
            cnn.Close()
        End Try
    End Function
    Private Function WireQty(Wire As String, subQuery As String) As Integer
        Dim Qty As Integer = 0
        Dim Query As String = "select SUM(WireBalance * LengthWire) * 0.0032808 AS Qty From tblWipDet where WireBalance > 0 AND Wire = @Wire AND " & subQuery
        Try
            cmd = New SqlCommand(Query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@Wire", SqlDbType.NVarChar).Value = Wire
            cnn.Open()
            Qty = If(IsDBNull(cmd.ExecuteScalar()), 0, CInt(Val(cmd.ExecuteScalar())))
            Return Qty
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias", "Error Loading WireQty")
            CorreoFalla.EnviaCorreoFalla("WireQty", host, UserName)
            Return 0
        Finally
            cnn.Close()
        End Try
    End Function
    Private Sub oHQty(PN As String)
        Dim Qty As Integer = 0
        Dim dtOH As New DataTable()
        dtOH.Clear()
        Dim Query As String = "SELECT DISTINCT Z.PN,
        (SELECT SUM(M.Balance) FROM tblItemsTags M WHERE M.PN=Z.PN AND M.Status='Available' ) AS QtyWarehouse,
        (SELECT SUM(N.Balance) FROM tblItemsTags N WHERE N.PN=Z.PN AND N.Status<>'Available' AND N.Status<>'Cancel' AND N.Status<>'Close' AND N.Status<>'Rework') AS QtyEnPiso,
        (SELECT SUM(O.Balance) FROM tblItemsTags O WHERE O.PN=Z.PN AND O.Status='Rework') AS QtyEnRework,
        (SELECT SUM(R.Balance) FROM tblItemsTags AS R WHERE R.PN=Z.PN AND (R.Status='NPI' OR R.Status='NoAvailable' OR R.Status='Available' OR R.Status='Rework')) AS QtyOnHand
        FROM tblItemsTags AS Z WHERE Z.Balance <> 0 AND Z.PN = @PN
        GROUP BY TAG, PN, Location, SubPN, Qty, ID, PO, Unit, Status, CreatedDate, ContainerName, OutDate, InDate, AssignedTo"
        Try
            cmd = New SqlCommand(Query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN
            cnn.Open()
            dr = cmd.ExecuteReader
            dtOH.Load(dr)
            If dtOH.Rows.Count > 0 Then
                oh = CDec(Val(dtOH.Rows(0).Item("QtyOnHand").ToString()))
                alm = CDec(Val(dtOH.Rows(0).Item("QtyWarehouse").ToString()))
                piso = CDec(Val(dtOH.Rows(0).Item("QtyEnPiso").ToString()))
                rework = CDec(Val(dtOH.Rows(0).Item("QtyEnRework").ToString()))
            Else
                oh = 0
                alm = 0
                piso = 0
                rework = 0
            End If
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias", "Error Loading OH")
            CorreoFalla.EnviaCorreoFalla("oHQty", host, UserName)
            oh = 0
            alm = 0
            piso = 0
            rework = 0
        Finally
            cnn.Close()
        End Try
    End Sub
    Public Sub PintarMateriales()
        Try
            Dim i As Integer
            Dim AUant As String = ""
            Dim PN As String = ""
            For i = 0 To dgvMatSinStockCompras.RowCount - 1
                dgvMatSinStockCompras.Rows(i).Cells(4).Style.BackColor = Color.DeepSkyBlue
                dgvMatSinStockCompras.Rows(i).Cells(8).Style.BackColor = Color.DeepSkyBlue
                If CDec(Val(dgvMatSinStockCompras.Rows(i).Cells("Diff").Value.ToString)) >= 0 Then
                    dgvMatSinStockCompras.Rows(i).Cells("Diff").Style.BackColor = Color.Lime
                    dgvMatSinStockCompras.Rows(i).Cells("Diff").Style.Font = New Font(Font, FontStyle.Bold)
                Else
                    dgvMatSinStockCompras.Rows(i).Cells("Diff").Style.BackColor = Color.Red
                    dgvMatSinStockCompras.Rows(i).Cells("Diff").Style.Font = New Font(Font, FontStyle.Bold)
                End If
                If CDec(Val(dgvMatSinStockCompras.Rows(i).Cells("DifIncComp").Value.ToString)) >= 0 Then
                    dgvMatSinStockCompras.Rows(i).Cells("DifIncComp").Style.BackColor = Color.Lime
                    dgvMatSinStockCompras.Rows(i).Cells("DifIncComp").Style.Font = New Font(Font, FontStyle.Bold)
                Else
                    dgvMatSinStockCompras.Rows(i).Cells("DifIncComp").Style.BackColor = Color.Red
                    dgvMatSinStockCompras.Rows(i).Cells("DifIncComp").Style.Font = New Font(Font, FontStyle.Bold)
                End If
                PN = dgvMatSinStockCompras.Rows(i).Cells("ComponentPN").Value.ToString
                If Microsoft.VisualBasic.Left(PN, 1) <> "W" Then
                    If AllocatedAQty("select COUNT(PnOriginal) AS QTY from TblDesviacionesTerm WHERE PnOriginal = '" & PN & "'") > 0 Then
                        dgvMatSinStockCompras.Rows(i).Cells("ComponentPN").Style.BackColor = Color.MintCream
                        dgvMatSinStockCompras.Rows(i).Cells("ComponentPN").Style.ForeColor = Color.DodgerBlue
                        dgvMatSinStockCompras.Rows(i).Cells("ComponentPN").Style.Font = New Font(Font, FontStyle.Bold)
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("PintarMateriales", host, UserName)
        End Try
    End Sub
    Private Sub CheckValidaciones(PN As String)
        Try
            Dim dtOH As New DataTable
            query = "SELECT DISTINCT Z.PN," &
                     " (SELECT SUM(M.Balance) FROM tblItemsTags M WHERE M.PN=Z.PN AND M.Status='Available' ) AS QtyWarehouse," &
                     " (SELECT SUM(N.Balance) FROM tblItemsTags N WHERE N.PN=Z.PN AND N.Status<>'Available' AND N.Status<>'Cancel' AND N.Status<>'Close' AND N.Status<>'Rework') AS QtyEnPiso," &
                     " (SELECT SUM(O.Balance) FROM tblItemsTags O WHERE O.PN=Z.PN AND O.Status='Rework') AS QtyEnRework," &
                     " (SELECT SUM(R.Balance) FROM tblItemsTags AS R WHERE R.PN=Z.PN AND (R.Status='NPI' OR R.Status='NoAvailable' OR R.Status='Available' OR R.Status='Rework')) AS QtyOnHand" &
                     " FROM tblItemsTags AS Z WHERE Z.Balance <> 0 AND Z.PN in (SELECT DISTINCT PnValido FROM tblDesviacionesTerm where PnOriginal = @PN)" &
                     " GROUP BY TAG, PN, Location, SubPN, Qty, ID, PO, Unit, Status, CreatedDate, ContainerName, OutDate, InDate, AssignedTo"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN
            cnn.Open()
            dr = cmd.ExecuteReader
            dtOH.Load(dr)
            cnn.Close()
            If dtOH.Rows.Count > 0 Then
                dgvMatSinStockCompras.DataSource = dtOH
                dgvMatSinStockCompras.AutoResizeColumns()
                dgvMatSinStockCompras.ClearSelection()
                dgvMatSinStockCompras.Columns("Chk").Visible = False
                Label8.Text = $"Numeros validados para {PN}"
                btnRefrescaGrid.Visible = True
            Else
                MsgBox($"No hay validaciones para {PN}")
                dgvMatSinStockCompras.DataSource = tblasig
                If opcion = 5 Then
                    dgvMatSinStockCompras.Columns("Chk").Visible = True
                Else
                    dgvMatSinStockCompras.Columns("Chk").Visible = False
                End If
            End If
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("CheckValidaciones", host, UserName)
        End Try
    End Sub
    Private Sub cargachart()
        If ColaGrafica = False Then
            Me.Chart1.Series("Carga en proceso de confirmacion").Points.Clear()
            Me.Chart1.Series("Carga Maquinas por entrar").Points.Clear()
            Me.Chart1.Series("Carga Actual").Points.Clear()
            Dim consulta As String
            Dim getMaqActives As String = "select Maq,0 [Proceso de confirmacion],0 [Listos para entrar], 0 [En corte] from tblMaqRates where Active = 1 order by CONVERT(int,Maq) asc"
            Dim getTableMaqs As DataTable = New DataTable(), dr1 As SqlDataReader
            Dim cm As SqlCommand = New SqlCommand(getMaqActives, conex)
            cm.CommandType = CommandType.Text
            conex.Open()
            dr1 = cm.ExecuteReader
            getTableMaqs.Load(dr1)
            conex.Close()
            getTableMaqs.Columns(1).ReadOnly = False
            getTableMaqs.Columns(2).ReadOnly = False
            getTableMaqs.Columns(3).ReadOnly = False

            '' Carga de maquinas por entrar
            consulta = "SELECT MR.Maq, isnull(convert(int, (SUM(Tsetup) /60 + sum(TRuntime)/60)),0) AS 'TotalTime' 
FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq /*INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO*/ INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 /*AND D.NumericalPath LIKE '2%'*/ and E.WSort =20 GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
            Using tabla As New DataTable
                Try
                    Dim cmdo As SqlCommand = New SqlCommand(consulta, conex)
                    cmdo.CommandType = CommandType.Text
                    Dim data As SqlDataReader
                    conex.Open()
                    data = cmdo.ExecuteReader
                    If data.HasRows Then
                        While data.Read()
                            For i = 0 To getTableMaqs.Rows.Count - 1
                                If data.GetValue(0) = getTableMaqs.Rows(i).Item("Maq").ToString Then
                                    getTableMaqs.Rows(i).Item(2) = data.GetValue(1)
                                    Exit For
                                End If
                            Next
                        End While
                    Else
                        conex.Close()
                    End If
                    conex.Close()
                Catch ex As Exception
                    conex.Close()
                    CorreoFalla.EnviaCorreoFalla("cargachart", host, UserName)
                    cargachart()
                End Try
            End Using
            'Carga de maquinas en proceso de corte
            consulta = "SELECT MR.Maq,ISNULL(convert(int, (SUM(c.TSetup) / 60 + sum(c.TRuntime) / 60)),0) [TotalTime]
        FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO INNER JOIN tblWipDet as C ON C.WireID = D.WireID INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
        WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (25,29,26) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
            Using tabla2 As New DataTable
                Try
                    Dim cmdo1 As SqlCommand = New SqlCommand(consulta, conex)
                    cmdo1.CommandType = CommandType.Text
                    Dim data1 As SqlDataReader
                    conex.Open()
                    data1 = cmdo1.ExecuteReader
                    If data1.HasRows Then
                        While data1.Read()
                            For i = 0 To getTableMaqs.Rows.Count - 1
                                If data1.GetValue(0) = getTableMaqs.Rows(i).Item("Maq").ToString Then
                                    getTableMaqs.Rows(i).Item(3) = data1.GetValue(1)
                                    Exit For
                                End If
                            Next
                        End While
                    Else
                        conex.Close()
                    End If
                    conex.Close()
                Catch ex As Exception
                    conex.Close()
                    CorreoFalla.EnviaCorreoFalla("cargachart", host, UserName)
                    cargachart()
                End Try
            End Using
            ' Carga de maquinas en proceso de confirmacion
            consulta = "SELECT MR.Maq, isnull(convert(int, (SUM(Tsetup) /60 + sum(TRuntime)/60)),0) AS 'TotalTime' 
                    FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
                    WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (3,9,11,12,13,14,27) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
            Using tabla As New DataTable
                Try
                    Dim cmdo2 As SqlCommand = New SqlCommand(consulta, conex)
                    cmdo2.CommandType = CommandType.Text
                    Dim data3 As SqlDataReader
                    conex.Open()
                    data3 = cmdo2.ExecuteReader
                    If data3.HasRows Then
                        While data3.Read()
                            For i = 0 To getTableMaqs.Rows.Count - 1
                                If data3.GetValue(0) = getTableMaqs.Rows(i).Item("Maq").ToString Then
                                    getTableMaqs.Rows(i).Item(1) = data3.GetValue(1)
                                    Exit For
                                End If
                            Next
                        End While
                    Else
                        conex.Close()
                    End If
                    conex.Close()
                Catch ex As Exception
                    conex.Close()
                    CorreoFalla.EnviaCorreoFalla("cargachart", host, UserName)
                    cargachart()
                End Try
            End Using
            '-------------------------------------------------------------------------
            With Me.Chart1
                .ChartAreas(0).AxisX.MajorGrid.LineWidth = 0
                .Series("Carga Maquinas por entrar").IsValueShownAsLabel = True
                .Series("Carga Maquinas por entrar").IsVisibleInLegend = True
                .Series("Carga Actual").IsValueShownAsLabel = True
                .Series("Carga en proceso de confirmacion").IsValueShownAsLabel = True
                .Titles.Clear()
                .Titles.Add("Grafico Carga de Maquinas").Font = New System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
                If getTableMaqs.Rows.Count > 0 Then
                    For j = 0 To getTableMaqs.Rows.Count - 1
                        .Series("Carga Maquinas por entrar").Points.AddXY(getTableMaqs.Rows(j).Item("Maq").ToString, getTableMaqs.Rows(j).Item("Listos para entrar").ToString)
                        .Series("Carga Actual").Points.AddXY(getTableMaqs.Rows(j).Item("Maq").ToString, getTableMaqs.Rows(j).Item("En corte").ToString)
                        .Series("Carga en proceso de confirmacion").Points.AddXY(getTableMaqs.Rows(j).Item("Maq").ToString, getTableMaqs.Rows(j).Item("Proceso de confirmacion").ToString)
                    Next
                End If
            End With
            ' ------------------------------------------------------------------
        End If
    End Sub
    Private Sub pintaceldas()
        If rbsolicitar.Checked = True Or rdbOnHold.Checked = True Or rbListosParaEntrar.Checked = True Then
            For Each linea As DataGridViewRow In dgvWips.Rows
                If linea.Cells(6).Value = 27 Or linea.Cells(7).Value = 27 Then
                    linea.DefaultCellStyle.BackColor = Color.Orange
                    linea.DefaultCellStyle.ForeColor = Color.White
                ElseIf linea.Cells(7).Value = 12 Or linea.Cells(7).Value = 14 Then
                    linea.Cells(6).Style.BackColor = Color.Red
                    linea.Cells(6).Style.ForeColor = Color.Black
                    linea.Cells(6).Style.Font = New Font(Font, FontStyle.Bold)
                    linea.Cells(7).Style.BackColor = Color.Red
                    linea.Cells(7).Style.ForeColor = Color.Black
                    linea.Cells(7).Style.Font = New Font(Font, FontStyle.Bold)
                ElseIf linea.Cells(7).Value = 3 Or linea.Cells(7).Value = 9 Or linea.Cells(7).Value = 11 Or linea.Cells(7).Value = 13 Then
                    linea.Cells(6).Style.BackColor = Color.Green
                    linea.Cells(6).Style.ForeColor = Color.Black
                    linea.Cells(6).Style.Font = New Font(Font, FontStyle.Bold)
                    linea.Cells(7).Style.BackColor = Color.Green
                    linea.Cells(7).Style.ForeColor = Color.Black
                    linea.Cells(7).Style.Font = New Font(Font, FontStyle.Bold)
                End If
            Next
        End If
        If (opcion = 6 Or opcion = 7 Or opcion = 8) And rbListosParaEntrar.Checked = True Then
            Dim t As New DataTable
            Try
                Dim cmdo1 As SqlCommand = New SqlCommand("select distinct CWO from tblCWO where Wsort = 20 and (PrintedSNCWO is null or PrintedSNCWO=0)", cnn)
                cmdo1.CommandType = CommandType.Text
                Dim data1 As SqlDataReader
                cnn.Open()
                data1 = cmdo1.ExecuteReader
                t.Load(data1)
                cnn.Close()
                If t.Rows.Count > 0 Then
                    If dgvWips.Rows.Count > 0 Then
                        For i As Integer = 0 To t.Rows.Count - 1
                            Dim comparacion = t.Rows(i).Item("CWO").ToString
                            For a As Integer = 0 To dgvWips.Rows.Count - 1
                                If comparacion = dgvWips.Rows(a).Cells(1).Value.ToString Then
                                    dgvWips.Rows(a).Cells(1).Style.BackColor = Color.Pink
                                End If
                            Next
                        Next
                    End If
                End If
            Catch ex As Exception
                cnn.Close()
                CorreoFalla.EnviaCorreoFalla("CWO sin imprimir", host, UserName)
            End Try
        End If
    End Sub
    Private Sub ToolStripTextBox1_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox1.Click
        If WIP <> "" And CWO <> "" Then
            Dim ver As Char = CWO(0)
            Dim ver2 As Char = WIP(0)
            If ver = "C" And ver2 = "W" Then
                If opcion = 1 Or opcion = 4 Or opcion = 6 Or opcion = 7 Then
                    actualizafecharequerimiento(CWO)
                End If
            Else
                MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
            End If
        End If
        ContextMenuDisponibilidad.Close()
        filtros(1)
    End Sub
    Private Sub dgvWips_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvWips.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Try
                    dgvWips.CurrentCell = dgvWips.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    dgvWips.Rows(e.RowIndex).Selected = True
                    WIP = Convert.ToString(dgvWips.Rows(e.RowIndex).Cells(0).Value)
                    CWO = Convert.ToString(dgvWips.Rows(e.RowIndex).Cells(1).Value)
                    sort = Convert.ToString(dgvWips.Rows(e.RowIndex).Cells(6).Value)
                    maq = Convert.ToString(dgvWips.Rows(e.RowIndex).Cells(3).Value)
                    If Not rbsolicitar.Checked = True And Not rbEmpezadosyDetenidos.Checked = True Then
                        If dgvWips.Rows(e.RowIndex).Cells(2).Value.ToString = "" Then
                            cola = 0
                        Else
                            cola = Convert.ToString(dgvWips.Rows(e.RowIndex).Cells(2).Value)
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
                    CorreoFalla.EnviaCorreoFalla("dgvWips_CellMouseDown", host, UserName)
                End Try
            End If
        End If
    End Sub
    Private Sub dgvWips_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvWips.MouseClick
        If dgvWips.RowCount > 0 Then
            If opcion = 1 Or opcion = 4 Then
                If rbsolicitar.Checked = True And (sort = 27 Or sort = 3) Then
                    If e.Button = System.Windows.Forms.MouseButtons.Right Then
                        ContextMenuDisponibilidad.Show(Cursor.Position.X, Cursor.Position.Y)
                        ToolStripTextBox1.Visible = True
                        ToolStripMenuItem2.Visible = False
                        ToolStripMenuItem4.Visible = False
                        ToolStripMenuItem7.Visible = False
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem15.Visible = False
                    End If
                ElseIf (rbSolicitado.Checked = True Or rbListosParaEntrar.Checked = True Or rbYaempezados.Checked = True) And (opcion = 1 Or opcion = 4) Then
                    If e.Button = System.Windows.Forms.MouseButtons.Right Then
                        ContextMenuDisponibilidad.Show(Cursor.Position.X, Cursor.Position.Y)
                        ToolStripTextBox1.Visible = False
                        ToolStripMenuItem2.Visible = False
                        ToolStripMenuItem4.Visible = True
                        ToolStripMenuItem7.Visible = True
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem15.Visible = False
                    End If
                ElseIf rbEmpezadosyDetenidos.Checked = True Then
                    If e.Button = System.Windows.Forms.MouseButtons.Right Then
                        ContextMenuDisponibilidad.Show(Cursor.Position.X, Cursor.Position.Y)
                        ToolStripTextBox1.Visible = False
                        ToolStripMenuItem2.Visible = False
                        ToolStripMenuItem4.Visible = True
                        ToolStripMenuItem7.Visible = True
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem15.Visible = False
                    End If
                End If
            ElseIf opcion = 6 Or opcion = 7 Then
                If e.Button = System.Windows.Forms.MouseButtons.Right Then
                    ContextMenuDisponibilidad.Show(Cursor.Position.X, Cursor.Position.Y)
                    If rbsolicitar.Checked = True Then
                        ToolStripTextBox1.Visible = True
                        ToolStripMenuItem7.Visible = False
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem2.Visible = True
                        ToolStripMenuItem4.Visible = False
                        ToolStripMenuItem15.Visible = False
                    ElseIf rbSolicitado.Checked = True Then
                        ToolStripTextBox1.Visible = False
                        ToolStripMenuItem2.Visible = True
                        ToolStripMenuItem4.Visible = True
                        ToolStripMenuItem7.Visible = True
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem15.Visible = True
                    ElseIf rbYaempezados.Checked = True Then
                        ToolStripTextBox1.Visible = False
                        ToolStripMenuItem2.Visible = True
                        ToolStripMenuItem4.Visible = False
                        ToolStripMenuItem7.Visible = True
                        ToolStripMenuItem8.Visible = True
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem15.Visible = True
                    ElseIf rbEmpezadosyDetenidos.Checked = True Then
                        ToolStripTextBox1.Visible = False
                        ToolStripMenuItem2.Visible = False
                        ToolStripMenuItem4.Visible = True
                        ToolStripMenuItem7.Visible = False
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = True
                        ToolStripMenuItem15.Visible = True
                    ElseIf rbListosParaEntrar.Checked = True Then
                        ToolStripTextBox1.Visible = False
                        ToolStripMenuItem2.Visible = True
                        ToolStripMenuItem4.Visible = True
                        ToolStripMenuItem7.Visible = True
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem15.Visible = True
                    ElseIf rdbOnHold.Checked = True Then
                        ToolStripTextBox1.Visible = False
                        ToolStripMenuItem2.Visible = True
                        ToolStripMenuItem4.Visible = True
                        ToolStripMenuItem7.Visible = True
                        ToolStripMenuItem8.Visible = False
                        ToolStripMenuItem9.Visible = False
                        ToolStripMenuItem15.Visible = True
                    End If
                End If
            ElseIf opcion = 2 Then
                If (rbSolicitado.Checked = True Or rdbOnHold.Checked = True) And dgvWips.Rows.Count > 0 Then
                    If e.Button = System.Windows.Forms.MouseButtons.Right Then
                        ContextMenuVerMW.Show(Cursor.Position.X, Cursor.Position.Y)
                        ToolStripMenuItem14.Visible = False
                        If sort = 12 Then
                            ToolStripMenuItem12.Visible = True
                        Else
                            ToolStripMenuItem12.Visible = False
                        End If
                        ToolStripMenuItem13.Visible = True
                        ToolStripTextBox5.Visible = True
                        ToolStripTextBox6.Visible = False
                        ToolStripTextBox7.Visible = False
                        If sort = 20 Or sort = 11 Or sort = 13 Or sort = 25 Then
                            AsignarMaterialToolStripMenuItem.Visible = True
                        Else
                            AsignarMaterialToolStripMenuItem.Visible = False
                        End If
                        ToolStripMenuItem15.Visible = False
                        DesviarTerminalToolStripMenuItem.Visible = False
                    End If
                ElseIf (rbListosParaEntrar.Checked = True Or rbYaempezados.Checked = True) Then
                    If e.Button = System.Windows.Forms.MouseButtons.Right Then
                        ContextMenuVerMW.Show(Cursor.Position.X, Cursor.Position.Y)
                        ToolStripMenuItem14.Visible = False
                        If sort = 12 Then
                            ToolStripMenuItem12.Visible = True
                            ToolStripMenuItem13.Visible = False
                        ElseIf sort = 14 Then
                            ToolStripMenuItem13.Visible = False
                            ToolStripMenuItem12.Visible = False
                        Else
                            ToolStripMenuItem12.Visible = False
                            ToolStripMenuItem13.Visible = True
                        End If
                        ToolStripTextBox5.Visible = True
                        ToolStripTextBox6.Visible = False
                        ToolStripTextBox7.Visible = False
                        AsignarMaterialToolStripMenuItem.Visible = True
                        ToolStripMenuItem15.Visible = False
                        DesviarTerminalToolStripMenuItem.Visible = False
                    End If
                End If
            ElseIf opcion = 3 And sort <> 27 Then
                If (rbListosParaEntrar.Checked = True Or rbYaempezados.Checked = True) And dgvWips.Rows.Count > 0 Then
                    If e.Button = System.Windows.Forms.MouseButtons.Right Then
                        ContextMenuVerMW.Show(Cursor.Position.X, Cursor.Position.Y)
                        ToolStripMenuItem14.Visible = False
                        If sort = 14 Then
                            ToolStripMenuItem12.Visible = True
                            ToolStripMenuItem13.Visible = False
                        ElseIf sort = 12 Then
                            ToolStripMenuItem13.Visible = False
                            ToolStripMenuItem12.Visible = False
                        Else
                            ToolStripMenuItem12.Visible = False
                            ToolStripMenuItem13.Visible = True
                        End If
                        ToolStripTextBox5.Visible = False
                        ToolStripTextBox6.Visible = True
                        ToolStripTextBox7.Visible = False
                        AsignarMaterialToolStripMenuItem.Visible = False
                        ToolStripMenuItem15.Visible = False
                        DesviarTerminalToolStripMenuItem.Visible = True
                    End If
                End If
            ElseIf opcion = 5 Then
                If rdbOnHold.Checked = True And dgvWips.Rows.Count > 0 Then
                    If e.Button = System.Windows.Forms.MouseButtons.Right Then
                        ContextMenuVerMW.Show(Cursor.Position.X, Cursor.Position.Y)
                        ToolStripMenuItem14.Visible = False
                        ToolStripMenuItem12.Visible = False
                        ToolStripMenuItem13.Visible = False
                        ToolStripTextBox5.Visible = True
                        ToolStripTextBox6.Visible = False
                        AsignarMaterialToolStripMenuItem.Visible = False
                        ToolStripTextBox7.Visible = False
                        ToolStripMenuItem15.Visible = False
                        DesviarTerminalToolStripMenuItem.Visible = False
                    End If
                End If
            End If
        End If
    End Sub 'En este poner lo que vaya a ver compras en onhold
    Public Sub DetenerCWO(CWO As String, notes As String, flag As Integer)
        Try
            Dim query As String = ""
            If flag = 1 Then
                query = "update tblCWO set Notes='Detenido', Wsort = 24, Id=null where CWO= @CWO"
            ElseIf flag = 2 Then
                Dim sortNew As AutomaticSort = New AutomaticSort(maq)
                query = "update tblCWO set Wsort = 25,Notes = case notes when 'Detenido' then null else notes end,Id=" + sortNew.GetSort().ToString + " where CWO= @CWO"
            End If
            Using cmd As New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
            End Using
            If flag = 1 Then
                Dim ReOrd As AutomaticSort = New AutomaticSort(maq, cola)
                ReOrd.ReOrderSort()
                If Not ReOrd.CheckZeros() Then
                    ReOrd.RemoveZeros()
                End If
            End If
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@notes,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            cmd.Parameters.Add("@notes", SqlDbType.NVarChar).Value = notes
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@notes,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = WIP
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            cmd.Parameters.Add("@notes", SqlDbType.NVarChar).Value = notes + " CWO: " + CWO
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            NotesInWIP(WIP, notes, "")
            If rbYaempezados.Checked = True Then filtros(4)
            If rbEmpezadosyDetenidos.Checked = True Then filtros(5)
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("DetenerCWO", host, UserName)
        End Try
    End Sub
    Public Sub actualizafecharequerimiento(CWO As String)
        Try
            Dim Idsort As AutomaticSort = New AutomaticSort(maq), query As String, aux As Integer
            If sort = 27 Then
                query = "update tblCWO set Id= @requerimiento, dateSolicitud = GETDATE() where CWO= @CWO"
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
                aux = Idsort.GetSort()
                cmd.Parameters.Add("@requerimiento", SqlDbType.Int).Value = aux
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Se ha asignado requerimiento para el CWO: " + CWO.ToString + ", su numero de fila es " + aux.ToString + "", MsgBoxStyle.OkOnly)
                ' ---------------------------
                Using cmd As New SqlCommand("update tblMLFNotifications set SendReceive=1,TypeOfNotify=2 where Dep in ('Almacen','Aplicadores')", cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                End Using
                ' ---------------------------
                query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,'Solicitado por Corte 1era vez',GETDATE(),'MLF')"
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
                cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,'Solicitado por Corte 1era vez, CWO= " + CWO + "',GETDATE(),'MLF')"
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = WIP
                cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                NotesInWIP(WIP, "Solicitado por Corte 1era vez, CWO= " + CWO + "", Convert.ToDateTime(Now))
            Else
                query = "update tblCWO set Id= @requerimiento, dateSolicitud = GETDATE() where CWO= @CWO"
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
                aux = Idsort.GetSort()
                cmd.Parameters.Add("@requerimiento", SqlDbType.Int).Value = aux
                cnn.Open()
                dr = cmd.ExecuteReader
                cnn.Close()
                MsgBox("Se ha asignado requerimiento para el CWO: " + CWO.ToString + ", su numero de fila es " + aux.ToString + "", MsgBoxStyle.OkOnly)
                ' ---------------------------
                Using cmd As New SqlCommand("update tblMLFNotifications set SendReceive=1,TypeOfNotify=1 where Dep in ('Almacen','Aplicadores')", cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                End Using
                ' ---------------------------
                query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,'Solicitado por Corte 1era vez',GETDATE(),'MLF')"
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
                cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,'Solicitado por Corte 1era vez, CWO= " + CWO + "',GETDATE(),'MLF')"
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = WIP
                cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                NotesInWIP(WIP, "Solicitado por Corte 1era vez, CWO= " + CWO + "", Convert.ToDateTime(Now))
            End If
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("actualizafecharequerimiento", host, UserName)
        End Try
        '*********
        poneokalm_apl(CWO, WIP)
        filtros(1)
    End Sub
    Public Sub poneokalm_apl(cwo As String, wip As String)
        If opcion = 1 Or opcion = 4 Or opcion = 6 Or opcion = 7 Then
            cmd = New SqlCommand("update tblCWO set WSort = 20,dateSolicitud= GETDATE() where CWO =@CWO", cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = cwo
            cnn.Open()
            dr = cmd.ExecuteReader
            cnn.Close()
            ' ---------------------------
            Using cmd As New SqlCommand("update tblMLFNotifications set SendReceive=1,TypeOfNotify=5 where Dep='Corte'", cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
            End Using
            ' ---------------------------
            wSortWIPAndACorte(wip)
            If rbsolicitar.Checked = True Then filtros(1)
            If rbSolicitado.Checked = True Then filtros(2)
            If rbListosParaEntrar.Checked = True Then filtros(3)
            If rbYaempezados.Checked = True Then filtros(4)
            If rbEmpezadosyDetenidos.Checked = True Then filtros(5)
            If rdbOnHold.Checked = True Then filtros(6)
        ElseIf opcion = 2 Or opcion = 3 Then
            Dim query As String = "update tblCWO 
set WSort = case when (select COUNT(Cutting) from tblCWOSerialNumbers where CWO=@CWO and Cutting is not null) = 0 then 20
when (select COUNT(Cutting) from tblCWOSerialNumbers where CWO=@CWO and Cutting is not null) > 0 then 25 
else 25 end,
ConfirmacionAlm= case ConfirmacionAlm when 'OnHold' then 'Confirmado' else ConfirmacionAlm end,
ConfirmacionApl= case ConfirmacionApl when 'OnHold' then 'Confirmado' else ConfirmacionApl end 
where CWO =@CWO"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = cwo
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            If opcion = 2 Then
                query = "update tblBOMCWO set Hold=0 where CWO='" + cwo + "' and Hold=1"
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
            End If
            cmd = New SqlCommand("update tblWIP set WSort = case when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort >= 25) = (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP)) then 25 when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort < 25) = (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP)) then 20 when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort = 12) >= 1 then 12 when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort = 14) >= 1 then 14 else 25 end where WIP =@WIP", cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = wip
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            If rbsolicitar.Checked = True Then filtros(1)
            If rbSolicitado.Checked = True Then filtros(2)
            If rbListosParaEntrar.Checked = True Then filtros(3)
            If rbYaempezados.Checked = True Then filtros(4)
            If rbEmpezadosyDetenidos.Checked = True Then filtros(5)
            If rdbOnHold.Checked = True Then filtros(6)
        End If
    End Sub
    Private Sub wSortWIPAndACorte(wip As String)
        Dim count As Integer = 0
        Try
            Dim queru As String = ""
            Dim query As String = "select distinct c.cwo,c.wsort from tblWipDet as b inner join tblWIP as a on b.wip=a.wip inner join tblCWO as c on c.cwo=b.cwo where a.WIP=@wip", t As New DataTable
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@wip", SqlDbType.NVarChar).Value = wip
            cnn.Open()
            dr = cmd.ExecuteReader
            t.Load(dr)
            edo = cnn.State.ToString
            cnn.Close()
            Dim wSort = (From row In t.AsEnumerable() Select row("wsort")).Where(Function(d) d >= 20).ToList().Count()
            queru = If(wSort > 0, "update tblWIP set Wsort = case when Wsort=12 then 12 when Wsort=14 then 14 when (select Count(CWO) from tblWipDet where WIP=@wip and CWO = '0') > 0 then 3 when Wsort=20 then Wsort when Wsort=3 then 20 when wsort=25 then wsort when wsort=29 then wsort else 20 end, [A.Corte] = case when [A.Corte] is null then Balance else [A.Corte] end where WIP= @wip", "update tblWIP set Wsort = case when Wsort=12 then 12 when Wsort=14 then 14 when (select Count(CWO) from tblWipDet where WIP=@wip and CWO = '0') > 0 then 3 else 20 end, [A.Corte] = case when [A.Corte] is null and (Corte + MP + Ens + IEns + Shipping + FGSHP + FGSHPEP) < Qty then Qty - (Corte + MP + Ens + IEns + Shipping + FGSHP + FGSHPEP) else [A.Corte] end where WIP= @wip")
            Try
                cmd = New SqlCommand(queru, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@wip", SqlDbType.NVarChar).Value = wip
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
            Catch ex As Exception
                MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
                CorreoFalla.EnviaCorreoFalla("wSortWIPAndACorte", host, UserName)
            End Try
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("wSortWIPAndACorte", host, UserName)
        End Try
    End Sub
    Public Function CheckWSort(CWO As String) As Boolean
        Try
            Dim query As String = $"select WSort from tblCWO where CWO='{CWO}'"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            If CInt(cmd.ExecuteScalar) = 3 Or CInt(cmd.ExecuteScalar) = 12 Or CInt(cmd.ExecuteScalar) = 14 Then
                cnn.Close()
                Return False
            Else
                cnn.Close()
                Return True
            End If
        Catch ex As Exception
            cnn.Close()
            Return False
        End Try
    End Function
    Private Function PNQuitandoCorto(PN As String) As DataTable 'Retorna Tabla para ver allocated y revisar si hay suficiente para poder quitar corto
        Try
            Dim query = $"SELECT DISTINCT Z.PN,
(SELECT ISNULL(SUM(M.Balance),0) FROM tblItemsTags M WHERE M.PN=Z.PN AND M.Status='Available' ) AS QtyWarehouse,
(SELECT ISNULL(SUM(N.Balance),0) FROM tblItemsTags N WHERE N.PN=Z.PN AND N.Status<>'Available' AND N.Status<>'Cancel' AND N.Status<>'Close' AND N.Status<>'Rework') AS QtyEnPiso,
(SELECT ISNULL(SUM(O.Balance),0) FROM tblItemsTags O WHERE O.PN=Z.PN AND O.Status='Rework') AS QtyEnRework,
(SELECT ISNULL(SUM(R.Balance),0) FROM tblItemsTags AS R WHERE R.PN=Z.PN AND (R.Status='NPI' OR R.Status='NoAvailable' OR R.Status='Available' OR R.Status='Rework')) AS QtyOnHand,
0 [QTY],0 [Allocated], 0 [Diff],0[DifIncComp],(SELECT ISNULL(SUM(QtyBalance),0) FROM tblItemsPOsDet AS A INNER JOIN tblItemsPOs AS B ON A.IDPO = B.IDPO WHERE A.PN = Z.PN AND A.QtyBalance > 0 AND B.Status = 'OPEN') [InTransit]
FROM tblItemsTags AS Z WHERE Z.Balance <> 0 AND Z.PN = '{PN}'
GROUP BY TAG, PN, Location, SubPN, Qty, ID, PO, Unit, Status, CreatedDate, ContainerName, OutDate, InDate, AssignedTo"
            Dim aTable As New DataTable()
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            aTable.Load(dr)
            cnn.Close()
            If aTable.Rows.Count > 0 Then
                aTable.Columns(5).ReadOnly = False
                aTable.Columns(6).ReadOnly = False
                aTable.Columns(7).ReadOnly = False
                aTable.Columns(8).ReadOnly = False
                Dim wip As String = "(select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) and (ConfirmacionAlm='OnHold'))"
                wip = "(WIP in " & wip & ")"
                Dim QtyTerm, QtySello, QtyWire As Integer
                Dim Allocated, InTransit, AllocatedFiltro As Decimal
                Dim diff As Integer = 0
                QtyTerm = 0
                QtySello = 0
                QtyWire = 0
                Allocated = 0
                AllocatedFiltro = 0
                InTransit = 0
                oh = CDec(Val(aTable.Rows(0).Item("QtyOnHand").ToString()))
                alm = CDec(Val(aTable.Rows(0).Item("QtyWarehouse").ToString()))
                piso = CDec(Val(aTable.Rows(0).Item("QtyEnPiso").ToString()))
                rework = CDec(Val(aTable.Rows(0).Item("QtyEnRework").ToString()))
                If Microsoft.VisualBasic.Left(PN, 1) = "T" Then
                    QtyTerm += TermAQty(PN, wip)
                    QtyTerm += TermBQty(PN, wip)
                    Allocated = AllocatedAQty("select SUM(TABalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND TABalance > 0  AND TermA = '" & PN & "' and CWO <> '0'")
                    Allocated += AllocatedAQty("select SUM(TBBalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND TBBalance > 0  AND TermB = '" & PN & "' and CWO <> '0'")

                    AllocatedFiltro = AllocatedAQty("select SUM(TABalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND  " & wip & " AND TABalance > 0  AND TermA = '" & PN & "' and CWO <> '0'")
                    AllocatedFiltro += AllocatedAQty("select SUM(TBBalance) AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND  " & wip & " AND TBBalance > 0  AND TermB = '" & PN & "' and CWO <> '0'")
                ElseIf Microsoft.VisualBasic.Left(PN, 1) = "W" Or Microsoft.VisualBasic.Left(PN, 1) = "C" Then
                    QtyWire += WireQty(PN, wip)
                    Allocated = AllocatedAQty("select SUM(WireBalance * LengthWire) * 0.0032808 AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (2,3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND WireBalance > 0 AND CWO <> '0' AND Wire =  '" & PN & "'")
                    AllocatedFiltro = AllocatedAQty("select SUM(WireBalance * LengthWire) * 0.0032808 AS QTY from tblWipDet where WIP in (select WIP from tblwip where Status = 'Open' and wSort IN (3,27,9,12,14,20,25,29) and ProcFDispMat IS NOT NULL) AND " & wip & " AND WireBalance > 0  AND Wire =  '" & PN & "' and CWO <> '0'")
                End If
                diff = (oh - (QtyTerm + QtyWire) - Allocated) + AllocatedFiltro
                aTable.Rows(0).Item(5) = QtyTerm + QtyWire 'Qty
                aTable.Rows(0).Item(6) = Allocated
                aTable.Rows(0).Item(7) = diff
                aTable.Rows(0).Item(8) = diff + InTransit
                Return aTable
            End If
        Catch ex As Exception
            cnn.Close()
            Return Nothing
        End Try
        Return Nothing
    End Function
    Private Sub CheckPN(aTable As DataTable)
        Try
            Dim aPn As String = aTable.Rows(0).Item("PN")
            Dim aQty As Long = aTable.Rows(0).Item("QTY")
            Dim pAllocated As Long = aTable.Rows(0).Item("Allocated")
            Dim aDiff As Long = aTable.Rows(0).Item("Diff")
            Dim aDiffAndTransit As Long = aTable.Rows(0).Item("DifIncComp")
            Dim aQtyOnHand As Long = aTable.Rows(0).Item("QtyOnHand")
            Dim aInTransit As Long = aTable.Rows(0).Item("InTransit")
            If aQtyOnHand > 0 Then
                If aQty > aQtyOnHand Then
                    'Quitar parcial el corto
                    QuitaCortoCWOyWIPxPN(aPn, aQtyOnHand, False)
                    MsgBox("Se ha quitado el corto parcialmente")
                ElseIf aQtyOnHand >= aQty Then
                    'Quitar completo
                    QuitaCortoCWOyWIPxPN(aPn, True)
                    MsgBox("Se ha quitado el corto en su totalidad por la cantidad completa")
                End If
            ElseIf aQtyOnHand = 0 Then
                If aInTransit > 0 Then
                    MsgBox($"Este Numero de parte {aPn} sigue sin stock, se encuentran en transito la cantidad de: {aInTransit}")
                Else
                    MsgBox($"Este Numero de parte {aPn} sigue sin stock, pero sun no hay cantidad en Transito, verifica fecha promesa del numero de parte, si aun no tiene, solicitala a departamente de Compras")
                End If
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub QuitaCortoCWOyWIPxPN(PN As String, Optional ByVal qty As Integer = 0, Optional aTypeParcialorCompleto As Boolean = True)
        Try
            Dim query As String = If(aTypeParcialorCompleto = True, $"select WIP,CWO,PN,Balance from tblBOMCWO where PN='{PN}' and Hold=1", $"select WIP,CWO,PN,Balance from tblBOMCWO where PN='{PN}' and Hold=1 group by wip,cwo,pn,balance having SUM(Balance) <={qty}")
            Dim aTable As New DataTable()
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            aTable.Load(dr)
            cnn.Close()
            If aTable.Rows.Count > 0 Then
                Dim CWOs = (From row In aTable.AsEnumerable() Select row("CWO")).Distinct().ToList()
                If CWOs IsNot Nothing Then
                    CWOs.ForEach(
                        Function(c)
                            UpdateCortoXPn(PN, c)
                            Return Nothing
                        End Function
                                 )
                    CWOs.ForEach(
                    Function(CWO)
                        CheckCortosCWOorWIP(CWO, "CWO")
                        Return Nothing
                    End Function
                    )
                End If
                Dim WIPs = (From row In aTable.AsEnumerable() Select row("WIP")).Distinct().ToList()
                If WIPs IsNot Nothing Then
                    WIPs.ForEach(
                    Function(WIP)
                        CheckCortosCWOorWIP(WIP, "WIP")
                        Return Nothing
                    End Function
                    )
                End If
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub UpdateCortoXPn(PN As String, CWO As String)
        Try
            Dim update As String = $"update tblBOMCWO set Hold=0 where CWO='{CWO}' and PN='{PN}'"
            cmd = New SqlCommand(update, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub CheckCortosCWOorWIP(WO As String, RevWO As String)
        Try
            Dim update As String = ""
            Dim Consulta As String = $"select Count({RevWO}) from tblBOMCWO where {RevWO}='{WO}' and Hold=1"
            cmd = New SqlCommand(Consulta, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            If CInt(cmd.ExecuteScalar) > 0 Then
                cnn.Close()
            ElseIf CInt(cmd.ExecuteScalar) = 0 Then
                cnn.Close()
                If RevWO = "CWO" Then
                    update = "update tblCWO 
                          set WSort = case when (select COUNT(Cutting) from tblCWOSerialNumbers where CWO=@CWO and Cutting is not null) = 0 then 20
                          when (select COUNT(Cutting) from tblCWOSerialNumbers where CWO=@CWO and Cutting is not null) > 0 then 25 
                          else 25 end,
                          ConfirmacionAlm= case ConfirmacionAlm when 'OnHold' then 'Confirmado' else ConfirmacionAlm end,
                          ConfirmacionApl= case ConfirmacionApl when 'OnHold' then 'Confirmado' else ConfirmacionApl end 
                          where CWO =@CWO"
                    cmd = New SqlCommand(update, cnn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = WO
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                ElseIf RevWO = "WIP" Then
                    update = "update tblWIP set WSort = case when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort >= 25) =
                              (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP)) then 25 
                              when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort < 25) = 
                             (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP)) then 20 
                             when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort = 12) >= 1 then 12 
                             when (select COUNT(WSort) from tblCWO where CWO in (select distinct CWO from tblWipDet where WIP=@WIP) and WSort = 14) >= 1 then 14 
                             else 25 end where WIP =@WIP"
                    cmd = New SqlCommand(update, cnn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = WO
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                End If
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub dgvWips_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvWips.ColumnHeaderMouseClick
        pintaceldas()
    End Sub
    Public Sub filtros(a As Integer)
        Dim query = "select distinct w.WIP,c.CWO,c.Id [Orden Corte],c.Maq,w.AU,w.Rev,w.wSort [wSort WIP],c.WSort [wSort CWO],w.Qty,w.KindOfAU,w.Customer, [100SU] + [100RT] [100TL],w.IT,w.PR,w.DueDateProcess,w.CreatedDate [DateCreatedWIP],Sem from tblCWO as c inner join tblWipDet as d on c.CWO=d.CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO"
        Dim cmd As String = ""
        If a = 1 Then
            If opcion = 1 Or opcion = 6 Then 'Solicitar Mat/Apl
                cmd = " where (w.WSort in (3,12,20,27,25)) and (c.Wsort in (3,27)) and c.Status = 'OPEN' and c.Id is null and (w.KindOfAU not like 'XP%' and w.KindOfAU not like '%PPAP%' and w.KindOfAU not like '%Only Cord%') and c.Maq > 0 order by c.Maq desc,c.Id asc"
            ElseIf opcion = 4 Or opcion = 7 Then 'Solicitar Mat/Apl XP
                cmd = " where (w.WSort in (3,12,20,27,25)) and (c.Wsort in (3,27)) and c.Status = 'OPEN' and c.Id is null and (w.KindOfAU like 'XP%' or w.KindOfAU like '%PPAP%' or w.KindOfAU ='Only Cord') order by c.Maq desc,c.Id asc"
            ElseIf opcion = 2 Or opcion = 3 Or opcion = 5 Or opcion = 8 Then 'Solicitar Mat/Apl XP and KoA normal
                cmd = " where (w.WSort in (3,12,20,27,25)) and (c.Wsort in (3,27)) and c.Status = 'OPEN' and c.Id is null order by c.Maq desc,c.Id asc"
            End If
        ElseIf a = 3 Then 'Listos Para Entrar
            If opcion = 1 Or opcion = 6 Then
                cmd = " where c.Wsort in (20,12,14) and c.Wsort in (20,12,14) and c.Status = 'OPEN' and (w.KindOfAU not like 'XP%' and w.KindOfAU not like '%PPAP%' and w.KindOfAU not like '%Only Cord%') and c.Maq > 0 order by c.Maq desc,c.Id asc"
            ElseIf opcion = 4 Or opcion = 7 Then
                cmd = " where c.Wsort in (20,12,14) and c.Wsort in (20,25,12,14) and c.Status = 'OPEN' and (w.KindOfAU like 'XP%' or w.KindOfAU like '%PPAP%' or w.KindOfAU ='Only Cord') order by c.Maq desc,c.Id asc"
            ElseIf opcion = 2 Or opcion = 3 Or opcion = 5 Or opcion = 8 Then
                cmd = " where c.Wsort in (20,12,14) and c.Wsort in (20,12,14) and c.Status = 'OPEN' order by c.CWO /*w.CreatedDate*/"
            End If
        ElseIf a = 4 Then 'Ya empezados
            If opcion = 1 Or opcion = 6 Then
                cmd = " where (w.WSort = 3 or w.WSort = 20 or w.WSort = 25 or w.WSort = 29) and c.wsort = 25 and c.Status = 'OPEN' and (w.KindOfAU not like 'XP%' and w.KindOfAU not like '%PPAP%' and w.KindOfAU not like '%Only Cord%') and c.Maq > 0 order by c.Maq desc,c.Id asc /*w.CreatedDate*/"
            ElseIf opcion = 4 Or opcion = 7 Then
                cmd = " where (w.WSort = 3 or w.WSort = 20 or w.WSort = 25 or w.WSort = 29) and c.wsort = 25 and c.Status = 'OPEN' and (w.KindOfAU like 'XP%' or w.KindOfAU like '%PPAP%' or w.KindOfAU ='Only Cord') order by c.Maq desc,c.Id asc /*w.CreatedDate*/"
            ElseIf opcion = 2 Or opcion = 3 Or opcion = 5 Or opcion = 8 Then
                cmd = " where (w.WSort = 3 or w.WSort = 20 or w.WSort = 25 or w.WSort = 29) and c.wsort = 25 and c.Status = 'OPEN' order by c.Maq desc,c.Id asc /*w.CreatedDate*/"
            End If
        ElseIf a = 5 Then 'Empezados y Detenidos
            If opcion = 1 Or opcion = 6 Then
                cmd = " where (w.WSort = 3 or w.WSort = 20 or w.WSort = 25 or w.WSort = 3) And (c.Wsort = 22 Or c.Wsort = 24) and c.Status = 'OPEN' and (w.KindOfAU not like 'XP%' and w.KindOfAU not like '%PPAP%' and w.KindOfAU not like '%Only Cord%') and c.Maq > 0 order by c.Maq desc,c.Id asc /*w.CreatedDate*/"
            ElseIf opcion = 4 Or opcion = 7 Then
                cmd = " where (w.WSort = 3 or w.WSort = 20 or w.WSort = 25 or w.WSort = 3) And (c.Wsort = 22 Or c.Wsort = 24) and c.Status = 'OPEN' and (w.KindOfAU like 'XP%' or w.KindOfAU like '%PPAP%' or w.KindOfAU ='Only Cord') order by c.Maq desc,c.Id asc /*w.CreatedDate*/"
            ElseIf opcion = 2 Or opcion = 3 Or opcion = 5 Or opcion = 8 Then
                cmd = " where (w.WSort = 3 or w.WSort = 20 or w.WSort = 25 or w.WSort = 3) And (c.Wsort = 22 Or c.Wsort = 24) and c.Status = 'OPEN' order by c.Maq desc,c.Id asc /*w.CreatedDate*/"
            End If
        ElseIf a = 6 Then 'On Hold
            If opcion = 5 Then
                cmd = " where (c.Wsort in (3,27,12,14,20,25)) And (c.Wsort in (27,11,12,13,14,20)) and (ConfirmacionAlm='OnHold') and w.WIP in (select distinct WIP from tblItemsQB as q inner join tblBOMCWO as bc on q.PN=bc.PN where WIP in (select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO where (c.Wsort in (3,27,12,14,20)) And (c.Wsort in (27,11,12,13,14,20)) and (ConfirmacionAlm='OnHold')) and (q.QtyOnHand = 0 or bc.Balance > q.QtyOnHand or bc.Hold=1)) order by c.Maq desc,c.Id asc"
            ElseIf opcion = 4 Or opcion = 7 Then
                cmd = " where (w.WSort = 3 or w.wSort = 27 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort = 27 Or c.Wsort = 12 or c.WSort = 14) and (w.KindOfAU like 'XP%' or w.KindOfAU like '%PPAP%' or w.KindOfAU ='Only Cord') and c.Status = 'OPEN' order by c.Maq desc,c.Id asc"
            ElseIf opcion = 1 Or opcion = 6 Then
                cmd = " where (w.WSort = 3 or w.wSort = 27 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort = 27 Or c.Wsort = 12 or c.WSort = 14) and (w.KindOfAU not like 'XP%' and w.KindOfAU not like '%PPAP%' and w.KindOfAU not like '%Only Cord%') and c.Maq > 0 and c.Status = 'OPEN' order by c.Maq desc,c.Id asc"
            Else
                cmd = " where (w.WSort = 3 or w.wSort = 27 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort = 27 Or c.Wsort = 12 or c.WSort = 14) and c.Status = 'OPEN' order by c.Maq desc,c.Id asc /*w.CreatedDate*/"
            End If
        End If
        If opcion = 5 Then
            query = "select distinct w.WIP,c.CWO,c.Id [Orden Corte],c.Maq,w.AU,w.Rev,w.wSort [wSort WIP],c.WSort [wSort CWO],w.Qty,w.KindOfAU,w.Customer, [100SU] + [100RT] [100TL],w.IT,w.PR,w.DueDateProcess,w.CreatedDate [DateCreatedWIP],Sem,ProcFDispMat [Fecha Materiales],ProcNotas [Notas Compras],ProcFDispMat2 [Fecha Materiales despues de Hold] from tblCWO as c inner join tblWipDet as d on c.CWO=d.CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO"
            query += cmd
        Else
            query += cmd
        End If
        llenagrid(query)
    End Sub
    Private Sub rbsolicitar_CheckedChanged(sender As Object, e As EventArgs) Handles rbsolicitar.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If rbsolicitar.Checked = True Then
            filtros(1)
            lblwsortasig.Text = "-"
            lblWIPorCWO.Text = "-"
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub rbListosParaEntrar_CheckedChanged(sender As Object, e As EventArgs) Handles rbListosParaEntrar.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If rbListosParaEntrar.Checked = True Then
            filtros(3)
            lblwsortasig.Text = "-"
            lblWIPorCWO.Text = "-"
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub rbYaempezados_CheckedChanged(sender As Object, e As EventArgs) Handles rbYaempezados.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If rbYaempezados.Checked = True Then
            filtros(4)
            lblwsortasig.Text = "-"
            lblWIPorCWO.Text = "-"
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub rbEmpezadosyDetenidos_CheckedChanged(sender As Object, e As EventArgs) Handles rbEmpezadosyDetenidos.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If rbEmpezadosyDetenidos.Checked = True Then
            filtros(5)
            lblwsortasig.Text = "-"
            lblWIPorCWO.Text = "-"
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub rdbOnHold_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOnHold.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If rdbOnHold.Checked = True Then
            filtros(6)
            lblwsortasig.Text = "-"
            lblWIPorCWO.Text = "-"
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Function wsorts(w As Integer) As String
        Try
            query = "select ISNULL(Definition,'Sin categoria') from tblWsorts where Wsort=@w"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@w", SqlDbType.NVarChar).Value = w
            cnn.Open()
            Return CStr(cmd.ExecuteScalar)
            cnn.Close()
        Catch ex As Exception
            Return Nothing
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("wsorts", host, UserName)
        Finally
            cnn.Close()
        End Try
    End Function
    Public Sub notesWIPandCWOquitaOnHoldde26(notes As String, cwo As String, wip As String)
        Try
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@note,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = cwo
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@note,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = wip
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes + " CWO: " + cwo
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            NotesInWIP(wip, notes + " CWO: " + cwo, "")
            query = "update tblCWO set wSort = 3 where CWO=@wo"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@wo", SqlDbType.NVarChar).Value = cwo
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()

            query = "update tblWIP set wSort= 3 where WIP=@wip"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@wip", SqlDbType.NVarChar).Value = wip
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()

            filtros(2)
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("notesWIPandCWOquitaOnHoldde26", host, UserName)
        End Try
    End Sub
    Public Sub notesWIPandCWOOnHold(cwo As String, fecha As String, notes As String)
        Try
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,FPromBeforeChange,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@FProm,@note,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = cwo
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            cmd.Parameters.Add("@FProm", SqlDbType.NVarChar).Value = fecha
            cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,FPromBeforeChange,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@FProm,@note,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = WIP
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            cmd.Parameters.Add("@FProm", SqlDbType.NVarChar).Value = fecha
            cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes + " CWO: " + cwo
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            If opcion = 2 Then
                NotesInWIP(WIP, notes + " CWO: " + cwo, fecha)
                query = "update tblCWO set wSort=12,ConfirmacionAlm='OnHold', dateConfirmaAlm=GETDATE() where CWO=@WO"
                ' ---------------------------
                Using cmd As New SqlCommand("update tblMLFNotifications set SendReceive=1,TypeOfNotify=6 where Dep in ('Compras','Corte')", cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                End Using
                ' ---------------------------
            ElseIf opcion = 3 Then
                query = "update tblCWO set wSort=14,ConfirmacionApl='OnHold', dateConfirmaApl=GETDATE() where CWO=@WO"
                ' ---------------------------
                Using cmd As New SqlCommand("update tblMLFNotifications set SendReceive=1,TypeOfNotify=7 where Dep='Corte'", cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                End Using
                ' ---------------------------
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@WO", SqlDbType.NVarChar).Value = cwo
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            If opcion = 2 Then
                query = "update tblWIP set wSort=12 where WIP in (select distinct WIP from tblWipDet where CWO=@wo)"
            ElseIf opcion = 3 Then
                query = "update tblWIP set wSort=14 where WIP in (select distinct WIP from tblWipDet where CWO=@wo)"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@wo", SqlDbType.NVarChar).Value = cwo
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            'filtros(3)
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("notesWIPandCWOOnHold", host, UserName)
        End Try
    End Sub
    Public Sub notas(cwo As String, fecha As String, notes As String, WIP As String, PN As String)
        Cursor.Current = Cursors.WaitCursor
        Try
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,FPromBeforeChange,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@FProm,@note,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = cwo
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            If fecha = "2021-01-01" Then
                cmd.Parameters.Add("@FProm", SqlDbType.NVarChar).Value = ""
            Else
                cmd.Parameters.Add("@FProm", SqlDbType.NVarChar).Value = fecha
            End If
            If opcion = 5 Then
                If notes Like $"*{PN}*" Then
                    cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes
                Else
                    cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes + " para PN= " + PN
                End If
            Else
                cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes
            End If
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,FPromBeforeChange,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,@Department,@FProm,@note,GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = WIP
            cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
            cmd.Parameters.Add("@Department", SqlDbType.NVarChar).Value = lbldept.Text
            If fecha = "2021-01-01" Then
                cmd.Parameters.Add("@FProm", SqlDbType.NVarChar).Value = ""
            Else
                cmd.Parameters.Add("@FProm", SqlDbType.NVarChar).Value = fecha
            End If
            If opcion = 5 Then
                If notes Like $"*{PN}*" Then
                    cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes
                Else
                    cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes + " para PN= " + PN
                End If
            Else
                cmd.Parameters.Add("@note", SqlDbType.NVarChar).Value = notes
            End If
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            NotesInWIP(WIP, notes, fecha)
            If lbldept.Text = "Compras" Or opcion = 5 Then
                ' ----------- Campo nuevo para que no afecte la primera Fecha puesta por compras
                Dim cambio As String = "update tblBOMWIP set ProcFDisMat=@fprom where WIP=@wip and PN=@PN"
                Dim cmdo As SqlCommand = New SqlCommand(cambio, cnn)
                cmdo.CommandType = CommandType.Text
                cmdo.Parameters.Add("@fprom", SqlDbType.NVarChar).Value = fecha
                cmdo.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN
                cmdo.Parameters.Add("@wip", SqlDbType.NVarChar).Value = WIP
                cnn.Open()
                cmdo.ExecuteNonQuery()
                cnn.Close()
                '---------------
                cambio = "update tblWIP set ProcFDispMat2 = (select top 1 ProcFDisMat from tblBOMWIP where WIP=@wip order by ProcFDisMat desc) where WIP=@wip"
                cmdo = New SqlCommand(cambio, cnn)
                cmdo.CommandType = CommandType.Text
                cmdo.Parameters.Add("@wip", SqlDbType.NVarChar).Value = WIP
                cnn.Open()
                cmdo.ExecuteNonQuery()
                cnn.Close()
                ' ----------- Manda alerta al almacen
                ' ---------------------------
                Using cmd As New SqlCommand("update tblMLFNotifications set SendReceive=1,TypeOfNotify=8 where Dep='Almacen'", cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                End Using
                ' ---------------------------
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("notas", host, UserName)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
    Sub NotesInWIP(WIP As String, notes As String, FProm As String)
        Dim Dept As String = "", FDept As String = ""
        Try
            If opcion = 1 Or opcion = 4 Or opcion = 6 Or opcion = 7 Then
                Dept = "CutNotas"
                FDept = "CutFProm"
            ElseIf opcion = 2 Then
                Dept = "AlmNotas"
                FDept = "AlmFProm"
            ElseIf opcion = 5 Then
                Dept = "ProcNotas"
                FDept = "ProcFDispMat2"
            End If
            If Dept <> "" Then
                If FProm = "" Then
                    FProm = Convert.ToDateTime(Now)
                End If
                Dim aQuery As String = $"select IsNull({Dept},'') from tblWIP where WIP = '{WIP}'"
                Dim aCmd As New SqlCommand(aQuery, cnn) With {
                    .CommandType = CommandType.Text
                }
                cnn.Open()
                If IsDBNull(aCmd.ExecuteScalar()) Or CStr(aCmd.ExecuteScalar) = "" Then
                    aQuery = $"update tblWIP set {Dept} = '{notes}', {FDept}='{FProm}' where WIP='{WIP}'"
                Else
                    aQuery = $"update tblWIP set {Dept} = {Dept} + '{notes}', {FDept}='{FProm}' where WIP='{WIP}'"
                End If
                cnn.Close()
                aCmd = New SqlCommand(aQuery, cnn) With {
                    .CommandType = CommandType.Text
                }
                cnn.Open()
                aCmd.ExecuteNonQuery()
                cnn.Close()
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema" + vbNewLine + ex.Message + vbNewLine + ", ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("NotesInWIP", host, UserName)
        End Try
    End Sub
    Private Sub llenanotas(WO As String, optionNotas As Integer)
        DataGridView1.DataSource = Nothing
        Try
            Dim tb As New DataTable()
            If optionNotas = 1 Then
                query = "select WIP [CWO],Uname [Usuario],AreaCreacion [Departamento],FPromBeforeChange [Fecha Promesa], NotasBeforeChange [Nota], Fecha from tblXpHist where WIP=@wo order by id desc"
            ElseIf optionNotas = 2 Then
                query = "select WIP [WIP],Uname [Usuario],AreaCreacion [Departamento],FPromBeforeChange [Fecha Promesa], NotasBeforeChange [Nota], Fecha from tblXpHist where WIP=@wo order by id desc"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@wo", SqlDbType.NVarChar).Value = WO
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            edo = cnn.State.ToString
            cnn.Close()
            If tb.Rows.Count > 0 Then
                With DataGridView1
                    .DataSource = tb
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Fecha Promesa").DefaultCellStyle.Format = ("dd-MMM-yy")
                    .Columns("Fecha").DefaultCellStyle.Format = ("dd-MMM-yy")
                    With GroupBox2
                        .Visible = True
                        .BringToFront()
                    End With
                End With
            Else
                GroupBox2.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            cnn.Close()
            CorreoFalla.EnviaCorreoFalla("llenanotas", host, UserName)
        End Try
    End Sub
    Private Sub dgvWips_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvWips.CellClick
        Dim cabecera As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            cabecera = Me.dgvWips.Columns(cdx).HeaderText
        End If
        If cabecera = "wSort WIP" Then
            lblwsortasig.Text = wsorts(dgvWips.Rows(e.RowIndex).Cells("wSort WIP").Value.ToString)
        ElseIf cabecera = "wSort CWO" Then
            lblwsortasig.Text = wsorts(dgvWips.Rows(e.RowIndex).Cells("wSort WIP").Value.ToString)
        ElseIf cabecera = "CWO" Then
            llenanotas(dgvWips.Rows(e.RowIndex).Cells("CWO").Value.ToString, 1)
            lblWIPorCWO.Text = dgvWips.Rows(e.RowIndex).Cells("CWO").Value.ToString
            lblWIP.Text = dgvWips.Rows(e.RowIndex).Cells("WIP").Value.ToString
        ElseIf cabecera = "WIP" Then
            llenanotas(dgvWips.Rows(e.RowIndex).Cells("WIP").Value.ToString, 2)
            lblWIPorCWO.Text = dgvWips.Rows(e.RowIndex).Cells("CWO").Value.ToString
            lblWIP.Text = dgvWips.Rows(e.RowIndex).Cells("WIP").Value.ToString
        End If
    End Sub
    Private Sub dgvWips_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvWips.CellMouseMove
        Dim Encabezado As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            Encabezado = Me.dgvWips.Columns(cdx).HeaderText
        End If
        If Encabezado = "CWO" Or Encabezado = "WIP" Or Encabezado = "wSort WIP" Or Encabezado = "wSort CWO" Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand
        End If
    End Sub
    Private Sub dgvWips_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvWips.CellMouseLeave
        Dim Encabezado As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            Encabezado = Me.dgvWips.Columns(cdx).HeaderText
        End If
        If Encabezado = "CWO" Or Encabezado = "WIP" Or Encabezado = "wSort WIP" Or Encabezado = "wSort CWO" Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        GroupBox2.Visible = False
        DataGridView1.DataSource = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim t As New DataTable
            query = "select Wsort,Definition [Definicion] from tblWsorts order by Wsort asc"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            t.Load(dr)
            edo = cnn.State.ToString
            cnn.Close()
            With dgvwSorts
                .DataSource = t
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            GroupBox3.Visible = True
            GroupBox3.BringToFront()
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("Button2_Click", host, UserName)
        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        GroupBox3.Visible = False
        dgvwSorts.DataSource = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripTextBox5_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox5.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If opcion = 2 Or opcion = 5 Or opcion = 8 Then
            If WIP <> "" And CWO <> "" Then
                Dim ver As Char = CWO(0)
                Dim ver2 As Char = WIP(0)
                If ver = "C" And ver2 = "W" Then
                    p = 0
                    Materiales.lblcwomat.Text = CWO
                    Materiales.Show()
                Else
                    MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    ContextMenuVerMW.Close()
                End If
            End If
        End If
        ContextMenuVerMW.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripTextBox6_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox6.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If opcion = 3 Or opcion = 8 Then
            If WIP <> "" And CWO <> "" Then
                Dim ver As Char = CWO(0)
                Dim ver2 As Char = WIP(0)
                If ver = "C" And ver2 = "W" Then
                    With Materiales
                        .MaximumSize = New System.Drawing.Size(1075, 870)
                        .DataGridView3.Anchor = (AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom)
                        .dgvBOM.Anchor = (AnchorStyles.Left Or AnchorStyles.Top)
                        .lblcwomat.Text = CWO
                        .Show()
                    End With
                Else
                    MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    ContextMenuVerMW.Close()
                End If
            End If
        End If
        ContextMenuVerMW.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub MonitorWIPS_Click(sender As Object, e As EventArgs) Handles MonitorWIPS.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Process.Start("\\10.17.182.22\sea-s\MonitorWips\MonitorWips.application")
        Catch ex As Exception
            MsgBox("No tienes acceso a esta aplicacion, si deseas consultarla, favor de pedir los debidos accesos")
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Process.Start("\\10.17.182.22\sea-s\ModMM\CSHP.application")
        Catch ex As Exception
            MsgBox("No tienes acceso a esta aplicacion, si deseas consultarla, favor de pedir los debidos accesos")
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        ' ------------------------------------------------
        ' Nuevo notify con tabla
        Try
            Dim consulta As String = ""
            Dim numero As Integer = 0
            Dim commm As SqlCommand
            If opcion = 1 Or opcion = 6 Then
                consulta = $"select IsNull(TypeOfNotify,0) from tblMLFNotifications where [User]='{UserName}' and SendReceive=1 and Dep='Corte'"
                conexNotify.Close()
                commm = New SqlCommand(consulta, conexNotify)
                commm.CommandType = CommandType.Text
                conexNotify.Open()
                If CInt(commm.ExecuteScalar) = 0 Then
                    numero = 0
                Else
                    numero = commm.ExecuteScalar()
                End If
                conexNotify.Close()
                If numero = 3 Then
                    NotifyIcon1.BalloonTipText = "Han sido confirmados por almacen CWO ya solicitados"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de solicitud"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 4 Then
                    NotifyIcon1.BalloonTipText = "Han sido confirmados por aplicadores CWO ya solicitados"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de solicitud"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 5 Then
                    NotifyIcon1.BalloonTipText = "Hay CWO listos para cortarse"
                    NotifyIcon1.BalloonTipTitle = "Fuera"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 6 Then
                    NotifyIcon1.BalloonTipText = "Han sido detenidos por almacen CWO ya solicitados"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de detenidos"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 7 Then
                    NotifyIcon1.BalloonTipText = "Han sido detenidos por aplicadores CWO ya solicitados"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de detenidos"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 8 Then
                    NotifyIcon1.BalloonTipText = "Hay CWO con nueva fecha de material"
                    NotifyIcon1.BalloonTipTitle = "Recibo de material"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 10 Then
                    NotifyIcon1.BalloonTipText = "Se desvio una terminal con exito"
                    NotifyIcon1.BalloonTipTitle = "Desviaciones"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                End If
                If numero > 0 Then
                    commm = New SqlCommand($"update tblMLFNotifications set SendReceive=0 where [User]='{UserName}' and SendReceive=1 and Dep='Corte'", conexNotify)
                    commm.CommandType = CommandType.Text
                    conexNotify.Open()
                    commm.ExecuteNonQuery()
                    conexNotify.Close()
                End If
            ElseIf opcion = 2 Then
                consulta = $"select IsNull(TypeOfNotify,0) from tblMLFNotifications where [User]='{UserName}' and SendReceive=1 and Dep='Almacen'"
                conexNotify.Close()
                commm = New SqlCommand(consulta, conexNotify)
                commm.CommandType = CommandType.Text
                conexNotify.Open()
                If CInt(commm.ExecuteScalar) = 0 Then
                    numero = 0
                Else
                    numero = commm.ExecuteScalar()
                End If
                conexNotify.Close()
                If numero = 1 Then
                    NotifyIcon1.BalloonTipText = "Han sido solicitados por corte CWO"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de solicitud"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 2 Then
                    NotifyIcon1.BalloonTipText = "Han sido solicitados por corte CWO con corto de material"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de solicitud"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 8 Then
                    NotifyIcon1.BalloonTipText = "Hay CWO con nueva fecha de material"
                    NotifyIcon1.BalloonTipTitle = "Recibo de material"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                End If
                If numero > 0 Then
                    commm = New SqlCommand($"update tblMLFNotifications set SendReceive=0 where [User]='{UserName}' and SendReceive=1 and Dep='Almacen'", conexNotify)
                    commm.CommandType = CommandType.Text
                    conexNotify.Open()
                    commm.ExecuteNonQuery()
                    conexNotify.Close()
                End If
            ElseIf opcion = 3 Then
                consulta = $"select IsNull(TypeOfNotify,0) from tblMLFNotifications where [User]='{UserName}' and SendReceive=1 and Dep='Aplicadores'"
                conexNotify.Close()
                commm = New SqlCommand(consulta, conexNotify)
                commm.CommandType = CommandType.Text
                conexNotify.Open()
                If CInt(commm.ExecuteScalar) = 0 Then
                    numero = 0
                Else
                    numero = commm.ExecuteScalar()
                End If
                conexNotify.Close()
                If numero = 1 Then
                    NotifyIcon1.BalloonTipText = "Han sido solicitados por corte CWO"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de solicitud"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 2 Then
                    NotifyIcon1.BalloonTipText = "Han sido solicitados por corte CWO con corto de material"
                    NotifyIcon1.BalloonTipTitle = "Notificacion de solicitud"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                ElseIf numero = 9 Then
                    NotifyIcon1.BalloonTipText = "Desviacion rechazada, por validaciones"
                    NotifyIcon1.BalloonTipTitle = "Desviaciones"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                End If
                If numero > 0 Then
                    commm = New SqlCommand($"update tblMLFNotifications set SendReceive=0 where [User]='{UserName}' and SendReceive=1 and Dep='Aplicadores'", conexNotify)
                    commm.CommandType = CommandType.Text
                    conexNotify.Open()
                    commm.ExecuteNonQuery()
                    conexNotify.Close()
                End If
            ElseIf opcion = 5 Then
                consulta = $"select IsNull(TypeOfNotify,0) from tblMLFNotifications where [User]='{UserName}' and SendReceive=1 and Dep='Compras'"
                conexNotify.Close()
                commm = New SqlCommand(consulta, conexNotify)
                commm.CommandType = CommandType.Text
                conexNotify.Open()
                If CInt(commm.ExecuteScalar) = 0 Then
                    numero = 0
                Else
                    numero = commm.ExecuteScalar()
                End If
                conexNotify.Close()
                If numero = 6 Then
                    NotifyIcon1.BalloonTipText = "Han sido detenido por almacen CWO"
                    NotifyIcon1.BalloonTipTitle = "Falta de material"
                    NotifyIcon1.Visible = True
                    NotifyIcon1.ShowBalloonTip(0)
                End If
                If numero > 0 Then
                    commm = New SqlCommand($"update tblMLFNotifications set SendReceive=0 where [User]='{UserName}' and SendReceive=1 and Dep='Compras'", conexNotify)
                    commm.CommandType = CommandType.Text
                    conexNotify.Open()
                    commm.ExecuteNonQuery()
                    conexNotify.Close()
                End If
            End If
        Catch ex As Exception
            conexNotify.Close()
        End Try
        Timer1.Enabled = True
        Timer1.Interval = 180000
    End Sub
    Private Sub FechasIncumplidas()
        Try
            Dim consulta As String = ""
            Dim commm As SqlCommand
            consulta = $"select distinct bw.PN,
             (
             select 
             case when bw.PN like 'C%' or bw.PN like 'W%'  
             then (
             select SUM(WireBalance * LengthWire) * 0.0032808 AS Qty 
             From tblWipDet where WireBalance > 0 AND Wire = bw.PN AND 
             wip in (
             select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.
             CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO 
             where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) and 
             (ConfirmacionAlm='OnHold')
             ))
             when bw.PN like 'T%'
             then (SELECT SUM(TABalance) As Qty FROM tblWipDet where TABalance > 0 AND TermA = bw.PN AND
             wip in (
             select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.
             CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO 
             where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) and 
             (ConfirmacionAlm='OnHold'))
             ) + (SELECT SUM(TBBalance) As Qty FROM tblWipDet where TBBalance > 0 AND TermB = bw.PN AND
             wip in (
             select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.
             CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO 
             where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) and 
             (ConfirmacionAlm='OnHold'))
             ) end)
             [QtyRequerida],
             qb.QtyOnHand,bw.ProcFDisMat
             from tblBOMWIP bw
             INNER JOIN tblItemsQB qb on bw.PN=qb.PN
             where 
             bw.PN in (
             SELECT tblBOMCWO.PN 
             FROM tblBOMCWO 
             WHERE 
             tblBOMCWO.WIP IN (select distinct w.WIP from tblCWO as c inner join tblWipDet as d on c.CWO=d.
             CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO 
             where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) and 
             (ConfirmacionAlm='OnHold')) and tblBOMCWO.Hold=1
             ) and
             bw.ProcFDisMat is not null and (
                 Convert(date,bw.ProcFDisMat) <= Convert(date,GETDATE())
             )
             order by bw.ProcFDisMat desc"
            conexMensajeCortos.Close()
            commm = New SqlCommand(consulta, conexMensajeCortos)
            Dim ReadData As SqlDataReader
            Dim aTableReader As DataTable = New DataTable
            commm.CommandType = CommandType.Text
            conexMensajeCortos.Open()
            ReadData = commm.ExecuteReader
            aTableReader.Load((ReadData))
            conexMensajeCortos.Close()
            If aTableReader.Rows.Count > 0 Then
                For ii = 0 To aTableReader.Rows.Count - 1
                    If Not CInt(aTableReader.Rows(ii).Item("QtyRequerida").ToString) > CInt(aTableReader.Rows(ii).Item("QtyOnHand").ToString) Then
                        aTableReader.Rows(ii).Delete()
                    End If
                Next
            End If
            aTableReader.AcceptChanges()
            If aTableReader.Rows.Count > 0 Then
                With MensajePN
                    With .dgvNumerosParte
                        .DataSource = aTableReader
                        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Columns("ProcFDisMat").DefaultCellStyle.Format = ("dd-MMM-yy")
                        .ClearSelection()
                    End With
                    .Label3.Text = "Items: " + .dgvNumerosParte.Rows.Count.ToString
                    .ShowDialog()
                    .BringToFront()
                End With
            End If
            conexMensajeCortos.Close()
        Catch ex As Exception
            conexMensajeCortos.Close()
            cnn.Close()
        End Try
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop()
        cargachart()
        Timer2.Enabled = True
        Timer2.Interval = 60000
    End Sub
    Private Function ChargeGridAfectacion(PN As String) As DataTable
        Try
            query = "select CONVERT(date,FechaSolicitudMat) [Fecha Solicitud CWO], b.CWO,a.PN,a.Balance [Qty],c.WIP,c.AU,c.Rev,c.ProcNotas [Notas Compras],
(SELECT SUM(QtyBalance) FROM tblItemsPOsDet AS c INNER JOIN tblItemsPOs AS d ON c.IDPO = d.IDPO WHERE c.PN=A.pn AND c.QtyBalance > 0 AND d.Status = 'OPEN') AS InTransit,
(SELECT TOP(1) JuarezDueDate FROM tblItemsPOsDet AS A INNER JOIN tblItemsPOs AS B ON A.IDPO = B.IDPO WHERE A.PN = a.PN AND A.QtyBalance > 0 AND B.Status = 'OPEN'  
ORDER BY A.JuarezDueDate) AS NextFReciboMat,c.ProcFDispMat2 [Nueva fecha de compras]
FROM tblBOMCWO a inner join tblCWO b on a.CWO=b.CWO inner join tblWIP c on c.WIP=a.WIP
where a.PN='" + PN + "' and c.Status='OPEN' and (b.WSort < 30 or c.wSort < 30) and a.Balance > 0"
            Dim tabla As New DataTable()
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            cnn.Close()
            Return tabla
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("ChargeGridAfectacion", host, UserName)
            Return Nothing
        End Try
    End Function
    Function GetTable(consulta As String)
        Try
            Dim table As New DataTable()
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            table.Load(dr)
            cnn.Close()
            Return table
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            Return Nothing
        End Try
    End Function
    Public Sub cambioMaquinaXCWO(MaqNew As Integer, NotesXCambio As String, CWOAceptChange As String, Optional ByVal flag As Integer = 0)
        Try
            NotesXCambio = "Cambio de maquina " & NotesXCambio
            Dim sort As AutomaticSort = New AutomaticSort(MaqNew), query As String = If(flag = 1, "update tblCWO set Maq= " + MaqNew.ToString + ",Notes='" & NotesXCambio.ToString & "' where CWO='" + CWOAceptChange.ToString + "'", "update tblCWO set Maq= " + MaqNew.ToString + ",Notes='" & NotesXCambio.ToString & "',Id=" + sort.GetSort().ToString + " where CWO='" + CWOAceptChange.ToString + "'")
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            If flag = 0 Then
                sort = New AutomaticSort(maq, cola)
                sort.ReOrderSort()
                If Not sort.CheckZeros() Then
                    sort.RemoveZeros()
                End If
            End If
            query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values ('" + CWOAceptChange.ToString + "','" + UserName.ToString + "','" + lbldept.Text.ToString + "','" & NotesXCambio.ToString & "',GETDATE(),'MLF')"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            If rbsolicitar.Checked = True Then filtros(1)
            If rbSolicitado.Checked = True Then filtros(2)
            If rbListosParaEntrar.Checked = True Then filtros(3)
            If rbYaempezados.Checked = True Then filtros(4)
            If rbEmpezadosyDetenidos.Checked = True Then filtros(5)
            If rdbOnHold.Checked = True Then filtros(6)
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("cambioMaquinaXCWO", host, UserName)
        End Try
    End Sub
    Private Sub dgvMatSinStockCompras_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvMatSinStockCompras.ColumnHeaderMouseClick
        PintarMateriales()
    End Sub
    ' -----------------
    ' Nuevo item para agregar los materiales una vez confirmados
    Private Sub AsignarMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsignarMaterialToolStripMenuItem.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If opcion = 2 Or opcion = 8 Then
            If WIP <> "" And CWO <> "" Then
                Dim ver As Char = CWO(0)
                Dim ver2 As Char = WIP(0)
                If ver = "C" And ver2 = "W" Then
                    p = 10
                    Materiales.lblcwomat.Text = CWO
                    Materiales.Label4.Text = WIP
                    Materiales.ShowDialog()
                    flag = 1
                Else
                    MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    ContextMenuVerMW.Close()
                End If
            End If
            ContextMenuVerMW.Close()
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If opcion = 6 Or opcion = 7 Then
            If WIP <> "" And CWO <> "" Then
                Dim ver As Char = CWO(0)
                Dim ver2 As Char = WIP(0)
                If ver = "C" And ver2 = "W" Then
                    With CambioMaquina
                        .lblcwoporsolicitar.Text = CWO
                        .lblwipporsolicitar.Text = WIP
                        .Label5.Text = maq
                        .Location = New Point(Cursor.Position.X, Cursor.Position.Y)
                        GetMaqsActive()
                        If rbsolicitar.Checked = True Then
                            .Categoria = 1
                        Else
                            .Categoria = 0
                        End If
                        .ShowDialog()
                    End With
                Else
                    MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    ContextMenuVerMW.Close()
                End If
            End If
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Sub GetMaqsActive()
        Try
            CambioMaquina.ComboBox1.Items.Clear()
            Dim consulta As String = "Select Maq from tblMaqRates where Active = 1 order by CONVERT(int,Maq) asc"
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    CambioMaquina.ComboBox1.Items.Add(dr.GetValue(0))
                End While
            End If
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If opcion = 1 Or opcion = 4 Or opcion = 6 Or opcion = 7 Then
            If WIP <> "" And CWO <> "" Then
                Dim ver As Char = CWO(0)
                Dim ver2 As Char = WIP(0)
                If ver = "C" And ver2 = "W" Then
                    Materiales.lblcwomat.Text = CWO
                    Materiales.ShowDialog()
                Else
                    MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    ContextMenuDisponibilidad.Close()
                End If
            End If
        End If
        ContextMenuDisponibilidad.Close()
        If rbsolicitar.Checked = True Then filtros(1)
        If rbSolicitado.Checked = True Then filtros(2)
        If rbListosParaEntrar.Checked = True Then filtros(3)
        If rbYaempezados.Checked = True Then filtros(4)
        If rbEmpezadosyDetenidos.Checked = True Then filtros(5)
        If rdbOnHold.Checked = True Then filtros(6)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If WIP <> "" And CWO <> "" Then
            Dim ver As Char = CWO(0)
            Dim ver2 As Char = WIP(0)
            If ver = "C" And ver2 = "W" Then
                If opcion = 1 Or opcion = 4 Or opcion = 6 Or opcion = 7 Then
                    With Asignar
                        .Text = "Orden de Corte"
                        .lblcwoporsolicitar.Text = CWO
                        .lblwipporsolicitar.Text = WIP
                        .lblMaq.Text = maq
                        .TabPage2.Visible = False
                        .TabPage2.Parent = Nothing
                        .TabPage1.Parent = Asignar.TabControl1
                        .TabControl1.SelectedTab = Asignar.TabPage1
                        .MaskedTextBox1.Clear()
                        .MaskedTextBox1.Focus()
                        With .dgvSort
                            .DataSource = GetTable("select CWO,Id [Orden] from tblCWO where (Id > 0 or Id is not null) and Maq=" + maq.ToString + " and Status='OPEN' order by Id asc")
                            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .AutoResizeColumns()
                        End With
                        .ShowDialog()
                    End With
                End If
            Else
                MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
            End If
        End If
        ContextMenuDisponibilidad.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem8.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If WIP <> "" And CWO <> "" Then
            Dim ver As Char = CWO(0)
            Dim ver2 As Char = WIP(0)
            If ver = "C" And ver2 = "W" Then
                If opcion = 6 Or opcion = 7 Then
                    With Asignar
                        .Text = "Detener"
                        .Label6.Text = CWO
                        .Label3.Text = WIP
                        .TabPage1.Visible = False
                        .TabPage1.Parent = Nothing
                        .TabPage2.Parent = Asignar.TabControl1
                        .TabControl1.SelectedTab = Asignar.TabPage2
                        .ShowDialog()
                    End With
                End If
            Else
                MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
            End If
        End If
        ContextMenuDisponibilidad.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem9_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem9.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If WIP <> "" And CWO <> "" Then
            Dim ver As Char = CWO(0)
            Dim ver2 As Char = WIP(0)
            If ver = "C" And ver2 = "W" Then
                If opcion = 6 Or opcion = 7 Then
                    DetenerCWO(CWO, "Vuelve a corte luego de detenerse", 2)
                    MsgBox("Cambio efectuado")
                End If
            Else
                MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
            End If
        End If
        ContextMenuDisponibilidad.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem10_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem10.Click 'Graficas
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Graficas.Show()
        Me.Hide()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem11_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem11.Click 'Hora X Hora
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        HoraXHora.Show()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem12_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem12.Click 'Listos, este sera solo para aplicadores, debido a que solo ellos confirman
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If opcion = 3 Then
            If WIP <> "" And CWO <> "" Then
                Dim ver As Char = CWO(0)
                Dim ver2 As Char = WIP(0)
                If ver = "C" And ver2 = "W" Then
                    query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,'Aplicadores','Confirmado',GETDATE(),'MLF')"
                    cmd = New SqlCommand(query, cnn)
                    cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha,DetPor) values (@CWO,@User,'Aplicadores','Confirmado',GETDATE(),'MLF')"
                    cmd = New SqlCommand(query, cnn)
                    cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = WIP
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    poneokalm_apl(CWO, WIP)
                Else
                    MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    ContextMenuVerMW.Close()
                End If
            End If
        ElseIf opcion = 2 Then
            Dim respuesta As Integer
            respuesta = MessageBox.Show("¿Desea quitar status Cortos a este CWO?", "Cortos", MessageBoxButtons.YesNo)
            If respuesta = 6 Then
                poneokalm_apl(CWO, WIP)
            Else
                ContextMenuVerMW.Close()
            End If
        End If
        ContextMenuVerMW.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem13_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem13.Click ' Detener, este sera para ambos dept
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim respuesta As String
        If opcion = 2 Then
            respuesta = MessageBox.Show("¿Seguro que desea poner estatus Corto a este CWO?", "Cortos", MessageBoxButtons.YesNo)
            If respuesta = vbYes Then
                If WIP <> "" And CWO <> "" Then
                    Dim ver As Char = CWO(0)
                    Dim ver2 As Char = WIP(0)
                    If ver = "C" And ver2 = "W" Then
                        p = 12
                        With Materiales
                            .MaximumSize = New System.Drawing.Size(1400, 620)
                            .AutoScroll = False
                            .lblcwomat.Text = CWO
                            .Label4.Text = WIP
                            .Show()
                        End With
                    Else
                        MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    End If
                End If
            ElseIf respuesta = vbNo Then
                ContextMenuVerMW.Close()
            End If
        ElseIf opcion = 3 Then
            respuesta = MessageBox.Show("¿Seguro que desea poner estatus Corto a este CWO?", "Cortos", MessageBoxButtons.YesNo)
            If respuesta = vbYes Then
                If WIP <> "" And CWO <> "" Then
                    Dim ver As Char = CWO(0)
                    Dim ver2 As Char = WIP(0)
                    If ver = "C" And ver2 = "W" Then
                        Hold.lblcwoporsolicitar.Text = CWO
                        Hold.lblwipporsolicitar.Text = WIP
                        Hold.ShowDialog()
                    Else
                        MessageBox.Show("La celda seleccionada no contiene un CWO o WIP")
                    End If
                End If
            ElseIf respuesta = vbNo Then
                ContextMenuVerMW.Close()
            End If
        End If
        ContextMenuVerMW.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Principal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        bgWorker.Dispose()
        With NotifyIcon1
            .Visible = False
            .Dispose()
        End With
        Application.Exit()
        End
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            Process.Start("\\10.17.182.22\sea-s\MLF\Manual\Manual MLF.pdf")
        Catch ex As Exception
            MsgBox("No tienes acceso a esta aplicacion, si deseas consultarla, favor de pedir los debidos accesos")
        End Try
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub dgvMatSinStockCompras_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMatSinStockCompras.CellDoubleClick
        Try
            If e.RowIndex >= 0 Then
                If e.ColumnIndex = 1 Then
                    With dgvAfectados
                        .DataSource = ChargeGridAfectacion(dgvMatSinStockCompras.Rows(e.RowIndex).Cells("ComponentPN").Value.ToString)
                        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .ClearSelection()
                        lblAfectados.Text = "Items: " + dgvAfectados.Rows.Count.ToString
                        If opcion = 2 Then
                            .Columns("aChk").Visible = True
                            If .Rows.Count > 0 Then
                                btnCortosPN.Visible = True
                            Else
                                btnCortosPN.Visible = False
                            End If
                        Else
                            .Columns("aChk").Visible = False
                        End If
                    End With
                End If
            End If
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("dgvMatSinStockCompras_CellDoubleClick", host, UserName)
        End Try
    End Sub
    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        If Me.Text = "Desarrollo" Or Me.Text = "Admin" Or Me.Text = "Direccion" Then
            With OpcionesLog
                .ShowDialog()
            End With
            Me.Text = Me.Text
            Form1_Load(New System.Object, New System.EventArgs)
        Else
            Dim path As String = "C:\Users\" + Environment.UserName.ToString + "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\SEA\MLF.appref-ms"
            Process.Start(path)
            Me.Close()
        End If
    End Sub
    Private Sub dgvMatSinStockCompras_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMatSinStockCompras.CellClick
        Try
            If e.RowIndex >= 0 Then
                If e.ColumnIndex = 2 Then
                    If Microsoft.VisualBasic.Left(dgvMatSinStockCompras.Rows(e.RowIndex).Cells("ComponentPN").Value.ToString, 1) = "T" Then
                        If dgvMatSinStockCompras.Rows(e.RowIndex).Cells("ComponentPN").Style.BackColor = Color.MintCream Then
                            CheckValidaciones(dgvMatSinStockCompras.Rows(e.RowIndex).Cells("ComponentPN").Value.ToString)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("dgvMatSinStockCompras_CellClick", host, UserName)
        End Try
    End Sub
    Private Sub btnRefrescaGrid_Click(sender As Object, e As EventArgs) Handles btnRefrescaGrid.Click
        Cursor.Current = Cursors.WaitCursor
        If Label8.Text = "Numeros de Parte cortos" Then
            cargadatosCompras()
        Else
            dgvMatSinStockCompras.DataSource = tblasig
            If dgvMatSinStockCompras.Rows.Count > 0 Then
                dgvMatSinStockCompras.Columns("NextFReciboMat").DefaultCellStyle.Format = ("dd-MMM-yy")
                dgvMatSinStockCompras.Columns("Fecha promesa PN").DefaultCellStyle.Format = ("dd-MMM-yy")
                dgvMatSinStockCompras.AutoResizeColumns()
                dgvMatSinStockCompras.Columns(4).Width = 5
                dgvMatSinStockCompras.Columns(8).Width = 5
                If opcion = 5 Then
                    dgvMatSinStockCompras.Columns("Chk").Visible = True
                Else
                    dgvMatSinStockCompras.Columns("Chk").Visible = False
                End If
                dgvMatSinStockCompras.ClearSelection()
                PintarMateriales()
                lblitemscortos.Text = "Items: " + dgvMatSinStockCompras.Rows.Count.ToString
                Label8.Text = "Numeros de Parte cortos"
            End If
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub btnCortosPN_Click(sender As Object, e As EventArgs) Handles btnCortosPN.Click
        Cursor.Current = Cursors.WaitCursor
        Dim count As Integer = 0
        For i As Integer = 0 To dgvAfectados.Rows.Count - 1
            If dgvAfectados.Rows(i).Cells("aChk").Value = True Then
                count += 1
            End If
        Next
        If count > 0 Then
            With Hold
                .lblcwoporsolicitar.Visible = False
                .lblwipporsolicitar.Visible = False
                .Text = "Notas Corto"
                Dim X = Convert.ToInt32(dgvAfectados.Location.X)
                Dim Y = Convert.ToInt32(dgvAfectados.Location.Y)
                .Location = New Point(X - Y, Math.Abs((Y * 2) - (Y * 10)))
                .ShowDialog()
            End With
        Else
            MsgBox("Debe seleccionar los numeros de parte para poner corto")
            Exit Sub
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub btnexportaeficc_Click(sender As Object, e As EventArgs) Handles btnexportaeficc.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            If dgvMatSinStockCompras.Rows.Count > 0 Then
                Dim exApp As New Microsoft.Office.Interop.Excel.Application
                Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
                Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

                exLibro = exApp.Workbooks.Add
                exHoja = exLibro.Worksheets("Sheet1")

                Dim NCol As Integer = dgvMatSinStockCompras.ColumnCount
                Dim NRow As Integer = dgvMatSinStockCompras.RowCount
                Try
                    For i As Integer = 1 To NCol
                        exHoja.Cells.Item(1, i) = dgvMatSinStockCompras.Columns(i - 1).DataPropertyName.ToString
                    Next
                    For Fila As Integer = 0 To NRow - 1
                        For Col As Integer = 0 To NCol - 1
                            exHoja.Cells.Item(Fila + 2, Col + 1) = dgvMatSinStockCompras.Rows(Fila).Cells(Col).FormattedValue
                        Next
                    Next
                    exHoja.Columns.AutoFit()
                    exApp.Application.Visible = True
                    exHoja = Nothing
                    exLibro = Nothing
                    exApp = Nothing
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            Else
                MsgBox("No existen items para exportar!!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub btnAgregaFecha_Click(sender As Object, e As EventArgs) Handles btnAgregaFecha.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            Dim count As Integer = 0
            For i As Integer = 0 To dgvMatSinStockCompras.Rows.Count - 1
                If dgvMatSinStockCompras.Rows(i).Cells("Chk").Value = True Then
                    count += 1
                End If
            Next
            If count > 0 Then
                With Hold
                    .lblcwoporsolicitar.Visible = False
                    .lblwipporsolicitar.Visible = False
                    .Text = "Notas"
                    Dim X = Convert.ToInt32(dgvMatSinStockCompras.Location.X)
                    Dim Y = Convert.ToInt32(dgvMatSinStockCompras.Location.Y)
                    .Location = New Point(X + X, Math.Abs((Y * 2) - (Y * 10)))
                    .ShowDialog()
                End With
            Else
                MsgBox("Debe seleccionar los numeros de parte para asignar fecha promesa")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ex.ToString)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
    'Evento de quitar corto al NP seleccionado para asi, evaluar su allocated y sobre eso, poder quitar corto,
    'en caso de ser parcial, hare funcion para registrar hasta donde se puede quitar el corto y que cwo si se salvan
    Private Sub ToolStripMenuItem14_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem14.Click
        Cursor.Current = Cursors.WaitCursor
        If opcion = 2 Then
            If PN <> "" Then
                Dim aTable = New DataTable()
                aTable = PNQuitandoCorto(PN)
                If aTable.Rows.Count = 1 Then
                    CheckPN(aTable)
                Else
                    MsgBox("No se logro realizar el proceso, verifica el Numero de parte seleccionado")
                End If
            Else
                MsgBox("Selecciona un numero de parte")
            End If
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub DesviacionesCheck()
        If WIP <> "" Then
            DesviacionesTerm.WIP.Text = WIP
            DesviacionesTerm.Location = New Point(Cursor.Position.X, Cursor.Position.Y)
            DesviacionesTerm.ShowDialog()
        Else
            MsgBox("Seleccione un WIP")
        End If
    End Sub
    Private Sub ToolStripMenuItem15_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem15.Click
        Cursor.Current = Cursors.WaitCursor
        If opcion = 6 Or opcion = 7 Then
            DesviacionesCheck()
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub DesviarTerminalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesviarTerminalToolStripMenuItem.Click
        Cursor.Current = Cursors.WaitCursor
        If opcion = 3 Then
            DesviacionesCheck()
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Timer3.Stop()
        FechasIncumplidas()
        Timer3.Enabled = True
        Timer3.Interval = 1800000
    End Sub
    Private Sub dgvMatSinStockCompras_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvMatSinStockCompras.CellMouseDown
        If opcion = 2 Then
            If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
                If e.Button = MouseButtons.Right Then
                    Try
                        dgvMatSinStockCompras.CurrentCell = dgvMatSinStockCompras.Rows(e.RowIndex).Cells(e.ColumnIndex)
                        dgvMatSinStockCompras.Rows(e.RowIndex).Selected = True
                        PN = Convert.ToString(dgvMatSinStockCompras.Rows(e.RowIndex).Cells(1).Value)
                    Catch ex As Exception
                        MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
                        CorreoFalla.EnviaCorreoFalla("dgvWips_CellMouseDown", host, UserName)
                    End Try
                End If
            End If
        End If
    End Sub
    Private Sub dgvMatSinStockCompras_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvMatSinStockCompras.MouseClick
        If dgvMatSinStockCompras.Rows.Count > 0 Then
            If opcion = 2 Then
                If e.Button = System.Windows.Forms.MouseButtons.Right Then
                    ContextMenuVerMW.Show(Cursor.Position.X, Cursor.Position.Y)
                    ToolStripMenuItem12.Visible = False
                    ToolStripMenuItem13.Visible = False
                    ToolStripTextBox5.Visible = False
                    ToolStripTextBox6.Visible = False
                    AsignarMaterialToolStripMenuItem.Visible = False
                    ToolStripTextBox7.Visible = False
                    ToolStripMenuItem14.Visible = True
                End If
            End If
        End If
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        If (ApplicationDeployment.IsNetworkDeployed) Then
            With ApplicationDeployment.CurrentDeployment.CurrentVersion
                MessageBox.Show(vbCrLf + "MLF Software" + vbNewLine + "© SPECIALIZED HARNESS PRODUCTS S DE RL DE CV" + vbCr + "All Rights Reserved" + vbLf + "Software Version " & .Major & "." & .Minor & "." & .Build & "." & .Revision & "" + vbCr + "Oct/2021", "MLF Software", MessageBoxButtons.OK)
            End With
        Else
            MessageBox.Show(vbCrLf + "MLF Software" + vbNewLine + "© SPECIALIZED HARNESS PRODUCTS S DE RL DE CV" + vbCr + "All Rights Reserved" + vbLf + "Software Version " & System.Windows.Forms.Application.ProductVersion & "" + vbCr + "Oct/2021", "MLF Software", MessageBoxButtons.OK)
        End If
    End Sub
    'Public Sub AutoUpdate()
    '    FSW.Path = "\\10.17.182.22\sea-s\MLF\Application Files"
    '    FSW.IncludeSubdirectories = True
    '    FSW.EnableRaisingEvents = True
    'End Sub
    'Private Sub FSW_Created(sender As Object, e As IO.FileSystemEventArgs) Handles FSW.Created
    '    Dim limpiaChar As String = e.Name, extraVersion As String = ""
    '    limpiaChar = LTrim(RTrim(limpiaChar))
    '    limpiaChar = limpiaChar.Replace("_", ".")
    '    For i As Integer = 0 To limpiaChar.Length - 1
    '        If IsNumeric(limpiaChar(i)) Or limpiaChar(i) = "." Then
    '            extraVersion += limpiaChar(i)
    '        End If
    '    Next
    '    ver = extraVersion
    '    ActualizacionRequerida.BringToFront()
    '    ActualizacionRequerida.ShowDialog()
    '    If flagActualizacion = 1 Then
    '        Application.Exit()
    '    End If
    'End Sub
End Class
