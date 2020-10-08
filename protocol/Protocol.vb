Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.BSON
Imports Microsoft.VisualBasic.MIME.application.json.Javascript

Public Enum Protocols As Long
    ''' <summary>
    ''' request html text
    ''' </summary>
    requestWeb = 100
    downloadFile = 101
End Enum

Public Class requestPackage(Of T As Class)

    Public Property data As T
    ''' <summary>
    ''' 用于加密<see cref="data"/>部分数据的随机数
    ''' </summary>
    ''' <returns></returns>
    Public Property random As Double

    Private Function getDataBuffer() As Byte()
        Return GetType(T) _
            .GetJsonElement(data, New JSONSerializerOptions) _
            .As(Of JsonObject) _
            .DoCall(AddressOf BSONFormat.GetBuffer) _
            .ToArray
    End Function

End Class