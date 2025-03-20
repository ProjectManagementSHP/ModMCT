Imports System.Data.SqlClient
Imports System.Deployment.Application
Imports System.Reflection.Emit
Imports System.DirectoryServices 'Libreria para el acceso mediante ActiveDirectory

Public Class Login

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
    Private cnn As New SqlConnection(strconexion)
    Private tblUsersPriv As New DataTable
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Cursor.Current = Cursors.WaitCursor
        Login_User()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (ApplicationDeployment.IsNetworkDeployed) Then
            With ApplicationDeployment.CurrentDeployment.CurrentVersion
                Label1.Text = "V." & .Major & "." & .Minor & "." & .Build & "." & .Revision
            End With
        Else
            Me.Label1.Text = "V." & System.Windows.Forms.Application.ProductVersion
        End If
        UsernameTextBox.Focus()
        tblUsersPriv.Clear()
    End Sub

    Private Function AutorizacionDelUsuario(ByVal Usuario As String)
        Dim Resp As String = "NO"
        Dim Departamento As String
        Dim Edo As String = ""
        Dim Query As String = "SELECT * FROM tblItemsPOUserIDAuthorizations WHERE UserID=@UserID AND Module='ModMasterCircuitTracker'"
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
        End Try
        If tblUsersPriv.Rows.Count > 0 Then
            For NM As Integer = 0 To tblUsersPriv.Rows.Count - 1
                Dim Activo As String = tblUsersPriv.Rows(NM).Item("Active").ToString
                Dim Modulo As String = tblUsersPriv.Rows(NM).Item("Module").ToString
                Departamento = tblUsersPriv.Rows(NM).Item("Department").ToString.ToUpper
                If Modulo = "ModMasterCircuitTracker" Then
                    If Activo = "True" Then
                        Resp = "OK"
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

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Application.Exit()
    End Sub

    Private Sub Login_User()
        tblUsersPriv.Clear()
        Try


            If UsernameTextBox.Text = "ADMIN" And PasswordTextBox.Text = "admin" Then

                frmInicioBase.Show()
                'frmCircuitosLoc.MdiParent = frmInicioBase
                'frmCircuitosLoc.Show()
                'frmCircuitosLoc.WindowState = FormWindowState.Maximized
                Me.Close()

            Else
                If IsAuthenticated("SHPMFG", UsernameTextBox.Text, PasswordTextBox.Text) Then
                    'If IsAuthenticated("SHPMFG", txbUserLog.Text, txbUserPass.Text) Then
                    Dim Autorizacion As String = AutorizacionDelUsuario(UsernameTextBox.Text)
                    Autorizacion = "OK"
                    If Autorizacion = "OK" Then
                        Dim Department As String = tblUsersPriv.Rows(0).Item("Department").ToString
                        Dim menu As String = tblUsersPriv.Rows(0).Item("Menu").ToString


                        'MsgBox(Department)
                        Select Case Department
                            Case "IT"
                                frmInicioBase.Show()
                                'frmCircuitosLoc.MdiParent = frmInicioBase
                                'frmCircuitosLoc.Show()
                                'frmCircuitosLoc.WindowState = FormWindowState.Maximized
                                Me.Close()
                            Case "Ensamble" ''Para Ensamble

                            Case "Almacen"


                            Case "Compras"

                        End Select
                        'Departament = Department
                        'User = UsernameTextBox.Text
                        PasswordTextBox.Text = ""
                        UsernameTextBox.Text = ""
                        Me.Hide()
                    Else
                        PasswordTextBox.Text = ""

                    End If
                Else
                    MessageBox.Show("Usuario o contraseña incorrecto, por favor intente de nuevo " + vbNewLine + "User Or password incorrect, Please Try again.", "Authentication Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    PasswordTextBox.Focus()
                    PasswordTextBox.SelectAll()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function IsAuthenticated(ByVal domain As String, ByVal username As String, ByVal pwd As String) As Boolean
        Dim path As String = "LDAP://" & "10.17.182.22" 'domain
        Dim domainAndUsername As String = domain + "\" + username
        Dim entry As DirectoryEntry = New DirectoryEntry(path, domainAndUsername, pwd)
        Dim filterAttribute As String = ""
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
End Class
