Imports Proxy.Protocol

Module Module1

    Sub Main()
        Dim obj As New requestWeb With {
            .headers = New Dictionary(Of String, String) From {{"a", "b"}},
            .method = "GET",
            .url = "http://127.0.0.1"
        }

        Dim tmp As Byte() = requestPackage(Of requestWeb).Create(obj).Serialize

        Dim obj2 = requestPackage(Of requestWeb).CreateObject(tmp)

        Pause()
    End Sub

End Module
