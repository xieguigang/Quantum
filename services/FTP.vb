Imports System.IO
Imports System.Net

Public Class FTP

    Public Shared Iterator Function ListDirectory(target As String, Optional userName$ = Nothing, Optional password$ = Nothing) As IEnumerable(Of String)
        Dim request As FtpWebRequest = DirectCast(WebRequest.Create(target), FtpWebRequest)

        request.Method = WebRequestMethods.Ftp.ListDirectory

        If Not (userName.StringEmpty OrElse password.StringEmpty) Then
            request.Credentials = New NetworkCredential(userName, password)
        End If

        Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Dim responseStream As Stream = response.GetResponseStream

            Using reader As New StreamReader(responseStream)
                Do While reader.Peek <> -1
                    Yield reader.ReadLine
                Loop
            End Using
        End Using
    End Function
End Class
