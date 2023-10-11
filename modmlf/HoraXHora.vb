Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.IO
Imports System.ComponentModel
Imports System.Windows.Forms
Public Class HoraXHora
    Dim tabla As New DataTable, TurnOfQuery As Integer = 1, tAcumulado As Integer = 0, CountCWO As Integer = 0, sumMinHrs As Long
    Dim HoraSet As Integer = 0, FlagLlenaGrids As Integer, FlagAux As Integer, PNSearch As String, SubQueryGlobalCWO As String, SubQueryMaterialesGlobal As String
    Public Event CellPainting As DataGridViewCellPaintingEventHandler ' Evento para desplegar los split de los encabezados de la Grid DataGridView1 
    Public SubQuery1Maq0 As String, SubQuery2Maq0 As String
    Public SubQuery1Maq1 As String, SubQuery2Maq1 As String
    Public SubQuery1Maq2 As String, SubQuery2Maq2 As String
    Public SubQuery1Maq3 As String, SubQuery2Maq3 As String
    Public SubQuery1Maq4 As String, SubQuery2Maq4 As String
    Public SubQuery1Maq5 As String, SubQuery2Maq5 As String
    Public SubQuery1Maq6 As String, SubQuery2Maq6 As String
    Public SubQuery1Maq10 As String, SubQuery2Maq10 As String
    Public SubQuery1Maq11 As String, SubQuery2Maq11 As String
    Public SubQuery1Maq12 As String, SubQuery2Maq12 As String
    Public SubQuery1Maq13 As String, SubQuery2Maq13 As String
    Public SubQuery1Maq14 As String, SubQuery2Maq14 As String
    Private Sub HoraXHora_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Time() ' Metodo del timer para refrescar el label del reloj
        StartProcessHidden() ' Metodo donde ocultamos todo al momento de iniciar la forma
        creandoGrid() ' Creamos las columnas de la Grid de maquinas
        CreandoDatosDgvMaquinas() 'Aqui llenamos la Grid de maquinas con los CWO activos
        LlenaGridPnInCWO() ' Metodo para llenar las grid del GroupBox1 donde estan las Grid de cada maquina, se llenan con las cantidades y numeros de parte
        llenaGridMatInProd() ' Metodo para llenar los materiales en Produccion
        LlenaGridSimilitud() ' Metodo para llenar lo alojado en el Metodo LlenaGridMatInProd() pero agrupado por qty y pn
        DataSourceInGrids() ' Metodo para llenado de todas las Grid, sin rellenado linea por linea
    End Sub
    Private Sub StartProcessHidden()
        Button1.Visible = False
        lblHrsConsultadas.Visible = False
        txtBuscador.Enabled = False
        txtBuscador.BackColor = Color.White
        TabPage1.Parent = Nothing
        TabPage1.Visible = False
    End Sub
    Public Sub creandoGrid()
        'Try
        '    Dim query As String = "select Maq from tblMaqRates where Active=1 order by CONVERT(int,Maq) asc", tabla As New DataTable
        '    cmd = New SqlCommand(query, cnn)
        '    cmd.CommandType = CommandType.Text
        '    cnn.Open()
        '    dr = cmd.ExecuteReader
        '    'tabla.Load(dr)
        '    If dr.HasRows Then
        '        While dr.Read
        '            Me.DataGridView1.Columns.Add(CStr(dr.GetValue(0)), "CWO")
        '            Me.DataGridView1.Columns.Add(CStr(dr.GetValue(0)), "Orden")
        '        End While
        '    End If
        '    cnn.Close()
        'Catch ex As Exception
        '    cnn.Close()
        'End Try
        Me.DataGridView1.Columns.Add("0", "CWO")
        Me.DataGridView1.Columns.Add("0", "Orden")
        Me.DataGridView1.Columns.Add("1", "CWO")
        Me.DataGridView1.Columns.Add("1", "Orden")
        Me.DataGridView1.Columns.Add("2", "CWO")
        Me.DataGridView1.Columns.Add("2", "Orden")
        Me.DataGridView1.Columns.Add("3", "CWO")
        Me.DataGridView1.Columns.Add("3", "Orden")
        Me.DataGridView1.Columns.Add("4", "CWO")
        Me.DataGridView1.Columns.Add("4", "Orden")
        Me.DataGridView1.Columns.Add("5", "CWO")
        Me.DataGridView1.Columns.Add("5", "Orden")
        Me.DataGridView1.Columns.Add("6", "CWO")
        Me.DataGridView1.Columns.Add("6", "Orden")
        Me.DataGridView1.Columns.Add("10", "CWO")
        Me.DataGridView1.Columns.Add("10", "Orden")
        Me.DataGridView1.Columns.Add("11", "CWO")
        Me.DataGridView1.Columns.Add("11", "Orden")
        Me.DataGridView1.Columns.Add("12", "CWO")
        Me.DataGridView1.Columns.Add("12", "Orden")
        Me.DataGridView1.Columns.Add("13", "CWO")
        Me.DataGridView1.Columns.Add("13", "Orden")
        Me.DataGridView1.Columns.Add("14", "CWO")
        Me.DataGridView1.Columns.Add("14", "Orden")
        For j As Integer = 0 To Me.DataGridView1.ColumnCount - 1
            Me.DataGridView1.Columns(j).Width = 80
            Me.DataGridView1.Columns(j).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        Me.DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.DataGridView1.ColumnHeadersHeight = Me.DataGridView1.ColumnHeadersHeight * 2
        Me.DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
    End Sub
    Private Sub dataGridView1_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnWidthChanged
        Dim rtHeader As Rectangle = Me.DataGridView1.DisplayRectangle
        rtHeader.Height = Me.DataGridView1.ColumnHeadersHeight / 2
        Me.DataGridView1.Invalidate(rtHeader)
    End Sub
    Private Sub dataGridView1_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles DataGridView1.Scroll
        Dim rtHeader As Rectangle = Me.DataGridView1.DisplayRectangle
        rtHeader.Height = Me.DataGridView1.ColumnHeadersHeight / 2
        Me.DataGridView1.Invalidate(rtHeader)
    End Sub
    Private Sub dataGridView1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles DataGridView1.Paint
        If Me.DataGridView1.ColumnCount > 0 Then
            Dim Maq As String() = {"Maquina 0", "Maquina 1", "Maquina 2", "Maquina 3", "Maquina 4", "Maquina 5", "Maquina 6", "Maquina 10", "Maquina 11", "Maquina 12", "Maquina 13", "Maquina 14"}
            Dim j As Integer = 0
            While j < 24
                Dim r1 As Rectangle = Me.DataGridView1.GetCellDisplayRectangle(j, -1, True)
                Dim w2 As Integer = Me.DataGridView1.GetCellDisplayRectangle(j, -1, True).Width
                r1.X += 1
                r1.Y += 1
                r1.Width = r1.Width + w2 - 2
                r1.Height = r1.Height / 2 - 2
                e.Graphics.FillRectangle(New SolidBrush(Me.DataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1)
                Dim format As StringFormat = New StringFormat()
                format.Alignment = StringAlignment.Center
                format.LineAlignment = StringAlignment.Center
                e.Graphics.DrawString(Maq(j / 2), Me.DataGridView1.ColumnHeadersDefaultCellStyle.Font, New SolidBrush(Me.DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, format)
                j += 2
            End While
        End If
    End Sub
    Public Sub dataGridView1_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        If e.RowIndex = -1 AndAlso e.ColumnIndex > -1 Then
            Dim r2 As Rectangle = e.CellBounds
            r2.Y += e.CellBounds.Height / 2
            r2.Height = e.CellBounds.Height / 2
            e.PaintBackground(r2, True)
            e.PaintContent(r2)
            e.Handled = True
        End If
    End Sub
    Sub Time()
        Timer1.Stop()
        lblHra.Text = "Hora actual: " + DateTime.Now.ToString("hh:mm tt")
        Timer1.Enabled = True
        Timer1.Interval = 60000
    End Sub
    Private Sub DataSourceInGrids()
        Dim systemType As Type
        Dim propertyInfo As PropertyInfo
        systemType = DataGridView1.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(DataGridView1, True, Nothing)
        ' **************** '
        systemType = dgvWipCutCard.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvWipCutCard, True, Nothing)
        ' **************** '
        '-------------
        systemType = dgvPNAllocated1.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNAllocated1, True, Nothing)
        '-------------
        systemType = dgvPNAllocated2.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNAllocated2, True, Nothing)
        '-------------
        systemType = dgvPNAllocated10.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNAllocated10, True, Nothing)
        '-------------
        systemType = dgvPNAllocated13.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNAllocated13, True, Nothing)
        '-------------
        systemType = dgvPNAllocated14.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNAllocated14, True, Nothing)
        '-------------
        systemType = dgvPNAllocated0.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNAllocated0, True, Nothing)
        '-------------
        systemType = dgvPNMaq05.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNMaq05, True, Nothing)
        '-------------
        systemType = dgvPnInMaq12.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPnInMaq12, True, Nothing)
        '-------------
        ' *************** '
        systemType = dgvPNinProdd.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNinProdd, True, Nothing)
        ' ************** '
        systemType = dgvMatInProdAndSolicitados.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvMatInProdAndSolicitados, True, Nothing)
        ' ************** '
        systemType = dgvPNinAlmacen.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvPNinAlmacen, True, Nothing)
    End Sub
    Sub CreandoDatosDgvMaquinas()
        Try
            tabla.Clear()
            TurnOfQuery = 1
            Dim query As String = ""
            While TurnOfQuery <= 12
                If TurnOfQuery = 1 Then 'Maquina 0
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=0 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=0 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count = 0 Then
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(0).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(1).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            ElseIf DataGridView1.Rows.Count > 0 Then
                                Me.DataGridView1.Rows.Add()
                                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                    If DataGridView1.Rows(i).Cells(0).Value = Nothing Then
                                        DataGridView1.Rows(i).Cells(0).Value = tabla.Rows(o).Item("CWO").ToString
                                        DataGridView1.Rows(i).Cells(1).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                    End If
                                Next
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 2 Then 'Maquina 1
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=1 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=1 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(2).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(3).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(2).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(3).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(2).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(3).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 3 Then 'Maquina 2
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=2 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=2 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(4).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(5).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(4).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(5).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(4).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(5).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 4 Then 'Maquina 3
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=2 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=3 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(6).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(7).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(6).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(7).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(6).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(7).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 5 Then 'Maquina 4
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=2 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=4 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(8).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(9).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(8).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(9).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(8).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(9).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 6 Then 'Maquina 5
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=5 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=5 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(10).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(11).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(10).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(11).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(10).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(11).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 7 Then 'Maquina 6
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=2 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=6 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(12).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(13).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(12).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(13).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(12).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(13).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 8 Then 'Maquina 10
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=10 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=10 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(14).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(15).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(14).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(15).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(14).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(15).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 9 Then 'Maquina 11
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=2 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=6 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(16).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(17).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(16).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(17).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(16).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(17).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 10 Then 'Maquina 12
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=12 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(18).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(19).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(18).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(19).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(18).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(19).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 11 Then 'Maquina 13
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=13 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=13 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(20).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(21).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(20).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(21).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(20).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(21).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                ElseIf TurnOfQuery = 12 Then 'Maquina 14
                    'query = "select CWO,Convert(datetime,FechaSolicitudMat) [Fecha Corte] from tblCWO where WSort in (20,25,29) and FechaSolicitudMat is not null and Maq=14 order by CONVERT(datetime,FechaSolicitudMat) asc"
                    query = "select CWO,Id [Orden de Corte] from tblCWO where WSort in (12,14,20,25,29) and (Id is not null or Id > 0) and Maq=14 and Status = 'OPEN' order by Id asc"
                    CargaTable(query)
                    If tabla.Rows.Count > 0 Then
                        For o As Integer = 0 To tabla.Rows.Count - 1
                            If DataGridView1.Rows.Count > 0 Then
                                If o >= DataGridView1.Rows.Count Then
                                    Me.DataGridView1.Rows.Add()
                                    DataGridView1.Rows(o).Cells(22).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(23).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                Else
                                    DataGridView1.Rows(o).Cells(22).Value = tabla.Rows(o).Item("CWO").ToString
                                    DataGridView1.Rows(o).Cells(23).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                                End If
                            Else
                                Me.DataGridView1.Rows.Add()
                                DataGridView1.Rows(0).Cells(22).Value = tabla.Rows(o).Item("CWO").ToString
                                DataGridView1.Rows(0).Cells(23).Value = tabla.Rows(o).Item("Orden de Corte").ToString
                            End If
                        Next
                    End If
                End If
                TurnOfQuery += 1
            End While
            If TurnOfQuery > 8 Then
                With DataGridView1
                    lblQtyCWO.Text = "Qty CWO: " & CountCWO.ToString
                    CountCWO = 0
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    TurnOfQuery = 0
                    .ClearSelection()
                    PintaCWOConStartStop()
                    lblCWOconStart.Text = "Qty CWO Start: " & CountCWO.ToString
                    CountCWO = 0
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Function CargaTable(consulta As String)
        Try
            tabla.Clear()
            cnn.Open()
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            cnn.Close()
            CountCWO += CInt(tabla.Rows.Count)
            Return tabla
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function RevisaStartStop(CWO As String) ' Funcion para ver que CWO se encuentra con un Start, de ser asi devuelve 1 y pinta ese CWO en color verde, si retorna 0 no pinta ese CWO
        Try
            Dim valor As Integer = 0
            If CWO = "" Then
                valor = 0
            Else
                cmd = New SqlCommand("select WO from tblEmpleadosStartStop where WO='" + CWO.ToString + "' and StartTask is not null and Active = 1 and StopTask is null", cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                valor = If(CStr(cmd.ExecuteScalar) = "", 0, 1)
                cnn.Close()
            End If
            Return valor
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Sub RevisaBalanceTAGs(PN As String, QtyCheck As Integer, ii As Integer, aGrid As DataGridView)
        Try
            Dim aCheck As Integer = 0
            Dim check As String = $"select case" &
                                   $" when SUM(Balance) > {QtyCheck} then 1" &
                                   $" When SUM(Balance) < {QtyCheck} Then 2" &
                                   $" When SUM(Balance) = {QtyCheck} Then 3 End " &
                                   " from tblItemsTags " &
                                   $" where PN='{PN}' and Status='NoAvailable' group by PN"
            cmd = New SqlCommand(check, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            aCheck = CInt(cmd.ExecuteScalar)
            cnn.Close()
            If aCheck = 1 Or aCheck = 3 Then
                aGrid.Rows(ii).Cells("PN").Style.BackColor = Color.LightBlue
                aGrid.Rows(ii).Cells("Qty").Style.BackColor = Color.LightBlue
            ElseIf aCheck = 2 Then
                aGrid.Rows(ii).Cells("PN").Style.BackColor = Color.Gray
                aGrid.Rows(ii).Cells("Qty").Style.BackColor = Color.Gray
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub PintaCWOConStartStop()
        ' Metodo para recorrer la Grid DataGridView1 para pintar celdas con CWO activos (Start) en color verde,
        ' ademas de llenar variable bandera CountCWO para contar cuantos CWO se encuentran activos (Start)
        ' Funcion RevisawSortCWO(), checamos que no sea wsort 12 o 14, si es uno de esos wsort,
        ' lo pintamos amarillo el cwo para visualizar que es un corto
        Try
            Dim sort As Integer = 0
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells(0).Value <> Nothing Then 'Maquina 0
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(0).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(0).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(0).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(0).Style.BackColor = Color.LightYellow
                    End If
                End If
                If DataGridView1.Rows(i).Cells(2).Value <> Nothing Then 'Maquina 1
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(2).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(2).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(2).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(2).Style.BackColor = Color.LightYellow
                    End If
                End If
                If DataGridView1.Rows(i).Cells(4).Value <> Nothing Then ' Maquina 2
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(4).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(4).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(4).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(4).Style.BackColor = Color.LightYellow
                    End If
                End If
                    If DataGridView1.Rows(i).Cells(6).Value <> Nothing Then ' Maquina 5
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(6).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(6).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.LightYellow
                    End If
                End If
                    If DataGridView1.Rows(i).Cells(8).Value <> Nothing Then 'Maquina 10
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(8).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(8).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(8).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(8).Style.BackColor = Color.LightYellow
                    End If
                End If
                    If DataGridView1.Rows(i).Cells(10).Value <> Nothing Then 'Maquina 12
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(10).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(10).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(10).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(10).Style.BackColor = Color.LightYellow
                    End If
                End If
                    If DataGridView1.Rows(i).Cells(12).Value <> Nothing Then 'Maquina 13
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(12).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(12).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(12).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(12).Style.BackColor = Color.LightYellow
                    End If
                End If
                    If DataGridView1.Rows(i).Cells(14).Value <> Nothing Then 'Maquina 14
                    If RevisaStartStop(DataGridView1.Rows(i).Cells(14).Value.ToString) = 1 Then
                        DataGridView1.Rows(i).Cells(14).Style.BackColor = Color.LightSeaGreen
                        CountCWO += 1
                    End If
                    sort = RevisawSortCWO(DataGridView1.Rows(i).Cells(14).Value.ToString)
                    If sort = 12 Or sort = 14 Then
                        DataGridView1.Rows(i).Cells(14).Style.BackColor = Color.LightYellow
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Hrs(Minutos As Integer)
        Try
            Dim Maquinas As Integer = 0, Consulta As String = "", MinutosAcumulados As Integer = 0
            While Maquinas <= 11
                If Maquinas = 0 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 0 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 1 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 1 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 2 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 2 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 3 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 3 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 4 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 4 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 5 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 5 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 6 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 6 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 7 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 10 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 8 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 11 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 9 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 12 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 10 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 13 and det.Balance > 0 order by c.Id asc,IdSort asc"
                If Maquinas = 11 Then Consulta = "select det.CWO,WireBalance,Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado' from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq = 14 and det.Balance > 0 order by c.Id asc,IdSort asc"
                Dim auditaCWO As String = "", auditaWire As String = "", auditaTermA As String = "", auditaTermB As String = "", SubQuery1 As String = "", SubQuery2 As String = ""
                Dim tabla As New DataTable()
                tabla.Reset()
                tabla.Clear()
                tabla.Dispose()
                cmd = New SqlCommand(Consulta, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                tabla.Load(dr)
                cnn.Close()
                If tabla.Rows.Count > 0 Then
                    For i As Integer = 0 To tabla.Rows.Count - 1
                        tabla.Columns(6).ReadOnly = False
                        If tabla.Rows(i).Item("WireBalance").ToString > 0 Then
                            tabla.Rows(i).Item(6) = AcumuladodeTiempo(tabla.Rows(i).Item("Total Minutos").ToString)
                        Else
                            tabla.Rows(i).Item(6) = 0
                        End If
                    Next
                    tAcumulado = 0
                    For countMat As Integer = 0 To tabla.Rows.Count - 1
                        MinutosAcumulados = CInt(Val(tabla.Rows(countMat).Item("Acumulado").ToString))
                        If MinutosAcumulados > 0 Then
                            If Minutos >= MinutosAcumulados Then
                                If auditaCWO = "" Then
                                    auditaCWO = tabla.Rows(countMat).Item("CWO").ToString()
                                    SubQuery1 = "(" + "'" + auditaCWO + "',"
                                    SubQueryGlobalCWO = SubQueryGlobalCWO + "'" + auditaCWO + "',"
                                Else
                                    If Not auditaCWO = tabla.Rows(countMat).Item("CWO").ToString() Then
                                        auditaCWO = tabla.Rows(countMat).Item("CWO").ToString()
                                        SubQuery1 = SubQuery1 + "'" + auditaCWO + "',"
                                        SubQueryGlobalCWO = SubQueryGlobalCWO + "'" + auditaCWO + "',"
                                    End If
                                End If
                                If auditaWire = "" Then
                                    auditaWire = tabla.Rows(countMat).Item("Wire").ToString()
                                    SubQuery2 = "(" + "'" + auditaWire + "',"
                                    If auditaWire <> "" Then
                                        SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaWire + "',"
                                    End If
                                Else
                                    If Not auditaWire = tabla.Rows(countMat).Item("Wire").ToString() Then
                                        auditaWire = tabla.Rows(countMat).Item("Wire").ToString()
                                        SubQuery2 = SubQuery2 + "'" + auditaWire + "',"
                                        If auditaWire <> "" Then
                                            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaWire + "',"
                                        End If
                                    End If
                                End If
                                If auditaTermA = "" Then
                                    auditaTermA = tabla.Rows(countMat).Item("TermA").ToString()
                                    If SubQuery2.Length > 1 Then
                                        SubQuery2 = SubQuery2 + "'" + auditaTermA + "',"
                                        If auditaTermA <> "" Then
                                            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaTermA + "',"
                                        End If
                                    Else
                                        SubQuery2 = "(" + "'" + auditaTermA + "',"
                                        If auditaTermA <> "" Then
                                            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaTermA + "',"
                                        End If
                                    End If
                                Else
                                    If Not auditaTermA = tabla.Rows(countMat).Item("TermA").ToString() Then
                                        auditaTermA = tabla.Rows(countMat).Item("TermA").ToString()
                                        SubQuery2 = SubQuery2 + "'" + auditaTermA + "',"
                                        If auditaTermA <> "" Then
                                            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaTermA + "',"
                                        End If
                                    End If
                                End If
                                If auditaTermB = "" Then
                                    auditaTermB = tabla.Rows(countMat).Item("TermB").ToString()
                                    If SubQuery2.Length > 1 Then
                                        SubQuery2 = SubQuery2 + "'" + auditaTermB + "',"
                                        If auditaTermB <> "" Then
                                            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaTermB + "',"
                                        End If
                                    Else
                                        SubQuery2 = "(" + "'" + auditaTermB + "',"
                                        If auditaTermB <> "" Then
                                            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaTermB + "',"
                                        End If
                                    End If
                                Else
                                    If Not auditaTermB = tabla.Rows(countMat).Item("TermB").ToString() Then
                                        auditaTermB = tabla.Rows(countMat).Item("TermB").ToString()
                                        SubQuery2 = SubQuery2 + "'" + auditaTermB + "',"
                                        If auditaTermB <> "" Then
                                            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + "'" + auditaTermB + "',"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                    SubQuery1 = SubQuery1.TrimEnd(",")
                    SubQuery1 = SubQuery1 + ")"
                    SubQuery2 = SubQuery2.TrimEnd(",")
                    SubQuery2 = SubQuery2 + ")"
                    If Maquinas = 0 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq0 = "('')"
                        Else
                            SubQuery1Maq0 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq0 = "('')"
                        Else
                            SubQuery2Maq0 = SubQuery2
                        End If
                    ElseIf Maquinas = 1 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq1 = "('')"
                        Else
                            SubQuery1Maq1 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq1 = "('')"
                        Else
                            SubQuery2Maq1 = SubQuery2
                        End If
                    ElseIf Maquinas = 2 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq2 = "('')"
                        Else
                            SubQuery1Maq2 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq2 = "('')"
                        Else
                            SubQuery2Maq2 = SubQuery2
                        End If
                    ElseIf Maquinas = 3 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq3 = "('')"
                        Else
                            SubQuery1Maq3 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq3 = "('')"
                        Else
                            SubQuery2Maq3 = SubQuery2
                        End If
                    ElseIf Maquinas = 4 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq4 = "('')"
                        Else
                            SubQuery1Maq4 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq4 = "('')"
                        Else
                            SubQuery2Maq4 = SubQuery2
                        End If
                    ElseIf Maquinas = 5 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq5 = "('')"
                        Else
                            SubQuery1Maq5 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq5 = "('')"
                        Else
                            SubQuery2Maq5 = SubQuery2
                        End If
                    ElseIf Maquinas = 6 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq6 = "('')"
                        Else
                            SubQuery1Maq6 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq6 = "('')"
                        Else
                            SubQuery2Maq6 = SubQuery2
                        End If
                    ElseIf Maquinas = 7 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq10 = "('')"
                        Else
                            SubQuery1Maq10 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq10 = "('')"
                        Else
                            SubQuery2Maq10 = SubQuery2
                        End If
                    ElseIf Maquinas = 8 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq11 = "('')"
                        Else
                            SubQuery1Maq11 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq11 = "('')"
                        Else
                            SubQuery2Maq11 = SubQuery2
                        End If
                    ElseIf Maquinas = 9 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq12 = "('')"
                        Else
                            SubQuery1Maq12 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq12 = "('')"
                        Else
                            SubQuery2Maq12 = SubQuery2
                        End If
                    ElseIf Maquinas = 10 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq13 = "('')"
                        Else
                            SubQuery1Maq13 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq13 = "('')"
                        Else
                            SubQuery2Maq13 = SubQuery2
                        End If
                    ElseIf Maquinas = 11 Then
                        If SubQuery1 = ")" Then
                            SubQuery1Maq14 = "('')"
                        Else
                            SubQuery1Maq14 = SubQuery1
                        End If
                        If SubQuery2 = ")" Then
                            SubQuery2Maq14 = "('')"
                        Else
                            SubQuery2Maq14 = SubQuery2
                        End If
                    End If
                Else
                    If Maquinas = 0 Then
                        SubQuery1Maq0 = "('')"
                        SubQuery2Maq0 = "('')"
                    ElseIf Maquinas = 1 Then
                        SubQuery1Maq1 = "('')"
                        SubQuery2Maq1 = "('')"
                    ElseIf Maquinas = 2 Then
                        SubQuery1Maq2 = "('')"
                        SubQuery2Maq2 = "('')"
                    ElseIf Maquinas = 3 Then
                        SubQuery1Maq3 = "('')"
                        SubQuery2Maq3 = "('')"
                    ElseIf Maquinas = 4 Then
                        SubQuery1Maq4 = "('')"
                        SubQuery2Maq4 = "('')"
                    ElseIf Maquinas = 5 Then
                        SubQuery1Maq5 = "('')"
                        SubQuery2Maq5 = "('')"
                    ElseIf Maquinas = 6 Then
                        SubQuery1Maq6 = "('')"
                        SubQuery2Maq6 = "('')"
                    ElseIf Maquinas = 7 Then
                        SubQuery1Maq10 = "('')"
                        SubQuery2Maq10 = "('')"
                    ElseIf Maquinas = 8 Then
                        SubQuery1Maq11 = "('')"
                        SubQuery2Maq11 = "('')"
                    ElseIf Maquinas = 9 Then
                        SubQuery1Maq12 = "('')"
                        SubQuery2Maq12 = "('')"
                    ElseIf Maquinas = 10 Then
                        SubQuery1Maq13 = "('')"
                        SubQuery2Maq13 = "('')"
                    ElseIf Maquinas = 11 Then
                        SubQuery1Maq14 = "('')"
                        SubQuery2Maq14 = "('')"
                    End If
                End If
                tAcumulado = 0
                Maquinas += 1
            End While
            SubQueryGlobalCWO = SubQueryGlobalCWO.TrimEnd(",")
            SubQueryGlobalCWO = SubQueryGlobalCWO + ")"
            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal.TrimEnd(",")
            SubQueryMaterialesGlobal = SubQueryMaterialesGlobal + ")"
            If (Not SubQueryGlobalCWO.Length > 3) And (Not SubQueryMaterialesGlobal.Length > 3) Then
                SubQueryGlobalCWO = "('')"
                SubQueryMaterialesGlobal = "('')"
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub llenaGridWipCutCard(consulta As String)
        Try
            Dim tabla As New DataTable()
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            cnn.Close()
            If tabla.Rows.Count > 0 Then
                tAcumulado = 0
                sumMinHrs = 0
                For i As Integer = 0 To tabla.Rows.Count - 1
                    tabla.Columns(14).ReadOnly = False
                    tabla.Columns(15).ReadOnly = False
                    If tabla.Rows(i).Item("WireBalance").ToString > 0 Then
                        tabla.Rows(i).Item(14) = AcumuladodeTiempo(tabla.Rows(i).Item("Total Minutos").ToString, 1)
                        tabla.Rows(i).Item(15) = CLng(sumMinHrs)
                    Else
                        tabla.Rows(i).Item(14) = 0
                        tabla.Rows(i).Item(15) = 0
                    End If
                Next
                If lblHrsConsultadas.Visible = True Then
                    For a As Integer = 0 To tabla.Rows.Count - 1
                        If Not HoraSet >= tabla.Rows(a).Item("Acumulado").ToString Then
                            tabla.Rows(a).Delete()
                        End If
                    Next
                End If
                With dgvWipCutCard
                    .DataSource = tabla
                    PintaLineasWipCutCard()
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsWipCutCard.Text = "Items: " + dgvWipCutCard.RowCount.ToString
                End With
            Else
                MsgBox("No hay detalle del CWO seleccionado para visualizar")
                dgvWipCutCard.DataSource = Nothing
                lblItemsWipCutCard.Text = "Items: " + dgvWipCutCard.RowCount.ToString
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub PintaLineasWipCutCard()
        Try
            For Each row As DataGridViewRow In dgvWipCutCard.Rows
                If RevisaStartStop(row.Cells(1).Value.ToString) = 1 Then
                    With row
                        .DefaultCellStyle.BackColor = Color.LightSeaGreen
                        .DefaultCellStyle.ForeColor = Color.Black
                        .DefaultCellStyle.Font = New Font(Font, FontStyle.Bold)
                    End With
                End If
                If row.Cells(14).Value.ToString = 0 Then
                    With row
                        .DefaultCellStyle.ForeColor = Color.Gray
                    End With
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Function AcumuladodeTiempo(Tiempo As Integer, Optional ByVal SumMin As Integer = 0) As Integer
        Try
            tAcumulado += Tiempo
            If SumMin = 1 Then
                sumMinHrs = tAcumulado * 1.0 / 60
            End If
            Return tAcumulado
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("UltimoEscaneo", host, UserName)
            Return Nothing
        End Try
    End Function
    Sub LlenaGridPnInCWO()
        ' Este metodo, llena las grid en el GropuBox1 para llenar lo que hay de materiales y cantidades por maquina, tiene 3 tipos de llenado
        ' 1) la variable bandera FlagLlenaGrids = 1 se usa en los eventos click de los metodos CellDoubleClick de las Grids del GroupBox1, evento dgvMatInProdAndSolicitados_CellClick
        ' y evento dgvPNinProdd_CellDoubleClick_1, llenando la Grid con solo 1 numero de parte.
        ' 2) la variable bandera FlagLlenaGrids = 2 se usa en el evento mtbxProxHrs_KeyPress_1() dentro del metodo Hrs() el cual este metodo arma el query de los CWO de esa maquina, y sus numeros de 
        ' parte seleccionados, esto es para lo del hora x hora.
        ' 3) este no contiene variable, el else es para cuando se llama a este metodo mediante el evento HoraXHora_Load() o recargar todo con el evento Button1_Click_1().
        Try
            Dim tb As New DataTable, tb2 As New DataTable, tb3 As New DataTable, tb4 As New DataTable, tb5 As New DataTable, tb6 As New DataTable, tb7 As New DataTable, tb8 As New DataTable, t9 As New DataTable, t10 As New DataTable, t11 As New DataTable
            ' -----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado]  from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 1) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold  from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 1) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold  from tblBOMCWO where CWO in " + SubQuery1Maq1.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq1.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado]  from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 1) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold  from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 1) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            cnn.Close()
            If tb.Rows.Count > 0 Then
                With dgvPNAllocated1
                    .DataSource = tb
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMAq1.Text = "Maquina 1: Items: " + dgvPNAllocated1.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated1.DataSource = Nothing
                lblItemsMAq1.Text = "Maquina 1: Items: " + dgvPNAllocated1.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 2) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 2) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold  from tblBOMCWO where CWO in " + SubQuery1Maq2.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq2.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 2) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 2) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb2.Load(dr)
            cnn.Close()
            If tb2.Rows.Count > 0 Then
                With dgvPNAllocated2
                    .DataSource = tb2
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMaq2.Text = "Maquina 2: Items: " + dgvPNAllocated2.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated2.DataSource = Nothing
                lblItemsMaq2.Text = "Maquina 2: Items: " + dgvPNAllocated2.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 10) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 10) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq10.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq10.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 10) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 10) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb3.Load(dr)
            cnn.Close()
            If tb3.Rows.Count > 0 Then
                With dgvPNAllocated10
                    .DataSource = tb3
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMaq10.Text = "Maquina 10: Items: " + dgvPNAllocated10.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated10.DataSource = Nothing
                lblItemsMaq10.Text = "Maquina 10: Items: " + dgvPNAllocated10.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 13) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 13) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq13.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq13.ToString + " group by PN,Hold"
            Else
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 13) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb4.Load(dr)
            cnn.Close()
            If tb4.Rows.Count > 0 Then
                With dgvPNAllocated13
                    .DataSource = tb4
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMaq13.Text = "Maquina 13: Items: " + dgvPNAllocated13.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated13.DataSource = Nothing
                lblItemsMaq13.Text = "Maquina 13: Items: " + dgvPNAllocated13.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 14) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 14) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq14.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq14.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 14) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 14) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb5.Load(dr)
            cnn.Close()
            If tb5.Rows.Count > 0 Then
                With dgvPNAllocated14
                    .DataSource = tb5
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMaq14.Text = "Maquina 14: Items: " + dgvPNAllocated14.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated14.DataSource = Nothing
                lblItemsMaq14.Text = "Maquina 14: Items: " + dgvPNAllocated14.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 0) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 0) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq0.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq0.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 0) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 0) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb6.Load(dr)
            cnn.Close()
            If tb6.Rows.Count > 0 Then
                With dgvPNAllocated0
                    .DataSource = tb6
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMaq0.Text = "Maquina 0: Items: " + dgvPNAllocated0.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated0.DataSource = Nothing
                lblItemsMaq0.Text = "Maquina 0: Items: " + dgvPNAllocated0.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 5) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 5) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq5.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq5.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 5) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 5) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb7.Load(dr)
            cnn.Close()
            If tb7.Rows.Count > 0 Then
                With dgvPNMaq05
                    .DataSource = tb7
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMaq5.Text = "Maquina 5: Items: " + dgvPNMaq05.Rows.Count.ToString
                End With
            Else
                dgvPNMaq05.DataSource = Nothing
                lblItemsMaq5.Text = "Maquina 5: Items: " + dgvPNMaq05.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 12) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 12) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq12.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq12.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 12) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 12) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb8.Load(dr)
            cnn.Close()
            If tb8.Rows.Count > 0 Then
                With dgvPnInMaq12
                    .DataSource = tb8
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblitemsMaq12.Text = "Maquina 12: Items: " + dgvPnInMaq12.Rows.Count.ToString
                End With
            Else
                dgvPnInMaq12.DataSource = Nothing
                lblitemsMaq12.Text = "Maquina 12: Items: " + dgvPnInMaq12.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 3) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 3) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq3.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq3.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 12) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 3) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            t9.Load(dr)
            cnn.Close()
            If t9.Rows.Count > 0 Then
                With dgvPNAllocated3
                    .DataSource = t9
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblitemsmaq3.Text = "Maquina 3: Items: " + dgvPNAllocated3.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated3.DataSource = Nothing
                lblitemsmaq3.Text = "Maquina 3: Items: " + dgvPNAllocated3.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq4.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq4.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            t10.Load(dr)
            cnn.Close()
            If t10.Rows.Count > 0 Then
                With dgvPNAllocated4
                    .DataSource = t10
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblItemsMaq4.Text = "Maquina 4: Items: " + dgvPNAllocated4.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated4.DataSource = Nothing
                lblItemsMaq4.Text = "Maquina 4: Items: " + dgvPNAllocated4.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 6) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq6.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq6.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 6) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            t10.Load(dr)
            cnn.Close()
            If t10.Rows.Count > 0 Then
                With dgvPNAllocated6
                    .DataSource = t10
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblitemsmaq6.Text = "Maquina 6: Items: " + dgvPNAllocated6.Rows.Count.ToString
                End With
            Else
                dgvPNAllocated6.DataSource = Nothing
                lblitemsmaq6.Text = "Maquina 6: Items: " + dgvPNAllocated6.Rows.Count.ToString
            End If
            ' ----------------
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 11) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' group by PN,Hold"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select PN, CONVERT(int,SUM(balance)) [Qty], CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in " + SubQuery1Maq11.ToString + " and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN in " + SubQuery2Maq11.ToString + " group by PN,Hold"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado] from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 11) and CONVERT(int,Balance) > 0 and PN not like 'S%' group by PN,Hold"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            t11.Load(dr)
            cnn.Close()
            If t11.Rows.Count > 0 Then
                With dgvPnInMaq11
                    .DataSource = t11
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    lblitemsmaq11.Text = "Maquina 11: Items: " + dgvPnInMaq11.Rows.Count.ToString
                End With
            Else
                dgvPnInMaq11.DataSource = Nothing
                lblitemsmaq11.Text = "Maquina 11: Items: " + dgvPnInMaq11.Rows.Count.ToString
            End If
            ' ----------------
            RecorreGridsMatInCWO()
            If dgvPNAllocated1.Rows.Count > 0 Then
                dgvPNAllocated1.Columns("Tag Asignado").Visible = False
                dgvPNAllocated1.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated2.Rows.Count > 0 Then
                dgvPNAllocated2.Columns("Tag Asignado").Visible = False
                dgvPNAllocated2.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated10.Rows.Count > 0 Then
                dgvPNAllocated10.Columns("Tag Asignado").Visible = False
                dgvPNAllocated10.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated13.Rows.Count > 0 Then
                dgvPNAllocated13.Columns("Tag Asignado").Visible = False
                dgvPNAllocated13.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated14.Rows.Count > 0 Then
                dgvPNAllocated14.Columns("Tag Asignado").Visible = False
                dgvPNAllocated14.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated0.Rows.Count > 0 Then
                dgvPNAllocated0.Columns("Tag Asignado").Visible = False
                dgvPNAllocated0.Columns("Hold").Visible = False
            End If
            If dgvPNMaq05.Rows.Count > 0 Then
                dgvPNMaq05.Columns("Tag Asignado").Visible = False
                dgvPNMaq05.Columns("Hold").Visible = False
            End If
            If dgvPnInMaq12.Rows.Count > 0 Then
                dgvPnInMaq12.Columns("Tag Asignado").Visible = False
                dgvPnInMaq12.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated3.Rows.Count > 0 Then
                dgvPNAllocated3.Columns("Tag Asignado").Visible = False
                dgvPNAllocated3.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated4.Rows.Count > 0 Then
                dgvPNAllocated4.Columns("Tag Asignado").Visible = False
                dgvPNAllocated4.Columns("Hold").Visible = False
            End If
            If dgvPNAllocated6.Rows.Count > 0 Then
                dgvPNAllocated6.Columns("Tag Asignado").Visible = False
                dgvPNAllocated6.Columns("Hold").Visible = False
            End If
            If dgvPnInMaq11.Rows.Count > 0 Then
                dgvPnInMaq11.Columns("Tag Asignado").Visible = False
                dgvPnInMaq11.Columns("Hold").Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub RecorreGridsMatInCWO()
        ' En este metodo, recorremos las 8 Grids que se encuentran en el GroupBox1, el cual es el de las maquinas y sus numeros de parte,
        ' aqui tratamos de recorrer las grid con tal de encontrar pn que se igualen en las maquinas, si coinciden se pintara en color verde la celda, 
        ' indicando al usuario que ese numero de parte se encuentra en 2 o mas maquinas.
        Try
            '----------------------------------------------------------------
            For a As Integer = 0 To dgvPNAllocated0.Rows.Count - 1 'Maquina 0
                For b As Integer = 0 To dgvPNAllocated2.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated2.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated2.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated1.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated1.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated1.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNMaq05.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNMaq05.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNMaq05.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated10.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated10.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated10.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq12.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq12.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq12.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated13.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated13.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated13.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated14.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated14.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated4.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated4.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated4.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated6.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated6.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated6.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq11.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq11.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq11.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated3.Rows.Count - 1
                    If dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated3.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated0.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated3.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                If dgvPNAllocated0.Rows(a).Cells("Tag Asignado").Value.ToString > 0 Then
                    dgvPNAllocated0.Rows(a).DefaultCellStyle.ForeColor = Color.Gray
                End If
                If dgvPNAllocated0.Rows(a).Cells("Hold").Value = True Then
                    dgvPNAllocated0.Rows(a).DefaultCellStyle.BackColor = Color.Red
                End If
                RevisaBalanceTAGs(dgvPNAllocated0.Rows(a).Cells("PN").Value.ToString, dgvPNAllocated0.Rows(a).Cells("Qty").Value.ToString, a, dgvPNAllocated0)
            Next
            '----------------------------------------------------------------
            For a As Integer = 0 To dgvPNAllocated1.Rows.Count - 1 'Maquina 1
                For b As Integer = 0 To dgvPNAllocated2.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated2.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated2.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated0.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated0.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated0.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNMaq05.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNMaq05.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNMaq05.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated10.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated10.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated10.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq12.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq12.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq12.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated13.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated13.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated13.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated14.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated14.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated4.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated4.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated4.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated6.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated6.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated6.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq11.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq11.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq11.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated3.Rows.Count - 1
                    If dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated3.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated1.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated3.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                If dgvPNAllocated1.Rows(a).Cells("Tag Asignado").Value.ToString > 0 Then
                    dgvPNAllocated1.Rows(a).DefaultCellStyle.ForeColor = Color.Gray
                End If
                If dgvPNAllocated1.Rows(a).Cells("Hold").Value = True Then
                    dgvPNAllocated1.Rows(a).DefaultCellStyle.BackColor = Color.Red
                End If
                RevisaBalanceTAGs(dgvPNAllocated1.Rows(a).Cells("PN").Value.ToString, dgvPNAllocated1.Rows(a).Cells("Qty").Value.ToString, a, dgvPNAllocated1)
            Next
            '----------------------------------------------------------------
            For a As Integer = 0 To dgvPNAllocated2.Rows.Count - 1 'Maquina 2
                For b As Integer = 0 To dgvPNAllocated1.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated1.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated1.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated10.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated10.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated10.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated13.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated13.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated13.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated14.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated14.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated0.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated0.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated0.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNMaq05.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNMaq05.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNMaq05.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq12.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq12.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq12.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated4.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated4.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated4.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated6.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated6.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated6.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq11.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq11.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq11.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated3.Rows.Count - 1
                    If dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated3.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated3.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                If dgvPNAllocated2.Rows(a).Cells("Tag Asignado").Value.ToString > 0 Then
                    dgvPNAllocated2.Rows(a).DefaultCellStyle.ForeColor = Color.Gray
                End If
                If dgvPNAllocated2.Rows(a).Cells("Hold").Value = True Then
                    dgvPNAllocated2.Rows(a).DefaultCellStyle.BackColor = Color.Red
                End If
                RevisaBalanceTAGs(dgvPNAllocated2.Rows(a).Cells("PN").Value.ToString, dgvPNAllocated2.Rows(a).Cells("Qty").Value.ToString, a, dgvPNAllocated2)
            Next
            '-------------------------------------------------------------------
            For a As Integer = 0 To dgvPNAllocated3.Rows.Count - 1 'Maquina 3
                For b As Integer = 0 To dgvPNAllocated1.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated1.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated1.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated0.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated0.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated0.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNMaq05.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNMaq05.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNMaq05.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated10.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated10.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated10.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq12.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq12.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq12.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated13.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated13.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated13.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated14.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated14.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated4.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated4.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated4.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated6.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated6.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated6.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq11.Rows.Count - 1
                    If dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq11.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated3.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq11.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                If dgvPNAllocated3.Rows(a).Cells("Tag Asignado").Value.ToString > 0 Then
                    dgvPNAllocated3.Rows(a).DefaultCellStyle.ForeColor = Color.Gray
                End If
                If dgvPNAllocated3.Rows(a).Cells("Hold").Value = True Then
                    dgvPNAllocated3.Rows(a).DefaultCellStyle.BackColor = Color.Red
                End If
                RevisaBalanceTAGs(dgvPNAllocated3.Rows(a).Cells("PN").Value.ToString, dgvPNAllocated3.Rows(a).Cells("Qty").Value.ToString, a, dgvPNAllocated3)
            Next
            '----------------------------------------------------------------
            For a As Integer = 0 To dgvPNAllocated10.Rows.Count - 1 'Maquina 10
                For b As Integer = 0 To dgvPNAllocated1.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated1.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated1.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated2.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated2.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated13.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated13.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated13.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated14.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated14.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated0.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated0.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated0.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNMaq05.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNMaq05.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNMaq05.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq12.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq12.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq12.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated4.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated4.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated4.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated6.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated6.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated6.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq11.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq11.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq11.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated3.Rows.Count - 1
                    If dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated3.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated3.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                If dgvPNAllocated10.Rows(a).Cells("Tag Asignado").Value.ToString > 0 Then
                    dgvPNAllocated10.Rows(a).DefaultCellStyle.ForeColor = Color.Gray
                End If
                If dgvPNAllocated10.Rows(a).Cells("Hold").Value = True Then
                    dgvPNAllocated10.Rows(a).DefaultCellStyle.BackColor = Color.Red
                End If
                RevisaBalanceTAGs(dgvPNAllocated10.Rows(a).Cells("PN").Value.ToString, dgvPNAllocated10.Rows(a).Cells("Qty").Value.ToString, a, dgvPNAllocated10)
            Next
            '----------------------------------------------------------------
            For a As Integer = 0 To dgvPNAllocated13.Rows.Count - 1 'Maquina 13
                For b As Integer = 0 To dgvPNAllocated1.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated1.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated1.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated2.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated2.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated10.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated10.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated14.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated14.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated0.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated0.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated0.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNMaq05.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNMaq05.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNMaq05.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq12.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq12.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq12.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated4.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated4.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated4.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated6.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated6.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated6.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq11.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq11.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq11.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated3.Rows.Count - 1
                    If dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated3.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated3.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                If dgvPNAllocated13.Rows(a).Cells("Tag Asignado").Value.ToString > 0 Then
                    dgvPNAllocated13.Rows(a).DefaultCellStyle.ForeColor = Color.Gray
                End If
                If dgvPNAllocated13.Rows(a).Cells("Hold").Value = True Then
                    dgvPNAllocated13.Rows(a).DefaultCellStyle.BackColor = Color.Red
                End If
                RevisaBalanceTAGs(dgvPNAllocated13.Rows(a).Cells("PN").Value.ToString, dgvPNAllocated13.Rows(a).Cells("Qty").Value.ToString, a, dgvPNAllocated13)
            Next
            '----------------------------------------------------------------
            For a As Integer = 0 To dgvPNAllocated14.Rows.Count - 1 'Maquina 14
                For b As Integer = 0 To dgvPNAllocated1.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated1.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated1.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated2.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated2.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated2.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated10.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated10.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated10.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated13.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated13.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated13.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated0.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated0.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated0.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNMaq05.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNMaq05.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNMaq05.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq12.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq12.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq12.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated4.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated4.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated4.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated6.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated6.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated6.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPnInMaq11.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPnInMaq11.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPnInMaq11.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                For b As Integer = 0 To dgvPNAllocated3.Rows.Count - 1
                    If dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString = dgvPNAllocated3.Rows(b).Cells("PN").Value.ToString Then
                        dgvPNAllocated14.Rows(a).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                        dgvPNAllocated3.Rows(b).Cells("PN").Style.BackColor = Color.DarkSeaGreen
                    End If
                Next
                If dgvPNAllocated14.Rows(a).Cells("Tag Asignado").Value.ToString > 0 Then
                    dgvPNAllocated14.Rows(a).DefaultCellStyle.ForeColor = Color.Gray
                End If
                If dgvPNAllocated14.Rows(a).Cells("Hold").Value = True Then
                    dgvPNAllocated14.Rows(a).DefaultCellStyle.BackColor = Color.Red
                End If
                RevisaBalanceTAGs(dgvPNAllocated14.Rows(a).Cells("PN").Value.ToString, dgvPNAllocated14.Rows(a).Cells("Qty").Value.ToString, a, dgvPNAllocated14)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub dgvWipCutCard_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles dgvWipCutCard.ColumnAdded
        e.Column.SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub
    Public Sub OcultaOpcionesHrs(flag As Integer)
        If flag = 1 Then
            If dgvPNAllocated0.Rows.Count > 0 Then btnDet0.Visible = True
            If dgvPNAllocated1.RowCount > 0 Then btnDet1.Visible = True
            If dgvPNAllocated2.RowCount > 0 Then btnDet2.Visible = True
            If dgvPNAllocated3.RowCount > 0 Then btnDet3.Visible = True
            If dgvPNAllocated4.RowCount > 0 Then btnDet4.Visible = True
            If dgvPNMaq05.RowCount > 0 Then btnDet5.Visible = True
            If dgvPNAllocated6.RowCount > 0 Then btnDet6.Visible = True
            If dgvPNAllocated10.RowCount > 0 Then btnDet10.Visible = True
            If dgvPnInMaq11.RowCount > 0 Then btnDet11.Visible = True
            If dgvPnInMaq12.RowCount > 0 Then btnDet12.Visible = True
            If dgvPNAllocated13.RowCount > 0 Then btnDet13.Visible = True
            If dgvPNAllocated14.RowCount > 0 Then btnDet14.Visible = True
        Else
            btnDet0.Visible = False
            btnDet1.Visible = False
            btnDet2.Visible = False
            btnDet3.Visible = False
            btnDet4.Visible = False
            btnDet5.Visible = False
            btnDet6.Visible = False
            btnDet10.Visible = False
            btnDet11.Visible = False
            btnDet12.Visible = False
            btnDet13.Visible = False
            btnDet14.Visible = False
        End If
    End Sub
    Private Sub btnRefrescarMaqPN_Click(sender As Object, e As EventArgs) Handles btnRefrescarMaqPN.Click
        ' Evento creado para salir del modo de busqueda por hora x hora seleccionada en el maskedbox, y ver todo en general
        Cursor.Current = Cursors.WaitCursor
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = False
        TabPage1.Parent = Nothing
        txtBuscador.Clear()
        Cursor.Current = Cursors.Default
    End Sub
    Sub CalculateCompare(PN As String, QtyInProd As Integer)
        ' En este metodo, invocado por el evento dgvPNinProdd_CellDoubleClick_1(), buscamos el numero de parte seleccionado para asi, si existe en
        ' mas de una de las grid, sume las cantidades requeridas en la variable sumQty y las muestre en el label lblqtyCompare, esto para que el usuario
        ' sepa cuanta cantidad debera surtir en total si ese numero de parte seleccionado se usa en mas de una maquina
        Try
            Dim sumQty As Integer, reset As Integer
            If dgvPNAllocated1.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated1.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated2.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated2.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated3.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated3.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated4.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated4.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated6.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated6.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated10.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated10.Rows(0).Cells(1).Value.ToString))
            If dgvPnInMaq11.Rows.Count > 0 Then sumQty += CInt(Val(dgvPnInMaq11.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated13.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated13.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated14.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated14.Rows(0).Cells(1).Value.ToString))
            If dgvPNAllocated0.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNAllocated0.Rows(0).Cells(1).Value.ToString))
            If dgvPNMaq05.Rows.Count > 0 Then sumQty += CInt(Val(dgvPNMaq05.Rows(0).Cells(1).Value.ToString))
            If dgvPnInMaq12.Rows.Count > 0 Then sumQty += CInt(Val(dgvPnInMaq12.Rows(0).Cells(1).Value.ToString))
            lblPNCompare.Text = "Numero de parte: " + PN.ToString
            lblqtyCompare.Text = "Cantidad a utilizar: " + sumQty.ToString
            lblInProdCompare.Text = "Cantidad en piso: " + QtyInProd.ToString
            If QtyInProd > sumQty Then
                lblTagRecomendado.Visible = False
                lblLocTagRecomendado.Visible = False
                lblQtyTagRecomendado.Visible = False
                lblDiffCompare.Text = "Surtir: 0"
            Else
                reset = sumQty - QtyInProd     ' Para surtir en caso de que se requiera, se invoca al metodo SetQTYandLocationPN()
                lblDiffCompare.Text = "Surtir: " + reset.ToString
                If Microsoft.VisualBasic.Left(reset, 1) = "-" Then
                    Dim quitNeg As Integer = Math.Abs(reset)
                    SetQTYandLocationPN(PN, quitNeg)
                    lblTagRecomendado.Visible = True
                    lblLocTagRecomendado.Visible = True
                    lblQtyTagRecomendado.Visible = True
                Else
                    SetQTYandLocationPN(PN, reset)
                    lblTagRecomendado.Visible = True
                    lblLocTagRecomendado.Visible = True
                    lblQtyTagRecomendado.Visible = True
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub SetQTYandLocationPN(PN As String, qty As Integer)
        ' En este metodo, invocado por CalculateCompare(), se utiliza para encontrar para el usuario, el material que este disponible para surtir con
        ' la cantidad ideal para el surtido
        Try
            cmd = New SqlCommand("select top(1) TAG,Location,CONVERT(int,Balance) [Balance] From tblItemsTags Where Balance > 0 and PN='" + PN.ToString + "' and Status='Available' and  Balance >= " + qty.ToString + " order by CreatedDate asc", cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    lblTagRecomendado.Text = "Tag recomendado: " & CStr(dr.GetValue(0))
                    lblLocTagRecomendado.Text = "Locacion de tag: " & CStr(dr.GetValue(1))
                    lblQtyTagRecomendado.Text = "Cantidad de Tag: " & CStr(dr.GetValue(2))
                End While
            Else
                lblTagRecomendado.Text = "Tag recomendado: - "
                lblLocTagRecomendado.Text = "Locacion de tag: - "
                lblQtyTagRecomendado.Text = "Cantidad de Tag: - "
            End If
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub btnRefrescaGridCut_Click(sender As Object, e As EventArgs) Handles btnRefrescaGridCut.Click
        ' Evento creado para refrescar toda la vista del usuario y estar actualizado
        Cursor.Current = Cursors.WaitCursor
        Dim clear As New DataTable()
        clear.Rows.Add(clear.NewRow)
        DataGridView1.DataSource = clear
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        DataGridView1.RefreshEdit()
        'HoraXHora_Load(New System.Object, New System.EventArgs)
        FlagLlenaGrids = 0
        FlagAux = 0
        CreandoDatosDgvMaquinas()
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        OcultaOpcionesHrs(0)
        Button1.Visible = False
        lblHrsConsultadas.Text = " Horas consultadas."
        lblHrsConsultadas.Visible = False
        dgvWipCutCard.DataSource = Nothing
        Cursor.Current = Cursors.Default
    End Sub
    Sub llenaGridMatInProd()
        Try
            If FlagLlenaGrids = 1 Then
                'query = "select TAG,bw.PN,'' as [Ultima Maquina usada],OutDate [Fecha de Salida],ml.Balance From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq in (0,1,2,5,10,12,13,14)) and bw.Balance > 0 and bw.PN not like 'S%' and bw.PN = '" + PNSearch.ToString + "' and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0 group by  TAG, bw.PN,OutDate,ml.Balance"
                query = "select TAG,bw.PN,'' as [Ultima Maquina usada],OutDate [Fecha de Salida],ml.Balance From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1)) and bw.Balance > 0 and bw.PN not like 'S%' and bw.PN = '" + PNSearch.ToString + "' and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0 group by  TAG, bw.PN,OutDate,ml.Balance"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select TAG,bw.PN,'' as [Ultima Maquina usada],OutDate [Fecha de Salida],ml.Balance From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in " + SubQueryGlobalCWO.ToString + " and bw.Balance > 0 and bw.PN not like 'S%' and bw.PN in " + SubQueryMaterialesGlobal.ToString + " and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0 group by  TAG, bw.PN,OutDate,ml.Balance"
            Else
                'query = "select TAG,bw.PN,'' as [Ultima Maquina usada],OutDate [Fecha de Salida],ml.Balance From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq in (0,1,2,5,10,12,13,14)) and bw.Balance > 0 and bw.PN not like 'S%' and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0 group by  TAG, bw.PN,OutDate,ml.Balance"
                query = "select TAG,bw.PN,'' as [Ultima Maquina usada],OutDate [Fecha de Salida],ml.Balance From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1)) and bw.Balance > 0 and bw.PN not like 'S%' and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0 group by TAG, bw.PN,OutDate,ml.Balance"
            End If
            Dim table As New DataTable(), tb As New DataTable()
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            cnn.Close()
            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    tb.Columns(2).ReadOnly = False
                    tb.Columns(2).MaxLength = 50
                    tb.Rows(i).Item(2) = UltimoEscaneo(tb.Rows(i).Item("TAG").ToString)
                Next
                With dgvPNinProdd
                    .DataSource = tb
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    .Visible = True
                    lblItemsPNinProd.Text = "Items: " + dgvPNinProdd.Rows.Count.ToString
                End With
            Else
                dgvPNinProdd.DataSource = Nothing
                lblItemsPNinProd.Text = "Items: " + dgvPNinProdd.Rows.Count.ToString
            End If
            If FlagLlenaGrids = 1 Then
                'query = "select TAG,bw.PN,CONVERT(int,ml.Balance) [Balance],Location From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq in (0,1,2,5,10,12,13,14)) and bw.PN not like 'S%' and bw.PN='" + PNSearch.ToString + "' and Status='Available' and ml.Balance > 0 and bw.Balance > 0 and (ml.Balance >= bw.Balance) and (bw.TagAsignado is null or bw.TagAsignado = 0) group by  TAG, bw.PN,OutDate,ml.Balance,ml.Location"
                query = "select TAG,bw.PN,CONVERT(int,ml.Balance) [Balance],Location From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1)) and bw.PN not like 'S%' and bw.PN='" + PNSearch.ToString + "' and Status='Available' and ml.Balance > 0 and bw.Balance > 0 and (ml.Balance >= bw.Balance) and (bw.TagAsignado is null or bw.TagAsignado = 0) group by  TAG, bw.PN,OutDate,ml.Balance,ml.Location"
            ElseIf FlagLlenaGrids = 2 Then
                query = "select TAG,bw.PN,CONVERT(int,ml.Balance) [Balance],Location From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in " + SubQueryGlobalCWO.ToString + " and bw.PN not like 'S%' and bw.PN in " + SubQueryMaterialesGlobal.ToString + " and Status='Available' and ml.Balance > 0 and bw.Balance > 0 and (ml.Balance >= bw.Balance) and (bw.TagAsignado is null or bw.TagAsignado = 0) group by  TAG, bw.PN,OutDate,ml.Balance,ml.Location"
            Else
                'query = "select TAG,bw.PN,CONVERT(int,ml.Balance) [Balance],Location From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (20,25,29) and Status='OPEN' and FechaSolicitudMat is not null and Maq in (0,1,2,5,10,12,13,14)) and bw.PN not like 'S%' and Status='Available' and ml.Balance > 0 and bw.Balance > 0 and (ml.Balance >= bw.Balance) and (bw.TagAsignado is null or bw.TagAsignado = 0) group by  TAG, bw.PN,OutDate,ml.Balance,ml.Location"
                query = "select TAG,bw.PN,CONVERT(int,ml.Balance) [Balance],Location From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (12,14,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1)) and bw.PN not like 'S%' and Status='Available' and ml.Balance > 0 and bw.Balance > 0 and (ml.Balance >= bw.Balance) and (bw.TagAsignado is null or bw.TagAsignado = 0) group by  TAG, bw.PN,OutDate,ml.Balance,ml.Location"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            table.Load(dr)
            cnn.Close()
            If table.Rows.Count > 0 Then
                With dgvPNinAlmacen
                    .DataSource = table
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    .Visible = True
                    lblItestagenAlmacen.Text = "Items: " + dgvPNinAlmacen.Rows.Count.ToString
                End With
            Else
                dgvPNinAlmacen.DataSource = Nothing
                lblItestagenAlmacen.Text = "Items: " + dgvPNinAlmacen.Rows.Count.ToString
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub LlenaGridSimilitud()
        Try
            Dim tb As New DataTable
            If FlagLlenaGrids = 1 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty a surtir], (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                '                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='NoAvailable') [Qty en Produccion]," &
                '                     " (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                '                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='Available') [Qty en Almacen]," &
                '                     " (" &
                '                     " select CONVERT(date,JuarezDueDate) from tblItemsPOsDet podet inner join tblItemsPOs po on po.IDPO=podet.IDPO " &
                '                     " where podet.PN=bw.PN  And (QtyReceivedJuarez > 0 And QtyReceivedJuarez < QtyBalance) " &
                '                     " And MasterList=1 " &
                '                     " And Status='OPEN'" &
                '                     " ) [Proxima Fecha de recibo] " &
                '                     " from tblBOMCWO bw inner join tblCWO c on bw.CWO=c.CWO " &
                '                     " where  WSort in (20,25,29) and " &
                '                     " Status='OPEN' and FechaSolicitudMat is not null and Maq in (0,1,2,5,10,12,13,14) and Balance > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' " &
                '                     " group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty a surtir], (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='NoAvailable') [Qty en Produccion]," &
                                     " (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='Available') [Qty en Almacen]," &
                                     " (" &
                                     " select top(1) CONVERT(date,JuarezDueDate) from tblItemsPOsDet podet inner join tblItemsPOs po on po.IDPO=podet.IDPO " &
                                     " where podet.PN=bw.PN  And (QtyReceivedJuarez > 0 And QtyReceivedJuarez < QtyBalance) " &
                                     " And MasterList=1 " &
                                     " And Status='OPEN'" &
                                     " ) [Proxima Fecha de recibo] " &
                                     " from tblBOMCWO bw inner join tblCWO c on bw.CWO=c.CWO " &
                                     " where  WSort in (12,14,20,25,29) and " &
                                     " Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1) and Balance > 0 and PN not like 'S%' and PN = '" + PNSearch.ToString + "' " &
                                     " group by PN"
            ElseIf FlagLlenaGrids = 2 Then
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty a surtir], (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                '                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='NoAvailable') [Qty en Produccion]," &
                '                     " (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                '                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='Available') [Qty en Almacen]," &
                '                     " (" &
                '                     " select CONVERT(date,JuarezDueDate) from tblItemsPOsDet podet inner join tblItemsPOs po on po.IDPO=podet.IDPO " &
                '                     " where podet.PN=bw.PN  And (QtyReceivedJuarez > 0 And QtyReceivedJuarez < QtyBalance) " &
                '                     " And MasterList=1 " &
                '                     " And Status='OPEN'" &
                '                     " ) [Proxima Fecha de recibo] " &
                '                     " from tblBOMCWO bw inner join tblCWO c on bw.CWO=c.CWO " &
                '                     " where  WSort in (20,25,29) and c.CWO in " + SubQueryGlobalCWO.ToString + " and " &
                '                     " Status='OPEN' and FechaSolicitudMat is not null and Maq in (0,1,2,5,10,12,13,14) and Balance > 0 and PN not like 'S%' and PN in " + SubQueryMaterialesGlobal.ToString + " " &
                '                     " group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty a surtir], (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='NoAvailable') [Qty en Produccion]," &
                                     " (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='Available') [Qty en Almacen]," &
                                     " (" &
                                     " select top(1) CONVERT(date,JuarezDueDate) from tblItemsPOsDet podet inner join tblItemsPOs po on po.IDPO=podet.IDPO " &
                                     " where podet.PN=bw.PN  And (QtyReceivedJuarez > 0 And QtyReceivedJuarez < QtyBalance) " &
                                     " And MasterList=1 " &
                                     " And Status='OPEN'" &
                                     " ) [Proxima Fecha de recibo] " &
                                     " from tblBOMCWO bw inner join tblCWO c on bw.CWO=c.CWO " &
                                     " where  WSort in (12,14,20,25,29) and c.CWO in " + SubQueryGlobalCWO.ToString + " and " &
                                     " Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1) and Balance > 0 and PN not like 'S%' and PN in " + SubQueryMaterialesGlobal.ToString + " " &
                                     " group by PN"
            Else
                'query = "select PN, CONVERT(int,SUM(balance)) [Qty a surtir], (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                '     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='NoAvailable') [Qty en Produccion]," &
                '     " (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                '     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='Available') [Qty en Almacen]," &
                '     " (" &
                '     " select CONVERT(date,JuarezDueDate) from tblItemsPOsDet podet inner join tblItemsPOs po on po.IDPO=podet.IDPO " &
                '     " where podet.PN=bw.PN  And (QtyReceivedJuarez > 0 And QtyReceivedJuarez < QtyBalance) " &
                '     " And MasterList=1 " &
                '     " And Status='OPEN'" &
                '     " ) [Proxima Fecha de recibo] " &
                '     " from tblBOMCWO bw inner join tblCWO c on bw.CWO=c.CWO " &
                '     " where  WSort in (20,25,29) and " &
                '     " Status='OPEN' and FechaSolicitudMat is not null and Maq in (0,1,2,5,10,12,13,14) and Balance > 0 and PN not like 'S%' " &
                '     " group by PN"
                query = "select PN, CONVERT(int,SUM(balance)) [Qty a surtir], (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='NoAvailable') [Qty en Produccion]," &
                     " (select ISNULL(NULLIF(CONVERT(int,SUM(Balance)),0),0) " &
                     " from tblItemsTags where tblItemsTags.PN=bw.PN and Balance > 0 and PN not like 'S%' and Status='Available') [Qty en Almacen]," &
                     " (" &
                     " select top(1) CONVERT(date,JuarezDueDate) from tblItemsPOsDet podet inner join tblItemsPOs po on po.IDPO=podet.IDPO " &
                     " where podet.PN=bw.PN  And (QtyReceivedJuarez > 0 And QtyReceivedJuarez < QtyBalance) " &
                     " And MasterList=1 " &
                     " And Status='OPEN'" &
                     " ) [Proxima Fecha de recibo] " &
                     " from tblBOMCWO bw inner join tblCWO c on bw.CWO=c.CWO " &
                     " where  WSort in (12,14,20,25,29) and " &
                     " Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1) and Balance > 0 and PN not like 'S%' " &
                     " group by PN"
            End If
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            cnn.Close()
            If tb.Rows.Count > 0 Then
                With dgvMatInProdAndSolicitados
                    .DataSource = tb
                    .ClearSelection()
                    SurtiendoMat()
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    lblItemsSurtido.Text = "Items: " + dgvMatInProdAndSolicitados.Rows.Count.ToString
                End With
            Else
                dgvMatInProdAndSolicitados.DataSource = Nothing
                lblItemsSurtido.Text = "Items: " + dgvMatInProdAndSolicitados.Rows.Count.ToString
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub dgvMatInProdAndSolicitados_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvMatInProdAndSolicitados.ColumnHeaderMouseClick
        SurtiendoMat()
    End Sub
    Private Sub dgvPNAllocated0_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated0.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated1.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated2_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated2.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNMaq05_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNMaq05.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated10_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated10.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPnInMaq12_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPnInMaq12.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated13_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated13.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated14_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated14.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub btnDet0_Click(sender As Object, e As EventArgs) Handles btnDet0.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=0 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 0"
    End Sub
    Private Sub btnDet1_Click(sender As Object, e As EventArgs) Handles btnDet1.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=1 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 1"
    End Sub
    Private Sub btnDet2_Click(sender As Object, e As EventArgs) Handles btnDet2.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=2 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 2"
    End Sub
    Private Sub btnDet5_Click(sender As Object, e As EventArgs) Handles btnDet5.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=5 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 5"
    End Sub
    Private Sub btnDet10_Click(sender As Object, e As EventArgs) Handles btnDet10.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=10 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 10"
    End Sub
    Private Sub btnDet12_Click(sender As Object, e As EventArgs) Handles btnDet12.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=12 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 12"
    End Sub
    Private Sub btnDet13_Click(sender As Object, e As EventArgs) Handles btnDet13.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=13 order by Id asc, IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 13"
    End Sub
    Private Sub btnDet14_Click(sender As Object, e As EventArgs) Handles btnDet14.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',0 [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=14 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 14"
    End Sub
    Private Sub AsignarMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Try
                    DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    CWO = Convert.ToString(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        End If
    End Sub
    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        If DataGridView1.RowCount > 0 Then
            If opcion = 2 Then
                If e.Button = System.Windows.Forms.MouseButtons.Right Then
                    ContextMenuAsignar.Show(Cursor.Position.X, Cursor.Position.Y)
                    If RevisawSortCWO(CWO) = 12 Then
                        ToolStripMenuItem1.Visible = True
                        ToolStripMenuItem2.Visible = False
                    Else
                        ToolStripMenuItem1.Visible = False
                        ToolStripMenuItem2.Visible = True
                    End If
                End If
            End If
        End If
    End Sub
    Function RevisawSortCWO(CWO As String) As Integer
        Try
            Dim wsort As Integer
            cmd = New SqlCommand("select wSort from tblCWO where CWO='" + CWO.ToString + "'", cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            wsort = If(CInt(cmd.ExecuteScalar) = 0, 0, cmd.ExecuteScalar())
            cnn.Close()
            Return wsort
        Catch ex As Exception
            cnn.Close()
            Return 0
        End Try
    End Function
    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click
        With Principal
            .Show()
            .BringToFront()
        End With
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub HoraXHora_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Principal.Show()
    End Sub
    Private Sub mtbxProxHrs_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles mtbxProxHrs.KeyPress
        Cursor.Current = Cursors.WaitCursor
        Try
            If e.KeyChar = Chr(13) And IsNumeric(mtbxProxHrs.Text) And mtbxProxHrs.Text.Length <= 2 Then
                If mtbxProxHrs.Text < 18 Then
                    HoraSet = CInt(mtbxProxHrs.Text)
                    HoraSet = HoraSet * 60 / 1
                    lblHrsConsultadas.Text = mtbxProxHrs.Text.ToString & " Horas consultadas."
                    lblHrsConsultadas.Visible = True
                    mtbxProxHrs.Clear()
                    mtbxProxHrs.Focus()
                    SubQueryGlobalCWO = "("
                    SubQueryMaterialesGlobal = "("
                    Hrs(HoraSet)
                    FlagAux = 2
                    FlagLlenaGrids = 2
                    LlenaGridPnInCWO()
                    llenaGridMatInProd()
                    LlenaGridSimilitud()
                    OcultaOpcionesHrs(1)
                    Button1.Visible = True
                Else
                    mtbxProxHrs.Clear()
                    mtbxProxHrs.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor.Current = Cursors.WaitCursor
        Dim clear As New DataTable()
        clear.Rows.Add(clear.NewRow)
        DataGridView1.DataSource = clear
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        DataGridView1.RefreshEdit()
        'HoraXHora_Load(New System.Object, New System.EventArgs)
        FlagLlenaGrids = 0
        FlagAux = 0
        CreandoDatosDgvMaquinas()
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        OcultaOpcionesHrs(0)
        Button1.Visible = False
        lblHrsConsultadas.Text = " Horas consultadas."
        lblHrsConsultadas.Visible = False
        dgvWipCutCard.DataSource = Nothing
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Dim respuesta As String
        respuesta = MessageBox.Show("¿Seguro que desea detener este CWO?", "Detenido", MessageBoxButtons.YesNo)
        If respuesta = vbYes Then
            If CWO <> "" Then
                Dim ver As Char = CWO(0)
                If ver = "C" Then
                    p = 12
                    With Materiales
                        .MaximumSize = New System.Drawing.Size(1228, 569)
                        .AutoScroll = False
                        .lblcwomat.Text = CWO
                        .Label4.Text = ""
                        .Show()
                    End With
                Else
                    MessageBox.Show("La celda seleccionada no contiene un CWO")
                    ContextMenuAsignar.Close()
                End If
                ContextMenuAsignar.Close()
            End If
        ElseIf respuesta = vbNo Then
            ContextMenuAsignar.Close()
        End If
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Cursor.Current = Cursors.WaitCursor
        If CWO <> "" Then
            Dim ver As Char = CWO(0)
            If ver = "C" Then
                p = 0
                Materiales.lblcwomat.Text = CWO
                Materiales.Show()
            Else
                MessageBox.Show("La celda seleccionada no contiene un CWO")
                ContextMenuAsignar.Close()
            End If
        End If
        ContextMenuAsignar.Close()
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

    End Sub
    Private Sub dgvPnInMaq11_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPnInMaq11.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated6_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated6.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated4_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated4.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated3_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPNAllocated3.ColumnHeaderMouseClick
        RecorreGridsMatInCWO()
    End Sub
    Private Sub dgvPNAllocated3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated3.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated3.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNAllocated4_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated4.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated4.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNAllocated6_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated6.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated6.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPnInMaq11_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPnInMaq11.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated6.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub btnDet3_Click(sender As Object, e As EventArgs) Handles btnDet3.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',convert(float,round(TSetup / 60 + TRuntime / 60,2,1)) [Total Hrs] from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=3 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 3"
    End Sub
    Private Sub btnDet4_Click(sender As Object, e As EventArgs) Handles btnDet4.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',convert(float,round(TSetup / 60 + TRuntime / 60,2,1)) [Total Hrs] from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=4 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 4"
    End Sub
    Private Sub btnDet6_Click(sender As Object, e As EventArgs) Handles btnDet6.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',convert(float,round(TSetup / 60 + TRuntime / 60,2,1)) [Total Hrs] from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=6 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 6"
    End Sub
    Private Sub btnDet11_Click(sender As Object, e As EventArgs) Handles btnDet11.Click
        llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',convert(float,round(TSetup / 60 + TRuntime / 60,2,1)) [Total Hrs] from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.Maq=11 order by Id asc,IdSort asc")
        lblmaquina.Text = "Maquina seleccionada: 11"
    End Sub
    Private Sub txtBuscador_TextChanged_1(sender As Object, e As EventArgs) Handles txtBuscador.TextChanged
        Cursor.Current = Cursors.WaitCursor
        Dim filtro As String = CType(sender, TextBox).Text
        If filtro.Trim() <> String.Empty Then
            filtrarDatos(filtro)
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Public Sub filtrarDatos(buscar As String)
        Try
            Dim da1 As New SqlDataAdapter, da2 As New SqlDataAdapter, da3 As New SqlDataAdapter, da4 As New SqlDataAdapter, da5 As New SqlDataAdapter, da6 As New SqlDataAdapter, da7 As New SqlDataAdapter, da8 As New SqlDataAdapter, da9 As New SqlDataAdapter, da10 As New SqlDataAdapter, da11 As New SqlDataAdapter, da12 As New SqlDataAdapter, da13 As New SqlDataAdapter, da14 As New SqlDataAdapter
            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 1) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da1 = New SqlDataAdapter(query, cnn)
            da1.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 2) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da2 = New SqlDataAdapter(query, cnn)
            da2.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 10) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da3 = New SqlDataAdapter(query, cnn)
            da3.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 13) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da4 = New SqlDataAdapter(query, cnn)
            da4.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 14) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da5 = New SqlDataAdapter(query, cnn)
            da5.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 0) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da6 = New SqlDataAdapter(query, cnn)
            da6.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 5) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da7 = New SqlDataAdapter(query, cnn)
            da7.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 12) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da8 = New SqlDataAdapter(query, cnn)
            da8.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 3) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da11 = New SqlDataAdapter(query, cnn)
            da11.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 4) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da12 = New SqlDataAdapter(query, cnn)
            da12.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 6) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da13 = New SqlDataAdapter(query, cnn)
            da13.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select PN, CONVERT(int,SUM(balance)) [Qty],CONVERT(int,SUM(ISNULL(TagAsignado,0))) [Tag Asignado],Hold from tblBOMCWO where CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq = 11) and CONVERT(int,Balance) > 0 and PN not like 'S%' and PN like @filtro group by PN,Hold"
            da14 = New SqlDataAdapter(query, cnn)
            da14.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))
            '------------------------
            query = "select TAG,bw.PN,'' as [Ultima Maquina usada],OutDate [Fecha de Salida],ml.Balance From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1)) and bw.Balance > 0 and bw.PN not like 'S%' and bw.PN like @filtro and Status='NoAvailable' and ml.Balance > 0 and bw.Balance > 0 group by TAG, bw.PN,OutDate,ml.Balance"
            da9 = New SqlDataAdapter(query, cnn)
            da9.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            query = "select TAG,bw.PN,CONVERT(int,ml.Balance) [Balance],Location From tblBOMCWO As bw inner Join tblItemsTags As ml On bw.PN = ml.PN Where bw.CWO in (select CWO from tblCWO where WSort in (12,20,25,29) and Status='OPEN' and (Id is not null or Id > 0) and Maq in (select Maq from tblMaqRates where Active = 1)) and bw.PN not like 'S%' and bw.PN like @filtro and Status='Available' and ml.Balance > 0 and bw.Balance > 0 and (ml.Balance >= bw.Balance) and (bw.TagAsignado is null or bw.TagAsignado = 0) group by TAG, bw.PN,OutDate,ml.Balance,ml.Location"
            da10 = New SqlDataAdapter(query, cnn)
            da10.SelectCommand.Parameters.AddWithValue("@filtro", String.Format("%{0}%", buscar))

            Dim table As New DataTable, table1 As New DataTable, table2 As New DataTable, table3 As New DataTable, table4 As New DataTable, table5 As New DataTable, table6 As New DataTable, table7 As New DataTable, table8 As New DataTable, table9 As New DataTable, table10 As New DataTable, table11 As New DataTable, table12 As New DataTable, table13 As New DataTable, table14 As New DataTable
            da1.Fill(table)
            da2.Fill(table1)
            da3.Fill(table2)
            da4.Fill(table3)
            da5.Fill(table4)
            da6.Fill(table5)
            da7.Fill(table6)
            da8.Fill(table7)
            da9.Fill(table8)
            da10.Fill(table9)
            da11.Fill(table11)
            da12.Fill(table12)
            da13.Fill(table13)
            da14.Fill(table14)

            With dgvPNAllocated1
                .DataSource = table
                .ClearSelection()
            End With
            With dgvPNAllocated2
                .DataSource = table1
                .ClearSelection()
            End With
            With dgvPNAllocated10
                .DataSource = table2
                .ClearSelection()
            End With
            With dgvPNAllocated13
                .DataSource = table3
                .ClearSelection()
            End With
            With dgvPNAllocated14
                .DataSource = table4
                .ClearSelection()
            End With
            With dgvPNAllocated0
                .DataSource = table5
                .ClearSelection()
            End With
            With dgvPNMaq05
                .DataSource = table6
                .ClearSelection()
            End With
            With dgvPnInMaq12
                .DataSource = table7
                .ClearSelection()
            End With
            With dgvPNAllocated3
                .DataSource = table11
                .ClearSelection()
            End With
            With dgvPNAllocated4
                .DataSource = table12
                .ClearSelection()
            End With
            With dgvPNAllocated6
                .DataSource = table13
                .ClearSelection()
            End With
            With dgvPnInMaq11
                .DataSource = table14
                .ClearSelection()
            End With
            '-----------------------
            If table8.Rows.Count > 0 Then
                For i As Integer = 0 To table8.Rows.Count - 1
                    table8.Columns(2).ReadOnly = False
                    table8.Columns(2).MaxLength = 50
                    table8.Rows(i).Item(2) = UltimoEscaneo(table8.Rows(i).Item("TAG").ToString)
                Next
                With dgvPNinProdd
                    .DataSource = table8
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    .Visible = True
                    lblItemsPNinProd.Text = "Items: " + dgvPNinProdd.Rows.Count.ToString
                End With
            Else
                dgvPNinProdd.DataSource = Nothing
                lblItemsPNinProd.Text = "Items: " + dgvPNinProdd.Rows.Count.ToString
            End If
            If table9.Rows.Count > 0 Then
                With dgvPNinAlmacen
                    .DataSource = table9
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.ForeColor = Color.Black
                    .DefaultCellStyle.Font = New Font(Font, FontStyle.Regular)
                    .ClearSelection()
                    .Visible = True
                    lblItestagenAlmacen.Text = "Items: " + dgvPNinAlmacen.Rows.Count.ToString
                End With
            Else
                dgvPNinAlmacen.DataSource = Nothing
                lblItestagenAlmacen.Text = "Items: " + dgvPNinAlmacen.Rows.Count.ToString
            End If
            RecorreGridsMatInCWO()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If lblHrsConsultadas.Visible = False Then
            Dim cabecera As String = ""
            Dim cdx As Integer = e.ColumnIndex
            Dim rdx As Integer = e.RowIndex
            If Not rdx = -1 Or cdx = -1 Then
                cabecera = Me.DataGridView1.Columns(cdx).HeaderText
            End If
            If cabecera = "CWO" Then
                llenaGridWipCutCard("select WIP,det.CWO,AU,Rev,WID,IdSort,LengthWire [Length],Balance,WireBalance,isnull(convert(int,(LengthWire * Balance) * 1.0 / 304.8),0) [Total Ft],Wire,TermA,TermB,ISNULL(TSetup + TRuntime,0) [Total Minutos],null as 'Acumulado',convert(float,round(TSetup / 60 + TRuntime / 60,2,1)) [Total Hrs],MaqA,MaqB,AplicatorA,AplicatorB from tblWipDet det inner join tblCWO c on c.CWO=det.CWO where c.WSort in (12,20,25,29) and c.Status='OPEN' and (c.Id is not null or c.Id > 0) and c.CWO= '" + Me.DataGridView1.CurrentCell.Value.ToString() + "' and c.Maq in (select Maq from tblMaqRates where Active = 1) order by Id asc,IdSort asc")
            End If
        Else
            MessageBox.Show("Esta consultando por horas, si desea ver detallado completo, presione el boton Exit situado en la parte superior derecha para salir de la vista por hora y poder consultar detalle completo por maquina", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub dgvPNAllocated0_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated0.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated0.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNMaq05.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNAllocated1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated1.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated1.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNMaq05.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNAllocated2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated2.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated2.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNMaq05.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNMaq05_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNMaq05.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNMaq05.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNAllocated10_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated10.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated10.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNMaq05.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPnInMaq12_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPnInMaq12.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPnInMaq12.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNMaq05.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNAllocated13_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated13.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated13.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNMaq05.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated14.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvPNAllocated14_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNAllocated14.CellDoubleClick
        FlagLlenaGrids = 1
        PNSearch = dgvPNAllocated14.Rows(e.RowIndex).Cells("PN").Value.ToString
        dgvPNAllocated0.DataSource = Nothing
        dgvPNAllocated1.DataSource = Nothing
        dgvPNAllocated2.DataSource = Nothing
        dgvPNMaq05.DataSource = Nothing
        dgvPNAllocated10.DataSource = Nothing
        dgvPnInMaq12.DataSource = Nothing
        dgvPNAllocated13.DataSource = Nothing
        LlenaGridPnInCWO()
        llenaGridMatInProd()
        LlenaGridSimilitud()
        btnRefrescarMaqPN.Visible = True
        FlagLlenaGrids = If(FlagAux = 2, 2, 0)
    End Sub
    Private Sub dgvMatInProdAndSolicitados_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMatInProdAndSolicitados.CellClick
        If e.RowIndex > -1 Then
            txtBuscador.Text = dgvMatInProdAndSolicitados.Rows(e.RowIndex).Cells("PN").Value.ToString
            btnRefrescarMaqPN.Visible = True
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Time()
    End Sub
    Private Sub dgvPNinProdd_CellDoubleClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPNinProdd.CellDoubleClick
        Dim cabecera As String = ""
        Dim cdx As Integer = e.ColumnIndex
        Dim rdx As Integer = e.RowIndex
        If Not rdx = -1 Or cdx = -1 Then
            cabecera = Me.dgvPNinProdd.Columns(cdx).HeaderText
        End If
        If cabecera = "PN" Then
            TabPage1.Parent = TabControl1
            TabControl1.SelectedTab = TabPage1
            FlagLlenaGrids = 1
            PNSearch = dgvPNinProdd.Rows(e.RowIndex).Cells("PN").Value.ToString()
            LlenaGridPnInCWO()
            CalculateCompare(dgvPNinProdd.Rows(e.RowIndex).Cells("PN").Value.ToString, dgvPNinProdd.Rows(e.RowIndex).Cells("Balance").Value.ToString)
            btnRefrescarMaqPN.Visible = True
            FlagLlenaGrids = If(FlagAux = 2, 2, 0)
        End If
    End Sub
    Private Sub SurtiendoMat()
        Try
            Dim surtir As Integer = 0, prodd As Integer = 0
            For i As Integer = 0 To dgvMatInProdAndSolicitados.Rows.Count - 1
                surtir = CInt(Val(dgvMatInProdAndSolicitados.Rows(i).Cells("Qty a surtir").Value.ToString))
                prodd = CInt(Val(dgvMatInProdAndSolicitados.Rows(i).Cells("Qty en Produccion").Value.ToString))
                If prodd = 0 Then
                    dgvMatInProdAndSolicitados.Rows(i).DefaultCellStyle.ForeColor = Color.Red
                ElseIf prodd > surtir Then
                    dgvMatInProdAndSolicitados.Rows(i).DefaultCellStyle.ForeColor = Color.Green
                ElseIf surtir > prodd Then
                    dgvMatInProdAndSolicitados.Rows(i).DefaultCellStyle.ForeColor = Color.Red
                ElseIf surtir = prodd Then
                    dgvMatInProdAndSolicitados.Rows(i).DefaultCellStyle.ForeColor = Color.Green
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Function UltimoEscaneo(tag As String) As String
        Try
            Dim consulta As String = "select Maq from tblCWO where CWO = (select MAX(WO) from tblBOMWIPRelationsTagsDet where TAG=@tag and WO like 'C%')"
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@tag", SqlDbType.NVarChar).Value = tag
            cnn.Open()
            Dim Maquinausadaxultimavez As String = If(CStr(cmd.ExecuteScalar) = "", "Sin escanear aun", CStr(cmd.ExecuteScalar))
            cnn.Close()
            Return Maquinausadaxultimavez
        Catch ex As Exception
            MsgBox(ex.ToString)
            cnn.Close()
            Return Nothing
        End Try
    End Function
End Class