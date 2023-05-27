﻿Imports randf = Microsoft.VisualBasic.Math.RandomExtensions

''' <summary>
''' 
''' </summary>
Public Class QubitVM : Implements IDisposable

    Dim qubits As Qubit()
    Private disposedValue As Boolean

    Sub New()
        Call Me.New(size:=1)
    End Sub

    Sub New(size As Integer)
        qubits = New Qubit(size - 1) {}
    End Sub

    Public Sub H()

    End Sub

    Public Function M() As QubitStates()
        Return (From q As Qubit In qubits Select q.state).ToArray
    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
                Erase qubits
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class

Public Class Qubit

    Public ReadOnly Property state As QubitStates
        Get
            If randf.NextDouble < h Then
                Return QubitStates.One
            Else
                Return QubitStates.Zero
            End If
        End Get
    End Property

    Dim h As Double

End Class

Public Enum QubitStates
    Zero
    One
End Enum