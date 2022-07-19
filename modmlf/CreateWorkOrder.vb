Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class CreateWorkOrder
    Private _IdSerial As String
    Private _ListForProcess As List(Of ChargeInfo)
    Public FlagSort As Boolean
    Public GridDemon As DataGridView
    ReadOnly Property SelectIDPWO
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
    Public Function GetListForProcess() As List(Of ChargeInfo)
        Return _ListForProcess
    End Function
    Public Sub SetListForProcess(value As List(Of ChargeInfo))
        _ListForProcess = value
    End Sub
    Public Function GetSerialNewPWO(PrevPWO As String) As String
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
    Public Sub GetSetWorkOrder()
        _IdSerial = GetSerialNewPWO(SelectIDPWO.ToString)
        If _IdSerial.ToString.Length > 0 Then
            If InsertNewWorkOrder() Then

            End If
        End If
    End Sub
    Private Sub CreateBOMWorkOrder()
        Try
            Dim ObAux As ChargeInfo
            Dim lst As List(Of ChargeInfo) = New List(Of ChargeInfo)
            Dim WIP As String = ""
            Dim auxBalanceForWIP As Integer = 0
            Dim auxTerm As String = ""
            Dim auxRow As Integer = 0
            Dim First = (From d In _ListForProcess Order By d.Rows Ascending Select d.Rows).First()
            _ListForProcess.ForEach(
                Function(Term)
                    For ii = 0 To GridDemon.Rows.Count - 1
                        If First = Term.Rows Then
                            auxRow = Term.Rows
                            If ii = 0 Then
                                WIP = GridDemon.Rows(ii).Cells("WIP").Value.ToString
                                If Term.PN.Equals(GridDemon.Rows(ii).Cells("TermA").Value.ToString) And GridDemon.Rows(ii).Cells("MaqA").Value.ToString = "MM" Then
                                    auxBalanceForWIP += CInt(GridDemon.Rows(ii).Cells("TABalance").Value.ToString)
                                End If
                                If Term.PN.Equals(GridDemon.Rows(ii).Cells("TermB").Value.ToString) And GridDemon.Rows(ii).Cells("MaqA").Value.ToString = "MM" Then
                                    auxBalanceForWIP += CInt(GridDemon.Rows(ii).Cells("TBBalance").Value.ToString)
                                End If
                            Else
                                If WIP = GridDemon.Rows(ii).Cells("WIP").Value.ToString Then
                                    If Term.PN.Equals(GridDemon.Rows(ii).Cells("TermA").Value.ToString) And GridDemon.Rows(ii).Cells("MaqA").Value.ToString = "MM" Then
                                        auxBalanceForWIP += CInt(GridDemon.Rows(ii).Cells("TABalance").Value.ToString)
                                    End If
                                    If Term.PN.Equals(GridDemon.Rows(ii).Cells("TermB").Value.ToString) And GridDemon.Rows(ii).Cells("MaqA").Value.ToString = "MM" Then
                                        auxBalanceForWIP += CInt(GridDemon.Rows(ii).Cells("TBBalance").Value.ToString)
                                    End If
                                Else
                                    ObAux = New ChargeInfo
                                    With ObAux
                                        .PN = Term.PN
                                        .Balance = auxBalanceForWIP
                                        .WIP = WIP
                                    End With
                                    lst.Add(ObAux)
                                    WIP = GridDemon.Rows(ii).Cells("WIP").Value.ToString
                                    auxBalanceForWIP = 0
                                    If Term.PN.Equals(GridDemon.Rows(ii).Cells("TermA").Value.ToString) And GridDemon.Rows(ii).Cells("MaqA").Value.ToString = "MM" Then
                                        auxBalanceForWIP += CInt(GridDemon.Rows(ii).Cells("TABalance").Value.ToString)
                                    End If
                                    If Term.PN.Equals(GridDemon.Rows(ii).Cells("TermB").Value.ToString) And GridDemon.Rows(ii).Cells("MaqA").Value.ToString = "MM" Then
                                        auxBalanceForWIP += CInt(GridDemon.Rows(ii).Cells("TBBalance").Value.ToString)
                                    End If
                                End If
                            End If
                        ElseIf auxRow < Term.Rows Then

                        End If
                        If lst Is Nothing Then
                            ObAux = New ChargeInfo
                            With ObAux
                                .PN = Term.PN
                                .Balance = auxBalanceForWIP
                                .WIP = WIP
                            End With
                            lst.Add(ObAux)
                        Else
                            If Not (lst.Where(Function(a) a.PN = Term.PN And a.WIP = WIP And a.Balance = auxBalanceForWIP).Count > 0) Then
                                ObAux = New ChargeInfo
                                With ObAux
                                    .PN = Term.PN
                                    .Balance = auxBalanceForWIP
                                    .WIP = WIP
                                End With
                                lst.Add(ObAux)
                            End If
                        End If
                    Next
                    Return Nothing
                End Function
                )
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SetCC()

    End Sub
    Private Function InsertNewWorkOrder()
        Try
            Dim ESetupTime As Integer = _ListForProcess.Count * 12
            Dim ERunTime As Integer = _ListForProcess.[Select](Function(i) i.RunTime).Sum(Function(a) a)
            Dim SumBalances As Integer = _ListForProcess.[Select](Function(i) i.Balance).Sum(Function(a) a)
            Dim CellMaxCurrent As String = _ListForProcess.Max(Function(c) c.Cell)
            CellMaxCurrent = Regex.Replace(CellMaxCurrent, "[aeiouAEIOU]", "")
            Dim Sort As AutomaticSort = New AutomaticSort(CellMaxCurrent.Trim().ToUpper())
            Dim dtDemon As New DataTable
            dtDemon = (DirectCast(GridDemon.DataSource, DataTable))
            Dim NumTravelers = (From row In dtDemon.AsEnumerable() Select row("WireID")).ToList().Count
            Dim oCmdo As New SqlCommand("INSERT INTO tblPWO (
                                         PWO,Cell,Status,ESetupTime,ERunTime,ETotalTime,NumTravelers,TotalCrimps,CreatedDate,CreatedBy,Id
                                         )
                                         VALUES (
                                         @PWO,@Cell,'OPEN',@ESetupTime,@ERunTime,@ETotalTime,@NumTravelers,@TotalCrimps,GETDATE(),@CreatedBy,@Id
                                         )", cnn)
            oCmdo.CommandType = CommandType.Text
            oCmdo.Parameters.Add("@PWO", SqlDbType.NVarChar).Value = _IdSerial
            oCmdo.Parameters.Add("@Cell", SqlDbType.NVarChar).Value = CellMaxCurrent.Trim().ToUpper()
            oCmdo.Parameters.Add("@ESetupTime", SqlDbType.Int).Value = ESetupTime
            oCmdo.Parameters.Add("@ERunTime", SqlDbType.Int).Value = ERunTime
            oCmdo.Parameters.Add("@ETotalTime", SqlDbType.Int).Value = ERunTime + ESetupTime
            oCmdo.Parameters.Add("@NumTravelers", SqlDbType.Int).Value = NumTravelers
            oCmdo.Parameters.Add("@TotalCrimps", SqlDbType.Int).Value = SumBalances
            oCmdo.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = UserName
            If FlagSort = True Then
                Sort.PushFirstPlacePWO()
                oCmdo.Parameters.Add("@Id", SqlDbType.Int).Value = 1
            Else
                oCmdo.Parameters.Add("@Id", SqlDbType.Int).Value = Sort.GetSortPWO()
            End If
            cnn.Open()
            oCmdo.ExecuteNonQuery()
            cnn.Close()
            Return True
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
            Return False
        End Try
    End Function
End Class
