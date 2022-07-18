Imports System.Data.SqlClient
Public Class CreatePWO
    Dim AU As Integer = 0
    Dim InfoTablas As List(Of ChargeInfo) = New List(Of ChargeInfo)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor.Current = Cursors.WaitCursor
        AU = 0
        If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
            Dim res = MessageBox.Show("Ya se estan visualizando terminales en por procesar, si deseas dejar de visualizar las terminales seleccionadas, pulsa Yes, si deseas continuar pulsa no.", "Recargando informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If res = 6 Then
                dgvDetalleTerminales.DataSource = Nothing
                InfoTablas.Clear()
                If TextBox1.Text <> "" Then TextBox1.Clear()
                ChargeGrid()
                CleanningState()
            End If
        Else
            If TextBox1.Text <> "" Then TextBox1.Clear()
            ChargeGrid()
            CleanningState()
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub ChargeGrid()
        Try
            'Al query principal, falta agregar que PWO sea nulo o que no tenga PWO
            Dim consulta As String = $"(select distinct TermA [Term],(
        select SUM(TABalance) + SUM(TBBalance) from tblWipDet det
        where ((det.TermA = tblWipDet.TermA and MaqA='MM' and PWOA is null) or 
        (det.TermB=tblWipDet.TermA and MaqB='MM'and PWOB is null)) and wip in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 
        {If(AU = 0, " ", $" and AU={AU}")})
        ) [Qty],((
		(
        select SUM(TABalance) + SUM(TBBalance) from tblWipDet det
        where ((det.TermA = tblWipDet.TermA and MaqA='MM' and PWOA is null) or 
        (det.TermB=tblWipDet.TermA and MaqB='MM'and PWOB is null)) and wip in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30
		{If(AU = 0, " ", $" and AU={AU}")}
        )
        ) * 3 + 12) / 60
		) [Test], (
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
        ) and MaqA='MM' and TermA like 'T%' and PWOA is null) 
        union
        (select distinct TermB [Term],(
        select SUM(TABalance) + SUM(TBBalance) from tblWipDet det 
        where ((det.TermA = tblWipDet.TermB and MaqA='MM' and PWOA is null) or 
        (det.TermB=tblWipDet.TermB and MaqB='MM' and PWOB is null)) and wip in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 
        {If(AU = 0, " ", $" and AU={AU}")})
        ) [Qty],(
		((
        select SUM(TABalance) + SUM(TBBalance) from tblWipDet det 
        where ((det.TermA = tblWipDet.TermB and MaqA='MM' and PWOA is null) or 
        (det.TermB=tblWipDet.TermB and MaqB='MM' and PWOB is null)) and wip in (
        select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 
        {If(AU = 0, " ", $" and AU={AU}")}
        )) * 3 + 12) / 60
		) [Test], (
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
        ) and MaqB='MM' and TermB like 'T%' and PWOB is null)"
            Dim len = consulta.Length
            Dim dr As SqlDataReader
            Dim aTable As New DataTable
            Dim cmd As SqlCommand = New SqlCommand(consulta, cnn)
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
    Private Sub CleanningState()
        dgvPNTermsProcess.DataSource = Nothing
        Label8.Text = "Number of terminals 
selected: " + InfoTablas.Count.ToString
        Label5.Text = "Records:"
        Label10.Text = "-"
        Label9.Text = "-"
        Label3.Text = "-"
        Label4.Text = "-"
    End Sub
    Private Sub SelectedTermProcess(PN As String, Cell As String, Balance As Integer)
        Try
            Dim aConsulta As String = "select AU,WIP,Wire,TermA,TABalance,TermB,TBBalance,((TABalance + TBBalance) * 3 + 12) / 60 [Test],'' [Celda],WireID,MaqA,MaqB from tblWipDet" &
                                      $" where ((TermA = '{PN}' and MaqA='MM') or (TermB='{PN}' and MaqB='MM')) and" &
                                      $" WIP in (select WIP from tblWIP where Status='OPEN' and MP > 0 and Corte = 0 and wSort >= 30 {If(AU = 0, " ", $" and AU={AU}")}) 
                                      and ((PWOA is null and MaqA='MM') or (PWOB is null and MaqB='MM'))" &
                                      "order by IDSort asc"
            Dim cmd As SqlCommand = New SqlCommand(aConsulta, cnn)
            Dim aTable As DataTable = New DataTable, tbAux As DataTable = New DataTable
            Dim dr As SqlDataReader
            cnn.Open()
            dr = cmd.ExecuteReader
            aTable.Load(dr)
            cnn.Close()
            If aTable.Rows.Count > 0 Then
                If dgvDetalleTerminales.Rows.Count > 0 Then
                    If InfoTablas.Count > 0 Then
                        With dgvDetalleTerminales
                            tbAux = MergedObjTables(aTable, dgvDetalleTerminales)
                            .DataSource = tbAux
                            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            .AutoResizeColumns()
                            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .ClearSelection()
                            SumQtyAndTest(tbAux)
                            Dim last = (From d In InfoTablas Order By d.Rows Descending Select d.Rows).First()
                            FillListInfoTablas(PN, last + aTable.Rows.Count, Cell, Balance)
                        End With
                    End If
                Else
                    With dgvDetalleTerminales
                        .DataSource = Nothing
                        .DataSource = aTable
                        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                        .AutoResizeColumns()
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .ClearSelection()
                        SumQtyAndTest(aTable)
                        FillListInfoTablas(PN, aTable.Rows.Count, Cell, Balance)
                    End With
                End If
            Else
                dgvDetalleTerminales.DataSource = Nothing
            End If
            DisableButton()
            FillColorGrid()
            ChargeSpecsTerms()
            Label5.Text = "Records: " + dgvDetalleTerminales.Rows.Count.ToString
            Label8.Text = "Number of terminals 
selected: " + InfoTablas.Count.ToString
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub ChargeSpecsTerms()
        Try
            With dgvPNTermsProcess
                Dim auxList = InfoTablas.[Select](Function(i) New With {i.PN, i.Balance, i.RunTime
                                                                        }).ToList()
                .DataSource = auxList
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ClearSelection()
                Dim SumRunTime = InfoTablas.[Select](Function(i) i.RunTime).Sum(Function(a) a)
                Dim SetupTime As Integer = InfoTablas.Count * 12
                Label10.Text = SumRunTime
                Label9.Text = SetupTime
                Label4.Text = SumRunTime + SetupTime
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Function GetSerialNewPWO(PrevPWO As String)
        Dim Numero, Ascii1, Ascii2, Ascii3, Ascii4 As Integer
        Dim NumeroString, Letras, Letra1, Letra2, Letra3, Letra4, NewSerial, TnewSerial As String
        NewSerial = ""
        TnewSerial = ""
        PrevPWO = Mid(PrevPWO, 5)
        Try
            If PrevPWO <> "" Then
                Letras = Microsoft.VisualBasic.Left(PrevPWO, 4)
                Numero = Convert.ToInt64(Microsoft.VisualBasic.Right(PrevPWO, 9))
                If Numero < 999999999 Then
                    Numero += 1
                    NumeroString = Numero.ToString
                    If NumeroString.Length < 9 Then
                        For count As Integer = NumeroString.Length To 8
                            NumeroString = "0" + NumeroString
                        Next
                    End If
                    NewSerial = Letras + NumeroString
                ElseIf Numero = 999999999 Then
                    NumeroString = "000000001"
                    Letra1 = Mid(Letras, 1, 1)
                    Letra2 = Mid(Letras, 2, 1)
                    Letra3 = Mid(Letras, 3, 1)
                    Letra4 = Mid(Letras, 4, 1)
                    Ascii1 = Asc(Letra1)
                    Ascii2 = Asc(Letra2)
                    Ascii3 = Asc(Letra3)
                    Ascii4 = Asc(Letra4)
                    If Ascii4 < 90 Then
                        Ascii4 = Ascii4 + 1
                    ElseIf Ascii4 = 90 And Ascii3 < 90 Then
                        Ascii4 = 65
                        Ascii3 = Ascii3 + 1
                    ElseIf Ascii4 = 90 And Ascii3 = 90 And Ascii2 < 90 Then
                        Ascii4 = 65
                        Ascii3 = 65
                        Ascii2 = Ascii2 + 1
                    ElseIf Ascii4 = 90 And Ascii3 = 90 And Ascii2 = 90 And Ascii1 < 90 Then
                        Ascii4 = 65
                        Ascii3 = 65
                        Ascii2 = 65
                        Ascii1 = Ascii1 + 1
                    ElseIf Ascii4 = 90 And Ascii3 = 90 And Ascii2 = 90 And Ascii1 = 90 Then
                        Ascii4 = 65
                        Ascii3 = 65
                        Ascii2 = 65
                        Ascii1 = 65
                    End If
                    Letra1 = Convert.ToChar(Ascii1).ToString
                    Letra2 = Convert.ToChar(Ascii2).ToString
                    Letra3 = Convert.ToChar(Ascii3).ToString
                    Letra4 = Convert.ToChar(Ascii4).ToString
                    Letras = Letra1 + Letra2 + Letra3 + Letra4
                    NewSerial = Letras + NumeroString
                End If
            ElseIf PrevPWO = "" Then
                Letras = "AAAA"
                NumeroString = "00000000" + Numero.ToString
                NewSerial = Letras + NumeroString
            End If
            TnewSerial = "PWO-" + NewSerial
            Return TnewSerial
        Catch ex As Exception
            MessageBox.Show(ex.Message + " Genera Serial Function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private ReadOnly Property SelectIDPWO
        Get
            Try
                Dim cmd As New SqlCommand("select MAX(PWO) from tblPWO", cnn) With {
                .CommandType = CommandType.Text
                }
                cnn.Open()
                Return cmd.ExecuteScalar()
                cnn.Close()
            Catch ex As Exception
                cnn.Close()
                Return Nothing
            Finally
                cnn.Close()
            End Try
        End Get
    End Property

    Private MergedObjTables As Func(Of DataTable, DataGridView, DataTable) = Function(x, y)
                                                                                 Dim dt As New DataTable 
                                                                                 dt = (DirectCast(y.DataSource, DataTable))
                                                                                 dt.Merge(x)
                                                                                 Return dt
                                                                             End Function
    Private SumQtyAndTest As Action(Of DataTable) = Function(a)
                                                        Try
                                                            Dim Qty = (From row In a.AsEnumerable() Select row("TABalance")).ToList().Sum(Function(i) CInt(i.ToString()))
                                                            Qty += (From row In a.AsEnumerable() Select row("TBBalance")).ToList().Sum(Function(i) CInt(i.ToString()))
                                                            Dim Test = (From row In a.AsEnumerable() Select row("Test")).ToList().Sum(Function(i) CInt(i.ToString()))
                                                            Label3.Text = Qty.ToString
                                                            Label4.Text = Test.ToString
                                                        Catch ex As Exception
                                                            MessageBox.Show(ex.ToString)
                                                        End Try
                                                        Return Nothing
                                                    End Function
    Private FillListInfoTablas As Action(Of String, Integer, String, Integer) = Function(b, c, d, e)
                                                                                    Dim oCreate As ChargeInfo = New ChargeInfo
                                                                                    oCreate.PN = b
                                                                                    oCreate.Rows = c
                                                                                    oCreate.Cell = d
                                                                                    oCreate.Balance = e
                                                                                    oCreate.RunTime = e * 3 / 60
                                                                                    InfoTablas.Add(oCreate)
                                                                                    Return Nothing
                                                                                End Function
    Private Sub FillColorGrid()
        If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
            Dim auxRow As Integer = 0
            Dim First = (From d In InfoTablas Order By d.Rows Ascending Select d.Rows).First()
            InfoTablas.ForEach(
                Function(Term)
                    Dim CountInteraction As Integer = 0
                    For Each row As DataGridViewRow In dgvDetalleTerminales.Rows
                        CountInteraction += 1
                        If First = Term.Rows Then
                            auxRow = Term.Rows
                            If CountInteraction >= 1 And CountInteraction <= Term.Rows Then
                                If Term.PN.Equals(row.Cells(3).Value) Then
                                    row.Cells(3).Style.BackColor = Color.LightBlue
                                End If
                                If Term.PN.Equals(row.Cells(5).Value) Then
                                    row.Cells(5).Style.BackColor = Color.LightBlue
                                End If
                                If CountInteraction = Term.Rows Then
                                    Exit For
                                Else
                                    Continue For
                                End If
                            End If
                        ElseIf auxRow < Term.Rows Then
                            If CountInteraction >= auxRow And CountInteraction <= Term.Rows Then
                                If Term.PN.Equals(row.Cells(3).Value) Then
                                    row.Cells(3).Style.BackColor = Color.LightBlue
                                End If
                                If Term.PN.Equals(row.Cells(5).Value) Then
                                    row.Cells(5).Style.BackColor = Color.LightBlue
                                End If
                                If CountInteraction = Term.Rows Then
                                    auxRow = Term.Rows
                                    Exit For
                                Else
                                    Continue For
                                End If
                            End If
                        End If
                    Next
                    Return Nothing
                End Function
                )
        End If
    End Sub
    Private Sub DisableButton()
        If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 1 Then
            Button3.Visible = True
        Else
            Button3.Visible = False
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Cursor.Current = Cursors.WaitCursor
            If IsNumeric(TextBox1.Text) Then
                AU = TextBox1.Text
                If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
                    Dim res = MessageBox.Show("Ya se estan visualizando terminales en por procesar, si deseas dejar de visualizar las terminales seleccionadas, pulsa Yes, si deseas continuar pulsa no.", "Recargando informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If res = 6 Then
                        dgvDetalleTerminales.DataSource = Nothing
                        InfoTablas.Clear()
                        ChargeGrid()
                        CleanningState()
                    End If
                Else
                    ChargeGrid()
                    CleanningState()
                End If
            Else
                MessageBox.Show("El AU ingresado no es valido.")
                dgvTerminalesXProcesar.DataSource = Nothing
            End If
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub dgvTerminalesXProcesar_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTerminalesXProcesar.CellDoubleClick
        If e.RowIndex >= 0 Then
            Cursor.Current = Cursors.WaitCursor
            Dim auxTerm As String = dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("Term").Value.ToString
            Dim auxCell As String = dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("Celda").Value.ToString
            Dim auxBalance As Integer = dgvTerminalesXProcesar.Rows(e.RowIndex).Cells("Qty").Value.ToString
            If (InfoTablas.Where(Function(Terminal) Terminal.PN.Equals(auxTerm)).Count) > 0 Then
                MessageBox.Show($"Esta terminal: {auxTerm} ya esta en la lista en por procesar, no es posible agregarla de nuevo.")
            Else
                SelectedTermProcess(auxTerm, auxCell, auxBalance)
            End If
            Cursor.Current = Cursors.Default
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Eliminar ultima terminal seleccionada
        Dim dt As New DataTable
        dt = (DirectCast(dgvDetalleTerminales.DataSource, DataTable))
        Dim last = (From d In InfoTablas Order By d.Rows Descending Select d.Rows).First()
        InfoTablas.RemoveAt(InfoTablas.FindIndex(Function(d) d.Rows.Equals(last)))
        last = (From d In InfoTablas Order By d.Rows Descending Select d.Rows).First()
        For i = 0 To dt.Rows.Count - 1
            If i >= last Then
                dt.Rows(i).Delete()
            End If
        Next
        dt.AcceptChanges()
        With dgvDetalleTerminales
            .DataSource = Nothing
            .DataSource = dt
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            .AutoResizeColumns()
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ClearSelection()
            SumQtyAndTest(dt)
        End With
        DisableButton()
        FillColorGrid()
        ChargeSpecsTerms()
        Label5.Text = "Records: " + dgvDetalleTerminales.Rows.Count.ToString
        Label8.Text = "Number of terminals 
selected: " + InfoTablas.Count.ToString
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click 'Create PWO
        If dgvDetalleTerminales.Rows.Count > 0 And InfoTablas.Count > 0 Then
            MessageBox.Show(GetSerialNewPWO(SelectIDPWO.ToString))
        Else
            MessageBox.Show("Seleccione primero una terminal para poder crear PWO.")
        End If
    End Sub
    Private Sub Button3_MouseHover(sender As Object, e As EventArgs) Handles Button3.MouseHover
        Cursor.Current = Cursors.Hand
    End Sub
    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles Button3.MouseLeave
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        Cursor.Current = Cursors.Hand
    End Sub
    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Cursor.Current = Cursors.Default
    End Sub
End Class