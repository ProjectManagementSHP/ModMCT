Imports System.Data.SqlClient
Public Class CreatePWO
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ChargeGrid()
    End Sub
    Private Sub ChargeGrid()
        Try
            Dim consulta As String = "(select distinct TermA [Term],(
        select Sum(Balance) from tblbomwip where wip in (select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30)
        and pn =tblWipDet.TermA
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
        (select top 1 Celda from tblToolCribAplicators where AplPn =(select top 1 Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%')
        )when (
        select count(distinct Substring(Aplicator,1,3)) from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%'
        ) = 1 then(
        select top 1 Celda from tblToolCribAplicators where AplPn =(select distinct Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermA and Aplicator not like 'P%')
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
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30
        ) and MaqA='MM' and TermA like 'T%') 
        union
        (select distinct TermB [Term],(
        select Sum(Balance) from tblbomwip where wip in (select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30)
        and pn =tblWipDet.TermB
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
        (select top 1 Celda from tblToolCribAplicators where AplPn =(select top 1 Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%')
        )when (
        select count(distinct Substring(Aplicator,1,3)) from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%'
        ) = 1 then(
        select top 1 Celda from tblToolCribAplicators where AplPn =(select distinct Substring(Aplicator,1,3) from tblTermSpecs where PN=tblWipDet.TermB and Aplicator not like 'P%')
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
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30
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
                End With
            Else
                dgvTerminalesXProcesar.DataSource = Nothing
                MessageBox.Show("No hay terminales por procesar.")
            End If
            Label6.Text = "Records: " + dgvTerminalesXProcesar.Rows.Count
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
End Class