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
Imports com.NuGardt.SC2Ranks.API
Imports System.IO
Imports System.Text
Imports com.NuGardt.SC2Ranks.API.Messages
Imports System.Globalization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Threading

Namespace SC2Ranks
  Module StartUp
    Public Trace As TraceListener

#Region "Test Switches"
    
    ''' <summary>
    '''   Causes the program to pause on error or data mismatch
    ''' </summary>
    ''' <remarks></remarks>
    Const PauseOnMismatchOrError As Boolean = True
    
    ''' <summary>
    '''   Verbose. Output JSON response.
    ''' </summary>
    ''' <remarks></remarks>
    Const OutputJson As Boolean = False
    
    ''' <summary>
    '''   Shows contents of parsed data.
    ''' </summary>
    ''' <remarks></remarks>
    Const OutputClass As Boolean = False
    
    ''' <summary>
    '''   Carries out request synchronously.
    ''' </summary>
    ''' <remarks></remarks>
    Const DoSyncTest As Boolean = True
    
    ''' <summary>
    '''   Carries out request asynchronously
    ''' </summary>
    ''' <remarks></remarks>
    Const DoAsyncTest As Boolean = False
    
    ''' <summary>
    '''   Test methods that are not recommended to use anymore.
    ''' </summary>
    ''' <remarks></remarks>
    Const DoObsoleteMethods As Boolean = True

#End Region

#Region "Tests"
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    Const TestGetBasePlayerByBattleNetID As Boolean = True
    
    ''' <summary>
    '''   Tested: 2013-05-12 PASS
    ''' </summary>
    ''' <remarks></remarks>
    Const TestGetBasePlayerByCharacterCode As Boolean = True
    
    ''' <summary>
    '''   Tested: 2013-05-12 PASS
    ''' </summary>
    ''' <remarks></remarks>
    Const TestGetBaseTeamByBattleNetID As Boolean = True
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    Const TestGetBaseTeamByCharacterCode As Boolean = True
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    Const TestGetCustomDivision As Boolean = True
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    Const TestGetTeamByBattleNetID As Boolean = True
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    Const TestGetTeamByCharacterCode As Boolean = True
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    Const TestGetBasePlayers As Boolean = True
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    Const TestSearchBaseCharacter As Boolean = True
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-09 FAIL: 404 Missing on SC2Ranks side?</remarks>
    Const TestManageCustomDivision As Boolean = False
    
    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-13 </remarks>
    Const TestGetBonusPools As Boolean = True

#End Region

#Region "Test Parameters"
    
    ''' <summary>
    '''   Region of player.
    ''' </summary>
    ''' <remarks></remarks>
    Const TestRegion As eRegion = eRegion.EU
    
    ''' <summary>
    '''   Character name of player
    ''' </summary>
    ''' <remarks></remarks>
    Const TestCharacterName As String = "OomJan"
    
    ''' <summary>
    '''   Character code. Should not be used anymore.
    ''' </summary>
    ''' <remarks></remarks>
    Const TestCharacterCode As Integer = 0
    
    ''' <summary>
    '''   Battle.net identifier
    ''' </summary>
    ''' <remarks></remarks>
    Const TestBattleNetID As Int32 = 1770249
    
    ''' <summary>
    '''   SC2Ranks custom division identifier
    ''' </summary>
    ''' <remarks></remarks>
    Const TestCustomDivisionID As Int32 = 7085
    
    ''' <summary>
    '''   SC2Ranks custom division password
    ''' </summary>
    ''' <remarks></remarks>
    Const TestCustomDivisionPassword As String = "secret"
    
    ''' <summary>
    '''   My key for tracking asynchronous calls. Use for your own purpose.
    ''' </summary>
    ''' <remarks></remarks>
    Const MyKey As Object = "Some random key for Async operations or nothing"

#End Region

    Private ReadOnly LockObject As Object = GetType(StartUp).ToString()
    Private AsyncCallsBusy As Int64
    Private RankService As Sc2RanksService = Nothing
    Private CacheStream As Stream
    
    ''' <summary>
    '''   Start.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Main()
      Dim PlayerInforDivisionArray As Sc2RanksResult(Of Division()) = Nothing
      Dim PlayerInfoExtended As Sc2RanksResult(Of PlayerExtended) = Nothing
      Dim PlayerInfoBase As Sc2RanksResult(Of PlayerBase) = Nothing
      Dim PlayerInfoBaseArray As Sc2RanksResult(Of PlayerBase()) = Nothing
      Dim SearchInfoResult As Sc2RanksResult(Of SearchResult) = Nothing
      Dim BonusPoolResult As Sc2RanksResult(Of BonusPool) = Nothing
      Dim AsyncResult As IAsyncResult = Nothing
      Dim Ex As Exception

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

      Try
        'try to open file log
        CacheStream = New FileStream("cache.blob", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read)
      Catch iEx As Exception
        CacheStream = Nothing
        Call Console.WriteLine(iEx)
      End Try

      'Create instance of SC2Ranks service
      Ex = Sc2RanksService.CreateInstance("com.nugardt.sc2ranks.customdivisionprofiler", CacheStream, RankService)

      If (Ex Is Nothing) Then
        Call Trace.WriteLine("SC2Ranks Test")
        Call Trace.WriteLine("=============")
        Call Trace.WriteLine("")
        'Write system information for debugging purposes.
        Call Trace.WriteSystemInformation()
        Call Trace.WriteLine("")

        If TestGetBasePlayerByBattleNetID Then
          If DoSyncTest Then
            Ex = RankService.GetBasePlayerByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Result := PlayerInfoBase)
            Call CheckResult(Of PlayerBase)("GetBasePlayerByBattleNetID (Sync)", Ex, PlayerInfoBase)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBasePlayerByBattleNetID (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetBasePlayerByBattleNetIDBegin(Key := "MyTestKey", Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Callback := AddressOf GetBasePlayerByBattleNetIDCallback)
          End If
        End If

        If TestGetBasePlayerByCharacterCode AndAlso DoObsoleteMethods Then
          If DoSyncTest Then
            Ex = RankService.GetBasePlayerByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Result := PlayerInfoBase)
            Call CheckResult(Of PlayerBase)("GetBasePlayerByCharacterCode (Sync)", Ex, PlayerInfoBase)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBasePlayerByCharacterCode (Async)...")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetBasePlayerByCharacterCodeBegin(Key := MyKey, Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Callback := AddressOf GetBasePlayerByCharacterCodeCallback)
          End If
        End If

        If TestGetBaseTeamByBattleNetID Then
          If DoSyncTest Then
            Ex = RankService.GetBaseTeamByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Result := PlayerInfoExtended)
            Call CheckResult(Of PlayerExtended)("GetBaseTeamByBattleNetID (Sync)", Ex, PlayerInfoExtended)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBaseTeamByBattleNetID (Async)...")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetBaseTeamByBattleNetIDBegin(Key := MyKey, Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Callback := AddressOf GetBaseTeamByBattleNetIDCallback)
          End If
        End If

        If TestGetBaseTeamByCharacterCode AndAlso DoObsoleteMethods Then
          If DoSyncTest Then
            Ex = RankService.GetBaseTeamByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Result := PlayerInfoExtended)
            Call CheckResult(Of PlayerExtended)("GetBaseTeamCharacterInfoByCharacterCode (Sync)", Ex, PlayerInfoExtended)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBaseTeamCharacterInfoByCharacterCode (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetBaseTeamByCharacterCodeBegin(Key := MyKey, Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Callback := AddressOf GetBaseTeamByCharacterCodeCallback)
          End If
        End If

        If TestGetCustomDivision Then
          If DoSyncTest Then
            Ex = RankService.GetCustomDivision(CustomDivisionID := TestCustomDivisionID, Region := eRegion.All, League := Nothing, Bracket := eBracket._3V3, Result := PlayerInforDivisionArray)
            Call CheckResult(Of Division())("GetCustomDivision (Sync)", Ex, PlayerInforDivisionArray)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetCustomDivision (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetCustomDivisionBegin(Key := MyKey, CustomDivisionID := TestCustomDivisionID, Region := eRegion.All, League := Nothing, Bracket := eBracket._1V1, Callback := AddressOf GetCustomDivisionCallback)
          End If
        End If

        If TestGetTeamByBattleNetID Then
          If DoSyncTest Then
            Ex = RankService.GetTeamByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Bracket := eBracket._1V1, Result := PlayerInfoExtended)
            Call CheckResult(Of PlayerExtended)("GetTeamByBattleNetID (Sync)", Ex, PlayerInfoExtended)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetTeamInfoByBNetID (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetTeamByBattleNetIDBegin(Key := MyKey, Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Bracket := eBracket._1V1, Callback := AddressOf GetTeamByBattleNetIDCallback)
          End If
        End If

        If TestGetTeamByCharacterCode AndAlso DoObsoleteMethods Then
          If DoSyncTest Then
            Ex = RankService.GetTeamByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Bracket := eBracket._1V1, Result := PlayerInfoExtended)
            Call CheckResult(Of PlayerExtended)("GetTeamByCharacterCode (Sync)", Ex, PlayerInfoExtended)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetTeamInfoByCharacterCode (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetTeamByCharacterCodeBegin(Key := MyKey, Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Bracket := eBracket._1V1, Callback := AddressOf GetTeamByCharacterCodeCallback)
          End If
        End If

        If TestGetBasePlayers Then
          Dim PlayerList As New List(Of PlayerBase)
          Call PlayerList.Add(PlayerBase.CreateByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID))

          If DoSyncTest Then
            Ex = RankService.GetBasePlayers(Players := PlayerList, Bracket := Nothing, Result := PlayerInfoBaseArray)
            Call CheckResult(Of PlayerBase())("GetBasePlayers (Sync)", Ex, PlayerInfoBaseArray)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling MassGetPlayers (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetBasePlayersBegin(Key := MyKey, Players := PlayerList, Bracket := Nothing, Callback := AddressOf GetBasePlayersCallback)
          End If
        End If

        If TestSearchBaseCharacter Then
          If DoSyncTest Then
            Ex = RankService.SearchBasePlayer(SearchType := eSearchType.Contains, Region := eRegion.EU, CharacterName := TestCharacterName, ResultOffset := Nothing, Result := SearchInfoResult)
            Call CheckResult(Of SearchResult)("SearchBasePlayer (Sync)", Ex, SearchInfoResult)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling SearchBaseCharacter (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.SearchBasePlayerBegin(Key := MyKey, SearchType := eSearchType.Contains, Region := eRegion.EU, CharacterName := TestCharacterName, ResultOffset := Nothing, Callback := AddressOf SearchBasePlayerCallback)
          End If
        End If

        If TestManageCustomDivision Then
          Dim PlayerList As New List(Of PlayerBase)
          Call PlayerList.Add(PlayerBase.CreateByBattleNetID(TestRegion, TestCharacterName, TestBattleNetID))

          If DoSyncTest Then
            Ex = RankService.ManageCustomDivision(CustomDivisionID := TestCustomDivisionID, Password := TestCustomDivisionPassword, Action := eCustomDivisionAction.Add, Players := PlayerList, Result := PlayerInforDivisionArray)
            Call CheckResult(Of Division())("ManageCustomDivision (Sync)", Ex, PlayerInforDivisionArray)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling ManageCustomDivision (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.ManageCustomDivisionBegin(Key := MyKey, CustomDivisionID := TestCustomDivisionID, Password := TestCustomDivisionPassword, Action := eCustomDivisionAction.Add, Players := PlayerList, Callback := AddressOf ManageCustomDivisionCallback)
          End If
        End If

        If TestGetBonusPools Then
          If DoSyncTest Then
            Ex = RankService.GetBonusPools(Result := BonusPoolResult)
            Call CheckResult(Of BonusPool)("GetBonusPools (Sync)", Ex, BonusPoolResult)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBonusPools (Async)")
            Call Interlocked.Increment(AsyncCallsBusy)
            AsyncResult = RankService.GetBonusPoolsBegin(Key := MyKey, Callback := AddressOf GetBonusPoolCallback)
          End If
        End If
      Else
        Call Trace.WriteLine(Ex)
      End If

      If DoAsyncTest Then
        'Wait for all callback operation to complete before ending the program
        Trace.WriteLine("Waiting on callbacks to complete...")
        Dim CallsBusy As Int64 = - 1

        Do Until CallsBusy = 0
          CallsBusy = Interlocked.Read(AsyncCallsBusy)

          Call Thread.Sleep(50)
        Loop
      End If

      If (RankService IsNot Nothing) Then Call RankService.Dispose()

      If (CacheStream IsNot Nothing) Then
        Call CacheStream.Close()
        Call CacheStream.Dispose()
      End If

      Call Trace.Close()
      Call Trace.Dispose()
    End Sub

#Region "Callbacks"

    Private Sub GetBasePlayerByBattleNetIDCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of PlayerBase) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetBasePlayerByBattleNetIDEnd(Result, Key, Response)

      Call CheckResult(Of PlayerBase)("GetBasePlayerByBattleNetIDCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetBasePlayerByCharacterCodeCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of PlayerBase) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetBasePlayerByCharacterCodeEnd(Result, Key, Response)

      Call CheckResult(Of PlayerBase)("GetBasePlayerByCharacterCodeCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetBaseTeamByBattleNetIDCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of PlayerExtended) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetBaseTeamByBattleNetIDEnd(Result, Key, Response)

      Call CheckResult(Of PlayerExtended)("GetBaseTeamByBattleNetIDCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetBaseTeamByCharacterCodeCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of PlayerExtended) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetBaseTeamByCharacterCodeEnd(Result, Key, Response)

      Call CheckResult(Of PlayerExtended)("GetBaseTeamByCharacterCodeCallback", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetCustomDivisionCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of Division()) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetCustomDivisionEnd(Result, Key, Response)

      Call CheckResult(Of Division())("GetCustomDivisionCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetTeamByBattleNetIDCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of PlayerExtended) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetTeamByBattleNetIDEnd(Result, Key, Response)

      Call CheckResult(Of PlayerExtended)("GetTeamByBattleNetIDCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetTeamByCharacterCodeCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of PlayerExtended) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetTeamByCharacterCodeEnd(Result, Key, Response)

      Call CheckResult(Of PlayerExtended)("GetTeamByCharacterCodeCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetBasePlayersCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of PlayerBase()) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetBasePlayersEnd(Result, Key, Response)

      Call CheckResult(Of PlayerBase())("GetBasePlayersCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub SearchBasePlayerCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of SearchResult) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.SearchBasePlayerEnd(Result, Key, Response)

      Call CheckResult(Of SearchResult)("SearchBasePlayerCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub ManageCustomDivisionCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of Division()) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.ManageCustomDivisionEnd(Result, Key, Response)

      Call CheckResult(Of Division())("ManageCustomDivisionCallback (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private Sub GetBonusPoolCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As Sc2RanksResult(Of BonusPool) = Nothing
      Dim Key As Object = Nothing

      Ex = RankService.GetBonusPoolsEnd(Result, Key, Response)

      Call CheckResult(Of BonusPool)("GetBonusPools (Async)", Ex, Response)

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

#End Region

#Region "Function CheckResult"
    
    ''' <summary>
    '''   Checks the response and parsed object for accuracy.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="Description"></param>
    ''' <param name="Ex"></param>
    ''' <param name="Response"></param>
    ''' <remarks></remarks>
    Private Sub CheckResult(Of T As Class)(ByVal Description As String,
                                           ByVal Ex As Exception,
                                           ByVal Response As Sc2RanksResult(Of T))
      Dim Fail As Boolean
      Dim SB As New StringBuilder

      Call SB.AppendLine(Description)
      Call SB.AppendLine(New String("="c, Description.Length))

      If (Ex IsNot Nothing) Then
        Call SB.AppendLine(Ex.ToString())
        Call SB.AppendLine("Result: FAIL")
        Fail = True
      Else
        If OutputClass Then
          Call SB.AppendLine("Class")
          Call SB.AppendLine("=====")

          If TypeOf Response.Result Is IEnumerable Then
            With DirectCast(Response.Result, IEnumerable).GetEnumerator()
              Call .Reset()

              Do While .MoveNext()
                Call SB.AppendLine(.Current.ToString)
              Loop
            End With
          Else
            Call SB.AppendLine(Response.ToString)
          End If
        End If

        If OutputJson Then
          Call SB.AppendLine("JSON Raw")
          Call SB.AppendLine("========")
          Call SB.AppendLine(Response.ResponseRaw)
          Call SB.AppendLine("")
        End If

        If (Not SanityCheck(Of T)(SB, Response.ResponseRaw, Response.Result)) Then
          Call SB.AppendLine("Result: FAIL")
          Fail = True
        Else
          Call SB.AppendLine("Result: PASS")
        End If
      End If

      'Make sure we write our result in the correct order
      SyncLock LockObject
        Call Trace.WriteLine(SB.ToString())
      End SyncLock

      If PauseOnMismatchOrError AndAlso Fail Then
        Call Console.ReadKey()
        Call Console.Clear()
      End If
    End Sub
    
    ''' <summary>
    '''   Compare the JSON response to the parsed data.
    ''' </summary>
    ''' <typeparam name="T">Type of class.</typeparam>
    ''' <param name="SB"></param>
    ''' <param name="ResponseRaw">JSON response.</param>
    ''' <param name="Obj">Parsed object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SanityCheck(Of T)(ByVal SB As StringBuilder,
                                       ByVal ResponseRaw As String,
                                       ByVal Obj As T) As Boolean
      Dim ResponseReSerialized As String

      ResponseReSerialized = JsonConvert.SerializeObject(Obj, Formatting.None)

      Return CompareJson(SB, ResponseRaw, ResponseReSerialized)
    End Function

#Region "Function CompareJson"
    
    ''' <summary>
    '''   Compare 2 JSON strings.
    ''' </summary>
    ''' <param name="SB">Write results to StringBuilder.</param>
    ''' <param name="A">JSON string A.</param>
    ''' <param name="B">JSON string B.</param>
    ''' <param name="ShowOnlyMismatches">
    '''   If <c>True</c> then only mismatches are show, otherwise matches and mismatches,
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CompareJson(ByVal SB As StringBuilder,
                                 ByVal A As String,
                                 ByVal B As String,
                                 Optional ShowOnlyMismatches As Boolean = True) As Boolean

      If (A IsNot Nothing) AndAlso (B IsNot Nothing) Then
        Dim MismatchCount As Int32 = 0

        Try
          Dim ItemsA As JContainer = DirectCast(JsonConvert.DeserializeObject(A), JContainer)
          Dim ItemsB As JContainer = DirectCast(JsonConvert.DeserializeObject(B), JContainer)
          Dim Dict As New SortedDictionary(Of String, Object)
          Dim Key As String

          If ItemsA.GetType() <> ItemsB.GetType() Then
            Call SB.AppendLine("A and B are not comparable as they are not of the same type.")
          Else
            If TypeOf ItemsA Is JArray Then
              Dim aA As JArray = DirectCast(ItemsA, JArray)
              Dim aB As JArray = DirectCast(ItemsB, JArray)

              If aA.Count <> aB.Count Then
                Call SB.AppendLine("A and B are not of the same array size. Mismatch.")
              Else
                Dim dMax As Int32 = aA.Count - 1
                For i As Int32 = 0 To dMax
                  Call EnumerateJObject(i.ToString(), Dict, DirectCast(aA.Item(i), JObject), DirectCast(aB.Item(i), JObject))
                Next i
              End If
            Else
              Call EnumerateJObject("", Dict, DirectCast(ItemsA, JObject), DirectCast(ItemsB, JObject))
            End If

            With Dict.GetEnumerator()
              Do While .MoveNext()
                Key = .Current.Key

                If TypeOf .Current.Value Is JObject Then
                  With DirectCast(.Current.Value, JObject)
                    Call SB.AppendLine(String.Format("{0}: {1} <<< Mismatch", Key, .ToString()))
                    MismatchCount += 1
                  End With
                Else
                  With DirectCast(.Current.Value, CompareAandB)
                    If .IsEqual Then
                      If (Not ShowOnlyMismatches) Then Call SB.AppendLine(String.Format("{0}: {1} = {2}", Key, .A, .B))
                    Else
                      Call SB.AppendLine(String.Format("{0}: {1} <> {2} <<< Mismatch", Key, .A, .B))
                      MismatchCount += 1
                    End If
                  End With
                End If
              Loop

              Call .Dispose()
            End With

            If (MismatchCount <> 0) Then Call SB.AppendLine("Mismatch count: " + MismatchCount.ToString())
          End If
        Catch iEx As Exception
          MismatchCount += 1
          Call SB.AppendLine(iEx.ToString())
        End Try

        Return (MismatchCount = 0)
      Else
        Return False
      End If
    End Function

    Private Function EnumerateJObject(ByVal Obj As JObject) As String
      Dim dict As New SortedDictionary(Of String, String)
      Dim SB As New StringBuilder

      For Each o In Obj
        If TypeOf o.Value Is JObject Then
          Call dict.Add(o.Key, EnumerateJObject(DirectCast(o.Value, JObject)))
        Else
          Call dict.Add(o.Key, o.Value.ToString())
        End If
      Next o

      With dict.GetEnumerator()
        Do While .MoveNext()
          Call SB.AppendLine(.Current.Key + ": " + .Current.Value.ToString())
        Loop

        Call .Dispose()
      End With

      Return SB.ToString()
    End Function
    
    ''' <summary>
    '''   Class for storing results of the compare.
    ''' </summary>
    ''' <remarks></remarks>
      Private NotInheritable Class CompareAandB
      Public ReadOnly A As Object
      Public B As Object

      Public Sub New(ByVal A As Object,
                     ByVal B As Object)
        Me.A = A
        Me.B = B
      End Sub

      Public Function IsEqual() As Boolean
        If A IsNot Nothing Then
          'A = Something

          If B IsNot Nothing Then
            'B = Something

            If (TypeOf A Is JValue) AndAlso (TypeOf B Is JValue) Then
              With DirectCast(B, JValue)
                If (.Type = JTokenType.Float) Then
                  ' Doubles are formatted differently in ResponseRaw so I adjust here
                  Return String.Equals(A.ToString(), .ToString("0.00", CultureInfo.InvariantCulture))
                Else
                  Return A.Equals(B)
                End If
              End With
            Else
              Return A.Equals(B)
            End If
          Else
            'B = Nothing
            Return False
          End If
        ElseIf B IsNot Nothing Then
          'A = Nothing, B = Something
          Return False
        Else
          'A = Nothing, B = Nothing
          Return True
        End If
      End Function
    End Class

    Private Sub EnumerateJObject(ByVal KeyPrefix As String,
                                 ByVal Dict As IDictionary(Of String, Object),
                                 ByVal A As JObject,
                                 ByVal B As JObject)
      Dim tObj As Object = Nothing

      For Each o In A
        If Not Dict.ContainsKey(KeyPrefix + o.Key) Then
          If TypeOf o.Value Is JObject Then
            Call Dict.Add(KeyPrefix + o.Key, DirectCast(o.Value, JObject))
          ElseIf TypeOf o.Value Is JArray Then
            Call Dict.Add(KeyPrefix + o.Key, DirectCast(o.Value, JArray))
          Else
            Call Dict.Add(KeyPrefix + o.Key, New CompareAandB(o.Value, Nothing))
          End If
        End If
      Next o

      For Each o In B
        If TypeOf o.Value Is JObject Then
          If Dict.ContainsKey(KeyPrefix + o.Key) Then
            Call Dict.TryGetValue(KeyPrefix + o.Key, tObj)
            Call Dict.Remove(KeyPrefix + o.Key)

            Call EnumerateJObject(KeyPrefix + o.Key, Dict, DirectCast(tObj, JObject), DirectCast(o.Value, JObject))
          Else
            Call Dict.Add(KeyPrefix + o.Key, EnumerateJObject(DirectCast(o.Value, JObject)))
          End If
        ElseIf TypeOf o.Value Is JArray Then
          If Dict.ContainsKey(KeyPrefix + o.Key) Then
            Call Dict.TryGetValue(KeyPrefix + o.Key, tObj)
            Call Dict.Remove(KeyPrefix + o.Key)

            Dim jA As JArray = DirectCast(tObj, JArray)
            Dim jB As JArray = DirectCast(o.Value, JArray)

            If jA.Count = jB.Count Then
              Dim dMax As Int32 = jA.Count - 1
              For i As Int32 = 0 To dMax
                Call EnumerateJObject(KeyPrefix + o.Key + i.ToString(), Dict, DirectCast(jA.Item(i), JObject), DirectCast(jB.Item(i), JObject))
              Next i
            Else
              Call Dict.Add(KeyPrefix + o.Key, "<<< Mismatch!!!")
            End If
          Else
            Call Dict.Add(KeyPrefix + o.Key, EnumerateJObject(DirectCast(o.Value, JObject)))
          End If
        Else
          If Dict.ContainsKey(KeyPrefix + o.Key) Then
            Call Dict.TryGetValue(KeyPrefix + o.Key, tObj)
            Call Dict.Remove(KeyPrefix + o.Key)

            With DirectCast(tObj, CompareAandB)
              .B = o.Value
            End With

            Call Dict.Add(KeyPrefix + o.Key, tObj)
          Else
            Call Dict.Add(KeyPrefix + o.Key, New CompareAandB(Nothing, o.Value))
          End If
        End If
      Next o
    End Sub

#End Region

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