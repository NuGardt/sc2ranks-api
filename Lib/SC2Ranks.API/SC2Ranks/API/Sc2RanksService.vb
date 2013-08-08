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

    Friend Const HeaderCreditsLeft As String = "X-Credits-Left"
    Friend Const HeaderCreditsUsed As String = "X-Credits-Used"

    ''' <summary>
    ''' Maximum request limit.
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MaxRequestLimit As Int32 = 50

    ''' <summary>
    ''' Base URL to access the SC2Ranks.com API.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const BaseUrlFormat As String = "http://api.sc2ranks.com/v2/{0}?api_key={1}"

    ''' <summary>
    ''' Query Support.
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly Query As JsonHelper(Of ISc2RanksBaseResult)

    ''' <summary>
    ''' Caching config.
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly CacheConfig As ICacheConfig

    ''' <summary>
    ''' Stream for reading and writing cache data.
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly CacheStream As Stream

    ''' <summary>
    ''' Private SC2Ranks API key.
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly m_ApiKey As String

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <param name="ApiKey">Your private SC2Ranks API key.</param>
    ''' <remarks></remarks>
    Private Sub New(ByVal ApiKey As String,
                    ByVal Query As JsonHelper(Of ISc2RanksBaseResult),
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
    ''' <param name="ApiKey">Your private SC2Ranks API key.</param>
    ''' <param name="Instance">Contains the instance if no <c>Exception</c> is requren otherwise is <c>Nothing</c>.</param>
    ''' <param name="CacheStream">Optional. Default is <c>Nothing</c>. Stream for reading and writing cache data.</param>
    ''' <param name="CacheConfig">Optional. If <c>Nothing</c> then default duration will be used to each API method. Config for caching duration for each API method.</param>
    ''' <param name="IgnoreBadCacheStream">Optional. Default is <c>True</c>. When set to <c>True</c> then all read errors are ignored and the data is discarded otherwise <c>False</c> to get the error messages also fails instance creation.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateInstance(ByVal ApiKey As String,
                                          <Out()> ByRef Instance As Sc2RanksService,
                                          Optional ByVal CacheStream As Stream = Nothing,
                                          Optional ByVal CacheConfig As ICacheConfig = Nothing,
                                          Optional ByVal IgnoreBadCacheStream As Boolean = True) As Exception
      Instance = Nothing
      Dim Ex As Exception = Nothing

      If String.IsNullOrEmpty(ApiKey) Then
        Ex = New ArgumentNullException("ApiKey")
      ElseIf (ApiKey.Length < 32) Then
        'Exact length of API is known to date. Seems length is always 37. So adding 5 in each direction.
        Ex = New FormatException("API Key too short.")
      ElseIf (ApiKey.Length > 42) Then
        Ex = New FormatException("API Key too long.")
      Else
        Dim Query As JsonHelper(Of ISc2RanksBaseResult)

        Query = New JsonHelper(Of ISc2RanksBaseResult)((CacheStream IsNot Nothing))

        If (CacheStream IsNot Nothing) Then
          Ex = Query.ReadCache(CacheStream)

          If (Ex IsNot Nothing) AndAlso IgnoreBadCacheStream Then Ex = Nothing
        End If

        If (Ex Is Nothing) Then Instance = New Sc2RanksService(Uri.EscapeUriString(ApiKey), Query, CacheConfig, CacheStream)
      End If

      Return Ex
    End Function

#End Region

    'Sorted in regions as in the documentation
    'http://www.sc2ranks.com/api

#Region "API Base Data"

#Region "Function GetBaseData"

    ''' <summary>
    ''' Returns base information about the API. Such as the value leagues, brackets, regions or rank regions that can be used. 
    ''' </summary>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetBaseData(<Out()> ByRef Result As Sc2RanksGetBaseDataResult,
                                Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetBaseDataResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, Me.CacheConfig.GetBaseDataCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns base information about the API. Such as the value leagues, brackets, regions or rank regions that can be used. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetBaseDataBegin(ByVal Key As Object,
                                     ByVal Callback As AsyncCallback,
                                     Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "data", Me.m_ApiKey), Nothing, Callback, IgnoreCache, Me.CacheConfig.GetBaseDataCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetBaseDataEnd(ByVal AsyncResult As IAsyncResult,
                                   <Out()> ByRef Key As Object,
                                   <Out()> ByRef Result As Sc2RanksGetBaseDataResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetBaseDataResult)(AsyncResult, Key, Result)
    End Function

#End Region

#End Region

#Region "API Characters"

#Region "Function GetCharacter"

    ''' <summary>
    ''' Returns a single character. 
    ''' </summary>
    ''' <param name="Region">The region of the character.</param>
    ''' <param name="BattleNetID">The Battle.net identifier.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacter(ByVal Region As eSc2RanksRegion,
                                 ByVal BattleNetID As Int32,
                                 <Out()> ByRef Result As Sc2RanksGetCharacterResult,
                                 Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCharacterResult)(eRequestMethod.Get, String.Format(BaseUrlFormat, String.Format("characters/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), Nothing, Me.CacheConfig.GetCharacterCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns a single character. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="Region">The region of the character.</param>
    ''' <param name="BattleNetID">The Battle.net identifier.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterBegin(ByVal Key As Object,
                                      ByVal Region As eSc2RanksRegion,
                                      ByVal BattleNetID As Int32,
                                      ByVal Callback As AsyncCallback,
                                      Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, String.Format("characters/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), Nothing, Callback, IgnoreCache, Me.CacheConfig.GetCharacterCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterEnd(ByVal AsyncResult As IAsyncResult,
                                    <Out()> ByRef Key As Object,
                                    <Out()> ByRef Result As Sc2RanksGetCharacterResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCharacterResult)(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetCharacterTeamsList"

    ''' <summary>
    ''' Returns all of the teams the character is on, as well as the characters info. Does not return other characters on the team. 
    ''' </summary>
    ''' <param name="Region">The region of the character.</param>
    ''' <param name="BattleNetID">The Battle.net identifier.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamsList(ByVal Region As eSc2RanksRegion,
                                          ByVal BattleNetID As Int32,
                                          ByVal Expansion As eSc2RanksExpansion,
                                          ByVal Bracket As eSc2RanksBracket,
                                          ByVal League As eSc2RanksLeague,
                                          <Out()> ByRef Result As Sc2RanksGetCharacterTeamsListResult,
                                          Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                          Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("expansion={0}&bracket={1}&league={2}", Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League))
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCharacterTeamsListResult, Sc2RanksTeamExtended, IList(Of Sc2RanksTeamExtended))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetCharacterTeamsListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns all of the teams the character is on, as well as the characters info. Does not return other characters on the team. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="Region">The region of the character.</param>
    ''' <param name="BattleNetID">The Battle.net identifier.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamsListBegin(ByVal Key As Object,
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("characters/teams/{0}/{1}", Enums.RegionBuffer.GetValue(Region), BattleNetID.ToString()), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetCharacterTeamsListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamsListEnd(ByVal AsyncResult As IAsyncResult,
                                             <Out()> ByRef Key As Object,
                                             <Out()> ByRef Result As Sc2RanksGetCharacterTeamsListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCharacterTeamsListResult, Sc2RanksTeamExtended, IList(Of Sc2RanksTeamExtended))(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function SearchCharacterTeamList"

    ''' <summary>
    ''' Returns all the teams with a team member whos name matches the search. 
    ''' </summary>
    ''' <param name="Name">The name to search for.</param>
    ''' <param name="Match">The type of matching to be done.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function SearchCharacterTeamList(ByVal Name As String,
                                            ByVal Match As eSc2RanksMatchType,
                                            ByVal RankRegion As eSc2RanksRankRegion,
                                            ByVal Expansion As eSc2RanksExpansion,
                                            ByVal Bracket As eSc2RanksBracket,
                                            ByVal League As eSc2RanksLeague,
                                            <Out()> ByRef Result As Sc2RanksSearchCharacterTeamListResult,
                                            Optional Race As Nullable(Of eSc2RanksRace) = Nothing,
                                            Optional ByVal Limit As Int32 = MaxRequestLimit,
                                            Optional ByVal Page As Int32 = 1,
                                            Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksSearchCharacterTeamListResult, Sc2RanksTeamExtended, IList(Of Sc2RanksTeamExtended))(eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.SearchCharacterTeamListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns all the teams with a team member whos name matches the search. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="Name">The name to search for.</param>
    ''' <param name="Match">The type of matching to be done.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
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

      Call RequestData.AppendFormat("name={0}&match={1}&rank_region={2}&expansion={3}&bracket={4}&league={5}&page={6}&limit={7}", Name, Enums.MatchTypeBuffer.GetValue(Match), Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Page.ToString(), Limit.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "characters/search", Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.SearchCharacterTeamListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function SearchCharacterTeamListEnd(ByVal AsyncResult As IAsyncResult,
                                               <Out()> ByRef Key As Object,
                                               <Out()> ByRef Result As Sc2RanksSearchCharacterTeamListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksSearchCharacterTeamListResult, Sc2RanksTeamExtended, IList(Of Sc2RanksTeamExtended))(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetCharacterList"

    ''' <summary>
    ''' Accepts an array of characters with region and bnet_id, up to 200 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. 
    ''' </summary>
    ''' <param name="Characters">A list of character information to get more information on.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterList(ByVal Characters As IList(Of Sc2RanksCharacterSimple),
                                     <Out()> ByRef Result As Sc2RanksGetCharacterListResult,
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCharacterListResult, Sc2RanksCharacterExtended, IList(Of Sc2RanksCharacterExtended))(eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetCharacterListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Accepts an array of characters with region and bnet_id, up to 200 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="Characters">A list of character information to get more information on.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterListBegin(ByVal Key As Object,
                                          ByVal Characters As IList(Of Sc2RanksCharacterSimple),
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/characters", Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetCharacterListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterListEnd(ByVal AsyncResult As IAsyncResult,
                                        <Out()> ByRef Key As Object,
                                        <Out()> ByRef Result As Sc2RanksGetCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCharacterListResult, Sc2RanksCharacterExtended, IList(Of Sc2RanksCharacterExtended))(AsyncResult, Key, Result)
    End Function

#End Region

#End Region

#Region "API Teams"

#Region "Function GetCharacterTeamList"

    ''' <summary>
    ''' Accepts an array of characters with *region* and *bnet_id*, up to 50 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. Returns all of the teams that match the given team filters for the characters passed. Does not return team characters. 
    ''' </summary>
    ''' <param name="Characters">A list of character information to get more information on.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamList(ByVal Characters As IList(Of Sc2RanksCharacterSimple),
                                         ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Expansion As eSc2RanksExpansion,
                                         ByVal Bracket As eSc2RanksBracket,
                                         ByVal League As eSc2RanksLeague,
                                         <Out()> ByRef Result As Sc2RanksGetCharacterTeamListResult,
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCharacterTeamListResult, Sc2RanksCharacterExtendedWithTeams, IList(Of Sc2RanksCharacterExtendedWithTeams))(eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/teams", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetCharacterTeamListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Accepts an array of characters with *region* and *bnet_id*, up to 50 characters at once. If one of the passed characters is invalid (region/bnet id/not found) then the bnet_id/region are returned with the error. Returns all of the teams that match the given team filters for the characters passed. Does not return team characters. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="Characters">A list of character information to get more information on.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamListBegin(ByVal Key As Object,
                                              ByVal Characters As IList(Of Sc2RanksCharacterSimple),
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "bulk/teams", Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetCharacterTeamListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterTeamListEnd(ByVal AsyncResult As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Result As Sc2RanksGetCharacterTeamListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCharacterTeamListResult, Sc2RanksCharacterExtendedWithTeams, IList(Of Sc2RanksCharacterExtendedWithTeams))(AsyncResult, Key, Result)
    End Function

#End Region

#End Region

#Region "API Clans"

#Region "Function GetClan"

    ''' <summary>
    ''' Returns base clan data with the Clans scores based on the bracket.
    ''' </summary>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Tag">The tag of the clan.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetClan(ByVal RankRegion As eSc2RanksRankRegion,
                            ByVal Tag As String,
                            <Out()> ByRef Result As Sc2RanksGetClanResult,
                            Optional ByVal Bracket As Nullable(Of eSc2RanksBracket) = Nothing,
                            Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      If (Bracket.HasValue) Then Call RequestData.AppendFormat("bracket={0}", Enums.BracketBuffer.GetValue(Bracket.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetClanResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetClanCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns base clan data with the Clans scores based on the bracket.
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Tag">The tag of the clan.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetClanBegin(ByVal Key As Object,
                                 ByVal RankRegion As eSc2RanksRankRegion,
                                 ByVal Tag As String,
                                 ByVal Callback As AsyncCallback,
                                 Optional ByVal Bracket As Nullable(Of eSc2RanksBracket) = Nothing,
                                 Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Dim RequestData As New StringBuilder

      If (Bracket.HasValue) Then Call RequestData.AppendFormat("bracket={0}", Enums.BracketBuffer.GetValue(Bracket.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetClanCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetClanEnd(ByVal AsyncResult As IAsyncResult,
                               <Out()> ByRef Key As Object,
                               <Out()> ByRef Result As Sc2RanksGetClanResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetClanResult)(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetClanCharacterList"

    ''' <summary>
    ''' Returns the characters who are part of the clan, up to 50 per request or less if limit is passed. 
    ''' </summary>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Tag">The tag of the clan.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetClanCharacterList(ByVal RankRegion As eSc2RanksRankRegion,
                                         ByVal Tag As String,
                                         <Out()> ByRef Result As Sc2RanksGetClanCharacterListResult,
                                         Optional ByVal Bracket As Nullable(Of eSc2RanksBracket) = Nothing,
                                         Optional ByVal Limit As Int32 = MaxRequestLimit,
                                         Optional ByVal Page As Int32 = 1,
                                         Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("limit={0}&page={1}", Limit.ToString(), Page.ToString())
      If (Bracket.HasValue) Then Call RequestData.AppendFormat("&bracket={0}", Enums.BracketBuffer.GetValue(Bracket.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetClanCharacterListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/characters/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetClanCharacterListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the characters who are part of the clan, up to 50 per request or less if limit is passed. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Tag">The tag of the clan.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/characters/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetClanCharacterListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetClanCharacterListEnd(ByVal AsyncResult As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Result As Sc2RanksGetClanCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetClanCharacterListResult)(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetClanTeamList"

    ''' <summary>
    ''' Returns the teams who are part of the clan, up to 50 per request or less if limit is passed. 
    ''' </summary>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Tag">The tag of the clan.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetClanTeamList(ByVal RankRegion As eSc2RanksRankRegion,
                                    ByVal Tag As String,
                                    ByVal Expansion As eSc2RanksExpansion,
                                    ByVal Bracket As eSc2RanksBracket,
                                    ByVal League As eSc2RanksLeague,
                                    <Out()> ByRef Result As Sc2RanksGetClanTeamListResult,
                                    Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                    Optional ByVal Limit As Int32 = MaxRequestLimit,
                                    Optional ByVal Page As Int32 = 1,
                                    Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("tag={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Tag, Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then Call RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetClanTeamListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/teams/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetClanTeamListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the teams who are part of the clan, up to 50 per request or less if limit is passed. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Tag">The tag of the clan.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("clans/teams/{0}/{1}", Enums.RankRegionBuffer.GetValue(RankRegion), Tag), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetClanTeamListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetClanTeamListEnd(ByVal AsyncResult As IAsyncResult,
                                       <Out()> ByRef Key As Object,
                                       <Out()> ByRef Result As Sc2RanksGetClanTeamListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetClanTeamListResult)(AsyncResult, Key, Result)
    End Function

#End Region

#End Region

#Region "API Rankings"

#Region "Function GetRankingsTop"

    ''' <summary>
    ''' Returns the top limit teams given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="TopCount">The top amount of results to return. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetRankingsTop(ByVal RankRegion As eSc2RanksRankRegion,
                                   ByVal Expansion As eSc2RanksExpansion,
                                   ByVal Bracket As eSc2RanksBracket,
                                   ByVal League As eSc2RanksLeague,
                                   ByVal TopCount As Int32,
                                   <Out()> ByRef Result As Sc2RanksGetRankingsTopResult,
                                   Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                   Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetRankingsTopResult, Sc2RanksTeamExtended, IList(Of Sc2RanksTeamExtended))(eRequestMethod.Post, String.Format(BaseUrlFormat, "rankings", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetRankingsTopCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the top limit teams given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="TopCount">The top amount of results to return. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
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

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "rankings", Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetRankingsTopCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetRankingsTopEnd(ByVal AsyncResult As IAsyncResult,
                                      <Out()> ByRef Key As Object,
                                      <Out()> ByRef Result As Sc2RanksGetRankingsTopResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetRankingsTopResult, Sc2RanksTeamExtended, IList(Of Sc2RanksTeamExtended))(AsyncResult, Key, Result)
    End Function

#End Region

#End Region

#Region "API Divisions"

#Region "Function GetDivisionsTop"

    ''' <summary>
    ''' Returns the top limit divisions given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="TopCount">The top amount of results to return. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDivisionsTop(ByVal RankRegion As eSc2RanksRankRegion,
                                    ByVal Expansion As eSc2RanksExpansion,
                                    ByVal Bracket As eSc2RanksBracket,
                                    ByVal League As eSc2RanksLeague,
                                    ByVal TopCount As Int32,
                                    <Out()> ByRef Result As Sc2RanksGetDivisionsTopResult,
                                    Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                    Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetDivisionsTopResult, Sc2RanksLeagueDivision, IList(Of Sc2RanksLeagueDivision))(eRequestMethod.Post, String.Format(BaseUrlFormat, "divisions", Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetDivisionsTopCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the top limit divisions given the passed params. Unlike other APIs, this cannot be paginated and is intended to be used for showing mini ranking type of widgets and not for data collection. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="TopCount">The top amount of results to return. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
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

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), TopCount.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, "divisions", Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetDivisionsTopCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDivisionsTopEnd(ByVal AsyncResult As IAsyncResult,
                                       <Out()> ByRef Key As Object,
                                       <Out()> ByRef Result As Sc2RanksGetDivisionsTopResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetDivisionsTopResult, Sc2RanksLeagueDivision, IList(Of Sc2RanksLeagueDivision))(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetDivision"

    ''' <summary>
    ''' Returns base information about the division.
    ''' </summary>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDivision(ByVal DivisionID As String,
                                <Out()> ByRef Result As Sc2RanksGetDivisionResult,
                                Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetDivisionResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.GetDivisionCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns base information about the division.
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetDivisionBegin(ByVal Key As Object,
                                     ByVal DivisionID As String,
                                     ByVal Callback As AsyncCallback,
                                     Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                     Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Callback, IgnoreCache, Me.CacheConfig.GetDivisionCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDivisionEnd(ByVal AsyncResult As IAsyncResult,
                                   <Out()> ByRef Key As Object,
                                   <Out()> ByRef Result As Sc2RanksGetDivisionResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetDivisionResult)(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetDivisionTeamsTop"

    ''' <summary>
    ''' Returns the top limit teams in the division. This cannot be paginated and is intended to be used for showing mini ranking type of widgets and not data collection.
    ''' </summary>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDivisionTeamsTop(ByVal DivisionID As String,
                                        <Out()> ByRef Result As Sc2RanksGetDivisionTeamsTopResult,
                                        Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetDivisionTeamsTopResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/teams/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.GetDivisionTeamsTopCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the top limit teams in the division. This cannot be paginated and is intended to be used for showing mini ranking type of widgets and not data collection.
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetDivisionTeamsTopBegin(ByVal Key As Object,
                                             ByVal DivisionID As String,
                                             ByVal Callback As AsyncCallback,
                                             Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                             Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("divisions/teams/{0}", DivisionID), Me.m_ApiKey), Nothing, Callback, IgnoreCache, Me.CacheConfig.GetDivisionTeamsTopCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDivisionTeamsTopEnd(ByVal AsyncResult As IAsyncResult,
                                           <Out()> ByRef Key As Object,
                                           <Out()> ByRef Result As Sc2RanksGetDivisionTeamsTopResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetDivisionTeamsTopResult)(AsyncResult, Key, Result)
    End Function

#End Region

#End Region

#Region "API Custom Divisions"

#Region "Function GetCustomDivisions"

    ''' <summary>
    ''' Returns all of the custom divisions attached to the user account of the API Key. 
    ''' </summary>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisions(<Out()> ByRef Result As Sc2RanksGetCustomDivisionsResult,
                                       Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCustomDivisionsResult, Sc2RanksCustomDivision, IList(Of Sc2RanksCustomDivision))(eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, Me.CacheConfig.GetCustomDivisionsCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns all of the custom divisions attached to the user account of the API Key. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionsBegin(ByVal Key As Object,
                                            ByVal Callback As AsyncCallback,
                                            Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Get, String.Format(BaseUrlFormat, "custom-divisions", Me.m_ApiKey), Nothing, Callback, IgnoreCache, Me.CacheConfig.GetCustomDivisionsCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionsEnd(ByVal AsyncResult As IAsyncResult,
                                          <Out()> ByRef Key As Object,
                                          <Out()> ByRef Result As Sc2RanksGetCustomDivisionsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCustomDivisionsResult, Sc2RanksCustomDivision, IList(Of Sc2RanksCustomDivision))(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetCustomDivision"

    ''' <summary>
    ''' Returns base information of the custom division by the given id. 
    ''' </summary>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivision(ByVal DivisionID As String,
                                      <Out()> ByRef Result As Sc2RanksGetCustomDivisionResult,
                                      Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCustomDivisionResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Me.CacheConfig.GetCustomDivisionCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns base information of the custom division by the given id. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionBegin(ByVal Key As Object,
                                           ByVal DivisionID As String,
                                           ByVal Callback As AsyncCallback,
                                           Optional ByVal IgnoreCache As Boolean = False) As IAsyncResult
      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/{0}", DivisionID), Me.m_ApiKey), Nothing, Callback, IgnoreCache, Me.CacheConfig.GetCustomDivisionCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionEnd(ByVal AsyncResult As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Result As Sc2RanksGetCustomDivisionResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCustomDivisionResult)(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetCustomDivisionTeamList"

    ''' <summary>
    ''' Returns the teams in the custom division filtered by the passed params. 
    ''' </summary>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionTeamList(ByVal DivisionID As String,
                                              ByVal RankRegion As eSc2RanksRankRegion,
                                              ByVal Expansion As eSc2RanksExpansion,
                                              ByVal Bracket As eSc2RanksBracket,
                                              ByVal League As eSc2RanksLeague,
                                              <Out()> ByRef Result As Sc2RanksGetCustomDivisionTeamListResult,
                                              Optional ByVal Race As Nullable(Of eSc2RanksRace) = Nothing,
                                              Optional ByVal Limit As Int32 = MaxRequestLimit,
                                              Optional ByVal Page As Int32 = 1,
                                              Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCustomDivisionTeamListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetCustomDivisionTeamListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the teams in the custom division filtered by the passed params. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="RankRegion">The rank region the character plays in.</param>
    ''' <param name="Expansion"> The expansion set of StarCraft II.</param>
    ''' <param name="Bracket">The bracket to filter.</param>
    ''' <param name="League">The league to filter.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Race">Optional. Default is all races. Race to filter.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
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

      Call RequestData.AppendFormat("rank_region={0}&expansion={1}&bracket={2}&league={3}&limit={4}&page={5}", Enums.RankRegionBuffer.GetValue(RankRegion), Enums.ExpansionBuffer.GetValue(Expansion), Enums.BracketBuffer.GetValue(Bracket), Enums.LeagueBuffer.GetValue(League), Limit.ToString(), Page.ToString())
      If (Race.HasValue) Then RequestData.AppendFormat("&race={0}", Enums.RacesBuffer.GetValue(Race.Value))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/teams/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetCustomDivisionTeamListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionTeamListEnd(ByVal AsyncResult As IAsyncResult,
                                                 <Out()> ByRef Key As Object,
                                                 <Out()> ByRef Result As Sc2RanksGetCustomDivisionTeamListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCustomDivisionTeamListResult)(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function GetCustomDivisionCharacterList"

    ''' <summary>
    ''' Returns the characters in the custom division without any team data, filtered by the passed params. 
    ''' </summary>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Region">The region of the character.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionCharacterList(ByVal DivisionID As String,
                                                   ByVal Region As eSc2RanksRegion,
                                                   <Out()> ByRef Result As Sc2RanksGetCustomDivisionCharacterListResult,
                                                   Optional ByVal Limit As Int32 = MaxRequestLimit,
                                                   Optional ByVal Page As Int32 = 1,
                                                   Optional ByVal IgnoreCache As Boolean = False) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("region={0}&limit={1}&page={2}", Enums.RegionBuffer.GetValue(Region), Limit.ToString(), Page.ToString())

      Result = Me.Query.QueryAndParse(Of Sc2RanksGetCustomDivisionCharacterListResult)(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.GetCustomDivisionCharacterListCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Returns the characters in the custom division without any team data, filtered by the passed params. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Region">The region of the character.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="Limit">The maximum limit of results returned. Cannot exceed <c>MaximumRequestLimit</c>.</param>
    ''' <param name="Page">The page of data to return based on the <c>Limit</c>.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/characters/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.GetCustomDivisionCharacterListCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivisionCharacterListEnd(ByVal AsyncResult As IAsyncResult,
                                                      <Out()> ByRef Key As Object,
                                                      <Out()> ByRef Result As Sc2RanksGetCustomDivisionCharacterListResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksGetCustomDivisionCharacterListResult)(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function CustomDivisionAdd"

    ''' <summary>
    ''' Adds characters to the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, added or already added. 
    ''' </summary>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Characters">A list of character information to add.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionAdd(ByVal DivisionID As String,
                                      ByVal Characters As IList(Of Sc2RanksCharacterSimple),
                                      <Out()> ByRef Result As Sc2RanksCustomDivisionAddResult,
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionAddResult, Sc2RanksCustomDivisionManagementStatus, IList(Of Sc2RanksCustomDivisionManagementStatus))(eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.CustomDivisionAddCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Adds characters to the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, added or already added. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Characters">A list of character information to add.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionAddBegin(ByVal Key As Object,
                                           ByVal DivisionID As String,
                                           ByVal Characters As IList(Of Sc2RanksCharacterSimple),
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.CustomDivisionAddCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionAddEnd(ByVal AsyncResult As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Result As Sc2RanksCustomDivisionAddResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionAddResult, Sc2RanksCustomDivisionManagementStatus, IList(Of Sc2RanksCustomDivisionManagementStatus))(AsyncResult, Key, Result)
    End Function

#End Region

#Region "Function CustomDivisionRemove"

    ''' <summary>
    ''' Removes characters from the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, removed, not added or unknown. 
    ''' </summary>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Characters">A list of character information to remove.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionRemove(ByVal DivisionID As String,
                                         ByVal Characters As IList(Of Sc2RanksCharacterSimple),
                                         <Out()> ByRef Result As Sc2RanksCustomDivisionRemoveResult,
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

      Result = Me.Query.QueryAndParse(Of Sc2RanksCustomDivisionRemoveResult, Sc2RanksCustomDivisionManagementStatus, IList(Of Sc2RanksCustomDivisionManagementStatus))(eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Me.CacheConfig.CustomDivisionRemoveCacheDuration, IgnoreCache, Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Removes characters from the custom division, up to 200 at once. Returns the status of each ID passed, whether invalid, removed, not added or unknown. 
    ''' </summary>
    ''' <param name="Key">Can contain anything. Useful for tracking asynchronous calls. Is returned when the End method is called.</param>
    ''' <param name="DivisionID">The division identifier.</param>
    ''' <param name="Characters">A list of character information to remove.</param>
    ''' <param name="Callback">Address of a method to call when a result is available.</param>
    ''' <param name="IgnoreCache">Optional. Default is <c>False</c>. Ignores any cached data that might be available when caching is enabled.</param>
    ''' <returns>Returns the status of the asynchronous operation.</returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionRemoveBegin(ByVal Key As Object,
                                              ByVal DivisionID As String,
                                              ByVal Characters As IList(Of Sc2RanksCharacterSimple),
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

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Delete, String.Format(BaseUrlFormat, String.Format("custom-divisions/manage/{0}", DivisionID), Me.m_ApiKey), RequestData.ToString(), Callback, IgnoreCache, Me.CacheConfig.CustomDivisionRemoveCacheDuration)
    End Function

    ''' <summary>
    ''' End method to call when the callback was invoked.
    ''' </summary>
    ''' <param name="AsyncResult">The <c>IAsyncResult</c>.</param>
    ''' <param name="Key">Contains the key used in Begin.</param>
    ''' <param name="Result">Contains the result. Is <c>Nothing</c> if no data was received and the query timed out.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function CustomDivisionRemoveEnd(ByVal AsyncResult As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Result As Sc2RanksCustomDivisionRemoveResult) As Exception
      Return Me.Query.QueryAndParseEnd(Of Sc2RanksCustomDivisionRemoveResult, Sc2RanksCustomDivisionManagementStatus, IList(Of Sc2RanksCustomDivisionManagementStatus))(AsyncResult, Key, Result)
    End Function

#End Region

#End Region

#Region "Sub Clear Cache"

    ''' <summary>
    ''' Create the cache.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearCache()
      Call Me.Query.ClearCache()
    End Sub

#End Region

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