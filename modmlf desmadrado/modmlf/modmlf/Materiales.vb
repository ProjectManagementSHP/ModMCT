Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Net
Imports System.DirectoryServices
Public Class Materiales
    Private nombreasig As String = ""
    Private Sub Materiales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = Nothing
        dgvBOM.DataSource = Nothing
        If opcion = 2 Or opcion = 5 Then
            Label1.Text = "Materiales BOM"
            LinkLabel1.Text = "Almacen"
            If p = 1 Then
                GroupBox2.Visible = True
                DataGridView1.Visible = True
                dgvBOM.Visible = False
                llenagrid2()
            Else
                GroupBox2.Visible = False
                dgvBOM.Visible = True
                DataGridView1.Visible = False
                gbnotasconfirmando.Visible = False
                Me.AutoScroll = False
                llenagrid()
            End If
        ElseIf opcion = 3 Then
            Label1.Text = "Aplicadores"
            LinkLabel1.Text = "ACS"
            Me.AutoScroll = False
            llenagrid1()
        End If
        Label4.Visible = False
        DateTimePicker1.Value = "2021-01-01"
        DateTimePicker1.Visible = False
        gbnotasconfirmando.Visible = False
    End Sub
    Private Sub llenagrid()
        Dim query As String = "Select bw.PN,bw.Description,CONVERT(int,bw.Qty) [Qty] ,CONVERT(int,bw.Balance) [Balance],
                               null [En Piso], null [Almacen],
                               Convert(Int, ml.QtyOnHand) [Total],
                               Convert(Int, ml.QtyOnHand) - CONVERT(int,bw.Balance) [Dif],
                               Convert(Int, ml.QtyOnOrder) [In Transit]
                               From tblBOMCWO As bw inner Join tblItemsQB As ml On bw.PN = ml.PN Where bw.CWO ='" + lblcwomat.Text + "' 
                               group by bw.PN,bw.Description,bw.Qty,bw.Balance,ml.QtyOnHand,ml.QtyOnOrder"

        Dim tabla As New DataTable
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If tabla.Rows.Count > 0 Then
                For i As Integer = 0 To tabla.Rows.Count - 1
                    tabla.Columns(4).ReadOnly = False
                    tabla.Rows(i).Item(4) = enpiso(tabla.Rows(i).Item("PN").ToString)
                    tabla.Columns(5).ReadOnly = False
                    tabla.Rows(i).Item(5) = enalmacen(tabla.Rows(i).Item("PN").ToString)
                Next
            End If

            With dgvBOM
                .DataSource = tabla
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Label2.Text = "Items: " & dgvBOM.Rows.Count
            End With
            pintandoceldas()
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("llenagrid'Materiales'", host, UserName)
        End Try
    End Sub
    Private Sub llenagrid1()
        Dim query As String = "select Wire,TermA,SUBSTRING(isnull(AplicatorA,0),0,4)[AplicatorA],IDKeyA,MaqA,TermB,SUBSTRING(isnull(AplicatorB,0),0,4)[AplicatorB],
                               IDKeyB,MaqB,PTA, QAPTA,EngPTA,WTAWG,WC,WA,IC,IA 
                               from tblWipDet inner join tblTermSpecs on tblTermSpecs.IDKey=tblWipDet.IDKeyA where CWO='" + lblcwomat.Text + "' 
                               group by Wire,TermA,AplicatorA,IDKeyA,MaqA,TermB,AplicatorB,IDKeyB,MaqB,PTA, QAPTA,EngPTA,WTAWG,WC,WA,IC,IA"
        Dim tabla As New DataTable
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            With dgvBOM
                .DataSource = tabla
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Label2.Text = "Items: " & dgvBOM.Rows.Count
                DataGridView1.Visible = False
            End With
            recorregrid()
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("llenagrid1'Materiales'", host, UserName)
        End Try
    End Sub
    Private Sub llenagrid2()
        Dim query As String = "Select bw.PN,bw.Description,CONVERT(int,bw.Qty) [Qty] ,CONVERT(int,bw.Balance) [Balance],
                               null [En Piso], null [Almacen],
                               Convert(Int, ml.QtyOnHand) [Total],
                               Convert(Int, ml.QtyOnHand) - CONVERT(int,bw.Balance) [Dif],
                               Convert(Int, ml.QtyOnOrder) [In Transit]
                               From tblBOMCWO As bw inner Join tblItemsQB As ml On bw.PN = ml.PN Where bw.CWO ='" + lblcwomat.Text + "' 
                               group by bw.PN,bw.Description,bw.Qty,bw.Balance,ml.QtyOnHand,ml.QtyOnOrder"
        Dim tabla As New DataTable
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tabla.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If tabla.Rows.Count > 0 Then
                For i As Integer = 0 To tabla.Rows.Count - 1
                    tabla.Columns(4).ReadOnly = False
                    tabla.Rows(i).Item(4) = enpiso(tabla.Rows(i).Item("PN").ToString)
                    tabla.Columns(5).ReadOnly = False
                    tabla.Rows(i).Item(5) = enalmacen(tabla.Rows(i).Item("PN").ToString)
                Next
            End If
            With DataGridView1
                .DataSource = tabla
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Label2.Text = "Items: " & DataGridView1.Rows.Count
            End With
            If DataGridView1.Rows.Count > 0 Then
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    DataGridView1.Rows(i).Cells("Chk").Value = True
                Next
            End If
            pintandoceldas()
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("llenagrid2'Materiales'", host, UserName)
        End Try
    End Sub
    Private Function enpiso(pn As String) As Integer
        Try
            Dim qtyenpiso As Integer
            Dim consulta As String = "select distinct isnull(CONVERT(int,sum(Balance)),0) [qty en piso] from tblItemsTags where Status='NoAvailable' and Balance > 0 and PN=@pn"
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@pn", SqlDbType.NVarChar).Value = pn
            cnn.Open()
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                While dr.Read()
                    qtyenpiso = dr.GetValue(0)
                End While
            Else
                cnn.Close()
                qtyenpiso = 0
            End If
            cnn.Close()
            Return qtyenpiso
        Catch ex As Exception
            MsgBox(ex.ToString)
            cnn.Close()
            Return Nothing
        End Try
    End Function
    Private Function enalmacen(pn As String) As Integer
        Try
            Dim qtyenalmacen As Integer
            Dim consulta As String = "select distinct isnull(CONVERT(int,sum(Balance)),0) [qty en almacen] from tblItemsTags where Status='Available' and Balance > 0 and PN=@pn"
            cmd = New SqlCommand(consulta, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@pn", SqlDbType.NVarChar).Value = pn
            cnn.Open()
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                While dr.Read()
                    qtyenalmacen = dr.GetValue(0)
                End While
            Else
                cnn.Close()
                qtyenalmacen = 0
            End If
            cnn.Close()
            Return qtyenalmacen
        Catch ex As Exception
            MsgBox(ex.ToString)
            cnn.Close()
            Return Nothing
        End Try
    End Function
    Private Sub pintandoceldas()
        For Each linea As DataGridViewRow In dgvBOM.Rows
            If linea.Cells(2).Value <= linea.Cells(6).Value Then
                linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
            Else
                linea.DefaultCellStyle.BackColor = Color.Red
            End If
        Next
        For Each linea As DataGridViewRow In DataGridView1.Rows
            If linea.Cells(3).Value <= linea.Cells(6).Value Then
                linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
            Else
                linea.DefaultCellStyle.BackColor = Color.Red
            End If
        Next
    End Sub
    Private Sub recorregrid()
        For i As Integer = 0 To dgvBOM.Rows.Count - 1
            If dgvBOM.Rows(i).Cells("AplicatorA").Value.ToString > 0 Then
                If DisponibilidadApl(dgvBOM.Rows(i).Cells("AplicatorA").Value.ToString) = "YES" Then
                    dgvBOM.Rows(i).Cells("AplicatorA").Style.BackColor = Color.Red
                    dgvBOM.Rows(i).Cells("AplicatorA").Style.ForeColor = Color.Black
                Else
                    dgvBOM.Rows(i).Cells("AplicatorA").Style.BackColor = Color.Green
                    dgvBOM.Rows(i).Cells("AplicatorA").Style.ForeColor = Color.Black
                End If
            End If
            If dgvBOM.Rows(i).Cells("AplicatorA").Value.ToString = 0 Then

            End If
            If dgvBOM.Rows(i).Cells("AplicatorB").Value.ToString > 0 Then
                If DisponibilidadApl(dgvBOM.Rows(i).Cells("AplicatorB").Value.ToString) = "YES" Then
                    dgvBOM.Rows(i).Cells("AplicatorB").Style.BackColor = Color.Red
                    dgvBOM.Rows(i).Cells("AplicatorB").Style.ForeColor = Color.Black
                Else
                    dgvBOM.Rows(i).Cells("AplicatorB").Style.BackColor = Color.Green
                    dgvBOM.Rows(i).Cells("AplicatorB").Style.ForeColor = Color.Black
                End If
            End If
            If dgvBOM.Rows(i).Cells("AplicatorB").Value.ToString = 0 Then

            End If
        Next
    End Sub
    Private Function DisponibilidadApl(apl As String) As String
        Try
            Dim res As String
            Dim tabla As New DataTable
            Dim query As String = "select * from tblApl where OemAplID=@apl and (Loc is not null or Loc='Produccion')"
            comando = New SqlCommand(query, conexion)
            comando.CommandType = CommandType.Text
            comando.Parameters.Add("@apl", SqlDbType.NVarChar).Value = apl
            conexion.Open()
            read = comando.ExecuteReader
            tabla.Load(read)
            conexion.Close()
            If tabla.Rows.Count > 0 Then
                res = "YES"
            Else
                res = "NO"
            End If
            Return res
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("DisponibilidadApl", host, UserName)
            Return Nothing
        End Try
    End Function
    Private Function CargaDestinatarios(ByVal Modulo As String, ByVal OpcionEnvio As String)
        Dim Destinatarios As String = ""
        Dim contador As Long = 0
        Using TE As New DataTable
            Try
                Dim Query As String = "SELECT Email FROM tblUserEmails WHERE Module=@Module AND Active=1 AND OptionToSend=@OptionToSend"
                cmd = New SqlCommand(Query, cnn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@Module", SqlDbType.NVarChar).Value = Modulo
                cmd.Parameters.Add("@OptionToSend", SqlDbType.NVarChar).Value = OpcionEnvio
                cnn.Open()
                dr = cmd.ExecuteReader
                TE.Load(dr)
                edo = cnn.State.ToString
                If edo = "Open" Then cnn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Loading tblUserEmails")
                CorreoFalla.EnviaCorreoFalla("CargaDestinatarios", host, UserName)
            End Try
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If TE.Rows.Count > 0 Then
                For NM As Integer = 0 To TE.Rows.Count - 1
                    Destinatarios += TE.Rows(NM).Item("Email").ToString
                    If NM < TE.Rows.Count - 1 Then Destinatarios += ","
                Next
            End If
        End Using
        Return Destinatarios
    End Function
    Private Sub EnviaCorreo(Mensaje As String)
        Try
            Dim DestinatariosTO As String = CargaDestinatarios("MatSinStock", "TO")
            'Dim DestinatariosCC As String = CargaDestinatarios("MatSinStock", "CC")
            'Dim DestinatariosBCC As String = CargaDestinatarios("MatSinStock", "BCC")
            Dim EnviadoPor As String = "shp.app@specializedharness.com"
            Dim Correo As String
            Correo = Mensaje + " por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + ""

            Correo += vbNewLine + vbNewLine + vbNewLine
            Correo += "Por favor no responder este correo" + vbNewLine + "Gracias"

            Dim _Message As New System.Net.Mail.MailMessage()
            Dim _SMTP As New System.Net.Mail.SmtpClient

            _SMTP.Credentials = New System.Net.NetworkCredential(EnviadoPor, "Row.6078$")
            _SMTP.Host = "smtp.ipower.com"
            _SMTP.Port = 587
            _SMTP.EnableSsl = True

            'If DestinatariosCC <> "" Then _Message.CC.Add(DestinatariosCC)
            'If DestinatariosBCC <> "" Then _Message.Bcc.Add(DestinatariosBCC)
            _Message.[To].Add(DestinatariosTO)
            _Message.From = New System.Net.Mail.MailAddress(EnviadoPor, "", System.Text.Encoding.UTF8)
            _Message.Subject = "Advertencia de confirmacion sin stock"
            _Message.SubjectEncoding = System.Text.Encoding.UTF8
            _Message.Body = Correo
            _Message.BodyEncoding = System.Text.Encoding.UTF8
            _Message.Priority = System.Net.Mail.MailPriority.High

            _Message.IsBodyHtml = False
            'ENVIO
            _SMTP.Send(_Message)
            'MsgBox("Se ha Enviado el Email", MsgBoxStyle.Information, "Email enviado")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            CorreoFalla.EnviaCorreoFalla("EnviaCorreoSinStock", host, UserName)
        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If LinkLabel1.Text = "Almacen" Then
                Process.Start("\\10.17.182.12\Test\WareHouse\Almacen\ConPeso\Almacen.exe")
            ElseIf LinkLabel1.Text = "ACS" Then
                Process.Start("\\10.17.182.12\Test\ACS\ACS.application")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("LinkLabel1_LinkClicked", host, UserName)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> "" Then
            Dim query, respuesta As String
            'Dim name As String = ""
            Dim rows, count As Integer
            Try
                If IsNumeric(TextBox1.Text) Then
                    query = "select CONCAT(FirstName,' ',LastName) [Empleado] from tblEmpleados where EmployeeNumber=" + TextBox1.Text + ""
                    cmd = New SqlCommand(query, cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    dr = cmd.ExecuteReader
                    If dr.HasRows Then
                        While dr.Read()
                            nombreasig = dr.GetValue(0)
                        End While
                    Else
                        MsgBox("Escanee un numero de empleado valido", MsgBoxStyle.Information)
                        cnn.Close()
                        Exit Sub
                    End If
                    cnn.Close()
                Else
                    MsgBox("Escanee un numero de empleado valido", MsgBoxStyle.Information)
                    Exit Sub
                End If
                rows = 0
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(i).Cells("Qty").Value > DataGridView1.Rows(i).Cells("Total").Value Then
                        rows += 1
                    End If
                Next
                'rows = DataGridView1.Rows.Count
                count = 0
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(i).Cells("Chk").Value = True Then
                        If DataGridView1.Rows(i).Cells("Qty").Value > DataGridView1.Rows(i).Cells("Total").Value Then
                            count += 1
                        End If
                    End If
                Next
                If rows > 0 And count > 0 Then
                    If rows = count Then
                        respuesta = MessageBox.Show("Hay numeros de parte sin stock, aun asi" + vbNewLine + "¿Desea confirmar los materiales? Si es asi, debe poner fecha promesa del material", "Confirmacion", MessageBoxButtons.YesNo)
                        If respuesta = vbYes Then
                            gbnotasconfirmando.Visible = True
                            DateTimePicker1.Visible = True
                        ElseIf respuesta = vbNo Then
                            TextBox1.Clear()
                            TextBox1.Focus()
                            Exit Sub
                        End If
                    ElseIf count <> rows Then
                        query = "update tblCWO set AsignadoA='" + nombreasig.ToString + "' where CWO= '" + lblcwomat.Text + "'"
                        cmd = New SqlCommand(query, cnn)
                        cmd.CommandType = CommandType.Text
                        cnn.Open()
                        cmd.ExecuteNonQuery()
                        cnn.Close()
                        query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha)
                             values (@CWO,@User,'Almacen','Confirmado',GETDATE())"
                        cmd = New SqlCommand(query, cnn)
                        cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = lblcwomat.Text
                        cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                        cmd.CommandType = CommandType.Text
                        cnn.Open()
                        cmd.ExecuteNonQuery()
                        cnn.Close()
                        '-------------------------------------------------------------------------------
                        ' Luego, confirmamos
                        Principal.poneokalm_apl(lblcwomat.Text, Label4.Text)
                        If flag = 1 Then Principal.notesWIPandCWOquitandoOnHold()
                        '-------------------------------------------------------------------------------
                        Me.Hide()
                    End If
                Else
                    query = "update tblCWO set AsignadoA='" + nombreasig.ToString + "' where CWO= '" + lblcwomat.Text + "'"
                    cmd = New SqlCommand(query, cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    '-------------------------------------------------------------------------------
                    query = "insert into tblXpHist (WIP,Uname,AreaCreacion,NotasBeforeChange,Fecha)
                             values (@CWO,@User,'Almacen','Confirmado',GETDATE())"
                    cmd = New SqlCommand(query, cnn)
                    cmd.Parameters.Add("@CWO", SqlDbType.NVarChar).Value = lblcwomat.Text
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = UserName
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    ' Luego, confirmamos
                    Principal.poneokalm_apl(lblcwomat.Text, Label4.Text)
                    If flag = 1 Then Principal.notesWIPandCWOquitandoOnHold()
                    '-------------------------------------------------------------------------------
                    Me.Hide()
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
                CorreoFalla.EnviaCorreoFalla("Button1_Click'Materiales'", host, UserName)
                cnn.Close()
            End Try
        Else
            MsgBox("Debe escanear un numero de empleado para poder continuar", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub DateTimePicker1_MouseDown(sender As Object, e As MouseEventArgs) Handles DateTimePicker1.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.DateTimePicker1.Format = DateTimePickerFormat.Short
            If DateTimePicker1.Value = "2021-01-01" Then
                Me.DateTimePicker1.Value = Date.Today
            End If
        End If
    End Sub
    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        pintandoceldas()
    End Sub
    Private Sub dgvBOM_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvBOM.ColumnHeaderMouseClick
        pintandoceldas()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim pn As String = ""
            If DateTimePicker1.Value = "2021-01-01" Then
                MsgBox("Debe agregar fecha promesa", MessageBoxIcon.Warning)
                Exit Sub
            Else
                If txbNotas.Text <> "" Then
                    '-----------------------------
                    ' Aqui empieza la ejecucion de los querys, agregando a la persona asignada 
                    query = "update tblCWO set AsignadoA='" + nombreasig.ToString + "' where CWO= '" + lblcwomat.Text + "'"
                    cmd = New SqlCommand(query, cnn)
                    cmd.CommandType = CommandType.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    ' Ahora, leemos de nuevo el grid para ver que numeros de parte no hay stock y agregarlos al correo y a campo notas del WIp
                    ' para el campo notas Almnotas en el WIP, abria que agregar tambien en tblXPhistory o en la otra tabla de historial
                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                        If DataGridView1.Rows(i).Cells("Qty").Value > DataGridView1.Rows(i).Cells("Total").Value Then
                            If pn = "" Then
                                pn = DataGridView1.Rows(i).Cells("PN").Value.ToString
                            ElseIf pn <> "" Then
                                pn = pn + "; " + DataGridView1.Rows(i).Cells("PN").Value.ToString
                            End If
                        End If
                    Next
                    pn = pn + " No tienen stock y fueron confirmados en el CWO: " + lblcwomat.Text + ""
                    ' Una vez obtenidos los numeros de parte que no cuentan con stock, hacemos el query en tabla WIP campo Almnotas y 
                    ' tabla tblXPhistory 
                    query = "update tblWIP set AlmNotas=@nota, AlmFProm=@fprom where WIP= '" + Label4.Text + "'"
                    cmd = New SqlCommand(query, cnn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("@nota", SqlDbType.NVarChar).Value = pn
                    cmd.Parameters.Add("@fprom", SqlDbType.NVarChar).Value = DateTimePicker1.Text
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    '------------------------------------------------------------------------------
                    query = "insert into tblXpHist (WIP,AreaCreacion,NotasBeforeChange,Uname,Fecha)
                                 values (@wip,'Almacen',@nota,@user,GETDATE())"
                    cmd = New SqlCommand(query, cnn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("@wip", SqlDbType.NVarChar).Value = Label4.Text
                    cmd.Parameters.Add("@nota", SqlDbType.NVarChar).Value = pn
                    cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = UserName
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    '-------------------------------------------------------------------------------
                    ' Luego de esto, hacemos el envio del correo para que sea verificado que se confirmo con materiales sin stock
                    pn = pn + " y WIP: " + Label4.Text + ""
                    EnviaCorreo(pn)
                    '-------------------------------------------------------------------------------
                    ' Luego, confirmamos
                    Principal.poneokalm_apl(lblcwomat.Text, Label4.Text)
                    If flag = 1 Then Principal.notesWIPandCWOquitandoOnHold()
                    '-------------------------------------------------------------------------------
                    Me.Hide()
                Else
                    MsgBox("Debe agregar sus notas", MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            cnn.Close()
            CorreoFalla.EnviaCorreoFalla("Button2_Click'Materiales'", host, UserName)
        End Try
    End Sub
End Class