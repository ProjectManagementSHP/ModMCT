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
    Private nombreasig As String = "", pn As String = ""
    Private holdoconfir As Integer = 0
    Dim FilaSeleccionada As String
    Private Sub Materiales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = Nothing
        dgvBOM.DataSource = Nothing
        TextBox2.Clear()
        If opcion = 2 Or opcion = 5 Then
            Label1.Text = "Materiales BOM"
            LinkLabel1.Text = "Almacen"
            If p = 1 Then 'Bandera de confirmacion
                'GroupBox2.Visible = True
                DataGridView1.Visible = True
                dgvBOM.Visible = False
                llenagrid2()
                p = 0
            ElseIf p = 12 Then 'Bandera de Hold
                Button3.Visible = True
                GroupBox2.Visible = False
                dgvBOM.Visible = False
                DataGridView1.Visible = True
                CheckBox1.Visible = False
                gbCompras.Visible = False
                llenagrid3()
                p = 0
            Else ' Solo ver materiales (Almacen y Compras)
                GroupBox2.Visible = False
                dgvBOM.Visible = True
                DataGridView1.Visible = False
                gbCompras.Visible = False
                CheckBox1.Visible = False
                Me.AutoScroll = False
                llenagrid()
            End If
        ElseIf opcion = 3 Then ' Ver aplicadores
            Label1.Text = "Aplicadores"
            LinkLabel1.Text = "ACS"
            'Me.AutoScroll = False
            CheckBox1.Visible = False
            DataGridView3.Visible = True
            llenagrid1()
        End If
        Label4.Visible = False
        DateTimePicker2.Value = "2021-01-01"
        DateTimePicker2.Visible = False
        gbCompras.Visible = False
    End Sub
    Private Sub llenagrid() 'Parte de ver los materiales
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
    Private Sub llenagrid1() 'Parte de aplicadores, validaciones, y disponibilidad de aplicadores
        Dim query As String = "select Wire,TermA,SUBSTRING(isnull(AplicatorA,0),0,4)[AplicatorA],IDKeyA,MaqA,TermB,SUBSTRING(isnull(AplicatorB,0),0,4)[AplicatorB],
                               IDKeyB,MaqB,PTA, QAPTA,EngPTA,WTAWG,WC,WA,IC,IA 
                               from tblWipDet inner join tblTermSpecs on tblTermSpecs.IDKey=tblWipDet.IDKeyA where CWO='" + lblcwomat.Text + "' 
                               group by Wire,TermA,AplicatorA,IDKeyA,MaqA,TermB,AplicatorB,IDKeyB,MaqB,PTA, QAPTA,EngPTA,WTAWG,WC,WA,IC,IA"
        Dim tabla As New DataTable, tb As New DataTable
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
            'recorregrid()
            query = "select AU,WID,Wire,LengthWire,TermA,MaqA,TermB,MaqB,WIP from tblWipDet where CWO = '" + lblcwomat.Text + "' order by WireID"
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            With DataGridView3
                .DataSource = tb
                .AutoResizeColumns()
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("llenagrid1'Materiales'", host, UserName)
        End Try
    End Sub
    Private Sub llenagrid2() 'Materiales para confirmar
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
    Private Sub llenagrid3() 'Materiales sin stock para poner en hold
        Dim query As String = "Select bw.PN,bw.Description,CONVERT(int,bw.Qty) [Qty] ,CONVERT(int,bw.Balance) [Balance],
                               null [En Piso], null [Almacen],
                               Convert(Int, ml.QtyOnHand) [Total],
                               Convert(Int, ml.QtyOnHand) - CONVERT(int,bw.Balance) [Dif],
                               Convert(Int, ml.QtyOnOrder) [In Transit]
                               From tblBOMCWO As bw inner Join tblItemsQB As ml On bw.PN = ml.PN Where bw.CWO ='" + lblcwomat.Text + "' and ml.QtyOnHand = 0 
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
            CorreoFalla.EnviaCorreoFalla("llenagrid3'Materiales'", host, UserName)
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
            If linea.Cells(3).Value <= linea.Cells(6).Value Then
                linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
            Else
                linea.DefaultCellStyle.BackColor = Color.Yellow
            End If
        Next
        For Each linea As DataGridViewRow In DataGridView1.Rows
            If linea.Cells(3).Value <= linea.Cells(7).Value Then
                linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
            Else
                linea.DefaultCellStyle.BackColor = Color.Red
            End If
            If linea.Cells(4).Value <= linea.Cells(7).Value Then
                linea.DefaultCellStyle.BackColor = Color.LightSeaGreen
            Else
                linea.DefaultCellStyle.BackColor = Color.Yellow
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
    Private Sub EnviaCorreoHoldMat(mensaje As String)
        Try
            Dim DestinatariosTO As String = CargaDestinatarios("ComprasMLFholdAlm", "TO")
            'Dim DestinatariosCC As String = CargaDestinatarios("MatSinStock", "CC")
            'Dim DestinatariosBCC As String = CargaDestinatarios("MatSinStock", "BCC")
            Dim EnviadoPor As String = "shp.app@specializedharness.com"
            Dim Correo As String
            Correo = mensaje

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
            _Message.Subject = "Material sin stock en Hold"
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
            CorreoFalla.EnviaCorreoFalla("EnviaCorreoHoldMat", host, UserName)
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
                            gbCompras.Visible = True
                            DateTimePicker2.Visible = True
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
                        Me.Close()
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
                    Me.Close()
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
    Private Sub DateTimePicker2_MouseDown(sender As Object, e As MouseEventArgs) Handles DateTimePicker2.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.DateTimePicker2.Format = DateTimePickerFormat.Short
            If DateTimePicker2.Value = "2021-01-01" Then
                Me.DateTimePicker2.Value = Date.Today
            End If
        End If
    End Sub
    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        pintandoceldas()
    End Sub
    Private Sub dgvBOM_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvBOM.ColumnHeaderMouseClick
        pintandoceldas()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If holdoconfir = 1 Then
            If TextBox3.Text <> "" Then
                Dim mensaje As String = ""
                If DataGridView1.Rows.Count > 0 Then
                    For o As Integer = 0 To DataGridView1.Rows.Count - 1
                        If DataGridView1.Rows(o).Cells("Check").Value = True Then
                            If mensaje = "" Then
                                mensaje = DataGridView1.Rows(o).Cells("PN").Value.ToString
                            ElseIf mensaje <> "" Then
                                mensaje = mensaje + "; " + DataGridView1.Rows(o).Cells("PN").Value.ToString
                            End If
                        End If
                    Next
                    mensaje = mensaje + " se han puesto en Hold por falta de material, en el WIP: " & Label4.Text & " y CWO: " & lblcwomat.Text & " por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + "" + vbNewLine + " Verificalo y asigna una nueva fecha de material."
                Else
                    mensaje = "Se notifica que se pone el WIP: " & Label4.Text & " y CWO: " & lblcwomat.Text & " en Hold por el usuario " + UserName.ToString + " del departamento de " + Principal.lbldept.Text + " pero," + vbNewLine + " sin numeros de parte que esten sin stock, por favor verificarlo"
                End If
                EnviaCorreoHoldMat(mensaje)
                Principal.NotifyIcon1.BalloonTipText = "Se han notificado los cambios a Compras"
                Principal.NotifyIcon1.BalloonTipTitle = "Material sin stock"
                Principal.NotifyIcon1.Visible = True
                Principal.NotifyIcon1.ShowBalloonTip(0)
                Principal.notesWIPandCWOOnHold(lblcwomat.Text, "2022-12-01", txbNotas.Text)
                holdoconfir = 0
                Me.Close()
            Else
                MsgBox("Debe agregar una nota")
            End If
        Else
            Try
                Dim pn As String = ""
                If TextBox3.Text <> "" Then
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
                    cmd.Parameters.Add("@fprom", SqlDbType.NVarChar).Value = DateTimePicker2.Text
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
                    Me.Close()
                Else
                    MsgBox("Debe agregar sus notas", MessageBoxIcon.Warning)
                    Exit Sub
                End If
            Catch ex As Exception
                cnn.Close()
                CorreoFalla.EnviaCorreoFalla("Button2_Click'Materiales'", host, UserName)
            End Try
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        holdoconfir = 1
        gbCompras.Visible = True
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            gbTAGS.Visible = True
            txbTAGSxentrar.Focus()
        ElseIf CheckBox1.Checked = False Then
            gbTAGS.Visible = False
        End If
    End Sub
    ' -------------- Parte para reserva de los materiales para el CWO ----------------
    Private Sub txbTAGSxentrar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txbTAGSxentrar.KeyPress
        Try
            Dim tags As New DataTable
            Dim add As ArrayList = New ArrayList()
            Dim existpn As Integer = 0
            Dim validaBalance As Boolean '= Nothing
            If e.KeyChar = Chr(13) Then
                If verificatag(txbTAGSxentrar.Text) = "YES" Then
                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                        If pn = DataGridView1.Rows(i).Cells("PN").Value.ToString Then
                            existpn = 1
                            If CInt(Val(DataGridView1.Rows(i).Cells("Balance").Value.ToString)) > 0 Then
                                validaBalance = True
                            Else
                                MsgBox("El PN: " + pn.ToString + " que escaneaste ya no tiene balance")
                                validaBalance = False
                                Exit For
                            End If
                        End If
                    Next
                    If existpn = 1 And validaBalance = True Then
                        If DataGridView2.Rows.Count = 0 Then
                            Me.DataGridView2.Rows.Add()
                            DataGridView2.Rows(0).Cells(0).Value = txbTAGSxentrar.Text.ToString
                            DataGridView2.Rows(0).Cells(1).Value = pn.ToString
                        ElseIf DataGridView2.Rows.Count > 0 Then
                            Me.DataGridView2.Rows.Add()
                            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                                If DataGridView2.Rows(i).Cells(0).Value = Nothing Then
                                    DataGridView2.Rows(i).Cells(0).Value = txbTAGSxentrar.Text.ToString
                                    DataGridView2.Rows(i).Cells(1).Value = pn.ToString
                                End If
                            Next
                        End If
                        txbTAGSxentrar.Clear()
                        txbTAGSxentrar.Focus()
                        lbltagsescaneados.Text = "Tag's Escaneados: " & CInt(Val(DataGridView2.Rows.Count))
                    Else
                        MsgBox("El Tag escaneado: " + txbTAGSxentrar.Text + " no contiene PN para este CWO" + vbNewLine + " Escanee otro")
                        txbTAGSxentrar.Clear()
                        txbTAGSxentrar.Focus()
                    End If
                Else
                    MsgBox("El tag que intentas escanear: " + txbTAGSxentrar.Text + " ya esta asignado a piso o a otro CWO")
                End If
            End If
        Catch ex As Exception
            CorreoFalla.EnviaCorreoFalla("txbTAGSxentrar_KeyPress()", host, UserName)
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub btnTerminarescaneo_Click(sender As Object, e As EventArgs) Handles btnTerminarescaneo.Click
        Try
            If DataGridView2.Rows.Count > 0 Then
                Dim subquery As String = ""
                For I As Integer = 0 To DataGridView2.Rows.Count - 1
                    Dim consulta As String = "update tblItemsTags set AssignedTo='" + lblcwomat.Text.ToString + "' where TAG='" + DataGridView2.Rows(I).Cells(0).Value.ToString + "'"
                    Dim cmdo As SqlCommand = New SqlCommand(consulta, cnn)
                    cmdo.CommandType = CommandType.Text
                    cnn.Open()
                    cmdo.ExecuteReader()
                    cnn.Close()
                Next
                For a As Integer = 0 To DataGridView2.Rows.Count - 1
                    If subquery = "" Then
                        subquery = "'" & DataGridView2.Rows(a).Cells(1).Value.ToString & "',"
                    Else
                        subquery = subquery + "'" & DataGridView2.Rows(a).Cells(1).Value.ToString & "',"
                    End If
                Next
                If Microsoft.VisualBasic.Right(subquery, 1) = "," Then
                    subquery = subquery.TrimEnd(",")
                End If
                Dim update As String = "update tblBOMCWO set TagAsignado=1 where CWO= '" + lblcwomat.Text.ToString + "' and PN in (" + subquery + ")"
                Dim comm As SqlCommand = New SqlCommand(update, cnn)
                comm.CommandType = CommandType.Text
                cnn.Open()
                comm.ExecuteReader()
                cnn.Close()
                GroupBox2.Visible = True
                DataGridView2.DataSource = Nothing
                DataGridView2.Refresh()
                btnGenerarReporte.Visible = True
            Else
                MsgBox("No hay materiales para cargar al CWO")
            End If
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.ToString)
            CorreoFalla.EnviaCorreoFalla("btnTerminarescaneo_Click", host, UserName)
        End Try
    End Sub
    Private Sub DataGridView2_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDown
        If e.RowIndex <> -1 And e.ColumnIndex <> -1 Then
            If e.Button = MouseButtons.Right Then
                Try
                    DataGridView2.CurrentCell = DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    DataGridView2.Rows(e.RowIndex).Selected = True
                    FilaSeleccionada = Convert.ToString(DataGridView2.Rows(e.RowIndex).Cells(0).Value)
                Catch ex As Exception
                    MsgBox(ex.ToString)
                    CorreoFalla.EnviaCorreoFalla("DataGridView2_CellMouseDown", host, UserName)
                End Try
            End If
        End If
    End Sub
    Private Sub DataGridView2_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView2.MouseClick
        If DataGridView2.RowCount > 0 Then
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                eliminartagmenu.Show(Cursor.Position.X, Cursor.Position.Y)
            End If
        End If
    End Sub
    Private Sub ToolStripTextBox1_Click(sender As Object, e As EventArgs) Handles ToolStripTextBox1.Click
        If DataGridView2.RowCount > 0 Then
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                If DataGridView2.Rows(i).Cells(0).Value.ToString = FilaSeleccionada Then
                    DataGridView2.Rows.Remove(DataGridView2.Rows(i))
                End If
            Next
            eliminartagmenu.Close()
        End If
    End Sub
    Private Function verificatag(tag As String) 'Nuevo
        Dim res As String
        Dim query As String = "select PN from tblItemsTags where TAG = @tag and Status = 'Available' and Balance > 0 and AssignedTo is null"
        Dim tb As New DataTable
        Try
            cmd = New SqlCommand(query, cnn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("@tag", SqlDbType.NVarChar).Value = tag
            cnn.Open()
            dr = cmd.ExecuteReader
            tb.Load(dr)
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            If tb.Rows.Count > 0 Then
                res = "YES"
                pn = tb.Rows(0).Item("PN").ToString
            Else
                res = "NO"
            End If
            Return res
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
End Class