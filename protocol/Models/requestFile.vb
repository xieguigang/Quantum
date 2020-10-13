Imports System.IO
Imports Microsoft.VisualBasic.Net.Http

Public Class requestFile : Inherits httpRequest

    Public Sub HttpRequest(response As Stream)
        Call wget.Download(url, response)
    End Sub
End Class
