Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports services

Module Program

    Public Function Main() As Integer
        Return GetType(Program).RunCLI(App.CommandLine)
    End Function

    <ExportAPI("/listen")>
    <Usage("/listen [/port <default=2588>]")>
    Public Function listen(args As CommandLine) As Integer
        Using server As New ListenServer(port:=args("/port") Or 2588)
            Return server.Run
        End Using
    End Function
End Module
