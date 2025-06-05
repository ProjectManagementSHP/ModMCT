<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCircuitosLoc
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvTerms = New System.Windows.Forms.DataGridView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgvTermsDet = New System.Windows.Forms.DataGridView()
        Me.cmbSelector = New System.Windows.Forms.ComboBox()
        Me.cmbCelda = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvSinCelda = New System.Windows.Forms.DataGridView()
        CType(Me.dgvTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTermsDet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvSinCelda, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvTerms
        '
        Me.dgvTerms.AllowUserToAddRows = False
        Me.dgvTerms.AllowUserToDeleteRows = False
        Me.dgvTerms.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTerms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTerms.Location = New System.Drawing.Point(12, 67)
        Me.dgvTerms.Name = "dgvTerms"
        Me.dgvTerms.Size = New System.Drawing.Size(1080, 232)
        Me.dgvTerms.TabIndex = 52
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(934, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 17)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Semana Actual:"
        '
        'dgvTermsDet
        '
        Me.dgvTermsDet.AllowUserToAddRows = False
        Me.dgvTermsDet.AllowUserToDeleteRows = False
        Me.dgvTermsDet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTermsDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTermsDet.Location = New System.Drawing.Point(24, 15)
        Me.dgvTermsDet.Name = "dgvTermsDet"
        Me.dgvTermsDet.Size = New System.Drawing.Size(1051, 253)
        Me.dgvTermsDet.TabIndex = 54
        '
        'cmbSelector
        '
        Me.cmbSelector.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSelector.FormattingEnabled = True
        Me.cmbSelector.Items.AddRange(New Object() {"PRESS", "SPLICE"})
        Me.cmbSelector.Location = New System.Drawing.Point(12, 19)
        Me.cmbSelector.Name = "cmbSelector"
        Me.cmbSelector.Size = New System.Drawing.Size(174, 33)
        Me.cmbSelector.TabIndex = 55
        Me.cmbSelector.Text = "PRESS"
        '
        'cmbCelda
        '
        Me.cmbCelda.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCelda.FormattingEnabled = True
        Me.cmbCelda.Items.AddRange(New Object() {"A1"})
        Me.cmbCelda.Location = New System.Drawing.Point(206, 19)
        Me.cmbCelda.Name = "cmbCelda"
        Me.cmbCelda.Size = New System.Drawing.Size(174, 33)
        Me.cmbCelda.TabIndex = 57
        Me.cmbCelda.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(13, 328)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1090, 312)
        Me.TabControl1.TabIndex = 58
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvTermsDet)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 3, 3, 3)
        Me.TabPage1.Size = New System.Drawing.Size(1082, 286)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detalle"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvSinCelda)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3, 3, 3, 3)
        Me.TabPage2.Size = New System.Drawing.Size(1082, 286)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sin Celda"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvSinCelda
        '
        Me.dgvSinCelda.AllowUserToAddRows = False
        Me.dgvSinCelda.AllowUserToDeleteRows = False
        Me.dgvSinCelda.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSinCelda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSinCelda.Location = New System.Drawing.Point(16, 17)
        Me.dgvSinCelda.Name = "dgvSinCelda"
        Me.dgvSinCelda.Size = New System.Drawing.Size(1051, 253)
        Me.dgvSinCelda.TabIndex = 55
        '
        'frmCircuitosLoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 645)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmbCelda)
        Me.Controls.Add(Me.cmbSelector)
        Me.Controls.Add(Me.dgvTerms)
        Me.Controls.Add(Me.Label8)
        Me.Name = "frmCircuitosLoc"
        Me.Text = "MasterCircuitTracker"
        CType(Me.dgvTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTermsDet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvSinCelda, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvTerms As DataGridView
    Friend WithEvents Label8 As Label
    Friend WithEvents dgvTermsDet As DataGridView
    Friend WithEvents cmbSelector As ComboBox
    Friend WithEvents cmbCelda As ComboBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents dgvSinCelda As DataGridView
End Class
