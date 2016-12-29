Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Webservices.Github

Module Program

    Sub Main()
        'Dim r = Microsoft.VisualBasic.Webservices.Github.API.Search.Users(New UsersQuery With {.term = "xieguigang"})
        'Dim user = Microsoft.VisualBasic.Webservices.Github.API.GetUser("xieguigang")


        Dim net As Network = Network.Load("G:\github-network\xieguigang").NormalizeNetwork

        Call net.Save("G:\github-network\xieguigang")


        WebAPI.Proxy = "http://127.0.0.1:8087/"


        Call BuildNetwork.FromUser("xieguigang", 0, 50).Save(App.HOME & "/xieguigang/")

        Pause()


        'Try
        '    Call BuildNetwork.DownloadAvatar("G:\github-network\gitnet\bin\Debug\xieguigang")
        'Catch ex As Exception
        '    Call ex.PrintException
        'End Try



        'Pause()
    End Sub

    <Extension>
    Public Function NormalizeNetwork(net As Network) As Network
        Dim edgesGroup = From ed As NetworkEdge
                         In net.Edges
                         Select ed,
                             uid = ed.GetNullDirectedGuid(True)
                         Group By uid Into Group

        Dim edges As New List(Of NetworkEdge)

        For Each ed In edgesGroup
            Dim array = ed.Group.Select(Function(x) x.ed).ToArray

            If array.Length = 1 Then
                edges += array(Scan0)
            Else
                edges += New NetworkEdge(array(Scan0)) With {
                    .InteractionType = "Friends"
                }
            End If
        Next

        Dim nodes As New Dictionary(Of Node)(net.Nodes)

        For Each edge In edges
            If Not nodes.ContainsKey(edge.FromNode) Then
                nodes += New Node With {
                    .Identifier = edge.FromNode,
                    .NodeType = "leaf-user"
                }
            End If
            If Not nodes.ContainsKey(edge.ToNode) Then
                nodes += New Node With {
                    .Identifier = edge.ToNode,
                    .NodeType = "leaf-user"
                }
            End If
        Next

        Dim degrees = net.GetDegrees

        For Each node As Node In net.Nodes
            node("Degree") = degrees(node.Identifier)
        Next

        Dim out = New Network With {
            .Edges = edges,
            .Nodes = nodes
        }.RemovesByDegreeQuantile(0.1)

        Return out
    End Function
End Module
