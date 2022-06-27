<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DesviacionesTerm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DesviacionesTerm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.WIP = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CurrentTerminal = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TerminalNew = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgvTerminales = New System.Windows.Forms.DataGridView()
        Me.Chk = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgvTerminalesValidadas = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.dgvTerminales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTerminalesValidadas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "WIP:"
        '
        'WIP
        '
        Me.WIP.AutoSize = True
        Me.WIP.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WIP.Location = New System.Drawing.Point(124, 23)
        Me.WIP.Name = "WIP"
        Me.WIP.Size = New System.Drawing.Size(17, 24)
        Me.WIP.TabIndex = 1
        Me.WIP.Text = "-"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(113, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 24)
        Me.Label3.TabIndex = 5437
        Me.Label3.Text = "Terminal actual:"
        '
        'CurrentTerminal
        '
        Me.CurrentTerminal.AutoSize = True
        Me.CurrentTerminal.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurrentTerminal.Location = New System.Drawing.Point(125, 121)
        Me.CurrentTerminal.Name = "CurrentTerminal"
        Me.CurrentTerminal.Size = New System.Drawing.Size(17, 24)
        Me.CurrentTerminal.TabIndex = 5438
        Me.CurrentTerminal.Text = "-"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(555, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(156, 24)
        Me.Label5.TabIndex = 5439
        Me.Label5.Text = "Terminal nueva:"
        '
        'TerminalNew
        '
        Me.TerminalNew.AutoSize = True
        Me.TerminalNew.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TerminalNew.Location = New System.Drawing.Point(565, 121)
        Me.TerminalNew.Name = "TerminalNew"
        Me.TerminalNew.Size = New System.Drawing.Size(17, 24)
        Me.TerminalNew.TabIndex = 5440
        Me.TerminalNew.Text = "-"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(40, 216)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(197, 24)
        Me.Label7.TabIndex = 5442
        Me.Label7.Text = "Lista de Terminales:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(519, 216)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(211, 24)
        Me.Label8.TabIndex = 5443
        Me.Label8.Text = "Terminales validadas:"
        '
        'dgvTerminales
        '
        Me.dgvTerminales.AllowUserToAddRows = False
        Me.dgvTerminales.AllowUserToDeleteRows = False
        Me.dgvTerminales.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTerminales.BackgroundColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGreen
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial Unicode MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTerminales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTerminales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTerminales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Chk})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTerminales.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTerminales.EnableHeadersVisualStyles = False
        Me.dgvTerminales.GridColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvTerminales.Location = New System.Drawing.Point(29, 252)
        Me.dgvTerminales.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvTerminales.Name = "dgvTerminales"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial Unicode MS", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTerminales.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTerminales.RowHeadersVisible = False
        Me.dgvTerminales.RowHeadersWidth = 51
        Me.dgvTerminales.RowTemplate.Height = 24
        Me.dgvTerminales.Size = New System.Drawing.Size(366, 139)
        Me.dgvTerminales.TabIndex = 5459
        '
        'Chk
        '
        Me.Chk.HeaderText = "Check"
        Me.Chk.MinimumWidth = 6
        Me.Chk.Name = "Chk"
        Me.Chk.Width = 125
        '
        'dgvTerminalesValidadas
        '
        Me.dgvTerminalesValidadas.AllowUserToAddRows = False
        Me.dgvTerminalesValidadas.AllowUserToDeleteRows = False
        Me.dgvTerminalesValidadas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTerminalesValidadas.BackgroundColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGreen
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial Unicode MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTerminalesValidadas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvTerminalesValidadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTerminalesValidadas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Check})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTerminalesValidadas.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvTerminalesValidadas.EnableHeadersVisualStyles = False
        Me.dgvTerminalesValidadas.GridColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvTerminalesValidadas.Location = New System.Drawing.Point(523, 252)
        Me.dgvTerminalesValidadas.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvTerminalesValidadas.Name = "dgvTerminalesValidadas"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial Unicode MS", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTerminalesValidadas.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvTerminalesValidadas.RowHeadersVisible = False
        Me.dgvTerminalesValidadas.RowHeadersWidth = 51
        Me.dgvTerminalesValidadas.RowTemplate.Height = 24
        Me.dgvTerminalesValidadas.Size = New System.Drawing.Size(366, 139)
        Me.dgvTerminalesValidadas.TabIndex = 5460
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Chocolate
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(789, 23)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(120, 22)
        Me.Button2.TabIndex = 5461
        Me.Button2.Text = "Efectuar Cambios"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Check
        '
        Me.Check.HeaderText = "Check"
        Me.Check.MinimumWidth = 6
        Me.Check.Name = "Check"
        Me.Check.Width = 125
        '
        'DesviacionesTerm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(940, 411)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.dgvTerminalesValidadas)
        Me.Controls.Add(Me.dgvTerminales)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TerminalNew)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CurrentTerminal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.WIP)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DesviacionesTerm"
        Me.Text = "Desviaciones Temporales"
        CType(Me.dgvTerminales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTerminalesValidadas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents WIP As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CurrentTerminal As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TerminalNew As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents dgvTerminales As DataGridView
    Friend WithEvents Chk As DataGridViewCheckBoxColumn
    Friend WithEvents dgvTerminalesValidadas As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents Check As DataGridViewCheckBoxColumn
End Class
