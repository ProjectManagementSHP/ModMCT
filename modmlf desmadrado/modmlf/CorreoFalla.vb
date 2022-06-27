Imports System.Data.SqlClient
Module CorreoFalla

    Public Sub EnviaCorreoFalla(Falla As String, hostanme As String, user As String)
        Try
            Dim DestinatariosTO As String = CargaDestinatarios("fallamodMLF", "TO")
            Dim EnviadoPor As String = "shp.app@specializedharness.com"
            Dim Correo As String = ""
            Correo = "Fallo la aplicacion, error potencialmente grave en funcion: " & Falla.ToString & ", Hostname que presento el error: " & hostanme.ToString & " y usuario: " & user.ToString
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
            MsgBox(ex.Message.ToString)
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
                cnn.Open()
                dr = cmd.ExecuteReader
                TE.Load(dr)
                edo = cnn.State.ToString
                If edo = "Open" Then cnn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Loading tblMaster")
                EnviaCorreoFalla("CargaDestinatarios", host, UserName)
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
