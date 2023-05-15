Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net.NetworkInformation
Imports System.Threading

Module Variables

	Public strconexion As String = "Server=10.17.182.12\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
	'Public strconexion As String = "Server=10.17.182.36\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
	'Private strconexion As String = "Server=10.17.182.255\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
	'Public strconexion As String = "Server=SHPLAPSIS01\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
	Public cnn As New SqlConnection(strconexion) 'Conexion Principal
	Public conex As New SqlConnection(strconexion) 'Graficas de MLF 
	Public conexOne As New SqlConnection(strconexion) 'Graficas de Planeacion
	Public conexNotify As New SqlConnection(strconexion) 'notificaciones
	Public conexMensajeCortos As New SqlConnection(strconexion) 'notificaciones
	Public conexPWO As New SqlConnection(strconexion) 'CreandoPWO
	Public tb As New DataTable
	Public cmd As SqlCommand
	Public dr As SqlDataReader
	Public da As SqlDataAdapter
	Public edo As String
	Public query As String = ""
	Public opcion As Integer
	Public IsAdmin As Boolean
	Public UserName As String = Environment.UserName
	Public campocortesolicitud As String
	Public CWO As String, ver As String, Cell As String
	Public WIP As String, PN As String
	Public ColaGrafica As Boolean = False, flagActualizacion As Boolean = False
	Public FlagFechas As Boolean = False
	Public op, sort, p, flag, maq, cola As Integer
	Public userID As String
	Public host As String = Security.Principal.WindowsIdentity.GetCurrent().Name.ToString
	Public opcionesDeExportacion As Integer
	Public nsemana As Integer = GetWeek()
	Public MultiDepart As Boolean = False, LogOut As Boolean = False
	Private bgWorker As New BackgroundWorker
	Public Function GetWeek()
		Return CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(Date.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday) 'DateDiff(DateInterval.WeekOfYear, New DateTime(Date.Now.Year, 1, 1), Date.Now) Comentado por bug sem 0
	End Function
	Public Function TimeOut_OpenConnection()
		cnn.Close()
		cnn.Dispose()
		Thread.Sleep(4000)
		cnn = New SqlConnection(strconexion)
		cnn.Open()
		If cnn.State.ToString = "Open" Then
			cnn.Close()
			Return True
		Else
			MessageBox.Show("Hay un problema con la conexion, cierra la app para refrescar la conexion con el servidor")
		End If
		cnn.Close()
		Return False
	End Function
	Public Sub Emergency_CloseApp()
		If Not LogOut Then
			bgWorker.Dispose()
			With Principal.NotifyIcon1
				.Visible = False
				.Dispose()
			End With
			Application.Exit()
			End
		End If
	End Sub
	Public Function CheckInternet()
		Dim response As Boolean = False
		Dim myPing As New Ping()
		Dim host As String = "10.17.182.12"
		Dim buffer As Byte() = New Byte(31) {}
		Dim timeout As Integer = &B101110111000
		Dim pingOptions As New PingOptions()
		Dim reply As PingReply = myPing.Send(host, timeout, buffer, pingOptions)
		response = reply.Status = IPStatus.Success
		Return response
	End Function
End Module
