<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CloseCWO
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
		Me.txbNotas = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.btnGuardar = New System.Windows.Forms.Button()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'txbNotas
		'
		Me.txbNotas.Location = New System.Drawing.Point(28, 51)
		Me.txbNotas.Multiline = True
		Me.txbNotas.Name = "txbNotas"
		Me.txbNotas.Size = New System.Drawing.Size(509, 106)
		Me.txbNotas.TabIndex = 20
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(38, 21)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(167, 30)
		Me.Label1.TabIndex = 21
		Me.Label1.Text = "Nota de Cierre"
		'
		'btnGuardar
		'
		Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnGuardar.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.btnGuardar.FlatAppearance.BorderSize = 0
		Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.btnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnGuardar.Location = New System.Drawing.Point(315, 168)
		Me.btnGuardar.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
		Me.btnGuardar.Name = "btnGuardar"
		Me.btnGuardar.Size = New System.Drawing.Size(138, 35)
		Me.btnGuardar.TabIndex = 60
		Me.btnGuardar.Text = "Save"
		Me.btnGuardar.UseVisualStyleBackColor = False
		'
		'Button1
		'
		Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Button1.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.Button1.FlatAppearance.BorderSize = 0
		Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button1.Location = New System.Drawing.Point(126, 169)
		Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(138, 35)
		Me.Button1.TabIndex = 61
		Me.Button1.Text = "Cancel"
		Me.Button1.UseVisualStyleBackColor = False
		'
		'CloseCWO
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(561, 234)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.btnGuardar)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.txbNotas)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.Name = "CloseCWO"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "CloseCWO"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents txbNotas As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents btnGuardar As Button
	Friend WithEvents Button1 As Button
End Class
