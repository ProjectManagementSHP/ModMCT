<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModifyAndAddPN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ModifyAndAddPN))
        Me.tbOpciones = New System.Windows.Forms.TabControl()
        Me.tbModify = New System.Windows.Forms.TabPage()
        Me.chkParoAU = New System.Windows.Forms.CheckBox()
        Me.dtpFProm = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txbNotasModify = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txbNotas = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txbVendorModify = New System.Windows.Forms.TextBox()
        Me.cmbPOModify = New System.Windows.Forms.ComboBox()
        Me.lblPn = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbNew = New System.Windows.Forms.TabPage()
        Me.chkParoAUNew = New System.Windows.Forms.CheckBox()
        Me.lblQtyOnHand = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblQtyTotal = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpAgregando = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txbNotasNew = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNewRazon = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txbNuevoVendor = New System.Windows.Forms.TextBox()
        Me.cmbPONuevo = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dgvCortosCompletos = New System.Windows.Forms.DataGridView()
        Me.ch = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txbNewPN = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.tbOpciones.SuspendLayout()
        Me.tbModify.SuspendLayout()
        Me.tbNew.SuspendLayout()
        CType(Me.dgvCortosCompletos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbOpciones
        '
        Me.tbOpciones.Controls.Add(Me.tbModify)
        Me.tbOpciones.Controls.Add(Me.tbNew)
        Me.tbOpciones.Location = New System.Drawing.Point(0, 0)
        Me.tbOpciones.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbOpciones.Name = "tbOpciones"
        Me.tbOpciones.SelectedIndex = 0
        Me.tbOpciones.Size = New System.Drawing.Size(491, 639)
        Me.tbOpciones.TabIndex = 0
        '
        'tbModify
        '
        Me.tbModify.Controls.Add(Me.chkParoAU)
        Me.tbModify.Controls.Add(Me.dtpFProm)
        Me.tbModify.Controls.Add(Me.Label6)
        Me.tbModify.Controls.Add(Me.txbNotasModify)
        Me.tbModify.Controls.Add(Me.Label5)
        Me.tbModify.Controls.Add(Me.txbNotas)
        Me.tbModify.Controls.Add(Me.Label4)
        Me.tbModify.Controls.Add(Me.Label3)
        Me.tbModify.Controls.Add(Me.Label2)
        Me.tbModify.Controls.Add(Me.txbVendorModify)
        Me.tbModify.Controls.Add(Me.cmbPOModify)
        Me.tbModify.Controls.Add(Me.lblPn)
        Me.tbModify.Controls.Add(Me.Label1)
        Me.tbModify.Location = New System.Drawing.Point(4, 25)
        Me.tbModify.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbModify.Name = "tbModify"
        Me.tbModify.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbModify.Size = New System.Drawing.Size(483, 610)
        Me.tbModify.TabIndex = 0
        Me.tbModify.Text = "Modificar"
        Me.tbModify.UseVisualStyleBackColor = True
        '
        'chkParoAU
        '
        Me.chkParoAU.AutoSize = True
        Me.chkParoAU.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkParoAU.Location = New System.Drawing.Point(28, 551)
        Me.chkParoAU.Name = "chkParoAU"
        Me.chkParoAU.Size = New System.Drawing.Size(78, 20)
        Me.chkParoAU.TabIndex = 25
        Me.chkParoAU.Text = "Paro AU"
        Me.chkParoAU.UseVisualStyleBackColor = True
        '
        'dtpFProm
        '
        Me.dtpFProm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtpFProm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFProm.Location = New System.Drawing.Point(28, 506)
        Me.dtpFProm.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dtpFProm.MinDate = New Date(2021, 1, 1, 0, 0, 0, 0)
        Me.dtpFProm.Name = "dtpFProm"
        Me.dtpFProm.Size = New System.Drawing.Size(417, 23)
        Me.dtpFProm.TabIndex = 24
        Me.dtpFProm.Value = New Date(2021, 1, 1, 0, 0, 0, 0)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 470)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 16)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Fecha Estimada"
        '
        'txbNotasModify
        '
        Me.txbNotasModify.Location = New System.Drawing.Point(26, 332)
        Me.txbNotasModify.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txbNotasModify.Multiline = True
        Me.txbNotasModify.Name = "txbNotasModify"
        Me.txbNotasModify.Size = New System.Drawing.Size(417, 127)
        Me.txbNotasModify.TabIndex = 22
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 307)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Notas:"
        '
        'txbNotas
        '
        Me.txbNotas.Location = New System.Drawing.Point(26, 166)
        Me.txbNotas.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txbNotas.Multiline = True
        Me.txbNotas.Name = "txbNotas"
        Me.txbNotas.Size = New System.Drawing.Size(417, 127)
        Me.txbNotas.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 16)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Razon:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(258, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Vendor:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "PO:"
        '
        'txbVendorModify
        '
        Me.txbVendorModify.Location = New System.Drawing.Point(262, 103)
        Me.txbVendorModify.Name = "txbVendorModify"
        Me.txbVendorModify.Size = New System.Drawing.Size(167, 23)
        Me.txbVendorModify.TabIndex = 6
        '
        'cmbPOModify
        '
        Me.cmbPOModify.FormattingEnabled = True
        Me.cmbPOModify.Location = New System.Drawing.Point(28, 103)
        Me.cmbPOModify.Name = "cmbPOModify"
        Me.cmbPOModify.Size = New System.Drawing.Size(190, 24)
        Me.cmbPOModify.TabIndex = 5
        '
        'lblPn
        '
        Me.lblPn.AutoSize = True
        Me.lblPn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPn.Location = New System.Drawing.Point(85, 45)
        Me.lblPn.Name = "lblPn"
        Me.lblPn.Size = New System.Drawing.Size(13, 18)
        Me.lblPn.TabIndex = 4
        Me.lblPn.Text = "-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(85, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(196, 18)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Modificar Numero de Parte"
        '
        'tbNew
        '
        Me.tbNew.Controls.Add(Me.chkParoAUNew)
        Me.tbNew.Controls.Add(Me.lblQtyOnHand)
        Me.tbNew.Controls.Add(Me.Label16)
        Me.tbNew.Controls.Add(Me.lblQtyTotal)
        Me.tbNew.Controls.Add(Me.Label15)
        Me.tbNew.Controls.Add(Me.dtpAgregando)
        Me.tbNew.Controls.Add(Me.Label10)
        Me.tbNew.Controls.Add(Me.txbNotasNew)
        Me.tbNew.Controls.Add(Me.Label11)
        Me.tbNew.Controls.Add(Me.txtNewRazon)
        Me.tbNew.Controls.Add(Me.Label12)
        Me.tbNew.Controls.Add(Me.Label13)
        Me.tbNew.Controls.Add(Me.Label14)
        Me.tbNew.Controls.Add(Me.txbNuevoVendor)
        Me.tbNew.Controls.Add(Me.cmbPONuevo)
        Me.tbNew.Controls.Add(Me.Label9)
        Me.tbNew.Controls.Add(Me.dgvCortosCompletos)
        Me.tbNew.Controls.Add(Me.Label8)
        Me.tbNew.Controls.Add(Me.txbNewPN)
        Me.tbNew.Controls.Add(Me.Label7)
        Me.tbNew.Location = New System.Drawing.Point(4, 25)
        Me.tbNew.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbNew.Name = "tbNew"
        Me.tbNew.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbNew.Size = New System.Drawing.Size(483, 610)
        Me.tbNew.TabIndex = 1
        Me.tbNew.Text = "Agregar"
        Me.tbNew.UseVisualStyleBackColor = True
        '
        'chkParoAUNew
        '
        Me.chkParoAUNew.AutoSize = True
        Me.chkParoAUNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkParoAUNew.Location = New System.Drawing.Point(18, 581)
        Me.chkParoAUNew.Name = "chkParoAUNew"
        Me.chkParoAUNew.Size = New System.Drawing.Size(78, 20)
        Me.chkParoAUNew.TabIndex = 5453
        Me.chkParoAUNew.Text = "Paro AU"
        Me.chkParoAUNew.UseVisualStyleBackColor = True
        '
        'lblQtyOnHand
        '
        Me.lblQtyOnHand.AutoSize = True
        Me.lblQtyOnHand.Location = New System.Drawing.Point(427, 218)
        Me.lblQtyOnHand.Name = "lblQtyOnHand"
        Me.lblQtyOnHand.Size = New System.Drawing.Size(13, 16)
        Me.lblQtyOnHand.TabIndex = 5452
        Me.lblQtyOnHand.Text = "-"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(261, 218)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(97, 16)
        Me.Label16.TabIndex = 5451
        Me.Label16.Text = "Cantidad O.H:"
        '
        'lblQtyTotal
        '
        Me.lblQtyTotal.AutoSize = True
        Me.lblQtyTotal.Location = New System.Drawing.Point(163, 218)
        Me.lblQtyTotal.Name = "lblQtyTotal"
        Me.lblQtyTotal.Size = New System.Drawing.Size(13, 16)
        Me.lblQtyTotal.TabIndex = 5450
        Me.lblQtyTotal.Text = "-"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(19, 218)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(100, 16)
        Me.Label15.TabIndex = 5449
        Me.Label15.Text = "Cantidad total:"
        '
        'dtpAgregando
        '
        Me.dtpAgregando.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtpAgregando.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAgregando.Location = New System.Drawing.Point(194, 542)
        Me.dtpAgregando.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dtpAgregando.MinDate = New Date(2021, 1, 1, 0, 0, 0, 0)
        Me.dtpAgregando.Name = "dtpAgregando"
        Me.dtpAgregando.Size = New System.Drawing.Size(266, 23)
        Me.dtpAgregando.TabIndex = 5448
        Me.dtpAgregando.Value = New Date(2021, 1, 1, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(14, 548)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(110, 16)
        Me.Label10.TabIndex = 5447
        Me.Label10.Text = "Fecha Estimada"
        '
        'txbNotasNew
        '
        Me.txbNotasNew.Location = New System.Drawing.Point(15, 436)
        Me.txbNotasNew.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txbNotasNew.Multiline = True
        Me.txbNotasNew.Name = "txbNotasNew"
        Me.txbNotasNew.Size = New System.Drawing.Size(460, 85)
        Me.txbNotasNew.TabIndex = 5446
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 411)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 16)
        Me.Label11.TabIndex = 5445
        Me.Label11.Text = "Notas:"
        '
        'txtNewRazon
        '
        Me.txtNewRazon.Location = New System.Drawing.Point(15, 344)
        Me.txtNewRazon.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtNewRazon.Multiline = True
        Me.txtNewRazon.Name = "txtNewRazon"
        Me.txtNewRazon.Size = New System.Drawing.Size(460, 65)
        Me.txtNewRazon.TabIndex = 5444
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(11, 319)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 16)
        Me.Label12.TabIndex = 5443
        Me.Label12.Text = "Razon:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(254, 247)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 16)
        Me.Label13.TabIndex = 5442
        Me.Label13.Text = "Vendor:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(12, 247)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(32, 16)
        Me.Label14.TabIndex = 5441
        Me.Label14.Text = "PO:"
        '
        'txbNuevoVendor
        '
        Me.txbNuevoVendor.Location = New System.Drawing.Point(249, 273)
        Me.txbNuevoVendor.Name = "txbNuevoVendor"
        Me.txbNuevoVendor.Size = New System.Drawing.Size(226, 23)
        Me.txbNuevoVendor.TabIndex = 5440
        '
        'cmbPONuevo
        '
        Me.cmbPONuevo.FormattingEnabled = True
        Me.cmbPONuevo.Location = New System.Drawing.Point(15, 273)
        Me.cmbPONuevo.Name = "cmbPONuevo"
        Me.cmbPONuevo.Size = New System.Drawing.Size(190, 24)
        Me.cmbPONuevo.TabIndex = 5439
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(384, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 16)
        Me.Label9.TabIndex = 5438
        Me.Label9.Text = "Items:"
        '
        'dgvCortosCompletos
        '
        Me.dgvCortosCompletos.AllowUserToAddRows = False
        Me.dgvCortosCompletos.AllowUserToDeleteRows = False
        Me.dgvCortosCompletos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCortosCompletos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Bisque
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCortosCompletos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCortosCompletos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCortosCompletos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ch})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCortosCompletos.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCortosCompletos.EnableHeadersVisualStyles = False
        Me.dgvCortosCompletos.GridColor = System.Drawing.SystemColors.ControlLightLight
        Me.dgvCortosCompletos.Location = New System.Drawing.Point(7, 70)
        Me.dgvCortosCompletos.Name = "dgvCortosCompletos"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCortosCompletos.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvCortosCompletos.RowHeadersVisible = False
        Me.dgvCortosCompletos.RowHeadersWidth = 51
        Me.dgvCortosCompletos.RowTemplate.Height = 24
        Me.dgvCortosCompletos.Size = New System.Drawing.Size(464, 127)
        Me.dgvCortosCompletos.TabIndex = 5437
        '
        'ch
        '
        Me.ch.HeaderText = "Check"
        Me.ch.MinimumWidth = 8
        Me.ch.Name = "ch"
        Me.ch.Width = 150
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(83, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 16)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "PN:"
        '
        'txbNewPN
        '
        Me.txbNewPN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txbNewPN.Location = New System.Drawing.Point(138, 30)
        Me.txbNewPN.Name = "txbNewPN"
        Me.txbNewPN.Size = New System.Drawing.Size(220, 23)
        Me.txbNewPN.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(84, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(188, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Agregar Numero de Parte"
        '
        'Button2
        '
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Image = Global.modmlf.My.Resources.Resources.delete_unapprove_discard_remove_x_red_icon_icons_com_55984
        Me.Button2.Location = New System.Drawing.Point(326, 645)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(56, 54)
        Me.Button2.TabIndex = 2
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Image = Global.modmlf.My.Resources.Resources.save_icon_icons_com_53618_1_
        Me.Button1.Location = New System.Drawing.Point(78, 640)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(62, 65)
        Me.Button1.TabIndex = 1
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ModifyAndAddPN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(490, 711)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.tbOpciones)
        Me.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "ModifyAndAddPN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.tbOpciones.ResumeLayout(False)
        Me.tbModify.ResumeLayout(False)
        Me.tbModify.PerformLayout()
        Me.tbNew.ResumeLayout(False)
        Me.tbNew.PerformLayout()
        CType(Me.dgvCortosCompletos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tbOpciones As TabControl
    Friend WithEvents tbModify As TabPage
    Friend WithEvents tbNew As TabPage
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txbVendorModify As TextBox
    Friend WithEvents cmbPOModify As ComboBox
    Friend WithEvents lblPn As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txbNotasModify As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txbNotas As TextBox
    Friend WithEvents dtpFProm As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents txbNewPN As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents dtpAgregando As DateTimePicker
    Friend WithEvents Label10 As Label
    Friend WithEvents txbNotasNew As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtNewRazon As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txbNuevoVendor As TextBox
    Friend WithEvents cmbPONuevo As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents dgvCortosCompletos As DataGridView
    Friend WithEvents ch As DataGridViewCheckBoxColumn
    Friend WithEvents lblQtyOnHand As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents lblQtyTotal As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents chkParoAU As CheckBox
    Friend WithEvents chkParoAUNew As CheckBox
End Class
