Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Proxy.Protocol
Imports services

Module Program

    Public Function Main() As Integer
        Return GetType(Program).RunCLI(App.CommandLine)
    End Function

    ''' <summary>
    ''' start server side
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    <ExportAPI("/listen")>
    <Usage("/listen [/port <default=232>]")>
    Public Function listen(args As CommandLine) As Integer
        Using server As New ListenServer(port:=args("/port") Or 232)
            Return server.Run
        End Using
    End Function

    <ExportAPI("/get")>
    <Usage("/get /url <url> /proxy <ip:port> [/save <file.html>]")>
    Public Function [get](args As CommandLine) As Integer
        Dim url As String = args("/url")
        Dim proxy As String = args("/proxy")
        Dim save As String = args("/save") Or $"./{url.Split("/"c).Select(Function(a) a.NormalizePathString).JoinBy("/")}.html"

        Return Handlers.GetHttpText(url, proxy) _
            .SaveTo(save) _
            .CLICode
    End Function

End Module
