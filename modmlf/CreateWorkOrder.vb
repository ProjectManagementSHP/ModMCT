Imports System.Data.SqlClient
Imports System.Text
Imports System.Text.RegularExpressions

Public Class CreateWorkOrder
    Private _IdSerial As String
    Private _ListForProcess As List(Of ChargeInfo)
    Private FlagSort As Boolean
    Private GridDemon As DataGridView
    Public Sub New(IGrid As DataGridView, ListOfTerm As List(Of ChargeInfo))
        NoDemon = IGrid
        ListForProcess = ListOfTerm
    End Sub
    Public Sub New()

    End Sub
    Public Function GetSelectIDPWO(Optional Pwo As Boolean = True) As Object
        Try
            Dim cmd As New SqlCommand($"select MAX({If(Pwo, "PWO", "IdentificadorBOM")}) from {If(Pwo, "tblPWO", "tblBOMPWO")}", cnn) With {
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
    End Function
    Public Property _PushFirstPlace As Boolean
        Get
            Return FlagSort
        End Get
        Set
            FlagSort = Value
        End Set
    End Property
    Public Property NoDemon As DataGridView
        Get
            Return GridDemon
        End Get
        Set
            GridDemon = Value
        End Set
    End Property
    Public Property SerialPWO As String
        Get
            Return _IdSerial
        End Get
        Set
            _IdSerial = Value
        End Set
    End Property
    Public Property ListForProcess As List(Of ChargeInfo)
        Get
            Return _ListForProcess
        End Get
        Set(value As List(Of ChargeInfo))
            _ListForProcess = value
        End Set
    End Property
    Public Function GetSerialNewPWO(PrevPWO As String) As String
        Dim Numero, Ascii1, Ascii2, Ascii3, Ascii4 As Integer
        Dim NumeroString, Letras, Letra1, Letra2, Letra3, Letra4, NewSerial, TnewSerial As String
        NewSerial = ""
        TnewSerial = ""
        PrevPWO = Mid(PrevPWO, 5)
        Try
            If PrevPWO <> "" Then
                Letras = Left(PrevPWO, 4)
                Numero = Convert.ToInt64(Right(PrevPWO, 9))
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
    Public Function GetIdBomPwo(PrevPWO As String)
        Dim Numero, ascii1, ascii2, ascii3, ascii4 As Integer
        Dim NumeroString, Letras, letra1, letra2, letra3, letra4 As String, NewSerial As String = ""
        Try
            If PrevPWO <> "" Then
                Letras = Left(PrevPWO, 7)
                Numero = Convert.ToInt64(Mid(PrevPWO, 8, 7))
                If Numero < 9999999 Then
                    Numero = Numero + 1
                    NumeroString = Numero.ToString
                    If NumeroString.Length < 7 Then
                        For count As Integer = NumeroString.Length To 6
                            NumeroString = "0" + NumeroString
                        Next
                    End If
                    NewSerial = Letras + NumeroString
                ElseIf Numero = 9999999 Then
                    NumeroString = "0000001"
                    letra1 = Mid(Letras, 4, 1)
                    letra2 = Mid(Letras, 5, 1)
                    letra3 = Mid(Letras, 6, 1)
                    letra4 = Mid(Letras, 7, 1)
                    ascii1 = Asc(letra1)
                    ascii2 = Asc(letra2)
                    ascii3 = Asc(letra3)
                    ascii4 = Asc(letra4)
                    If ascii4 < 90 Then
                        ascii4 = ascii4 + 1
                    ElseIf ascii4 = 90 And ascii3 < 90 Then
                        ascii4 = 65
                        ascii3 = ascii3 + 1
                    ElseIf ascii4 = 90 And ascii3 = 90 And ascii2 < 90 Then
                        ascii4 = 65
                        ascii3 = 65
                        ascii2 = ascii3 + 1
                    ElseIf ascii4 = 90 And ascii3 = 90 And ascii2 = 90 And ascii1 < 90 Then
                        ascii4 = 65
                        ascii3 = 65
                        ascii2 = 65
                        ascii1 = ascii3 + 1
                    ElseIf ascii4 = 90 And ascii3 = 90 And ascii2 = 90 And ascii1 = 90 Then
                        ascii4 = 65
                        ascii3 = 65
                        ascii2 = 65
                        ascii1 = 65
                    End If
                    letra1 = Convert.ToChar(ascii1).ToString
                    letra2 = Convert.ToChar(ascii2).ToString
                    letra3 = Convert.ToChar(ascii3).ToString
                    letra4 = Convert.ToChar(ascii4).ToString
                    Letras = letra1 + letra2 + letra3 + letra4
                    NewSerial = "IBP" + Letras + NumeroString
                End If
            ElseIf PrevPWO = "" Then
                Letras = "AAAA"
                NumeroString = "0000001"
                NewSerial = "IBP" + Letras + NumeroString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
        Return NewSerial
    End Function
    Public Sub MaterialReserve(Optional FlagReserve As Boolean = True)
        Try
            Dim oChange As Action(Of ChargeInfo) = Function(Term)
                                                       Dim insertItem As String = If(FlagReserve, $"insert into tblPWOTemp (PN) Values ('{Term.PN}')", $"delete from tblPWOTemp where PN='{Term.PN}'")
                                                       Dim command As SqlCommand = New SqlCommand(insertItem, cnn)
                                                       command.CommandType = CommandType.Text
                                                       cnn.Open()
                                                       command.ExecuteNonQuery()
                                                       cnn.Close()
                                                       Return Nothing
                                                   End Function

            ListForProcess.ForEach(oChange)
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Public Function GetSetWorkOrder()
        Dim flag As Boolean
        SerialPWO = GetSerialNewPWO(GetSelectIDPWO().ToString)
        If SerialPWO.ToString.Length > 0 AndAlso (InsertNewWorkOrder() AndAlso CreateBOMWorkOrder() AndAlso SetCC()) Then
            flag = True
        Else
            flag = False
        End If
        Return flag
    End Function
    Public Function CreateBOMWorkOrder()
        Try
            Dim First = (From d In _ListForProcess Order By d.Rows Ascending Select d.Rows).First()
            Dim auxRow As Integer = 0
            Dim dtDemon As New DataTable
            dtDemon = (DirectCast(GridDemon.DataSource, DataTable))
            Dim objMerge As New DataTable
            _ListForProcess.ForEach(Function(Term)
                                        Dim CountInteraction As Integer = 0
                                        Dim AppendInfo As New StringBuilder("")
                                        For Each row As DataRow In dtDemon.Rows
                                            CountInteraction += 1
                                            If First = Term.Rows Then
                                                auxRow = Term.Rows
                                                If CountInteraction >= 1 And CountInteraction <= Term.Rows Then
                                                    If Term.PN.Equals(row.Item(3).ToString) Then
                                                        AppendInfo.Append($"'{row.Item("WireID")}',")
                                                    End If
                                                    If Term.PN.Equals(row.Item(5).ToString) Then
                                                        AppendInfo.Append($"'{row.Item("WireID")}',")
                                                    End If
                                                    If CountInteraction = Term.Rows Then
                                                        Exit For
                                                    Else
                                                        Continue For
                                                    End If
                                                End If
                                            ElseIf auxRow < Term.Rows Then
                                                If CountInteraction >= auxRow And CountInteraction <= Term.Rows Then
                                                    If Term.PN.Equals(row.Item(3).ToString) Then
                                                        AppendInfo.Append($"'{row.Item("WireID")}',")
                                                    End If
                                                    If Term.PN.Equals(row.Item(5).ToString) Then
                                                        AppendInfo.Append($"'{row.Item("WireID")}',")
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
                                        If AppendInfo.Length > 0 Then
                                            If objMerge.Rows.Count = 0 Then
                                                objMerge = MergeDataBOM(AppendInfo.ToString.TrimEnd(",").Trim, Term.PN.ToString).Copy()
                                            Else
                                                'Dim objCopyTb =
                                                objMerge.Merge(MergeDataBOM(AppendInfo.ToString.TrimEnd(",").Trim, Term.PN.ToString), True)
                                            End If
                                        End If
                                        Return Nothing
                                    End Function)
            If objMerge IsNot Nothing Then
                For Each row As DataRow In objMerge.Rows
                    InsertItem(row.Item("WIP").ToString, row.Item("Term").ToString, CInt(row.Item("Bal").ToString))
                Next
            End If
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
            Return False
        End Try
        Return True
    End Function
    Private Function MergeDataBOM(Id As String, PN As String) As DataTable
        Try
            Dim reader As SqlDataReader
            Dim command As New SqlCommand($";WITH BomPwo (WIP,Term) as (
            (select WIP, TermA [Term] from tblWipDet where WireID in ({Id}) and (TermA='{PN}' and MaqA='MM')) union 
            (select WIP, TermB [Term] from tblWipDet where WireID in ({Id}) and (TermB='{PN}' and MaqB='MM')))
            select WIP,Term,(select (Select IsNull((select IsNull(SUM(TABalance),0) from tblWipDet where WireID in ({Id}) and (TermA='{PN}' and MaqA='MM') and WIP=BomPwo.WIP group by MaqA having MaqA = 'MM'),0)) +
            (Select IsNull((select IsNull(SUM(TBBalance),0) from tblWipDet where WireID in ({Id}) and (TermB='{PN}' and MaqB='MM') and WIP=BomPwo.WIP group by MaqB having MaqB = 'MM'),0))) [Bal] from BomPwo", cnn)
            command.CommandType = CommandType.Text
            cnn.Open()
            reader = command.ExecuteReader
            Dim tb As New DataTable
            tb.Load(reader)
            cnn.Close()
            If tb IsNot Nothing Then
                Return tb
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.Message + vbNewLine + ex.ToString)
            Return Nothing
        End Try
        Return Nothing
    End Function
    Private Sub InsertItem(Wip As String, PN As String, Qty As Integer)
        Try
            Dim insertBom As String = $"insert into tblBOMPWO (IdentificadorBOM,PWO,WIP,AU,Rev,PN,Qty,Unit,Description,Balance,CreatedDate) Values (@IDBOMProcesos,@PWO,@WIP,(select AU from tblWIP where WIP=@WIP),(select Rev from tblWIP where WIP=@WIP),@PN,@Qty,'ea',(select top 1 Description from tblItemsQB where pn = @PN),@Qty,GETDATE())"
            Dim command As SqlCommand = New SqlCommand(insertBom, cnn)
            command.CommandType = CommandType.Text
            command.CommandTimeout = 120000
            command.Parameters.Add("@IDBOMProcesos", SqlDbType.NVarChar).Value = GetIdBomPwo(GetSelectIDPWO(False).ToString)
            command.Parameters.Add("@PWO", SqlDbType.NVarChar).Value = SerialPWO
            command.Parameters.Add("@PN", SqlDbType.NVarChar).Value = PN
            command.Parameters.Add("@WIP", SqlDbType.NVarChar).Value = Wip
            command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = Qty
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Private Sub UpdatePWOForWipdet(side As String, Cell As String, WireID As String)
        Try
            Dim Bom As String = $"update tblWipDet set PWO{side}='{SerialPWO}',Cell{side}='{Cell}' where WireID='{WireID}'"
            Dim command As SqlCommand = New SqlCommand(Bom, cnn)
            command.CommandType = CommandType.Text
            cnn.Open()
            command.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
    End Sub
    Public Function SetCC()
        Try
            Dim First = (From d In _ListForProcess Order By d.Rows Ascending Select d.Rows).First()
            Dim auxRow As Integer = 0
            Dim dtDemon As New DataTable
            dtDemon = (DirectCast(GridDemon.DataSource, DataTable))
            _ListForProcess.ForEach(Function(Term)
                                        Dim CountInteraction As Integer = 0
                                        For Each row As DataRow In dtDemon.Rows
                                            CountInteraction += 1
                                            If First = Term.Rows Then
                                                auxRow = Term.Rows
                                                If CountInteraction >= 1 And CountInteraction <= Term.Rows Then
                                                    If Term.PN.Equals(row.Item(3).ToString) Then
                                                        UpdatePWOForWipdet("A", Term.Cell.ToString.ToUpper, row.Item("WireId").ToString)
                                                    End If
                                                    If Term.PN.Equals(row.Item(5).ToString) Then
                                                        UpdatePWOForWipdet("B", Term.Cell.ToString.ToUpper, row.Item("WireId").ToString)
                                                    End If
                                                    If CountInteraction = Term.Rows Then
                                                        Exit For
                                                    Else
                                                        Continue For
                                                    End If
                                                End If
                                            ElseIf auxRow < Term.Rows Then
                                                If CountInteraction >= auxRow And CountInteraction <= Term.Rows Then
                                                    If Term.PN.Equals(row.Item(3).ToString) Then
                                                        UpdatePWOForWipdet("A", Term.Cell.ToString.ToUpper, row.Item("WireId").ToString)
                                                    End If
                                                    If Term.PN.Equals(row.Item(5).ToString) Then
                                                        UpdatePWOForWipdet("B", Term.Cell.ToString.ToUpper, row.Item("WireId").ToString)
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
                                    End Function)
        Catch ex As Exception
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
            Return False
        End Try
        Return True
    End Function
    Public Function InsertNewWorkOrder()
        Try
            Dim ESetupTime As Integer = _ListForProcess.Count * 5
            Dim ERunTime As Integer = _ListForProcess.[Select](Function(i) i.RunTime).Sum(Function(a) a)
            Dim SumBalances As Integer = _ListForProcess.[Select](Function(i) i.Balance).Sum(Function(a) a)
            Dim CellMaxCurrent As String = _ListForProcess.Max(Function(c) c.Cell)
            CellMaxCurrent = Regex.Replace(CellMaxCurrent, "[aeiouAEIOU]", "")
            Dim Sort As AutomaticSort = New AutomaticSort(CellMaxCurrent.Trim().ToUpper())
            Dim dtDemon As New DataTable
            dtDemon = (DirectCast(GridDemon.DataSource, DataTable))
            Dim NumTravelers = (From row In dtDemon.AsEnumerable() Select row("WireID")).ToList().Count
            Dim oCmdo As New SqlCommand("INSERT INTO tblPWO (
                                         PWO,Cell,Status,ESetupTime,ERunTime,ETotalTime,NumTravelers,TotalCrimps,CreatedDate,CreatedBy,Id,wSort
                                         )
                                         VALUES (
                                         @PWO,@Cell,'OPEN',@ESetupTime,@ERunTime,@ETotalTime,@NumTravelers,@TotalCrimps,GETDATE(),@CreatedBy,@Id,3
                                         )", cnn)
            oCmdo.CommandType = CommandType.Text
            oCmdo.Parameters.Add("@PWO", SqlDbType.NVarChar).Value = SerialPWO
            oCmdo.Parameters.Add("@Cell", SqlDbType.NVarChar).Value = CellMaxCurrent.Trim().ToUpper()
            oCmdo.Parameters.Add("@ESetupTime", SqlDbType.Int).Value = ESetupTime
            oCmdo.Parameters.Add("@ERunTime", SqlDbType.Int).Value = ERunTime
            oCmdo.Parameters.Add("@ETotalTime", SqlDbType.Int).Value = ERunTime + ESetupTime
            oCmdo.Parameters.Add("@NumTravelers", SqlDbType.Int).Value = NumTravelers
            oCmdo.Parameters.Add("@TotalCrimps", SqlDbType.Int).Value = SumBalances
            oCmdo.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = UserName
            If _PushFirstPlace Then
                Sort.PushFirstPlacePWO()
                oCmdo.Parameters.Add("@Id", SqlDbType.Int).Value = 1
            Else
                oCmdo.Parameters.Add("@Id", SqlDbType.Int).Value = Sort.GetSortPWO()
            End If
            cnn.Open()
            Return If(oCmdo.ExecuteNonQuery() > 0, True, False)
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
            Return False
        Finally
            cnn.Close()
        End Try
    End Function
End Class
