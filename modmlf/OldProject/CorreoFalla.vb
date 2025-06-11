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
                Correo = "Fallo la aplicacion, error potencialmente grave en funcion: " & Falla.ToString & ", Hostname que presento el error: " & hostanme.ToString & " y usuario: " & user.ToString & " " + vbNewLine + " Version del usuario: " & Application.ProductVersion & ""
            End If
            Correo += vbNewLine + vbNewLine + vbNewLine
            Correo += "Por favor no responder este correo" + vbNewLine + "Gracias"

            Dim _Message As New Net.Mail.MailMessage()
            Dim _SMTP As New Net.Mail.SmtpClient

            _SMTP.Credentials = New Net.NetworkCredential(EnviadoPor, "Row.6078$")
            _SMTP.Host = "smtp.ipower.com"
            _SMTP.Port = 587
            _SMTP.EnableSsl = True

            _Message.[To].Add(DestinatariosTO)
            _Message.From = New Net.Mail.MailAddress(EnviadoPor, "", Text.Encoding.UTF8)
            _Message.Subject = "Advertencia, falla de MLF"
            _Message.SubjectEncoding = Text.Encoding.UTF8

            _Message.Body = Correo
            _Message.BodyEncoding = Text.Encoding.UTF8
            _Message.Priority = Net.Mail.MailPriority.High

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

            Dim _Message As New Net.Mail.MailMessage()
            Dim _SMTP As New Net.Mail.SmtpClient

            _SMTP.Credentials = New Net.NetworkCredential(EnviadoPor, "Row.6078$")
            _SMTP.Host = "smtp.ipower.com"
            _SMTP.Port = 587
            _SMTP.EnableSsl = True

            If DestinatariosCC <> "" Then _Message.CC.Add(DestinatariosCC)
            If DestinatariosBCC <> "" Then _Message.Bcc.Add(DestinatariosBCC)
            _Message.[To].Add(DestinatariosTO)
            _Message.From = New Net.Mail.MailAddress(EnviadoPor, "", Text.Encoding.UTF8)
            _Message.Subject = "Material sin stock en Hold"
            _Message.SubjectEncoding = Text.Encoding.UTF8
            _Message.Body = Correo
            _Message.BodyEncoding = Text.Encoding.UTF8
            _Message.Priority = Net.Mail.MailPriority.High

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

            Dim _Message As New Net.Mail.MailMessage()
            Dim _SMTP As New Net.Mail.SmtpClient With {
                .Credentials = New Net.NetworkCredential(EnviadoPor, "Row.6078$"),
                .Host = "smtp.ipower.com",
                .Port = 587,
                .EnableSsl = True
            }

            If DestinatariosCC <> "" Then _Message.CC.Add(DestinatariosCC)
            If DestinatariosBCC <> "" Then _Message.Bcc.Add(DestinatariosBCC)
            _Message.[To].Add(DestinatariosTO)
            _Message.From = New Net.Mail.MailAddress(EnviadoPor, "", Text.Encoding.UTF8)
            _Message.Subject = "Material sin stock en Hold"
            _Message.SubjectEncoding = Text.Encoding.UTF8
            _Message.Body = Correo
            _Message.BodyEncoding = Text.Encoding.UTF8
            _Message.Priority = Net.Mail.MailPriority.High

            _Message.IsBodyHtml = False
            'ENVIO
            _SMTP.Send(_Message)
            'MsgBox("Se ha Enviado el Email", MsgBoxStyle.Information, "Email enviado")
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            CorreoFalla.EnviaCorreoFalla("EnviaCorreoHoldMat", host, UserName)
        End Try
    End Sub
    Private Function CargaDestinatarios(Modulo As String, OpcionEnvio As String)
        Dim Destinatarios As String = ""
        Try
            Dim Query As String = $"declare @Email as nvarchar(500)
		                                set @Email=''
			                             select @Email=@Email + T.Email + ',' from
			                             (SELECT Email FROM tblUserEmails WHERE Module='{Modulo}' AND Active=1 AND OptionToSend='{OpcionEnvio}') as T
		                                if @Email <> ''
			                             begin
				                          set @Email=LEFT(@Email, len(@Email) - 1)
				                          select @Email [Email]
			                             end
		                                else
			                             begin
				                          set @Email= 'S/A'
				                          select @Email [Email]
		                                end"
            cmd = New SqlCommand(Query, cnn)
            cmd.CommandType = CommandType.Text
            edo = cnn.State.ToString
            If edo = "Open" Then cnn.Close()
            cnn.Open()
            Destinatarios = CStr(cmd.ExecuteScalar())
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
        End Try
        Return Destinatarios
    End Function
End Module
