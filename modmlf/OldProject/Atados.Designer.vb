<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Atados
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Atados))
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.Button4 = New System.Windows.Forms.Button()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TxbQtyAtado = New System.Windows.Forms.TextBox()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.LblAu = New System.Windows.Forms.Label()
		Me.LblRev = New System.Windows.Forms.Label()
		Me.DgvAus = New System.Windows.Forms.DataGridView()
		CType(Me.DgvAus, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Button4
		'
		Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
		Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button4.Location = New System.Drawing.Point(11, 11)
		Me.Button4.Margin = New System.Windows.Forms.Padding(2)
		Me.Button4.Name = "Button4"
		Me.Button4.Size = New System.Drawing.Size(35, 29)
		Me.Button4.TabIndex = 5482
		Me.Button4.UseVisualStyleBackColor = True
		'
		'Label5
		'
		Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(328, 41)
		Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(50, 16)
		Me.Label5.TabIndex = 5483
		Me.Label5.Text = "Items: "
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(87, 21)
		Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(27, 17)
		Me.Label2.TabIndex = 5485
		Me.Label2.Text = "AU:"
		'
		'TextBox1
		'
		Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TextBox1.Location = New System.Drawing.Point(118, 19)
		Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(110, 21)
		Me.TextBox1.TabIndex = 5484
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(67, 234)
		Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(59, 17)
		Me.Label1.TabIndex = 5488
		Me.Label1.Text = "Qty Atado:"
		'
		'TxbQtyAtado
		'
		Me.TxbQtyAtado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxbQtyAtado.Location = New System.Drawing.Point(130, 232)
		Me.TxbQtyAtado.Margin = New System.Windows.Forms.Padding(2)
		Me.TxbQtyAtado.Name = "TxbQtyAtado"
		Me.TxbQtyAtado.Size = New System.Drawing.Size(110, 21)
		Me.TxbQtyAtado.TabIndex = 5487
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(275, 228)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(103, 29)
		Me.Button2.TabIndex = 5489
		Me.Button2.Text = "Guardar cambios"
		Me.Button2.UseVisualStyleBackColor = True
		Me.Button2.Visible = False
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(87, 202)
		Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(27, 17)
		Me.Label3.TabIndex = 5490
		Me.Label3.Text = "AU:"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(191, 200)
		Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(31, 17)
		Me.Label4.TabIndex = 5491
		Me.Label4.Text = "Rev:"
		'
		'LblAu
		'
		Me.LblAu.AutoSize = True
		Me.LblAu.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblAu.Location = New System.Drawing.Point(125, 202)
		Me.LblAu.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.LblAu.Name = "LblAu"
		Me.LblAu.Size = New System.Drawing.Size(12, 17)
		Me.LblAu.TabIndex = 5492
		Me.LblAu.Text = "-"
		'
		'LblRev
		'
		Me.LblRev.AutoSize = True
		Me.LblRev.Font = New System.Drawing.Font("Arial Narrow", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblRev.Location = New System.Drawing.Point(235, 200)
		Me.LblRev.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.LblRev.Name = "LblRev"
		Me.LblRev.Size = New System.Drawing.Size(12, 17)
		Me.LblRev.TabIndex = 5493
		Me.LblRev.Text = "-"
		'
		'DgvAus
		'
		Me.DgvAus.AllowUserToAddRows = False
		Me.DgvAus.AllowUserToDeleteRows = False
		Me.DgvAus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DgvAus.BackgroundColor = System.Drawing.Color.GhostWhite
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGreen
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DgvAus.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.DgvAus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.DgvAus.DefaultCellStyle = DataGridViewCellStyle2
		Me.DgvAus.EnableHeadersVisualStyles = False
		Me.DgvAus.GridColor = System.Drawing.SystemColors.ButtonHighlight
		Me.DgvAus.Location = New System.Drawing.Point(11, 59)
		Me.DgvAus.Margin = New System.Windows.Forms.Padding(2)
		Me.DgvAus.Name = "DgvAus"
		Me.DgvAus.ReadOnly = True
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
		DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DgvAus.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
		Me.DgvAus.RowHeadersVisible = False
		Me.DgvAus.RowHeadersWidth = 51
		Me.DgvAus.RowTemplate.Height = 24
		Me.DgvAus.Size = New System.Drawing.Size(446, 124)
		Me.DgvAus.TabIndex = 5494
		'
		'Atados
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(468, 280)
		Me.Controls.Add(Me.DgvAus)
		Me.Controls.Add(Me.LblRev)
		Me.Controls.Add(Me.LblAu)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.TxbQtyAtado)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.TextBox1)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Button4)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.Name = "Atados"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Atados"
		CType(Me.DgvAus, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Button4 As Button
	Friend WithEvents Label5 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents TxbQtyAtado As TextBox
	Friend WithEvents Button2 As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents LblAu As Label
	Friend WithEvents LblRev As Label
	Friend WithEvents DgvAus As DataGridView
End Class
