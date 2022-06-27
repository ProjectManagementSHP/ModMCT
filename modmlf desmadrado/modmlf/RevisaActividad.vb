Imports System.Data.SqlClient
Public Class RevisaActividad
    Public Shared Function ActividadRealizada()
        Dim respuesta As String
        Dim t As New DataTable
        Try
            query = ""
            Select Case opcion
                Case 1
                    query = "select CWO from tblCWO where (CONVERT(date,dateConfirmaAlm) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "' or CONVERT(date,dateConfirmaApl) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "') and Id in (2,3)"
                Case 2
                    query = "select CWO from tblCWO where CONVERT(date,dateSolicitud) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "' and Id=1"
                Case 3
                    query = "select CWO from tblCWO where CONVERT(date,dateSolicitud) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "' and Id=1"
                Case 4
                    query = "select CWO from tblCWO where (CONVERT(date,dateConfirmaAlm) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "' or CONVERT(date,dateConfirmaApl) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "') and Id in (2,3)"
            End Select
            If query <> "" Then
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                t.Load(dr)
                edo = cnn.State.ToString
                cnn.Close()
                If t.Rows.Count > 0 Then
                    respuesta = "YES"
                    Dim q As String = "update tblCWO set Id=0 where CWO in (" + query + ")"
                    cmd = New SqlCommand(q, cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                Else
                    respuesta = "NO"
                End If
            Else
                respuesta = "NO"
            End If
            Return respuesta
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("ActividadRealizada", host, UserName)
            Return Nothing
        End Try
    End Function
    Public Shared Function RevisaActividadHold()
        Dim respuesta As String
        Dim t As New DataTable
        Try
            query = ""
            If opcion = 1 Or opcion = 4 Or opcion = 5 Then
                query = "select CWO from tblCWO where (CONVERT(date,dateConfirmaAlm) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "' or CONVERT(date,dateConfirmaApl) = '" & FormatDateTime(Now, DateFormat.ShortDate) & "') and Id in (12,14)"
            End If
            If query <> "" Then
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                t.Load(dr)
                edo = cnn.State.ToString
                cnn.Close()
                If t.Rows.Count > 0 Then
                    respuesta = "YES"
                    Dim q As String = "update tblCWO set Id=0 where CWO in (" + query + ")"
                    cmd = New SqlCommand(q, cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                Else
                    respuesta = "NO"
                End If
            Else
                respuesta = "NO"
            End If
            Return respuesta
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("RevisaActividadHold", host, UserName)
            Return Nothing
        End Try
    End Function
    Public Shared Function revisaFPROM()
        Dim respuesta As String
        Dim t As New DataTable
        Try
            query = ""
            If (opcion <> 3) And (opcion <> 5) Then
                query = "select CWO from tblCWO where Id = 44"
            End If
            If query <> "" Then
                cmd = New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cnn.Open()
                dr = cmd.ExecuteReader
                t.Load(dr)
                edo = cnn.State.ToString
                cnn.Close()
                If t.Rows.Count > 0 Then
                    respuesta = "YES"
                    Dim q As String = "update tblCWO set Id=0 where CWO in (" + query + ")"
                    cmd = New SqlCommand(q, cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                Else
                    respuesta = "NO"
                End If
            Else
                respuesta = "NO"
            End If
            Return respuesta
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("revisaFPROM", host, UserName)
            Return Nothing
        End Try
    End Function
End Class
