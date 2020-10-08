''' <summary>
''' request web text
''' </summary>
Public Class requestWeb

    Public Property url As String
    Public Property headers As Dictionary(Of String, String)
    Public Property method As String

    Public Function HttpRequest() As String
        If method.TextEquals("GET") Then
            Return url.GET(headers:=headers)
        Else
            Return url.POST(headers:=headers)
        End If
    End Function

End Class
