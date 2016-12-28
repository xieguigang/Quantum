Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Language.UnixBash
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Webservices.Github
Imports Microsoft.VisualBasic.Webservices.Github.Class
Imports Microsoft.VisualBasic.Webservices.Github.WebAPI

''' <summary>
''' API for build a network model for d3js or cytoscape
''' </summary>
Public Module BuildNetwork

    Public Function DownloadAvatar(DIR$) As Boolean
        Dim users As New List(Of User)

        For Each file$ In ls - l - r - "*.csv" <= DIR
            users += file.LoadCsv(Of User).AsEnumerable
        Next

        Dim distincts = From user As User
                        In users
                        Select user
                        Group user By user.login Into Group

        For Each user In distincts
            Dim name = user.login
            Dim avatar = user.Group.First.avatar_url
            Dim path$ = $"{DIR}/avatar/{name}.jpg"

            Call avatar.DownloadFile(path, WebAPI.Proxy)
        Next

        Return True
    End Function

    ''' <summary>
    ''' Build network model from user relationships.(从一个用户开始构建网络)
    ''' </summary>
    ''' <param name="username"></param>
    ''' <param name="recursionDepth">从最开始的第一个用户开始递归，的最深的深度</param>
    ''' <returns></returns>
    Public Function FromUser(username As String, Optional recursionDepth% = 1, Optional maxFollows% = 50, Optional work$ = Nothing) As Network

        If work Is Nothing Then
            work = App.HOME & "/" & username
        End If

        Dim followers As User() = username.Followers(maxFollows)
        Dim followings As User() = username.Following(maxFollows)
        Dim visited As New List(Of String) '  A list of user name that we already have visited, to avoid the dead loop.
        Dim gets As New List(Of UserModel)

        Call followers.SaveTo(work & "/followers.csv")
        Call followings.SaveTo(work & "/following.csv")

        For Each user As User In followers
            gets += user.login.__visit(recursionDepth, visited, maxFollows, work, $"/{NameOf(followers)}")
        Next
        For Each user As User In followings
            gets += user.login.__visit(recursionDepth, visited, maxFollows, work, $"/{NameOf(followings)}")
        Next

        ' build network model
        Dim userNodes As New Dictionary(Of Node)
        Dim connections As New List(Of NetworkEdge)

        For Each user As UserModel In gets
            userNodes += New Node With {
                .Identifier = user.User.login,
                .NodeType = NameOf(user),
                .Properties = New Dictionary(Of String, String) From {
                    {NameOf(followers), user.Followers.Length},
                    {NameOf(Following), user.Followings.Length},
                    {NameOf(connections), user.Followings.Length + user.Followers.Length}
                }
            }

            For Each follower As String In user.Followers
                connections += New NetworkEdge With {
                    .FromNode = follower,
                    .ToNode = user.User.login,
                    .InteractionType = NameOf(follower)
                }
            Next
            For Each following As String In user.Followings
                connections += New NetworkEdge With {
                    .FromNode = user.User.login,
                    .ToNode = following,
                    .InteractionType = NameOf(following)
                }
            Next
        Next

        Return New Network With {
            .Edges = connections,
            .Nodes = userNodes
        }
    End Function

    ''' <summary>
    ''' Get user's social network
    ''' </summary>
    ''' <param name="username$"></param>
    ''' <param name="recursionDepth%"></param>
    ''' <param name="visited"></param>
    ''' <returns></returns>
    <Extension>
    Private Function __visit(username$, recursionDepth%, visited As List(Of String), maxFollows%, work$, parent$) As UserModel()
        Dim followers, followings As User()

        If visited.IndexOf(username) > -1 Then
            Return {}
        Else
            visited += username

            followings = username.Following(maxFollows)
            followers = username.Followers(maxFollows)
        End If

        Dim out As New List(Of UserModel)
        Dim save As Func(Of UserModel()) =
            Function()
                Dim path$ = work & $"/{parent}/{username}.json"

                Call out _
                    .GetJson(True) _
                    .SaveTo(path, encoding:=Encoding.UTF8)

                path = work & $"/{parent}/followers/{username}.csv"
                Call followers.SaveTo(path)
                path = work & $"/{parent}/following/{username}.csv"
                Call followings.SaveTo(path)

                Return out.ToArray
            End Function

        out += New UserModel With {
            .User = New User With {
                .login = username
            },
            .Followers = followers.ToArray(Function(u) u.login),
            .Followings = followings.ToArray(Function(u) u.login)
        }

        If recursionDepth < 0 Then
            Return save()
        Else

        End If

        For Each follower As User In followers
            out += follower.login.__visit(recursionDepth - 1, visited, maxFollows, work, $"/{parent}/{username}/{NameOf(follower)}")
        Next
        For Each following As User In followings
            out += following.login.__visit(recursionDepth - 1, visited, maxFollows, work, $"/{parent}/{username}/{NameOf(following)}")
        Next

        Return save()
    End Function

    ''' <summary>
    ''' 从一个组织开始构建网络
    ''' </summary>
    ''' <param name="org"></param>
    ''' <returns></returns>
    Public Function FromOrganization(org As String) As Network

    End Function

    ''' <summary>
    ''' 从一个源开始构建网络
    ''' </summary>
    ''' <param name="repo"></param>
    ''' <returns></returns>
    Public Function FromRepository(repo As String) As Network

    End Function
End Module
