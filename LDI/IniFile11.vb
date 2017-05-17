Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' IniFile11(IniFile version 1.1) class
''' </summary>
''' <remarks></remarks>
Public Class IniFile11
    Implements IDisposable

#Region "Public Properties"

    ''' <summary>
    ''' Returns a Boolean value indicating the state of the ignoreCase flag.
    ''' </summary>
    ''' <remarks>Deffault is True.</remarks>
    Public IgnoreCase As Boolean
    'Public Property IgnoreCase() As Boolean
    '    Get
    '        Return RegexOptions
    '    End Get
    '    Set(value As Boolean)
    '        RegexOptions = value
    '    End Set
    'End Property

#Region "-RegexOptions"

    ''' <summary>
    ''' Regular Expression option.
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RegexOptions As RegexOptions = RegexOptions.IgnoreCase

#End Region

#Region "-SectionPathDelimiter"

    ''' <summary>
    ''' Section path delimiter. 
    ''' </summary>
    ''' <remarks>Default is '/'.</remarks>
    Public SectionPathDelimiter As String = "/"

#End Region

#Region "-NameValueDelimiter"

    ''' <summary>
    ''' Key name and value delimiter.
    ''' </summary>
    ''' <remarks>Default is '='.</remarks>
    Public NameValueDelimiter As String = "="

#End Region

#Region "-Encoding"

    ''' <summary>
    ''' 設定ファイルの文字エンコーディングです。
    ''' </summary>
    ''' <remarks>
    ''' デフォルトは UTF-8 で下記の文字エンコーディングが使えます。
    ''' IBM037         x-cp20001        IBM01146          iso-8859-1
    ''' IBM437         x-Chinese-Eten   IBM01147          iso-8859-2
    ''' IBM500         x-cp20003        IBM01148          iso-8859-3
    ''' ASMO-708       x-cp20004        IBM01149          iso-8859-4
    ''' DOS-720        x-cp20005        utf-16            iso-8859-5
    ''' ibm737         x-IA5            unicodeFFFE       so-8859-6
    ''' ibm775         x-IA5-German     windows-1250      iso-8859-7
    ''' ibm850         x-IA5-Swedish    windows-1251      iso-8859-8
    ''' ibm852         x-IA5-Norwegian  Windows -1252     iso-8859-9
    ''' IBM855         us-ascii         windows-1253      iso-8859-13
    ''' ibm857         x-cp20261        windows-1254      iso-8859-15
    ''' IBM00858       x-cp20269        windows-1255      x-Europa
    ''' IBM860         IBM273           windows-1256      iso-8859-8-i
    ''' ibm861         IBM277           windows-1257      iso-2022-jp
    ''' DOS-862        IBM278           windows-1258      csISO2022JP
    ''' IBM863         IBM280           Johab             iso-2022-jp
    ''' IBM864         IBM284           macintosh         iso-2022-kr
    ''' IBM865         IBM285           x-mac-japanese    x-cp50227
    ''' cp866          IBM290           x-mac-chinesetrad
    ''' ibm869         IBM297           x-mac-korean      EUC-CN
    ''' IBM870         IBM420           x-mac-arabic      euc-kr
    ''' windows-874    IBM423           x-mac-hebrew      hz-gb-2312
    ''' cp875          IBM424           x-mac-greek       GB18030
    ''' shift_jis      x-mac-cyrillic   x-iscii-de        x-iscii-be
    ''' gb2312         IBM-Thai         x-mac-chinesesimp
    ''' ks_c_5601-1987 koi8-r           x-mac-romanian    x-iscii-ta
    ''' big5           IBM871           x-mac-ukrainian   x-iscii-te
    ''' IBM1026        IBM880           x-mac-thai        x-iscii-as
    ''' IBM01047       IBM905           x-mac-ce          x-iscii-or
    ''' IBM01140       IBM00924         x-mac-icelandic   x-iscii-ka
    ''' IBM01141       EUC-JP           x-mac-turkish     x-iscii-ma
    ''' IBM01142       x-cp20936        x-mac-croatian    x-iscii-gu
    ''' IBM01143       x-cp20949        utf-32            x-iscii-pa
    ''' IBM01144       cp1025           utf-32BE          utf-7
    ''' IBM01145       koi8-u           x-Chinese-CNS     utf-8
    ''' x-EBCDIC-KoreanExtended         euc-jp
    ''' </remarks>
    Public Encoding As System.Text.Encoding = System.Text.Encoding.UTF8

#End Region

#Region "-Parameter"

    ''' <summary>
    ''' データ構造を保持する Dictionary クラスです。
    ''' </summary>
    Public Parameter As Dictionary(Of String, String)

#End Region

#End Region

#Region "Private Properties"

    Private _encoding As System.Text.Encoding
    Private _secDml As String() = {"[", "[/", "]", "]"}
    Private _slcd As String() = {"#", "//", ";"}
    Private _mlcd As String() = {"/*", "*/"}

#End Region

#Region "Public Function"

#Region "-Constructor"

    ''' <overloads>
    ''' </overloads>
    ''' <summary>
    ''' Initializes a new instance of the StreamReader class for the specified file name, with the specified character encoding.
    ''' </summary>
    ''' <param name="Path">The complete file path to be read.</param>
    ''' <remarks></remarks>
    ''' <example>The following code implements the examples from this section.
    ''' <code>
    '''    Dim ini As New IniFile11("C:\Users\Dev\Keyboard.ini")
    '''    Dim key As String = ini.GetVal("[Function][Player1]Space", "Shot")
    ''' </code>
    ''' </example>
    Public Sub New(ByVal path As String, Optional ByVal ignoreCase As Boolean = True)
        If ignoreCase Then
            Parameter = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Else
            Parameter = New Dictionary(Of String, String)
        End If
        Load(path)
    End Sub

#End Region

#Region "-Generic Procedure"

#Region "--GetVal"

    ''' <summary>
    ''' 指定されたキーに関連付けられている値を取得します。
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定の値</param>
    ''' <returns>キー名に対応した値</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    ''' <example>このサンプルは、<c>GetVal()</c> メソッドの呼び出し方法を示します。
    ''' <code>
    '''    Dim ini As New Ini11("C:\Users\Dev\Keyboard.ini")
    '''    Dim key As String = ini.GetVal("[Function][Player1]Space", "Shot")
    ''' </code>
    ''' </example>
    Public Function GetVal(Of t)(ByVal keyPath As String, ByVal defaultValue As t) As t
        Try
            If String.IsNullOrEmpty(Parameter(keyPath)) Then Return defaultValue
            Return CType(CType(Parameter(keyPath), Object), t)
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "--SetVal"

    ''' <summary>
    ''' 指定されたキーに関連付けられた値を設定します。
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="Value">値</param>
    ''' <returns>キー名に対応した値</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    ''' <example>このサンプルは、<c>SetVal()</c> メソッドの呼び出し方法を示します。
    ''' <code>
    '''    Dim ini As New Ini11("C:\Users\Dev\Keyboard.ini")
    '''    Dim key As String = ini.SetVal("[Function][Player1]Space", "Shot")
    ''' </code>
    ''' </example>
    Public Function SetVal(Of t)(ByVal keyPath As String, ByVal Value As t) As t
        If Parameter.ContainsKey(keyPath) Then
            Parameter(keyPath) = Value.ToString
        Else
            Parameter.Add(keyPath, Value.ToString)
        End If
        Return Value
    End Function

#End Region

#End Region

#Region "-ReadString"

    ''' <summary>
    ''' Retrieves a string from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定の文字列</param>
    ''' <returns>型<see cref="System.String"/>キー名に対応した文字列</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    ''' <example>このサンプルは、<c>ReadString()</c> メソッドの呼び出し方法を示します。
    ''' <code>
    '''    Dim ini As New ini11("C:\Users\Dev\Keyboard.ini")
    '''    Dim key As String = ini.ReadString("[Function][Player1]Space", "Shot")
    ''' </code>
    ''' </example>
    Public Function ReadString(ByVal keyPath As String, Optional ByVal defaultValue As String = "") As String
        Try
            Return Parameter(keyPath)
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadInteger"

    ''' <summary>
    ''' Retrieves a integer from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定の整数値</param>
    ''' <returns>キー名に対応した整数値</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    Public Function ReadInteger(ByVal keyPath As String, ByVal defaultValue As Integer) As Integer
        Try
            Return CInt(Parameter(keyPath))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadBoolean"

    ''' <summary>
    ''' Retrieves a boolean from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定の真理値</param>
    ''' <returns>キー名に対応した真理値</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    Public Function ReadBoolean(ByVal keyPath As String, ByVal defaultValue As Boolean) As Boolean
        Try
            Select Case Parameter(keyPath)
                Case "0"
                Case "False"
                    Return False
                Case Else
                    Return True
            End Select
            Return True
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadFloat"

    ''' <summary>
    ''' Retrieves a float from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定の浮動小数点値</param>
    ''' <returns>キー名に対応した浮動小数点値</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    Public Function ReadFloat(ByVal keyPath As String, ByVal defaultValue As Double) As Double
        Try
            Return CDbl(Parameter(keyPath))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadFont"

    ''' <summary>
    ''' Retrieves a font from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定の Font</param>
    ''' <returns>キー名に対応した Font</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合は既定の値が返ります。</remarks>
    ''' <example>
    ''' ' [Path]
    ''' ' Key = MS Gothic, 22
    ''' Dim f = ini.ReadFont("/Path/Key", New Font("Meiryo", 12))
    ''' </example>
    Public Function ReadFont(ByVal keyPath As String, ByVal defaultValue As Font) As Font
        Try
            Dim foo = Parameter(keyPath).Split(",")
            Return New Font(foo(0).Trim, CSng(foo(1).Trim))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadSize"

    ''' <summary>
    ''' Retrieves a size from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定のサイズ</param>
    ''' <returns>キー名に対応したサイズ</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    Public Function ReadSize(ByVal keyPath As String, ByVal defaultValue As Size) As Size
        Try
            Dim foo = Parameter(keyPath).Split(",")
            Return New Size(CInt(foo(0).Trim), CInt(foo(1).Trim))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadPoint"

    ''' <summary>
    ''' Retrieves a point from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="defaultValue">既定の座標</param>
    ''' <returns>キー名に対応した値</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合と、NotBlank が真で戻り値が空の場合は既定の値が返ります。</remarks>
    Public Function ReadPoint(ByVal keyPath As String, ByVal defaultValue As Point) As Point
        Try
            Dim foo = Parameter(keyPath).Split(",")
            Return New Point(CInt(foo(0).Trim), CInt(foo(1).Trim))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadRectangle"

    ''' <summary>
    ''' Retrieves a rectangle from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">Key Name</param>
    ''' <param name="defaultValue">Defaiult Value</param>
    ''' <returns>キー名に対応した値</returns>
    ''' <remarks>関連付けられている値が取得できなかった場合は既定の値が返ります。</remarks>
    Public Function ReadRectangle(ByVal keyPath As String, ByVal defaultValue As Rectangle) As Rectangle
        Try
            Dim foo = Parameter(keyPath).Split(",")
            Return New Rectangle(CInt(foo(0).Trim), CInt(foo(1).Trim), CInt(foo(2).Trim), CInt(foo(3).Trim))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadColor"

    ''' <summary>
    ''' Retrieves a color from the specified section in an initialization file.
    ''' </summary>
    ''' <param name="keyPath">Key Path</param>
    ''' <param name="defaultValue">
    ''' A default coloe. If the lpKeyName key cannot be found in the initialization file, ini11７ copies the default color to the return buffer.
    ''' If this parameter is NULL, the default is Color.Black.
    '''</param>
    ''' <returns>キー名に対応した Color 値</returns>
    ''' <remarks>If the lpKeyName key cannot be found in the initialization file, this function returns defaultValue.</remarks>
    ''' <example>
    ''' ' [Path]
    ''' ' Red = Red
    ''' ' Green = #008800
    ''' </example>
    Public Function ReadColor(ByVal keyPath As String, Optional ByVal defaultValue As Color = Nothing) As Color
        defaultValue = Color.Black
        Try
            Return ColorTranslator.FromHtml(Parameter(keyPath).Replace("#", "0x"))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

#End Region

#Region "-ReadKeyRegex"

    ''' <summary>
    ''' Options パラメータに一致オプションを指定し、Pattern パラメータに指定した正規表現に一致するキー名をすべて検索します。
    ''' </summary>
    ''' <param name="Pattern">一致させる正規表現パターン</param>
    ''' <param name="Groupnum">Groupnum 番目のグルーピングを戻り値として得ます。既定は 0 です。</param>
    ''' <returns>検索された文字列の配列</returns>
    ''' <remarks></remarks>
    Public Function ReadKeyRegex(ByVal Pattern As String, Optional ByVal Groupnum As Integer = 0) As String()
        Dim rtn As New List(Of String)
        Dim ex As String = ""
        For Each p As KeyValuePair(Of String, String) In Parameter
            For Each m As Match In Regex.Matches(p.Key, Pattern, RegexOptions)
                If m.Value <> ex Then rtn.Add(m.Groups(Groupnum).Value)
                ex = m.Value
            Next
        Next
        Return rtn.ToArray
    End Function

#End Region

#Region "-ReadSections"
    Public Function ReadSections() As String()
        Dim rtn As New List(Of String)
        Dim ex As String = ""
        For Each p As KeyValuePair(Of String, String) In Parameter
            For Each m As Match In Regex.Matches(p.Key, "(\[.+\])", RegexOptions)
                If m.Value <> ex Then rtn.Add(m.Groups(0).Value)
                ex = m.Value
            Next
        Next
        Return rtn.ToArray
    End Function
#End Region

#Region "-ReadSectionValues"
	Public Function ReadSectionValues(ByVal sec As String) As String()
		Dim rtn As New List(Of String)
		Dim ex As String = ""
		For Each p As KeyValuePair(Of String, String) In Parameter
			For Each m As Match In Regex.Matches(p.Key, "^\[" & sec & "\]", RegexOptions)
				If m.Value <> ex Then rtn.Add(m.Groups(0).Value)
				ex = m.Value
			Next
		Next
		Return rtn.ToArray
	End Function
#End Region

#Region "-WriteString"

    ''' <summary>
    ''' 指定されたキーに文字列を設定します。
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="Value">文字列</param>
    ''' <returns>設定した文字列が返ります。</returns>
    ''' <remarks>Save メソッドが呼ばれるまでは、ファイルに書き込まれません。</remarks>
    Public Function WriteString(ByVal keyPath As String, ByVal Value As String) As String
        If Parameter.ContainsKey(keyPath) Then
            Parameter(keyPath) = CStr(Value)
        Else
            Parameter.Add(keyPath, CStr(Value))
        End If
        Return Value
    End Function

#End Region

#Region "-WriteInteger"

    ''' <summary>
    ''' 指定されたキーに整数値を設定します。
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="Value">整数値</param>
    ''' <returns>設定した整数値が返ります。</returns>
    ''' <remarks>Save メソッドが呼ばれるまでは、ファイルに書き込まれません。</remarks>
    Public Function WriteInteger(ByVal keyPath As String, ByVal Value As Integer) As Integer
        If Parameter.ContainsKey(keyPath) Then
            Parameter(keyPath) = CStr(Value)
        Else
            Parameter.Add(keyPath, CStr(Value))
        End If
        Return Value
    End Function

#End Region

#Region "-WriteBoolean"

    ''' <summary>
    ''' 指定されたキーに真理値を設定します。
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <param name="Value">真理値</param>
    ''' <returns>設定した真理値が返ります。</returns>
    ''' <remarks>Save メソッドが呼ばれるまでは、ファイルに書き込まれません。</remarks>
    Public Function WriteBoolean(ByVal keyPath As String, ByVal Value As Boolean) As Boolean
        If Parameter.ContainsKey(keyPath) Then
            Parameter(keyPath) = CStr(Value)
        Else
            Parameter.Add(keyPath, CStr(Value))
        End If
        Return Value
    End Function

#End Region

#Region "-Remove"

    ''' <summary>
    ''' 指定したキーを持つパラメータを削除します。 
    ''' </summary>
    ''' <param name="keyPath">キー名</param>
    ''' <returns>パラメータが正常に削除された場合は true。それ以外の場合は false。このメソッドは、key が Dictionary に見つからない場合にも false  を返します。 </returns>
    ''' <remarks></remarks>
    Public Function Remove(ByVal keyPath As String) As Boolean
        Return Parameter.Remove(keyPath)
    End Function

#End Region

#Region "-RemoveRegex"

    ''' <summary>
    ''' 指定した正規表現に一致するキーを持つパラメータを削除します。 
    ''' </summary>
    ''' <param name="Pattern">一致させる正規表現パターン</param>
    ''' <returns>削除されたパラメータの件数。</returns>
    ''' <remarks></remarks>
    Public Function RemoveRegex(ByVal Pattern As String) As Integer
        Dim rtn As Integer = 0
        For Each p As KeyValuePair(Of String, String) In Parameter
            If Regex.IsMatch(p.Key, Pattern, RegexOptions) Then
                Parameter.Remove(p.Key)
                rtn = rtn + 1
            End If
        Next
        Return rtn
    End Function

#End Region

#Region "-Clear"

    ''' <summary>
    ''' すべてのキーと値を削除します。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        Parameter.Clear()
    End Sub

#End Region

#Region "-Load"

    ''' <summary>
    ''' 指定された .ini ファイルを読み込みます。
    ''' </summary>
    ''' <param name="Path">完全修飾名または相対ファイル名。</param>
    ''' <remarks></remarks>
    Public Function Load(ByVal Path As String) As Boolean
        Return ReadBody(Path)
    End Function

#End Region

#Region "-Save"

    ''' <summary>
    ''' 指定された .ini ファイルに書き込みます。
    ''' </summary>
    ''' <param name="Path">完全修飾名または相対ファイル名。</param>
    ''' <remarks></remarks>
    Public Function Save(ByVal Path As String) As Boolean
        Dim sb As New StringBuilder
        Dim exParam = Regex.Matches("", "(\[.*?\])")

        Parameter.Add("", "")

        For Each v As KeyValuePair(Of String, String) In Parameter
            Dim matches = Regex.Matches(v.Key, "(\[.*?\])")

            '// 階層が深くなった
            If exParam.Count < matches.Count Then
                For n = exParam.Count To matches.Count - exParam.Count - 1
                    sb.AppendLine(Space(n * 4) & matches(n).Value)
                Next
            End If

            '// 階層が浅くなった
            If exParam.Count > matches.Count Then
                For n = exParam.Count - 1 To matches.Count Step -1
                    sb.AppendLine(Space(n * 4) & exParam(n).Value.Replace("[", "[/"))
                Next
            End If

            '//
            If exParam.Count = matches.Count And matches.Count > 0 Then
                If matches(matches.Count - 1).Value <> exParam(exParam.Count - 1).Value Then
                    '// end tag
                    sb.AppendLine(exParam(exParam.Count - 1).Value).Replace("[", "[/")
                    '// start tag
                    sb.AppendLine(matches(matches.Count - 1).Value)
                End If
            End If

            Dim match = Regex.Match(v.Key, "\](.*?)$")
            If Not String.IsNullOrEmpty(match.Groups(1).Value) Then
                sb.AppendLine(match.Groups(1).Value & NameValueDelimiter & v.Value)
            End If

            exParam = matches
        Next

        Try
            Using sw = New IO.StreamWriter(Path, False, Encoding)
                sw.Write(sb.ToString)
            End Using
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

#End Region

#End Region

#Region "Private Function"

#Region "-ReadBody"

    Private Function ReadBody(ByVal path As String) As Boolean
        Dim _work As String
        Dim _curSec As String = ""
        Dim _lines() As String = Nothing
        Dim _sectionPath As New List(Of String)

        Parameter.Clear()

        Try
            _lines = File.ReadAllLines(path, Encoding)
        Catch ex As Exception
            Return False
        End Try

        For i As Integer = 0 To _lines.Length - 1
            If IgnoreCase Then
                _work = _lines(i).Trim.ToUpper
            Else
                _work = _lines(i).Trim
            End If

            '// comment or empty line
            If String.IsNullOrEmpty(_work) Or _work.StartsWith(";") Then
                Continue For
            End If

            '// start of section
            If _work.StartsWith("[") And Not _work.StartsWith("[/") Then
                _sectionPath.Add(_work)
                Continue For
            End If

            '// end of section
            If _work.StartsWith("[/") Then
                _work = _work.Replace("[/", "[")
                _sectionPath.RemoveAt(_sectionPath.Count - 1)
                Continue For
            End If

            '// get key and value
            If _work.Contains(NameValueDelimiter) Then
                Dim _pair = _work.Split(NameValueDelimiter.ToCharArray, 2)
                'If _pair(1).Trim = "==" Or _pair(1).Trim = "==-" Then
                '    Dim _sb As New StringBuilder
                '    i = i + 1
                '    While _lines(i).Trim <> "=="
                '        If _pair(1).Trim = "==-" Then '// 行頭のインデントを無視
                '            _sb.AppendLine(_lines(i).TrimStart)
                '        Else
                '            _sb.AppendLine(_lines(i))
                '        End If
                '        i = i + 1
                '    End While
                '    _sb.Remove(_sb.Length - 4, 4)
                '    _pair(1) = _sb.ToString
                'End If

                Dim _currentPath = ""
                For Each item As String In _sectionPath
                    _currentPath &= item
                Next

                Parameter.Add(_currentPath & _pair(0).Trim, _pair(1).Trim)

                'If _curSec = "-OPTIONS/" Then
                '    Select Case _pair(0).Trim.ToUpper
                '        Case "IGNORECASE"
                '            IgnoreCase = GetBool(_pair(1).Trim.ToUpper)
                '        Case "DELIMITER"
                '            KeyValueDelimiter = _pair(1).Trim.ToUpper
                '        Case "ENCODING"
                '            Encoding = System.Text.Encoding.GetEncoding(_pair(1).Trim)
                '    End Select
                'End If
            End If
        Next
        Return True
    End Function

#End Region

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' 重複する呼び出しを検出するには
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: マネージ状態を破棄します (マネージ オブジェクト)。
            End If
            ' TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下の Finalize() をオーバーライドします。
            ' TODO: 大きなフィールドを null に設定します。
        End If
        Me.disposedValue = True
    End Sub
    ' TODO: 上の Dispose(ByVal disposing As Boolean) にアンマネージ リソースを解放するコードがある場合にのみ、Finalize() をオーバーライドします。
    'Protected Overrides Sub Finalize()
    '    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(ByVal disposing As Boolean) に記述します。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub
    ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(ByVal disposing As Boolean) に記述します。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class