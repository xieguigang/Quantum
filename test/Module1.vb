Imports Microsoft.VisualBasic.Net
Imports Microsoft.VisualBasic.Net.Tcp
Imports Microsoft.VisualBasic.Parallel
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

        '   Dim obj2 = requestPackage(Of requestWeb).CreateObject(tmp)
        Dim result = New RequestStream(New TcpRequest(New IPEndPoint("127.0.0.1:232")).SendMessage(tmp))


        Pause()
    End Sub

End Module
