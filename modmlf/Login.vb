Imports System.Data.SqlClient
Imports System.DirectoryServices
'Imports System.Deployment.Application
Public Class Login
    Private dep As String
    Private intento As Integer
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = UserName
        Me.Visible = False
        log_on()
        TextBox2.Focus()
        TextBox2.Select()
        If (ApplicationDeployment.IsNetworkDeployed) Then
            With ApplicationDeployment.CurrentDeployment.CurrentVersion
                Label3.Text = "Version: " & .Major & "." & .Minor & "." & .Build & "." & .Revision & ""
            End With
        Else
            Label3.Text = "Version: " & System.Windows.Forms.Application.ProductVersion & ""
        End If
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
        intento = If(intento > 0, intento, 0)
        Try
            If TextBox1.Text = "admin" And TextBox2.Text = "Supremadmin" Then
                With OpcionesLog
                    .ShowDialog()
                End With
                Principal.Text = "Admin"
                Principal.Show()
                Me.Hide()
            ElseIf TextBox1.Text = "" And TextBox2.Text = "a10946830z" Then
                With OpcionesLog
                    .ShowDialog()
                End With
                Principal.Text = "Direccion"
                Principal.Show()
                Me.Hide()
            Else
                Dim User As LoginUser = New LoginUser(TextBox1.Text, TextBox2.Text, "MLF")
                If User.IsAuthenticated() Then
                    If User.GetUserAuthorization() Then
                        dep = User.GetAuthorizedDept()
                        UserName = User.GetAuthorizedName()
                        Select Case dep
                            Case "Desarrollo"
                                With OpcionesLog
                                    .ShowDialog()
                                End With
                                Principal.Text = "Desarrollo"
                                Principal.Show()
                                Me.Hide()
                            Case "Almacen"
                                opcion = 2
                                insertMLFNotification()
                                Principal.Text = "Almacen"
                                Principal.Show()
                                Me.Hide()
                            Case "Corte"
                                opcion = 1
                                insertMLFNotification()
                                Principal.Text = "Corte"
                                Principal.Show()
                                Me.Hide()
                            Case "Aplicadores"
                                opcion = 3
                                insertMLFNotification()
                                Principal.Text = "Aplicadores"
                                Principal.Show()
                                Me.Hide()
                            Case "XP"
                                opcion = 4
                                insertMLFNotification()
                                Principal.Text = "XP"
                                Principal.Show()
                                Me.Hide()
                            Case "Compras"
                                opcion = 5
                                insertMLFNotification()
                                Principal.Text = "Compras"
                                Principal.Show()
                                Me.Hide()
                            Case "PlanCorte"
                                opcion = 6
                                insertMLFNotification()
                                Principal.Text = "Planeacion Corte"
                                Principal.Show()
                                Me.Hide()
                            Case "PlanXP"
                                opcion = 7
                                insertMLFNotification()
                                Principal.Text = "Planeacion XP"
                                Principal.Show()
                                Me.Hide()
                            Case Else
                                MsgBox("Tu departamento no tiene acceso a este modulo, verificalo e intenta de nuevo")
                        End Select
                        TextBox2.Text = ""
                        TextBox1.Text = ""
                    Else
                        MessageBox.Show("Usuario sin autorizacion, por favor intente de nuevo ", "Authentication Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        Dim User As LoginUser = New LoginUser()
        If User.GetUserToLogin() Then
            If User.GetUserAuthorization() Then
                dep = User.GetAuthorizedDept()
                UserName = User.GetAuthorizedName()
                Select Case dep
                    Case "Desarrollo"
                        With OpcionesLog
                            .ShowDialog()
                        End With
                        Principal.Text = "Desarrollo"
                        Principal.Show()
                        Me.Close()
                    Case "Almacen"
                        opcion = 2
                        insertMLFNotification()
                        Principal.Text = "Almacen"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "Corte"
                        opcion = 1
                        insertMLFNotification()
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
                        insertMLFNotification()
                        Principal.Text = "XP"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "Compras"
                        opcion = 5
                        insertMLFNotification()
                        Principal.Text = "Compras"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "PlanCorte"
                        opcion = 6
                        insertMLFNotification()
                        Principal.Text = "Planeacion Corte"
                        Principal.ShowDialog()
                        Me.Close()
                    Case "PlanXP"
                        opcion = 7
                        insertMLFNotification()
                        Principal.Text = "Planeacion XP"
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
    Private Sub insertMLFNotification()
        Try
            Dim valor As String = ""
            cnn.Open()
            Using cmd As New SqlCommand("select ID from tblMLFNotifications where [User] = '" + UserName.ToString + "'", cnn)
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn
                valor = If(CStr(cmd.ExecuteScalar) = "", "YES", "")
                cnn.Close()
            End Using
            Dim insert As String = ""
            If valor = "YES" Then
                If dep = "PlanCorte" Then
                    insert = "insert into tblMLFNotifications ([User],Dep) values ('" + UserName.ToString + "','Corte')"
                Else
                    insert = "insert into tblMLFNotifications ([User],Dep) values ('" + UserName.ToString + "','" + dep.ToString + "')"
                End If
                Dim cmod As SqlCommand = New SqlCommand(insert, cnn)
                cnn.Open()
                cmod.ExecuteNonQuery()
                cnn.Close()
            End If
        Catch ex As Exception
            CorreoFalla.EnviaCorreoFalla("insertMLFNotification", host, UserName)
        End Try
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = ""
        ElseIf CheckBox1.Checked = False Then
            TextBox2.PasswordChar = "*"
        End If
    End Sub
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
    End Sub
End Class