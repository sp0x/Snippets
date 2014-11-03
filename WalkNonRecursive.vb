 Public Delegate Function Func(Of TT)() As TT
        Public Delegate Function Func(Of TT, TT1)(ByRef a As TT) As TT1
        Public Delegate Function Func(Of TT, TT1, TT2)(ByRef a As TT, ByRef b As TT1) As TT2

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <typeparam name="TT"></typeparam>
        ''' <param name="roots"></param>
        ''' <param name="while">Do while this true</param>
        ''' <param name="fetcher">Fetches subrelatives</param>
        ''' <remarks></remarks>
        Public Shared Function WalkNonRecursive(Of TT)(roots As TT(), findFirstOnly As Boolean, [while] As Func(Of TT, Boolean, Boolean), _
                                                    fetcher As Func(Of TT, IEnumerable(Of TT))) As Boolean
            Dim stack As New Stack(Of TT)
            PushAll(stack, roots)  'seed the stack 
            While stack.Count > 0
                Dim cVal As TT = stack.Pop
                Dim isValid As Boolean = False
                If Not [while](cVal, isValid) Then
                    Continue While
                End If
                If isValid Then
                    If findFirstOnly Then
                        Return True
                    End If
                End If
                PushAll(stack, fetcher(cVal))
            End While
            Return False
        End Function