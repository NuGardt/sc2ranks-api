' NuGardt SC2Ranks API Test
' Copyright (C) 2011-2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Imports System.IO
Imports NuGardt.SC2Ranks.UnitTest.GetCharacterTeams
Imports NuGardt.SC2Ranks.UnitTest.GetCharacter
Imports NuGardt.SC2Ranks.UnitTest.GetData
Imports NuGardt.SC2Ranks.UnitTest.GetCharacters
Imports NuGardt.UnitTest
Imports System.Threading
Imports NuGardt.SC2Ranks.UnitTest.SearchCharacterTeams

Namespace SC2Ranks
  Module StartUp
    Public Trace As TraceListener

    Private ReadOnly LockObject As Object = GetType(StartUp).ToString()
    Private TestingComplete As Boolean
    Private TestCases As Queue(Of IUnitTestCase)
    Private CacheStream As Stream

    ''' <summary>
    ''' Start.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Main()
      Dim TraceListener As TraceListener
      Dim LogStream As Stream

      Try
        'try to open file log
        LogStream = New FileStream("output.log", FileMode.Create, FileAccess.Write, FileShare.Read)
        TraceListener = New TraceListener(LogStream, AddressOf TraceRelay)
      Catch iEx As Exception
        'Failed to open log so we don't write anything to file
        TraceListener = New TraceListener(AddressOf TraceRelay)
        Call Console.WriteLine(iEx)
      End Try

      Trace = TraceListener
      Call Diagnostics.Trace.Listeners.Add(TraceListener)
      Diagnostics.Trace.AutoFlush = True

      TestCases = New Queue(Of IUnitTestCase)()
      TestingComplete = False

      Try
        'Try to open cache log
        CacheStream = New FileStream("cache.blob", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read)
      Catch iEx As Exception
        CacheStream = Nothing
        Call Console.WriteLine(iEx)
      End Try

      Call Trace.WriteLine("SC2Ranks Test")
      Call Trace.WriteLine("=============")
      Call Trace.WriteLine("")
      'Write system information for debugging purposes.
      Call Trace.WriteSystemInformation()
      Call Trace.WriteLine("")

      Call TestCases.Enqueue(New GetDataBeginUnitTest) 'Tested 2013-08-03: Ok
      Call TestCases.Enqueue(New GetDataUnitTest) 'Tested 2013-08-03: Ok

      Call TestCases.Enqueue(New GetCharacterBeginUnitTest) 'Tested 2013-08-03: Ok
      Call TestCases.Enqueue(New GetCharacterUnitTest) 'Tested 2013-08-03: Ok

      Call TestCases.Enqueue(New GetCharacterTeamsBeginUnitTest) 'Tested 2013-08-03: Ok
      Call TestCases.Enqueue(New GetCharacterTeamsUnitTest) 'Tested 2013-08-0§: Ok

      Call TestCases.Enqueue(New SearchCharacterTeamsBeginUnitTest) 'Tested 2013-08-03: Ok
      Call TestCases.Enqueue(New SearchCharacterTeamsUnitTest) 'Tested 2013-08-03: Ok

      'Call TestCases.Enqueue(New GetCharactersBeginUnitTest) 'Error 400
      'Call TestCases.Enqueue(New GetCharactersUnitTest) 'Error 400

      Call TestingBegin()

      Call Trace.WriteLine("Waiting on unit tests to complete...")

      Do Until TestingComplete
        Call Thread.Sleep(50)
      Loop

      If (CacheStream IsNot Nothing) Then
        Call CacheStream.Close()
        Call CacheStream.Dispose()
      End If

      Call Trace.Close()
      Call Trace.Dispose()

      'Call Console.ReadKey()
    End Sub

    Public Sub TestingBegin()
      If (Not DoNextTestCase()) Then Call TestingEnd()
    End Sub

    Private Sub TestingEnd()
      TestingComplete = True
    End Sub

    Private Function DoNextTestCase() As Boolean
      Dim TestCase As IUnitTestCase

      If (TestCases.Count > 0) Then
        TestCase = TestCases.Dequeue()

        With TestCase
          Call .Initialize()

          Call Trace.WriteLine(String.Format("Testing: {0}", .Name))

          Dim Wrapper As New TestCaseWrapper(TestCase, TestCaseEndCallback)
          Call .Start(Wrapper.TestCaseEndCallback, Nothing)
        End With

        Return True
      Else
        Return False
      End If
    End Function

#Region "Class TestCaseWrapper"

    Private NotInheritable Class TestCaseWrapper
      Public ReadOnly TestCase As IUnitTestCase
      Private ReadOnly m_TestCaseEndCallback As Action(Of TestCaseWrapper)
      Public Result As IAsyncResult

      Public Sub New(ByVal TestCase As IUnitTestCase,
                     ByVal TestCaseEndCallback As Action(Of TestCaseWrapper))
        Me.TestCase = TestCase
        Me.m_TestCaseEndCallback = TestCaseEndCallback
      End Sub

      Public ReadOnly TestCaseEndCallback As AsyncCallback = AddressOf Me.iTestCaseEndCallback

      Private Sub iTestCaseEndCallback(ByVal Result As IAsyncResult)
        Me.Result = Result
        Call Me.m_TestCaseEndCallback.Invoke(Me)
      End Sub
    End Class

#End Region

#Region "Sub TestCaseEndCallback"
    Private ReadOnly TestCaseEndCallback As Action(Of TestCaseWrapper) = AddressOf iTestCaseEndCallback

    Private Sub iTestCaseEndCallback(ByVal TestCaseWrapper As TestCaseWrapper)
      With TestCaseWrapper.TestCase
        Call .End(TestCaseWrapper.Result)

        SyncLock LockObject
          Call Trace.WriteLine(String.Format("Test: {0}", .Name))
          Call Trace.WriteLine(String.Format("Test Passed: {0}", .Successfull.ToString()))
          If (Not String.IsNullOrEmpty(.Result)) Then Call Trace.WriteLine(String.Format("Result: {0}", .Result))
        End SyncLock

        Call .Dispose()
      End With

      If (Not DoNextTestCase()) Then Call TestingEnd()
    End Sub

#End Region

    ''' <summary>
    '''   Write trace message to console.
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Private Sub TraceRelay(ByVal Message As String)
      Call Console.Write(Message)
    End Sub
  End Module
End Namespace