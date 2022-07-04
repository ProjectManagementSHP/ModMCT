Imports System.Data.SqlClient
Public Class CreatePWO
    Dim AU As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor.Current = Cursors.WaitCursor
        AU = 0
        If dgvDetalleTerminales.Rows.Count > 0 Then dgvDetalleTerminales.DataSource = Nothing
        If TextBox1.Text <> "" Then TextBox1.Clear()
        ChargeGrid()
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub ChargeGrid()
        Try
            'Al query principal, falta agregar que PWO sea nulo o que no tenga PWO
            Dim consulta As String = $"(select distinct TermA [Term],(
        select SUM(TABalance) + SUM(TBBalance) from tblWipDet det
        where ((det.TermA = tblWipDet.TermA and MaqA='MM') or 
        (det.TermB=tblWipDet.TermA and MaqB='MM')) and wip in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 
        {If(AU = 0, " ", $" and AU={AU}")})
        ) [Qty],0 [Test], (
        select case when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermA and AplPn not like 'P%') > 0
        then
        (select top 1 Celda from tblToolCribAplicators where AplPn = (select top 1 AplPn
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermA and AplPn not like 'P%'))
        when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermA and AplPn not like 'P%') = 0
        then
        ((select case when (select count(distinct Substring(Aplicator,1,3)) 
        from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%'
        ) > 1 then 
        (select top 1 Celda from tblToolCribAplicators where AplPn =(select top 1 Substring(Aplicator,1,3) from tblTermSpecs 
        where PN=tblWipDet.TermA and Aplicator not like 'P%')
        )when (
        select count(distinct Substring(Aplicator,1,3)) from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%'
        ) = 1 then(
        select top 1 Celda from tblToolCribAplicators where AplPn =(select distinct Substring(Aplicator,1,3) from tblTermSpecs 
        where PN=tblWipDet.TermA and Aplicator not like 'P%')
        )end))end
        ) [Celda],
        (
        select case when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermA and AplPn not like 'P%') > 0
        then
        (select top 1 AplPn
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermA and AplPn not like 'P%')
        when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermA and AplPn not like 'P%') = 0
        then
        ((select case when (select count(distinct Substring(Aplicator,1,3)) 
        from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%'
        ) > 1 then 
        (select top 1 Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%'
        )when (
        select count(distinct Substring(Aplicator,1,3)) from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%'
        ) = 1 then(
        select distinct Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%'
        )end))end
        ) [Aplicator] 
        from tblWipDet where WIP in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 {If(AU = 0, " ", $" and AU={AU}")}
        ) and MaqA='MM' and TermA like 'T%') 
        union
        (select distinct TermB [Term],(
        select SUM(TABalance) + SUM(TBBalance) from tblWipDet det 
        where ((det.TermA = tblWipDet.TermB and MaqA='MM') or 
        (det.TermB=tblWipDet.TermB and MaqB='MM')) and wip in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 
        {If(AU = 0, " ", $" and AU={AU}")})
        ) [Qty],0 [Test], (
        select case when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermB and AplPn not like 'P%') > 0
        then
        (select top 1 Celda from tblToolCribAplicators where AplPn = (select top 1 AplPn
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermB and AplPn not like 'P%'))
        when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermB and AplPn not like 'P%') = 0
        then
        ((select case when (select count(distinct Substring(Aplicator,1,3)) 
        from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%'
        ) > 1 then 
        (select top 1 Celda from tblToolCribAplicators where AplPn =(select top 1 Substring(Aplicator,1,3) from tblTermSpecs 
        where PN=tblWipDet.TermB and Aplicator not like 'P%')
        )when (
        select count(distinct Substring(Aplicator,1,3)) from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%'
        ) = 1 then(
        select top 1 Celda from tblToolCribAplicators where AplPn =(select distinct Substring(Aplicator,1,3) from tblTermSpecs 
        where PN=tblWipDet.TermB and Aplicator not like 'P%')
        )end))end
        ) [Celda],(
        select case when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermB and AplPn not like 'P%') > 0
        then
        (select top 1 AplPn
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermB and AplPn not like 'P%')
        when (select COUNT(AplPn)
        from tblToolCribAplicators appl inner join tblToolCribAplicatorsRelationsWithTerminals tool 
        on appl.AplPn = tool.Serie where tool.TerminalPN=tblWipDet.TermB and AplPn not like 'P%') = 0
        then
        ((select case when (select count(distinct Substring(Aplicator,1,3)) 
        from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%'
        ) > 1 then 
        (select top 1 Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%'
        )when (
        select count(distinct Substring(Aplicator,1,3)) from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%'
        ) = 1 then(
        select distinct Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%'
        )end))end
        ) [Aplicator] 
        from tblWipDet where WIP in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 {If(AU = 0, " ", $" and AU={AU}")}
        ) and MaqB='MM' and TermB like 'T%')"
            Dim cmd As SqlCommand = New SqlCommand(consulta, cnn)
            Dim dr As SqlDataReader
            Dim aTable As DataTable = New DataTable
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
    Private Sub SelectedTermProcess(PN As String)
        Try
            Dim aConsulta As String = "select AU,WIP,Wire,TermA,TABalance,TermB,TBBalance,0 [Test],'' [Celda],WireID from tblWipDet" &
                                      $" where ((TermA = '{PN}' and MaqA='MM') or (TermB='{PN}' and MaqB='MM')) and" &
                                      $" WIP in (select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 {If(AU = 0, " ", $" and AU={AU}")})" &
                                      "order by IDSort asc"
            Dim cmd As SqlCommand = New SqlCommand(aConsulta, cnn)
            Dim dr As SqlDataReader
            Dim aTable As DataTable = New DataTable
            cnn.Open()
            dr = cmd.ExecuteReader
            aTable.Load(dr)
            cnn.Close()
            If aTable.Rows.Count > 0 Then
                With dgvDetalleTerminales
                    .DataSource = aTable
                    .DataSource = aTable
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    .AutoResizeColumns()
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .ClearSelection()
                End With
                SumQtyAndTest(aTable)
            Else
                dgvDetalleTerminales.DataSource = Nothing
            End If
            Label5.Text = "Records: " + dgvDetalleTerminales.Rows.Count.ToString
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private SumQtyAndTest As Action(Of DataTable) = Function(a)
                                                        Dim Qty = (From row In a.AsEnumerable() Select row("TABalance")).ToList().Sum(Function(i) CInt(i.ToString()))
                                                        Qty += (From row In a.AsEnumerable() Select row("TBBalance")).ToList().Sum(Function(i) CInt(i.ToString()))
                                                        Dim Test = (From row In a.AsEnumerable() Select row("Test")).ToList().Sum(Function(i) CInt(i.ToString()))
                                                        Label3.Text = Qty.ToString
                                                        Label4.Text = Test.ToString
                                                        Return Nothing
                                                    End Function
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Cursor.Current = Cursors.WaitCursor
            If IsNumeric(TextBox1.Text) Then
                AU = TextBox1.Text
                If dgvDetalleTerminales.Rows.Count > 0 Then dgvDetalleTerminales.DataSource = Nothing
                ChargeGrid()
            Else
                MessageBox.Show("El AU ingresado no es valido.")
                dgvTerminalesXProcesar.DataSource = Nothing
            End If
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub dgvTerminalesXProcesar_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTerminalesXProcesar.CellDoubleClick
        If e.RowIndex >= 0 Then
            SelectedTermProcess(dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("Term").Value.ToString)
        End If
    End Sub
End Class