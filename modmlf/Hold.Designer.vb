<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Hold
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
        Me.gbCompras = New System.Windows.Forms.GroupBox()
        Me.dtpFProm = New System.Windows.Forms.DateTimePicker()
        Me.txbNotas = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblcwoporsolicitar = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblwipporsolicitar = New System.Windows.Forms.Label()
        Me.gbCompras.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbCompras
        '
        Me.gbCompras.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbCompras.BackColor = System.Drawing.Color.Transparent
        Me.gbCompras.Controls.Add(Me.dtpFProm)
        Me.gbCompras.Controls.Add(Me.txbNotas)
        Me.gbCompras.Controls.Add(Me.Label1)
        Me.gbCompras.Location = New System.Drawing.Point(19, 73)
        Me.gbCompras.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbCompras.Name = "gbCompras"
        Me.gbCompras.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbCompras.Size = New System.Drawing.Size(275, 165)
        Me.gbCompras.TabIndex = 57
        Me.gbCompras.TabStop = False
        '
        'dtpFProm
        '
        Me.dtpFProm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFProm.Location = New System.Drawing.Point(99, 12)
        Me.dtpFProm.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dtpFProm.MinDate = New Date(2021, 1, 1, 0, 0, 0, 0)
        Me.dtpFProm.Name = "dtpFProm"
        Me.dtpFProm.Size = New System.Drawing.Size(148, 22)
        Me.dtpFProm.TabIndex = 20
        '
        'txbNotas
        '
        Me.txbNotas.Location = New System.Drawing.Point(17, 41)
        Me.txbNotas.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txbNotas.Multiline = True
        Me.txbNotas.Name = "txbNotas"
        Me.txbNotas.Size = New System.Drawing.Size(229, 114)
        Me.txbNotas.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 18)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "FPROM:"
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnGuardar.FlatAppearance.BorderSize = 0
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(171, 246)
        Me.btnGuardar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(123, 28)
        Me.btnGuardar.TabIndex = 59
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 19)
        Me.Label4.TabIndex = 5445
        Me.Label4.Text = "CWO:"
        '
        'lblcwoporsolicitar
        '
        Me.lblcwoporsolicitar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcwoporsolicitar.AutoSize = True
        Me.lblcwoporsolicitar.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcwoporsolicitar.Location = New System.Drawing.Point(72, 7)
        Me.lblcwoporsolicitar.Name = "lblcwoporsolicitar"
        Me.lblcwoporsolicitar.Size = New System.Drawing.Size(15, 19)
        Me.lblcwoporsolicitar.TabIndex = 5444
        Me.lblcwoporsolicitar.Text = "-"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 19)
        Me.Label2.TabIndex = 5443
        Me.Label2.Text = "WIP:"
        '
        'lblwipporsolicitar
        '
        Me.lblwipporsolicitar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblwipporsolicitar.AutoSize = True
        Me.lblwipporsolicitar.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwipporsolicitar.Location = New System.Drawing.Point(69, 42)
        Me.lblwipporsolicitar.Name = "lblwipporsolicitar"
        Me.lblwipporsolicitar.Size = New System.Drawing.Size(15, 19)
        Me.lblwipporsolicitar.TabIndex = 5442
        Me.lblwipporsolicitar.Text = "-"
        '
        'Hold
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(329, 283)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblcwoporsolicitar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblwipporsolicitar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.gbCompras)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Hold"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Hold"
        Me.gbCompras.ResumeLayout(False)
        Me.gbCompras.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbCompras As GroupBox
    Friend WithEvents dtpFProm As DateTimePicker
    Friend WithEvents txbNotas As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents lblcwoporsolicitar As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblwipporsolicitar As Label
End Class
