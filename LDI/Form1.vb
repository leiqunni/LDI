Imports System
Imports System.Drawing
Imports System.IO
Imports System.Text

Public Class Form1
    Private IndicatorList As New List(Of CIndicator)
    Private SkinFile As String = "Skin.ini"
    Private LangFile As String = "Lang.ini"

#Region "Form"

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        InitLang()
        InitSkinCombobox()
        InitDriveListView()
        InitIndicator()

        If My.Settings.Minimized Then
            Me.WindowState = FormWindowState.Minimized
        End If
    End Sub

    Private Sub Form1_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        For Each item As CIndicator In IndicatorList
            item.Dispose()
        Next
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized And IndicatorList.Count > 0 Then
            Me.ShowInTaskbar = False
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        Else
            Me.ShowInTaskbar = True
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End If
    End Sub

#End Region

#Region "InitSkinCombobox"

    Private Sub InitSkinCombobox()
        cmbSkin.Items.Clear()
        cmbSkin.Items.Add("Default")
        Dim ini As New IniFile11(SkinFile)
        For Each value As String In ini.ReadKeyRegex("^\[(.+?)\]", 1)
            cmbSkin.Items.Add(value)
        Next
        If My.Settings.Skin = "" Then
            cmbSkin.Text = "Default"
        Else
            cmbSkin.Text = My.Settings.Skin
        End If
    End Sub

#End Region

#Region "InitDriveListView"

    Private Sub InitDriveListView()
        lsvDrive.Items.Clear()
        For Each di As DriveInfo In DriveInfo.GetDrives()
            If di.IsReady And di.DriveType = DriveType.Fixed Then
                Dim item As New ListViewItem(di.Name.TrimEnd("\\") & " " & If(di.VolumeLabel <> "", di.VolumeLabel, "Local Disk"))
                item.SubItems.AddRange({"", ""})
                item.Name = di.Name.TrimEnd({":"c, "\"c})

                If My.Settings.Drives.Contains(di.Name.TrimEnd("\\")) Then
                    item.Checked = True
                End If

                item.Tag = di.Name.TrimEnd("\\")
                lsvDrive.Items.Add(item)
            End If
        Next
    End Sub

#End Region

#Region "InitIndicator"

    Private Sub InitIndicator()
        For Each item As CIndicator In IndicatorList
            item.Dispose()
        Next

        IndicatorList.Clear()

        Dim d As New List(Of String)

        Try
            For Each item As String In My.Settings.Drives.Trim.Split(" ")
                Dim di = New IO.DriveInfo(item)
                If di.IsReady Then
                    d.Add(item)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try

        '// load skin.
        Dim _skin = New CSkin()
        _skin.Load(SkinFile, My.Settings.Skin)

        If _skin.Wide Then
            For i As Integer = 0 To d.Count - 1
                IndicatorList.Add(New CIndicator({d(i)}, _skin))
            Next
        Else
            For i As Integer = 0 To d.Count - If(d.Count Mod 2 <> 0, 2, 1) Step 2
                IndicatorList.Add(New CIndicator({d(i), d(i + 1)}, _skin))
            Next
            If d.Count Mod 2 <> 0 Then
                IndicatorList.Add(New CIndicator({d(d.Count - 1)}, _skin))
            End If
        End If
    End Sub

    Private elapsed As Integer = 0

    Private Sub tmrGlobal_Tick(sender As Object, e As EventArgs) Handles tmrGlobal.Tick
        If My.Settings.RestartEveryHour Then
            elapsed += 1
            If elapsed > 3600 Then
                elapsed = 0
                InitIndicator()
            End If
        End If
    End Sub

#End Region

#Region "ListView Mouse Sorting"

    Private Sub lsvDrive_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lsvDrive.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub lsvDrive_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lsvDrive.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub lsvDrive_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lsvDrive.DragDrop
        Dim lvItem As ListViewItem
        Dim destItem As ListViewItem
        Dim destLv As ListView = CType(sender, ListView)
        Dim clX As Integer = destLv.PointToClient(New Point(e.X, e.Y)).X
        Dim clY As Integer = destLv.PointToClient(New Point(e.X, e.Y)).Y
        If e.Data.GetDataPresent("System.Windows.Forms.ListViewItem", False) Then
            'dragging a listview item
            lvItem = CType(e.Data.GetData("System.Windows.Forms.ListViewItem"), ListViewItem)
            destItem = CType(sender, ListView).GetItemAt(clX, clY)
            destLv.Items.Insert(destItem.Index, lvItem.Clone)
            lvItem.Remove()
        End If
    End Sub

#End Region

#Region "ContextMenu"

    Public Sub cmsRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsRestore.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub cmsExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsExit.Click
        End
    End Sub

#End Region

#Region "Form Tools"

    Private Sub btnApply_Click(sender As System.Object, e As System.EventArgs) Handles btnApply.Click
        My.Settings.Skin = cmbSkin.Text
        Dim d As String = ""
        For Each item As ListViewItem In lsvDrive.Items
            If item.Checked Then d = d & item.Tag & " "
        Next
        My.Settings.Drives = d
        My.Settings.Save()
        InitIndicator()
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        InitDriveListView()
    End Sub


#End Region

#Region "InitLang"

    Private Sub InitLang()
        If IO.File.Exists(LangFile) Then
            Dim ini = New IniFile11(LangFile)
            TabControl1.TabPages(0).Text = ini.ReadString("[Lang]Drives")
            TabControl1.TabPages(1).Text = ini.ReadString("[Lang]Options")
            TabControl1.TabPages(2).Text = ini.ReadString("[Lang]About")
            btnReload.Text = ini.ReadString("[Lang]Reload")
            btnApply.Text = ini.ReadString("[Lang]Apply")
            lblSkin.Text = ini.ReadString("[Lang]Skin")
            chbMinimized.Text = ini.ReadString("[Lang]Minimized")
            chbToolTip.Text = ini.ReadString("[Lang]Tooltip")
            chbUseScrlLockLed.Text = ini.ReadString("[Lang]UseScrlLockLed")
            chbRestartEveryHour.Text = ini.ReadString("[Lang]RestartEveryHour")
            lblInterval.Text = ini.ReadString("[Lang]Interval")
            cmsRestore.Text = ini.ReadString("[Lang]Restore")
            cmsExit.Text = ini.ReadString("[Lang]Exit")
        End If

        LangFile = "Lang.toml"
        If IO.File.Exists(LangFile) Then
            Dim t = Nett.Toml.ReadFile(LangFile)
            Dim dict = t.ToDictionary()
            TabControl1.TabPages(0).Text = t.Get(Of Nett.TomlTable)("Lang").Get(Of String)("Drives")

            MsgBox("!1")
            'TabControl1.TabPages(0).Text = toml.Get(Of Nett.TomlTable)("Lang").Get(Of String)("Drives")
            'TabControl1.TabPages(1).Text = toml.





            'Get("Lang.Options").ToString

            'TabControl1.TabPages(2).Text = ini.ReadString("[Lang]About")
            'btnReload.Text = ini.ReadString("[Lang]Reload")
            'btnApply.Text = ini.ReadString("[Lang]Apply")
            'lblSkin.Text = ini.ReadString("[Lang]Skin")
            'chbMinimized.Text = ini.ReadString("[Lang]Minimized")
            'chbToolTip.Text = ini.ReadString("[Lang]Tooltip")
            'chbUseScrlLockLed.Text = ini.ReadString("[Lang]UseScrlLockLed")
            'chbRestartEveryHour.Text = ini.ReadString("[Lang]RestartEveryHour")
            'lblInterval.Text = ini.ReadString("[Lang]Interval")
            'cmsRestore.Text = ini.ReadString("[Lang]Restore")
            'cmsExit.Text = ini.ReadString("[Lang]Exit")
        End If

    End Sub

#End Region

#Region "About Text"

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        txtAbout.Text = txtAbout.Text.Replace("%version%", My.Application.Info.Version.ToString)
    End Sub



#End Region

End Class
