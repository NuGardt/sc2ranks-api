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
Imports NuGardt.SC2Ranks.UnitTest.CustomDivisions.CustomDivisionRemove
Imports NuGardt.SC2Ranks.UnitTest.CustomDivisions.CustomDivisionAdd
Imports NuGardt.SC2Ranks.UnitTest.CustomDivisions.GetCustomDivisionCharacterList
Imports NuGardt.SC2Ranks.UnitTest.CustomDivisions.GetCustomDivisionTeamList
Imports NuGardt.SC2Ranks.UnitTest.CustomDivisions.GetCustomDivisions
Imports NuGardt.SC2Ranks.UnitTest.CustomDivisions.GetCustomDivision
Imports NuGardt.SC2Ranks.UnitTest.Divisions.GetDivisionTeamsTop
Imports NuGardt.SC2Ranks.UnitTest.Divisions.GetDivision
Imports NuGardt.SC2Ranks.UnitTest.Divisions.GetDivisionsTop
Imports NuGardt.SC2Ranks.UnitTest.Rankings.GetRankingsTop
Imports NuGardt.SC2Ranks.UnitTest.Clans.GetClanTeamList
Imports NuGardt.SC2Ranks.UnitTest.Clans.GetClanCharacterList
Imports NuGardt.SC2Ranks.UnitTest.Clans.GetClan
Imports NuGardt.SC2Ranks.UnitTest.Teams.GetCharacterTeamList
Imports NuGardt.SC2Ranks.UnitTest.Characters.GetCharacterList
Imports NuGardt.SC2Ranks.UnitTest.Characters.GetCharacterTeamsList
Imports NuGardt.SC2Ranks.UnitTest.Characters.GetCharacter
Imports NuGardt.SC2Ranks.UnitTest.BaseData.GetBaseData
Imports NuGardt.UnitTest
Imports NuGardt.SC2Ranks.UnitTest.Characters.SearchCharacterTeamList
Imports System.Threading

Namespace SC2Ranks
  Module StartUp
    Public Trace As TraceListener

    Private ReadOnly LockObject As Object = GetType(StartUp).ToString()
    Private TestingComplete As Boolean
    Private TestCases As Queue(Of IUnitTestCase)
    Private CacheStream As Stream
    Private TestsDone As Int32
    Private TestsSuccessful As Int32

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

      '-Base Data
      Call TestCases.Enqueue(New GetBaseDataBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetBaseDataUnitTest) 'Tested 2013-08-05: Ok

      '-Characters
      Call TestCases.Enqueue(New GetCharacterBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCharacterUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetCharacterTeamsListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCharacterTeamsListUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New SearchCharacterTeamListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New SearchCharacterTeamListUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetCharacterListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCharacterListUnitTest) 'Tested 2013-08-05: Ok

      '-Teams
      Call TestCases.Enqueue(New GetCharacterTeamListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCharacterTeamListUnitTest) 'Tested 2013-08-05: Ok

      '-Clans
      Call TestCases.Enqueue(New GetClanBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetClanUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetClanCharacterListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetClanCharacterListUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetClanTeamListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetClanTeamListUnitTest) 'Tested 2013-08-05: Ok

      '-Rankings
      Call TestCases.Enqueue(New GetRankingsTopBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetRankingsTopUnitTest) 'Tested 2013-08-05: Ok

      '-Divisions
      Call TestCases.Enqueue(New GetDivisionsTopBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetDivisionsTopUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetDivisionBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetDivisionUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetDivisionTeamsTopBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetDivisionTeamsTopUnitTest) 'Tested 2013-08-05: Ok

      '-Custom Divisions
      Call TestCases.Enqueue(New GetCustomDivisionBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCustomDivisionUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetCustomDivisionsBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCustomDivisionsUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetCustomDivisionTeamListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCustomDivisionTeamListUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New GetCustomDivisionCharacterListBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New GetCustomDivisionCharacterListUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New CustomDivisionAddBeginUnitTest) 'Tested 2013-08-05: Ok
      Call TestCases.Enqueue(New CustomDivisionAddUnitTest) 'Tested 2013-08-05: Ok

      Call TestCases.Enqueue(New CustomDivisionRemoveBeginUnitTest) 'Tested 2013-08-05': Ok
      Call TestCases.Enqueue(New CustomDivisionRemoveUnitTest) 'Tested 2013-08-05: Ok

      Call TestingBegin()

      Call Trace.WriteLine("Waiting on unit tests to complete...")

      Do Until TestingComplete
        Call Thread.Sleep(50)
      Loop

      If (CacheStream IsNot Nothing) Then
        Call CacheStream.Close()
        Call CacheStream.Dispose()
      End If

      Dim SucessRate As Double

      If TestsDone <> 0 Then
        SucessRate = ((TestsSuccessful / TestsDone) * 100)
      Else
        SucessRate = Double.NaN
      End If

      Call Trace.WriteLine(String.Format("{0} out of {1} successful ({2}%).", TestsSuccessful.ToString(), TestsDone.ToString(), SucessRate.ToString()))

      Call Trace.Close()
      Call Trace.Dispose()
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
          TestsDone += 1
          If .Successfull Then TestsSuccessful += 1
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