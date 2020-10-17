Imports System
Imports System.Drawing
Imports System.Drawing.Text

Public Structure CSkinDrive
    Public Font As Font
    Public FontRect As Rectangle
    Public Brush As SolidBrush
    Public Image() As Bitmap
    Public ImageRect As Rectangle
End Structure

Public Class CSkin
    Public Wide As Boolean
    Public DriveLetter As Boolean
    Public RenderingHint As TextRenderingHint = TextRenderingHint.SystemDefault
    Public Drives(1) As CSkinDrive

    Public Sub New()
    End Sub

    Public Sub Load(ByVal skinFile As String, ByVal skinName As String)
        Dim ini = New IniFile11(skinFile)
        Dim skinPath = "[" & skinName & "]"

        Wide = ini.ReadBoolean(skinPath & "Wide", False)
        DriveLetter = ini.ReadBoolean(skinPath & "DriveLetter", False)
        RenderingHint = ini.ReadInteger(skinPath & "TextRenderingHint", TextRenderingHint.SystemDefault)

        For i As Integer = 0 To 1
            Dim skinPathDrive = skinPath & "[Drive" & i.ToString & "]"
            Try
                Drives(i).FontRect = ini.ReadRectangle(skinPathDrive & "FontRect", New Rectangle(i * 8, 8, 8, 8))
                Drives(i).Font = ini.ReadFont(skinPathDrive & "Font", New Font("Tahoma", 6))
                Drives(i).Brush = New SolidBrush(ini.ReadColor(skinPathDrive & "FontColor", Color.WhiteSmoke))
            Catch ex As Exception
                DriveLetter = False
                Drives(i).FontRect = Nothing
                Drives(i).Font = Nothing
                Drives(i).Brush = Nothing
            End Try
            Try
                Drives(i).ImageRect = ini.ReadRectangle(skinPathDrive & "ImageRect", New Rectangle(i * 8, 0, 8, 8))
                Drives(i).Image = {
                    New Bitmap(ini.ReadString(skinPathDrive & "xx", "")),
                    New Bitmap(ini.ReadString(skinPathDrive & "Rx", "")),
                    New Bitmap(ini.ReadString(skinPathDrive & "xW", "")),
                    New Bitmap(ini.ReadString(skinPathDrive & "RW", ""))}
            Catch ex As Exception
                Drives(i).ImageRect = New Rectangle(i * 8, 0, 8, 16)
                Drives(i).Image = New Bitmap() {My.Resources.xx, My.Resources.Rx, My.Resources.xW, My.Resources.RW}
            End Try
        Next
    End Sub
End Class
