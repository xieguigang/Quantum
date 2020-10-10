Imports System.Reflection
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Net.Protocols.Reflection
Imports Microsoft.VisualBasic.SecurityString

Public Module Globals

    Public Property Hash1 As String = getHashKey(GetType(App).Assembly)
    Public Property Hash2 As String = getHashKey(GetType(Globals).Assembly)
    Public ReadOnly Property byteOrder As ByteOrder = ByteOrderHelper.SystemByteOrder
    Public ReadOnly Property protocol As Long

    Public ReadOnly Property verbose As Boolean = False

    Sub New()
        protocol = New ProtocolAttribute(GetType(Protocols)).EntryPoint
        verbose = App.CommandLine("--verbose")

        If verbose Then
            Call "'--verbose' debug option is turn on!".__DEBUG_ECHO
        End If
    End Sub

    Private Function getHashKey(assm As Assembly) As String
        Dim file As String = assm.Location.GetFullPath
        Dim md5 As String = SecurityString.GetFileMd5(file)

        Return md5.ToLower
    End Function

    Public Function EncryptData(random As Double, data As Byte(), Optional appendSalt As Boolean = True) As Byte()
        Dim salt = ByteOrderHelper.GetBytes(random)
        Dim key As String = GetHashKey64(salt)

        If verbose Then
            Call printSaltBytes(salt)
        End If

        ' 进行data数据部分的加密
        Using encrypt As SecurityStringModel = New SHA256(key, salt)
            data = encrypt.Encrypt(data)
        End Using

        If appendSalt Then
            Dim buffer As Byte() = New Byte(salt.Length + data.Length - 1) {}

            Call Array.ConstrainedCopy(salt, Scan0, buffer, Scan0, salt.Length)
            Call Array.ConstrainedCopy(data, Scan0, buffer, salt.Length, data.Length)

            Return buffer
        Else
            Return data
        End If
    End Function

    ''' <summary>
    ''' 使用两个程序模块文件来作为私有的加密证书
    ''' 产生的弊端就是对应版本的服务器端只能够通信于
    ''' 对应版本的客户端，每一次编译都必须要重新进行
    ''' 部署，即使代码文本一摸一样
    ''' </summary>
    ''' <param name="salt"></param>
    ''' <returns></returns>
    Public Function GetHashKey64(salt As Byte()) As String
        If salt(3) Mod 2 = 0 Then
            Return Globals.Hash1 & Globals.Hash2
        Else
            Return Globals.Hash2 & Globals.Hash1
        End If
    End Function

    Public Function GetRandomKey(buffer As Byte()) As Double
        Dim chunk8 As Byte() = New Byte(7) {}

        Call Array.ConstrainedCopy(buffer, Scan0, chunk8, Scan0, 8)

        If byteOrder = ByteOrder.LittleEndian Then
            Call Array.Reverse(chunk8)
        End If

        Return BitConverter.ToDouble(chunk8, Scan0)
    End Function

    Private Sub printSaltBytes(salt As Byte())
        Call $"salt value for decryption is: {salt.Select(Function(a) a.ToHexString).JoinBy("-")}".__DEBUG_ECHO
    End Sub

    Public Function DecryptData(data As Byte()) As Byte()
        Dim salt As Byte() = New Byte(7) {}
        Dim buffer As Byte() = New Byte(data.Length - 8 - 1) {}

        Call Array.ConstrainedCopy(data, Scan0, salt, Scan0, 8)
        Call Array.ConstrainedCopy(data, 8, buffer, Scan0, buffer.Length)

        If verbose Then
            Call printSaltBytes(salt)
        End If

        Using encrypt As SecurityStringModel = New SHA256(GetHashKey64(salt), salt)
            data = encrypt.Decrypt(buffer)
        End Using

        Return data
    End Function

    Public Function DecryptData(random As Double, data As Byte()) As Byte()
        Dim salt = ByteOrderHelper.GetBytes(random)

        Using encrypt As SecurityStringModel = New SHA256(GetHashKey64(salt), salt)
            Return encrypt.Decrypt(data)
        End Using
    End Function

End Module
