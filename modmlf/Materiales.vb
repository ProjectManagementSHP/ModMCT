Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text

Public Class Materiales
    Private nombreasig As String = "", pn As String = "", PNAux As String = ""
    Private holdoconfir As Integer = 0, balance As Integer = 0, vertagasignado As Integer = 0, tAcumulado As Integer
    Dim FilaSeleccionada As String
    Private Sub Materiales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = Nothing
        dgvBOM.DataSource = Nothing
        TextBox2.Clear()
        If opcion = 2 Or opcion = 5 Then
            Label1.Text = "Materiales BOM"
            LinkLabel1.Text = "Almacen"
            If p = 10 Then 'Bandera de asignando material
                If Not MAtAsigcomplete(lblcwomat.Text) Then
                    CheckBox1.Visible = True
                    GroupBox2.Visible = False
                    GroupBox3.Visible = True
                    DataGridView1.Visible = True
                    dgvBOM.Visible = False
                    Llenagrid2()
                Else
                    MsgBox($"El {Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO ya tiene todo el material asignado")
                    Me.Dispose()
                    Me.Close()
                End If
            ElseIf p = 12 Then 'Bandera de Hold
                Button3.Visible = True
                GroupBox2.Visible = False
                dgvBOM.Visible = False
                DataGridView1.Visible = True
                CheckBox1.Visible = False
                gbnotasconfirmando.Visible = False
                Llenagrid3()
                TabControl1.Parent = Nothing
            Else ' Solo ver materiales (Almacen y Compras)
                GroupBox2.Visible = False
                GroupBox3.Visible = True
                dgvBOM.Visible = True
                DataGridView1.Visible = False
                gbnotasconfirmando.Visible = False
                CheckBox1.Visible = False
                Llenagrid()
                TabPage1.Parent = Nothing
            End If
        ElseIf opcion = 3 Then ' Ver aplicadores
            Label1.Text = "Aplicadores"
            LinkLabel1.Text = "ACS"
            GroupBox2.Visible = False
            CheckBox1.Visible = False
            DataGridView3.Visible = True
            TabControl1.Visible = False
            Llenagrid1()
        ElseIf opcion = 1 Or opcion = 4 Or opcion = 6 Or opcion = 7 Or opcion = 8 Then
            TabPage1.Parent = Nothing
            Label1.Text = "Materiales BOM y Re-Asignacion"
            GroupBox2.Visible = False
            dgvBOM.Visible = True
            DataGridView1.Visible = False
            gbnotasconfirmando.Visible = False
            CheckBox1.Visible = False
            LlenaGridMaterialesParaCorte()
            GroupBox3.Visible = True
            LinkLabel1.Visible = False
        End If
        Label4.Visible = False
        DateTimePicker1.Value = "2021-01-01"
        DateTimePicker1.Visible = False
        gbnotasconfirmando.Visible = False
        GridCharge()
    End Sub
    Private Sub GridCharge()
        Dim systemType As Type
        Dim propertyInfo As PropertyInfo
        '-------------------------------
        systemType = DataGridView1.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(DataGridView1, True, Nothing)
        '-------------------------------
        systemType = DataGridView3.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(DataGridView3, True, Nothing)
        '-------------------------------
        systemType = DataGridView2.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(DataGridView2, True, Nothing)
        '-------------------------------
        systemType = DataGridView5.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(DataGridView5, True, Nothing)
        '-------------------------------
        systemType = DataGridView4.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(DataGridView4, True, Nothing)
        '-------------------------------
        systemType = dgvBOM.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvBOM, True, Nothing)
    End Sub
    Public SumVal As Func(Of Integer, Integer, Integer, Integer) = Function(a, b, result) result + a + b
    Private Sub Llenagrid() 'Solo ver materiales
        Dim query As String = $"Select bw.PN,bw.Description,CONVERT(int,bw.Qty) [Qty] ,CONVERT(int,bw.Balance) [Balance],(select isnull(Convert(int,SUM(Balance)),0) from tblItemsTags where tblItemsTags.PN=bw.PN and Status='NoAvailable' and Balance>0) [En Piso],(select isnull(Convert(int,SUM(Balance)),0) from tblItemsTags where tblItemsTags.PN=bw.PN and Status='Available' and Balance>0) [Almacen],Convert(Int, ml.QtyOnHand) [Total],Convert(Int, ml.QtyOnHand) - CONVERT(int,bw.Balance) [Dif],Convert(Int, ml.QtyOnOrder) [In Transit], '' [Locaciones],ISNULL(bw.TagAsignado,0) [TagAsignado],(SELECT TOP(1) JuarezDueDate FROM tblItemsPOsDet AS A INNER JOIN tblItemsPOs AS B ON A.IDPO = B.IDPO WHERE A.PN = bw.PN AND A.QtyBalance > 0 AND B.Status = 'OPEN'  AND A.Confirmed = 1 ORDER BY A.JuarezDueDate) [Next Fecha Recibo] From tblBOM{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO As bw inner Join tblItemsQB As ml On bw.PN = ml.PN Where bw.{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO ='{lblcwomat.Text}' and bw.PN not like 'S%' group by bw.PN,bw.Description,bw.Qty,bw.Balance,ml.QtyOnHand,ml.QtyOnOrder,bw.TagAsignado"
        Dim muestra As Integer = 0, query2 As String = ""
        Dim tabla As New DataTable(), tabl As New DataTable(), tb As New DataTable()
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If tabla.Rows.Count > 0 Then
                For i As Integer = 0 To tabla.Rows.Count - 1
                    tabla.Columns(9).ReadOnly = False
                    tabla.Columns(9).MaxLength = 400
                    tabla.Rows(i).Item(9) = Locacion(tabla.Rows(i).Item("PN").ToString)
                Next
            End If
            With dgvBOM
                .DataSource = tabla
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Label2.Text = "Items: " & dgvBOM.Rows.Count
            End With
            Pintandoceldas()
            btnExportar.Visible = If(opcion = 2 And DataGridView1.Rows.Count > 0, True, False)
            If Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C" Then
                cmd = New SqlCommand("select COUNT(*) from tblCWOSerialNumbers where CWO='" + lblcwomat.Text + "'", cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                muestra = If(CInt(cmd.ExecuteScalar) = 0, 0, 1)
                cnn.Close()
                query2 = If(muestra = 0, "select IdSort,Wire,TermA,MaqA,TermB,MaqB,IsNull(Tsetup,0) [TSetup],IsNull(TRuntime,0) [TRuntime],null as 'Acumulado' from tblWipDet where CWO='" + lblcwomat.Text + "' and WireBalance>0 order by IDSort", "select IdSort,det.Wire,det.WireBalance,det.TermA,TABalance,MaqA,det.TermB,TBBalance,MaqB,IsNull(Tsetup,0) [Tsetup],IsNull(TRuntime,0) [TRuntime],null as 'Acumulado' from tblWipDet det inner join tblCWOSerialNumbers cw on det.wireid=cw.WireID where det.CWO='" + lblcwomat.Text + "' and (Cutting is not null or det.WireBalance > 0) order by IDSort")
                cmd = New SqlCommand(query2, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tabl.Load(dr)
                cnn.Close()
                If tabl.Rows.Count > 0 Then
                    tAcumulado = 0
                    Dim column As Integer = If(muestra = 1, 11, 8)
                    For i As Integer = 0 To tabl.Rows.Count - 1
                        tabl.Columns(column).ReadOnly = False
                        tAcumulado = SumVal(CInt(tabl.Rows(i).Item("Tsetup").ToString), CInt(tabl.Rows(i).Item("TRuntime").ToString), tAcumulado)
                        tabl.Rows(i).Item(column) = tAcumulado
                    Next
                    tAcumulado = 0
                    With DataGridView3
                        .DataSource = tabl
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Visible = True
                    End With
                End If
            End If
            vertagasignado = 1
            cmd = New SqlCommand($"select TAG,bw.PN,(select NULLif({If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "Maq", "Cell")},'S/E') from tbl{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO where {If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO = (select MAX(WO) from tblBOMWIPRelationsTagsDet where tblBOMWIPRelationsTagsDet.TAG=ml.TAG and WO like '{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}%')) [Ultima Maquina usada],convert(date,OutDate) [OutDate] From tblBOM{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO ='" + lblcwomat.Text + "' and bw.Balance > 0 and bw.PN not like 'S%' and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0", cnn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120000
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            cnn.Close()
            If tb.Rows.Count > 0 Then
                With DataGridView4
                    .DataSource = tb
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .Visible = True
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message + vbNewLine + ex.ToString)
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("llenagrid'Materiales'", host, UserName)
            cnn.Close()
        End Try
    End Sub
    Private Sub Llenagrid1() 'Aplicadores
        Dim tabla As New DataTable, tb As New DataTable
        Try
            cmd = New SqlCommand($"select Wire,TermA,SUBSTRING(isnull(AplicatorA,0),0,4)[AplicatorA],IDKeyA,MaqA,TermB,SUBSTRING(isnull(AplicatorB,0),0,4)[AplicatorB],IDKeyB,MaqB,PTA, QAPTA,EngPTA,WTAWG,WC,WA,IC,IA from tblWipDet inner join tblTermSpecs on tblTermSpecs.IDKey=tblWipDet.IDKeyA where {If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "P", $"PWOA='{lblcwomat.Text}' or PWOB='{lblcwomat.Text}'", $"CWO='{lblcwomat.Text}'")} group by Wire,TermA,AplicatorA,IDKeyA,MaqA,TermB,AplicatorB,IDKeyB,MaqB,PTA, QAPTA,EngPTA,WTAWG,WC,WA,IC,IA", cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            With dgvBOM
                .DataSource = tabla
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Label2.Text = "Items: " & .Rows.Count
                DataGridView1.Visible = False
            End With
            Recorregrid()
            cmd = New SqlCommand($"select AU,WID,Wire,LengthWire,TermA,MaqA,AplicatorA,TermB,MaqB,AplicatorB,WIP from tblWipDet where {If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "P", $"PWOA='{lblcwomat.Text}' or PWOB='{lblcwomat.Text}'", $"CWO='{lblcwomat.Text}'")} order by WireID", cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            With DataGridView3
                .DataSource = tb
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("llenagrid1'Materiales'", host, UserName)
        End Try
    End Sub
    Private Sub Llenagrid2()
        Dim query As String = $"Select bw.PN,bw.Description,CONVERT(int,bw.Qty) [Qty] ,CONVERT(int,bw.Balance) [Balance],(select isnull(Convert(int,SUM(Balance)),0) from tblItemsTags where tblItemsTags.PN=bw.PN and Status='NoAvailable' and Balance>0) [En Piso],(select isnull(Convert(int,SUM(Balance)),0) from tblItemsTags where tblItemsTags.PN=bw.PN and Status='Available' and Balance>0) [Almacen],Convert(Int, ml.QtyOnHand) [Total],Convert(Int, ml.QtyOnHand) - CONVERT(int,bw.Balance) [Dif],Convert(Int, ml.QtyOnOrder) [In Transit], '' [Locaciones],ISNULL(bw.TagAsignado,0) [TagAsignado],(SELECT TOP(1) JuarezDueDate FROM tblItemsPOsDet AS A INNER JOIN tblItemsPOs AS B ON A.IDPO = B.IDPO WHERE A.PN = bw.PN AND A.QtyBalance > 0 AND B.Status = 'OPEN'  AND A.Confirmed = 1 ORDER BY A.JuarezDueDate) [Next Fecha Recibo] From tblBOM{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO As bw inner Join tblItemsQB As ml On bw.PN = ml.PN Where bw.{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO ='" + lblcwomat.Text + "' and bw.PN not like 'S%' group by bw.PN,bw.Description,bw.Qty,bw.Balance,ml.QtyOnHand,ml.QtyOnOrder,bw.TagAsignado"
        Dim muestra As Integer = 0, query2 As String = ""
        Dim tabla As New DataTable, tabl As New DataTable()
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If tabla.Rows.Count > 0 Then
                For i As Integer = 0 To tabla.Rows.Count - 1
                    tabla.Columns(9).ReadOnly = False
                    tabla.Columns(9).MaxLength = 400
                    tabla.Rows(i).Item(9) = Locacion(tabla.Rows(i).Item("PN").ToString)
                Next
            End If
            With DataGridView1
                .DataSource = tabla
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Chk").Visible = False
                Label2.Text = "Items: " & DataGridView1.RowCount
            End With
            Pintandoceldas()
            btnExportar.Visible = If(opcion = 2 And DataGridView1.Rows.Count > 0, True, False)
            If p = 10 Then
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    If ConfirmarMatAsignado(lblcwomat.Text, DataGridView1.Rows(i).Cells("PN").Value.ToString) Then
                        DataGridView1.Rows(i).Cells("PN").Style.BackColor = Color.LightBlue
                    Else
                        DataGridView1.Rows(i).Cells("PN").Style.BackColor = Color.Orange
                    End If
                Next
                If Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C" Then
                    cmd = New SqlCommand("select COUNT(*) from tblCWOSerialNumbers where CWO='" + lblcwomat.Text + "'", cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    muestra = If(CInt(cmd.ExecuteScalar) = 0, 0, 1)
                    cnn.Close()
                    query2 = If(muestra = 0, "select IdSort,Wire,TermA,MaqA,TermB,MaqB,IsNull(Tsetup,0) [TSetup],IsNull(TRuntime,0) [TRuntime],null as 'Acumulado' from tblWipDet where CWO='" + lblcwomat.Text + "' and WireBalance>0 order by IDSort", "select IdSort,det.Wire,det.WireBalance,det.TermA,TABalance,MaqA,det.TermB,TBBalance,MaqB,Tsetup,TRuntime,null as 'Acumulado' from tblWipDet det inner join tblCWOSerialNumbers cw on det.wireid=cw.WireID where det.CWO='" + lblcwomat.Text + "' and (Cutting is not null or det.WireBalance > 0) order by IDSort")
                    cmd = New SqlCommand(query2, cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    dr = cmd.ExecuteReader
                    tabl.Load(dr)
                    cnn.Close()
                    If tabl.Rows.Count > 0 Then
                        tAcumulado = 0
                        Dim column As Integer = If(muestra = 1, 11, 8)
                        For i As Integer = 0 To tabl.Rows.Count - 1
                            tabl.Columns(column).ReadOnly = False
                            tAcumulado = SumVal(tabl.Rows(i).Item("Tsetup").ToString, tabl.Rows(i).Item("TRuntime").ToString, tAcumulado)
                            tabl.Rows(i).Item(column) = tAcumulado
                        Next
                        tAcumulado = 0
                    End If
                    With DataGridView3
                        .DataSource = tabl
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Visible = True
                    End With
                End If
            End If
            vertagasignado = 1
            cmd = New SqlCommand($"select TAG,bw.PN,(select NULLif({If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "Maq", "Cell")},'S/E') from tbl{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO where {Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO = (select MAX(WO) from tblBOMWIPRelationsTagsDet where tblBOMWIPRelationsTagsDet.TAG=ml.TAG and WO like '{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}%')) [Ultima Maquina usada],convert(date,OutDate) [OutDate] From tblBOM{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO ='" + lblcwomat.Text + "' and bw.Balance > 0 and bw.PN not like 'S%' and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0", cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            cnn.Close()
            If tb.Rows.Count > 0 Then
                With DataGridView4
                    .DataSource = tb
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .Visible = True
                End With
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias" + vbNewLine + ex.ToString)
            CorreoFalla.EnviaCorreoFalla("llenagrid2'Materiales'", host, UserName)
        End Try
    End Sub
    Private Sub Llenagrid3() 'Materiales sin stock para poner en hold
        Dim query As String = $"Select bw.PN,bw.Description,CONVERT(int,bw.Qty) [Qty] ,CONVERT(int,bw.Balance) [Balance],(select isnull(Convert(int,SUM(Balance)),0) from tblItemsTags where tblItemsTags.PN=bw.PN and Status='NoAvailable' and Balance>0) [En Piso],(select isnull(Convert(int,SUM(Balance)),0) from tblItemsTags where tblItemsTags.PN=bw.PN and Status='Available' and Balance>0) [Almacen],Convert(Int, ml.QtyOnHand) [Total],Convert(Int, ml.QtyOnHand) - CONVERT(int,bw.Balance) [Dif],Convert(Int, ml.QtyOnOrder) [In Transit], '' [Locaciones],(SELECT TOP(1) JuarezDueDate FROM tblItemsPOsDet AS A INNER JOIN tblItemsPOs AS B ON A.IDPO = B.IDPO WHERE A.PN = bw.PN AND A.QtyBalance > 0 AND B.Status = 'OPEN'  AND A.Confirmed = 1 ORDER BY A.JuarezDueDate) [Next Fecha Recibo] From tblBOM{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO As bw inner Join tblItemsQB As ml On bw.PN = ml.PN Where bw.{If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO ='" + lblcwomat.Text + "' and bw.PN not like 'S%' and Hold = 0 group by bw.PN,bw.Description,bw.Qty,bw.Balance,ml.QtyOnHand,ml.QtyOnOrder"
        Dim tabla As New DataTable
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            cnn.Close()
            If tabla.Rows.Count > 0 Then
                For i As Integer = 0 To tabla.Rows.Count - 1
                    tabla.Columns(9).ReadOnly = False
                    tabla.Columns(9).MaxLength = 400
                    tabla.Rows(i).Item(9) = Locacion(tabla.Rows(i).Item("PN").ToString)
                Next
                With DataGridView1
                    .DataSource = tabla
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Label2.Text = "Items: " & DataGridView1.Rows.Count
                End With
                Pintandoceldas()
                btnExportar.Visible = opcion = 2 And DataGridView1.RowCount > 0
            Else
                DataGridView1.DataSource = Nothing
                MessageBox.Show("No hay numeros de parte para colocar cortos.")
                TabControl1.Parent = Nothing
                p = 0
                Me.Dispose()
                Me.Close()
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("llenagrid3'Materiales'", host, UserName)
        End Try
    End Sub
    Private Sub LlenaGridMaterialesParaCorte() 'Carga consulta para corte
        Try
            Dim tabla As New DataTable, tabl As New DataTable, tb As New DataTable
            Dim query As String = $"Select bw.PN,bw.Description,CONVERT(int,bw.Qty) [Qty] ,CONVERT(int,bw.Balance) [Balance],Convert(Int, ml.QtyOnHand) [Total],Convert(Int, ml.QtyOnHand) - CONVERT(int,bw.Balance) [Dif],ISNULL(bw.TagAsignado,0) [TagAsignado] From tblBOM{If(opcion = 8, "P", "C")}WO As bw inner Join tblItemsQB As ml On bw.PN = ml.PN Where bw.{If(opcion = 8, "P", "C")}WO ='" + lblcwomat.Text + "' and bw.Balance > 0 and bw.PN not like 'S%' group by bw.PN,bw.Description,bw.Qty,bw.Balance,ml.QtyOnHand,ml.QtyOnOrder,bw.TagAsignado"
            Dim muestra As Integer = 0, query2 As String = ""
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If tabla.Rows.Count > 0 Then
                With dgvBOM
                    .DataSource = tabla
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Label2.Text = "Items: " & dgvBOM.Rows.Count
                End With
            Else
                dgvBOM.DataSource = Nothing
                Label2.Text = "Items: " & dgvBOM.Rows.Count
            End If
            If Not opcion = 8 Then
                cmd = New SqlCommand("select COUNT(*) from tblCWOSerialNumbers where CWO='" + lblcwomat.Text + "'", cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                muestra = If(CInt(cmd.ExecuteScalar) = 0, 0, 1)
                cnn.Close()
                query2 = If(muestra = 0, "select IdSort,Wire,TermA,MaqA,TermB,MaqB,IsNull(Tsetup,0) [TSetup],IsNull(TRuntime,0) [TRuntime],null as 'Acumulado' from tblWipDet where CWO='" + lblcwomat.Text + "' and WireBalance>0 order by IDSort", "select IdSort,det.Wire,det.WireBalance,det.TermA,TABalance,MaqA,det.TermB,TBBalance,MaqB,Tsetup,TRuntime,null as 'Acumulado' from tblWipDet det inner join tblCWOSerialNumbers cw on det.wireid=cw.WireID where det.CWO='" + lblcwomat.Text + "' and (Cutting is not null or det.WireBalance > 0) order by IDSort")
                cmd = New SqlCommand(query2, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tabl.Load(dr)
                cnn.Close()
                If tabl.Rows.Count > 0 Then
                    tAcumulado = 0
                    Dim column As Integer = If(muestra = 1, 11, 8)
                    For i As Integer = 0 To tabl.Rows.Count - 1
                        tabl.Columns(column).ReadOnly = False
                        tAcumulado = SumVal(tabl.Rows(i).Item("Tsetup").ToString, tabl.Rows(i).Item("TRuntime").ToString, tAcumulado)
                        tabl.Rows(i).Item(column) = tAcumulado
                    Next
                    tAcumulado = 0
                    With DataGridView3
                        .DataSource = tabl
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Visible = True
                    End With
                End If
            End If
            vertagasignado = 1
            cmd = New SqlCommand($"select TAG,bw.PN,(select NULLif({If(opcion = 8, "Cell", "Maq")},'S/E') from tbl{If(opcion = 8, "P", "C")}WO where {If(opcion = 8, "P", "C")}WO = (select MAX(WO) from tblBOMWIPRelationsTagsDet where tblBOMWIPRelationsTagsDet.TAG=ml.TAG and WO like '{If(opcion = 8, "P", "C")}%')) [Ultima Maquina usada],convert(date,OutDate) [OutDate] From tblBOM{If(opcion = 8, "P", "C")}WO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.{If(opcion = 8, "P", "C")}WO ='" + lblcwomat.Text + "' and bw.Balance > 0 and bw.PN not like 'S%' and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0", cnn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120000
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            cnn.Close()
            If tb.Rows.Count > 0 Then
                With DataGridView4
                    .DataSource = tb
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .Visible = True
                End With
            End If
            PintaMaterialesConAsignacion()
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias" + vbNewLine + ex.ToString)
            CorreoFalla.EnviaCorreoFalla("llenaGridMaterialesParaCorte", host, UserName)
        End Try
    End Sub
    Private Function Locacion(pn As String) As String
        Try
            Dim resultado As String = ""
            Dim consulta As String = "declare @locaciones as nvarchar(500)
                                  set @locaciones=''
                                  select @locaciones=@locaciones + T.Location + ',' from
                                  (select distinct Location from tblItemsTags where PN= '" + pn.ToString + "' and Balance > 0 and Status='Available' group by Location) as T
                                  if @locaciones <> ''
                                  begin
                                  set @locaciones=LEFT(@locaciones, len(@locaciones) - 1)
                                  select @locaciones [Locaciones]
                                  end
                                  else
                                  begin
                                  set @locaciones= 'S/L'
                                  select @locaciones [Locaciones]
                                  end"
            Dim comand As SqlCommand = New SqlCommand(consulta, cnn)
            cnn.Open()
            resultado = comand.ExecuteScalar
            cnn.Close()
            Return resultado
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("locacion", host, UserName)
            Return Nothing
        End Try
    End Function
    Private Function ConfirmarMatAsignado(ByVal cwo As String, pn As String) As Boolean
        Try
            Dim muestra As Boolean
            Dim t As New DataTable
            Dim query As String = $"select TagAsignado from tblBOM{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO where {Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO=@CWO and PN= @PN and TagAsignado is not null"
            Dim comand As SqlCommand = New SqlCommand(query, cnn)
            comand.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = cwo
            comand.Parameters.Add("@PN", SqlDbType.NVarChar).Value = pn
            cnn.Open()
            muestra = If(CStr(comand.ExecuteScalar) = "", True, False)
            cnn.Close()
            Return muestra
        Catch ex As Exception
            cnn.Close()
            CorreoFalla.EnviaCorreoFalla("ConfirmarMatAsignado", host, UserName)
            Return Nothing
        End Try
    End Function
    Private Sub Pintandoceldas()
        Try
            For Each linea As DataGridViewRow In dgvBOM.Rows
                If linea.Cells(2).Value <= linea.Cells(6).Value Then
                    linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
                Else
                    linea.DefaultCellStyle.BackColor = Color.Red
                End If
                If linea.Cells(3).Value <= linea.Cells(6).Value Then
                    linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
                Else
                    linea.DefaultCellStyle.BackColor = Color.Yellow
                End If
            Next
            For Each linea As DataGridViewRow In DataGridView1.Rows
                If linea.Cells(3).Value <= linea.Cells(7).Value Then
                    linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
                Else
                    linea.DefaultCellStyle.BackColor = Color.Red
                End If
                If linea.Cells(4).Value <= linea.Cells(7).Value Then
                    linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
                Else
                    linea.DefaultCellStyle.BackColor = Color.Yellow
                End If
                If p = 12 Then
                    Dim PN As String = ""
                    PN = linea.Cells(1).Value.ToString
                    If Microsoft.VisualBasic.Left(PN, 1) <> "W" Then
                        If Principal.AllocatedAQty("select COUNT(PnOriginal) AS QTY from TblDesviacionesTerm WHERE PnOriginal = '" & PN & "'") > 0 Then
                            linea.DefaultCellStyle.BackColor = Color.MintCream
                            linea.DefaultCellStyle.BackColor = Color.DodgerBlue
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("pintandoceldas", host, UserName)
        End Try
    End Sub
    Private Sub PintaMaterialesConAsignacion()
        Try
            For Each linea As DataGridViewRow In dgvBOM.Rows
                If linea.Cells(6).Value = 1 Then
                    linea.DefaultCellStyle.BackColor = Color.LightGreen
                End If
            Next
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("PintaMaterialesConAsignacion", host, UserName)
        End Try
    End Sub
    Private Sub Recorregrid()
        Try
            For i As Integer = 0 To dgvBOM.Rows.Count - 1
                If CInt(Val(dgvBOM.Rows(i).Cells("AplicatorA").Value.ToString)) > 0 Then
                    If DisponibilidadApl(dgvBOM.Rows(i).Cells("AplicatorA").Value.ToString) Then
                        dgvBOM.Rows(i).Cells("AplicatorA").Style.BackColor = Color.Red
                        dgvBOM.Rows(i).Cells("AplicatorA").Style.ForeColor = Color.Black
                    Else
                        dgvBOM.Rows(i).Cells("AplicatorA").Style.BackColor = Color.Green
                        dgvBOM.Rows(i).Cells("AplicatorA").Style.ForeColor = Color.Black
                    End If
                End If
                If CInt(Val(dgvBOM.Rows(i).Cells("AplicatorB").Value.ToString)) > 0 Then
                    If DisponibilidadApl(dgvBOM.Rows(i).Cells("AplicatorB").Value.ToString) Then
                        dgvBOM.Rows(i).Cells("AplicatorB").Style.BackColor = Color.Red
                        dgvBOM.Rows(i).Cells("AplicatorB").Style.ForeColor = Color.Black
                    Else
                        dgvBOM.Rows(i).Cells("AplicatorB").Style.BackColor = Color.Green
                        dgvBOM.Rows(i).Cells("AplicatorB").Style.ForeColor = Color.Black
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("recorregrid", host, UserName)
        End Try
    End Sub
    Private Function DisponibilidadApl(apl As String) As Boolean
        Try
            Dim query As String = $"select Count(*) from tblApl where OemAplID='{apl}' and [AssignedTo]='Produccion'"
            comando = New SqlCommand(query, conexion)
            comando.CommandType = CommandType.Text
            conexion.Open()
            Return CInt(comando.ExecuteScalar) > 0
            conexion.Close()
        Catch ex As Exception
            conexion.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("DisponibilidadApl", host, UserName)
            Return Nothing
        Finally
            conexion.Close()
        End Try
    End Function
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If LinkLabel1.Text = "Almacen" Then
                Process.Start("\\10.17.182.12\Test\WareHouse\Almacen\ConPeso\Almacen.exe")
            ElseIf LinkLabel1.Text = "ACS" Then
                Process.Start("\\10.17.182.12\Test\ACS\ACS.application")
            End If
        Catch ex As Exception
            MsgBox("No se puede iniciar la aplicacion, debido a que no cuentas con los permisos requeridos, si deseas consultar en esta aplicacion, solicita los accesos necesarios, gracias")
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub DateTimePicker1_MouseDown(sender As Object, e As MouseEventArgs) Handles DateTimePicker1.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.DateTimePicker1.Format = DateTimePickerFormat.Short
            If DateTimePicker1.Value = "2021-01-01" Then
                Me.DateTimePicker1.Value = Date.Today
            End If
        End If
    End Sub
    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        Pintandoceldas()
    End Sub
    Private Sub dgvBOM_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvBOM.ColumnHeaderMouseClick
        If e.RowIndex > -1 Then
            If opcion = 1 Or opcion = 4 Then
                PintaMaterialesConAsignacion()
            Else
                Pintandoceldas()
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If holdoconfir = 1 Then
                Dim lst As New List(Of String)
                If TextBox2.Text <> "" Then
                    Dim mensaje As String = ""
                    If Label4.Text <> "" Then
                        If DataGridView1.Rows.Count > 0 Then
                            For o As Integer = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView1.Rows(o).Cells("chk").Value = True Then
                                    lst.Add(DataGridView1.Rows(o).Cells("PN").Value.ToString)
                                    UpdateHoldPN(lblcwomat.Text, DataGridView1.Rows(o).Cells("PN").Value.ToString)
                                    If mensaje = "" Then
                                        If CheckMovNegative(DataGridView1.Rows(o).Cells("PN").Value.ToString, DataGridView1.Rows(o).Cells("Balance").Value.ToString) = True Then
                                            mensaje = DataGridView1.Rows(o).Cells("PN").Value.ToString & " (Ajustes Neg)"
                                        Else
                                            mensaje = DataGridView1.Rows(o).Cells("PN").Value.ToString
                                        End If
                                    ElseIf mensaje <> "" Then
                                        If CheckMovNegative(DataGridView1.Rows(o).Cells("PN").Value.ToString, DataGridView1.Rows(o).Cells("Balance").Value.ToString) = True Then
                                            mensaje = mensaje + "; " + DataGridView1.Rows(o).Cells("PN").Value.ToString & " (Ajustes Neg)"
                                        Else
                                            mensaje = mensaje + "; " + DataGridView1.Rows(o).Cells("PN").Value.ToString
                                        End If
                                    End If
                                End If
                            Next
                            mensaje = mensaje + " se han puesto en Cortos por falta de material, en el WIP: " & Label4.Text & $" y {If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO: " & lblcwomat.Text & " por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + "" + vbNewLine + " Verificalo y asigna una nueva fecha de material."
                        Else
                            mensaje = "Se notifica que se pone el WIP: " & Label4.Text & $" y {If(Microsoft.VisualBasic.Left(lblcwomat.Text, 1) = "C", "C", "P")}WO: " & lblcwomat.Text & " en Hold por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + " pero," + vbNewLine + " sin numeros de parte que esten sin stock, por favor verificarlo"
                        End If
                        'CorreoFalla.EnviaCorreoHoldMat(mensaje) 'Esto descomentar al momento de subir el codigo
                        Principal.NotifyIcon1.BalloonTipText = "Se han notificado los cambios a Compras"
                        Principal.NotifyIcon1.BalloonTipTitle = "Material sin stock"
                        Principal.NotifyIcon1.Visible = True
                        Principal.NotifyIcon1.ShowBalloonTip(0)
                        Principal.NotesWIPandCWOOnHold(lblcwomat.Text, Convert.ToDateTime(Now), TextBox2.Text)
                        lst.ForEach(Function(pn)
                                        Principal.CheckCortosPN(pn)
                                        Return Nothing
                                    End Function)
                        Principal.Filtros(3)
                        holdoconfir = 0
                        MessageBox.Show("Numeros de Parte colocados en status corto con exito")
                        Me.Dispose()
                        Me.Close()
                    Else
                        If DataGridView1.Rows.Count > 0 Then
                            For o As Integer = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView1.Rows(o).Cells("chk").Value = True Then
                                    UpdateHoldPN(lblcwomat.Text, DataGridView1.Rows(o).Cells("PN").Value.ToString)
                                    lst.Add(DataGridView1.Rows(o).Cells("PN").Value.ToString)
                                    If mensaje = "" Then
                                        If CheckMovNegative(DataGridView1.Rows(o).Cells("PN").Value.ToString, DataGridView1.Rows(o).Cells("Balance").Value.ToString) = True Then
                                            mensaje = DataGridView1.Rows(o).Cells("PN").Value.ToString & " (Ajustes Neg)"
                                        Else
                                            mensaje = DataGridView1.Rows(o).Cells("PN").Value.ToString
                                        End If
                                    ElseIf mensaje <> "" Then
                                        If CheckMovNegative(DataGridView1.Rows(o).Cells("PN").Value.ToString, DataGridView1.Rows(o).Cells("Balance").Value.ToString) = True Then
                                            mensaje = mensaje + "; " + DataGridView1.Rows(o).Cells("PN").Value.ToString & " (Ajustes Neg)"
                                        Else
                                            mensaje = mensaje + "; " + DataGridView1.Rows(o).Cells("PN").Value.ToString
                                        End If
                                    End If
                                End If
                            Next
                            mensaje = mensaje + " se han puesto en Hold por falta de material, en el CWO: " & lblcwomat.Text & " por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + "" + vbNewLine + " Verificalo y asigna una nueva fecha de material."
                        Else
                            mensaje = "Se notifica que se pone el WO: " & lblcwomat.Text & " en Hold por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + " pero," + vbNewLine + " sin numeros de parte que esten sin stock, por favor verificarlo"
                        End If
                        'CorreoFalla.EnviaCorreoHoldMat(mensaje) 'Esto descomentar al momento de subir el codigo
                        Principal.NotifyIcon1.BalloonTipText = "Se han notificado los cambios a Compras"
                        Principal.NotifyIcon1.BalloonTipTitle = "Material sin stock"
                        Principal.NotifyIcon1.Visible = True
                        Principal.NotifyIcon1.ShowBalloonTip(0)
                        Principal.NotesWIPandCWOOnHold(lblcwomat.Text, Convert.ToDateTime(Now), TextBox2.Text)
                        lst.ForEach(Function(pn)
                                        Principal.CheckCortosPN(pn)
                                        Return Nothing
                                    End Function)
                        Principal.Filtros(3)
                        holdoconfir = 0
                        MessageBox.Show("Numeros de Parte colocados en status corto con exito")
                        Me.Dispose()
                        Me.Close()
                    End If
                Else
                    MsgBox("Debe agregar una nota")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("Button2_Click'Materiales'", host, UserName)
        End Try
    End Sub
    Function CheckMovNegative(PN As String, qty As Integer) As Boolean
        Try
            Dim resp As Boolean
            query = "select case when Qty > (select SUM(QtyActual-QtyNew) from tblItemsAdjustmentTAGs where tblItemsTags.TAG=tblItemsAdjustmentTAGs.TAG and AdjusmentType='Negative') then 'Ajuste negativo' end [Ajuste],PN from tblItemsTags where PN=@PN and (Qty >= @QTY or qty <= @QTY) and qty= (case when Qty > (select SUM(QtyActual-QtyNew) from tblItemsAdjustmentTAGs where tblItemsTags.TAG=tblItemsAdjustmentTAGs.TAG and AdjusmentType='Negative') then Qty end)"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandTimeout = 120000
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN
            cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = qty
            cnn.Open()
            dr = cmd.ExecuteReader
            resp = dr.HasRows
            cnn.Close()
            Return resp
        Catch ex As Exception
            cnn.Close()
            Return False
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("CheckMovNegative", host, UserName)
        End Try
    End Function
    Public Sub UpdateHoldPN(
            CWO As String,
            PN As String)
        Try
            query = $"update tblBOM{Microsoft.VisualBasic.Left(CWO, 1)}WO set Hold=1 where {Microsoft.VisualBasic.Left(CWO, 1)}WO='" + CWO + "' and PN='" + PN + "'"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim values As String = ""
        For jj = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(jj).Cells("Chk").Value Then
                If values <> "" Then
                    values = values + DataGridView1.Rows(jj).Cells("PN").Value.ToString + ", "
                Else
                    values = DataGridView1.Rows(jj).Cells("PN").Value.ToString + ", "
                End If
            End If
        Next
        If values.Length > 0 Then
            holdoconfir = 1
            gbnotasconfirmando.Visible = True
            TextBox2.Text = values
            'Dim qtylen As Integer = Len(values)
            TextBox2.Focus()
            TextBox2.[Select](TextBox2.Text.Length, Len(values))
        Else
            MsgBox("Debe seleccionar minimo un numero de parte a agregar a las notas")
        End If
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            gbTAGS.Visible = True
            txbTAGSxentrar.Focus()
        Else
            gbTAGS.Visible = False
        End If
    End Sub
    Private Function Verificatag(tag As String) As Boolean 'Nuevo
        Dim res As Boolean
        Dim tb As New DataTable
        Try
            cmd = New SqlCommand("select PN,Balance,AssignedTo from tblItemsTags where TAG = @tag and Status = 'Available' and Balance > 0", cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@tag", SqlDbType.NVarChar).Value = tag
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            cnn.Close()
            If tb.Rows.Count > 0 Then
                res = True
                pn = tb.Rows(0).Item("PN").ToString
                balance = CInt(Val(tb.Rows(0).Item("Balance").ToString))
            Else
                res = False
                pn = ""
                balance = 0
            End If
            Return res
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            pn = ""
            balance = 0
            cnn.Close()
            Return Nothing
        End Try
    End Function
    Private Sub btnTerminarescaneo_Click(sender As Object, e As EventArgs) Handles btnTerminarescaneo.Click
        Dim marca As Integer = 0
        Try
            If DataGridView2.Rows.Count > 0 Then
                Dim subquery As StringBuilder = New StringBuilder("")
                For I As Integer = 0 To DataGridView2.Rows.Count - 1
                    Dim cmdo As SqlCommand = New SqlCommand("update tblItemsTags set AssignedTo='" + lblcwomat.Text.ToString + "',Status='NoAvailable', OutDate=GETDATE() where TAG='" + DataGridView2.Rows(I).Cells(0).Value.ToString + "'", cnn)
                    cmdo.CommandType = CommandType.Text
                    cnn.Open()
                    cmdo.ExecuteReader()
                    cnn.Close()
                Next
                For a As Integer = 0 To DataGridView2.Rows.Count - 1
                    subquery.Append($"'{Convert.ToString(DataGridView2.Rows(a).Cells(1).Value)}',")
                Next
                Dim update As String = $"update tblBOM{Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO set TagAsignado=1 where {Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO= '" + lblcwomat.Text.ToString + "' and PN in (" + subquery.ToString.TrimEnd(",").Trim() + ")"
                Dim comm As SqlCommand = New SqlCommand(update, cnn)
                comm.CommandType = CommandType.Text
                cnn.Open()
                comm.ExecuteReader()
                cnn.Close()
                If MAtAsigcomplete(lblcwomat.Text.ToString.Trim) Then
                    MsgBox($"Ya han sido asignados todos los materiales para este {Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO")
                    Me.Dispose()
                    Me.Close()
                Else
                    DataGridView2.DataSource = Nothing
                    Llenagrid2()
                End If
            Else
                MsgBox($"No hay materiales para cargar al {Microsoft.VisualBasic.Left(lblcwomat.Text, 1)}WO")
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            CorreoFalla.EnviaCorreoFalla("btnTerminarescaneo_Click", host, UserName)
        End Try
    End Sub
    Private Sub DataGridView2_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Try
                    DataGridView2.CurrentCell = DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    DataGridView2.Rows(e.RowIndex).Selected = True
                    FilaSeleccionada = Convert.ToString(DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                Catch ex As Exception
                    MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
                    CorreoFalla.EnviaCorreoFalla("DataGridView2_CellMouseDown", host, UserName)
                End Try
            End If
        End If
    End Sub
    Private Sub DataGridView2_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView2.MouseClick
        If DataGridView2.RowCount > 0 Then
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                eliminartagmenu.Show(Cursor.Position.X, Cursor.Position.Y)
            End If
        End If
    End Sub
    Private Sub ToolStripTextBox1_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox1.Click
        If DataGridView2.RowCount > 0 Then
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Rows(i).Cells(0).Value.ToString = FilaSeleccionada Then
                    DataGridView2.Rows.Remove(DataGridView2.Rows(i))
                End If
            Next
            lbltagsescaneados.Text = "Tag's Escaneados: " & CInt(Val(DataGridView2.Rows.Count))
            eliminartagmenu.Close()
        End If
    End Sub
    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        If dgvBOM.Rows.Count > 0 Then
            Dim save As New SaveFileDialog
            Dim ruta As String
            Dim xlApp As Object = CreateObject("Excel.Application")
            Dim pth As String = ""
            'se crea una nueva hoja de calculo
            Dim xlwb As Object = xlApp.WorkBooks.add
            Dim xlws As Object = xlwb.WorkSheets(1)
            Try
                'exportaremos los caracteres de las columnas
                For c As Integer = 0 To dgvBOM.Columns.Count - 1
                    xlws.cells(1, c + 1).value = dgvBOM.Columns(c).HeaderText
                Next
                'exportaremos las cabeceras de las columnas
                For r As Integer = 0 To dgvBOM.RowCount - 1
                    For c As Integer = 0 To dgvBOM.Columns.Count - 1
                        xlws.cells(r + 2, c + 1).value = Convert.ToString(dgvBOM.Item(c, r).Value)
                    Next
                Next
                'guardamos la hoja de excel en la ruta espesifica
                Dim SaveFileDialog1 As SaveFileDialog = New SaveFileDialog
                SaveFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                SaveFileDialog1.Filter = "Archivo Excel|*.xlsx"
                SaveFileDialog1.FileName = "Lista de Materiales - " + Date.Today.ToString("dd-MMM-yy")
                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                    ruta = SaveFileDialog1.FileName
                    xlwb.saveas(ruta)
                    xlws = Nothing
                    xlwb = Nothing
                    xlApp.Columns.AutoFit()
                    xlApp.visible = True
                End If
            Catch ex As Exception
                MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
                CorreoFalla.EnviaCorreoFalla("btnExportar_Click()", host, UserName)
            End Try
        ElseIf DataGridView1.Rows.Count > 0 Then
            Dim save As New SaveFileDialog
            Dim ruta As String
            Dim xlApp As Object = CreateObject("Excel.Application")
            Dim pth As String = ""
            'se crea una nueva hoja de calculo
            Dim xlwb As Object = xlApp.WorkBooks.add
            Dim xlws As Object = xlwb.WorkSheets(1)
            Try
                'exportaremos los caracteres de las columnas
                For c As Integer = 0 To DataGridView1.Columns.Count - 1
                    xlws.cells(1, c + 1).value = DataGridView1.Columns(c).HeaderText
                Next
                'exportaremos las cabeceras de las columnas
                For r As Integer = 0 To DataGridView1.RowCount - 1
                    For c As Integer = 0 To DataGridView1.Columns.Count - 1
                        xlws.cells(r + 2, c + 1).value = Convert.ToString(DataGridView1.Item(c, r).Value)
                    Next
                Next
                'guardamos la hoja de excel en la ruta espesifica
                Dim SaveFileDialog1 As SaveFileDialog = New SaveFileDialog
                SaveFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                SaveFileDialog1.Filter = "Archivo Excel|*.xlsx"
                SaveFileDialog1.FileName = "Lista de Materiales - " + Date.Today.ToString("dd-MMM-yy")
                If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                    ruta = SaveFileDialog1.FileName
                    xlwb.saveas(ruta)
                    xlws = Nothing
                    xlwb = Nothing
                    xlApp.Columns.AutoFit()
                    xlApp.visible = True
                End If
            Catch ex As Exception
                MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
                CorreoFalla.EnviaCorreoFalla("btnExportar_Click()", host, UserName)
            End Try
        Else
            MsgBox("No existen items para exportar!!", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub DataGridView1_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnAdded
        e.Column.SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub
    Private Sub DataGridView3_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView3.ColumnAdded
        e.Column.SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub
    Private Sub dgvBOM_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBOM.CellClick
        Dim cabecera As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            cabecera = Me.dgvBOM.Columns(cdx).HeaderText
        End If
        If cabecera = "PN" Then
            Me.DataGridView3.DefaultCellStyle.BackColor = DefaultBackColor
            If Microsoft.VisualBasic.Left(dgvBOM.Rows(e.RowIndex).Cells("PN").Value.ToString, 1) = "W" Then
                For i As Integer = 0 To DataGridView3.Rows.Count - 1
                    If dgvBOM.Rows(e.RowIndex).Cells("PN").Value.ToString = DataGridView3.Rows(i).Cells("Wire").Value.ToString Then
                        DataGridView3.Rows(i).Cells("Wire").Style.BackColor = Color.LightSeaGreen
                        DataGridView3.Rows(i).Cells("TermA").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("TermB").Style.BackColor = DefaultBackColor
                    Else
                        DataGridView3.Rows(i).Cells("Wire").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("TermA").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("TermB").Style.BackColor = DefaultBackColor
                    End If
                Next
            ElseIf Microsoft.VisualBasic.Left(dgvBOM.Rows(e.RowIndex).Cells("PN").Value.ToString, 1) = "T" Then
                For i As Integer = 0 To DataGridView3.Rows.Count - 1
                    If dgvBOM.Rows(e.RowIndex).Cells("PN").Value.ToString = DataGridView3.Rows(i).Cells("TermA").Value.ToString Then
                        DataGridView3.Rows(i).Cells("TermA").Style.BackColor = Color.LightSeaGreen
                        DataGridView3.Rows(i).Cells("TermB").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("Wire").Style.BackColor = DefaultBackColor
                    Else
                        DataGridView3.Rows(i).Cells("TermA").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("TermB").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("Wire").Style.BackColor = DefaultBackColor
                    End If
                    If dgvBOM.Rows(e.RowIndex).Cells("PN").Value.ToString = DataGridView3.Rows(i).Cells("TermB").Value.ToString Then
                        DataGridView3.Rows(i).Cells("TermB").Style.BackColor = Color.LightSeaGreen
                        DataGridView3.Rows(i).Cells("TermA").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("Wire").Style.BackColor = DefaultBackColor
                    Else
                        DataGridView3.Rows(i).Cells("TermA").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("TermB").Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells("Wire").Style.BackColor = DefaultBackColor
                    End If
                Next
            End If
        End If
        If vertagasignado = 1 Then
            Try
                CheckBox2.Checked = False
                If e.RowIndex > -1 Then
                    If dgvBOM.Rows(e.RowIndex).Cells("TagAsignado").Value.ToString = 1 Then
                        Dim tabla As New DataTable
                        cmd = New SqlCommand("select TAG, PN from tblItemsTags where AssignedTo='" + lblcwomat.Text + "' and status='NoAvailable' and PN='" + dgvBOM.Rows(e.RowIndex).Cells("PN").Value.ToString + "'", cnn)
                        cmd.CommandType = CommandType.Text
                        cnn.Open()
                        dr = cmd.ExecuteReader
                        tabla.Load(dr)
                        edo = cnn.State.ToString
                        If edo = "Open" Then cnn.Close()
                        If tabla.Rows.Count > 0 Then
                            With DataGridView5
                                .DataSource = tabla
                                .AutoResizeColumns()
                                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            End With
                        Else
                            DataGridView5.DataSource = Nothing
                        End If
                    End If
                End If
            Catch ex As Exception
                MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            End Try
        End If
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim cabecera As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            cabecera = Me.DataGridView1.Columns(cdx).HeaderText
        End If
        If cabecera = "PN" Then
            DataGridView3.DefaultCellStyle.BackColor = DefaultBackColor
            If Microsoft.VisualBasic.Left(DataGridView1.Rows(e.RowIndex).Cells("PN").Value.ToString, 1) = "W" Then
                For i As Integer = 0 To DataGridView3.Rows.Count - 1
                    If DataGridView1.Rows(e.RowIndex).Cells("PN").Value.ToString = DataGridView3.Rows(i).Cells(1).Value.ToString Then
                        DataGridView3.Rows(i).Cells(1).Style.BackColor = Color.LightSeaGreen
                        DataGridView3.Rows(i).Cells(2).Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells(4).Style.BackColor = DefaultBackColor
                    Else
                        DataGridView3.Rows(i).Cells(1).Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells(2).Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells(4).Style.BackColor = DefaultBackColor
                    End If
                Next
            ElseIf Microsoft.VisualBasic.Left(DataGridView1.Rows(e.RowIndex).Cells("PN").Value.ToString, 1) = "T" Then
                For i As Integer = 0 To DataGridView3.Rows.Count - 1
                    If DataGridView1.Rows(e.RowIndex).Cells("PN").Value.ToString = DataGridView3.Rows(i).Cells(2).Value.ToString Then
                        DataGridView3.Rows(i).Cells(2).Style.BackColor = Color.LightSeaGreen
                        DataGridView3.Rows(i).Cells(1).Style.BackColor = DefaultBackColor
                    Else
                        DataGridView3.Rows(i).Cells(2).Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells(1).Style.BackColor = DefaultBackColor
                    End If
                    If DataGridView1.Rows(e.RowIndex).Cells("PN").Value.ToString = DataGridView3.Rows(i).Cells(4).Value.ToString Then
                        DataGridView3.Rows(i).Cells(4).Style.BackColor = Color.LightSeaGreen
                        DataGridView3.Rows(i).Cells(1).Style.BackColor = DefaultBackColor
                    Else
                        DataGridView3.Rows(i).Cells(4).Style.BackColor = DefaultBackColor
                        DataGridView3.Rows(i).Cells(1).Style.BackColor = DefaultBackColor
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Try
            If CheckBox2.Checked = True Then
                Dim consulta As String = "select TAG, PN from tblItemsTags where AssignedTo='" + lblcwomat.Text + "' and status='NoAvailable'"
                Dim tabla As New DataTable
                cmd = New SqlCommand(consulta, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tabla.Load(dr)
                edo = cnn.State.ToString
                If edo = "Open" Then cnn.Close()
                If tabla.Rows.Count > 0 Then
                    With DataGridView5
                        .DataSource = tabla
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    End With
                Else
                    DataGridView5.DataSource = Nothing
                End If
            ElseIf CheckBox2.Checked = False Then
                DataGridView5.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
        End Try
    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If p <> 10 Then
            GroupBox2.Visible = True
            TextBox1.Text = 10
            ItemsAdjustementTags(DataGridView1.Rows(e.RowIndex).Cells("PN").Value.ToString)
        End If
    End Sub
    Public Sub ItemsAdjustementTags(PN As String)
        Try
            Dim Query As String = "", dtAjus As New DataTable()
            PNAux = PN
            Query = "SELECT B.PN, A.TAG, (QtyActual - QtyNew) * -1 AS QtyAjuste, A.CreatedDate, A.CreatedBy, Reason, Amount, ApprovedBy, Notes, WIP FROM tblItemsAdjustmentTAGs AS A INNER JOIN tblItemsTags AS B ON A.TAG = B.TAG WHERE PN = @PN AND A.AdjusmentType = 'Negative' AND A.CreatedDate >= GETDATE() - @Dias ORDER BY A.CreatedDate, PN"
            cmd = New SqlCommand(Query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@Dias", SqlDbType.Int).Value = CInt(Val(TextBox1.Text))
            cmd.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN
            cnn.Open()
            dr = cmd.ExecuteReader
            dtAjus.Load(dr)
            cnn.Close()
            Me.AutoScroll = True
            DataGridView3.DataSource = dtAjus
            DataGridView3.Visible = True
        Catch ex As Exception
            cnn.Close()
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias" + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            ItemsAdjustementTags(PNAux)
        End If
    End Sub
    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Try
            If DataGridView3.Rows.Count > 0 Then
                Dim cabecera As String = ""
                Dim cdx As Integer = e.ColumnIndex
                Dim rdx As Integer = e.RowIndex
                If Not rdx = -1 Or cdx = -1 Then
                    cabecera = Me.DataGridView3.Columns(cdx).HeaderText
                End If
                If opcion <> 3 Then
                    If cabecera = "Wire" Then
                        If DataGridView1.Rows.Count > 0 And DataGridView1.Visible = True Then
                            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView3.Rows(e.RowIndex).Cells("Wire").Value.ToString = DataGridView1.Rows(i).Cells("PN").Value.ToString Then
                                    DataGridView1.Rows(i).Cells("PN").Style.ForeColor = Color.Red
                                Else
                                    DataGridView1.Rows(i).Cells("PN").Style.ForeColor = Color.Black
                                End If
                            Next
                        ElseIf dgvBOM.Rows.Count > 0 And dgvBOM.Visible = True Then
                            For i As Integer = 0 To dgvBOM.Rows.Count - 1
                                If DataGridView3.Rows(e.RowIndex).Cells("Wire").Value.ToString = dgvBOM.Rows(i).Cells("PN").Value.ToString Then
                                    dgvBOM.Rows(i).Cells("PN").Style.ForeColor = Color.Red
                                Else
                                    dgvBOM.Rows(i).Cells("PN").Style.ForeColor = Color.Black
                                End If
                            Next
                        End If
                    ElseIf cabecera = "TermA" Then
                        If DataGridView1.Rows.Count > 0 And DataGridView1.Visible = True Then
                            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView3.Rows(e.RowIndex).Cells("TermA").Value.ToString = DataGridView1.Rows(i).Cells("PN").Value.ToString Then
                                    DataGridView1.Rows(i).Cells("PN").Style.ForeColor = Color.Red
                                Else
                                    DataGridView1.Rows(i).Cells("PN").Style.ForeColor = Color.Black
                                End If
                            Next
                        ElseIf dgvBOM.Rows.Count > 0 And dgvBOM.Visible = True Then
                            For i As Integer = 0 To dgvBOM.Rows.Count - 1
                                If DataGridView3.Rows(e.RowIndex).Cells("TermA").Value.ToString = dgvBOM.Rows(i).Cells("PN").Value.ToString Then
                                    dgvBOM.Rows(i).Cells("PN").Style.ForeColor = Color.Red
                                Else
                                    dgvBOM.Rows(i).Cells("PN").Style.ForeColor = Color.Black
                                End If
                            Next
                        End If
                    ElseIf cabecera = "TermB" Then
                        If DataGridView1.Rows.Count > 0 And DataGridView1.Visible = True Then
                            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView3.Rows(e.RowIndex).Cells("TermB").Value.ToString = DataGridView1.Rows(i).Cells("PN").Value.ToString Then
                                    DataGridView1.Rows(i).Cells("PN").Style.ForeColor = Color.Red
                                Else
                                    DataGridView1.Rows(i).Cells("PN").Style.ForeColor = Color.Black
                                End If
                            Next
                        ElseIf dgvBOM.Rows.Count > 0 And dgvBOM.Visible = True Then
                            For i As Integer = 0 To dgvBOM.Rows.Count - 1
                                If DataGridView3.Rows(e.RowIndex).Cells("TermB").Value.ToString = dgvBOM.Rows(i).Cells("PN").Value.ToString Then
                                    dgvBOM.Rows(i).Cells("PN").Style.ForeColor = Color.Red
                                Else
                                    dgvBOM.Rows(i).Cells("PN").Style.ForeColor = Color.Black
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
        End Try
    End Sub
    Private Sub Materiales_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
    Private Function MAtAsigcomplete(cwo As String)
        Dim res As Boolean
        Dim query As String = $"select COUNT(PN) [matasignado],(select COUNT(PN) from tblBOM{Microsoft.VisualBasic.Left(cwo, 1)}WO where {Microsoft.VisualBasic.Left(cwo, 1)}WO='" + cwo.ToString + $"' and TagAsignado is null and PN not like 'S%') [Matnoasignado] from tblBOM{Microsoft.VisualBasic.Left(cwo, 1)}WO where {Microsoft.VisualBasic.Left(cwo, 1)}WO='" + lblcwomat.Text.ToString + "' and TagAsignado=1 and PN not like 'S%'"
        Try
            Dim commm As SqlCommand = New SqlCommand(query, cnn)
            Dim read As SqlDataReader
            commm.CommandType = CommandType.Text
            cnn.Open()
            read = commm.ExecuteReader()
            If read.HasRows Then
                While read.Read
                    res = CInt(read.GetValue(0)) = CInt(read.GetValue(1))
                End While
            Else
                res = False
            End If
            cnn.Close()
            Return res
        Catch ex As Exception
            cnn.Close()
            CorreoFalla.EnviaCorreoFalla("MAtAsigcomplete()", host, UserName)
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
            Return Nothing
        End Try
    End Function
    Private Sub txbTAGSxentrar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txbTAGSxentrar.KeyPress
        Try
            Dim existpn As Boolean = False, validaBalance As Boolean
            If e.KeyChar = Chr(13) Then
                If Verificatag(txbTAGSxentrar.Text) Then
                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                        If pn = DataGridView1.Rows(i).Cells("PN").Value.ToString Then
                            existpn = True
                            If CInt(Val(DataGridView1.Rows(i).Cells("Balance").Value.ToString)) > 0 Then
                                validaBalance = True
                            Else
                                MsgBox("El PN: " + pn.ToString + " que escaneaste ya no tiene balance")
                                validaBalance = False
                                Exit For
                            End If
                        End If
                    Next
                    If existpn = True And validaBalance = True Then
                        For i As Integer = 0 To DataGridView1.Rows.Count - 1
                            If pn = DataGridView1.Rows(i).Cells("PN").Value.ToString Then
                                If balance < CInt(Val(DataGridView1.Rows(i).Cells("Balance").Value.ToString)) Then
                                    MsgBox("El PN: " + pn.ToString + " que escaneaste no completa la cantidad requerida, favor de escanear mas tag's para completar la diferencia o un tag que complete la cantidad requerida, en caso de no a ver mas tag's, se sugiere poner en Hold")
                                    Exit For
                                End If
                            End If
                        Next
                        If DataGridView2.Rows.Count = 0 Then
                            Me.DataGridView2.Rows.Add()
                            DataGridView2.Rows(0).Cells(0).Value = txbTAGSxentrar.Text.ToString
                            DataGridView2.Rows(0).Cells(1).Value = pn.ToString
                        ElseIf DataGridView2.Rows.Count > 0 Then
                            Me.DataGridView2.Rows.Add()
                            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                                If DataGridView2.Rows(i).Cells(0).Value = Nothing Then
                                    DataGridView2.Rows(i).Cells(0).Value = txbTAGSxentrar.Text.ToString
                                    DataGridView2.Rows(i).Cells(1).Value = pn.ToString
                                End If
                            Next
                        End If
                        DataGridView2.DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                        txbTAGSxentrar.Clear()
                        txbTAGSxentrar.Focus()
                        lbltagsescaneados.Text = "Tag's Escaneados: " & CInt(Val(DataGridView2.Rows.Count))
                    Else
                        MsgBox("El Tag escaneado: " + txbTAGSxentrar.Text + " no contiene PN para este CWO" + vbNewLine + " Escanee otro")
                        txbTAGSxentrar.Clear()
                        txbTAGSxentrar.Focus()
                    End If
                Else
                    MsgBox("El tag que intentas escanear: " + txbTAGSxentrar.Text + " ya esta asignado a piso o a otro CWO")
                    txbTAGSxentrar.Clear()
                    txbTAGSxentrar.Focus()
                End If
            End If
        Catch ex As Exception
            CorreoFalla.EnviaCorreoFalla("txbTAGSxentrar_KeyPress()", host, UserName)
            MsgBox("Ha ocurrido un problema, ya se a reportado a departamento de IT, gracias")
        End Try
    End Sub
End Class