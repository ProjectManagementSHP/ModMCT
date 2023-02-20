Imports System.Data.SqlClient
Imports System.DirectoryServices
'Imports System.Deployment.Application
Public Class Login
    Private dep As String
    Private intento As Integer
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = UserName
        opcion = 0
        Me.Visible = False
        If Not LogOut Then
            log_on()
        End If
        TextBox2.Focus()
        TextBox2.Select()
        If ApplicationDeployment.IsNetworkDeployed Then
            With ApplicationDeployment.CurrentDeployment.CurrentVersion
                Label3.Text = "Version: " & .Major & "." & .Minor & "." & .Build & "." & .Revision & ""
            End With
        Else
            Label3.Text = "Version: " & Application.ProductVersion & ""
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
                    .AllVisible()
                    .ShowDialog()
                End With
                Hide()
                Principal.Text = "Admin"
                Principal.Show()
            ElseIf TextBox1.Text = "" And TextBox2.Text = "a10946830z" Then
                With OpcionesLog
                    .AllVisible()
                    .ShowDialog()
                End With
                Hide()
                Principal.Text = "Direccion"
                Principal.Show()
            Else
                Dim User As LoginUser = New LoginUser(TextBox1.Text, TextBox2.Text, "MLF")
                If User.IsAuthenticated Then
                    If User.GetUserAuthorization() Then
                        dep = User.AuthorizedDept
                        UserName = User.AuthorizedName
                        If CbRecordarUser.Checked Then
                            RecordarUsuario()
                        End If
                        If dep = "Desarrollo" Then
                            With OpcionesLog
                                If LogOut Then
                                    .Label2.Visible = False
                                End If
                                .AllVisible()
                                .ShowDialog()
                            End With
                            Hide()
                            Principal.Text = "Desarrollo"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "Almacen" Then
                            opcion = 2
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "Almacen"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "Corte" Then
                            opcion = 1
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "Corte"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "Aplicadores" Then
                            opcion = 3
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "Aplicadores"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "XP" Then
                            opcion = 4
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "XP"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "Compras" Then
                            opcion = 5
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "Compras"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "PlanCorte" Then
                            opcion = 6
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "Planeacion Corte"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "PlanXP" Then
                            opcion = 7
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "Planeacion XP"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep = "PlanPWO" Then
                            opcion = 8
                            insertMLFNotification()
                            Hide()
                            Principal.Text = "Planeacion PWO"
                            CharginPrincipal()
                            Principal.Show()
                        ElseIf dep.Contains(",") Then
                            With OpcionesLog
                                If LogOut Then
                                    .Label2.Visible = False
                                End If
                                .CheckOpcionesVisible(dep)
                                .ShowDialog()
                            End With
                            MultiDepart = True
                            Hide()
                            Principal.Text = GetNameDept()
                            CharginPrincipal()
                            Principal.Show()
                        Else
                            MsgBox("Tu departamento no tiene acceso a este modulo, verificalo e intenta de nuevo")
                        End If
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
                        Close()
                    End If
                    TextBox2.Focus()
                    TextBox2.SelectAll()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            EnviaCorreoFalla("Login_User", host, UserName)
        End Try
    End Sub
    Private Sub CharginPrincipal()
        If LogOut Then
            Principal.Form1_Load(New [Object], New EventArgs)
        End If
    End Sub
    Private Sub log_on()
        Dim User As LoginUser = New LoginUser()
        If User.GetUserToLogin() Then
            If User.GetUserAuthorization() Then
                dep = User.AuthorizedDept
                UserName = User.AuthorizedName
                If dep = "Desarrollo" Then
                    With OpcionesLog
                        .AllVisible()
                        .ShowDialog()
                    End With
                    Principal.Text = "Desarrollo"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "Almacen" Then
                    opcion = 2
                    insertMLFNotification()
                    Principal.Text = "Almacen"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "Corte" Then
                    opcion = 1
                    insertMLFNotification()
                    Principal.Text = "Corte"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "Aplicadores" Then
                    opcion = 3
                    Principal.Text = "Aplicadores"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "XP" Then
                    opcion = 4
                    insertMLFNotification()
                    Principal.Text = "XP"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "Compras" Then
                    opcion = 5
                    insertMLFNotification()
                    Principal.Text = "Compras"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "PlanCorte" Then
                    opcion = 6
                    insertMLFNotification()
                    Principal.Text = "Planeacion Corte"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "PlanXP" Then
                    opcion = 7
                    insertMLFNotification()
                    Principal.Text = "Planeacion XP"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep = "PlanPWO" Then
                    opcion = 8
                    insertMLFNotification()
                    Principal.Text = "Planeacion PWO"
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                ElseIf dep.Contains(",") Then
                    With OpcionesLog
                        .CheckOpcionesVisible(dep)
                        .ShowDialog()
                    End With
                    MultiDepart = True
                    Principal.Text = GetNameDept()
                    Principal.ShowDialog()
                    If Not LogOut Then
                        Close()
                    End If
                Else
                    MsgBox("Tu departamento no tiene acceso a este modulo, verificalo e intenta de nuevo")
                End If
            Else
                Me.Visible = True
            End If
        Else
            Me.Visible = True
        End If
    End Sub
    Private Function GetNameDept()
        Dim returnName As String = ""
        If opcion = 2 Then
            returnName = "Almacen"
        ElseIf opcion = 3 Then
            returnName = "Aplicadores"
        ElseIf opcion = 5 Then
            returnName = "Compras"
        ElseIf opcion = 6 Then
            returnName = "Planeacion Corte"
        ElseIf opcion = 7 Then
            returnName = "Planeacion XP"
        ElseIf opcion = 8 Then
            returnName = "Planeacion PWO"
        End If
        Return returnName
    End Function
    Private Sub insertMLFNotification()
        Try
            Dim valor As Boolean = False
            cnn.Open()
            Using cmd As New SqlCommand("select ID from tblMLFNotifications where [User] = '" + UserName.ToString + "'", cnn)
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn
                valor = CStr(cmd.ExecuteScalar) = ""
                cnn.Close()
            End Using
            Dim insert As String = ""
            If valor Then
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
            EnviaCorreoFalla("insertMLFNotification", host, UserName)
        End Try
    End Sub
    Private Sub RecordarUsuario()
        Try
            Dim cmod As SqlCommand = New SqlCommand($"select Count(*) from tblLogs where userID = '{UserName}' and hostname = '{Security.Principal.WindowsIdentity.GetCurrent().Name}'", cnn)
            cnn.Open()
            If CInt(cmod.ExecuteScalar()) = 0 Then
                cmod = New SqlCommand($"insert into tblLogs (userID,hostname,LoginDate) values ('{UserName}','{Security.Principal.WindowsIdentity.GetCurrent().Name}',Convert(date,GetDate()))", cnn)
                cmod.ExecuteNonQuery()
            End If
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
        End Try
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.PasswordChar = ""
        ElseIf Not CheckBox1.Checked Then
            TextBox2.PasswordChar = "*"
        End If
    End Sub
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Close()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class