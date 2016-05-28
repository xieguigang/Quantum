Imports Microsoft.VisualBasic.DataVisualization.Network.FileStream

''' <summary>
''' API for build a network model for d3js or cytoscape
''' </summary>
Public Module BuildNetwork

    ''' <summary>
    ''' 从一个用户开始构建网络
    ''' </summary>
    ''' <param name="username"></param>
    ''' <returns></returns>
    Public Function FromUser(username As String) As Network

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
