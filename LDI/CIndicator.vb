Imports System
Imports System.Drawing
Imports System.Text
Imports System.Runtime.InteropServices

Friend NotInheritable Class NativeMethods
    Public Sub New()
    End Sub

    <DllImport("USER32.DLL", CharSet:=CharSet.Auto)>
    Public Shared Function DestroyIcon(ByVal handle As IntPtr) As Boolean
    End Function
End Class

Public Class CIndicator
    Implements IDisposable

    Private _icon As New NotifyIcon
    Private _tmr As New Timer
    Private _read(1) As PerformanceCounter
    Private _write(1) As PerformanceCounter
    Private _letter(1) As String
    Private _bmp As Bitmap = New Bitmap(16, 16)
    Private _g As Graphics = Graphics.FromImage(_bmp)
    Private _skin As CSkin
    Private _drivesNum As Integer
    Private _brush As SolidBrush
    Private _tickCount As Long

    Public Sub New(ByVal drives() As String, ByVal skin As CSkin)
        _drivesNum = UBound(drives)
        If _drivesNum < 0 Then Return

        _skin = skin

        For i As Integer = 0 To _drivesNum
            _read(i) = New PerformanceCounter("LogicalDisk", "Disk Read Bytes/sec", drives(i))
            _write(i) = New PerformanceCounter("LogicalDisk", "Disk Write Bytes/sec", drives(i))
            _letter(i) = drives(i).TrimEnd(":")
        Next

        AddHandler _tmr.Tick, AddressOf Tick
        AddHandler _icon.MouseDoubleClick, AddressOf Form1.cmsRestore_Click

        _tmr.Interval = My.Settings.Interval

        _g.TextRenderingHint = _skin.RenderingHint
        _icon.ContextMenuStrip = Form1.cmsIcon
        _icon.Text = "Logical Disk Indicator"
        _icon.Icon = Icon.FromHandle(_bmp.GetHicon()) '// dummy

        While True
            Dim tickCount = Environment.TickCount

            _icon.Visible = True
            tickCount = Environment.TickCount - tickCount

            If tickCount < 4000 Then ' within 4 sec, it's ok.
                Exit While
            Else
                'if fault, turn false again.
                _icon.Visible = False
            End If

            System.Threading.Thread.Sleep(100)
        End While

        _tmr.Enabled = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    ' Protected implementation of Dispose pattern.
    Protected Overridable Sub Dispose(disposing As Boolean)
        If disposing Then
            _tmr.Dispose()
            For i As Integer = 0 To _drivesNum
                _read(i).Dispose()
                _write(i).Dispose()
            Next
            NativeMethods.DestroyIcon(_icon.Icon.Handle)
            _icon.Dispose()
            _bmp.Dispose()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Private read_, write_ As Single
    Private readMin(1), readMax(1), writeMin(1), writeMax(1) As Single
    Private tip_ As String = ""

    Private Sub Tick()
        NativeMethods.DestroyIcon(_icon.Icon.Handle)

        For i As Integer = 0 To _drivesNum
            read_ = _read(i).NextValue
            write_ = _write(i).NextValue

            If readMin(i) < read_ Then readMin(i) = read_
            If readMax(i) > read_ Then readMax(i) = read_
            If writeMin(i) < write_ Then writeMin(i) = write_
            If writeMax(i) > write_ Then writeMax(i) = write_

            _g.DrawImage(_skin.Drives(i).Image(If(write_ > 0, 1, 0) * 2 + If(read_ > 0, 1, 0)), _skin.Drives(i).ImageRect)

            If My.Settings.UseScrlRockLed Then
                If read_ > 0 Or write_ > 0 Then
                    ScrlRockLed(True)
                End If
            End If

            If _skin.DriveLetter Then
                _g.DrawString(_letter(i), _skin.Drives(i).Font, _skin.Drives(i).Brush, _skin.Drives(i).FontRect)
            End If

            If My.Settings.ToolTip Then
                tip_ &= _letter(i) & ": " & Units(read_) & " Rx / " & Units(write_) & " Wx" & vbCrLf
                _icon.Text = tip_
            End If

            If Form1.WindowState = FormWindowState.Normal Then
                Form1.lsvDrive.Items(_letter(i)).SubItems(1).Text = Units(read_)
                Form1.lsvDrive.Items(_letter(i)).SubItems(2).Text = Units(write_)
            End If
        Next

        _icon.Icon = Icon.FromHandle(_bmp.GetHicon())
    End Sub

    Private Sub ScrlRockLed(ByVal value As Boolean)
        If value Then
            If Control.IsKeyLocked(Keys.Scroll) Then
            Else
                SendKeys.Send("{SCROLLLOCK}")
            End If
        Else
            If Control.IsKeyLocked(Keys.Scroll) Then
                SendKeys.Send("{SCROLLLOCK}")
            End If
        End If
    End Sub

    Private Const Ki = 1024
    Private Const Mi = 1024 * 1024
    Private Const Gi = 1024 * 1024 * 1024

    Private Function Units(ByVal v As Single) As String
        If v > Gi Then
            Return String.Format("{0:#,#,0}GB ", v / Gi)
        ElseIf v > Mi Then
            Return String.Format("{0:#,#.0}MB ", v / Mi)
        ElseIf v > Ki Then
            Return String.Format("{0:#,#.0}KB ", v / Ki)
        Else
            Return String.Format("{0:#,#.0}B ", v)
        End If
    End Function

End Class
