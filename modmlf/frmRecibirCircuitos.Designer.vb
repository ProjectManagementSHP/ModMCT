<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecibirCircuitos
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
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtCelda = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.OK = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtIdAtado = New System.Windows.Forms.TextBox()
        Me.lblCelda = New System.Windows.Forms.Label()
        Me.GroupBox5.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtCelda)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(178, 49)
        Me.GroupBox5.TabIndex = 5453
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Barcode Loc"
        '
        'txtCelda
        '
        Me.txtCelda.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCelda.Location = New System.Drawing.Point(13, 17)
        Me.txtCelda.Name = "txtCelda"
        Me.txtCelda.Size = New System.Drawing.Size(149, 23)
        Me.txtCelda.TabIndex = 47
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(25, 87)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(792, 433)
        Me.DataGridView1.TabIndex = 5454
        '
        'OK
        '
        Me.OK.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.OK.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.OK.FlatAppearance.BorderSize = 0
        Me.OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK.Location = New System.Drawing.Point(679, 526)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(138, 33)
        Me.OK.TabIndex = 5455
        Me.OK.Text = "Recibir"
        Me.OK.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtIdAtado)
        Me.GroupBox1.Location = New System.Drawing.Point(196, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(307, 49)
        Me.GroupBox1.TabIndex = 5456
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "idAtado"
        '
        'txtIdAtado
        '
        Me.txtIdAtado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdAtado.Location = New System.Drawing.Point(13, 17)
        Me.txtIdAtado.Name = "txtIdAtado"
        Me.txtIdAtado.Size = New System.Drawing.Size(275, 23)
        Me.txtIdAtado.TabIndex = 47
        '
        'lblCelda
        '
        Me.lblCelda.AutoSize = True
        Me.lblCelda.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelda.Location = New System.Drawing.Point(758, 12)
        Me.lblCelda.Name = "lblCelda"
        Me.lblCelda.Size = New System.Drawing.Size(30, 51)
        Me.lblCelda.TabIndex = 5457
        Me.lblCelda.Text = "'"
        '
        'frmRecibirCircuitos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 571)
        Me.Controls.Add(Me.lblCelda)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.GroupBox5)
        Me.Name = "frmRecibirCircuitos"
        Me.Text = "Transferir Circuitos"
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtCelda As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents OK As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtIdAtado As TextBox
    Friend WithEvents lblCelda As Label
End Class
