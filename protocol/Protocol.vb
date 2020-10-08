Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.BSON
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports Microsoft.VisualBasic.SecurityString
Imports Microsoft.VisualBasic.Serialization

Public Enum Protocols As Long
    ''' <summary>
    ''' request html text
    ''' </summary>
    requestWeb = 100
    downloadFile = 101
End Enum

Public Class requestPackage(Of T As Class) : Implements ISerializable

    Public Property data As T
    ''' <summary>
    ''' 用于加密<see cref="data"/>部分数据的随机数
    ''' </summary>
    ''' <returns></returns>
    Public Property random As Double

    Public Function Serialize() As Byte() Implements ISerializable.Serialize
        Dim header = ByteOrderHelper.GetBytes(random)
        Dim data = getDataBuffer()
        Dim key As String
        Dim salt As String

        If header(3) Mod 2 = 0 Then
            key = Globals.Hash1
            salt = Globals.Hash2
        Else
            key = Globals.Hash2
            salt = Globals.Hash1
        End If

        ' 进行data数据部分的加密
        Using encrypt As SecurityStringModel = New SHA256(key, salt.Substring(0, 8))
            data = encrypt.Encrypt(header.JoinIterates(data).ToArray)
        End Using

        Dim buffer As Byte() = New Byte(header.Length + data.Length - 1) {}

        Call Array.ConstrainedCopy(header, Scan0, buffer, Scan0, header.Length)
        Call Array.ConstrainedCopy(data, Scan0, buffer, header.Length, data.Length)

        Return buffer
    End Function

    Private Function getDataBuffer() As Byte()
        Return GetType(T) _
            .GetJsonElement(data, New JSONSerializerOptions) _
            .As(Of JsonObject) _
            .DoCall(AddressOf BSONFormat.GetBuffer) _
            .ToArray
    End Function

End Class