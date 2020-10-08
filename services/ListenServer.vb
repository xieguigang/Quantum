Imports Microsoft.VisualBasic.Net
Imports Microsoft.VisualBasic.Net.Protocols.Reflection
Imports Microsoft.VisualBasic.Net.Tcp
Imports Microsoft.VisualBasic.Parallel
Imports Proxy.Protocol

<Protocol(GetType(Protocols))>
Public Class ListenServer

    Dim socket As TcpServicesSocket
    Dim protocol As New ProtocolHandler(Me)

    <Protocol(Protocols.requestWeb)>
    Private Function requestWeb(request As RequestStream, remoteDevcie As IPEndPoint) As RequestStream

    End Function

End Class
