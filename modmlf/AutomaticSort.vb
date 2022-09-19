Imports System.Data.SqlClient
Public Class AutomaticSort
    'Inherits CreateWorkOrder
    Public conexion As New SqlConnection(strconexion)
    Public Maq As Integer
    Public Cell As String
    Public NewSort As Integer
    Public OldSort As Integer
    Public LongitudArray As Integer
    Public Grid As DataGridView
    Public CWOForChange As String
    Public Sub New(NewSort As Integer, OldSort As Integer, LongArray As Integer, Grid As DataGridView, Maquina As Integer, CWO As String)
        Me.NewSort = NewSort
        Me.OldSort = OldSort
        Me.LongitudArray = LongArray
        Me.Grid = Grid
        Me.Maq = Maquina
        Me.CWOForChange = CWO
    End Sub
    Public Sub New(Maquina As Integer)
        Me.Maq = Maquina
    End Sub
    Public Sub New(Maquina As Integer, oldsort As Integer)
        Me.Maq = Maquina
        Me.OldSort = oldsort
    End Sub
    Public Sub New(Cell As String)
        Me.Cell = Cell
    End Sub
    Function Compare() As Boolean
        If Me.NewSort > 0 Then
            If Me.NewSort < Me.OldSort Then
                Return True
            ElseIf Me.NewSort >= Me.OldSort Then
                Return False
            End If
        Else
            Return False
        End If
        Return Nothing
    End Function
    Sub SetArray()
        Dim arrayCWO(LongitudArray - 1) As String, arraySort(LongitudArray - 1) As Integer, tempSort As Integer, tempCWO As String
        For i As Integer = 0 To Grid.Rows.Count - 1
            Dim auxString As String = Grid.Rows(i).Cells("CWO").Value.ToString, auxSort As Integer = CInt(Val(Grid.Rows(i).Cells("Orden").Value.ToString))
            arrayCWO(i) = auxString
            arraySort(i) = auxSort
        Next
        If (arrayCWO IsNot Nothing) And (arraySort IsNot Nothing) Then
            For i As Integer = 0 To arrayCWO.Length - 1
                If arrayCWO(i) = CWOForChange Then
                    arraySort(i) = NewSort
                    Exit For
                End If
            Next
            For j As Integer = 0 To arrayCWO.Length - 1
                If arraySort(j) >= NewSort And arrayCWO(j) <> CWOForChange Then
                    arraySort(j) += 1
                End If
            Next
            For ii = 0 To arraySort.Length - 2
                For i = 0 To arraySort.Length - 2
                    If arraySort(i) > arraySort(i + 1) Then
                        tempSort = arraySort(i)
                        tempCWO = arrayCWO(i)
                        arrayCWO(i) = arrayCWO(i + 1)
                        arraySort(i) = arraySort(i + 1)
                        arrayCWO(i + 1) = tempCWO
                        arraySort(i + 1) = tempSort
                    End If
                Next
            Next
            For jj = 0 To arraySort.Length - 1
                If Not arraySort(jj) = (jj + 1) Then
                    arraySort(jj) = arraySort(jj) - 1
                End If
            Next
            For i = 0 To arraySort.Length - 1
                Update(arraySort(i).ToString, arrayCWO(i).ToString)
            Next
        End If
    End Sub
    Function CheckZeros() As Boolean
        Try
            Dim ArraySort As DataTable = New DataTable
            Dim aCmdo As SqlCommand = New SqlCommand($"select Id from tblCWO where Maq={Me.Maq} and (Id is not null or Id>0) and Status='OPEN' order by Id asc", cnn)
            Dim daread As SqlDataReader
            aCmdo.CommandType = CommandType.Text
            cnn.Open()
            daread = aCmdo.ExecuteReader
            ArraySort.Load(daread)
            cnn.Close()
            If ArraySort.Rows.Count > 0 Then
                Dim Ids = (From row In ArraySort.AsEnumerable() Select row("Id")).ToList()
                If Ids IsNot Nothing Then
                    For i = 0 To Ids.Count - 1
                        If i = 0 Then
                            If Ids(i) > 1 Or Ids(i) <= 0 Then
                                Return False
                            End If
                        Else
                            If Not Ids(i) = (Ids(i - 1) + 1) Then
                                Return False
                            End If
                        End If
                    Next
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            cnn.Close()
            Return False
        End Try
        Return False
    End Function
    Public Function RemoveZeros()
        Try
            Dim aCmdo As SqlCommand = New SqlCommand($"select Id,CWO from tblCWO where Maq={Me.Maq} and (Id is not null or Id>0) and Status='OPEN' ORDER BY tblCWO.Id ASC ", cnn)
            Dim aRead As SqlDataReader
            Dim oArray As DataTable = New DataTable
            aCmdo.CommandType = CommandType.Text
            cnn.Open()
            aRead = aCmdo.ExecuteReader
            oArray.Load(aRead)
            cnn.Close()
            If oArray.Rows.Count > 0 Then
                For i = 0 To oArray.Rows.Count - 1
                    If i = 0 Then
                        If CInt(oArray.Rows(i).Item("Id").ToString) < 1 Or CInt(oArray.Rows(i).Item("Id").ToString) > 1 Then
                            Update(1, oArray.Rows(i).Item("CWO").ToString)
                            Return RemoveZeros()
                        ElseIf CInt(oArray.Rows(i).Item("Id").ToString) = 1 Then
                            Continue For
                        End If
                    Else
                        If CInt(oArray.Rows(i).Item("Id").ToString) = (CInt(oArray.Rows(i - 1).Item("Id").ToString) + 1) Then
                            Continue For
                        Else
                            Update((CInt(oArray.Rows(i - 1).Item("Id").ToString) + 1), oArray.Rows(i).Item("CWO").ToString)
                            Return RemoveZeros()
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            cnn.Close()
        End Try
        Return Nothing
    End Function
    Function GetIdSort()
        Try
            Dim aCmdo As SqlCommand = New SqlCommand($"select Id from tblCWO where CWO='{Me.CWOForChange}'", cnn)
            aCmdo.CommandType = CommandType.Text
            cnn.Open()
            Return If(CInt(aCmdo.ExecuteScalar) = Me.NewSort, True, False)
        Catch ex As Exception
            cnn.Close()
        Finally
            cnn.Close()
        End Try
        Return Nothing
    End Function
    Sub Update(IdNew As Integer, CWO As String)
        Try
            Dim cmdo1 As SqlCommand = New SqlCommand("update tblCWO set Id=" + IdNew.ToString + " where CWO='" + CWO.ToString + "'", cnn)
            cmdo1.CommandType = CommandType.Text
            cnn.Open()
            Dim edo = cnn.State.ToString
            cmdo1.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
        End Try
    End Sub
    Function GetSort() As Integer
        Try
            Dim value As Integer, query As String = ""
            query = $"select ISNULL(NULLIF(MAX(Id),0),0) + 1 [Id] from tblCWO where Maq={Maq} and Status='OPEN' and CloseDate is null and (Id is not null or Id > 0)"
            Dim cmd As SqlCommand = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            'cmd.Parameters.Add("@Maq", SqlDbType.Int).Value = Maq
            cnn.Open()
            value = cmd.ExecuteScalar
            cnn.Close()
            Return value
        Catch ex As Exception
            cnn.Close()
            Return 0
        End Try
    End Function
    Function GetSortPWO() As Integer
        Try
            Dim value As Integer, query As String = ""
            query = $"select ISNULL(NULLIF(MAX(Id),0),0) + 1 [Id] from tblPWO where Cell='{Cell}' and Status='OPEN' and CloseDate is null and (Id is not null or Id > 0)"
            Dim cmd As SqlCommand = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            value = cmd.ExecuteScalar
            cnn.Close()
            Return value
        Catch ex As Exception
            cnn.Close()
            Return 0
        End Try
    End Function
    Sub ReOrderSort()
        Try
            Using cmd As New SqlCommand("update tblCWO set Id= Id - 1 where Maq=" + Maq.ToString + " and Status='OPEN' and Id > 0 and Id > " + OldSort.ToString + "", cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
            End Using
        Catch ex As Exception
            cnn.Close()
        End Try
    End Sub
    Sub PushFirstPlacePWO()
        Try
            Using cmd As New SqlCommand("update tblPWO set Id= Id + 1 where Cell='" + Cell.ToString + "' and Status='OPEN' and Id > 0 ", cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
            End Using
        Catch ex As Exception
            cnn.Close()
        End Try
    End Sub
End Class
