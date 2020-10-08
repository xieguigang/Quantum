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
        Return Globals.EncryptData(random, getDataBuffer())
    End Function

    Private Function getDataBuffer() As Byte()
        Return GetType(T) _
            .GetJsonElement(data, New JSONSerializerOptions) _
            .As(Of JsonObject) _
            .DoCall(AddressOf BSONFormat.GetBuffer) _
            .ToArray
    End Function

End Class