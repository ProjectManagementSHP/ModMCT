<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportePWO
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource4 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource5 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource6 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportePWO))
        Me.TblBOMPWOBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet11 = New modMCT.DataSet1()
        Me.TblPWOBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TblWIPBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TblWipDetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TblPWO1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.DataTable1TableAdapter1 = New modMCT.DataSet1TableAdapters.DataTable1TableAdapter()
        Me.TblBOMPWOTableAdapter1 = New modMCT.DataSet1TableAdapters.tblBOMPWOTableAdapter()
        Me.TblPWOTableAdapter1 = New modMCT.DataSet1TableAdapters.tblPWOTableAdapter()
        Me.TblWipDetTableAdapter1 = New modMCT.DataSet1TableAdapters.tblWipDetTableAdapter()
        Me.TblWIPTableAdapter1 = New modMCT.DataSet1TableAdapters.tblWIPTableAdapter()
        Me.TblPWO1TableAdapter1 = New modMCT.DataSet1TableAdapters.tblPWO1TableAdapter()
        CType(Me.TblBOMPWOBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TblPWOBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TblWIPBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TblWipDetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TblPWO1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TblBOMPWOBindingSource
        '
        Me.TblBOMPWOBindingSource.DataMember = "tblBOMPWO"
        Me.TblBOMPWOBindingSource.DataSource = Me.DataSet11
        '
        'DataSet11
        '
        Me.DataSet11.DataSetName = "DataSet1"
        Me.DataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TblPWOBindingSource
        '
        Me.TblPWOBindingSource.DataMember = "tblPWO"
        Me.TblPWOBindingSource.DataSource = Me.DataSet11
        '
        'DataTableBindingSource
        '
        Me.DataTableBindingSource.DataMember = "DataTable1"
        Me.DataTableBindingSource.DataSource = Me.DataSet11
        '
        'TblWIPBindingSource
        '
        Me.TblWIPBindingSource.DataMember = "tblWIP"
        Me.TblWIPBindingSource.DataSource = Me.DataSet11
        '
        'TblWipDetBindingSource
        '
        Me.TblWipDetBindingSource.DataMember = "tblWipDet"
        Me.TblWipDetBindingSource.DataSource = Me.DataSet11
        '
        'TblPWO1BindingSource
        '
        Me.TblPWO1BindingSource.DataMember = "tblPWO1"
        Me.TblPWO1BindingSource.DataSource = Me.DataSet11
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "PN_Information"
        ReportDataSource1.Value = Me.TblBOMPWOBindingSource
        ReportDataSource2.Name = "General_Information"
        ReportDataSource2.Value = Me.TblPWOBindingSource
        ReportDataSource3.Name = "Working_times"
        ReportDataSource3.Value = Me.DataTableBindingSource
        ReportDataSource4.Name = "WipContain"
        ReportDataSource4.Value = Me.TblWIPBindingSource
        ReportDataSource5.Name = "Details_cc"
        ReportDataSource5.Value = Me.TblWipDetBindingSource
        ReportDataSource6.Name = "InfoPWO"
        ReportDataSource6.Value = Me.TblPWO1BindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource3)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource4)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource5)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource6)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "modmlf.PWONew.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(942, 569)
        Me.ReportViewer1.TabIndex = 0
        '
        'DataTable1TableAdapter1
        '
        Me.DataTable1TableAdapter1.ClearBeforeFill = True
        '
        'TblBOMPWOTableAdapter1
        '
        Me.TblBOMPWOTableAdapter1.ClearBeforeFill = True
        '
        'TblPWOTableAdapter1
        '
        Me.TblPWOTableAdapter1.ClearBeforeFill = True
        '
        'TblWipDetTableAdapter1
        '
        Me.TblWipDetTableAdapter1.ClearBeforeFill = True
        '
        'TblWIPTableAdapter1
        '
        Me.TblWIPTableAdapter1.ClearBeforeFill = True
        '
        'TblPWO1TableAdapter1
        '
        Me.TblPWO1TableAdapter1.ClearBeforeFill = True
        '
        'ReportePWO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(942, 569)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ReportePWO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReportePWO"
        CType(Me.TblBOMPWOBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TblPWOBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TblWIPBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TblWipDetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TblPWO1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents DataTableBindingSource As BindingSource
    Friend WithEvents TblBOMPWOBindingSource As BindingSource
    Friend WithEvents TblPWOBindingSource As BindingSource
    Friend WithEvents TblWipDetBindingSource As BindingSource
    Friend WithEvents TblWIPBindingSource As BindingSource
    Friend WithEvents TblPWO1BindingSource As BindingSource
    Friend WithEvents DataTable1TableAdapter1 As DataSet1TableAdapters.DataTable1TableAdapter
    Friend WithEvents TblBOMPWOTableAdapter1 As DataSet1TableAdapters.tblBOMPWOTableAdapter
    Friend WithEvents TblPWOTableAdapter1 As DataSet1TableAdapters.tblPWOTableAdapter
    Friend WithEvents TblWipDetTableAdapter1 As DataSet1TableAdapters.tblWipDetTableAdapter
    Friend WithEvents TblWIPTableAdapter1 As DataSet1TableAdapters.tblWIPTableAdapter
    Friend WithEvents TblPWO1TableAdapter1 As DataSet1TableAdapters.tblPWO1TableAdapter
    Friend WithEvents DataSet11 As DataSet1
End Class
