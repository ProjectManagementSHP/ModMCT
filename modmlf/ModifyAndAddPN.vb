Imports System.Text

Public Class ModifyAndAddPN
    Public PN As String
    Protected Friend IDemonChanges As Principal
    Private Sub ModifyAndAddPN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FlagFechas = True
        If tbModify.Visible = True Then lblPn.Text = Me.PN
        If tbNew.Visible = True Then Label9.Text = "Items: " + dgvCortosCompletos.Rows.Count.ToString
        FlagFechas = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor.Current = Cursors.WaitCursor
        If tbModify.Visible And tbNew.Visible = False Then
            If txbNotas.Text = "" And txbNotasModify.Text = "" And dtpFProm.Value = "2021-01-01" Then
                MessageBox.Show("Debe colocar fecha y nota")
            Else
                IDemonChanges = New Principal
                If IDemonChanges.ModificandoPN(dtpFProm.Value, txbNotasModify.Text, cmbPOModify.Text, txbNotas.Text, Me.PN, txbVendorModify.Text, chkParoAU.Checked) Then
                    Dim tb As New DataTable
                    tb = IDemonChanges.GetTable($"select distinct WIP from tblBOMWIP cw where cw.PN = '{Me.PN}' and (cw.WIP in 
(select distinct w.WIP from tblCWO as c inner join tblWipDet as d 
on c.CWO=d.CWO inner join tblWIP as w on w.WIP=d.WIP inner join tblTiemposEstCWO as t on t.CWO=c.CWO 
where (w.WSort = 3 or w.WSort=12 or w.WSort=14 or w.WSort=25) And (c.Wsort in (12,13,14)) 
and (ConfirmacionAlm='OnHold')) or cw.WIP in (select distinct w.WIP from tblPWO as c inner join tblWipDet as d on c.PWO=d.PWOA OR c.PWO=d.PWOB inner join tblWIP as w on w.WIP=d.WIP 
where (((KindOfAU like '[XP]%' and (w.wSort > 32 or w.wSort in (12,14)) or (KindOfAU not like '[XP]%' and (w.wSort > 30 or w.wSort in (12,14)))) And (c.Wsort in (12,14)) and (ConfirmacionAlm='OnHold')))) or Wip in (select WIP from tblWipCortosPN where PN='{Me.PN}')) ")
                    If tb.Rows.Count > 0 Then
                        For Each wip As DataRow In tb.Rows
                            IDemonChanges.NotesInWIP(wip.Item("WIP").ToString, txbNotasModify.Text + " " + txbNotas.Text, dtpFProm.Value.ToString)
                            IDemonChanges.LlenandoNotasCompras(dtpFProm.Value.ToString, txbNotasModify.Text + " " + txbNotas.Text, Me.PN, wip.Item("WIP").ToString)
                        Next
                    End If
                    'Reflejando cambios sin llamar metodo de carga
                    Reflectchanges()
                    MessageBox.Show("Cambios realizados.")
                    Me.Close()
                End If
            End If
        ElseIf tbNew.Visible And tbModify.Visible = False Then
            If txbNewPN.Text = "" And dgvCortosCompletos.RowCount = 0 Then
                MessageBox.Show("Antes de continuar, favor de escribir un numero de parte y rellenar los campos.")
            Else
                If txbNotasNew.Text = "" And dtpAgregando.Value = "2021-01-01" And CheckWips = False Then
                    MessageBox.Show("Debe colocar fecha, nota y seleccionar al menos un WIP.")
                Else
                    If ProcessPN() Then
                        MessageBox.Show($"Numero de parte: {txbNewPN.Text} agregado a cortos.")
                        Principal.ChargeInfoCortos()
                        Me.Close()
                    Else
                        MessageBox.Show($"No se completo el proceso al agregar el numero de parte: {txbNewPN.Text} a cortos, revisalo.")
                    End If
                End If
            End If
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Protected ReadOnly Property CheckWips
        Get
            Dim countSelect As Integer = 0
            For Each row As DataGridViewRow In dgvCortosCompletos.Rows
                countSelect = If(row.Cells(0).Value = True, +1, 0)
            Next
            Return If(countSelect > 0, True, False)
        End Get
    End Property
    Protected Friend Function ProcessPN()
        Try
            IDemonChanges = New Principal
            Dim Wips As New StringBuilder("")
            Dim WipsWithOutCWO As New StringBuilder("")
            Dim Aus As New StringBuilder("")
            Dim CountWipsPass As Integer = 0
            For Each row As DataGridViewRow In dgvCortosCompletos.Rows
                If row.Cells(0).Value Then
                    If CInt(row.Cells(5).Value.ToString) > 2 And CInt(row.Cells(5).Value.ToString) <> 12 Then
                        IDemonChanges.CheckCWOonPN(row.Cells(1).Value.ToString, txbNewPN.Text)
                        IDemonChanges.NotesInWIP(row.Cells("WIP").Value.ToString, txbNotasNew.Text + " " + txtNewRazon.Text, dtpFProm.Value.ToString)
                        IDemonChanges.LlenandoNotasCompras(dtpAgregando.Value.ToString, txbNotasNew.Text + " " + txtNewRazon.Text, txbNewPN.Text, row.Cells("WIP").Value.ToString)
                        CountWipsPass += 1
                    Else
                        Aus.Append($"{row.Cells(2).Value},")
                        WipsWithOutCWO.Append($"'{row.Cells(1).Value}',")
                    End If
                    Wips.Append($"{row.Cells(1).Value},")
                End If
            Next
            If CountWipsPass > 0 Then
                If Not IDemonChanges.CheckCortosPN(txbNewPN.Text, True, chkParoAUNew.Checked) Then
                    IDemonChanges.InsertNew(txbNewPN.Text, dtpAgregando.Value.ToString, cmbPONuevo.Text, txbNuevoVendor.Text, txtNewRazon.Text, txbNotasNew.Text, True, If(chkParoAUNew.Checked, True, False))
                End If
            End If
            If Aus.Length > 0 And WipsWithOutCWO.Length > 0 Then
                IDemonChanges.InsertNew(txbNewPN.Text, dtpAgregando.Value.ToString, cmbPONuevo.Text, txbNuevoVendor.Text, txtNewRazon.Text,
                txbNotasNew.Text, True, chkParoAUNew.Checked, True, WipsWithOutCWO.ToString.TrimEnd(","),
                Aus.ToString.TrimEnd(","))
                Dim WipsClean As String = WipsWithOutCWO.ToString.Replace("'", "")
                WipsClean = WipsClean.ToString.TrimEnd(",", "")
                Dim _WipsClean As String() = WipsClean.Split(",")
                If _WipsClean IsNot Nothing Then
                    For Each _wips In _WipsClean
                        IDemonChanges.NotesInWIP(_wips.ToString, txbNotasNew.Text + " " + txtNewRazon.Text, dtpFProm.Value.ToString)
                        IDemonChanges.LlenandoNotasCompras(dtpAgregando.Value.ToString, txbNotasNew.Text + " " + txtNewRazon.Text, txbNewPN.Text, _wips.ToString)
                    Next
                End If
            End If
            If Wips.Length > 0 Then
                'Aqui colocar codigo para envio de correo de numeros de parte puestos por compras
                Dim mensaje As String = $"Se ha colocado por parte de compras el siguiente numero de parte: {txbNewPN.Text}" + vbNewLine + "Y los WIP's afectados son los siguientes: " + vbNewLine + $"{Wips.ToString.TrimEnd(",").Trim}."
                'EnviaCorreoHoldMatPorCompras(mensaje)
            End If
            Return True
        Catch Exc As Exception
            Return False
        End Try
    End Function
    Protected Friend Sub Reflectchanges()
        Dim tableChanges As DataTable
        tableChanges = (DirectCast(Principal.dgvCortosCompletos.DataSource, DataTable))
        tableChanges.Columns(8).ReadOnly = False 'Fecha Promesa
        tableChanges.Columns(10).ReadOnly = False 'PO
        tableChanges.Columns(10).MaxLength = 50 'PO
        tableChanges.Columns(11).ReadOnly = False 'Vendor
        tableChanges.Columns(11).MaxLength = 100 'Vendor
        tableChanges.Columns(12).ReadOnly = False 'Razon
        tableChanges.Columns(12).MaxLength = 100 'Razon
        tableChanges.Columns(13).ReadOnly = False 'Notas
        tableChanges.Columns(13).MaxLength = 500 'Notas
        tableChanges.Columns(13).ReadOnly = False 'paro au
        For i = 0 To tableChanges.Rows.Count - 1
            If tableChanges.Rows(i).Item("Component PN").ToString.Equals(Me.PN) Then
                tableChanges.Rows(i).Item(8) = dtpFProm.Value.ToString
                tableChanges.Rows(i).Item(10) = cmbPOModify.Text
                tableChanges.Rows(i).Item(11) = txbVendorModify.Text
                tableChanges.Rows(i).Item(12) = txbNotas.Text
                tableChanges.Rows(i).Item(13) = txbNotasModify.Text
                tableChanges.Rows(i).Item(14) = chkParoAU.Checked
                Exit For
            End If
        Next
        tableChanges.AcceptChanges()
        With Principal.dgvCortosCompletos
            .DataSource = tableChanges
            .Columns("Fecha Corto").DefaultCellStyle.Format = ("dd-MMM-yy")
            .Columns("AU Date").DefaultCellStyle.Format = ("dd-MMM-yy")
            .Columns("ETA").DefaultCellStyle.Format = ("dd-MMM-yy")
            .AutoResizeColumns()
        End With
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub txbNewPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txbNewPN.KeyPress
        Cursor.Current = Cursors.WaitCursor
        If e.KeyChar = Chr(13) Then
            Dim tb As New DataTable
            Dim filtro As String = CType(sender, TextBox).Text
            If filtro.Trim() <> String.Empty Then
                If Principal.FiltraPNMaterialGroup(filtro) Then
                    With dgvCortosCompletos
                        tb = Principal.FiltraPN(filtro)
                        .DataSource = tb
                        .AutoResizeColumns()
                    End With
                    cmbPONuevo.DataSource = Principal.CargaComboModificandoPN(filtro)
                    If cmbPONuevo.Text <> "" Then txbNuevoVendor.Text = Principal.CargaVendor(cmbPONuevo.Text.ToString, filtro)
                    Label9.Text = "Items: " + dgvCortosCompletos.Rows.Count.ToString
                    lblQtyTotal.Text = (From row In tb.AsEnumerable() Select row("Cantidad")).ToList() _
                                                                                         .Sum(Function(i) i).ToString
                    lblQtyOnHand.Text = Principal.AllocatedAQty($"select distinct QtyOnHand from tblItemsQB where PN = '{filtro}'")
                Else
                    MessageBox.Show("El numero de parte buscado, no pertenece a Corte o MP, revisalo por favor.", "Busqueda PN", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                If cmbPONuevo.Text <> "" Then cmbPONuevo.Text = ""
                If txbNuevoVendor.Text <> "" Then txbNuevoVendor.Text = ""
                If dgvCortosCompletos.Rows.Count > 0 Then dgvCortosCompletos.DataSource = Nothing
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
    Private Sub dtpAgregando_MouseDown(sender As Object, e As MouseEventArgs) Handles dtpAgregando.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.dtpAgregando.Format = DateTimePickerFormat.Short
            If dtpAgregando.Value = "2021-01-01" Then
                Me.dtpAgregando.Value = Date.Today
            End If
        End If
    End Sub
    Private Sub cmbPOModify_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPOModify.SelectedValueChanged
        txbVendorModify.Text = Principal.CargaVendor(cmbPOModify.Text.ToString, Me.PN)
    End Sub
    Private Sub cmbPONuevo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPONuevo.SelectedValueChanged
        txbNuevoVendor.Text = Principal.CargaVendor(cmbPONuevo.Text.ToString, txbNewPN.Text)
    End Sub
    Private Sub dtpFProm_ValueChanged(sender As Object, e As EventArgs) Handles dtpFProm.ValueChanged
        If FlagFechas = False Then
            If dtpFProm.Value.ToString("dd-MM-yyyy") < Now.ToString("dd-MM-yyyy") Then
                MessageBox.Show("Estas seleccionando una fecha menor a la actual, asegurate de seleccionar una fecha mas reciente.", "Fecha promesa", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub dtpAgregando_ValueChanged(sender As Object, e As EventArgs) Handles dtpAgregando.ValueChanged
        If FlagFechas = False Then
            If dtpAgregando.Value.ToString("dd-MM-yyyy") < Now.ToString("dd-MM-yyyy") Then
                MessageBox.Show("Estas seleccionando una fecha menor a la actual, asegurate de seleccionar una fecha mas reciente.", "Fecha promesa", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
End Class