Imports System.Reflection

Public Module Globals

    Public Property Hash1 As String = getHashKey(GetType(App).Assembly)
    Public Property Hash2 As String = getHashKey(GetType(Globals).Assembly)

    Sub New()

    End Sub

    Private Function getHashKey(assm As Assembly) As String
        Dim file As String = assm.Location.GetFullPath
        Dim md5 As String = SecurityString.GetFileMd5(file)

        Return md5.ToLower
    End Function

End Module
