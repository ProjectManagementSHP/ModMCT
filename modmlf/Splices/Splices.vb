Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection
Imports System.Text
Imports Microsoft.Office.Interop

Public Class Splices

    Private Sub LlenarDataGridSplicesDet(Wip As String, Splice As String)

        Dim query As String = $" SELECT 
                                    WID, 
                                    WIP,
                                    Wirebalance as Qty,
                                    '' as Color1,
                                    '' as Color2,
                                    '' as Color3,
                                    Wire,                                    
                                    LengthWire,
                                    JoinA,
                                    JoinB,
                                    CASE 
                                        WHEN LEFT(PathCWO, CHARINDEX('>', PathCWO) - 1) = 'C' THEN 'Corte'
                                        WHEN LEFT(PathCWO, CHARINDEX('>', PathCWO) - 1) = 'P' THEN 'Prensa'
                                        WHEN LEFT(PathCWO, CHARINDEX('>', PathCWO) - 1) = 'SP' THEN 'SPLICE'
                                        WHEN LEFT(PathCWO, CHARINDEX('>', PathCWO) - 1) = 'SA' THEN 'Subalmacen'
                                    END AS Area, PathCWO as Path,
	                                '' as Pagoda, C.porcentaje as Atados
                                FROM 
                                    tblCWOSerialNumbers
                                Cross apply( SELECT 
                                                CONCAT(ROUND((SUM(A.qty) * 100 / tblCWOSerialNumbers.WireBalance), 2), '%') as porcentaje
                                            FROM tblatados A 
                                            WHERE 
                                                A.IDCWO = tblCWOSerialNumbers.IDCWO 
                                                AND A.CellSpliceReceived = 1) C
                                WHERE 
                                    wip LIKE '%{Wip}%'
	                                and (joinA like '%{Splice}%' or joinB like '%{Splice}%')
                                                            ;"

        cnn.Open()
        Using cmd As New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            Dim tb As New DataTable
            Dim dr As SqlDataReader
            cmd.CommandTimeout = 1800000
            dr = cmd.ExecuteReader
            tb.Load(dr)
            dgvSplicesDet.DataSource = tb

        End Using
        cnn.Close()

        RecorrerdgvSplicesDet()
        StyleDgvWIP()
    End Sub
    Private Sub LlenarDataGridSplices(valor As String)

        Dim query As String = $"SELECT 
    WIP,
    Qty, 
    SPLICE_CLEANED,
    COUNT(SPLICE_CLEANED) AS CKTS,
    '' AS FALTA
FROM (
    SELECT 
        A.WIP, 
        B.Qty, 
        CASE 
            WHEN CHARINDEX('(', A.SPLICES) > 0 THEN SUBSTRING(A.SPLICES, 1, CHARINDEX('(', A.SPLICES) - 1) 
            ELSE A.SPLICES 
        END AS SPLICE_CLEANED
    FROM (
        SELECT JoinA AS SPLICES, wip FROM tblwipdet WHERE wip LIKE '%{valor}%' AND JoinA LIKE '%SP%'
        UNION ALL 
        SELECT JoinB AS SPLICES, wip FROM tblwipdet WHERE wip LIKE '%{valor}%' AND JoinB LIKE '%SP%'
    ) AS A
    INNER JOIN tblwip B ON A.wip = B.wip
    WHERE B.status = 'Open' AND A.wip LIKE '%{valor}%' 
) AS T
GROUP BY WIP, Qty, SPLICE_CLEANED
ORDER BY 
    CASE 
        WHEN ISNUMERIC(SUBSTRING(SPLICE_CLEANED, 3, LEN(SPLICE_CLEANED))) = 1 THEN 0 
        ELSE 1 
    END,
    PATINDEX('%[^0-9]%', SPLICE_CLEANED),
    CAST(SUBSTRING(SPLICE_CLEANED, 3, LEN(SPLICE_CLEANED)) AS INT);"

        cnn.Open()
        Using cmd As New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            Dim tb As New DataTable
            Dim dr As SqlDataReader
            cmd.CommandTimeout = 1800000
            dr = cmd.ExecuteReader
            tb.Load(dr)
            dgvSplices.DataSource = tb

        End Using
        cnn.Close()
    End Sub

    Private Sub txtDato_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDato.KeyPress
        If e.KeyChar = Chr(13) Then
            LlenarDataGridSplices(txtDato.Text)
            LlenarDataCmbSplices(txtDato.Text)
        End If

    End Sub

    Private Sub dgvSplices_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSplices.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim Wip As String = dgvSplices.Rows(e.RowIndex).Cells(0).Value.ToString()
            Dim Splice As String = dgvSplices.Rows(e.RowIndex).Cells(2).Value.ToString()


            LlenarDataGridSplicesDet(Wip, Splice)

        End If
    End Sub

    Private Sub PintarCeldaGrid(row As Integer, color As String, columna As String)
        Select Case color
            Case "1"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.White

            Case "2"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Yellow
            Case "3"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Orange
            Case "4"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Pink
            Case "5"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Purple
            Case "6"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Red
            Case "7"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Tan
            Case "8"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Black
            Case "9"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.Brown
            Case "A"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.DarkBlue
            Case "B"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.LightBlue
            Case "C"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.MediumBlue
            Case "D"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.DarkGreen
            Case "E"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.LightGreen
            Case "F"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.DarkGray
            Case "G"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.MediumPurple
            Case "H"
                dgvSplicesDet.Rows(row).Cells(columna).Style.BackColor = System.Drawing.Color.LightGray
        End Select

    End Sub

    Private Sub RecorrerdgvSplicesDet()
        Dim renglon As Integer = 0
        For Each row As DataGridViewRow In dgvSplicesDet.Rows
            ' Verificar si la fila no está vacía
            If Not row.IsNewRow Then
                ' Obtener el valor de la columna "Wire" en esta fila
                Dim wireValue As String = row.Cells("Wire").Value.ToString()

                ' Verificar si el valor de "Wire" tiene al menos 6 caracteres
                If wireValue.Length >= 6 Then
                    ' Omitir los primeros 5 caracteres
                    Dim substringAfter5Chars As String = wireValue.Substring(5)

                    ' Obtener el caracter número 6
                    Dim ColorA As Char = wireValue(5)

                    ' Buscar si hay un guion después del quinto carácter
                    Dim indexOfDash As Integer = substringAfter5Chars.IndexOf("-")

                    ' Variable para almacenar el resultado
                    Dim ColorB As String = ""

                    ' Si hay un guion después del quinto carácter
                    If indexOfDash >= 0 Then
                        ' Obtener el caracter después del guion
                        Dim charAfterDash As Char = substringAfter5Chars(indexOfDash + 1)
                        ' Almacenar el resultado
                        ColorB = charAfterDash
                    Else
                        ' Si no hay guion, asignar el sexto carácter como resultado
                        ColorB = ColorA
                    End If

                    PintarCeldaGrid(renglon, ColorA, "Color1")
                    PintarCeldaGrid(renglon, ColorB, "Color2")
                    PintarCeldaGrid(renglon, ColorA, "Color3")
                End If
            End If


            renglon += 1
        Next

    End Sub

    Private Sub StyleDgvWIP()
        Dim rowCount As Integer = dgvSplicesDet.Rows.Count
        lblCountRows.Text = rowCount.ToString()
        dgvSplicesDet.AutoResizeColumns()
        dgvSplicesDet.Columns("WIP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvSplicesDet.Columns("WIP").Width = 150
        dgvSplicesDet.Columns("Color1").Width = 15
        dgvSplicesDet.Columns("Color2").Width = 15
        dgvSplicesDet.Columns("Color3").Width = 15





    End Sub
    Private Sub dgvSplicesDet_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvSplicesDet.CellPainting
        ' Verificar si se está pintando una celda de encabezado y si es la columna específica
        If e.RowIndex = -1 AndAlso e.ColumnIndex >= 0 AndAlso dgvSplicesDet.Columns(e.ColumnIndex).Name = "Color1" Then
            ' Dibujar el fondo del encabezado de la columna sin las líneas divisorias
            e.PaintBackground(e.CellBounds, True)

            ' Dibujar el texto del encabezado de la columna
            Using brush As New SolidBrush(e.CellStyle.ForeColor)
                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, brush, e.CellBounds.X + 2, e.CellBounds.Y + 2)
            End Using

            ' Indicar que ya se ha pintado la celda y evitar el procesamiento estándar
            e.Handled = True
        End If
    End Sub

    Private Sub LlenarDataCmbSplices(valor As String)
        Dim query As String = $"SELECT SPLICE_CLEANED
FROM (
    SELECT 
        A.WIP, 
        B.Qty, 
        CASE 
            WHEN CHARINDEX('(', A.SPLICES) > 0 THEN SUBSTRING(A.SPLICES, 1, CHARINDEX('(', A.SPLICES) - 1) 
            ELSE A.SPLICES 
        END AS SPLICE_CLEANED
    FROM (
        SELECT JoinA AS SPLICES, wip FROM tblwipdet WHERE wip LIKE '%{valor}%' AND JoinA LIKE '%SP%'
        UNION ALL 
        SELECT JoinB AS SPLICES, wip FROM tblwipdet WHERE wip LIKE '%{valor}%' AND JoinB LIKE '%SP%'
    ) AS A
    INNER JOIN tblwip B ON A.wip = B.wip
    WHERE B.status = 'Open' AND A.wip LIKE '%{valor}%' 
) AS T
GROUP BY WIP, Qty, SPLICE_CLEANED
ORDER BY 
    CASE 
        WHEN ISNUMERIC(SUBSTRING(SPLICE_CLEANED, 3, LEN(SPLICE_CLEANED))) = 1 THEN 0 
        ELSE 1 
    END,
    PATINDEX('%[^0-9]%', SPLICE_CLEANED),
    CAST(SUBSTRING(SPLICE_CLEANED, 3, LEN(SPLICE_CLEANED)) AS INT);"

        cnn.Open()
        Using cmd As New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            Dim tb As New DataTable
            Dim dr As SqlDataReader
            cmd.CommandTimeout = 1800000
            dr = cmd.ExecuteReader
            tb.Load(dr)


            ' Llenar ComboBox cmbSplices
            cmbSplices.Items.Clear() ' Limpiar items previos
            For Each row As DataRow In tb.Rows
                cmbSplices.Items.Add(row("SPLICE_CLEANED").ToString())
            Next
        End Using
        cnn.Close()
    End Sub

    Private Sub cmbSplices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSplices.SelectedIndexChanged
        Dim Wip As String = txtDato.Text
        LlenarDataGridSplicesDet(Wip, cmbSplices.Text)
    End Sub


    Private Sub LlenarDataGridAtados(wip As String, wid As String)

        Dim query As String = $"select idAtado, IDCWO,Qty,Route,ReceivedBy, CellSpliceA, CellSpliceReceived,CellSpliceB from tblatados where IDCWO=(select top 1 IDCWO from tblcwoserialnumbers where wip='{wip}' and wid={wid}) order by CellSpliceReceived DESC"
        cnn.Open()
        Using cmd As New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            Dim tb As New DataTable
            Dim dr As SqlDataReader
            cmd.CommandTimeout = 1800000
            dr = cmd.ExecuteReader
            tb.Load(dr)
            dgvAtados.DataSource = tb

        End Using
        cnn.Close()
    End Sub

    Private Sub dgvSplicesDet_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSplicesDet.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim wip As String = dgvSplicesDet.Rows(e.RowIndex).Cells(1).Value.ToString()
            Dim wid As String = dgvSplicesDet.Rows(e.RowIndex).Cells(0).Value.ToString()



            LlenarDataGridAtados(wip, wid)

        End If
    End Sub
End Class