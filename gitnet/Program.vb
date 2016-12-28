Imports Microsoft.VisualBasic.Webservices.Github
Imports Microsoft.VisualBasic.Webservices.Github.API.Search

Module Program

    Sub Main()
        'Dim r = Microsoft.VisualBasic.Webservices.Github.API.Search.Users(New UsersQuery With {.term = "xieguigang"})
        'Dim user = Microsoft.VisualBasic.Webservices.Github.API.GetUser("xieguigang")

        WebAPI.Proxy = "http://127.0.0.1:8087/"

        Call BuildNetwork.FromUser("xieguigang", 2, 65).Save(App.HOME & "/xieguigang/")

    End Sub
End Module
