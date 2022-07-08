Imports System.Data.SqlClient
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
    Public tb As New DataTable
    Public cmd As SqlCommand
    Public dr As SqlDataReader
    Public da As SqlDataAdapter
    Public edo As String
    Public query As String = ""
    Public opcion As Integer
    Public UserName As String = Environment.UserName
    Public campocortesolicitud As String
    Public CWO As String, ver As String
    Public WIP As String, PN As String
    Public ColaGrafica As Boolean = False
    Public op, sort, p, flag, maq, flagActualizacion, cola As Integer
    Public userID As String
    Public host As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString
    Public opcionesDeExportacion As Integer
    Public nsemana As Integer = DateDiff(DateInterval.WeekOfYear, New DateTime(Date.Now.Year, 1, 1), Date.Now)
End Module
