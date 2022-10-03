Public Class Hold
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Cursor.Current = Cursors.WaitCursor
        If opcion = 5 Then
            Dim lst As New List(Of String)
            If dtpFProm.Value = "2021-01-01" Then
                MsgBox("Debe agregar fecha promesa", MessageBoxIcon.Warning)
                Me.Close()
            Else
                With Principal
                    Me.Hide()
                    For jj = 0 To .dgvMatSinStockCompras.Rows.Count - 1
                        If .dgvMatSinStockCompras.Rows(jj).Cells("Chk").Value = True Then
                            Dim pn = .dgvMatSinStockCompras.Rows(jj).Cells("ComponentPN").Value.ToString
                            .Notas(Principal.dgvMatSinStockCompras.Rows(jj).Cells("WO").Value.ToString, dtpFProm.Text, txbNotas.Text, .dgvMatSinStockCompras.Rows(jj).Cells("WIP").Value.ToString, pn)
                            .tblasig.Columns(18).ReadOnly = False
                            .tblasig.Rows(jj).Item(18) = Convert.ToDateTime(dtpFProm.Text.ToString)
                            If lst IsNot Nothing Then
                                If lst.Where(Function(a) a.Equals(pn.ToString)).Count = 0 Then
                                    lst.Add(.dgvMatSinStockCompras.Rows(jj).Cells("ComponentPN").Value.ToString)
                                End If
                            End If
                        End If
                    Next
                    .dgvMatSinStockCompras.DataSource = .tblasig
                    .dgvMatSinStockCompras.Columns("NextFReciboMat").DefaultCellStyle.Format = ("dd-MMM-yy")
                    .dgvMatSinStockCompras.Columns("Fecha promesa PN").DefaultCellStyle.Format = ("dd-MMM-yy")
                    .dgvMatSinStockCompras.AutoResizeColumns() 'ajustamos el tamaño de las columnas
                    .dgvMatSinStockCompras.Columns(4).Width = 5
                    .dgvMatSinStockCompras.Columns(8).Width = 5
                    .dgvMatSinStockCompras.Columns(1).Frozen = True
                    If opcion = 5 Then
                        .dgvMatSinStockCompras.Columns("Chk").Visible = True
                        .btnAgregaFecha.Visible = True
                    Else
                        .dgvMatSinStockCompras.Columns("Chk").Visible = False
                    End If
                    .dgvMatSinStockCompras.ClearSelection()
                    .PintarMateriales()
                    .lblitemscortos.Text = "Items: " + .dgvMatSinStockCompras.Rows.Count.ToString
                    .btnexportaeficc.Visible = True
                    ' ----------- Notificacion de que esta listo
                    .NotifyIcon1.BalloonTipText = "Se han notificado los cambios"
                    .NotifyIcon1.BalloonTipTitle = "Fecha promesa"
                    .NotifyIcon1.Visible = True
                    .NotifyIcon1.ShowBalloonTip(0)
                    lst.ForEach(Function(pn) Principal.CheckCortosPN(pn, True))
                    .Filtros(6)
                    For j = 0 To .dgvMatSinStockCompras.Rows.Count - 1
                        If .dgvMatSinStockCompras.Rows(j).Cells("Chk").Value Then
                            .dgvMatSinStockCompras.Rows(j).Cells("Chk").Value = False
                        End If
                    Next
                End With
                MessageBox.Show("Actualizados los cambios")
                Me.Close()
            End If
        ElseIf opcion = 3 Then
            If dtpFProm.Value = "2021-01-01" Then
                MsgBox("Debe agregar fecha promesa", MessageBoxIcon.Warning)
            Else
                Principal.NotesWIPandCWOOnHold(lblcwoporsolicitar.Text, dtpFProm.Text, txbNotas.Text)
                Me.Close()
            End If
        ElseIf opcion = 2 Then
            If txbNotas.Text <> "" Then
                Dim mensaje As String = "", Wips As String = "", CWos As String = "", AuxWip As String = "", AuxCwo As String = ""
                With Principal
                    Dim lst As New List(Of String)
                    For o As Integer = 0 To .dgvAfectados.Rows.Count - 1
                        If .dgvAfectados.Rows(o).Cells("aChk").Value Then
                            If .CheckWSort(.dgvAfectados.Rows(o).Cells("WO").Value.ToString) Then
                                If AuxWip = "" And AuxCwo = "" Then
                                    AuxCwo = .dgvAfectados.Rows(o).Cells("WO").Value.ToString
                                    AuxWip = .dgvAfectados.Rows(o).Cells("WIP").Value.ToString
                                    Wips += .dgvAfectados.Rows(o).Cells("WIP").Value.ToString + ", "
                                    CWos += .dgvAfectados.Rows(o).Cells("WO").Value.ToString + ", "
                                Else
                                    If AuxCwo = .dgvAfectados.Rows(o).Cells("WO").Value.ToString Then
                                        AuxCwo = .dgvAfectados.Rows(o).Cells("WO").Value.ToString
                                    Else
                                        AuxCwo = .dgvAfectados.Rows(o).Cells("WO").Value.ToString
                                        CWos += .dgvAfectados.Rows(o).Cells("WO").Value.ToString + ", "
                                    End If
                                    If AuxWip = .dgvAfectados.Rows(o).Cells("WIP").Value.ToString Then
                                        AuxWip = .dgvAfectados.Rows(o).Cells("WIP").Value.ToString
                                        Wips += .dgvAfectados.Rows(o).Cells("WIP").Value.ToString + ", "
                                    Else
                                        AuxWip = .dgvAfectados.Rows(o).Cells("WIP").Value.ToString
                                    End If
                                End If
                                Materiales.UpdateHoldPN(.dgvAfectados.Rows(o).Cells("WO").Value.ToString, .dgvAfectados.Rows(o).Cells("PN").Value.ToString)
                                lst.Add(.dgvAfectados.Rows(o).Cells("PN").Value.ToString)
                                If mensaje = "" Then
                                    If Materiales.CheckMovNegative(.dgvAfectados.Rows(o).Cells("PN").Value.ToString, .dgvAfectados.Rows(o).Cells("Qty").Value.ToString) Then
                                        mensaje = .dgvAfectados.Rows(o).Cells("PN").Value.ToString & " (Ajustes Neg)"
                                    Else
                                        mensaje = .dgvAfectados.Rows(o).Cells("PN").Value.ToString
                                    End If
                                ElseIf mensaje <> "" Then
                                    If Materiales.CheckMovNegative(.dgvAfectados.Rows(o).Cells("PN").Value.ToString, .dgvAfectados.Rows(o).Cells("Qty").Value.ToString) Then
                                        mensaje = mensaje + "; " + .dgvAfectados.Rows(o).Cells("PN").Value.ToString & " (Ajustes Neg)"
                                    Else
                                        mensaje = mensaje + "; " + .dgvAfectados.Rows(o).Cells("PN").Value.ToString
                                    End If
                                End If
                                WIP = .dgvAfectados.Rows(o).Cells("WIP").Value.ToString
                                .NotesWIPandCWOOnHold(.dgvAfectados.Rows(o).Cells("WO").Value.ToString, Convert.ToDateTime(Now), txbNotas.Text)
                            Else
                                MessageBox.Show($"Este WO: { .dgvAfectados.Rows(o).Cells("WO").Value} aun no es requerido por corte.")
                            End If
                        End If
                    Next
                    lst.ForEach(Function(pn)
                                    Principal.CheckCortosPN(pn)
                                    Return Nothing
                                End Function)
                    mensaje = mensaje + " se han puesto en Cortos por falta de material, en los WIP: " & Wips & " y WO: " & CWos & " por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + "" + vbNewLine + " Verificalo y asigna una nueva fecha de material."
                    'CorreoFalla.EnviaCorreoHoldMat(mensaje) 'Descomentar cuando se suba
                    .NotifyIcon1.BalloonTipText = "Se han notificado los cambios a Compras"
                    .NotifyIcon1.BalloonTipTitle = "Material sin stock"
                    .NotifyIcon1.Visible = True
                    .NotifyIcon1.ShowBalloonTip(0)
                    .dgvAfectados.DataSource = Nothing
                    .btnCortosPN.Visible = False
                End With
                Me.Close()
            Else
                MsgBox("Debe agregar nota", MessageBoxIcon.Warning)
            End If
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub dtpFProm_MouseDown(sender As Object, e As MouseEventArgs) Handles dtpFProm.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.dtpFProm.Format = DateTimePickerFormat.Short
            If dtpFProm.Value = "2021-01-01" Then
                Me.dtpFProm.Value = Date.Today
            End If
        End If
    End Sub
    Private Sub Hold_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txbNotas.Text = ""
        If flag = 12 Then dtpFProm.Value = "2022-12-01"
        dtpFProm.Value = "2021-01-01"
        If opcion = 2 Then
            btnGuardar.BackColor = Color.LightBlue
        ElseIf opcion = 3 Then
            btnGuardar.BackColor = Color.LightGray
        End If
    End Sub
End Class