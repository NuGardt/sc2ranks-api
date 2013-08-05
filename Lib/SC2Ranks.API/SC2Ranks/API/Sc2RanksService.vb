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
Imports NuGardt.SC2Ranks.API.Result.Element
Imports NuGardt.SC2Ranks.Helper
Imports NuGardt.API.Helper.JSON
Imports NuGardt.SC2Ranks.API.Result
Imports System.Text

Namespace SC2Ranks.API
  ''' <summary>
  ''' Class containing all API calls to SC2Ranks.
  ''' </summary>
  ''' <remarks></remarks>
  Public Class Sc2RanksService
    Implements IDisposable

    Public Const MaxRequestLimit As Int32 = 10

    Private Const BaseUrlFormat As String = "http://api.sc2ranks.com/v2/{0}?api_key={1}"

    Private ReadOnly Query As JsonHelper(Of Sc2RanksBaseResult)
    Private ReadOnly CacheConfig As ICacheConfig
    Private ReadOnly CacheStream As Stream
    Private ReadOnly m_ApiKey As String

    ''' <summary>
    ''' Construct.
    ''' </summary>
    ''' <param name="ApiKey"></param>
    ''' <remarks></remarks>
    Private Sub New(ByVal ApiKey As String,
                    ByVal Query As JsonHelper(Of Sc2RanksBaseResult),
                    ByVal CacheConfig As ICacheConfig,
                    ByVal CacheStream As Stream)
      Me.m_ApiKey = ApiKey

      Me.Query = Query

      Me.CacheConfig = CacheConfig
      Me.CacheStream = CacheStream

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
        Dim Query As JsonHelper(Of Sc2RanksBaseResult)

        Query = New JsonHelper(Of Sc2RanksBaseResult)((CacheStream IsNot Nothing))

        If (CacheStream IsNot Nothing) Then
          Ex = Query.ReadCache(CacheStream)

          If (Ex IsNot Nothing) AndAlso IgnoreFaultCacheStream Then Ex = Nothing
        End If

        If (Ex Is Nothing) Then Instance = New Sc2RanksService(Uri.EscapeUriString(AppKey), Query, CacheConfig, CacheStream)
      End If

      Return Ex
    End Function

#End Region

    'Sorted in regions as in the documentation
    'http://www.sc2ranks.com/api

#Region "API Base Data"

#Region "Function GetData"

    Public Function GetData(<Out()> ByRef Result As Sc2RanksDataResult,
                            Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksDataResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetDataBegin(ByVal Key As Object,
                                 ByVal Callback As AsyncCallback,
                                 Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetDataEnd(ByVal Result As IAsyncResult,
                               <Out()> ByRef Key As Object,
                               <Out()> ByRef Response As Sc2RanksDataResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksDataResult)(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Characters"

#Region "Function GetCharacter"

    Public Function GetCharacter(ByVal Region As eSc2RanksRegion,
                                 ByVal BattleNetID As Int32,
                                 <Out()> ByRef Result As Sc2RanksCharacterResult,
                                 Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, String.Format("characters/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCharacterBegin(ByVal Key As Object,
                                      ByVal Region As eSc2RanksRegion,
                                      ByVal BattleNetID As Int32,
                                      ByVal Callback As AsyncCallback,
                                      Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, String.Format("characters/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharacterEnd(ByVal Result As IAsyncResult,
                                    <Out()> ByRef Key As Object,
                                    <Out()> ByRef Response As Sc2RanksCharacterResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCharacterResult)(Result, Key, Response)
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharacterTeamsEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksCharacterTeamsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function SearchCharacterTeams"

    Public Function SearchCharacterTeams(ByVal Name As String,
                                         ByVal Match As eSc2RanksMatchType,
                                         ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         <Out()> ByRef Result As Sc2RanksCharacterTeamsResult,
                                         Optional Race As Nullable(Of eSc2RanksRace) = Nothing,
                                         Optional ByVal Limit As Int32 = MaxRequestLimit,
                                         Optional ByVal Page As Int32 = 1,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function SearchCharacterTeamsBegin(ByVal Key As Object,
                                              ByVal Name As String,
                                              ByVal Match As eSc2RanksMatchType,
                                              ByVal RankRegion As eSc2RanksRankRegion,
                                              ByVal Expansion As eSc2RanksExpansion,
                                              ByVal Bracket As eSc2RanksBracket,
                                              ByVal League As eSc2RanksLeague,
                                              ByVal Callback As AsyncCallback,
                                              Optional Race As Nullable(Of eSc2RanksRace) = Nothing,
                                              Optional ByVal Limit As Int32 = MaxRequestLimit,
                                              Optional ByVal Page As Int32 = 1,
                                              Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function SearchCharacterTeamsEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksCharacterTeamsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCharacters"

    Public Function GetCharacters(ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                  <Out()> ByRef Result As Sc2RanksCharacterListResult,
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterListResult, Sc2RanksCharacterResult, IList(Of Sc2RanksCharacterResult))(eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharactersEnd(ByVal Result As IAsyncResult,
                                     <Out()> ByRef Key As Object,
                                     <Out()> ByRef Response As Sc2RanksCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCharacterListResult, Sc2RanksCharacterResult, IList(Of Sc2RanksCharacterResult))(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Teams"

#End Region

#Region "API Clans"

#End Region

#Region "API Rankings"

#Region "Function GetRankingsTop"

    Public Function GetRankingsTop(ByVal RankRegion As eSc2RanksRankRegion,
                                   ByVal Expansion As eSc2RanksExpansion,
                                   ByVal Bracket As eSc2RanksBracket,
                                   ByVal League As eSc2RanksLeague,
                                   ByVal TopCount As Int32,
                                   <Out()> ByRef Result As Sc2RanksCharacterTeamsResult,
                                   Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                   Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, "rankings", Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetRankingsTopBegin(ByVal Key As Object,
                                        ByVal RankRegion As eSc2RanksRankRegion,
                                        ByVal Expansion As eSc2RanksExpansion,
                                        ByVal Bracket As eSc2RanksBracket,
                                        ByVal League As eSc2RanksLeague,
                                        ByVal TopCount As Int32,
                                        ByVal Callback As AsyncCallback,
                                        Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                        Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "rankings", Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetRankingsTopEnd(ByVal Result As IAsyncResult,
                                      <Out()> ByRef Key As Object,
                                      <Out()> ByRef Response As Sc2RanksCharacterTeamsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCharacterTeamsResult, Sc2RanksCharacterTeamElement, IList(Of Sc2RanksCharacterTeamElement))(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Divisions"

#Region "Function GetRankingsTop"

    Public Function GetDivisionsTop(ByVal RankRegion As eSc2RanksRankRegion,
                                    ByVal Expansion As eSc2RanksExpansion,
                                    ByVal Bracket As eSc2RanksBracket,
                                    ByVal League As eSc2RanksLeague,
                                    ByVal TopCount As Int32,
                                    <Out()> ByRef Result As Sc2RanksDivisionListResult,
                                    Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                    Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksDivisionListResult, Sc2RanksDivisionResult, IList(Of Sc2RanksDivisionResult))(eRequestMethod.Post, String.Format(BaseUrlFormat, "divisions", Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetDivisionsTopBegin(ByVal Key As Object,
                                         ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         ByVal TopCount As Int32,
                                         ByVal Callback As AsyncCallback,
                                         Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                         Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "divisions", Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetDivisionsTopEnd(ByVal Result As IAsyncResult,
                                       <Out()> ByRef Key As Object,
                                       <Out()> ByRef Response As Sc2RanksDivisionListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksDivisionListResult, Sc2RanksDivisionResult, IList(Of Sc2RanksDivisionResult))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetDivision"

    Public Function GetDivision(ByVal DivisionID As String,
                                <Out()> ByRef Result As Sc2RanksDivisionResult,
                                Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksDivisionResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetDivisionBegin(ByVal Key As Object,
                                     ByVal DivisionID As String,
                                     ByVal Callback As AsyncCallback,
                                     Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                     Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetDivisionEnd(ByVal Result As IAsyncResult,
                                   <Out()> ByRef Key As Object,
                                   <Out()> ByRef Response As Sc2RanksDivisionResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksDivisionResult)(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Custom Divisions"

#Region "Function GetCustomDivisions"

    Public Function GetCustomDivisions(<Out()> ByRef Result As Sc2RanksCustomDivisionListResult,
                                       Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionListResult, Sc2RanksCustomDivisionResult, IList(Of Sc2RanksCustomDivisionResult))(eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCustomDivisionsBegin(ByVal Key As Object,
                                            ByVal Callback As AsyncCallback,
                                            Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionsEnd(ByVal Result As IAsyncResult,
                                          <Out()> ByRef Key As Object,
                                          <Out()> ByRef Response As Sc2RanksCustomDivisionListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionListResult, Sc2RanksCustomDivisionResult, IList(Of Sc2RanksCustomDivisionResult))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivision"

    Public Function GetCustomDivision(ByVal DivisionID As String,
                                      <Out()> ByRef Result As Sc2RanksCustomDivisionResult,
                                      Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCustomDivisionBegin(ByVal Key As Object,
                                           ByVal DivisionID As String,
                                           ByVal Callback As AsyncCallback,
                                           Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksCustomDivisionResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionResult)(Result, Key, Response)
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
                                           Optional ByVal Limit As Int32 = MaxRequestLimit,
                                           Optional ByVal Page As Int32 = 1,
                                           Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionTeamsResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

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
                                                Optional ByVal Limit As Int32 = MaxRequestLimit,
                                                Optional ByVal Page As Int32 = 1,
                                                Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then PostData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionTeamsEnd(ByVal Result As IAsyncResult,
                                              <Out()> ByRef Key As Object,
                                              <Out()> ByRef Response As Sc2RanksCustomDivisionTeamsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionTeamsResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivisionCharacters"

    Public Function GetCustomDivisionCharacters(ByVal DivisionID As String,
                                                ByVal Region As eSc2RanksRegion,
                                                <Out()> ByRef Result As Sc2RanksCustomDivisionCharactersResult,
                                                Optional ByVal Limit As Int32 = MaxRequestLimit,
                                                Optional ByVal Page As Int32 = 1,
                                                Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("region={0}&limit={1}&page={2}", Enums.RegionBuffer.GetValue(Region), Limit.ToString(), Page.ToString())

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionCharactersResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetCustomDivisionCharactersBegin(ByVal Key As Object,
                                                     ByVal DivisionID As String,
                                                     ByVal Region As eSc2RanksRegion,
                                                     ByVal Callback As AsyncCallback,
                                                     Optional ByVal Limit As Int32 = MaxRequestLimit,
                                                     Optional ByVal Page As Int32 = 1,
                                                     Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim PostData As New StringBuilder

      Call PostData.AppendFormat("region={0}&limit={1}&page={2}", Enums.RegionBuffer.GetValue(Region), Limit.ToString(), Page.ToString())

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionCharactersEnd(ByVal Result As IAsyncResult,
                                                   <Out()> ByRef Key As Object,
                                                   <Out()> ByRef Response As Sc2RanksCustomDivisionCharactersResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionCharactersResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function CustomDivisionAdd"

    Public Function CustomDivisionAdd(ByVal DivisionID As String,
                                      ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                      <Out()> ByRef Result As Sc2RanksCustomDivisionManageListResult,
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function CustomDivisionAddEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksCustomDivisionManageListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function CustomDivisionRemove"

    Public Function CustomDivisionRemove(ByVal DivisionID As String,
                                         ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                         <Out()> ByRef Result As Sc2RanksCustomDivisionManageListResult,
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), PostData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function CustomDivisionRemoveEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksCustomDivisionManageListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(Result, Key, Response)
    End Function

#End Region

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
      Call Me.Query.ClearCache()
    End Sub

#Region "IDisposable Support"
    Private DisposedValue As Boolean

    Protected Overridable Sub Dispose(ByVal Disposing As Boolean)
      If (Not Me.DisposedValue) Then
        If Disposing Then
          Call Me.Query.WriteCache(Me.CacheStream)
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