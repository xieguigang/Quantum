
Imports randf = Microsoft.VisualBasic.Math.RandomExtensions

Namespace VM

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

        Sub New(h As Double)
            Me.h = h
        End Sub

        Public Overrides Function ToString() As String
            Return state.Description
        End Function

    End Class

    Public Enum QubitStates
        Zero
        One
    End Enum

End Namespace