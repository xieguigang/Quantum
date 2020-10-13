''' <summary>
''' request web text
''' </summary>
Public Class requestWeb : Inherits httpRequest

    Public Function HttpRequest() As String
        If method.TextEquals("GET") Then
            Return url.GET(headers:=headers)
        Else
            Return url.POST(headers:=headers)
        End If
    End Function

End Class
