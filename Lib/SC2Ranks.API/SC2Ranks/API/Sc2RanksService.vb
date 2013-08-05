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

    'ToDo: Some calls have a limit of 50 some only 10, taking lowest mean
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

    ''' <summary>
    ''' Returns base information about the API. Such as the value leagues, brackets, regions or rank regions that can be used. 
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseData(<Out()> ByRef Result As Sc2RanksBaseDataResult,
                                Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksBaseDataResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns base information about the API. Such as the value leagues, brackets, regions or rank regions that can be used. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Callback"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseDataBegin(ByVal Key As Object,
                                     ByVal Callback As AsyncCallback,
                                     Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetBaseDataEnd(ByVal Result As IAsyncResult,
                                   <Out()> ByRef Key As Object,
                                   <Out()> ByRef Response As Sc2RanksBaseDataResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksBaseDataResult)(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Characters"

#Region "Function GetCharacter"

    ''' <summary>
    ''' Returns a single character. 
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharacter(ByVal Region As eSc2RanksRegion,
                                 ByVal BattleNetID As Int32,
                                 <Out()> ByRef Result As Sc2RanksCharacterResult,
                                 Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, String.Format("characters/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns a single character. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Region"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Callback"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

#Region "Function GetTeamCharacterList"

    ''' <summary>
    ''' Returns all of the teams the character is on, as well as the characters info. Does not return other characters on the team. 
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Result"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTeamCharacterList(ByVal Region As eSc2RanksRegion,
                                         ByVal BattleNetID As Int32,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         <Out()> ByRef Result As Sc2RanksTeamCharacterListResult,
                                         Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("expansion={0}&bracket={1}&league={2}", Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksTeamCharacterListResult, Sc2RanksTeamCharacterListElement, IList(Of Sc2RanksTeamCharacterListElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns all of the teams the character is on, as well as the characters info. Does not return other characters on the team. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Region"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTeamCharacterListBegin(ByVal Key As Object,
                                              ByVal Region As eSc2RanksRegion,
                                              ByVal BattleNetID As Int32,
                                              ByVal Expansion As eSc2RanksExpansion,
                                              ByVal Bracket As eSc2RanksBracket,
                                              ByVal League As eSc2RanksLeague,
                                              ByVal Callback As AsyncCallback,
                                              Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                              Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("expansion={0}&bracket={1}&league={2}", Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetTeamCharacterListEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksTeamCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksTeamCharacterListResult, Sc2RanksTeamCharacterListElement, IList(Of Sc2RanksTeamCharacterListElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function SearchCharacterTeams"

    ''' <summary>
    ''' Returns all the teams with a team member whos name matches the search. 
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="Match"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Result"></param>
    ''' <param name="Race"></param>
    ''' <param name="Limit"></param>
    ''' <param name="Page"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SearchCharacterTeamList(ByVal Name As String,
                                            ByVal Match As eSc2RanksMatchType,
                                            ByVal RankRegion As eSc2RanksRankRegion,
                                            ByVal Expansion As eSc2RanksExpansion,
                                            ByVal Bracket As eSc2RanksBracket,
                                            ByVal League As eSc2RanksLeague,
                                            <Out()> ByRef Result As Sc2RanksTeamCharacterListResult,
                                            Optional Race As Nullable(Of eSc2RanksRace) = Nothing,
                                            Optional ByVal Limit As Int32 = MaxRequestLimit,
                                            Optional ByVal Page As Int32 = 1,
                                            Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksTeamCharacterListResult, Sc2RanksTeamCharacterListElement, IList(Of Sc2RanksTeamCharacterListElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns all the teams with a team member whos name matches the search. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Name"></param>
    ''' <param name="Match"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="Limit"></param>
    ''' <param name="Page"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SearchCharacterTeamListBegin(ByVal Key As Object,
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
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function SearchCharacterTeamListEnd(ByVal Result As IAsyncResult,
                                               <Out()> ByRef Key As Object,
                                               <Out()> ByRef Response As Sc2RanksTeamCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksTeamCharacterListResult, Sc2RanksTeamCharacterListElement, IList(Of Sc2RanksTeamCharacterListElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCharacters"

    ''' <summary>
    ''' Accepts an array of characters with region and bnet_id, up to 200 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. 
    ''' </summary>
    ''' <param name="Characters"></param>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharacterList(ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                     <Out()> ByRef Result As Sc2RanksCharacterListResult,
                                     Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (RequestData.Length > 0) Then Call RequestData.Append("&")

            Call RequestData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterListResult, Sc2RanksCharacterResult, IList(Of Sc2RanksCharacterResult))(eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Accepts an array of characters with region and bnet_id, up to 200 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Characters"></param>
    ''' <param name="Callback"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharacterListBegin(ByVal Key As Object,
                                          ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                          ByVal Callback As AsyncCallback,
                                          Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (RequestData.Length > 0) Then Call RequestData.Append("&")

            Call RequestData.AppendFormat("characters[{0}][region]={1}", (d + 1).ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", (d + 1).ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharacterListEnd(ByVal Result As IAsyncResult,
                                        <Out()> ByRef Key As Object,
                                        <Out()> ByRef Response As Sc2RanksCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCharacterListResult, Sc2RanksCharacterResult, IList(Of Sc2RanksCharacterResult))(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Teams"

#Region "Function GetCharacterTeams"

    ''' <summary>
    ''' Accepts an array of characters with *region* and *bnet_id*, up to 50 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. Returns all of the teams that match the given team filters for the characters passed. Does not return team characters. 
    ''' </summary>
    ''' <param name="Characters"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Result"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamList(ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                         ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         <Out()> ByRef Result As Sc2RanksCharacterTeamListResult,
                                         Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            Call RequestData.AppendFormat("&characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Result = Me.Query.QueryAndParse(Of Sc2RanksCharacterTeamListResult, Sc2RanksCharacterTeamListElement, IList(Of Sc2RanksCharacterTeamListElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/teams", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Accepts an array of characters with *region* and *bnet_id*, up to 50 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. Returns all of the teams that match the given team filters for the characters passed. Does not return team characters. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Characters"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamListBegin(ByVal Key As Object,
                                              ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                              ByVal RankRegion As eSc2RanksRankRegion,
                                              ByVal Expansion As eSc2RanksExpansion,
                                              ByVal Bracket As eSc2RanksBracket,
                                              ByVal League As eSc2RanksLeague,
                                              ByVal Callback As AsyncCallback,
                                              Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                              Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            Call RequestData.AppendFormat("&characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/teams", Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCharacterTeamListEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksCharacterTeamListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCharacterTeamListResult, Sc2RanksCharacterTeamListElement, IList(Of Sc2RanksCharacterTeamListElement))(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Clans"

#Region "Function GetClan"

    Public Function GetClan(ByVal RankRegion As eSc2RanksRankRegion,
                            ByVal Tag As String,
                            <Out()> ByRef Result As Sc2RanksClanResult,
                            Optional ByVal Bracket As Nullable(Of eSc2RanksBracket) = Nothing,
                            Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      If (Bracket.HasValue) Then Call RequestData.AppendFormat("bracket={0}", Enums.BracketBuffer.GetValue(Bracket.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksClanResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetClanBegin(ByVal Key As Object,
                                 ByVal RankRegion As eSc2RanksRankRegion,
                                 ByVal Tag As String,
                                 ByVal Callback As AsyncCallback,
                                 Optional ByVal Bracket As Nullable(Of eSc2RanksBracket) = Nothing,
                                 Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      If (Bracket.HasValue) Then Call RequestData.AppendFormat("bracket={0}", Enums.BracketBuffer.GetValue(Bracket.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetClanEnd(ByVal Result As IAsyncResult,
                               <Out()> ByRef Key As Object,
                               <Out()> ByRef Response As Sc2RanksClanResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksClanResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetClanCharacterList"

    Public Function GetClanCharacterList(ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Tag As String,
                                         <Out()> ByRef Result As Sc2RanksClanCharacterListResult,
                                         Optional ByVal Bracket As Nullable(Of eSc2RanksBracket) = Nothing,
                                         Optional ByVal Limit As Int32 = MaxRequestLimit,
                                         Optional ByVal Page As Int32 = 1,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("limit={0}&page={1}", Limit.ToString(), Page.ToString())
      If (Bracket.HasValue) Then Call RequestData.AppendFormat("&bracket={0}", Enums.BracketBuffer.GetValue(Bracket.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksClanCharacterListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/characters/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetClanCharacterListBegin(ByVal Key As Object,
                                              ByVal RankRegion As eSc2RanksRankRegion,
                                              ByVal Tag As String,
                                              ByVal Callback As AsyncCallback,
                                              Optional ByVal Bracket As Nullable(Of eSc2RanksBracket) = Nothing,
                                              Optional ByVal Limit As Int32 = MaxRequestLimit,
                                              Optional ByVal Page As Int32 = 1,
                                              Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("limit={0}&page={1}", Limit.ToString(), Page.ToString())
      If (Bracket.HasValue) Then Call RequestData.AppendFormat("&bracket={0}", Enums.BracketBuffer.GetValue(Bracket.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/characters/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetClanCharacterListEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksClanCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksClanCharacterListResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetClanTeamList"

    Public Function GetClanTeamList(ByVal RankRegion As eSc2RanksRankRegion,
                                    ByVal Tag As String,
                                    ByVal Expansion As eSc2RanksExpansion,
                                    ByVal Bracket As eSc2RanksBracket,
                                    ByVal League As eSc2RanksLeague,
                                    <Out()> ByRef Result As Sc2RanksClanTeamListResult,
                                    Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                    Optional ByVal Limit As Int32 = MaxRequestLimit,
                                    Optional ByVal Page As Int32 = 1,
                                    Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("tag={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Tag, Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then Call RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksClanTeamListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/teams/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    Public Function GetClanTeamListBegin(ByVal Key As Object,
                                         ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Tag As String,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         ByVal Callback As AsyncCallback,
                                         Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                         Optional ByVal Limit As Int32 = MaxRequestLimit,
                                         Optional ByVal Page As Int32 = 1,
                                         Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("tag={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Tag, Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then Call RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/teams/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetClanTeamListEnd(ByVal Result As IAsyncResult,
                                       <Out()> ByRef Key As Object,
                                       <Out()> ByRef Response As Sc2RanksClanTeamListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksClanTeamListResult)(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Rankings"

#Region "Function GetRankingsTop"

    ''' <summary>
    ''' Returns the top limit teams given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="TopCount"></param>
    ''' <param name="Result"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRankingsTop(ByVal RankRegion As eSc2RanksRankRegion,
                                   ByVal Expansion As eSc2RanksExpansion,
                                   ByVal Bracket As eSc2RanksBracket,
                                   ByVal League As eSc2RanksLeague,
                                   ByVal TopCount As Int32,
                                   <Out()> ByRef Result As Sc2RanksTeamCharacterListResult,
                                   Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                   Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksTeamCharacterListResult, Sc2RanksTeamCharacterListElement, IList(Of Sc2RanksTeamCharacterListElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, "rankings", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the top limit teams given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="TopCount"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRankingsTopBegin(ByVal Key As Object,
                                        ByVal RankRegion As eSc2RanksRankRegion,
                                        ByVal Expansion As eSc2RanksExpansion,
                                        ByVal Bracket As eSc2RanksBracket,
                                        ByVal League As eSc2RanksLeague,
                                        ByVal TopCount As Int32,
                                        ByVal Callback As AsyncCallback,
                                        Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                        Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "rankings", Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetRankingsTopEnd(ByVal Result As IAsyncResult,
                                      <Out()> ByRef Key As Object,
                                      <Out()> ByRef Response As Sc2RanksTeamCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksTeamCharacterListResult, Sc2RanksTeamCharacterListElement, IList(Of Sc2RanksTeamCharacterListElement))(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Divisions"

#Region "Function GetDivisionsTop"

    ''' <summary>
    ''' Returns the top limit divisions given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="TopCount"></param>
    ''' <param name="Result"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDivisionsTop(ByVal RankRegion As eSc2RanksRankRegion,
                                    ByVal Expansion As eSc2RanksExpansion,
                                    ByVal Bracket As eSc2RanksBracket,
                                    ByVal League As eSc2RanksLeague,
                                    ByVal TopCount As Int32,
                                    <Out()> ByRef Result As Sc2RanksDivisionListResult,
                                    Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                    Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksDivisionListResult, Sc2RanksDivisionResult, IList(Of Sc2RanksDivisionResult))(eRequestMethod.Post, String.Format(BaseUrlFormat, "divisions", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the top limit divisions given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="TopCount"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDivisionsTopBegin(ByVal Key As Object,
                                         ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         ByVal TopCount As Int32,
                                         ByVal Callback As AsyncCallback,
                                         Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                         Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "divisions", Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetDivisionsTopEnd(ByVal Result As IAsyncResult,
                                       <Out()> ByRef Key As Object,
                                       <Out()> ByRef Response As Sc2RanksDivisionListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksDivisionListResult, Sc2RanksDivisionResult, IList(Of Sc2RanksDivisionResult))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetDivision"

    ''' <summary>
    ''' Returns base information about the division.
    ''' </summary>
    ''' <param name="DivisionID"></param>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDivision(ByVal DivisionID As String,
                                <Out()> ByRef Result As Sc2RanksDivisionResult,
                                Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksDivisionResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns base information about the division.
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="DivisionID"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

#Region "Function GetDivisionTeamList"

    ''' <summary>
    ''' Returns the top limit teams in the division. This cannot be paginated and is intended to be used for showing mini ranking type of widgets and not data collection.
    ''' </summary>
    ''' <param name="DivisionID"></param>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDivisionTeamList(ByVal DivisionID As String,
                                        <Out()> ByRef Result As Sc2RanksDivisionTeamsListResult,
                                        Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksDivisionTeamsListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/teams/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the top limit teams in the division. This cannot be paginated and is intended to be used for showing mini ranking type of widgets and not data collection.
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="DivisionID"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDivisionTeamListBegin(ByVal Key As Object,
                                             ByVal DivisionID As String,
                                             ByVal Callback As AsyncCallback,
                                             Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                             Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/teams/{0}", DivisionID), Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetDivisionTeamListEnd(ByVal Result As IAsyncResult,
                                           <Out()> ByRef Key As Object,
                                           <Out()> ByRef Response As Sc2RanksDivisionTeamsListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksDivisionTeamsListResult)(Result, Key, Response)
    End Function

#End Region

#End Region

#Region "API Custom Divisions"

#Region "Function GetCustomDivisions"

    ''' <summary>
    ''' Returns all of the custom divisions attached to the user account of the API Key. 
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionList(<Out()> ByRef Result As Sc2RanksCustomDivisionListResult,
                                          Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionListResult, Sc2RanksCustomDivisionResult, IList(Of Sc2RanksCustomDivisionResult))(eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns all of the custom divisions attached to the user account of the API Key. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="Callback"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionListBegin(ByVal Key As Object,
                                               ByVal Callback As AsyncCallback,
                                               Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionListEnd(ByVal Result As IAsyncResult,
                                             <Out()> ByRef Key As Object,
                                             <Out()> ByRef Response As Sc2RanksCustomDivisionListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionListResult, Sc2RanksCustomDivisionResult, IList(Of Sc2RanksCustomDivisionResult))(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivision"

    ''' <summary>
    ''' Returns base information of the custom division by the given id. 
    ''' </summary>
    ''' <param name="DivisionID"></param>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivision(ByVal DivisionID As String,
                                      <Out()> ByRef Result As Sc2RanksCustomDivisionResult,
                                      Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns base information of the custom division by the given id. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="DivisionID"></param>
    ''' <param name="Callback"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Returns the teams in the custom division filtered by the passed params. 
    ''' </summary>
    ''' <param name="DivisionID"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Result"></param>
    ''' <param name="Race"></param>
    ''' <param name="Limit"></param>
    ''' <param name="Page"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionTeamList(ByVal DivisionID As String,
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
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionTeamsResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the teams in the custom division filtered by the passed params. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="DivisionID"></param>
    ''' <param name="RankRegion"></param>
    ''' <param name="Expansion"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="League"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Race"></param>
    ''' <param name="Limit"></param>
    ''' <param name="Page"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionTeamListBegin(ByVal Key As Object,
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
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("&rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionTeamListEnd(ByVal Result As IAsyncResult,
                                                 <Out()> ByRef Key As Object,
                                                 <Out()> ByRef Response As Sc2RanksCustomDivisionTeamsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionTeamsResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivisionCharacters"

    ''' <summary>
    ''' Returns the characters in the custom division without any team data, filtered by the passed params. 
    ''' </summary>
    ''' <param name="DivisionID"></param>
    ''' <param name="Region"></param>
    ''' <param name="Result"></param>
    ''' <param name="Limit"></param>
    ''' <param name="Page"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionCharacterList(ByVal DivisionID As String,
                                                   ByVal Region As eSc2RanksRegion,
                                                   <Out()> ByRef Result As Sc2RanksCustomDivisionCharacterListResult,
                                                   Optional ByVal Limit As Int32 = MaxRequestLimit,
                                                   Optional ByVal Page As Int32 = 1,
                                                   Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("region={0}&limit={1}&page={2}", Enums.RegionBuffer.GetValue(Region), Limit.ToString(), Page.ToString())

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionCharacterListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the characters in the custom division without any team data, filtered by the passed params. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="DivisionID"></param>
    ''' <param name="Region"></param>
    ''' <param name="Callback"></param>
    ''' <param name="Limit"></param>
    ''' <param name="Page"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionCharacterListBegin(ByVal Key As Object,
                                                        ByVal DivisionID As String,
                                                        ByVal Region As eSc2RanksRegion,
                                                        ByVal Callback As AsyncCallback,
                                                        Optional ByVal Limit As Int32 = MaxRequestLimit,
                                                        Optional ByVal Page As Int32 = 1,
                                                        Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("region={0}&limit={1}&page={2}", Enums.RegionBuffer.GetValue(Region), Limit.ToString(), Page.ToString())

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionCharacterListEnd(ByVal Result As IAsyncResult,
                                                      <Out()> ByRef Key As Object,
                                                      <Out()> ByRef Response As Sc2RanksCustomDivisionCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionCharacterListResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function CustomDivisionAdd"

    ''' <summary>
    ''' Adds characters to the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, added or already added. 
    ''' </summary>
    ''' <param name="DivisionID"></param>
    ''' <param name="Characters"></param>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionAdd(ByVal DivisionID As String,
                                      ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                      <Out()> ByRef Result As Sc2RanksCustomDivisionManageListResult,
                                      Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (RequestData.Length > 0) Then Call RequestData.Append("&")

            Call RequestData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Adds characters to the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, added or already added. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="DivisionID"></param>
    ''' <param name="Characters"></param>
    ''' <param name="Callback"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionAddBegin(ByVal Key As Object,
                                           ByVal DivisionID As String,
                                           ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                           ByVal Callback As AsyncCallback,
                                           Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (RequestData.Length > 0) Then Call RequestData.Append("&")

            Call RequestData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function CustomDivisionAddEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksCustomDivisionManageListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(Result, Key, Response)
    End Function

#End Region

#Region "Function CustomDivisionRemove"

    ''' <summary>
    ''' Removes characters from the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, removed, not added or unknown. 
    ''' </summary>
    ''' <param name="DivisionID"></param>
    ''' <param name="Characters"></param>
    ''' <param name="Result"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionRemove(ByVal DivisionID As String,
                                         ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                         <Out()> ByRef Result As Sc2RanksCustomDivisionManageListResult,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (RequestData.Length > 0) Then Call RequestData.Append("&")

            Call RequestData.AppendFormat("characters[{0}][region]={1}", d.ToString(), Enums.RegionBuffer.GetValue(.Region))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", d.ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchBasePlayerCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Removes characters from the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, removed, not added or unknown. 
    ''' </summary>
    ''' <param name="Key"></param>
    ''' <param name="DivisionID"></param>
    ''' <param name="Characters"></param>
    ''' <param name="Callback"></param>
    ''' <param name="IgnoreCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionRemoveBegin(ByVal Key As Object,
                                              ByVal DivisionID As String,
                                              ByVal Characters As IList(Of Sc2RanksBulkCharacter),
                                              ByVal Callback As AsyncCallback,
                                              Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      If (Characters IsNot Nothing) AndAlso (Characters.Count > 0) Then
        Dim dMax As Int32 = Characters.Count - 1
        For d = 0 To dMax
          With Characters.Item(d)
            If (RequestData.Length > 0) Then Call RequestData.Append("&")

            Call RequestData.AppendFormat("characters[{0}][region]={1}", (d + 1).ToString(), Enums.RegionBuffer.GetValue(eSc2RanksRegion.Global))
            Call RequestData.AppendFormat("&characters[{0}][bnet_id]={1}", (d + 1).ToString(), .BattleNetID.ToString())
          End With
        Next d
      End If

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), IgnoreCache, Me.CacheConfig.SearchBasePlayerCacheDuration, Callback)
    End Function

    Public Function CustomDivisionRemoveEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksCustomDivisionManageListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionManageListResult, Sc2RanksCustomDivisionManageElement, IList(Of Sc2RanksCustomDivisionManageElement))(Result, Key, Response)
    End Function

#End Region

#End Region

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