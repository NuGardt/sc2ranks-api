﻿Imports com.NuGardt.SC2Ranks.API
Imports System.Text
Imports System.IO
Imports System.Globalization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports com.NuGardt.SC2Ranks.API.PlayerInfo
Imports com.NuGardt.SC2Ranks.API.SearchInfo

Namespace SC2Ranks
  Module StartUp
    Public Trace As TraceListener

#Region "Test Switches"
    Const PauseOnMismatchOrError As Boolean = True
    Const OutputJson As Boolean = False
    Const OutputClass As Boolean = False
    Const DoSyncTest As Boolean = True
    Const DoAsyncTest As Boolean = False
#End Region

#Region "Tests"
    ' Tested: 2013-05-12 PASS
    Const TestGetBasePlayerByBattleNetID As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestGetBasePlayerByCharacterCode As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestGetBaseTeamByBattleNetID As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestGetBaseTeamByCharacterCode As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestGetCustomDivision As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestGetTeamByBattleNetID As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestGetTeamByCharacterCode As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestGetBasePlayers As Boolean = True

    ' Tested: 2013-05-12 PASS
    Const TestSearchBaseCharacter As Boolean = True

    ' Tested: 2013-05-09 FAIL: 404 Missing on SC2Ranks side?
    Const TestManageCustomDivision As Boolean = False
#End Region

#Region "Test Parameters"
    Const TestRegion As eRegion = eRegion.EU
    Const TestCharacterName As String = "OomJan"
    Const TestCharacterCode As Integer = 0
    Const TestBattleNetID As Int32 = 1770249
    Const TestCustomDivisionID As Int32 = 7085
    Const TestCustomDivisionPassword As String = "secret"
    Const SomeKey As Object = "Some random key for Async operations or nothing"
#End Region

    Private ReadOnly LockObject As Object = GetType(StartUp).ToString()
    Private RankService As Sc2RanksService = Nothing
    
    ''' <summary>
    '''   Start.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Main()
      Dim PlayerInforDivisionArray() As PlayerInfoDivision = Nothing
      Dim PlayerInfoExtended As PlayerInfoExtended = Nothing
      Dim PlayerInfoBase As PlayerInfoBase = Nothing
      Dim PlayerInfoBaseArray() As PlayerInfoBase = Nothing
      Dim SearchInfoResult As SearchInfoResult = Nothing
      Dim ResponseRaw As String = Nothing
      Dim AsyncResult As IAsyncResult = Nothing
      Dim Ex As Exception

      Dim TraceListener As TraceListener
      Dim LogStream As Stream = Nothing

      Try
        LogStream = New FileStream("output.log", FileMode.Create, FileAccess.Write, FileShare.Read)
      Catch
        '-
      End Try

      TraceListener = New TraceListener(LogStream, AddressOf TraceRelay)

      Trace = TraceListener
      Call Diagnostics.Trace.Listeners.Add(TraceListener)
      Diagnostics.Trace.AutoFlush = True

      Ex = Sc2RanksService.CreateInstance("com.nugardt.sc2ranks.customdivisionprofiler", RankService, True)

      If (Ex Is Nothing) Then
        Call Trace.WriteLine("SC2Ranks Test")
        Call Trace.WriteLine("=============")
        Call Trace.WriteLine("")
        Call Trace.WriteSystemInformation()
        Call Trace.WriteLine("")

        If TestGetBasePlayerByBattleNetID Then
          If DoSyncTest Then
            Ex = RankService.GetBasePlayerByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Result := PlayerInfoBase, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoBase)("GetBasePlayerByBattleNetID (Sync)", Ex, PlayerInfoBase, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBasePlayerByBattleNetID (Async)")
            AsyncResult = RankService.GetBasePlayerByBattleNetIDBegin(Key := "MyTestKey", Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Callback := AddressOf GetBasePlayerByBattleNetIDCallback)
          End If
        End If

        If TestGetBasePlayerByCharacterCode Then
          If DoSyncTest Then
            Ex = RankService.GetBasePlayerByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Result := PlayerInfoBase, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoBase)("GetBasePlayerByCharacterCode (Sync)", Ex, PlayerInfoBase, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBasePlayerByCharacterCode (Async)...")
            AsyncResult = RankService.GetBasePlayerByCharacterCodeBegin(Key := SomeKey, Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Callback := AddressOf GetBasePlayerByCharacterCodeCallback)
          End If
        End If

        If TestGetBaseTeamByBattleNetID Then
          If DoSyncTest Then
            Ex = RankService.GetBaseTeamByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Result := PlayerInfoExtended, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoExtended)("GetBaseTeamByBattleNetID (Sync)", Ex, PlayerInfoExtended, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBaseTeamByBattleNetID (Async)...")
            AsyncResult = RankService.GetBaseTeamByBattleNetIDBegin(Key := SomeKey, Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Callback := AddressOf GetBaseTeamByBattleNetIDCallback)
          End If
        End If

        If TestGetBaseTeamByCharacterCode Then
          If DoSyncTest Then
            Ex = RankService.GetBaseTeamByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Result := PlayerInfoExtended, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoExtended)("GetBaseTeamCharacterInfoByCharacterCode (Sync)", Ex, PlayerInfoExtended, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetBaseTeamCharacterInfoByCharacterCode (Async)")
            AsyncResult = RankService.GetBaseTeamByCharacterCodeBegin(Key := SomeKey, Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Callback := AddressOf GetBaseTeamByCharacterCodeCallback)
          End If
        End If

        If TestGetCustomDivision Then
          If DoSyncTest Then
            Ex = RankService.GetCustomDivision(CustomDivisionID := TestCustomDivisionID, Region := eRegion.All, League := Nothing, Bracket := eBracket._3V3, Result := PlayerInforDivisionArray, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoDivision())("GetCustomDivision (Sync)", Ex, PlayerInforDivisionArray, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetCustomDivision (Async)")
            AsyncResult = RankService.GetCustomDivisionBegin(Key := SomeKey, CustomDivisionID := TestCustomDivisionID, Region := eRegion.All, League := Nothing, Bracket := eBracket._1V1, Callback := AddressOf GetCustomDivisionCallback)
          End If
        End If

        If TestGetTeamByBattleNetID Then
          If DoSyncTest Then
            Ex = RankService.GetTeamByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Bracket := eBracket._1V1, Result := PlayerInfoExtended, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoExtended)("GetTeamByBattleNetID (Sync)", Ex, PlayerInfoExtended, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetTeamInfoByBNetID (Async)")
            AsyncResult = RankService.GetTeamByBattleNetIDBegin(Key := SomeKey, Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Bracket := eBracket._1V1, Callback := AddressOf GetTeamByBattleNetIDCallback)
          End If
        End If

        If TestGetTeamByCharacterCode Then
          If DoSyncTest Then
            Ex = RankService.GetTeamByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Bracket := eBracket._1V1, Result := PlayerInfoExtended, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoExtended)("GetTeamByCharacterCode (Sync)", Ex, PlayerInfoExtended, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling GetTeamInfoByCharacterCode (Async)")
            AsyncResult = RankService.GetTeamByCharacterCodeBegin(Key := SomeKey, Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Bracket := eBracket._1V1, Callback := AddressOf GetTeamByCharacterCodeCallback)
          End If
        End If

        If TestGetBasePlayers Then
          Dim PlayerList As New List(Of PlayerInfoBase)
          Call PlayerList.Add(PlayerInfoBase.CreateByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID))

          If DoSyncTest Then
            Ex = RankService.GetBasePlayers(Players := PlayerList, Bracket := Nothing, Result := PlayerInfoBaseArray, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoBase())("GetBasePlayers (Sync)", Ex, PlayerInfoBaseArray, ResponseRaw)
          End If

          If DoAsyncTest Then
            'Call Trace.WriteLine("Calling MassGetPlayers (Async)")
            'AsyncResult = RankService.GetBasePlayersBegin(Players := PlayerList, Bracket := Nothing, Callback := AddressOf GetBasePlayersCallback)
          End If
        End If

        If TestSearchBaseCharacter Then
          If DoSyncTest Then
            Ex = RankService.SearchBasePlayer(SearchType := eSearchType.Contains, Region := eRegion.EU, CharacterName := TestCharacterName, ResultOffset := Nothing, Result := SearchInfoResult, ResponseRaw := ResponseRaw)
            Call CheckResult(Of SearchInfoResult)("SearchBasePlayer (Sync)", Ex, SearchInfoResult, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling SearchBaseCharacter (Async)")
            AsyncResult = RankService.SearchBasePlayerBegin(Key := SomeKey, SearchType := eSearchType.Contains, Region := eRegion.EU, CharacterName := TestCharacterName, ResultOffset := Nothing, Callback := AddressOf SearchBasePlayerCallback)
          End If
        End If

        If TestManageCustomDivision Then
          Dim PlayerList As New List(Of PlayerInfoBase)
          Call PlayerList.Add(PlayerInfoBase.CreateByBattleNetID(TestRegion, TestCharacterName, TestBattleNetID))

          If DoSyncTest Then
            Ex = RankService.ManageCustomDivision(CustomDivisionID := TestCustomDivisionID, Password := TestCustomDivisionPassword, Action := eCustomDivisionAction.Add, Players := PlayerList, Result := PlayerInforDivisionArray, ResponseRaw := ResponseRaw)
            Call CheckResult(Of PlayerInfoDivision())("ManageCustomDivision (Sync)", Ex, PlayerInforDivisionArray, ResponseRaw)
          End If

          If DoAsyncTest Then
            Call Trace.WriteLine("Calling ManageCustomDivision (Async)")
            AsyncResult = RankService.ManageCustomDivisionBegin(Key := SomeKey, CustomDivisionID := TestCustomDivisionID, Password := TestCustomDivisionPassword, Action := eCustomDivisionAction.Add, Players := PlayerList, Callback := AddressOf ManageCustomDivisionCallback)
          End If
        End If
      Else
        Call Trace.WriteLine(Ex)
      End If

      If DoAsyncTest Then Call Console.ReadKey()

      Call Trace.Close()
      Call Trace.Dispose()
    End Sub

#Region "Function CompareJson"

    Private Function CompareJson(ByVal SB As StringBuilder,
                                 ByVal A As String,
                                 ByVal B As String,
                                 Optional ShowOnlyMismatches As Boolean = True) As Boolean

      If (A IsNot Nothing) AndAlso (B IsNot Nothing) Then
        Dim ItemsA As JContainer = DirectCast(JsonConvert.DeserializeObject(A), JContainer)
        Dim ItemsB As JContainer = DirectCast(JsonConvert.DeserializeObject(B), JContainer)
        Dim Dict As New SortedDictionary(Of String, Object)
        Dim MismatchCount As Int32 = 0
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

    Private Function SanityCheck(Of T)(ByVal SB As StringBuilder,
                                       ByVal ResponseRaw As String,
                                       ByVal Obj As T) As Boolean
      Dim ResponseReSerialized As String

      ResponseReSerialized = JsonConvert.SerializeObject(Obj, Formatting.None)

      Return CompareJson(SB, ResponseRaw, ResponseReSerialized)
    End Function

    Private Sub TraceRelay(ByVal Message As String)
      Call Console.Write(Message)
    End Sub

    Private Sub GetBasePlayerByBattleNetIDCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoBase = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.GetBasePlayerByBattleNetIDEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoBase)("GetBasePlayerByBattleNetIDCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub GetBasePlayerByCharacterCodeCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoBase = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.GetBasePlayerByCharacterCodeEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoBase)("GetBasePlayerByCharacterCodeCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub GetBaseTeamByBattleNetIDCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoExtended = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.GetBaseTeamByBattleNetIDEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoExtended)("GetBaseTeamByBattleNetIDCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub GetBaseTeamByCharacterCodeCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoExtended = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.GetBaseTeamByCharacterCodeEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoExtended)("GetBaseTeamByCharacterCodeCallback", Ex, Response, ResponseRaw)
    End Sub

    Private Sub GetCustomDivisionCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoDivision() = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.GetCustomDivisionEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoDivision())("GetCustomDivisionCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub GetTeamByBattleNetIDCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoExtended = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.GetTeamByBattleNetIDEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoExtended)("GetTeamByBattleNetIDCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub GetTeamByCharacterCodeCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoExtended = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.GetTeamByCharacterCodeEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoExtended)("GetTeamByCharacterCodeCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub GetBasePlayersCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoBase() = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      'Ex = RankService.GetBasePlayersEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoBase())("GetBasePlayersCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub SearchBasePlayerCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As SearchInfoResult = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.SearchBasePlayerEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of SearchInfoResult)("SearchBasePlayerCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

    Private Sub ManageCustomDivisionCallback(ByVal Result As IAsyncResult)
      Dim Ex As Exception
      Dim Response As PlayerInfoDivision() = Nothing
      Dim ResponseRaw As String
      Dim Key As Object = Nothing

      Ex = RankService.ManageCustomDivisionEnd(Result, Key, Response, ResponseRaw)

      Call CheckResult(Of PlayerInfoDivision())("ManageCustomDivisionCallback (Async)", Ex, Response, ResponseRaw)
    End Sub

#Region "Function CheckResult"

    Private Sub CheckResult(Of T As Class)(ByVal Description As String,
                                           ByVal Ex As Exception,
                                           ByVal Response As T,
                                           ByVal ResponseRaw As String)
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

          If TypeOf Response Is IEnumerable Then
            With DirectCast(Response, IEnumerable).GetEnumerator()
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
          Call SB.AppendLine(ResponseRaw)
          Call SB.AppendLine("")
        End If

        If (Not SanityCheck(Of T)(SB, ResponseRaw, Response)) Then
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

#End Region
  End Module
End Namespace