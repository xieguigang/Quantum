Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.Net
Imports Microsoft.VisualBasic.Net.Protocols.Reflection
Imports Microsoft.VisualBasic.Net.Tcp
Imports Microsoft.VisualBasic.Parallel
Imports Microsoft.VisualBasic.Text
Imports Proxy.Protocol

<Protocol(GetType(Protocols))>
Public Class ListenServer : Implements ITaskDriver, IDisposable

    Dim socket As TcpServicesSocket
    Dim protocol As New ProtocolHandler(Me)

    Private disposedValue As Boolean

    Sub New(port As Integer)
        socket = New TcpServicesSocket(port) With {
            .ResponseHandler = AddressOf protocol.HandleRequest
        }
    End Sub

    Public Function Run() As Integer Implements ITaskDriver.Run
        Return socket.Run
    End Function

    <Protocol(Protocols.requestWeb)>
    Friend Function requestWeb(request As RequestStream) As BufferPipe
        Dim pkg = requestPackage(Of requestWeb).CreateObject(request.ChunkBuffer)
        Dim text As String = pkg.data.HttpRequest
        Dim result As Byte() = Encodings.UTF8WithoutBOM.CodePage.GetBytes(text)

        result = Globals.EncryptData(pkg.random, result, appendSalt:=False)

        Return New DataPipe(New RequestStream(200, 0, result))
    End Function

    <Protocol(Protocols.downloadFile)>
    Friend Function requestFile(request As RequestStream) As BufferPipe
        Return requestPackage(Of requestFile) _
            .CreateObject(request.ChunkBuffer).data _
            .HttpRequest()
    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
                Call socket.Dispose()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
