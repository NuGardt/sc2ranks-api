Imports System.Runtime.InteropServices
Imports com.NuGardt.SC2Ranks.API
Imports System.IO
Imports System.Globalization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports com.NuGardt.SC2Ranks.API.PlayerInfo
Imports com.NuGardt.SC2Ranks.API.SearchInfo
Imports System.Text

Namespace SC2Ranks
  Module StartUp
    Public Trace As TraceListener

    ''' <summary>
    ''' Start.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Main()
      Dim RankService As Sc2RanksService
      Dim BaseArray() As DivisionInfoDivision = Nothing
      Dim Player As PlayerInfoExtended = Nothing
      Dim PlayerBase As PlayerInfoBase = Nothing
      Dim PlayerBaseArray() As PlayerInfoBase = Nothing
      Dim SearchResult As SearchInfoResult = Nothing
      Dim ResponseRaw As String = Nothing
      Dim Result As String = Nothing
      Dim Ex As Exception
      
      'Test Switches
      Const PauseOnMismatch As Boolean = True
      Const OutputJson As Boolean = False
      Const OutputClass As Boolean = False

      ' Tested: 2013-05-09 PASS
      Const TestGetBaseCharacterInfoByBNetID As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestGetBaseCharacterInfoByCharacterCode As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestGetBaseTeamCharacterInfoByBNetID As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestGetBaseTeamCharacterInfoByCharacterCode As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestGetCustomDivision As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestGetTeamInfoByBNetID As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestGetTeamInfoByCharacterCode As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestMassGetPlayers As Boolean = True

      ' Tested: 2013-05-09 PASS
      Const TestSearchBaseCharacter As Boolean = True

      ' Tested: 2013-05-09 FAIL: 404 Missing on SC2Ranks side?
      Const TestManageCustomDivision As Boolean = True

      'Test Parameters
      Const TestRegion As eRegion = eRegion.EU
      Const TestCharacterName As String = "OomJan"
      Const TestCharacterCode As Integer = 0
      Const TestBattleNetID As Int32 = 1770249
      Const TestCustomDivisionID As Int32 = 7085
      Const TestCustomDivisionPassword As String = "secret"

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

        If TestGetBaseCharacterInfoByBNetID Then
          Call Trace.WriteLine("GetBaseCharacterInfoByBNetID")
          Call Trace.WriteLine("============================")
          Ex = RankService.GetBasePlayerByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Result := PlayerBase, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")
              Call Trace.WriteLine(PlayerBase.ToString)
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
              Call Trace.WriteLine("")
            End If

            If Not SanityCheck(Of PlayerInfoBase)(ResponseRaw, PlayerBase, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestGetBaseCharacterInfoByCharacterCode Then
          Call Trace.WriteLine("GetBaseCharacterInfoByCharacterCode")
          Call Trace.WriteLine("===================================")
          Ex = RankService.GetBasePlayerByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Result := PlayerBase, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")
              Call Trace.WriteLine(PlayerBase.ToString)
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
              Call Trace.WriteLine("")
            End If

            If Not SanityCheck(Of PlayerInfoBase)(ResponseRaw, PlayerBase, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestGetBaseTeamCharacterInfoByBNetID Then
          Call Trace.WriteLine("GetBaseTeamCharacterInfoByBNetID")
          Call Trace.WriteLine("================================")

          Ex = RankService.GetBaseTeamByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Result := Player, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")
              Call Trace.WriteLine(Player.ToString)
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
              Call Trace.WriteLine("")
            End If

            If Not SanityCheck(Of PlayerInfoExtended)(ResponseRaw, Player, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestGetBaseTeamCharacterInfoByCharacterCode Then
          Call Trace.WriteLine("GetBaseTeamCharacterInfoByCharacterCode")
          Call Trace.WriteLine("=======================================")

          Ex = RankService.GetBaseTeamByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Result := Player, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")
              Call Trace.WriteLine(Player.ToString)
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
              Call Trace.WriteLine("")
            End If

            If Not SanityCheck(Of PlayerInfoExtended)(ResponseRaw, Player, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If

        End If

        If TestGetCustomDivision Then
          Call Trace.WriteLine("GetCustomDivision")
          Call Trace.WriteLine("=================")

          Ex = RankService.GetCustomDivision(CustomDivisionID := TestCustomDivisionID, Region := eRegion.All, League := Nothing, Bracket := eBracket._3V3, Result := BaseArray, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")

              For Each b As DivisionInfoDivision In BaseArray
                Call Trace.WriteLine(b.ToString)
              Next b
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
              Call Trace.WriteLine("")
            End If

            If Not SanityCheck(Of DivisionInfoDivision())(ResponseRaw, BaseArray, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestGetTeamInfoByBNetID Then
          Call Trace.WriteLine("GetTeamInfoByBNetID")
          Call Trace.WriteLine("===================")

          Ex = RankService.GetTeamByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID, Bracket := eBracket._1V1, Result := Player, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")
              Call Trace.WriteLine(Player.ToString)
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
              Call Trace.WriteLine("")
            End If

            If Not SanityCheck(Of PlayerInfoExtended)(ResponseRaw, Player, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestGetTeamInfoByCharacterCode Then
          Call Trace.WriteLine("GetTeamInfoByCharacterCode")
          Call Trace.WriteLine("==========================")

          Ex = RankService.GetTeamByCharacterCode(Region := TestRegion, CharacterName := TestCharacterName, CharacterCode := TestCharacterCode, Bracket := eBracket._1V1, Result := Player, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")
              Call Trace.WriteLine(Player.ToString)
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
              Call Trace.WriteLine("")
            End If

            If Not SanityCheck(Of PlayerInfoExtended)(ResponseRaw, Player, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestMassGetPlayers Then
          Call Trace.WriteLine("MassGetPlayers")
          Call Trace.WriteLine("==============")

          Dim PlayerList As New List(Of PlayerInfoBase)

          Call PlayerList.Add(PlayerInfoBase.CreateByBattleNetID(Region := TestRegion, CharacterName := TestCharacterName, BattleNetID := TestBattleNetID))

          Ex = RankService.GetBasePlayers(Players := PlayerList, Bracket := Nothing, Result := PlayerBaseArray, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")

              For Each p As PlayerInfoExtended In PlayerBaseArray
                Call Trace.WriteLine(p.ToString)
              Next p
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
            End If

            If Not SanityCheck(Of PlayerInfoBase())(ResponseRaw, PlayerBaseArray, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestSearchBaseCharacter Then
          Call Trace.WriteLine("SearchBaseCharacter")
          Call Trace.WriteLine("===================")

          Ex = RankService.SearchBasePlayer(SearchType := eSearchType.Contains, Region := eRegion.EU, CharacterName := TestCharacterName, ResultOffset := Nothing, Result := SearchResult, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            If OutputClass Then
              Call Trace.WriteLine("Class")
              Call Trace.WriteLine("=====")

              Call Trace.WriteLine(SearchResult.ToString)
            End If

            If OutputJson Then
              Call Trace.WriteLine("JSON Raw")
              Call Trace.WriteLine("========")
              Call Trace.WriteLine(ResponseRaw)
            End If

            If Not SanityCheck(Of SearchInfoResult)(ResponseRaw, SearchResult, Result) Then
              Call Trace.WriteLine(Result)
              Call Trace.WriteLine("Result: FAIL")
              If PauseOnMismatch Then
                Call Console.ReadKey()
                Call Console.Clear()
              End If
            Else
              Call Trace.WriteLine("Result: PASS")
            End If
          End If
        End If

        If TestManageCustomDivision Then
          Call Trace.WriteLine("ManageCustomDivision")
          Call Trace.WriteLine("====================")

          Dim PlayerList As New List(Of PlayerInfoBase)

          Call PlayerList.Add(PlayerInfoBase.CreateByBattleNetID(TestRegion, TestCharacterName, TestBattleNetID))

          Ex = RankService.ManageCustomDivision(CustomDivisionID := TestCustomDivisionID, Password := TestCustomDivisionPassword, Action := eCustomDivisionAction.Add, Players := PlayerList, Result := BaseArray, ResponseRaw := ResponseRaw)

          If (Ex IsNot Nothing) Then
            Call Trace.WriteLine(Ex)
            Call Trace.WriteLine("Result: FAIL")
            If PauseOnMismatch Then
              Call Console.ReadKey()
              Call Console.Clear()
            End If
          Else
            Call Trace.WriteLine("JSON Raw")
            Call Trace.WriteLine("========")
            Call Trace.WriteLine(ResponseRaw)

            'ToDo: SanityCheck, once the API works again
          End If
        End If
      Else
        Call Trace.WriteLine(Ex)
      End If

      Call Trace.Close()
      Call Trace.Dispose()
    End Sub

    Private Function CompareJson(ByVal A As String,
                                 ByVal B As String,
                                 <Out()> ByRef Result As String,
                                 Optional ShowOnlyMismatches As Boolean = True) As Boolean
      Dim ItemsA As JContainer = DirectCast(JsonConvert.DeserializeObject(A), JContainer)
      Dim ItemsB As JContainer = DirectCast(JsonConvert.DeserializeObject(B), JContainer)
      Dim Dict As New SortedDictionary(Of String, Object)
      Dim SB As New StringBuilder
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

        Call SB.AppendLine("Mismatch count: " + MismatchCount.ToString())
      End If

      Result = SB.ToString()

      Return (MismatchCount = 0)
    End Function

    Private Function EnumerateJObject(ByVal Obj As JObject) As String
      Dim dict As New SortedDictionary(Of String, String)
      Dim SB As New stringbuilder

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

    Private Function SanityCheck(Of T)(ByVal ResponseRaw As String,
                                       ByVal Obj As T,
                                       <out()> ByRef Result As String) As Boolean
      Dim ResponseReSerialized As String

      'Dim Setting = New JsonSerializerSettings
      'With Setting
      '  .FloatFormatHandling = FloatFormatHandling.String
      '  .Culture = System.Globalization.CultureInfo.GetCultureInfo("en-US")
      '  .FloatParseHandling = FloatParseHandling.Double
      'End With

      ResponseReSerialized = JsonConvert.SerializeObject(Obj, Formatting.None)

      Return CompareJson(ResponseRaw, ResponseReSerialized, Result)
    End Function

    Private Sub TraceRelay(ByVal Message As String)
      Call Console.Write(Message)
    End Sub
  End Module
End Namespace