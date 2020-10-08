Imports System.Reflection
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.SecurityString

Public Module Globals

    Public Property Hash1 As String = getHashKey(GetType(App).Assembly)
    Public Property Hash2 As String = getHashKey(GetType(Globals).Assembly)
    Public ReadOnly Property byteOrder As ByteOrder = ByteOrderHelper.SystemByteOrder

    Sub New()

    End Sub

    Private Function getHashKey(assm As Assembly) As String
        Dim file As String = assm.Location.GetFullPath
        Dim md5 As String = SecurityString.GetFileMd5(file)

        Return md5.ToLower
    End Function

    Public Function EncryptData(random As Double, data As Byte()) As Byte()
        Dim header = ByteOrderHelper.GetBytes(random)
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

    Public Function GetRandomKey(buffer As Byte()) As Double
        Dim chunk8 As Byte() = New Byte(7) {}

        Call Array.ConstrainedCopy(buffer, Scan0, chunk8, Scan0, 8)

        If byteOrder = ByteOrder.LittleEndian Then
            Call Array.Reverse(chunk8)
        End If

        Return BitConverter.ToDouble(chunk8, Scan0)
    End Function

End Module
