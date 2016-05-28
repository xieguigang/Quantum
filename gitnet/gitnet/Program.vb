Imports Microsoft.VisualBasic.Webservices.Github.API.Search

Module Program

    Sub Main()
        Dim r = Microsoft.VisualBasic.Webservices.Github.API.Search.Users(New UsersQuery With {.term = "xieguigang"})
        Dim user = Microsoft.VisualBasic.Webservices.Github.API.GetUser("xieguigang")
    End Sub
End Module
