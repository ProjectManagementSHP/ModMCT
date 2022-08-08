Imports System.Data.SqlClient
Imports System.Deployment.Application
Module CorreoFalla

    Public Sub EnviaCorreoFalla(Falla As String, hostanme As String, user As String)
        Try
            Dim DestinatariosTO As String = CargaDestinatarios("fallamodMLF", "TO")
            Dim EnviadoPor As String = "shp.app@specializedharness.com"
            Dim Correo As String = ""
            If (ApplicationDeployment.IsNetworkDeployed) Then
                With ApplicationDeployment.CurrentDeployment.CurrentVersion
                    Correo = "Fallo la aplicacion, error potencialmente grave en funcion: " & Falla.ToString & ", Hostname que presento el error: " & hostanme.ToString & " y usuario: " & user.ToString & " " + vbNewLine + " Version del usuario: " & .Major & "." & .Minor & "." & .Build & "." & .Revision & ""
                End With
            Else
                Correo = "Fallo la aplicacion, error potencialmente grave en funcion: " & Falla.ToString & ", Hostname que presento el error: " & hostanme.ToString & " y usuario: " & user.ToString & " " + vbNewLine + " Version del usuario: " & System.Windows.Forms.Application.ProductVersion & ""
            End If
            Correo += vbNewLine + vbNewLine + vbNewLine
            Correo += "Por favor no responder este correo" + vbNewLine + "Gracias"

            Dim _Message As New System.Net.Mail.MailMessage()
            Dim _SMTP As New System.Net.Mail.SmtpClient

            _SMTP.Credentials = New System.Net.NetworkCredential(EnviadoPor, "Row.6078$")
            _SMTP.Host = "smtp.ipower.com"
            _SMTP.Port = 587
            _SMTP.EnableSsl = True

            _Message.[To].Add(DestinatariosTO)
            _Message.From = New System.Net.Mail.MailAddress(EnviadoPor, "", System.Text.Encoding.UTF8)
            _Message.Subject = "Advertencia, falla de MLF"
            _Message.SubjectEncoding = System.Text.Encoding.UTF8

            _Message.Body = Correo
            _Message.BodyEncoding = System.Text.Encoding.UTF8
            _Message.Priority = System.Net.Mail.MailPriority.High

            _Message.IsBodyHtml = False
            'ENVIO
            _SMTP.Send(_Message)
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Public Sub EnviaCorreoHoldMat(mensaje As String)
        Try
            Dim DestinatariosTO As String = CargaDestinatarios("ComprasMLFholdAlm", "TO")
            Dim DestinatariosCC As String = CargaDestinatarios("ComprasMLFholdAlm", "CC")
            Dim DestinatariosBCC As String = CargaDestinatarios("ComprasMLFholdAlm", "BCC")
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

            If DestinatariosCC <> "" Then _Message.CC.Add(DestinatariosCC)
            If DestinatariosBCC <> "" Then _Message.Bcc.Add(DestinatariosBCC)
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
            'MsgBox(ex.Message.ToString)
            CorreoFalla.EnviaCorreoFalla("EnviaCorreoHoldMat", host, UserName)
        End Try
    End Sub
    Public Sub EnviaCorreoHoldMatPorCompras(mensaje As String)
        Try
            Dim DestinatariosTO As String = CargaDestinatarios("ComprasMLFholdComp", "TO")
            Dim DestinatariosCC As String = CargaDestinatarios("ComprasMLFholdComp", "CC")
            Dim DestinatariosBCC As String = CargaDestinatarios("ComprasMLFholdComp", "BCC")
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

            If DestinatariosCC <> "" Then _Message.CC.Add(DestinatariosCC)
            If DestinatariosBCC <> "" Then _Message.Bcc.Add(DestinatariosBCC)
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
            'MsgBox(ex.Message.ToString)
            CorreoFalla.EnviaCorreoFalla("EnviaCorreoHoldMat", host, UserName)
        End Try
    End Sub
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
                If edo = "Open" Then cnn.Close()
                edo = cnn.State.ToString
                If edo = "Close" Then cnn.Open()
                edo = cnn.State.ToString
                cnn.Open()
                dr = cmd.ExecuteReader
                TE.Load(dr)
                edo = cnn.State.ToString
                If edo = "Open" Then cnn.Close()
            Catch ex As Exception
                'MessageBox.Show(ex.Message, "Error Loading tblMaster")
                If edo = "Open" Then cnn.Close()
                'EnviaCorreoFalla("CargaDestinatarios", host, UserName)
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
End Module
