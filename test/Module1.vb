Imports System.Threading
Imports Proxy.Protocol

Module Module1

    Sub Main()
        Call Thread.Sleep(1000)
        Call Console.WriteLine(Handlers.GetHttpText("https://baidu.com", "127.0.0.1:232"))

        Pause()
    End Sub

End Module
