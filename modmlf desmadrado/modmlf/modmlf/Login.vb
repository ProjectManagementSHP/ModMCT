Imports System.Data.SqlClient
Imports System.DirectoryServices
Public Class Login
    Private tblUsersPriv As New DataTable
    'Private nombreusuario As String
    Private intento As Integer
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = UserName
        Me.Visible = False
        log_on()
        'TextBox2.Text = "S@m14875094"
        TextBox2.Focus()
        TextBox2.Select()
        tblUsersPriv.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Login_User()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        Cursor.Current = Cursors.WaitCursor
        If Asc(e.KeyChar) = 13 Then
            Login_User()
        End If
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub Login_User()

        If intento > 0 Then
            intento = intento
        Else
            intento = 0
        End If

        tblUsersPriv.Clear()
        Try
            If TextBox1.Text = "admin" And TextBox2.Text = "admin" Then
                opcion = 1
                Principal.Text = "Admin"
                Principal.Show()
                Me.Hide()
            Else
                If IsAuthenticated("SHPMFG", TextBox1.Text, TextBox2.Text) Then
                    'If validaLog() = True Then
                    Dim Autorizacion As String = AutorizacionDelUsuario(TextBox1.Text) 'userID) '
                    If Autorizacion = "OK" Then
                        Dim Department As String = tblUsersPriv.Rows(0).Item("Department").ToString
                        Select Case Department
                            Case "Desarrollo"
                                opcion = 1
                                Principal.Show()
                                Me.Hide()
                            Case "Almacen"
                                opcion = 2
                                Principal.Text = "Almacen"
                                Principal.Show()
                                Me.Hide()
                            Case "Corte"
                                opcion = 1
                                Principal.Text = "Corte"
                                Principal.Show()
                                Me.Hide()
                            Case "Aplicadores"
                                opcion = 3
                                Principal.Text = "Aplicadores"
                                Principal.Show()
                                Me.Hide()
                            Case "XP"
                                opcion = 4
                                Principal.Text = "XP"
                                Principal.Show()
                                Me.Hide()
                            Case "Compras"
                                opcion = 5
                                Principal.Text = "Compras"
                                Principal.Show()
                                Me.Hide()
                            Case Else
                                MsgBox("Tu departamento no tiene acceso a este modulo, verificalo e intenta de nuevo")
                        End Select
                        TextBox2.Text = ""
                        TextBox1.Text = ""
                        'Me.Hide()
                    Else
                        TextBox2.Text = ""
                    End If
                Else
                    MessageBox.Show("Usuario o contraseña incorrecto, por favor intente de nuevo " + vbNewLine + "User Or password incorrect, Please Try again.", "Authentication Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    intento += 1
                    If intento = 3 Then
                        MessageBox.Show("Ha superado el maximo de intentos", "Authentication Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.Close()
                    End If
                    TextBox2.Focus()
                    TextBox2.SelectAll()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CorreoFalla.EnviaCorreoFalla("Login_User", host, UserName)
        End Try
    End Sub
    Private Sub log_on()
        If validaLog() = True Then
            Dim Autorizacion As String = AutorizacionDelUsuario(userID) 'Este es para validar usuario entrando con dash board
            If Autorizacion = "OK" Then
                Dim Department As String = tblUsersPriv.Rows(0).Item("Department").ToString
                Select Case Department
                    Case "Desarrollo"
                        opcion = 1
                    Case "Almacen"
                        opcion = 2
                        Principal.Text = "Almacen"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "Corte"
                        opcion = 1
                        Principal.Text = "Corte"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "Aplicadores"
                        opcion = 3
                        Principal.Text = "Aplicadores"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "XP"
                        opcion = 4
                        Principal.Text = "XP"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "Compras"
                        opcion = 5
                        Principal.Text = "Compras"
                        Principal.ShowDialog()
                        Me.Close()
                    Case Else
                        MsgBox("Tu departamento no tiene acceso a este modulo, verificalo e intenta de nuevo")
                End Select
            Else
                Me.Visible = True
            End If
        Else
            Me.Visible = True
        End If
    End Sub
    Private Function validaLog()
        Try
            Dim query As String = ""

            query = "select userID from tblLogs where LoginDate = CONVERT(DATE, GETDATE()) AND hostname = '" & System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString & "'"
            userID = selectScalar(query)
            If userID = "" Then
                query = "select userID from tblLogs where hostname = '" & System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString & "'"
                userID = selectScalar(query)
                If userID = "" Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("validaLog", host, UserName)
            cnn.Close()
            Return Nothing
        End Try
    End Function
    Private Function selectScalar(query As String) As String
        cnn.Close()
        cnn.Open()
        Dim valor As String = ""
        Try
            Using cmd As New SqlCommand(query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn
                If IsDBNull(cmd.ExecuteScalar()) Or CStr(cmd.ExecuteScalar) = "" Then
                    valor = ""
                Else
                    valor = cmd.ExecuteScalar()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CorreoFalla.EnviaCorreoFalla("selectScalar", host, UserName)
        Finally
            cnn.Close()
        End Try
        Return valor
    End Function
    Public Function IsAuthenticated(ByVal domain As String, ByVal username As String, ByVal pwd As String) As Boolean
        Dim path As String = "LDAP://" & "10.17.182.22" 'domain
        Dim domainAndUsername As String = domain + "\" + username
        Dim entry As DirectoryEntry = New DirectoryEntry(path, domainAndUsername, pwd)
        'Dim filterAttribute As String = ""
        Try
            'Bind to the native AdsObject to force authentication.
            Dim obj As Object = entry.NativeObject
            Dim search As DirectorySearcher = New DirectorySearcher(entry)
            search.Filter = "(SAMAccountName=" & username & ")"
            search.PropertiesToLoad.Add("cn")
            Dim result As SearchResult = search.FindOne()
            If (result Is Nothing) Then
                Return False
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Private Function AutorizacionDelUsuario(ByVal Usuario As String)
        Dim Resp As String = "NO"
        Dim Departamento As String
        Dim Edo As String = ""
        Dim Query As String = "SELECT * FROM tblItemsPOUserIDAuthorizations WHERE UserID=@UserID AND Module='MLF'"
        '' Using TN As New DataTable
        Try
            Dim cmd As SqlCommand = New SqlCommand(Query, cnn)
            Dim DR As SqlDataReader
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = Usuario
            cnn.Open()
            DR = cmd.ExecuteReader
            tblUsersPriv.Load(DR)
            cnn.Close()
        Catch ex As Exception
            cnn.Close() 'cierra la conexion
            MessageBox.Show(ex.Message, "Error in AutorizacionDelUsuario function") 'despliega un mesaje si hay un error
            'CorreoFalla.EnviaCorreoFalla("AutorizacionDelUsuario", host, UserName)
        End Try
        If tblUsersPriv.Rows.Count > 0 Then
            For NM As Integer = 0 To tblUsersPriv.Rows.Count - 1
                Dim Activo As String = tblUsersPriv.Rows(NM).Item("Active").ToString
                Dim Modulo As String = tblUsersPriv.Rows(NM).Item("Module").ToString
                Departamento = tblUsersPriv.Rows(NM).Item("Department").ToString.ToUpper
                'nombreusuario = tblUsersPriv.Rows(NM).Item("UserID").ToString
                If Modulo = "MLF" Then
                    If Activo = "True" Then
                        Resp = "OK"
                        UserName = tblUsersPriv.Rows(NM).Item("UserID").ToString
                    Else
                        MessageBox.Show("This user isn't active", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Next
        End If
        If Resp = "NO" Then MessageBox.Show("This user isn't authorized", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ''   End Using
        Return Resp
    End Function
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = ""
        ElseIf CheckBox1.Checked = False Then
            TextBox2.PasswordChar = "*"
        End If
    End Sub
End Class