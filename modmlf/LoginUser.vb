Imports System.Data.SqlClient
Imports System.DirectoryServices
Public Class LoginUser
    Public username As String
    Public password As String
    Public aModule As String
    Public Dept As String
    Public SuperAdmin As Boolean
    Public Sub New(Optional username As String = "", Optional password As String = "", Optional aModule As String = "MLF")
        Me.username = username
        Me.password = password
        Me.aModule = aModule
    End Sub
    Public ReadOnly Property IsAuthenticated As Boolean
        Get
            Dim path As String = "LDAP://" & "10.17.182.22" 'domain
            Dim domainAndUsername As String = "SHPMFG" + "\" + username
            Dim entry As DirectoryEntry = New DirectoryEntry(path, domainAndUsername, password)
            Try
                Dim obj As Object = entry.NativeObject
                Dim search As DirectorySearcher = New DirectorySearcher(entry)
                search.Filter = "(SAMAccountName=" & username & ")"
                search.PropertiesToLoad.Add("cn")
                Dim result As SearchResult = search.FindOne()
                If (result Is Nothing) Then
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Get
    End Property
    Public Function GetUserAuthorization() As Boolean
        Dim Resp As Boolean
        Dim Query As String = "SELECT UserID,Department,IsNull(Menu,'0') FROM tblItemsPOUserIDAuthorizations WHERE UserID=@UserID AND Module=@Module and Active=1"
        Try
            Dim cmd As SqlCommand = New SqlCommand(Query, cnn)
            Dim DR As SqlDataReader
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = username
            cmd.Parameters.Add("@Module", SqlDbType.NVarChar).Value = aModule
            cnn.Open()
            DR = cmd.ExecuteReader
            If DR.HasRows Then
                While DR.Read
                    Me.Dept = DR.GetValue(1).ToString
                    Me.username = DR.GetValue(0).ToString
                    SuperAdmin = CBool(DR.GetValue(2).ToString)
                    Resp = True
                End While
            Else
                Resp = False
            End If
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            EnviaCorreoFalla("AutorizacionDelUsuario", host, username)
            Return Nothing
        End Try
        Return Resp
    End Function
    Public Function GetUserToLogin() As Boolean
        cnn.Close()
        cnn.Open()
        Dim valor As String
        query = "select userID from tblLogs where /*LoginDate = CONVERT(DATE, GETDATE()) AND*/ hostname = '" & Security.Principal.WindowsIdentity.GetCurrent().Name.ToString & "'"
        Try
            Using cmd As New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn
                valor = If(IsDBNull(cmd.ExecuteScalar()) Or CStr(cmd.ExecuteScalar) = "", "", DirectCast(cmd.ExecuteScalar(), String))
            End Using
            cnn.Close()
            If valor = "" Then
                Return False
            Else
                Me.username = valor
                Return True
            End If
        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error)
            cnn.Close()
        End Try
    End Function
    Public ReadOnly Property AuthorizedName As String
        Get
            Return Me.username
        End Get
    End Property
    Public ReadOnly Property AuthorizedDept As String
        Get
            Return Me.Dept
        End Get
    End Property
End Class
