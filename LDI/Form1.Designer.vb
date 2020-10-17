<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tmrGlobal = New System.Windows.Forms.Timer(Me.components)
        Me.btnApply = New System.Windows.Forms.Button()
        Me.cmsExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_0 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsRestore = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsIcon = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.txtAbout = New System.Windows.Forms.TextBox()
        Me.chbRestartEveryHour = New System.Windows.Forms.CheckBox()
        Me.chbUseScrlLockLed = New System.Windows.Forms.CheckBox()
        Me.lblMs = New System.Windows.Forms.Label()
        Me.lblInterval = New System.Windows.Forms.Label()
        Me.cmbSkin = New System.Windows.Forms.ComboBox()
        Me.tabAbout = New System.Windows.Forms.TabPage()
        Me.lblSkin = New System.Windows.Forms.Label()
        Me.chbToolTip = New System.Windows.Forms.CheckBox()
        Me.chbMinimized = New System.Windows.Forms.CheckBox()
        Me.btnReload = New System.Windows.Forms.Button()
        Me.tabOptions = New System.Windows.Forms.TabPage()
        Me.nudInterval = New System.Windows.Forms.NumericUpDown()
        Me.colWrite = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRead = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDrives = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lsvDrive = New System.Windows.Forms.ListView()
        Me.tabDrives = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.cmsIcon.SuspendLayout()
        Me.tabAbout.SuspendLayout()
        Me.tabOptions.SuspendLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDrives.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrGlobal
        '
        Me.tmrGlobal.Enabled = True
        Me.tmrGlobal.Interval = 1000
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnApply.Location = New System.Drawing.Point(382, 225)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(80, 23)
        Me.btnApply.TabIndex = 17
        Me.btnApply.Text = "&Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'cmsExit
        '
        Me.cmsExit.Name = "cmsExit"
        Me.cmsExit.Size = New System.Drawing.Size(124, 22)
        Me.cmsExit.Text = "E&xit"
        '
        'cms_0
        '
        Me.cms_0.Name = "cms_0"
        Me.cms_0.Size = New System.Drawing.Size(121, 6)
        '
        'cmsRestore
        '
        Me.cmsRestore.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmsRestore.Name = "cmsRestore"
        Me.cmsRestore.Size = New System.Drawing.Size(124, 22)
        Me.cmsRestore.Text = "&Restore"
        '
        'cmsIcon
        '
        Me.cmsIcon.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsRestore, Me.cms_0, Me.cmsExit})
        Me.cmsIcon.Name = "cmsIcon"
        Me.cmsIcon.Size = New System.Drawing.Size(125, 54)
        '
        'txtAbout
        '
        Me.txtAbout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAbout.Location = New System.Drawing.Point(0, 0)
        Me.txtAbout.MaxLength = 65536
        Me.txtAbout.Multiline = True
        Me.txtAbout.Name = "txtAbout"
        Me.txtAbout.Size = New System.Drawing.Size(462, 187)
        Me.txtAbout.TabIndex = 0
        Me.txtAbout.Text = resources.GetString("txtAbout.Text")
        '
        'chbRestartEveryHour
        '
        Me.chbRestartEveryHour.AutoSize = True
        Me.chbRestartEveryHour.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.LDI.My.MySettings.Default, "RestartEveryHour", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chbRestartEveryHour.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.chbRestartEveryHour.Location = New System.Drawing.Point(180, 69)
        Me.chbRestartEveryHour.Name = "chbRestartEveryHour"
        Me.chbRestartEveryHour.Size = New System.Drawing.Size(122, 16)
        Me.chbRestartEveryHour.TabIndex = 8
        Me.chbRestartEveryHour.Text = "&Restart every hour."
        Me.chbRestartEveryHour.UseVisualStyleBackColor = True
        '
        'chbUseScrlLockLed
        '
        Me.chbUseScrlLockLed.AutoSize = True
        Me.chbUseScrlLockLed.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.LDI.My.MySettings.Default, "UseScrlRockLed", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chbUseScrlLockLed.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.chbUseScrlLockLed.Location = New System.Drawing.Point(180, 47)
        Me.chbUseScrlLockLed.Name = "chbUseScrlLockLed"
        Me.chbUseScrlLockLed.Size = New System.Drawing.Size(114, 16)
        Me.chbUseScrlLockLed.TabIndex = 7
        Me.chbUseScrlLockLed.Text = "Use &ScrlLock Led"
        Me.chbUseScrlLockLed.UseVisualStyleBackColor = True
        '
        'lblMs
        '
        Me.lblMs.AutoSize = True
        Me.lblMs.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.lblMs.Location = New System.Drawing.Point(157, 93)
        Me.lblMs.Name = "lblMs"
        Me.lblMs.Size = New System.Drawing.Size(20, 12)
        Me.lblMs.TabIndex = 6
        Me.lblMs.Text = "ms"
        '
        'lblInterval
        '
        Me.lblInterval.AutoSize = True
        Me.lblInterval.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.lblInterval.Location = New System.Drawing.Point(6, 93)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(83, 12)
        Me.lblInterval.TabIndex = 4
        Me.lblInterval.Text = "Polling &Interval:"
        '
        'cmbSkin
        '
        Me.cmbSkin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSkin.FormattingEnabled = True
        Me.cmbSkin.Location = New System.Drawing.Point(8, 21)
        Me.cmbSkin.Name = "cmbSkin"
        Me.cmbSkin.Size = New System.Drawing.Size(447, 20)
        Me.cmbSkin.TabIndex = 1
        '
        'tabAbout
        '
        Me.tabAbout.Controls.Add(Me.txtAbout)
        Me.tabAbout.Location = New System.Drawing.Point(4, 22)
        Me.tabAbout.Name = "tabAbout"
        Me.tabAbout.Size = New System.Drawing.Size(462, 187)
        Me.tabAbout.TabIndex = 2
        Me.tabAbout.Text = "About"
        Me.tabAbout.UseVisualStyleBackColor = True
        '
        'lblSkin
        '
        Me.lblSkin.AutoSize = True
        Me.lblSkin.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.lblSkin.Location = New System.Drawing.Point(6, 6)
        Me.lblSkin.Name = "lblSkin"
        Me.lblSkin.Size = New System.Drawing.Size(29, 12)
        Me.lblSkin.TabIndex = 0
        Me.lblSkin.Text = "&Skin:"
        '
        'chbToolTip
        '
        Me.chbToolTip.AutoSize = True
        Me.chbToolTip.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.LDI.My.MySettings.Default, "ToolTip", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chbToolTip.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.chbToolTip.Location = New System.Drawing.Point(8, 69)
        Me.chbToolTip.Name = "chbToolTip"
        Me.chbToolTip.Size = New System.Drawing.Size(104, 16)
        Me.chbToolTip.TabIndex = 3
        Me.chbToolTip.Text = "Enable &Tool Tip"
        Me.chbToolTip.UseVisualStyleBackColor = True
        '
        'chbMinimized
        '
        Me.chbMinimized.AutoSize = True
        Me.chbMinimized.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.LDI.My.MySettings.Default, "Minimized", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chbMinimized.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.chbMinimized.Location = New System.Drawing.Point(8, 47)
        Me.chbMinimized.Name = "chbMinimized"
        Me.chbMinimized.Size = New System.Drawing.Size(115, 16)
        Me.chbMinimized.TabIndex = 2
        Me.chbMinimized.Text = "Startup &Minimized"
        Me.chbMinimized.UseVisualStyleBackColor = True
        '
        'btnReload
        '
        Me.btnReload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReload.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnReload.Location = New System.Drawing.Point(296, 225)
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(80, 23)
        Me.btnReload.TabIndex = 18
        Me.btnReload.Text = "&Reload"
        Me.btnReload.UseVisualStyleBackColor = True
        '
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.chbRestartEveryHour)
        Me.tabOptions.Controls.Add(Me.chbUseScrlLockLed)
        Me.tabOptions.Controls.Add(Me.lblMs)
        Me.tabOptions.Controls.Add(Me.lblInterval)
        Me.tabOptions.Controls.Add(Me.cmbSkin)
        Me.tabOptions.Controls.Add(Me.lblSkin)
        Me.tabOptions.Controls.Add(Me.nudInterval)
        Me.tabOptions.Controls.Add(Me.chbToolTip)
        Me.tabOptions.Controls.Add(Me.chbMinimized)
        Me.tabOptions.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOptions.Size = New System.Drawing.Size(462, 187)
        Me.tabOptions.TabIndex = 1
        Me.tabOptions.Text = "Options"
        Me.tabOptions.UseVisualStyleBackColor = True
        '
        'nudInterval
        '
        Me.nudInterval.DataBindings.Add(New System.Windows.Forms.Binding("Value", Global.LDI.My.MySettings.Default, "Interval", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.nudInterval.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudInterval.Location = New System.Drawing.Point(95, 91)
        Me.nudInterval.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudInterval.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudInterval.Name = "nudInterval"
        Me.nudInterval.Size = New System.Drawing.Size(56, 19)
        Me.nudInterval.TabIndex = 5
        Me.nudInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudInterval.ThousandsSeparator = True
        Me.nudInterval.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'colWrite
        '
        Me.colWrite.Text = "Write Bytes/sec"
        Me.colWrite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colWrite.Width = 120
        '
        'colRead
        '
        Me.colRead.Text = "Read Bytes/sec"
        Me.colRead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colRead.Width = 120
        '
        'colDrives
        '
        Me.colDrives.Text = "Drive(s)"
        Me.colDrives.Width = 200
        '
        'lsvDrive
        '
        Me.lsvDrive.AllowDrop = True
        Me.lsvDrive.CheckBoxes = True
        Me.lsvDrive.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colDrives, Me.colRead, Me.colWrite})
        Me.lsvDrive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsvDrive.FullRowSelect = True
        Me.lsvDrive.HideSelection = False
        Me.lsvDrive.Location = New System.Drawing.Point(3, 3)
        Me.lsvDrive.Name = "lsvDrive"
        Me.lsvDrive.Size = New System.Drawing.Size(456, 181)
        Me.lsvDrive.TabIndex = 4
        Me.lsvDrive.UseCompatibleStateImageBehavior = False
        Me.lsvDrive.View = System.Windows.Forms.View.Details
        '
        'tabDrives
        '
        Me.tabDrives.Controls.Add(Me.lsvDrive)
        Me.tabDrives.Location = New System.Drawing.Point(4, 22)
        Me.tabDrives.Name = "tabDrives"
        Me.tabDrives.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDrives.Size = New System.Drawing.Size(462, 187)
        Me.tabDrives.TabIndex = 0
        Me.tabDrives.Text = "Drive(s)"
        Me.tabDrives.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabDrives)
        Me.TabControl1.Controls.Add(Me.tabOptions)
        Me.TabControl1.Controls.Add(Me.tabAbout)
        Me.TabControl1.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.TabControl1.Location = New System.Drawing.Point(3, 6)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(470, 213)
        Me.TabControl1.TabIndex = 16
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 251)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnReload)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "LDI Logical Disk Indicator"
        Me.cmsIcon.ResumeLayout(False)
        Me.tabAbout.ResumeLayout(False)
        Me.tabAbout.PerformLayout()
        Me.tabOptions.ResumeLayout(False)
        Me.tabOptions.PerformLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDrives.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tmrGlobal As Timer
    Friend WithEvents btnApply As Button
    Friend WithEvents cmsExit As ToolStripMenuItem
    Friend WithEvents cms_0 As ToolStripSeparator
    Friend WithEvents cmsRestore As ToolStripMenuItem
    Friend WithEvents cmsIcon As ContextMenuStrip
    Friend WithEvents txtAbout As TextBox
    Friend WithEvents chbRestartEveryHour As CheckBox
    Friend WithEvents chbUseScrlLockLed As CheckBox
    Friend WithEvents lblMs As Label
    Friend WithEvents lblInterval As Label
    Friend WithEvents cmbSkin As ComboBox
    Friend WithEvents tabAbout As TabPage
    Friend WithEvents lblSkin As Label
    Friend WithEvents chbToolTip As CheckBox
    Friend WithEvents chbMinimized As CheckBox
    Friend WithEvents btnReload As Button
    Friend WithEvents tabOptions As TabPage
    Friend WithEvents nudInterval As NumericUpDown
    Friend WithEvents colWrite As ColumnHeader
    Friend WithEvents colRead As ColumnHeader
    Friend WithEvents colDrives As ColumnHeader
    Friend WithEvents lsvDrive As ListView
    Friend WithEvents tabDrives As TabPage
    Friend WithEvents TabControl1 As TabControl
End Class
