Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Language
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
    Public Function FromUser(username As String, Optional recursionDepth% = 10) As Network
        Dim followers As User() = username.Followers
        Dim following As User() = username.Following
        Dim visited As New List(Of String) '  A list of user name that we already have visited, to avoid the dead loop.
        Dim gets As New List(Of User)

        For Each user As User In followers
            gets += user.login.__visit(recursionDepth, visited)
        Next

        ' build network model
        Dim userNodes As New Dictionary(Of Node)
        Dim connections As New List(Of NetworkEdge)

        For Each user As User In gets

        Next

        Return New Network With {
            .Edges = connections,
            .Nodes = userNodes
        }
    End Function

    <Extension>
    Private Function __visit(username$, recursionDepth As int, visited As List(Of String)) As User()

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
