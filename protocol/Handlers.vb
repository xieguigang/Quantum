Imports System.Text
Imports Microsoft.VisualBasic.Net
Imports Microsoft.VisualBasic.Net.Tcp
Imports Microsoft.VisualBasic.Parallel
Imports Microsoft.VisualBasic.Text

Public Module Handlers

    ReadOnly textEncoder As Encoding = Encodings.UTF8.CodePage

    Public Function GetHttpText(url As String, proxy As String, Optional headers As Dictionary(Of String, String) = Nothing) As String
        Dim obj As New requestWeb With {
            .headers = If(headers, New Dictionary(Of String, String)),
            .method = "GET",
            .url = url
        }

        Dim message = requestPackage(Of requestWeb).Create(obj)
        Dim request As New RequestStream(Globals.protocol, Protocols.requestWeb, message.Serialize)
        Dim result As RequestStream = New TcpRequest(New IPEndPoint(proxy)).SendMessage(request)

        If result.ProtocolCategory <> 200 Then
            Throw New Exception(result.GetString(Encoding.UTF8))
        End If

        Dim tmp As Byte() = Globals.DecryptData(message.random, result.ChunkBuffer)
        Dim text As String = textEncoder.GetString(tmp)

        Return text
    End Function

    Public Function GetFile(url As String, saveAs As String, proxy As String) As Boolean
        Throw New NotImplementedException
    End Function
End Module
