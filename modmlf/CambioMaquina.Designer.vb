<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CambioMaquina
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CambioMaquina))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblcwoporsolicitar = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblwipporsolicitar = New System.Windows.Forms.Label()
        Me.gbCompras = New System.Windows.Forms.GroupBox()
        Me.btnpasarseporlosbuebostodoelsistem = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.txbNotas = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.gbCompras.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 5)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 16)
        Me.Label4.TabIndex = 5449
        Me.Label4.Text = "CWO:"
        '
        'lblcwoporsolicitar
        '
        Me.lblcwoporsolicitar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcwoporsolicitar.AutoSize = True
        Me.lblcwoporsolicitar.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcwoporsolicitar.Location = New System.Drawing.Point(59, 3)
        Me.lblcwoporsolicitar.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblcwoporsolicitar.Name = "lblcwoporsolicitar"
        Me.lblcwoporsolicitar.Size = New System.Drawing.Size(13, 16)
        Me.lblcwoporsolicitar.TabIndex = 5448
        Me.lblcwoporsolicitar.Text = "-"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 32)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 16)
        Me.Label2.TabIndex = 5447
        Me.Label2.Text = "WIP:"
        '
        'lblwipporsolicitar
        '
        Me.lblwipporsolicitar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblwipporsolicitar.AutoSize = True
        Me.lblwipporsolicitar.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwipporsolicitar.Location = New System.Drawing.Point(57, 32)
        Me.lblwipporsolicitar.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblwipporsolicitar.Name = "lblwipporsolicitar"
        Me.lblwipporsolicitar.Size = New System.Drawing.Size(13, 16)
        Me.lblwipporsolicitar.TabIndex = 5446
        Me.lblwipporsolicitar.Text = "-"
        '
        'gbCompras
        '
        Me.gbCompras.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbCompras.BackColor = System.Drawing.Color.Transparent
        Me.gbCompras.Controls.Add(Me.btnpasarseporlosbuebostodoelsistem)
        Me.gbCompras.Controls.Add(Me.Label1)
        Me.gbCompras.Controls.Add(Me.ComboBox1)
        Me.gbCompras.Controls.Add(Me.txbNotas)
        Me.gbCompras.Location = New System.Drawing.Point(14, 73)
        Me.gbCompras.Name = "gbCompras"
        Me.gbCompras.Size = New System.Drawing.Size(225, 153)
        Me.gbCompras.TabIndex = 5450
        Me.gbCompras.TabStop = False
        '
        'btnpasarseporlosbuebostodoelsistem
        '
        Me.btnpasarseporlosbuebostodoelsistem.Location = New System.Drawing.Point(72, 119)
        Me.btnpasarseporlosbuebostodoelsistem.Name = "btnpasarseporlosbuebostodoelsistem"
        Me.btnpasarseporlosbuebostodoelsistem.Size = New System.Drawing.Size(81, 27)
        Me.btnpasarseporlosbuebostodoelsistem.TabIndex = 5457
        Me.btnpasarseporlosbuebostodoelsistem.Text = "Confirmar"
        Me.btnpasarseporlosbuebostodoelsistem.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 19)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 5451
        Me.Label1.Text = "Maquina:"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(93, 19)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(80, 21)
        Me.ComboBox1.TabIndex = 5451
        '
        'txbNotas
        '
        Me.txbNotas.Location = New System.Drawing.Point(13, 42)
        Me.txbNotas.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txbNotas.Multiline = True
        Me.txbNotas.Name = "txbNotas"
        Me.txbNotas.Size = New System.Drawing.Size(200, 72)
        Me.txbNotas.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 54)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 16)
        Me.Label3.TabIndex = 5452
        Me.Label3.Text = "Maquina actual:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(145, 54)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 16)
        Me.Label5.TabIndex = 5453
        Me.Label5.Text = "-"
        '
        'CambioMaquina
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(249, 232)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.gbCompras)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblcwoporsolicitar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblwipporsolicitar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "CambioMaquina"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Cambio Maquina"
        Me.gbCompras.ResumeLayout(False)
        Me.gbCompras.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label4 As Label
    Friend WithEvents lblcwoporsolicitar As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblwipporsolicitar As Label
    Friend WithEvents gbCompras As GroupBox
    Friend WithEvents txbNotas As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnpasarseporlosbuebostodoelsistem As Button
End Class
