Imports Microsoft.VisualBasic.Webservices.Github
Imports Microsoft.VisualBasic.Webservices.Github.API.Search

Module Program

    Sub Main()
        'Dim r = Microsoft.VisualBasic.Webservices.Github.API.Search.Users(New UsersQuery With {.term = "xieguigang"})
        'Dim user = Microsoft.VisualBasic.Webservices.Github.API.GetUser("xieguigang")

        WebAPI.Proxy = "http://127.0.0.1:8087/"
        Try
            Call BuildNetwork.DownloadAvatar("G:\github-network\gitnet\bin\Debug\xieguigang")
        Catch ex As Exception
            Call ex.PrintException
        End Try



        Pause()

        Call BuildNetwork.FromUser("xieguigang", 0, 50).Save(App.HOME & "/xieguigang/")

        Pause()
    End Sub
End Module
