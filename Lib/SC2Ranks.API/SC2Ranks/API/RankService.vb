Imports System.Runtime.InteropServices
Imports com.NuGardt.SC2Ranks.API.SearchInfo
Imports com.NuGardt.SC2Ranks.API.PlayerInfo
Imports System.Collections.Generic
Imports com.NuGardt.SC2Ranks.Helper
Imports System.Text
Imports System.Runtime.Serialization.Json
Imports System.Net
Imports System.IO

Namespace SC2Ranks.API
''' <summary>
'''   Class containing all API calls to SC2Ranks.
''' </summary>
''' <remarks></remarks>
  Public Class Sc2RanksService
    Private ReadOnly m_AppKey As String
    Private ReadOnly m_ReturnResponseRaw As Boolean
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <param name="AppKey"></param>
    ''' <param name="ReturnResponseRaw"></param>
    ''' <remarks></remarks>
    Private Sub New(ByVal AppKey As String,
                    Optional ByVal ReturnResponseRaw As Boolean = False)
      Me.m_AppKey = AppKey
      Me.m_ReturnResponseRaw = ReturnResponseRaw
    End Sub

#Region "Function CreateInstance"
    
    ''' <summary>
    '''   Create an instance of the SC2Ranks Service.
    ''' </summary>
    ''' <param name="AppKey">Required by SC2Ranks.com.</param>
    ''' <param name="Instance">
    '''   Contains the instance if Ex is <c>Nothing</c>.
    ''' </param>
    ''' <param name="ReturnResponseRaw">
    '''   If <c>True</c> then the JSON response is returned to the calling function otherwise (useful for debugging purposes) <c>False</c>.
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateInstance(ByVal AppKey As String,
                                          <Out()> ByRef Instance As Sc2RanksService,
                                          Optional ByVal ReturnResponseRaw As Boolean = False) As Exception
      Instance = Nothing

      If String.IsNullOrEmpty(AppKey) Then
        Return New ArgumentNullException("AppKey")
      ElseIf AppKey.Length >= 32766 Then
        'Silly check but never know^^
        Return New FormatException("AppKey too long.")
      Else
        Instance = New Sc2RanksService(Uri.EscapeUriString(AppKey), ReturnResponseRaw)
      End If

      Return Nothing
    End Function

#End Region

#Region "Function SearchBasePlayer"
    
    ''' <summary>
    '''   Allows you to perform small searches, useful if you want to hookup an IRC bot or such. Only returns the first 10 names, but you can see the total number of characters and pass an offset if you need more. Search is case-insensitive.
    ''' </summary>
    ''' <param name="SearchType">The type of the search.</param>
    ''' <param name="Region">The region to search in.</param>
    ''' <param name="CharacterName">The fulll or partial name of the character depending on search type.</param>
    ''' <param name="ResultOffset">The offset of the result.</param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SearchBasePlayer(ByVal SearchType As eSearchType,
                                     ByVal Region As eRegion,
                                     ByVal CharacterName As String,
                                     ByVal ResultOffset As Nullable(Of Int32),
                                     <Out()> ByRef Result As SearchInfoResult,
                                     <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing

      If ResultOffset.HasValue Then
        Result = GetDataAndParse(Of SearchInfoResult)(String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}/{3}.json?appKey={4}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, ResultOffset.Value.ToString, Me.m_AppKey), Ex, ResponseRaw)
      Else
        Result = GetDataAndParse(Of SearchInfoResult)(String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}.json?appKey={3}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, Me.m_AppKey), Ex, ResponseRaw)
      End If

      Return Ex
    End Function

    Public Function SearchBasePlayerBegin(ByVal Key As Object,
                                          ByVal SearchType As eSearchType,
                                          ByVal Region As eRegion,
                                          ByVal CharacterName As String,
                                          ByVal ResultOffset As Nullable(Of Int32),
                                          ByVal Callback As AsyncCallback) As IAsyncResult

      If ResultOffset.HasValue Then
        Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}/{3}.json?appKey={4}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, ResultOffset.Value.ToString, Me.m_AppKey), Callback)
      Else
        Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}.json?appKey={3}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, Me.m_AppKey), Callback)
      End If
    End Function

    Public Function SearchBasePlayerEnd(ByVal Result As IAsyncResult,
                                        <Out()> ByRef Key As Object,
                                        <Out()> ByRef Response As SearchInfoResult,
                                        <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of SearchInfoResult)(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetBasePlayerByCharacterCode"
    
    ''' <summary>
    '''   Minimum amount of character data, just gives achievement points, character code and battle.net id info.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="CharacterCode"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete()>
    Public Function GetBasePlayerByCharacterCode(ByVal Region As eRegion,
                                                 ByVal CharacterName As String,
                                                 ByVal CharacterCode As Integer,
                                                 <Out()> ByRef Result As PlayerInfoBase,
                                                 <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing

      Result = GetDataAndParse(Of PlayerInfoBase)(String.Format("http://sc2ranks.com/api/base/char/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Ex, ResponseRaw)

      Return Ex
    End Function

    Public Function GetBasePlayerByCharacterCodeBegin(ByVal Key As Object,
                                                      ByVal Region As eRegion,
                                                      ByVal CharacterName As String,
                                                      ByVal CharacterCode As Integer,
                                                      ByVal Callback As AsyncCallback) As IAsyncResult
      Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/char/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Callback)
    End Function

    Public Function GetBasePlayerByCharacterCodeEnd(ByVal Result As IAsyncResult,
                                                    <Out()> ByRef Key As Object,
                                                    <Out()> ByRef Response As PlayerInfoBase,
                                                    <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of PlayerInfoBase)(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetBasePlayerByBattleNetID"
    
    ''' <summary>
    '''   Minimum amount of character data, just gives achievement points, character code and battle.net id info.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBasePlayerByBattleNetID(ByVal Region As eRegion,
                                               ByVal CharacterName As String,
                                               ByVal BattleNetID As Int32,
                                               <Out()> ByRef Result As PlayerInfoBase,
                                               <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing

      Result = GetDataAndParse(Of PlayerInfoBase)(String.Format("http://sc2ranks.com/api/base/char/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Ex, ResponseRaw)

      Return Ex
    End Function

    Public Function GetBasePlayerByBattleNetIDBegin(ByVal Key As Object,
                                                    ByVal Region As eRegion,
                                                    ByVal CharacterName As String,
                                                    ByVal BattleNetID As Int32,
                                                    ByVal Callback As AsyncCallback) As IAsyncResult
      Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/char/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Callback)
    End Function

    Public Function GetBasePlayerByBattleNetIDEnd(ByVal Result As IAsyncResult,
                                                  <Out()> ByRef Key As Object,
                                                  <Out()> ByRef Response As PlayerInfoBase,
                                                  <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of PlayerInfoBase)(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetBaseTeamByCharacterCode"
    
    ''' <summary>
    '''   Includes base character data, as well as base data on all of the players teams.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="CharacterCode"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete()>
    Public Function GetBaseTeamByCharacterCode(ByVal Region As eRegion,
                                               ByVal CharacterName As String,
                                               ByVal CharacterCode As Integer,
                                               <Out()> ByRef Result As PlayerInfoExtended,
                                               <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing

      Result = GetDataAndParse(Of PlayerInfoExtended)(String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Ex, ResponseRaw)

      Return Ex
    End Function

    Public Function GetBaseTeamByCharacterCodeBegin(ByVal Key As Object,
                                                    ByVal Region As eRegion,
                                                    ByVal CharacterName As String,
                                                    ByVal CharacterCode As Integer,
                                                    ByVal Callback As AsyncCallback) As IAsyncResult
      Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Callback)
    End Function

    Public Function GetBaseTeamByCharacterCodeEnd(ByVal Result As IAsyncResult,
                                                  <Out()> ByRef Key As Object,
                                                  <Out()> ByRef Response As PlayerInfoExtended,
                                                  <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of PlayerInfoExtended)(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetBaseTeamByBattleNetID"
    
    ''' <summary>
    '''   Includes base character data, as well as base data on all of the players teams.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseTeamByBattleNetID(ByVal Region As eRegion,
                                             ByVal CharacterName As String,
                                             ByVal BattleNetID As Int32,
                                             <Out()> ByRef Result As PlayerInfoExtended,
                                             <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing

      Result = GetDataAndParse(Of PlayerInfoExtended)(String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Ex, ResponseRaw)

      Return Ex
    End Function

    Public Function GetBaseTeamByBattleNetIDBegin(ByVal Key As Object,
                                                  ByVal Region As eRegion,
                                                  ByVal CharacterName As String,
                                                  ByVal BattleNetID As Int32,
                                                  ByVal Callback As AsyncCallback) As IAsyncResult
      Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Callback)
    End Function

    Public Function GetBaseTeamByBattleNetIDEnd(ByVal Result As IAsyncResult,
                                                <Out()> ByRef Key As Object,
                                                <Out()> ByRef Response As PlayerInfoExtended,
                                                <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of PlayerInfoExtended)(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetTeamByCharacterCode"
    
    ''' <summary>
    '''   Includes base character data, and extended team information for the passed bracket.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="CharacterCode"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete()>
    Public Function GetTeamByCharacterCode(ByVal Region As eRegion,
                                           ByVal CharacterName As String,
                                           ByVal CharacterCode As Integer,
                                           ByVal Bracket As eBracket,
                                           <Out()> ByRef Result As PlayerInfoExtended,
                                           <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Result = GetDataAndParse(Of PlayerInfoExtended)(String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}${2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Ex, ResponseRaw)

      Return Ex
    End Function

    Public Function GetTeamByCharacterCodeBegin(ByVal Key As Object,
                                                ByVal Region As eRegion,
                                                ByVal CharacterName As String,
                                                ByVal CharacterCode As Integer,
                                                ByVal Bracket As eBracket,
                                                ByVal Callback As AsyncCallback) As IAsyncResult
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}${2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Callback)
    End Function

    Public Function GetTeamByCharacterCodeEnd(ByVal Result As IAsyncResult,
                                              <Out()> ByRef Key As Object,
                                              <Out()> ByRef Response As PlayerInfoExtended,
                                              <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Return GetDataAndParseEnd(Of PlayerInfoExtended)(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetTeamByBattleNetID"
    
    ''' <summary>
    '''   Includes base character data, and extended team information for the passed bracket.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTeamByBattleNetID(ByVal Region As eRegion,
                                         ByVal CharacterName As String,
                                         ByVal BattleNetID As Int32,
                                         ByVal Bracket As eBracket,
                                         <Out()> ByRef Result As PlayerInfoExtended,
                                         <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Result = GetDataAndParse(Of PlayerInfoExtended)(String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}!{2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Ex, ResponseRaw)

      Return Ex
    End Function

    Public Function GetTeamByBattleNetIDBegin(ByVal Key As Object,
                                              ByVal Region As eRegion,
                                              ByVal CharacterName As String,
                                              ByVal BattleNetID As Int32,
                                              ByVal Bracket As eBracket,
                                              ByVal Callback As AsyncCallback) As IAsyncResult
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)
      Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}!{2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Callback)
    End Function

    Public Function GetTeamByBattleNetIDEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As PlayerInfoExtended,
                                            <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of PlayerInfoExtended)(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetCustomDivision"
    
    ''' <summary>
    '''   Allows you to get everyone in a custom division.
    ''' </summary>
    ''' <param name="CustomDivisionID"></param>
    ''' <param name="Region"></param>
    ''' <param name="League"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivision(ByVal CustomDivisionID As Integer,
                                      ByVal Region As Nullable(Of eRegion),
                                      ByVal League As Nullable(Of eLeague),
                                      ByVal Bracket As eBracket,
                                      <Out()> ByRef Result As PlayerInfoDivision(),
                                      <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Dim Ex As Exception = Nothing
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Result = GetDataAndParse(Of PlayerInfoDivision())(String.Format("http://sc2ranks.com/api/clist/{0}/{1}/{2}/{3}/{4}.json?appKey={5}", CustomDivisionID, If(Region.HasValue, Enums.RegionBuffer.GetValue(Region.Value), "all"), If(League.HasValue, Enums.LeaguesBuffer.GetValue(League.Value), "all"), Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Ex, ResponseRaw)

      Return Ex
    End Function

    Public Function GetCustomDivisionBegin(ByVal Key As Object,
                                           ByVal CustomDivisionID As Integer,
                                           ByVal Region As Nullable(Of eRegion),
                                           ByVal League As Nullable(Of eLeague),
                                           ByVal Bracket As eBracket,
                                           ByVal Callback As AsyncCallback) As IAsyncResult
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/clist/{0}/{1}/{2}/{3}/{4}.json?appKey={5}", CustomDivisionID, If(Region.HasValue, Enums.RegionBuffer.GetValue(Region.Value), "all"), If(League.HasValue, Enums.LeaguesBuffer.GetValue(League.Value), "all"), Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Callback)
    End Function

    Public Function GetCustomDivisionEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As PlayerInfoDivision(),
                                         <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of PlayerInfoDivision())(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function ManageCustomDivision"
    
    ''' <summary>
    '''   Allows you to add or remove characters to a custom division, to reduce abuse you are required to use the custom divisions password to manage.
    ''' </summary>
    ''' <param name="CustomDivisionID"></param>
    ''' <param name="Password"></param>
    ''' <param name="Action"></param>
    ''' <param name="Players"></param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks>Broken</remarks>
    Public Function ManageCustomDivision(ByVal CustomDivisionID As Integer,
                                         ByVal Password As String,
                                         ByVal Action As eCustomDivisionAction,
                                         ByVal Players As IEnumerable(Of PlayerInfoBase),
                                         <Out()> ByRef Result As PlayerInfoDivision(),
                                         <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Result = Nothing
      ResponseRaw = Nothing
      Dim Ex As Exception = Nothing

      If Players Is Nothing Then
        Ex = New NullReferenceException("Players")
      ElseIf (Players.Count = 0) Then
        Ex = New Exception("No Players defined.")
      Else
        Dim Chars As New StringBuilder

        For Each p In Players
          If Chars.Length <> 0 Then Call Chars.Append(",")
          With p
            Call Chars.AppendFormat("{0}-{1}!{2}", p.Region, p.CharacterName, p.BattleNetID)
          End With
        Next p

        Result = GetDataAndParse(Of PlayerInfoDivision())(String.Format("http://sc2ranks.com/api/custom/{0}/{1}/{2}/{3}.json?appKey={4}", CustomDivisionID, Password, Enums.CustomDivisionActionBuffer.GetValue(Action), Chars, Me.m_AppKey), Ex, ResponseRaw)
      End If

      Return Ex
    End Function

    Public Function ManageCustomDivisionBegin(ByVal Key As Object,
                                              ByVal CustomDivisionID As Integer,
                                              ByVal Password As String,
                                              ByVal Action As eCustomDivisionAction,
                                              ByVal Players As IEnumerable(Of PlayerInfoBase),
                                              ByVal Callback As AsyncCallback) As IAsyncResult
      If Players Is Nothing Then
        Return Callback.BeginInvoke(Nothing, Nothing, New NullReferenceException("Players"))
      ElseIf (Players.Count = 0) Then
        Return Callback.BeginInvoke(Nothing, Nothing, New Exception("No Players defined."))
      Else
        Dim Chars As New StringBuilder

        For Each p In Players
          If Chars.Length <> 0 Then Call Chars.Append(",")
          With p
            Call Chars.AppendFormat("{0}-{1}!{2}", p.Region, p.CharacterName, p.BattleNetID)
          End With
        Next p

        Return GetDataAndParseBegin(Key, String.Format("http://sc2ranks.com/api/custom/{0}/{1}/{2}/{3}.json?appKey={4}", CustomDivisionID, Password, Enums.CustomDivisionActionBuffer.GetValue(Action), Chars, Me.m_AppKey), Callback)
      End If
    End Function

    Public Function ManageCustomDivisionEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As PlayerInfoDivision(),
                                            <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      ResponseRaw = Nothing
      Return GetDataAndParseEnd(Of PlayerInfoDivision())(Result, Key, Response, ResponseRaw)
    End Function

#End Region

#Region "Function GetBasePlayers"
    
    ''' <summary>
    '''   Same as pulling just character information, except you can pull 100 characters at once. The returns are the same, except you get an array of characters rather than a hash.
    ''' </summary>
    ''' <param name="Players"></param>
    ''' <param name="Bracket">
    '''   Optional. Can be <c>Nothing</c>.
    ''' </param>
    ''' <param name="Result"></param>
    ''' <param name="ResponseRaw">Returns the JSON response if enabled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBasePlayers(ByVal Players As IEnumerable(Of PlayerInfoBase),
                                   ByVal Bracket As Nullable(Of eBracket),
                                   <Out()> ByRef Result As PlayerInfoBase(),
                                   <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
      Result = Nothing
      ResponseRaw = Nothing
      Dim Ex As Exception

      If Players Is Nothing Then
        Ex = New NullReferenceException("Players")
      ElseIf (Players.Count = 0) Then
        Ex = New Exception("No players defined.")
      ElseIf (Players.Count > 100) Then
        Ex = New ArgumentException("Too many players requested. Maximum of 100 players allowed at a time.")
      Else
        Dim Bytes As Byte()
        Dim Serializer As DataContractJsonSerializer
        Dim Web As WebClient
        Dim Stream As Stream
        Dim Request As String = Nothing

        Try
          If Bracket.HasValue Then
            Ex = GetSearchRequestBody(Players, True, Bracket.Value, Request)
            If (Ex IsNot Nothing) Then Return Ex

            Bytes = Encoding.UTF8.GetBytes(Request)

            Web = New WebClient()
            With Web
              Bytes = .UploadData(String.Format("http://sc2ranks.com/api/mass/base/teams/?appKey={0}", Me.m_AppKey), "POST", Bytes)
              Call .Dispose()
            End With
          Else
            Ex = GetSearchRequestBody(Players, False, Nothing, Request)
            If (Ex IsNot Nothing) Then Return Ex

            Bytes = Encoding.UTF8.GetBytes(Request)

            Web = New WebClient()
            With Web
              Bytes = .UploadData(String.Format("http://sc2ranks.com/api/mass/base/char/?appKey={0}", Me.m_AppKey), "POST", Bytes)
              Call .Dispose()
            End With
          End If

          Serializer = New DataContractJsonSerializer(GetType(PlayerInfoBase()))
          Stream = New MemoryStream(Bytes)
          Result = DirectCast(Serializer.ReadObject(Stream), PlayerInfoBase())
          With Stream
            Call .Close()
            Call .Dispose()
          End With

          If m_ReturnResponseRaw Then ResponseRaw = Encoding.UTF8.GetString(Bytes)
        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return Ex
    End Function

    'Public Function GetBasePlayersBegin(ByVal Players As IEnumerable(Of PlayerInfoBase),
    '                                    ByVal Bracket As Nullable(Of eBracket),
    '                                    ByVal Callback As AsyncCallback) As IAsyncResult
    'ToDo: GetBasePlayersBegin
    'End Function

    'Public Function GetBasePlayersEnd(ByVal Result As IAsyncResult,
    '                                  <Out()> ByRef Key As Object,
    '                                  <Out()> ByRef Response As PlayerInfoBase(),
    '                                  <Out()> Optional ByRef ResponseRaw As String = Nothing) As Exception
    '  ResponseRaw = Nothing
    '  Return GetDataAndParseEnd(Of PlayerInfoBase())(Result, Key, Response, ResponseRaw)
    'End Function

#End Region

    Private Shared Function GetSearchRequestBody(ByVal Players As IEnumerable(Of PlayerInfoBase),
                                                 ByVal UseTeam As Boolean,
                                                 ByVal Bracket As eBracket,
                                                 <Out()> ByRef Result As String) As Exception
      Dim Ex As Exception = Nothing
      Dim SB As New StringBuilder

      If UseTeam Then
        Dim IsRandom As Boolean = IsRandomBracket(Bracket)

        If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

        If IsRandom Then
          Call SB.AppendFormat("team[bracket]={0}&team[is_random]=1", Bracket)
        Else
          Call SB.AppendFormat("team[bracket]={0}&team[is_random]=0", Bracket)
        End If
      End If

      Dim dMax As Int32 = Players.Count - 1
      For i As Integer = 0 To dMax
        With Players(i)
          If SB.Length <> 0 Then Call SB.Append("&")

          Call SB.AppendFormat("characters[{0}][region]={1}&characters[{0}][name]={2}&characters[{0}]", i, Enums.RegionBuffer.GetValue(.Region), .CharacterName)

          If .CharacterCode.HasValue Then
            Call SB.AppendFormat("[code]={0}", .CharacterCode.Value.ToString())
          Else
            Call SB.AppendFormat("[bnet_id]={0}", .BattleNetID)
          End If
        End With
      Next i

      Result = SB.ToString()

      Return Ex
    End Function
    
    ''' <summary>
    '''   Returns <c>True</c> if the bracket is random or <c>False</c> for solo or fixed team.
    ''' </summary>
    ''' <param name="Bracket"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsRandomBracket(ByVal Bracket As eBracket) As Boolean
      If (Bracket And eBracket.Random) = eBracket.Random Then
        Return True
      Else
        Return False
      End If
    End Function

    Protected Function GetDataAndParse(Of T As Class)(ByVal URL As String,
                                                      <Out()> ByRef Ex As Exception,
                                                      <Out()> ByRef ResponseRaw As String) As T
      Ex = Nothing
      ResponseRaw = Nothing
      Dim Result As T = Nothing
      Dim Serializer As DataContractJsonSerializer
      Dim ResponseStream As Stream
      Dim Stream As Stream = Nothing

      Try
        Try
          Dim Request As WebRequest
          Dim Response As WebResponse
          Request = HttpWebRequest.Create(URL)
          Response = Request.GetResponse()

          ResponseStream = Response.GetResponseStream()

          'Create serializer
          Serializer = New DataContractJsonSerializer(GetType(T))

          If Me.m_ReturnResponseRaw Then
            'Copy stream
            Stream = New MemoryStream()
            Call ResponseStream.CopyTo(Stream)
            Stream.Position = 0

            'Close stream
            Call ResponseStream.Close()
            Call ResponseStream.Dispose()

            'Close response
            Call Response.Close()

            'Replace Stream with MemoryStream so we can seek (read it more than once)
            ResponseStream = Stream
          End If

          'Deserialize
          Result = DirectCast(Serializer.ReadObject(ResponseStream), T)

          If Me.m_ReturnResponseRaw Then
            Stream.Position = 0
            Dim sr As New StreamReader(Stream)

            ResponseRaw = sr.ReadToEnd
          Else
            'Close stream
            Call ResponseStream.Close()
            Call ResponseStream.Dispose()

            'Close response
            Call Response.Close()
          End If
        Catch iEx As Exception
          Ex = iEx
        End Try
      Catch iEx As Exception
        Ex = iEx
      End Try

      Return Result
    End Function

#Region "Class AsyncStateWithKey"

    Private NotInheritable Class AsyncStateWithKey
      Public ReadOnly Key As Object
      Public ReadOnly State As Object

      Public Sub New(ByVal Key As Object,
                     ByVal State As Object)
        Me.Key = Key
        Me.State = State
      End Sub
    End Class

#End Region

    Public Function GetDataAndParseBegin(ByVal Key As Object,
                                         ByVal URL As String,
                                         ByVal Callback As AsyncCallback) As IAsyncResult
      Dim Result As IAsyncResult
      Dim Request As WebRequest

      Try
        Request = HttpWebRequest.Create(URL)
        Result = Request.BeginGetResponse(Callback, New AsyncStateWithKey(Key, Request))
      Catch iEx As Exception
        Result = Callback.BeginInvoke(Nothing, Nothing, iEx)
      End Try

      Return Result
    End Function

    Public Function GetDataAndParseEnd(Of T As Class)(ByVal Result As IAsyncResult,
                                                      <Out()> ByRef Key As Object,
                                                      <Out()> ByRef Response As T,
                                                      <Out()> ByRef ResponseRaw As String) As Exception
      Key = Nothing
      Response = Nothing
      ResponseRaw = Nothing
      Dim Ex As Exception = Nothing
      Dim State As AsyncStateWithKey
      Dim Request As WebRequest
      Dim ResponseStream As Stream
      Dim Stream As Stream = Nothing
      Dim Serializer As DataContractJsonSerializer

      If (Result Is Nothing) Then
        Ex = New ArgumentNullException("Result")
      Else
        State = TryCast(Result.AsyncState, AsyncStateWithKey)
        Key = State.Key

        If (State Is Nothing) Then
          Ex = New Exception("Invalid AsyncState.")
        Else
          Request = TryCast(State.State, WebRequest)

          If (Request Is Nothing) Then
            Ex = New Exception("WebRequest expected.")
          Else
            Try
              Dim WebResponse As WebResponse = Request.EndGetResponse(Result)
              ResponseStream = WebResponse.GetResponseStream()

              'Create serializer
              Serializer = New DataContractJsonSerializer(GetType(T))

              If Me.m_ReturnResponseRaw Then
                'Copy stream
                Stream = New MemoryStream()
                Call ResponseStream.CopyTo(Stream)
                Stream.Position = 0

                'Close stream
                Call ResponseStream.Close()
                Call ResponseStream.Dispose()

                'Close response
                Call WebResponse.Close()

                'Replace Stream with MemoryStream so we can seek (read it more than once)
                ResponseStream = Stream
              End If

              'Deserialize
              Response = DirectCast(Serializer.ReadObject(ResponseStream), T)

              If Me.m_ReturnResponseRaw Then
                Stream.Position = 0
                Dim sr As New StreamReader(Stream)

                ResponseRaw = sr.ReadToEnd
              Else
                'Close stream
                Call ResponseStream.Close()
                Call ResponseStream.Dispose()

                'Close response
                Call WebResponse.Close()
              End If
            Catch iEx As Exception
              Ex = iEx
            End Try
          End If
        End If
      End If

      Return Ex
    End Function
  End Class
End Namespace