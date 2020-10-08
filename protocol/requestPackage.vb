Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.BSON
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports Microsoft.VisualBasic.Serialization
Imports randf = Microsoft.VisualBasic.Math.RandomExtensions

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

    Public Shared Function Create(obj As T) As requestPackage(Of T)
        Static seed As Random = randf.seeds

        Return New requestPackage(Of T) With {
            .data = obj,
            .random = seed.NextDouble
        }
    End Function

    Public Shared Function CreateObject(buffer As Byte()) As requestPackage(Of T)
        Dim random As Double = Globals.GetRandomKey(buffer)
        Dim data As Byte() = Globals.DecryptData(buffer)
        Dim json As JsonObject = BSONFormat.Load(data)
        Dim obj As T = json.CreateObject(Of T)

        Return New requestPackage(Of T) With {
            .data = obj,
            .random = random
        }
    End Function
End Class