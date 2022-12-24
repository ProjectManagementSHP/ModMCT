Imports System.Data.SqlClient
Public Class Graficas
    Dim countProcess As Integer = 0, tabla As New DataTable(), activeProcessBar As Boolean
    Private Sub Graficas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor
        GroupBox5.Visible = True
        GroupBox5.BringToFront()
        ProgressBar1.Maximum = 600 * 600
        cargaChartPlanCut()
        LlenaGrid("Wips Planeados Sin CWO", "Todos")
        cargachart()
        ComboBox1.Text = "Wips Planeados Sin CWO"
        RadioButton1.Checked = True
        If countProcess < ProgressBar1.Maximum Then
            countProcess = ProgressBar1.Maximum
        ElseIf countProcess > ProgressBar1.Maximum Then
            ProgressBar1.Maximum = countProcess
            countProcess = ProgressBar1.Maximum
        End If
        GroupBox5.Visible = False
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub LlenaGrid(filtroPrincipal As String, dept As String)
        Try
            Dim consulta As String = "SELECT DISTINCT w.WIP,w.wSort,w.AU,w.Rev,w.Qty,w.IT,(Select SUM(WireBalance * 1) from tblWipDet x where x.WIP=w.WIP) as Ckts,w.KindOfAU,w.Balance,null as [Unit Price],null as [Total Price],w.Sem,CONVERT(date,w.ProcFDispMat) [Fecha Materiales],w.Pr,CONVERT(INT,[100RT] / 60)  + CONVERT(INT,[100SU] / 60) [100Tl] FROM tblWIP w INNER JOIN tblWipDet d ON w.WIP=d.WIP INNER JOIN tblTiemposEstIngDiv ti ON ti.AU=w.AU ", where As String = ""
            If filtroPrincipal = "Wips Planeados Sin CWO" And dept = "Todos" Then
                where = " WHERE Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN'"
            ElseIf filtroPrincipal = "Wips Planeados Sin CWO" And dept = "Produccion" Then
                where = " WHERE Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN' AND w.KindOfAU NOT LIKE 'XP%'"
            ElseIf filtroPrincipal = "Wips Planeados Sin CWO" And dept = "Zona 0" Then
                where = " WHERE Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN' AND w.KindOfAU LIKE 'XP%'"
            ElseIf filtroPrincipal = "CWO Nuevos" And dept = "Todos" Then
                where = "  inner join tblCWO c on c.CWO=d.CWO where (W.wSort IN (3,27)) and (C.WSort IN (3,27)) and c.FechaSolicitudMat is null"
            ElseIf filtroPrincipal = "CWO Nuevos" And dept = "Produccion" Then
                where = "  inner join tblCWO c on c.CWO=d.CWO where (W.wSort IN (3,27)) and (C.WSort IN (3,27)) and c.FechaSolicitudMat is null and w.KindOfAU not like 'XP%'"
            ElseIf filtroPrincipal = "CWO Nuevos" And dept = "Zona 0" Then
                where = "  inner join tblCWO c on c.CWO=d.CWO where (W.wSort IN (3,27)) and (C.WSort IN (3,27)) and c.FechaSolicitudMat is null and w.KindOfAU like 'XP%'"
            ElseIf filtroPrincipal = "CWO Solicitados" And dept = "Todos" Then
                where = "  inner join tblCWO c on c.CWO=d.CWO where (w.wSort in (3,27,12,14,25)) and (c.Wsort in (9,11,13,14,12,27)) and c.FechaSolicitudMat is not null"
            ElseIf filtroPrincipal = "CWO Solicitados" And dept = "Produccion" Then
                where = "  inner join tblCWO c on c.CWO=d.CWO where (w.wSort in (3,27,12,14,25)) and (c.Wsort in (9,11,13,14,12,27)) and c.FechaSolicitudMat is not null and w.KindOfAU not like 'XP%'"
            ElseIf filtroPrincipal = "CWO Solicitados" And dept = "Zona 0" Then
                where = "  inner join tblCWO c on c.CWO=d.CWO where (w.wSort in (3,27,12,14,25)) and (c.Wsort in (9,11,13,14,12,27)) and c.FechaSolicitudMat is not null and w.KindOfAU like 'XP%'"
            ElseIf filtroPrincipal = "CWO En Corte" And dept = "Todos" Then
                where = " inner join tblCWO c on c.CWO=d.CWO where (w.WSort in (3,20,25,29)) and c.wsort = 25"
            ElseIf filtroPrincipal = "CWO En Corte" And dept = "Produccion" Then
                where = " inner join tblCWO c on c.CWO=d.CWO where (w.WSort in (3,20,25,29)) and c.wsort = 25 and w.KindOfAU not like 'XP%'"
            ElseIf filtroPrincipal = "CWO En Corte" And dept = "Zona 0" Then
                where = "  inner join tblCWO c on c.CWO=d.CWO where (w.WSort in (3,20,25,29)) and c.wsort = 25 and w.KindOfAU like 'XP%'"
            End If
            consulta += where
            tabla.Clear()
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            cnn.Close()
            If tabla.Rows.Count > 0 Then
                If activeProcessBar = True Then
                    ProgressBar1.Maximum = tabla.Rows.Count()
                    countProcess = 0
                    GroupBox5.Visible = True
                End If
                For i As Integer = 0 To tabla.Rows.Count - 1
                    tabla.Columns(9).ReadOnly = False
                    tabla.Rows(i).Item(9) = ReturnUnitAndTotalPrice(CInt(Val(tabla.Rows(i).Item("Qty").ToString)), CInt(Val(tabla.Rows(i).Item("AU").ToString)), tabla.Rows(i).Item("Rev").ToString)
                    tabla.Columns(10).ReadOnly = False
                    tabla.Rows(i).Item(10) = CInt(Val(tabla.Rows(i).Item("Qty").ToString)) * CInt(Val(tabla.Rows(i).Item("Unit Price").ToString))
                    If GroupBox5.Visible = True Then
                        ProgressBar1.Value = countProcess
                        Application.DoEvents()
                        countProcess += 1
                    End If
                Next
                With RadGridView1
                    .DataSource = tabla
                    lblItemsRows.Text = "Records: " + RadGridView1.Rows.Count.ToString
                    GroupBox4.Visible = True
                End With
                If activeProcessBar = True Then
                    activeProcessBar = False
                    countProcess = 0
                    GroupBox5.Visible = False
                End If
            Else
                GroupBox4.Visible = False
                RadGridView1.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            cnn.Close()
        End Try
    End Sub
    Function ReturnUnitAndTotalPrice(Qty As Integer, AU As Integer, Rev As String) As Integer
        Try
            Dim valor As Integer = 0
            Dim QueryPrices As String = $"select Price from tblMasterPrice where AU={AU} and Rev='{Rev}' and {Qty} < [to] and {Qty} > [From] group by Price"
            cmd = New SqlCommand(QueryPrices, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            valor = If(CInt(cmd.ExecuteScalar) = 0, 0, CInt(cmd.ExecuteScalar))
            cnn.Close()
            Return valor
        Catch ex As Exception
            MsgBox(ex.ToString)
            cnn.Close()
            Return 0
        End Try
    End Function
    Private Sub cargaChartPlanCut()
        'Aqui como usare timer para refrescar las graficas, se debe limpiar los puntos, si no se duplican
        Me.Chart1.Series("Carga en proceso de confirmacion").Points.Clear()
        Me.Chart1.Series("Carga Maquinas por entrar").Points.Clear()
        Me.Chart1.Series("Carga Actual").Points.Clear()
        Me.Chart1.Series("WIP's").Points.Clear()

        Dim consulta As String = "select distinct w.WIP,convert(int,[100RT] / 60)  + convert(int,[100SU] / 60) [100Tl] from tblWIP w inner join tblWipDet d on w.WIP=d.WIP inner join tblTiemposEstIngDiv ti on ti.AU=w.AU", where As String = ""

        Dim sumTotalMaqXentrar As Integer = 0, sumTotalMaqProcess As Integer = 0, sumTotalConfirmacion As Integer = 0, sumTotalWIPS As Integer = 0
        ' -------------------------------------------------------------------
        ' WIPS sin crear CWO y con semana de corte
        If RadioButton1.Checked = True Then
            where = " where Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN'"
        ElseIf RadioButton2.Checked = True Then
            where = " where Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN' AND w.KindOfAU NOT LIKE 'XP%'"
        ElseIf RadioButton3.Checked = True Then
            where = " where Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN' AND w.KindOfAU LIKE 'XP%'"
        End If
        consulta += where
        Using tabla As New DataTable
            Try
                Dim cmdo As SqlCommand = New SqlCommand(consulta, conexOne)
                cmdo.CommandType = CommandType.Text
                Dim data As SqlDataReader
                conexOne.Open()
                data = cmdo.ExecuteReader
                If data.HasRows Then
                    While data.Read()
                        sumTotalWIPS += data.GetValue(1)
                        If GroupBox5.Visible = True And activeProcessBar = False Then
                            ProgressBar1.Value = countProcess
                            Application.DoEvents()
                            countProcess += 1
                        End If
                    End While
                Else
                    conexOne.Close()
                End If
                conexOne.Close()
            Catch ex As Exception
                conexOne.Close()
                MsgBox(ex.ToString)
                EnviaCorreoFalla("cargaChartPlanCut", host, UserName)
            End Try
        End Using

        ' Carga de maquinas por entrar
        consulta = "SELECT isnull(convert(int, (SUM(Tsetup) /60 + sum(TRuntime)/60)),0) AS 'TotalTime' 
                    FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
                    WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort =20 GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
        Using tabla As New DataTable
            Try
                Dim cmdo As SqlCommand = New SqlCommand(consulta, conexOne)
                cmdo.CommandType = CommandType.Text
                Dim data As SqlDataReader
                conexOne.Open()
                data = cmdo.ExecuteReader
                If data.HasRows Then
                    While data.Read()
                        sumTotalMaqXentrar += data.GetValue(0)
                        If GroupBox5.Visible And activeProcessBar = False Then
                            ProgressBar1.Value = countProcess
                            Application.DoEvents()
                            countProcess += 1
                        End If
                    End While
                Else
                    conexOne.Close()
                End If
                conexOne.Close()
            Catch ex As Exception
                conexOne.Close()
                MsgBox(ex.ToString)
                EnviaCorreoFalla("cargaChartPlanCut", host, UserName)
            End Try
        End Using
        'Carga de maquinas en proceso de corte
        consulta = "SELECT ISNULL(convert(int, (SUM(c.TSetup) / 60 + sum(c.TRuntime) / 60)),0) [TotalTime]
        FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO INNER JOIN tblWipDet as C ON C.WireID = D.WireID INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
        WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (25,29,26) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
        Using tabla2 As New DataTable
            Try
                Dim cmdo1 As SqlCommand = New SqlCommand(consulta, conexOne)
                cmdo1.CommandType = CommandType.Text
                Dim data1 As SqlDataReader
                conexOne.Open()
                data1 = cmdo1.ExecuteReader
                If data1.HasRows Then
                    While data1.Read()
                        sumTotalMaqProcess += data1.GetValue(0)
                        If GroupBox5.Visible And activeProcessBar = False Then
                            ProgressBar1.Value = countProcess
                            Application.DoEvents()
                            countProcess += 1
                        End If
                    End While
                Else
                    conexOne.Close()
                End If
                conexOne.Close()
            Catch ex As Exception
                conexOne.Close()
                MsgBox(ex.ToString)
                EnviaCorreoFalla("cargaChartPlanCut", host, UserName)
            End Try
        End Using
        ' Carga de maquinas en proceso de confirmacion
        consulta = "SELECT MR.Maq, isnull(convert(int, (SUM(Tsetup) /60 + sum(TRuntime)/60)),0) AS 'TotalTime' 
                    FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
                    WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (3,9,11,12,13,14,27) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
        Using tabla As New DataTable
            Try
                Dim cmdo2 As SqlCommand = New SqlCommand(consulta, conexOne)
                cmdo2.CommandType = CommandType.Text
                Dim data3 As SqlDataReader
                conexOne.Open()
                data3 = cmdo2.ExecuteReader
                If data3.HasRows Then
                    While data3.Read()
                        sumTotalConfirmacion += data3.GetValue(1)
                        If GroupBox5.Visible And activeProcessBar = False Then
                            ProgressBar1.Value = countProcess
                            Application.DoEvents()
                            countProcess += 1
                        End If
                    End While
                Else
                    conexOne.Close()
                End If
                conexOne.Close()
            Catch ex As Exception
                MsgBox(ex.ToString)
                conexOne.Close()
                EnviaCorreoFalla("cargaChartPlanCut", host, UserName)
            End Try
        End Using
        Try
            '-------------------------------------------------------------------------
            Me.Chart1.ChartAreas(0).AxisX.MajorGrid.LineWidth = 0 'Este es para que se vean lineas eje X
            Me.Chart1.Series("WIP's").IsValueShownAsLabel = True
            Me.Chart1.Series("WIP's").IsVisibleInLegend = True
            Me.Chart1.Series("Carga Maquinas por entrar").IsValueShownAsLabel = True
            Me.Chart1.Series("Carga Actual").IsValueShownAsLabel = True
            Me.Chart1.Series("Carga en proceso de confirmacion").IsValueShownAsLabel = True
            'Me.Chart1.ChartAreas(0).AxisY.MajorGrid.LineWidth = 0 'este es para quitar lineas eje Y
            Me.Chart1.Titles.Clear()
            Me.Chart1.Titles.Add("Grafico Planeacion Corte").Font = New Font("Arial", 12, FontStyle.Bold)
            '--------------------------------------------------------------------------
            Me.Chart1.Series("Carga Maquinas por entrar").Points.AddXY("", sumTotalMaqXentrar)
            '-------------------------------------------------------------------------
            Me.Chart1.Series("Carga Actual").Points.AddXY("", sumTotalMaqProcess)
            ' -------------------------------------------------------------------
            Me.Chart1.Series("Carga en proceso de confirmacion").Points.AddXY("Valores", sumTotalConfirmacion)
            '--------------------------------------------------------------------------
            Me.Chart1.Series("WIP's").Points.AddXY("", sumTotalWIPS)
            'Me.Chart1.Series("WIP's").Points(0).Color = Color.Purple
            ' ------------------------------------------------------------------
            If GroupBox5.Visible And Not activeProcessBar Then
                ProgressBar1.Value = countProcess
                Application.DoEvents()
                countProcess += 1
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cargachart()
        ColaGrafica = True
        Me.Chart2.Series("Carga en proceso de confirmacion").Points.Clear()
        Me.Chart2.Series("Carga Maquinas por entrar").Points.Clear()
        Me.Chart2.Series("Carga Actual").Points.Clear()
        Dim consulta As String
        Dim getMaqActives As String = "select Maq,0 [Proceso de confirmacion],0 [Listos para entrar], 0 [En corte] from tblMaqRates where Active = 1 order by CONVERT(int,Maq) asc"
        Dim getTableMaqs As DataTable = New DataTable(), dr1 As SqlDataReader
        Dim cm As New SqlCommand(getMaqActives, conex)
        cm.CommandType = CommandType.Text
        conex.Open()
        dr1 = cm.ExecuteReader
        getTableMaqs.Load(dr1)
        conex.Close()
        getTableMaqs.Columns(1).ReadOnly = False
        getTableMaqs.Columns(2).ReadOnly = False
        getTableMaqs.Columns(3).ReadOnly = False
        ' Carga de maquinas por entrar
        consulta = "SELECT MR.Maq, IsNull(convert(int, (SUM(Tsetup) /60 + sum(TRuntime)/60)),0) AS 'TotalTime' 
FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq /*INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO*/ INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 /*AND D.NumericalPath LIKE '2%'*/ and E.WSort =20 GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
        Using tabla As New DataTable
            Try
                Dim cmdo As SqlCommand = New SqlCommand(consulta, conexOne)
                cmdo.CommandType = CommandType.Text
                Dim data As SqlDataReader
                conexOne.Open()
                data = cmdo.ExecuteReader
                If data.HasRows Then
                    While data.Read()
                        For i = 0 To getTableMaqs.Rows.Count - 1
                            If data.GetValue(0) = getTableMaqs.Rows(i).Item("Maq").ToString Then
                                getTableMaqs.Rows(i).Item(2) = data.GetValue(1)
                                Exit For
                            End If
                        Next
                        If GroupBox5.Visible = True And activeProcessBar = False Then
                            ProgressBar1.Value = countProcess
                            Application.DoEvents()
                            countProcess += 1
                        End If
                    End While
                Else
                    conexOne.Close()
                End If
                conexOne.Close()
            Catch ex As Exception
                conexOne.Close()
                MsgBox(ex.ToString)
                EnviaCorreoFalla("cargachart", host, UserName)
                cargachart()
            End Try
        End Using
        'Carga de maquinas en proceso de corte
        consulta = "SELECT MR.Maq,ISNULL(convert(int, (SUM(c.TSetup) / 60 + sum(c.TRuntime) / 60)),0) [TotalTime]
        FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO INNER JOIN tblWipDet as C ON C.WireID = D.WireID INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
        WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (25,29,26) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
        Using tabla2 As New DataTable
            Try
                Dim cmdo1 As SqlCommand = New SqlCommand(consulta, conexOne)
                cmdo1.CommandType = CommandType.Text
                Dim data1 As SqlDataReader
                conexOne.Open()
                data1 = cmdo1.ExecuteReader
                If data1.HasRows Then
                    While data1.Read()
                        For i = 0 To getTableMaqs.Rows.Count - 1
                            If data1.GetValue(0) = getTableMaqs.Rows(i).Item("Maq").ToString Then
                                getTableMaqs.Rows(i).Item(3) = data1.GetValue(1)
                                Exit For
                            End If
                        Next
                        If GroupBox5.Visible = True And activeProcessBar = False Then
                            ProgressBar1.Value = countProcess
                            Application.DoEvents()
                            countProcess += 1
                        End If
                    End While
                Else
                    conexOne.Close()
                End If
                conexOne.Close()
            Catch ex As Exception
                MsgBox(ex.ToString)
                conexOne.Close()
                EnviaCorreoFalla("cargachart", host, UserName)
                cargachart()
            End Try
        End Using
        ' Carga de maquinas en proceso de confirmacion
        consulta = "SELECT MR.Maq, isnull(convert(int, (SUM(Tsetup) /60 + sum(TRuntime)/60)),0) AS 'TotalTime' 
                    FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP
                    WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (3,9,11,12,13,14,27) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
        Using tabla As New DataTable
            Try
                Dim cmdo2 As SqlCommand = New SqlCommand(consulta, conexOne)
                cmdo2.CommandType = CommandType.Text
                Dim data3 As SqlDataReader
                conexOne.Open()
                data3 = cmdo2.ExecuteReader
                If data3.HasRows Then
                    While data3.Read()
                        For i = 0 To getTableMaqs.Rows.Count - 1
                            If data3.GetValue(0) = getTableMaqs.Rows(i).Item("Maq").ToString Then
                                getTableMaqs.Rows(i).Item(1) = data3.GetValue(1)
                                Exit For
                            End If
                        Next
                        If GroupBox5.Visible And activeProcessBar = False Then
                            ProgressBar1.Value = countProcess
                            Application.DoEvents()
                            countProcess += 1
                        End If
                    End While
                Else
                    conexOne.Close()
                End If
                conexOne.Close()
            Catch ex As Exception
                MsgBox(ex.ToString)
                conexOne.Close()
                EnviaCorreoFalla("cargachart", host, UserName)
                cargachart()
            End Try
        End Using
        '-------------------------------------------------------------------------
        With Me.Chart2
            .ChartAreas(0).AxisX.MajorGrid.LineWidth = 0
            .Series("Carga Maquinas por entrar").IsValueShownAsLabel = True
            .Series("Carga Maquinas por entrar").IsVisibleInLegend = True
            .Series("Carga Actual").IsValueShownAsLabel = True
            .Series("Carga en proceso de confirmacion").IsValueShownAsLabel = True
            .Titles.Clear()
            .Titles.Add("Grafico Carga de Maquinas").Font = New Font("Arial", 12, FontStyle.Bold)
            If getTableMaqs.Rows.Count > 0 Then
                For j = 0 To getTableMaqs.Rows.Count - 1
                    .Series("Carga Maquinas por entrar").Points.AddXY(getTableMaqs.Rows(j).Item("Maq").ToString, getTableMaqs.Rows(j).Item("Listos para entrar").ToString)
                    .Series("Carga Actual").Points.AddXY(getTableMaqs.Rows(j).Item("Maq").ToString, getTableMaqs.Rows(j).Item("En corte").ToString)
                    .Series("Carga en proceso de confirmacion").Points.AddXY(getTableMaqs.Rows(j).Item("Maq").ToString, getTableMaqs.Rows(j).Item("Proceso de confirmacion").ToString)
                Next
            End If
        End With
        ' ------------------------------------------------------------------
        Timer1.Enabled = True
        Timer1.Interval = 60000
        If GroupBox5.Visible And activeProcessBar = False Then
            ProgressBar1.Value = countProcess
            Application.DoEvents()
            countProcess += 1
        End If
        ColaGrafica = false
    End Sub
    Sub SumTotales(opcionDeSum As String)
        Dim tablaRev As New DataTable()
        Try
            Dim sumTotalValores As Integer = 0, arreglo As String = ""
            If opcionDeSum = "Hrs" Then
                arreglo = "select SUM(convert(int,[100RT] / 60)  + convert(int,[100SU] / 60)) [100Tl] from tblWIP w inner join tblWipDet d on w.WIP=d.WIP inner join tblTiemposEstIngDiv ti on ti.AU=w.AU where Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN'"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                sumTotalValores = CInt(cmd.ExecuteScalar)
                cnn.Close()
                lblWips.Text = sumTotalValores.ToString
                sumTotalValores = 0

                arreglo = "SELECT ISNULL(convert(int, (SUM(c.TSetup) / 60 + sum(c.TRuntime) / 60)),0) [TotalTime] FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO INNER JOIN tblWipDet as C ON C.WireID = D.WireID INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (3,27) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tablaRev.Load(dr)
                cnn.Close()
                lblCWOnew.Text = (From row In tablaRev.AsEnumerable() Select row("TotalTime")).ToList().Sum(Function(i) CInt(i.ToString()))
                tablaRev.Clear()

                arreglo = "SELECT convert(int, (SUM(Tsetup) /60 + sum(TRuntime)/60)) AS 'TotalTime' FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (20,9,11,12,13,14,27) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tablaRev.Load(dr)
                cnn.Close()
                lblCWOsol.Text = (From row In tablaRev.AsEnumerable() Select row("TotalTime")).ToList().Sum(Function(i) CInt(i.ToString()))
                tablaRev.Clear()

                arreglo = "SELECT ISNULL(convert(int, (SUM(c.TSetup) / 60 + sum(c.TRuntime) / 60)),0) [TotalTime] FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO INNER JOIN tblWipDet as C ON C.WireID = D.WireID INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (25,29,26) GROUP BY MR.Maq, Cat,Categoria, MaxAWG, MinAWG ORDER BY MR.Maq * 1"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tablaRev.Load(dr)
                cnn.Close()
                blCWOcut.Text = (From row In tablaRev.AsEnumerable() Select row("TotalTime")).ToList().Sum(Function(i) CInt(i.ToString()))
            ElseIf opcionDeSum = "Total Ckts" Then
                arreglo = "Select ISNULL(SUM(WireBalance * 1),0) from tblWipDet where WIP in (select distinct w.WIP from tblWIP w inner join tblWipDet d on w.WIP=d.WIP inner join tblTiemposEstIngDiv ti on ti.AU=w.AU where Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN')"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                sumTotalValores = CInt(cmd.ExecuteScalar)
                cnn.Close()
                lblWips.Text = sumTotalValores.ToString
                sumTotalValores = 0

                tablaRev.Clear()
                arreglo = "Select ISNULL(SUM(WireBalance * 1),0) from tblWipDet where WIP in (SELECT distinct  wp.WIP FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO INNER JOIN tblWipDet as C ON C.WireID = D.WireID INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (3,27))"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                sumTotalValores = CInt(cmd.ExecuteScalar)
                cnn.Close()
                lblCWOnew.Text = sumTotalValores.ToString
                sumTotalValores = 0

                arreglo = "Select ISNULL(SUM(WireBalance * 1),0) from tblWipDet where WIP in (SELECT DISTINCT WP.WIP FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (20,9,11,12,13,14,27))"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                sumTotalValores = CInt(cmd.ExecuteScalar)
                cnn.Close()
                lblCWOsol.Text = sumTotalValores.ToString
                sumTotalValores = 0

                arreglo = "Select ISNULL(SUM(WireBalance * 1),0) from tblCWOSerialNumbers where WIP in (SELECT DISTINCT WP.WIP FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (25,26,29))"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                sumTotalValores = CInt(cmd.ExecuteScalar)
                cnn.Close()
                blCWOcut.Text = sumTotalValores.ToString
                sumTotalValores = 0
            ElseIf opcionDeSum = "$ Valor" Then
                tablaRev.Clear()
                arreglo = "select distinct w.AU,w.Rev,w.Qty from tblWIP w inner join tblWipDet d on w.WIP=d.WIP inner join tblTiemposEstIngDiv ti on ti.AU=w.AU where Sem <= DATEPART(ISO_WEEK,GETDATE()) + 2 and DATEPART(year,CONVERT(date,DayOfSem)) <= DATEPART(year,CONVERT(date,GETDATE())) AND CWO='0' AND ti.Rev=w.Rev AND Status='OPEN'"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tablaRev.Load(dr)
                cnn.Close()
                If tablaRev.Rows.Count > 0 Then
                    For i As Integer = 0 To tablaRev.Rows.Count - 1
                        sumTotalValores += CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)) * ReturnUnitAndTotalPrice(CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)), CInt(Val(tablaRev.Rows(i).Item("AU").ToString)), tablaRev.Rows(i).Item("Rev").ToString)
                    Next
                End If
                lblWips.Text = sumTotalValores.ToString
                sumTotalValores = 0
                tablaRev.Clear()
                arreglo = "select distinct wp.AU,wp.Rev,wp.Qty FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblCWOSerialNumbers AS D ON E.CWO = D.CWO INNER JOIN tblWipDet as C ON C.WireID = D.WireID INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (3,27)"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tablaRev.Load(dr)
                cnn.Close()
                If tablaRev.Rows.Count > 0 Then
                    For i As Integer = 0 To tablaRev.Rows.Count - 1
                        sumTotalValores += CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)) * ReturnUnitAndTotalPrice(CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)), CInt(Val(tablaRev.Rows(i).Item("AU").ToString)), tablaRev.Rows(i).Item("Rev").ToString)
                    Next
                End If
                lblCWOnew.Text = sumTotalValores.ToString
                sumTotalValores = 0
                tablaRev.Clear()
                arreglo = "select distinct wp.AU,wp.Rev,wp.Qty FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (20,9,11,12,13,14,27)"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tablaRev.Load(dr)
                cnn.Close()
                If tablaRev.Rows.Count > 0 Then
                    For i As Integer = 0 To tablaRev.Rows.Count - 1
                        sumTotalValores += CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)) * ReturnUnitAndTotalPrice(CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)), CInt(Val(tablaRev.Rows(i).Item("AU").ToString)), tablaRev.Rows(i).Item("Rev").ToString)
                    Next
                End If
                lblCWOsol.Text = sumTotalValores.ToString
                sumTotalValores = 0
                tablaRev.Clear()
                arreglo = "select distinct wp.AU,wp.Rev,wp.Qty FROM tblMaqRates AS MR INNER JOIN tblCWO AS E ON MR.Maq = E.Maq INNER JOIN tblWipDet as C ON C.CWO = E.CWO INNER JOIN tblWIP AS WP ON C.WIP = WP.WIP WHERE E.Maq = MR.Maq AND E.CloseDate IS NULL AND WP.Status = 'Open' AND C.WireBalance > 0 AND MR.Active = 1 and E.WSort in (25,26,29)"
                cmd = New SqlCommand(arreglo, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tablaRev.Load(dr)
                cnn.Close()
                If tablaRev.Rows.Count > 0 Then
                    For i As Integer = 0 To tablaRev.Rows.Count - 1
                        sumTotalValores += CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)) * ReturnUnitAndTotalPrice(CInt(Val(tablaRev.Rows(i).Item("Qty").ToString)), CInt(Val(tablaRev.Rows(i).Item("AU").ToString)), tablaRev.Rows(i).Item("Rev").ToString)
                    Next
                End If
                blCWOcut.Text = sumTotalValores.ToString
                sumTotalValores = 0
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor.Current = Cursors.WaitCursor
        activeProcessBar = True
        If RadioButton1.Checked Then LlenaGrid(ComboBox1.Text, RadioButton1.Text.ToString)
        If RadioButton2.Checked Then LlenaGrid(ComboBox1.Text, RadioButton2.Text.ToString)
        If RadioButton3.Checked Then LlenaGrid(ComboBox1.Text, RadioButton3.Text.ToString)
        cargaChartPlanCut()
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If RadioButton5.Checked Then
            SumTotales(RadioButton5.Text.ToString)
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If RadioButton4.Checked = True Then
            SumTotales(RadioButton4.Text.ToString)
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        cargaChartPlanCut()
        cargachart()
    End Sub
    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
    End Sub
    Private Sub Graficas_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Principal.Show()
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        ColaGrafica = False
        Principal.Show()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        Cursor.Current = Cursors.WaitCursor
        If RadioButton6.Checked Then
            SumTotales(RadioButton6.Text.ToString)
        End If
        Cursor.Current = Cursors.Default
    End Sub
End Class