
Imports System.Runtime.CompilerServices
Imports randf = Microsoft.VisualBasic.Math.RandomExtensions

Namespace VM

    ''' <summary>
    ''' ## The qubit in quantum computing
    ''' 
    ''' While a bit, or binary digit, can have a value either 0 Or 1, 
    ''' a qubit can have a value that Is either 0, 1 Or a quantum superposition of
    ''' 0 And 1.
    ''' 
    ''' The state of a single qubit can be described by a two-dimensional column 
    ''' vector of unit norm, that is, the magnitude squared of its entries must 
    ''' sum to 1. 
    ''' 
    ''' This vector, called the quantum state vector, holds all the information needed 
    ''' to describe the one-qubit quantum system just as a single bit holds all of
    ''' the information needed to describe the state of a binary variable.
    ''' </summary>
    Public Class Qubit

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' These two quantum states are taken to correspond to the two states of a 
        ''' classical bit, namely 0 And 1.The standard convention Is to choose:
        ''' 
        ''' + 0 = [1 0]
        ''' + 1 = [0 1]
        ''' 
        ''' although the opposite choice could equally well be taken. Thus, out of the 
        ''' infinite number of possible single-qubit quantum state vectors, only two 
        ''' correspond to states of classical bits; all other quantum states do not.
        ''' </remarks>
        Public ReadOnly Property state As QubitStates
            Get
                Dim h0 = bit(Me.a)
                Dim h1 = bit(Me.b)

                If h0 AndAlso Not h1 Then
                    Return QubitStates.One
                ElseIf h1 AndAlso Not h0 Then
                    Return QubitStates.Zero
                Else
                    Return QubitStates.NA
                End If
            End Get
        End Property

        ' When a qubit given by the quantum state vector [αβ] Is measured, the outcome 0 Is
        ' obtained with probability |α|2 And the outcome 1 With probability |β|2. On outcome
        ' 0, the qubit's new state is [10]; on outcome 1 its state Is [01]. Note that these
        ' probabilities sum up to 1 because of the normalization condition |α|2+|β|2=1.

        Dim a As Double
        Dim b As Double

        Sub New(h As Double)
            Me.a = h
            Me.b = 1 - a
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Private Shared Function bit(h0 As Double) As Boolean
            Return randf.NextDouble < h0
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function ToString() As String
            Return state.Description
        End Function

    End Class
End Namespace