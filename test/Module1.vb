Imports System.Threading
Imports Microsoft.VisualBasic.Net
Imports Microsoft.VisualBasic.Net.Tcp
Imports Microsoft.VisualBasic.Parallel
Imports Microsoft.VisualBasic.Text
Imports Proxy.Protocol

Module Module1

    Sub Main()
        Dim obj As New requestWeb With {
            .headers = New Dictionary(Of String, String) From {{"a", "b"}},
            .method = "GET",
            .url = "http://baidu.com"
        }

        Dim message = requestPackage(Of requestWeb).Create(obj)
        Dim tmp As Byte() = message.Serialize

        Call Thread.Sleep(1000)

        Dim request As New RequestStream(Globals.protocol, Protocols.requestWeb, tmp)
        '   Dim obj2 = requestPackage(Of requestWeb).CreateObject(tmp)
        Dim result As RequestStream = New TcpRequest(New IPEndPoint("127.0.0.1:232")).SendMessage(request)

        Dim text As String = Encodings.UTF8.CodePage.GetString(Globals.DecryptData(message.random, request.ChunkBuffer))

        Pause()
    End Sub

End Module
