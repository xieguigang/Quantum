Imports System.Threading
Imports Microsoft.VisualBasic.Net.HTTP
Imports Microsoft.VisualBasic.Parallel

Public Class requestFile : Inherits httpRequest

    Public Function HttpRequest() As DuplexPipe
        Return wget.PipeTask(url)
    End Function
End Class
