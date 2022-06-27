Imports System.Data.SqlClient
Module Variables
    'Private strconexion As String = "Server=10.17.182.12\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
    'Private strconexion As String = "Server=10.17.182.36\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
    'Private strconexion As String = "Server=10.17.182.180\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
    Private strconexion As String = "Server=SHPLAPSIS01\SQLEXPRESS2012;Database=SEA;User ID=sa;Password=SHPadmin14%"
    Public cnn As New SqlConnection(strconexion)
    Public tb As New DataTable
    Public cmd As SqlCommand
    Public dr As SqlDataReader
    Public edo As String
    Public query As String = ""
    Public opcion As Integer
    'Public val As Integer = 0
    Public UserName As String = Environment.UserName
    Public campocortesolicitud As String
    Public CWO As String
    Public WIP As String
    Public ok1, ok2 As String
    Public op, sort, p, flag As Integer
    Public userID As String
    Public host As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString
End Module
