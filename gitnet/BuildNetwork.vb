Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Webservices.Github.Class
Imports Microsoft.VisualBasic.Webservices.Github.WebAPI

''' <summary>
''' API for build a network model for d3js or cytoscape
''' </summary>
Public Module BuildNetwork

    ''' <summary>
    ''' Build network model from user relationships.(从一个用户开始构建网络)
    ''' </summary>
    ''' <param name="username"></param>
    ''' <param name="recursionDepth">从最开始的第一个用户开始递归，的最深的深度</param>
    ''' <returns></returns>
    Public Function FromUser(username As String, Optional recursionDepth% = 1, Optional maxFollows% = 50) As Network
        Dim followers As User() = username.Followers(maxFollows)
        Dim followings As User() = username.Following(maxFollows)
        Dim visited As New List(Of String) '  A list of user name that we already have visited, to avoid the dead loop.
        Dim gets As New List(Of UserModel)

        For Each user As User In followers
            gets += user.login.__visit(recursionDepth, visited, maxFollows)
        Next
        For Each user As User In followings
            gets += user.login.__visit(recursionDepth, visited, maxFollows)
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
    Private Function __visit(username$, recursionDepth%, visited As List(Of String), maxFollows%) As UserModel()
        Dim followings = username.Following(maxFollows)
        Dim followers = username.Followers(maxFollows)

        Dim out As New List(Of UserModel)

        out += New UserModel With {
            .User = New User With {
                .login = username
            },
            .Followers = followers.ToArray(Function(u) u.login),
            .Followings = followings.ToArray(Function(u) u.login)
        }

        If recursionDepth < 0 Then
            Return out
        Else
            If visited.IndexOf(username) > -1 Then
                Return out
            Else
                visited += username
            End If
        End If

        For Each follower In followers
            out += follower.login.__visit(recursionDepth - 1, visited, maxFollows)
        Next
        For Each following As User In followings
            out += following.login.__visit(recursionDepth - 1, visited, maxFollows)
        Next

        Return out
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
