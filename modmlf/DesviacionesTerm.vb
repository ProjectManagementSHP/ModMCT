Imports System.Data.SqlClient
Imports System.Text

Public Class DesviacionesTerm
    Private Sub DesviacionesTerm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListTermForWIP()
    End Sub
    Private Sub ListTermForWIP()
        Try
            Dim tabla As New DataTable()
            Dim query As String = $"select PN [Terminales] from tblBOMWIP where WIP='{WIP.Text}' and PN like 'T%' and Balance > 0"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            cnn.Close()
            If tabla.Rows.Count > 0 Then
                With dgvTerminales
                    .DataSource = tabla
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
            End If
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub CheckTermWithValidation(CheckTerm As String)
        Try
            Dim tabla As New DataTable()
            Dim query As String = $"SELECT DISTINCT PnValido [Terminales] FROM tblDesviacionesTerm a inner join tblItemsQB b on a.PnValido=b.PN where PnOriginal = '{CheckTerm}' and QtyOnHand > 0"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            cnn.Close()
            If tabla.Rows.Count > 0 Then
                With dgvTerminalesValidadas
                    .DataSource = tabla
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
            Else
                dgvTerminalesValidadas.DataSource = Nothing
            End If
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub processTerminal()
        Dim actualterm As String = CurrentTerminal.Text.Trim()
        Dim newTerm As String = TerminalNew.Text.Trim()
        Dim oAwg As StringBuilder = New StringBuilder("")
        Dim getDescription As String = GetPnDescripton(newTerm)
        Dim tblwipdet As DataTable = New DataTable()
        tblwipdet = LoadWipDet(WIP.Text, actualterm)
        If LoadDesviacionesTerm(actualterm, newTerm) Then
            Dim CountValidation As Integer = 0
            Dim Wires = (From row In tblwipdet.AsEnumerable() Select row("Wire")).[Select](Function(w) w.ToString.Substring(0, 5)).Distinct.ToList()
            Wires.ForEach(
                Function(AWG)
                    If loadtermSpecs(newTerm, AWG.ToString) Then
                        CountValidation += 1
                    Else
                        oAwg.Append($"{AWG.ToString},")
                    End If
                    Return Nothing
                End Function
                         )
            If CountValidation = Wires.Count() Then
                UpdateTblWipDet(WIP.Text, actualterm, newTerm, "A")
                UpdateTblWipDet(WIP.Text, actualterm, newTerm, "B")
                UpdateTblCWOSerialNumbers(WIP.Text, actualterm, newTerm, "A")
                UpdateTblCWOSerialNumbers(WIP.Text, actualterm, newTerm, "B")
                UpdateTblWipDetIDKey(WIP.Text, newTerm, "A")
                UpdateTblWipDetIDKey(WIP.Text, newTerm, "B")
                UpdateBomWip(WIP.Text, actualterm, newTerm, getDescription)
                Dim CWOs = (From row In tblwipdet.AsEnumerable() Select row("CWO")).Distinct().ToList()
                If CWOs IsNot Nothing Then
                    CWOs.ForEach(
                    Function(aCWO)
                        UpdateBomCWO(aCWO, actualterm, newTerm, getDescription)
                        Return Nothing
                    End Function
                    )
                End If
                Dim PWOA = (From row In tblwipdet.AsEnumerable() Select row("PWOA")).Distinct().ToList()
                If Not IsDBNull(PWOA) Then
                    PWOA.ForEach(
                        Function(PA)
                            If PA <> "N/A" Then UpdateBomPWO(PA, actualterm, newTerm, getDescription)
                            Return Nothing
                        End Function
                                 )
                End If
                Dim PWOB = (From row In tblwipdet.AsEnumerable() Select row("PWOB")).Distinct().ToList()
                If Not IsDBNull(PWOB) Then
                    PWOB.ForEach(Function(PB)
                                     If PB <> "N/A" Then UpdateBomPWO(PB, actualterm, newTerm, getDescription)
                                     Return Nothing
                                 End Function
                                 )
                End If
                UpdateWipCord(WIP.Text, actualterm, newTerm)
                RegistraCambio(WIP.Text, newTerm, actualterm)
                MessageBox.Show("El Proceso a terminado exitosamente")
                SendNotify("'aplicadores','Compras','corte'", 10)
                Me.Dispose()
                Me.Close()
            Else
                MessageBox.Show($"Los calibres {oAwg.ToString.TrimEnd(",")} para esta terminal {newTerm} no estan validados, por lo tanto no se ha podido realizar la desviacion, contacte al area de aplicadores e ingenieria, ya fueron reportados.")
                SendNotify("'aplicadores'", 9)
            End If
        Else
            MessageBox.Show("No hay validaciones existentes, pruebe con otra terminal")
        End If
    End Sub
    Private Sub RegistraCambio(Wip As String, termNew As String, termOld As String)
        Try
            Dim insertNew As String = $"Insert into tblProcesoDesviaciones (WIP,CreatedChange,TerminalNueva,TerminalAnterior,Usuario) values ('{Wip}',GETDATE(),'{termNew}','{termOld}','{Environment.UserName}')"
            Dim command As SqlCommand = New SqlCommand(insertNew, cnn)
            command.CommandType = CommandType.Text
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Function GetPnDescripton(PN As String) As String
        Try
            Dim NumeroDeparte As String = ""
            Dim query As String = "SELECT TOP(1) Description FROM tblItemsQB WHERE PN = @PN"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@PN", SqlDbType.VarChar).Value = PN
            cnn.Open()
            NumeroDeparte = CStr(command.ExecuteScalar())
            cnn.Close()
            Return NumeroDeparte
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message, "Error")
            Return ""
        End Try
    End Function
    Private Function LoadDesviacionesTerm(Term As String, newTerm As String) As Boolean
        Try
            Dim query As String = "SELECT Count(*) FROM tblDesviacionesTerm where PnOriginal = @term and PnValido = @newTerm"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@term", SqlDbType.VarChar).Value = Term
            command.Parameters.Add("@newTerm", SqlDbType.VarChar).Value = newTerm
            cnn.Open()
            If CInt(command.ExecuteScalar()) > 0 Then
                cnn.Close()
                Return True
            Else
                cnn.Close()
                Return False
            End If
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message, "Error")
            Return False
        End Try
        Return False
    End Function
    Private Function LoadWipDet(Wips As String, Term As String) As DataTable
        Dim TblwipDet As New DataTable
        Try
            Dim query As String = "SELECT Wire, TermA, TermB, isNULL(PWOA,'') AS PWOA, ISNULL(PWOB, '') as PWOB, CWO FROM Tblwipdet WHERE WIP=@WIP AND (TermA=@Term OR TermB=@Term) order by wire,terma,termb"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            Dim dr As SqlDataReader
            command.Parameters.Add("@WIP", SqlDbType.VarChar).Value = Wips
            command.Parameters.Add("@Term", SqlDbType.VarChar).Value = Term
            cnn.Open()
            dr = command.ExecuteReader
            TblwipDet.Load(dr)
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message, "Error")
        End Try
        Return TblwipDet
    End Function
    Private Function loadtermSpecs(newTerminal As String, wire As String) As Boolean
        Try
            Dim query As String = "SELECT IsNull(COUNT(IDKey),0) FROM tblTermSpecs WHERE (PN=@PN AND WTAWG = @Wire) AND (PTA = 1 OR (TemporalApprovedDate > CONVERT(date,GETDATE()) AND TemporalApproved = 1))"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@PN", SqlDbType.NVarChar).Value = newTerminal
            command.Parameters.Add("@Wire", SqlDbType.NVarChar).Value = wire
            cnn.Open()
            If CInt(command.ExecuteScalar) > 0 Then
                cnn.Close()
                Return True
            End If
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message, "Error")
        End Try
        Return False
    End Function
    Private Sub UpdateTblWipDet(ByVal wip As String, ByVal ActualTerm As String, ByVal NewTerm As String, ByVal side As String)
        Try
            Dim query As String = $"UPDATE tblWipDet SET Term{side}=@NewTerm WHERE WIP=@WIP AND Term{side}=@ActualTerm AND T{side}Balance > 0"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@ActualTerm", SqlDbType.NVarChar).Value = ActualTerm
            command.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = wip
            command.Parameters.Add("@NewTerm", SqlDbType.NVarChar).Value = NewTerm
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Sub UpdateTblCWOSerialNumbers(ByVal wip As String, ByVal ActualTerm As String, ByVal NewTerm As String, ByVal side As String)
        Try
            Dim query As String = $"UPDATE tblCWOSerialNumbers SET Term{side} = @NewTerm + (SELECT CASE WHEN Maq{side} = 'MA' THEN '  /MA' ELSE '' END AS MAQ FROM tblWipDet WHERE tblWipDet.WireID = tblCWOSerialNumbers.WireID) WHERE WIP = @WIP and (Term{side} LIKE @ActualTerm + '%') AND Cutting IS NOT Null"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@ActualTerm", SqlDbType.NVarChar).Value = ActualTerm
            command.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = wip
            command.Parameters.Add("@NewTerm", SqlDbType.NVarChar).Value = NewTerm
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Sub UpdateTblWipDetIDKey(ByVal wip As String, ByVal NewTerm As String, ByVal side As String)
        Try
            Dim query As String = $"update tblWipDet set IDKey{side} = (select top 1 IDKey from tblTermSpecs where PN = tblWipDet.Term{side} and WTAWG = SUBSTRING(tblWipDet.Wire,1,5)) WHERE WIP = @WIP and Term{side} = @NewTerm"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = wip
            command.Parameters.Add("@NewTerm", SqlDbType.NVarChar).Value = NewTerm
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Sub UpdateBomWip(wip As String, ActualTerm As String, newTerm As String, description As String)
        Try
            Dim query As String = "UPDATE tblBOMWIP SET PN=@Newterm, Description=@Descrip WHERE WIP=@WIP AND PN=@ActualTerm"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@ActualTerm", SqlDbType.NVarChar).Value = ActualTerm
            command.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = wip
            command.Parameters.Add("@NewTerm", SqlDbType.NVarChar).Value = newTerm
            command.Parameters.Add("@Descrip", SqlDbType.NVarChar).Value = description
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Sub UpdateBomCWO(CWO As String, ActualTerm As String, newTerm As String, description As String)
        Try
            Dim query As String = "UPDATE tblBOMCWO SET PN=@Newterm, Description=@Descrip WHERE CWO=@CWO AND PN=@ActualTerm"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@ActualTerm", SqlDbType.NVarChar).Value = ActualTerm
            command.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = CWO
            command.Parameters.Add("@NewTerm", SqlDbType.NVarChar).Value = newTerm
            command.Parameters.Add("@Descrip", SqlDbType.NVarChar).Value = description
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Sub UpdateBomPWO(PWO As String, ActualTerm As String, newTerm As String, description As String)
        Try
            Dim query As String = "UPDATE tblBOMPWO SET PN=@Newterm, Description=@Descrip WHERE PWO=@PWO AND PN=@ActualTerm"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@ActualTerm", SqlDbType.NVarChar).Value = ActualTerm
            command.Parameters.Add("@PWO", SqlDbType.NVarChar).Value = PWO
            command.Parameters.Add("@NewTerm", SqlDbType.NVarChar).Value = newTerm
            command.Parameters.Add("@Descrip", SqlDbType.NVarChar).Value = description
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Sub UpdateWipCord(wip As String, ActualTerm As String, newTerm As String)
        Try
            Dim query As String = "UPDATE tblWipCord SET PN=@NewTerm WHERE WIP=@WIP AND PN=@ActualTerm"
            Dim command As SqlCommand = New SqlCommand(query, cnn)
            command.CommandType = CommandType.Text
            command.Parameters.Add("@ActualTerm", SqlDbType.NVarChar).Value = ActualTerm
            command.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = wip
            command.Parameters.Add("@NewTerm", SqlDbType.NVarChar).Value = newTerm
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Public Sub SendNotify(SendTo As String, TypeOfNotify As Integer)
        Try
            Using cmd As New SqlCommand($"update tblMLFNotifications set SendReceive=1,TypeOfNotify={TypeOfNotify} where Dep in ({SendTo})", cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
            End Using
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString, "Error")
        End Try
    End Sub
    Private Function CheckValue(Grid As Integer)
        Try
            Dim count As Integer = 0
            If Grid = 1 Then
                For i As Integer = 0 To dgvTerminales.Rows.Count - 1
                    If dgvTerminales.Rows(i).Cells("Chk").Value = True Then
                        count += 1
                    End If
                Next
            ElseIf Grid = 2 Then
                For i As Integer = 0 To dgvTerminalesValidadas.Rows.Count - 1
                    If dgvTerminalesValidadas.Rows(i).Cells("Check").Value = True Then
                        count += 1
                    End If
                Next
            End If
            Return count
        Catch ex As Exception
            Return Nothing
        End Try
        Return Nothing
    End Function
    Private Sub CheckTable(Current As Integer, Grid As Integer)
        Try
            If Grid = 1 Then
                For i As Integer = 0 To dgvTerminales.Rows.Count - 1
                    If i <> Current Then
                        dgvTerminales.Rows(i).Cells("Chk").Value = False
                    End If
                Next
            ElseIf Grid = 2 Then
                For i As Integer = 0 To dgvTerminalesValidadas.Rows.Count - 1
                    If i <> Current Then
                        dgvTerminalesValidadas.Rows(i).Cells("Check").Value = False
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub dgvTerminales_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTerminales.CellClick
        Dim count As Integer
        If e.ColumnIndex = 0 Then
            count = CheckValue(1)
            If count = 0 Then
                CurrentTerminal.Text = dgvTerminales.Rows(e.RowIndex).Cells("Terminales").Value.ToString
                CheckTermWithValidation(dgvTerminales.Rows(e.RowIndex).Cells("Terminales").Value.ToString)
            Else
                Dim current As Integer = e.RowIndex
                CheckTable(current, 1)
                CurrentTerminal.Text = dgvTerminales.Rows(e.RowIndex).Cells("Terminales").Value.ToString
                CheckTermWithValidation(dgvTerminales.Rows(e.RowIndex).Cells("Terminales").Value.ToString)
            End If
            If TerminalNew.Text <> "-" Then
                TerminalNew.Text = "-"
                dgvTerminalesValidadas.DataSource = Nothing
            End If
        End If
    End Sub
    Private Sub dgvTerminalesValidadas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTerminalesValidadas.CellClick
        If e.ColumnIndex = 0 Then
            Dim count As Integer = CheckValue(2)
            If count = 0 Then
                TerminalNew.Text = dgvTerminalesValidadas.Rows(e.RowIndex).Cells("Terminales").Value.ToString
            Else
                Dim current As Integer = e.RowIndex
                CheckTable(current, 2)
                TerminalNew.Text = dgvTerminalesValidadas.Rows(e.RowIndex).Cells("Terminales").Value.ToString
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TerminalNew.Text <> "-" And CurrentTerminal.Text <> "-" Then
            processTerminal()
        Else
            MessageBox.Show("Seleccione la terminal a Desviar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class