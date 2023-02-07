<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Usuarios
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Usuarios))
		Me.dgvUsers = New System.Windows.Forms.DataGridView()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.CheckBox1 = New System.Windows.Forms.CheckBox()
		Me.CheckBox2 = New System.Windows.Forms.CheckBox()
		Me.CheckBox3 = New System.Windows.Forms.CheckBox()
		Me.CheckBox4 = New System.Windows.Forms.CheckBox()
		Me.CheckBox5 = New System.Windows.Forms.CheckBox()
		Me.CheckBox6 = New System.Windows.Forms.CheckBox()
		Me.CheckBox7 = New System.Windows.Forms.CheckBox()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.TextBox2 = New System.Windows.Forms.TextBox()
		Me.CheckBox8 = New System.Windows.Forms.CheckBox()
		Me.Button4 = New System.Windows.Forms.Button()
		CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'dgvUsers
		'
		Me.dgvUsers.AllowUserToAddRows = False
		Me.dgvUsers.AllowUserToDeleteRows = False
		Me.dgvUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.dgvUsers.BackgroundColor = System.Drawing.Color.GhostWhite
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGreen
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.dgvUsers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.dgvUsers.DefaultCellStyle = DataGridViewCellStyle2
		Me.dgvUsers.EnableHeadersVisualStyles = False
		Me.dgvUsers.GridColor = System.Drawing.SystemColors.ButtonHighlight
		Me.dgvUsers.Location = New System.Drawing.Point(11, 31)
		Me.dgvUsers.Margin = New System.Windows.Forms.Padding(2)
		Me.dgvUsers.Name = "dgvUsers"
		Me.dgvUsers.ReadOnly = True
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
		DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.dgvUsers.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
		Me.dgvUsers.RowHeadersVisible = False
		Me.dgvUsers.RowHeadersWidth = 51
		Me.dgvUsers.RowTemplate.Height = 24
		Me.dgvUsers.Size = New System.Drawing.Size(526, 86)
		Me.dgvUsers.TabIndex = 5436
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(8, 9)
		Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(68, 18)
		Me.Label4.TabIndex = 5459
		Me.Label4.Text = "Usuarios"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(397, 119)
		Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(138, 17)
		Me.Label1.TabIndex = 5465
		Me.Label1.Text = "Seleccionar departamento:"
		'
		'TextBox1
		'
		Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TextBox1.Location = New System.Drawing.Point(206, 143)
		Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(110, 21)
		Me.TextBox1.TabIndex = 3
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(203, 121)
		Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(89, 17)
		Me.Label2.TabIndex = 5468
		Me.Label2.Text = "Nombre Usuario"
		'
		'CheckBox1
		'
		Me.CheckBox1.AutoSize = True
		Me.CheckBox1.Location = New System.Drawing.Point(412, 139)
		Me.CheckBox1.Name = "CheckBox1"
		Me.CheckBox1.Size = New System.Drawing.Size(107, 17)
		Me.CheckBox1.TabIndex = 6
		Me.CheckBox1.Text = "Planeacion Corte"
		Me.CheckBox1.UseVisualStyleBackColor = True
		'
		'CheckBox2
		'
		Me.CheckBox2.AutoSize = True
		Me.CheckBox2.Location = New System.Drawing.Point(412, 160)
		Me.CheckBox2.Name = "CheckBox2"
		Me.CheckBox2.Size = New System.Drawing.Size(96, 17)
		Me.CheckBox2.TabIndex = 7
		Me.CheckBox2.Text = "Planeacion XP"
		Me.CheckBox2.UseVisualStyleBackColor = True
		'
		'CheckBox3
		'
		Me.CheckBox3.AutoSize = True
		Me.CheckBox3.Location = New System.Drawing.Point(412, 182)
		Me.CheckBox3.Name = "CheckBox3"
		Me.CheckBox3.Size = New System.Drawing.Size(108, 17)
		Me.CheckBox3.TabIndex = 8
		Me.CheckBox3.Text = "Planeacion PWO"
		Me.CheckBox3.UseVisualStyleBackColor = True
		'
		'CheckBox4
		'
		Me.CheckBox4.AutoSize = True
		Me.CheckBox4.Location = New System.Drawing.Point(412, 205)
		Me.CheckBox4.Name = "CheckBox4"
		Me.CheckBox4.Size = New System.Drawing.Size(67, 17)
		Me.CheckBox4.TabIndex = 9
		Me.CheckBox4.Text = "Almacen"
		Me.CheckBox4.UseVisualStyleBackColor = True
		'
		'CheckBox5
		'
		Me.CheckBox5.AutoSize = True
		Me.CheckBox5.Location = New System.Drawing.Point(412, 227)
		Me.CheckBox5.Name = "CheckBox5"
		Me.CheckBox5.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox5.TabIndex = 10
		Me.CheckBox5.Text = "Aplicadores"
		Me.CheckBox5.UseVisualStyleBackColor = True
		'
		'CheckBox6
		'
		Me.CheckBox6.AutoSize = True
		Me.CheckBox6.Location = New System.Drawing.Point(412, 250)
		Me.CheckBox6.Name = "CheckBox6"
		Me.CheckBox6.Size = New System.Drawing.Size(67, 17)
		Me.CheckBox6.TabIndex = 11
		Me.CheckBox6.Text = "Compras"
		Me.CheckBox6.UseVisualStyleBackColor = True
		'
		'CheckBox7
		'
		Me.CheckBox7.AutoSize = True
		Me.CheckBox7.Location = New System.Drawing.Point(412, 271)
		Me.CheckBox7.Name = "CheckBox7"
		Me.CheckBox7.Size = New System.Drawing.Size(73, 17)
		Me.CheckBox7.TabIndex = 12
		Me.CheckBox7.Text = "Desarrollo"
		Me.CheckBox7.UseVisualStyleBackColor = True
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(25, 149)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(99, 30)
		Me.Button1.TabIndex = 1
		Me.Button1.Text = "Button1"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(25, 192)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(99, 30)
		Me.Button2.TabIndex = 2
		Me.Button2.Text = "Limpiar"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(207, 173)
		Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(42, 17)
		Me.Label3.TabIndex = 5478
		Me.Label3.Text = "Puesto"
		'
		'TextBox2
		'
		Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TextBox2.Location = New System.Drawing.Point(206, 192)
		Me.TextBox2.Margin = New System.Windows.Forms.Padding(2)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(111, 21)
		Me.TextBox2.TabIndex = 4
		'
		'CheckBox8
		'
		Me.CheckBox8.AutoSize = True
		Me.CheckBox8.Location = New System.Drawing.Point(210, 235)
		Me.CheckBox8.Name = "CheckBox8"
		Me.CheckBox8.Size = New System.Drawing.Size(56, 17)
		Me.CheckBox8.TabIndex = 5
		Me.CheckBox8.Text = "Activo"
		Me.CheckBox8.UseVisualStyleBackColor = True
		'
		'Button4
		'
		Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
		Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button4.Location = New System.Drawing.Point(506, 2)
		Me.Button4.Margin = New System.Windows.Forms.Padding(2)
		Me.Button4.Name = "Button4"
		Me.Button4.Size = New System.Drawing.Size(30, 27)
		Me.Button4.TabIndex = 5481
		Me.Button4.UseVisualStyleBackColor = True
		'
		'Usuarios
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(548, 289)
		Me.Controls.Add(Me.Button4)
		Me.Controls.Add(Me.CheckBox8)
		Me.Controls.Add(Me.TextBox2)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.CheckBox7)
		Me.Controls.Add(Me.CheckBox6)
		Me.Controls.Add(Me.CheckBox5)
		Me.Controls.Add(Me.CheckBox4)
		Me.Controls.Add(Me.CheckBox3)
		Me.Controls.Add(Me.CheckBox2)
		Me.Controls.Add(Me.CheckBox1)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.TextBox1)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.dgvUsers)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "Usuarios"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Usuarios"
		CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents dgvUsers As DataGridView
	Friend WithEvents Label4 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents CheckBox1 As CheckBox
	Friend WithEvents CheckBox2 As CheckBox
	Friend WithEvents CheckBox3 As CheckBox
	Friend WithEvents CheckBox4 As CheckBox
	Friend WithEvents CheckBox5 As CheckBox
	Friend WithEvents CheckBox6 As CheckBox
	Friend WithEvents CheckBox7 As CheckBox
	Friend WithEvents Button1 As Button
	Friend WithEvents Button2 As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents CheckBox8 As CheckBox
	Friend WithEvents Button4 As Button
End Class
