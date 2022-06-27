Imports System.Data.SqlClient
Module cnnACS
    Private conexionacs As String = "Server=10.17.182.12\SQLEXPRESS2012;Database=ACS;User ID=sa;Password=SHPadmin14%"
    Public conexion As New SqlConnection(conexionacs)
    Public comando As SqlCommand
    Public read As SqlDataReader
End Module
