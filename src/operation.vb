
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports Quantum.VM
Imports SMRUCC.Rsharp.Runtime.Interop

<Package("operations")>
<RTypeExport("Qubit", GetType(QubitVM))>
Public Module operations

    ''' <summary>
    ''' allocate a register of n qubits in |0> 
    ''' </summary>
    ''' <param name="n"></param>
    ''' <returns></returns>
    <ExportAPI("Qubit")>
    Public Function Qubit(Optional n As Integer = 1) As QubitVM
        Return New QubitVM(n)
    End Function

    ''' <summary>
    ''' Hadamard operation H to the state, Put a qubit in superposition
    ''' 
    ''' To put a qubit in superposition, R# provides the H, or Hadamard, operation.
    ''' the H operation flips the qubit halfway into a state of equal probabilities 
    ''' of 0 or 1. When measured, a qubit in superposition should return roughly an
    ''' equal number of Zero and One results.
    ''' </summary>
    ''' <param name="q"></param>
    ''' <returns></returns>
    <ExportAPI("H")>
    Public Function H(q As QubitVM) As QubitVM
        Call q.H()
        Return q
    End Function

    ''' <summary>
    ''' Measure the qubit in Z-basis.
    ''' </summary>
    ''' <param name="q"></param>
    ''' <returns></returns>
    <ExportAPI("M")>
    Public Function M(q As QubitVM) As Object
        Return q.M
    End Function

    ''' <summary>
    ''' Reset the qubit before releasing it.
    ''' </summary>
    ''' <param name="q"></param>
    ''' <returns></returns>
    <ExportAPI("X")>
    Public Sub X(q As QubitVM)
        Call q.Dispose()
    End Sub
End Module
