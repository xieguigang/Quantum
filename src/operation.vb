
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.Rsharp.Runtime.Interop

<Package("operations")>
<RTypeExport("Qubit", GetType(QubitVM))>
Public Module operations

    <ExportAPI("Qubit")>
    Public Function Qubit() As QubitVM
        Return New QubitVM
    End Function

    ''' <summary>
    ''' Hadamard operation H to the state
    ''' </summary>
    ''' <param name="q"></param>
    ''' <returns></returns>
    <ExportAPI("H")>
    Public Function H(q As QubitVM)

    End Function

    ''' <summary>
    ''' Measure the qubit in Z-basis.
    ''' </summary>
    ''' <param name="q"></param>
    ''' <returns></returns>
    <ExportAPI("M")>
    Public Function M(q As QubitVM)

    End Function

    ''' <summary>
    ''' Reset the qubit before releasing it.
    ''' </summary>
    ''' <param name="q"></param>
    ''' <returns></returns>
    <ExportAPI("X")>
    Public Function X(q As QubitVM)

    End Function
End Module
