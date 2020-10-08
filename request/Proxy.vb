Imports Proxy.Protocol

Public Class Proxy

    ReadOnly proxy As String

    Sub New(proxy As String)
        Me.proxy = proxy
    End Sub

    Public Function GetText(url As String) As String
        Return Handlers.GetHttpText(url, proxy)
    End Function

    Public Overrides Function ToString() As String
        Return proxy
    End Function
End Class
