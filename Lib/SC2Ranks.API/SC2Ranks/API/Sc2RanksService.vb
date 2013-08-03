' NuGardt SC2Ranks API
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
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports NuGardt.SC2Ranks.API.Result.Element
Imports NuGardt.SC2Ranks.Helper
Imports NuGardt.SC2Ranks.API.Cache
Imports NuGardt.SC2Ranks.API.Result
Imports System.Threading
Imports System.Text

Namespace SC2Ranks.API
  ''' <summary>
  ''' Class containing all API calls to SC2Ranks.
  ''' </summary>
  ''' <remarks></remarks>
  Public Class Sc2RanksService
    Implements IDisposable

    Private Const BaseUrlFormat As String = "http://api.sc2ranks.com/v2/{0}?api_key={1}"
    Private Const HeaderCreditsLeft As String = "X-Credits-Left"
    Private Const HeaderCreditsUsed As String = "X-Credits-Used"

    Private ReadOnly UseRequestCache As Boolean
    Private ReadOnly Cache As CacheRequest
    Private ReadOnly CacheConfig As ICacheConfig
    Private ReadOnly CacheStream As Stream
    Private ReadOnly m_ApiKey As String

    ''' <summary>
    ''' Construct.
    ''' </summary>
    ''' <param name="ApiKey"></param>
    ''' <remarks></remarks>
    Private Sub New(ByVal ApiKey As String,
                    ByVal Cache As CacheRequest,
                    ByVal CacheConfig As ICacheConfig,
                    ByVal CacheStream As Stream)
      Me.m_ApiKey = ApiKey

      If (Cache IsNot Nothing) Then
        Me.UseRequestCache = True
        Me.Cache = Cache
        Me.CacheConfig = CacheConfig
        Me.CacheStream = CacheStream
      Else
        Me.UseRequestCache = False
        Me.Cache = Nothing
        Me.CacheStream = Nothing
      End If

      If (Me.CacheConfig Is Nothing) Then Me.CacheConfig = New CacheConfig
    End Sub

#Region "Function CreateInstance"

    ''' <summary>
    ''' Create an instance of the SC2Ranks Service.
    ''' </summary>
    ''' <param name="AppKey">Required by SC2Ranks.com.</param>
    ''' <param name="Instance">
    ''' Contains the instance if Ex is <c>Nothing</c>.
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateInstance(ByVal AppKey As String,
                                          ByVal CacheStream As Stream,
                                          ByVal CacheConfig As ICacheConfig,
                                          <Out()> ByRef Instance As Sc2RanksService,
                                          Optional ByVal IgnoreFaultCacheStream As Boolean = True) As Exception
      Instance = Nothing
      Dim Ex As Exception = Nothing

      If String.IsNullOrEmpty(AppKey) Then
        Ex = New ArgumentNullException("AppKey")
      ElseIf (AppKey.Length > 32766) Then
        'Silly check but never know^^
        Ex = New FormatException("AppKey too long.")
      Else
        Dim Cache As CacheRequest

        If (CacheStream IsNot Nothing) Then
          Cache = New CacheRequest()
          Ex = Cache.Read(CacheStream)

          If (Ex IsNot Nothing) AndAlso IgnoreFaultCacheStream Then Ex = Nothing
        Else
          Cache = Nothing
        End If

        If (Ex Is Nothing) Then Instance = New Sc2RanksService(Uri.EscapeUriString(AppKey), Cache, CacheConfig, CacheStream)
      End If

      Return Ex
    End Function

#End Region

#Region "Function GetData"

    Public Function GetData(<Out()> ByRef Result As Sc2RanksDataResult,
                            Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of Sc2RanksDataResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetDataBegin(ByVal Key As Object,
                                 ByVal Callback As AsyncCallback,
                                 Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetDataEnd(ByVal Result As IAsyncResult,
                               <Out()> ByRef Key As Object,
                               <Out()> ByRef Response As Sc2RanksDataResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksDataResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCharacter"

    Public Function GetCharacter(ByVal Region As eSc2RanksRegion,
                                 ByVal BattleNetID As Int32,
                                 <Out()> ByRef Result As Sc2RanksCharacterResult,
                                 Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of Sc2RanksCharacterResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, String.Format("characters/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCharacterBegin(ByVal Key As Object,
                                      ByVal Region As eSc2RanksRegion,
                                      ByVal BattleNetID As Int32,
                                      ByVal Callback As AsyncCallback,
                                      Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, String.Format("characters/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharacterEnd(ByVal Result As IAsyncResult,
                                    <Out()> ByRef Key As Object,
                                    <Out()> ByRef Response As Sc2RanksCharacterResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCharacterResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCharacters"

    Public Function GetCharacters(ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                  <Out()> ByRef Result As Sc2RanksCharactersResult,
                                  Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (PostData.Length > 0) Then Call PostData.Append("&")

            Call PostData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call PostData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Result = QueryAndParse(Of Sc2RanksCharactersResult, Sc2RanksCharacterResult, IList(Of Sc2RanksCharacterResult))(eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCharactersBegin(ByVal Key As Object,
                                       ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                       ByVal Callback As AsyncCallback,
                                       Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (PostData.Length > 0) Then Call PostData.Append("&")

            Call PostData.AppendFormat("characters[{0}][region]={1}", (d + 1).ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call PostData.AppendFormat("&characters[{0}][bnet_id]={1}", (d + 1).ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Return QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharactersEnd(ByVal Result As IAsyncResult,
                                     <Out()> ByRef Key As Object,
                                     <Out()> ByRef Response As Sc2RanksCharactersResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCharactersResult, Sc2RanksCharacterResult, IList(Of Sc2RanksCharacterResult))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCharacterTeams"

    Public Function GetCharacterTeams(ByVal Region As eSc2RanksRegion,
                                      ByVal BattleNetID As Int32,
                                      ByVal Expansion As eSc2RanksExpansion,
                                      ByVal Bracket As eSc2RanksBracket,
                                      ByVal League As eSc2RanksLeague,
                                      <Out()> ByRef Result As Sc2RanksCharacterTeamsResult,
                                      Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                      Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("expansion={0}&bracket={1}&league={2}", Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = QueryAndParse(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCharacterTeamsBegin(ByVal Key As Object,
                                           ByVal Region As eSc2RanksRegion,
                                           ByVal BattleNetID As Int32,
                                           ByVal Expansion As eSc2RanksExpansion,
                                           ByVal Bracket As eSc2RanksBracket,
                                           ByVal League As eSc2RanksLeague,
                                           ByVal Callback As AsyncCallback,
                                           Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                           Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("expansion={0}&bracket={1}&league={2}", Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharacterTeamsEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksCharacterTeamsResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function SearchCharacterTeams"

    Public Function SearchCharacterTeams(ByVal Name As String,
                                         ByVal Match As eSc2RanksMatchType,
                                         ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         ByVal Page As Int32,
                                         ByVal Limit As Int32,
                                         <Out()> ByRef Result As Sc2RanksCharacterTeamsResult,
                                         Optional Race As Nullable(Of eSc2RanksRace) = Nothing,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = QueryAndParse(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function SearchCharacterTeamsBegin(ByVal Key As Object,
                                              ByVal Name As String,
                                              ByVal Match As eSc2RanksMatchType,
                                              ByVal RankRegion As eSc2RanksRankRegion,
                                              ByVal Expansion As eSc2RanksExpansion,
                                              ByVal Bracket As eSc2RanksBracket,
                                              ByVal League As eSc2RanksLeague,
                                              ByVal Page As Int32,
                                              ByVal Limit As Int32,
                                              ByVal Callback As AsyncCallback,
                                              Optional Race As Nullable(Of eSc2RanksRace) = Nothing,
                                              Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function SearchCharacterTeamsEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksCharacterTeamsResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivision"

    Public Function GetCustomDivision(ByVal DivisionID As String,
                                      <Out()> ByRef Result As Sc2RanksCustomDivisionResult,
                                      Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of Sc2RanksCustomDivisionResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCustomDivisionBegin(ByVal Key As Object,
                                           ByVal DivisionID As String,
                                           ByVal Callback As AsyncCallback,
                                           Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksCustomDivisionResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCustomDivisionResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivisions"

    Public Function GetCustomDivisions(<Out()> ByRef Result As Sc2RanksCustomDivisionsResult,
                                       Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of Sc2RanksCustomDivisionsResult, Sc2RanksCustomDivisionResult, IList(Of Sc2RanksCustomDivisionResult))(eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCustomDivisionsBegin(ByVal Key As Object,
                                            ByVal Callback As AsyncCallback,
                                            Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionsEnd(ByVal Result As IAsyncResult,
                                          <Out()> ByRef Key As Object,
                                          <Out()> ByRef Response As Sc2RanksCustomDivisionsResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCustomDivisionsResult, Sc2RanksCustomDivisionResult, IList(Of Sc2RanksCustomDivisionResult))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivisionTeams"

    Public Function GetCustomDivisionTeams(ByVal DivisionID As String,
                                           ByVal RankRegion As eSc2RanksRankRegion,
                                           ByVal Expansion As eSc2RanksExpansion,
                                           ByVal Bracket As eSc2RanksBracket,
                                           ByVal League As eSc2RanksLeague,
                                           <Out()> ByRef Result As Sc2RanksCustomDivisionTeamsResult,
                                           Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                           Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = QueryAndParse(Of Sc2RanksCustomDivisionTeamsResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCustomDivisionTeamsBegin(ByVal Key As Object,
                                                ByVal DivisionID As String,
                                                ByVal RankRegion As eSc2RanksRankRegion,
                                                ByVal Expansion As eSc2RanksExpansion,
                                                ByVal Bracket As eSc2RanksBracket,
                                                ByVal League As eSc2RanksLeague,
                                                ByVal Callback As AsyncCallback,
                                                Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                                Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionTeamsEnd(ByVal Result As IAsyncResult,
                                              <Out()> ByRef Key As Object,
                                              <Out()> ByRef Response As Sc2RanksCustomDivisionTeamsResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCustomDivisionTeamsResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivisionCharacters"

    Public Function GetCustomDivisionCharacters(ByVal DivisionID As String,
                                                ByVal Region As eSc2RanksRegion,
                                                <Out()> ByRef Result As Sc2RanksCustomDivisionCharactersResult,
                                                Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("region={0}", Enums.RegionBuffer.GetValue(Region))

      Result = QueryAndParse(Of Sc2RanksCustomDivisionCharactersResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCustomDivisionCharactersBegin(ByVal Key As Object,
                                                     ByVal DivisionID As String,
                                                     ByVal Region As eSc2RanksRegion,
                                                     ByVal Callback As AsyncCallback,
                                                     Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("region={0}", Enums.RegionBuffer.GetValue(Region))

      Return QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionCharactersEnd(ByVal Result As IAsyncResult,
                                                   <Out()> ByRef Key As Object,
                                                   <Out()> ByRef Response As Sc2RanksCustomDivisionCharactersResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCustomDivisionCharactersResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function CustomDivisionAdd"

    Public Function CustomDivisionAdd(ByVal DivisionID As String,
                                      ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                      <Out()> ByRef Result As Sc2RanksCustomDivisionManageResult,
                                      Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (PostData.Length > 0) Then Call PostData.Append("&")

            Call PostData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call PostData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Result = QueryAndParse(Of Sc2RanksCustomDivisionManageResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function CustomDivisionAddBegin(ByVal Key As Object,
                                           ByVal DivisionID As String,
                                           ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                           ByVal Callback As AsyncCallback,
                                           Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (PostData.Length > 0) Then Call PostData.Append("&")

            Call PostData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call PostData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Return QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function CustomDivisionAddEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksCustomDivisionManageResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCustomDivisionManageResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function CustomDivisionRemove"

    Public Function CustomDivisionRemove(ByVal DivisionID As String,
                                         ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                         <Out()> ByRef Result As Sc2RanksCustomDivisionManageResult,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (PostData.Length > 0) Then Call PostData.Append("&")

            Call PostData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call PostData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Result = QueryAndParse(Of Sc2RanksCustomDivisionManageResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function CustomDivisionRemoveBegin(ByVal Key As Object,
                                              ByVal DivisionID As String,
                                              ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                              ByVal Callback As AsyncCallback,
                                              Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (PostData.Length > 0) Then Call PostData.Append("&")

            Call PostData.AppendFormat("characters[{0}][region]={1}", (d + 1).ToString(), Enums.RegionBuffer.GetValue(eSc2RanksRegion.Global))
            Call PostData.AppendFormat("&characters[{0}][bnet_id]={1}", (d + 1).ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Return QueryAndParseBegin(Key, eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function CustomDivisionRemoveEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksCustomDivisionManageResult) As Exception
      Return QueryAndParseEnd(Of Sc2RanksCustomDivisionManageResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(Result, Key, Response)
    End Function

#End Region

    ' ''' <summary>
    ' ''' Returns <c>True</c> if the bracket is random or <c>False</c> for solo or fixed team.
    ' ''' </summary>
    ' ''' <param name="Bracket"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Shared Function IsRandomBracket(ByVal Bracket As eSc2RanksBracket) As Boolean
    '  If (Bracket And eSc2RanksBracket.Random) = eSc2RanksBracket.Random Then
    '    Return True
    '  Else
    '    Return False
    '  End If
    'End Function

    Public Sub ClearCache()
      If (Me.UseRequestCache) Then Call Me.Cache.Clear()
    End Sub

    Private Function QueryAndParse(Of T As Sc2RanksBaseResult)(ByVal Method As eRequestMethod,
                                                               ByVal URL As String,
                                                               ByVal PostData As String,
                                                               ByVal CacheDuration As TimeSpan,
                                                               ByVal IgnoreCache As Boolean,
                                                               <Out()> ByRef Ex As Exception) As T
      Ex = Nothing
      Dim Result As T = Nothing
      Dim Serializer As DataContractJsonSerializer
      Dim ResponseStream As Stream
      Dim Expires As Nullable(Of DateTime)
      Dim CreditsLeft As Int32 = Nothing
      Dim CreditsUsed As Int32 = Nothing

      Try
        ResponseStream = Me.CacheOrQuery(Method, URL, PostData, IgnoreCache, Expires)

        'Create serializer
        Serializer = New DataContractJsonSerializer(GetType(T))

        'Deserialize
        Result = DirectCast(Serializer.ReadObject(ResponseStream), T)
        Result.CacheExpires = Expires

        'If Me.UseRequestCache AndAlso (String.IsNullOrEmpty(Post)) Then
        ResponseStream.Position = 0
        Dim sr As New StreamReader(ResponseStream)

        With Result
          .ResponseRaw = sr.ReadToEnd
          .CreditsLeft = CreditsLeft
          .CreditsUsed = CreditsUsed
        End With

        If Me.UseRequestCache Then Call Me.Cache.AddResponse(URL, PostData, Result.ResponseRaw, CacheDuration)

        'Close stream
        Call ResponseStream.Close()
        Call ResponseStream.Dispose()
      Catch iEx As Exception
        Ex = iEx
      End Try

      Return Result
    End Function

    Private Function QueryAndParse(Of T As {Sc2RanksBaseResult, TArray}, TArrayItem, TArray As ICollection(Of TArrayItem))(ByVal Method As eRequestMethod,
                                                                                                                           ByVal URL As String,
                                                                                                                           ByVal RequestData As String,
                                                                                                                           ByVal CacheDuration As TimeSpan,
                                                                                                                           ByVal IgnoreCache As Boolean,
                                                                                                                           <Out()> ByRef Ex As Exception) As T
      Ex = Nothing
      Dim Result As T = Nothing
      Dim ResultArray As TArray
      Dim Serializer As DataContractJsonSerializer
      Dim ResponseStream As Stream
      Dim Expires As Nullable(Of DateTime)
      Dim CreditsLeft As Int32 = Nothing
      Dim CreditsUsed As Int32 = Nothing

      Try
        ResponseStream = Me.CacheOrQuery(Method, URL, RequestData, IgnoreCache, Expires)

        'Create serializer
        Serializer = New DataContractJsonSerializer(GetType(TArray))

        'Deserialize
        ResultArray = DirectCast(Serializer.ReadObject(ResponseStream), TArray)

        'Create instance and add arrray
        'This is a workaround for Mono as the standard method fails with: System.Runtime.Serialization.SerializationException: Deserialization has failed ---> System.InvalidOperationException: Node type Element is not supported in this operation.  (line 1, column 21)
        'Known bug: https://bugzilla.xamarin.com/show_bug.cgi?id=2205
        Result = DirectCast(GetType(T).GetConstructor(New Type() {}).Invoke(New Object() {}), T)
        With DirectCast(Result, TArray)
          Dim dMax As Int32 = ResultArray.Count - 1
          For d As Int32 = 0 To dMax
            Call .Add(ResultArray(d))
          Next d
        End With

        Result.CacheExpires = Expires

        ResponseStream.Position = 0
        Dim sr As New StreamReader(ResponseStream)

        With Result
          .ResponseRaw = sr.ReadToEnd
          .CreditsLeft = CreditsLeft
          .CreditsUsed = CreditsUsed
        End With

        If Me.UseRequestCache Then Call Me.Cache.AddResponse(URL, RequestData, Result.ResponseRaw, CacheDuration)

        'Close stream
        Call ResponseStream.Close()
        Call ResponseStream.Dispose()
      Catch iEx As Exception
        Ex = iEx
      End Try

      Return Result
    End Function

    Private Enum eRequestMethod
      [Get]
      Post
      Delete
    End Enum

    Private Function CacheOrQuery(ByVal Method As eRequestMethod,
                                  ByVal URL As String,
                                  ByVal RequestData As String,
                                  ByVal IgnoreCache As Boolean,
                                  <Out()> ByRef Expires As Nullable(Of DateTime)) As Stream
      Expires = Nothing
      Dim ResponseStream As Stream
      Dim Stream As Stream
      Dim Request As WebRequest
      Dim Response As WebResponse
      Dim ResponseRaw As String = Nothing

      'Only get from cache when there is no POST data
      If (Not IgnoreCache) AndAlso (Me.UseRequestCache) Then ResponseRaw = Me.Cache.GetResponse(URL, RequestData, Expires)

      If String.IsNullOrEmpty(ResponseRaw) Then
        Request = HttpWebRequest.Create(URL)

        'ToDO: EnumHelper?
        Select Case Method
          Case eRequestMethod.Post
            Request.Method = "POST"
          Case eRequestMethod.Delete
            Request.Method = "DELETE"
          Case Else
            Request.Method = "GET"
        End Select

        If (Not String.IsNullOrEmpty(RequestData)) Then
          Dim Writer As New StreamWriter(Request.GetRequestStream())
          Call Writer.Write(RequestData)
          Call Writer.Flush()
        End If

        Response = Request.GetResponse()

        ResponseStream = Response.GetResponseStream()

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
      Else
        ResponseStream = New MemoryStream(Encoding.UTF8.GetBytes(ResponseRaw))
      End If

      Return ResponseStream
    End Function

#Region "Class AsyncStateWithKey"

    Private NotInheritable Class AsyncStateWithKey
      Public ReadOnly Key As Object
      Public ReadOnly Request As WebRequest
      Public ReadOnly RequestString As String
      Public ReadOnly Post As String
      Public ReadOnly CacheDuration As TimeSpan
      Public ReadOnly FromCacheResponseRaw As String
      Public ReadOnly FromCacheExpires As Nullable(Of DateTime)

      Public Sub New(ByVal Key As Object,
                     ByVal State As WebRequest,
                     ByVal Request As String,
                     ByVal Post As String,
                     ByVal CacheDuration As TimeSpan,
                     ByVal FromCacheResponseRaw As String,
                     ByVal FromCacheExpires As Nullable(Of DateTime))
        Me.Key = Key
        Me.Request = State
        Me.RequestString = Request
        Me.Post = Post
        Me.CacheDuration = CacheDuration
        Me.FromCacheResponseRaw = FromCacheResponseRaw
        Me.FromCacheExpires = FromCacheExpires
      End Sub
    End Class

#End Region

#Region "Class StateAsyncResult"

    Private NotInheritable Class StateAsyncResult
      Implements IAsyncResult

      Private ReadOnly m_AsyncState As Object

      Public Sub New(ByVal AsyncState As Object)
        Me.m_AsyncState = AsyncState
      End Sub

      Public ReadOnly Property AsyncState As Object Implements IAsyncResult.AsyncState
        Get
          Return Me.m_AsyncState
        End Get
      End Property

      Public ReadOnly Property AsyncWaitHandle As WaitHandle Implements IAsyncResult.AsyncWaitHandle
        Get
          Return Nothing
        End Get
      End Property

      Public ReadOnly Property CompletedSynchronously As Boolean Implements IAsyncResult.CompletedSynchronously
        Get
          Return True
        End Get
      End Property

      Public ReadOnly Property IsCompleted As Boolean Implements IAsyncResult.IsCompleted
        Get
          Return True
        End Get
      End Property
    End Class

#End Region

    Private Function QueryAndParseBegin(ByVal Key As Object,
                                        ByVal Method As eRequestMethod,
                                        ByVal URL As String,
                                        ByVal RequestData As String,
                                        ByVal IgnoreCache As Boolean,
                                        ByVal CacheDuration As TimeSpan,
                                        ByVal Callback As AsyncCallback) As IAsyncResult
      Dim Result As IAsyncResult
      Dim Request As WebRequest
      Dim ResponseRaw As String = Nothing
      Dim Expires As Nullable(Of DateTime) = Nothing

      Try
        If (Not IgnoreCache) AndAlso (Me.UseRequestCache) Then ResponseRaw = Me.Cache.GetResponse(URL, RequestData, Expires)

        If (String.IsNullOrEmpty(ResponseRaw)) Then
          Request = HttpWebRequest.Create(URL)

          Select Case Method
            Case eRequestMethod.Post
              Request.Method = "POST"
            Case eRequestMethod.Delete
              Request.Method = "DELETE"
            Case Else
              Request.Method = "GET"
          End Select

          If (Not String.IsNullOrEmpty(RequestData)) Then
            Dim Writer As New StreamWriter(Request.GetRequestStream())
            Call Writer.Write(RequestData)
            Call Writer.Flush()
          End If

          Result = Request.BeginGetResponse(Callback, New AsyncStateWithKey(Key, Request, URL, RequestData, CacheDuration, ResponseRaw, Expires))
        Else
          Result = Callback.BeginInvoke(New StateAsyncResult(New AsyncStateWithKey(Key, Nothing, URL, RequestData, CacheDuration, ResponseRaw, Expires)), Nothing, Nothing)
        End If
      Catch iEx As Exception
        Result = Callback.BeginInvoke(New StateAsyncResult(iEx), Nothing, Nothing)
      End Try

      Return Result
    End Function

    Public Function QueryAndParseEnd(Of T As Sc2RanksBaseResult)(ByVal Result As IAsyncResult,
                                                                 <Out()> ByRef Key As Object,
                                                                 <Out()> ByRef Response As T) As Exception
      Key = Nothing
      Response = Nothing
      Dim Ex As Exception = Nothing
      Dim State As AsyncStateWithKey
      Dim ResponseStream As Stream = Nothing
      Dim Stream As Stream
      Dim Serializer As DataContractJsonSerializer
      Dim CreditsLeft As Int32 = Nothing
      Dim CreditsUsed As Int32 = Nothing

      If (Result Is Nothing) Then
        Ex = New ArgumentNullException("Result")
      Else
        State = TryCast(Result.AsyncState, AsyncStateWithKey)

        If (State Is Nothing) Then
          Ex = New Exception("Invalid AsyncState.")
        Else
          Try
            Key = State.Key

            'Create serializer
            Serializer = New DataContractJsonSerializer(GetType(T))

            If (Not String.IsNullOrEmpty(State.FromCacheResponseRaw)) Then
              ResponseStream = New MemoryStream(Encoding.UTF8.GetBytes(State.FromCacheResponseRaw))
            ElseIf (State.Request Is Nothing) Then
              Ex = New ArgumentNullException("Request")
            Else
              Dim WebResponse As WebResponse

              Try
                WebResponse = State.Request.EndGetResponse(Result)
              Catch iEx As WebException
                Ex = iEx
                WebResponse = iEx.Response
              Catch iEx As Exception
                Return iEx
              End Try

              Call Int32.TryParse(WebResponse.Headers.Get(HeaderCreditsLeft), CreditsLeft)
              Call Int32.TryParse(WebResponse.Headers.Get(HeaderCreditsUsed), CreditsUsed)

              ResponseStream = WebResponse.GetResponseStream()

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
            Response.CacheExpires = State.FromCacheExpires

            ResponseStream.Position = 0
            Dim sr As New StreamReader(ResponseStream)

            With Response
              .ResponseRaw = sr.ReadToEnd
              .CreditsLeft = CreditsLeft
              .CreditsUsed = CreditsUsed
            End With

            If Me.UseRequestCache Then Call Me.Cache.AddResponse(State.RequestString, State.Post, Response.ResponseRaw, State.CacheDuration)

            'Close stream
            Call ResponseStream.Close()
            Call ResponseStream.Dispose()

            Ex = Nothing
          Catch iEx As Exception
            If (Ex Is Nothing) Then Ex = iEx
          End Try
        End If
      End If

      Return Ex
    End Function

    Public Function QueryAndParseEnd(Of T As {Sc2RanksBaseResult, TArray}, TArrayItem, TArray As ICollection(Of TArrayItem))(ByVal Result As IAsyncResult,
                                                                                                                             <Out()> ByRef Key As Object,
                                                                                                                             <Out()> ByRef Response As T) As Exception
      Key = Nothing
      Response = Nothing
      Dim Ex As Exception = Nothing
      Dim State As AsyncStateWithKey
      Dim ResponseArray As TArray
      Dim ResponseStream As Stream = Nothing
      Dim Stream As Stream
      Dim Serializer As DataContractJsonSerializer
      Dim CreditsLeft As Int32 = Nothing
      Dim CreditsUsed As Int32 = Nothing

      If (Result Is Nothing) Then
        Ex = New ArgumentNullException("Result")
      Else
        State = TryCast(Result.AsyncState, AsyncStateWithKey)

        If (State Is Nothing) Then
          Ex = New Exception("Invalid AsyncState.")
        Else
          Try
            Key = State.Key

            'Create serializer
            Serializer = New DataContractJsonSerializer(GetType(TArray))

            If (Not String.IsNullOrEmpty(State.FromCacheResponseRaw)) Then
              ResponseStream = New MemoryStream(Encoding.UTF8.GetBytes(State.FromCacheResponseRaw))
            ElseIf (State.Request Is Nothing) Then
              Ex = New ArgumentNullException("Request")
            Else
              Dim WebResponse As WebResponse

              Try
                WebResponse = State.Request.EndGetResponse(Result)
              Catch iEx As WebException
                Ex = iEx
                WebResponse = iEx.Response
              Catch iEx As Exception
                Return iEx
              End Try

              Call Int32.TryParse(WebResponse.Headers.Get(HeaderCreditsLeft), CreditsLeft)
              Call Int32.TryParse(WebResponse.Headers.Get(HeaderCreditsUsed), CreditsUsed)

              ResponseStream = WebResponse.GetResponseStream()

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
            ResponseArray = DirectCast(Serializer.ReadObject(ResponseStream), TArray)

            'Create instance and add arrray
            'This is a workaround for Mono as the standard method fails with: System.Runtime.Serialization.SerializationException: Deserialization has failed ---> System.InvalidOperationException: Node type Element is not supported in this operation.  (line 1, column 21)
            'Known bug: https://bugzilla.xamarin.com/show_bug.cgi?id=2205
            Response = DirectCast(GetType(T).GetConstructor(New Type() {}).Invoke(New Object() {}), T)
            With DirectCast(Response, TArray)
              Dim dMax As Int32 = ResponseArray.Count - 1
              For d As Int32 = 0 To dMax
                Call .Add(ResponseArray(d))
              Next d
            End With

            Response.CacheExpires = State.FromCacheExpires

            ResponseStream.Position = 0
            Dim sr As New StreamReader(ResponseStream)

            With Response
              .ResponseRaw = sr.ReadToEnd
              .CreditsLeft = CreditsLeft
              .CreditsUsed = CreditsUsed
            End With

            If Me.UseRequestCache Then Call Me.Cache.AddResponse(State.RequestString, State.Post, Response.ResponseRaw, State.CacheDuration)

            'Close stream
            Call ResponseStream.Close()
            Call ResponseStream.Dispose()

            Ex = Nothing
          Catch iEx As Exception
            If (Ex Is Nothing) Then Ex = iEx
          End Try
        End If
      End If

      Return Ex
    End Function

#Region "IDisposable Support"
    Private DisposedValue As Boolean

    Protected Overridable Sub Dispose(ByVal Disposing As Boolean)
      If (Not Me.DisposedValue) Then
        If Disposing Then
          If Me.UseRequestCache Then Call Me.Cache.Write(Me.CacheStream)
        End If
      End If
      Me.DisposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
      Call Me.Dispose(True)
      Call GC.SuppressFinalize(Me)
    End Sub

#End Region
  End Class
End Namespace