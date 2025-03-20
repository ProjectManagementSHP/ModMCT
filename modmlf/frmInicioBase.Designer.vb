<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInicioBase
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
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ToolsMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrackerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrearPWOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecibirSplicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplicesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrensasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecibirCircuitosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.LocalizacionAtadosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsMenu, Me.SplicesToolStripMenuItem, Me.PrensasToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(862, 27)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'ToolsMenu
        '
        Me.ToolsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TrackerToolStripMenuItem, Me.CrearPWOToolStripMenuItem, Me.LocalizacionAtadosToolStripMenuItem})
        Me.ToolsMenu.Name = "ToolsMenu"
        Me.ToolsMenu.Size = New System.Drawing.Size(52, 23)
        Me.ToolsMenu.Text = "Tools"
        '
        'TrackerToolStripMenuItem
        '
        Me.TrackerToolStripMenuItem.Name = "TrackerToolStripMenuItem"
        Me.TrackerToolStripMenuItem.Size = New System.Drawing.Size(203, 24)
        Me.TrackerToolStripMenuItem.Text = "MasterCircuitTracker"
        '
        'CrearPWOToolStripMenuItem
        '
        Me.CrearPWOToolStripMenuItem.Name = "CrearPWOToolStripMenuItem"
        Me.CrearPWOToolStripMenuItem.Size = New System.Drawing.Size(203, 24)
        Me.CrearPWOToolStripMenuItem.Text = "CrearPWO"
        '
        'SplicesToolStripMenuItem
        '
        Me.SplicesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RecibirSplicesToolStripMenuItem, Me.SplicesToolStripMenuItem1})
        Me.SplicesToolStripMenuItem.Name = "SplicesToolStripMenuItem"
        Me.SplicesToolStripMenuItem.Size = New System.Drawing.Size(61, 23)
        Me.SplicesToolStripMenuItem.Text = "Splices"
        '
        'RecibirSplicesToolStripMenuItem
        '
        Me.RecibirSplicesToolStripMenuItem.Name = "RecibirSplicesToolStripMenuItem"
        Me.RecibirSplicesToolStripMenuItem.Size = New System.Drawing.Size(180, 24)
        Me.RecibirSplicesToolStripMenuItem.Text = "Recibir Splices"
        '
        'SplicesToolStripMenuItem1
        '
        Me.SplicesToolStripMenuItem1.Name = "SplicesToolStripMenuItem1"
        Me.SplicesToolStripMenuItem1.Size = New System.Drawing.Size(180, 24)
        Me.SplicesToolStripMenuItem1.Text = "Splices"
        '
        'PrensasToolStripMenuItem
        '
        Me.PrensasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RecibirCircuitosToolStripMenuItem})
        Me.PrensasToolStripMenuItem.Name = "PrensasToolStripMenuItem"
        Me.PrensasToolStripMenuItem.Size = New System.Drawing.Size(68, 23)
        Me.PrensasToolStripMenuItem.Text = "Prensas"
        '
        'RecibirCircuitosToolStripMenuItem
        '
        Me.RecibirCircuitosToolStripMenuItem.Name = "RecibirCircuitosToolStripMenuItem"
        Me.RecibirCircuitosToolStripMenuItem.Size = New System.Drawing.Size(180, 24)
        Me.RecibirCircuitosToolStripMenuItem.Text = "Recibir Circuitos"
        '
        'LocalizacionAtadosToolStripMenuItem
        '
        Me.LocalizacionAtadosToolStripMenuItem.Name = "LocalizacionAtadosToolStripMenuItem"
        Me.LocalizacionAtadosToolStripMenuItem.Size = New System.Drawing.Size(203, 24)
        Me.LocalizacionAtadosToolStripMenuItem.Text = "Info Atado"
        '
        'frmInicioBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 537)
        Me.Controls.Add(Me.MenuStrip)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "frmInicioBase"
        Me.Text = "frmInicioBase"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolsMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrackerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CrearPWOToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplicesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecibirSplicesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplicesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PrensasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecibirCircuitosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LocalizacionAtadosToolStripMenuItem As ToolStripMenuItem
End Class
