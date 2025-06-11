Public Class ReportePWO
    Dim PWOReceived As String
    Private Sub ReportePWO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInformation()
        Text = PWOReceived.ToString
        Me.ReportViewer1.RefreshReport()
    End Sub
    Public Sub New(PWO As String)

        ' This call is required by the designer.
        InitializeComponent()
        PWOReceived = PWO
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub LoadInformation()
        DataTable1TableAdapter1.Fill(DataSet11.DataTable1, PWOReceived)
        TimeOut()
        TblBOMPWOTableAdapter1.Fill(DataSet11.tblBOMPWO, PWOReceived)
        TimeOut()
        TblPWO1TableAdapter1.Fill(DataSet11.tblPWO1, PWOReceived)
        TimeOut()
        TblPWOTableAdapter1.Fill(DataSet11.tblPWO, PWOReceived)
        TimeOut()
        TblWIPTableAdapter1.Fill(DataSet11.tblWIP, PWOReceived)
        TimeOut()
        TblWipDetTableAdapter1.Fill(DataSet11.tblWipDet, PWOReceived)
    End Sub
    Private Function TimeOut()
        Threading.Thread.Sleep(2000)
        Return True
    End Function
End Class