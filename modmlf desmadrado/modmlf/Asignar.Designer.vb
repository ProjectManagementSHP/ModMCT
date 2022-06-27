<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Asignar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Asignar))
        Me.lblwipporsolicitar = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblcwoporsolicitar = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtfechaasignada = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btnenviarsolicitud = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblwipporsolicitar
        '
        Me.lblwipporsolicitar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblwipporsolicitar.AutoSize = True
        Me.lblwipporsolicitar.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwipporsolicitar.Location = New System.Drawing.Point(328, 35)
        Me.lblwipporsolicitar.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblwipporsolicitar.Name = "lblwipporsolicitar"
        Me.lblwipporsolicitar.Size = New System.Drawing.Size(13, 16)
        Me.lblwipporsolicitar.TabIndex = 5438
        Me.lblwipporsolicitar.Text = "-"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(285, 35)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 16)
        Me.Label2.TabIndex = 5439
        Me.Label2.Text = "WIP:"
        '
        'lblcwoporsolicitar
        '
        Me.lblcwoporsolicitar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcwoporsolicitar.AutoSize = True
        Me.lblcwoporsolicitar.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcwoporsolicitar.Location = New System.Drawing.Point(81, 34)
        Me.lblcwoporsolicitar.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblcwoporsolicitar.Name = "lblcwoporsolicitar"
        Me.lblcwoporsolicitar.Size = New System.Drawing.Size(13, 16)
        Me.lblcwoporsolicitar.TabIndex = 5440
        Me.lblcwoporsolicitar.Text = "-"
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(27, 35)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 16)
        Me.Label4.TabIndex = 5441
        Me.Label4.Text = "CWO:"
        '
        'dtfechaasignada
        '
        Me.dtfechaasignada.CustomFormat = ""
        Me.dtfechaasignada.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtfechaasignada.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfechaasignada.Location = New System.Drawing.Point(34, 31)
        Me.dtfechaasignada.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dtfechaasignada.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtfechaasignada.Name = "dtfechaasignada"
        Me.dtfechaasignada.Size = New System.Drawing.Size(130, 21)
        Me.dtfechaasignada.TabIndex = 5442
        Me.dtfechaasignada.Value = New Date(2021, 9, 30, 0, 0, 0, 0)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtfechaasignada)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(19, 73)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(205, 71)
        Me.GroupBox1.TabIndex = 5444
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Asignar fecha:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(260, 73)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(214, 71)
        Me.GroupBox2.TabIndex = 5445
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Asignar hora:"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(28, 31)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(131, 20)
        Me.ComboBox1.TabIndex = 5448
        '
        'btnenviarsolicitud
        '
        Me.btnenviarsolicitud.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnenviarsolicitud.BackColor = System.Drawing.Color.Chocolate
        Me.btnenviarsolicitud.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnenviarsolicitud.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnenviarsolicitud.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnenviarsolicitud.Location = New System.Drawing.Point(193, 166)
        Me.btnenviarsolicitud.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnenviarsolicitud.Name = "btnenviarsolicitud"
        Me.btnenviarsolicitud.Size = New System.Drawing.Size(113, 22)
        Me.btnenviarsolicitud.TabIndex = 5446
        Me.btnenviarsolicitud.Text = "Enviar solicitud"
        Me.btnenviarsolicitud.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnenviarsolicitud.UseVisualStyleBackColor = False
        '
        'Asignar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 211)
        Me.Controls.Add(Me.btnenviarsolicitud)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblcwoporsolicitar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblwipporsolicitar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Asignar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asignar"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblwipporsolicitar As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblcwoporsolicitar As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dtfechaasignada As DateTimePicker
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnenviarsolicitud As Button
    Friend WithEvents ComboBox1 As ComboBox
End Class
