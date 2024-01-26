#If VBA7 Then
    Private Declare PtrSafe Sub fibonacci_init Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\x64\Release\MathLibrary.dll" (ByVal a As LongLong, ByVal b As LongLong)
    Private Declare PtrSafe Function fibonacci_next Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\x64\Release\MathLibrary.dll" () As Boolean
    Private Declare PtrSafe Function fibonacci_current Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\x64\Release\MathLibrary.dll" () As LongLong
    Private Declare PtrSafe Function fibonacci_index Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\x64\Release\MathLibrary.dll" () As Long
#Else
    Private Declare Sub fibonacci_init Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\Release\MathLibrary.dll" (ByVal a As LongLong, ByVal b As LongLong)
    Private Declare Function fibonacci_next Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\Release\MathLibrary.dll" () As Boolean
    Private Declare Function fibonacci_current Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\Release\MathLibrary.dll" () As LongLong
    Private Declare Function fibonacci_index Lib "C:\Users\offsec\source\rt_templates\VBACallCustomDocumentedDLL\third-party\MathLibrary\Release\MathLibrary.dll" () As Long
#End If


Sub CallCustomDocumentedDLL()

    fibonacci_init 1, 1

    Dim i, n As Long
    n = 13
    For i = 0 To n - 1 Step 1
        fibonacci_next
    Next i

    Dim current As LongLong
    current = fibonacci_current
    
    Dim index As LongLong
    index = fibonacci_index
    
    MsgBox "Fibonacci number " & current & " has index " & fibonacci_index & " (if counting from zero)"

End Sub

Sub Document_Open()
    CallCustomDocumentedDLL
End Sub

Sub AutoOpen()
    CallCustomDocumentedDLL
End Sub
