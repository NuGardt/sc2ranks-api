﻿' NuGardt SC2Ranks API
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
Imports com.NuGardt.SC2Ranks.API.Messages
Imports System.Collections.Generic
Imports System.Net
Imports System.Threading
Imports com.NuGardt.SC2Ranks.Helper
Imports System.Text
Imports System.Runtime.Serialization.Json

Namespace SC2Ranks.API
''' <summary>
'''   Class containing all API calls to SC2Ranks.
''' </summary>
''' <remarks></remarks>
  Public Class Sc2RanksService
    Implements IDisposable

    Private ReadOnly UseRequestCache As Boolean
    Private ReadOnly Cache As RequestCache
    Private ReadOnly CacheStream As Stream
    Private ReadOnly m_AppKey As String
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <param name="AppKey"></param>
    ''' <remarks></remarks>
    Private Sub New(ByVal AppKey As String,
                    ByVal Cache As RequestCache,
                    ByVal CacheStream As Stream)
      Me.m_AppKey = AppKey

      If (Cache IsNot Nothing) Then
        Me.UseRequestCache = True
        Me.Cache = Cache
        Me.CacheStream = CacheStream
      Else
        Me.UseRequestCache = False
        Me.Cache = Nothing
        Me.CacheStream = Nothing
      End If
    End Sub

#Region "Function CreateInstance"
    
    ''' <summary>
    '''   Create an instance of the SC2Ranks Service.
    ''' </summary>
    ''' <param name="AppKey">Required by SC2Ranks.com.</param>
    ''' <param name="Instance">
    '''   Contains the instance if Ex is <c>Nothing</c>.
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateInstance(ByVal AppKey As String,
                                          ByVal CacheStream As Stream,
                                          <Out()> ByRef Instance As Sc2RanksService) As Exception
      Instance = Nothing
      Dim Ex As Exception = Nothing

      If String.IsNullOrEmpty(AppKey) Then
        Ex = New ArgumentNullException("AppKey")
      ElseIf (AppKey.Length >= 32766) Then
        'Silly check but never know^^
        Ex = New FormatException("AppKey too long.")
      Else
        Dim Cache As RequestCache

        If (CacheStream IsNot Nothing) Then
          Cache = New RequestCache()
          Ex = Cache.Read(CacheStream)
        Else
          Cache = Nothing
        End If

        If (Ex Is Nothing) Then Instance = New Sc2RanksService(Uri.EscapeUriString(AppKey), Cache, CacheStream)
      End If

      Return Ex
    End Function

#End Region

#Region "Function SearchBasePlayer"
    Private Shared ReadOnly SearchBasePlayerCacheDuration As TimeSpan = TimeSpan.FromMinutes(30)
    
    ''' <summary>
    '''   Allows you to perform small searches, useful if you want to hookup an IRC bot or such. Only returns the first 10 names, but you can see the total number of characters and pass an offset if you need more. Search is case-insensitive.
    ''' </summary>
    ''' <param name="SearchType">The type of the search.</param>
    ''' <param name="Region">The region to search in.</param>
    ''' <param name="CharacterName">The fulll or partial name of the character depending on search type.</param>
    ''' <param name="ResultOffset">The offset of the result.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SearchBasePlayer(ByVal SearchType As eSearchType,
                                     ByVal Region As eRegion,
                                     ByVal CharacterName As String,
                                     ByVal ResultOffset As Nullable(Of Int32),
                                     <Out()> ByRef Result As Sc2RanksResult(Of SearchResult)) As Exception
      Dim Ex As Exception = Nothing

      If ResultOffset.HasValue Then
        Result = QueryAndParse(Of SearchResult)(String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}/{3}.json?appKey={4}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, ResultOffset.Value.ToString, Me.m_AppKey), Nothing, SearchBasePlayerCacheDuration, Ex)
      Else
        Result = QueryAndParse(Of SearchResult)(String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}.json?appKey={3}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, Me.m_AppKey), Nothing, SearchBasePlayerCacheDuration, Ex)
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
        Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}/{3}.json?appKey={4}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, ResultOffset.Value.ToString, Me.m_AppKey), Nothing, SearchBasePlayerCacheDuration, Callback)
      Else
        Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/search/{0}/{1}/{2}.json?appKey={3}", Enums.SearchTypeBuffer.GetValue(SearchType), Enums.RegionBuffer.GetValue(Region), CharacterName, Me.m_AppKey), Nothing, SearchBasePlayerCacheDuration, Callback)
      End If
    End Function

    Public Function SearchBasePlayerEnd(ByVal Result As IAsyncResult,
                                        <Out()> ByRef Key As Object,
                                        <Out()> ByRef Response As Sc2RanksResult(Of SearchResult)) As Exception
      Return QueryAndParseEnd(Of SearchResult)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetBasePlayerByCharacterCode"
    Private Shared ReadOnly GetBasePlayerByCharacterCodeCacheDuration As TimeSpan = TimeSpan.FromHours(1)
    
    ''' <summary>
    '''   Minimum amount of character data, just gives achievement points, character code and battle.net id info.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="CharacterCode"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetBasePlayerByCharacterCode(ByVal Region As eRegion,
                                                 ByVal CharacterName As String,
                                                 ByVal CharacterCode As Integer,
                                                 <Out()> ByRef Result As Sc2RanksResult(Of PlayerBase)) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of PlayerBase)(String.Format("http://sc2ranks.com/api/base/char/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Nothing, GetBasePlayerByCharacterCodeCacheDuration, Ex)

      Return Ex
    End Function

    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetBasePlayerByCharacterCodeBegin(ByVal Key As Object,
                                                      ByVal Region As eRegion,
                                                      ByVal CharacterName As String,
                                                      ByVal CharacterCode As Integer,
                                                      ByVal Callback As AsyncCallback) As IAsyncResult
      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/char/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Nothing, GetBasePlayerByCharacterCodeCacheDuration, Callback)
    End Function

    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetBasePlayerByCharacterCodeEnd(ByVal Result As IAsyncResult,
                                                    <Out()> ByRef Key As Object,
                                                    <Out()> ByRef Response As Sc2RanksResult(Of PlayerBase)) As Exception
      Return QueryAndParseEnd(Of PlayerBase)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetBasePlayerByBattleNetID"
    Private Shared ReadOnly GetBasePlayerByBattleNetIDCacheDuration As TimeSpan = TimeSpan.FromHours(1)
    
    ''' <summary>
    '''   Minimum amount of character data, just gives achievement points, character code and battle.net id info.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBasePlayerByBattleNetID(ByVal Region As eRegion,
                                               ByVal CharacterName As String,
                                               ByVal BattleNetID As Int32,
                                               <Out()> ByRef Result As Sc2RanksResult(Of PlayerBase)) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of PlayerBase)(String.Format("http://sc2ranks.com/api/base/char/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Nothing, GetBasePlayerByBattleNetIDCacheDuration, Ex)

      Return Ex
    End Function

    Public Function GetBasePlayerByBattleNetIDBegin(ByVal Key As Object,
                                                    ByVal Region As eRegion,
                                                    ByVal CharacterName As String,
                                                    ByVal BattleNetID As Int32,
                                                    ByVal Callback As AsyncCallback) As IAsyncResult
      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/char/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Nothing, GetBasePlayerByBattleNetIDCacheDuration, Callback)
    End Function

    Public Function GetBasePlayerByBattleNetIDEnd(ByVal Result As IAsyncResult,
                                                  <Out()> ByRef Key As Object,
                                                  <Out()> ByRef Response As Sc2RanksResult(Of PlayerBase)) As Exception
      Return QueryAndParseEnd(Of PlayerBase)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetBaseTeamByCharacterCode"
    Private Shared ReadOnly GetBaseTeamByCharacterCodeCacheDuration As TimeSpan = TimeSpan.FromHours(3)
    
    ''' <summary>
    '''   Includes base character data, as well as base data on all of the players teams.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="CharacterCode"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetBaseTeamByCharacterCode(ByVal Region As eRegion,
                                               ByVal CharacterName As String,
                                               ByVal CharacterCode As Integer,
                                               <Out()> ByRef Result As Sc2RanksResult(Of PlayerExtended)) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of PlayerExtended)(String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Nothing, GetBaseTeamByCharacterCodeCacheDuration, Ex)

      Return Ex
    End Function

    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetBaseTeamByCharacterCodeBegin(ByVal Key As Object,
                                                    ByVal Region As eRegion,
                                                    ByVal CharacterName As String,
                                                    ByVal CharacterCode As Integer,
                                                    ByVal Callback As AsyncCallback) As IAsyncResult
      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}${2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Me.m_AppKey), Nothing, GetBaseTeamByCharacterCodeCacheDuration, Callback)
    End Function

    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetBaseTeamByCharacterCodeEnd(ByVal Result As IAsyncResult,
                                                  <Out()> ByRef Key As Object,
                                                  <Out()> ByRef Response As Sc2RanksResult(Of PlayerExtended)) As Exception
      Return QueryAndParseEnd(Of PlayerExtended)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetBaseTeamByBattleNetID"
    Private Shared ReadOnly GetBaseTeamByBattleNetIDCacheDuration As TimeSpan = TimeSpan.FromHours(3)
    
    ''' <summary>
    '''   Includes base character data, as well as base data on all of the players teams.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBaseTeamByBattleNetID(ByVal Region As eRegion,
                                             ByVal CharacterName As String,
                                             ByVal BattleNetID As Int32,
                                             <Out()> ByRef Result As Sc2RanksResult(Of PlayerExtended)) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of PlayerExtended)(String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Nothing, GetBaseTeamByBattleNetIDCacheDuration, Ex)

      Return Ex
    End Function

    Public Function GetBaseTeamByBattleNetIDBegin(ByVal Key As Object,
                                                  ByVal Region As eRegion,
                                                  ByVal CharacterName As String,
                                                  ByVal BattleNetID As Int32,
                                                  ByVal Callback As AsyncCallback) As IAsyncResult
      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/base/teams/{0}/{1}!{2}.json?appKey={3}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Me.m_AppKey), Nothing, GetBaseTeamByBattleNetIDCacheDuration, Callback)
    End Function

    Public Function GetBaseTeamByBattleNetIDEnd(ByVal Result As IAsyncResult,
                                                <Out()> ByRef Key As Object,
                                                <Out()> ByRef Response As Sc2RanksResult(Of PlayerExtended)) As Exception
      Return QueryAndParseEnd(Of PlayerExtended)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetTeamByCharacterCode"
    Private Shared ReadOnly GetTeamByCharacterCodeCacheDuration As TimeSpan = TimeSpan.FromHours(6)
    
    ''' <summary>
    '''   Includes base character data, and extended team information for the passed bracket.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="CharacterCode"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetTeamByCharacterCode(ByVal Region As eRegion,
                                           ByVal CharacterName As String,
                                           ByVal CharacterCode As Integer,
                                           ByVal Bracket As eBracket,
                                           <Out()> ByRef Result As Sc2RanksResult(Of PlayerExtended)) As Exception
      Dim Ex As Exception = Nothing
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Result = QueryAndParse(Of PlayerExtended)(String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}${2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Nothing, GetTeamByCharacterCodeCacheDuration, Ex)

      Return Ex
    End Function

    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetTeamByCharacterCodeBegin(ByVal Key As Object,
                                                ByVal Region As eRegion,
                                                ByVal CharacterName As String,
                                                ByVal CharacterCode As Integer,
                                                ByVal Bracket As eBracket,
                                                ByVal Callback As AsyncCallback) As IAsyncResult
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}${2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, CharacterCode, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Nothing, GetTeamByCharacterCodeCacheDuration, Callback)
    End Function

    <Obsolete("Not reliable when searching with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Function GetTeamByCharacterCodeEnd(ByVal Result As IAsyncResult,
                                              <Out()> ByRef Key As Object,
                                              <Out()> ByRef Response As Sc2RanksResult(Of PlayerExtended)) As Exception
      Return QueryAndParseEnd(Of PlayerExtended)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetTeamByBattleNetID"
    Private Shared ReadOnly GetTeamByBattleNetIDCacheDuration As TimeSpan = TimeSpan.FromHours(6)
    
    ''' <summary>
    '''   Includes base character data, and extended team information for the passed bracket.
    ''' </summary>
    ''' <param name="Region"></param>
    ''' <param name="CharacterName"></param>
    ''' <param name="BattleNetID"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTeamByBattleNetID(ByVal Region As eRegion,
                                         ByVal CharacterName As String,
                                         ByVal BattleNetID As Int32,
                                         ByVal Bracket As eBracket,
                                         <Out()> ByRef Result As Sc2RanksResult(Of PlayerExtended)) As Exception
      Dim Ex As Exception = Nothing
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Result = QueryAndParse(Of PlayerExtended)(String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}!{2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Nothing, GetTeamByBattleNetIDCacheDuration, Ex)

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
      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/char/teams/{0}/{1}!{2}/{3}/{4}.json?appKey={5}", Enums.RegionBuffer.GetValue(Region), CharacterName, BattleNetID, Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Nothing, GetTeamByBattleNetIDCacheDuration, Callback)
    End Function

    Public Function GetTeamByBattleNetIDEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksResult(Of PlayerExtended)) As Exception
      Return QueryAndParseEnd(Of PlayerExtended)(Result, Key, Response)
    End Function

#End Region

#Region "Function GetCustomDivision"
    Private Shared ReadOnly GetCustomDivisionCacheDuration As TimeSpan = TimeSpan.FromHours(12)
    
    ''' <summary>
    '''   Allows you to get everyone in a custom division.
    ''' </summary>
    ''' <param name="CustomDivisionID"></param>
    ''' <param name="Region"></param>
    ''' <param name="League"></param>
    ''' <param name="Bracket"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomDivision(ByVal CustomDivisionID As Integer,
                                      ByVal Region As Nullable(Of eRegion),
                                      ByVal League As Nullable(Of eLeague),
                                      ByVal Bracket As eBracket,
                                      <Out()> ByRef Result As Sc2RanksResult(Of Division())) As Exception
      Dim Ex As Exception = Nothing
      Dim IsRandom As Boolean = IsRandomBracket(Bracket)

      If (Bracket And eBracket.Random) = eBracket.Random Then Bracket = CType(Bracket - eBracket.Random, eBracket)

      Result = QueryAndParse(Of Division())(String.Format("http://sc2ranks.com/api/clist/{0}/{1}/{2}/{3}/{4}.json?appKey={5}", CustomDivisionID, If(Region.HasValue, Enums.RegionBuffer.GetValue(Region.Value), "all"), If(League.HasValue, Enums.LeaguesBuffer.GetValue(League.Value), "all"), Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Nothing, GetCustomDivisionCacheDuration, Ex)

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

      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/clist/{0}/{1}/{2}/{3}/{4}.json?appKey={5}", CustomDivisionID, If(Region.HasValue, Enums.RegionBuffer.GetValue(Region.Value), "all"), If(League.HasValue, Enums.LeaguesBuffer.GetValue(League.Value), "all"), Enums.BracketBuffer.GetValue(Bracket), If(IsRandom, 1, 0), Me.m_AppKey), Nothing, GetCustomDivisionCacheDuration, Callback)
    End Function

    Public Function GetCustomDivisionEnd(ByVal Result As IAsyncResult,
                                         <Out()> ByRef Key As Object,
                                         <Out()> ByRef Response As Sc2RanksResult(Of Division())) As Exception
      Return QueryAndParseEnd(Of Division())(Result, Key, Response)
    End Function

#End Region

#Region "Function ManageCustomDivision"
    'No caching
    Private Shared ReadOnly ManageCustomDivisionCacheDuration As TimeSpan = TimeSpan.FromHours(0)
    
    ''' <summary>
    '''   Allows you to add or remove characters to a custom division, to reduce abuse you are required to use the custom divisions password to manage.
    ''' </summary>
    ''' <param name="CustomDivisionID"></param>
    ''' <param name="Password"></param>
    ''' <param name="Action"></param>
    ''' <param name="Players"></param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks>Broken</remarks>
    Public Function ManageCustomDivision(ByVal CustomDivisionID As Integer,
                                         ByVal Password As String,
                                         ByVal Action As eCustomDivisionAction,
                                         ByVal Players As IEnumerable(Of PlayerBase),
                                         <Out()> ByRef Result As Sc2RanksResult(Of Division())) As Exception
      Result = Nothing
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

        Result = QueryAndParse(Of Division())(String.Format("http://sc2ranks.com/api/custom/{0}/{1}/{2}/{3}.json?appKey={4}", CustomDivisionID, Password, Enums.CustomDivisionActionBuffer.GetValue(Action), Chars, Me.m_AppKey), Nothing, ManageCustomDivisionCacheDuration, Ex)
      End If

      Return Ex
    End Function

    Public Function ManageCustomDivisionBegin(ByVal Key As Object,
                                              ByVal CustomDivisionID As Integer,
                                              ByVal Password As String,
                                              ByVal Action As eCustomDivisionAction,
                                              ByVal Players As IEnumerable(Of PlayerBase),
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

        Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/custom/{0}/{1}/{2}/{3}.json?appKey={4}", CustomDivisionID, Password, Enums.CustomDivisionActionBuffer.GetValue(Action), Chars, Me.m_AppKey), Nothing, ManageCustomDivisionCacheDuration, Callback)
      End If
    End Function

    Public Function ManageCustomDivisionEnd(ByVal Result As IAsyncResult,
                                            <Out()> ByRef Key As Object,
                                            <Out()> ByRef Response As Sc2RanksResult(Of Division())) As Exception
      Return QueryAndParseEnd(Of Division())(Result, Key, Response)
    End Function

#End Region

#Region "Function GetBasePlayers"
    Private Shared ReadOnly GetBasePlayersCharCacheDuration As TimeSpan = TimeSpan.FromHours(1)
    Private Shared ReadOnly GetBasePlayersTeamCacheDuration As TimeSpan = TimeSpan.FromHours(3)
    
    ''' <summary>
    '''   Same as pulling just character information, except you can pull 100 characters at once. The returns are the same, except you get an array of characters rather than a hash.
    ''' </summary>
    ''' <param name="Players"></param>
    ''' <param name="Bracket">
    '''   Optional. Can be <c>Nothing</c>.
    ''' </param>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBasePlayers(ByVal Players As IEnumerable(Of PlayerBase),
                                   ByVal Bracket As Nullable(Of eBracket),
                                   <Out()> ByRef Result As Sc2RanksResult(Of PlayerBase())) As Exception
      Result = Nothing
      Dim Ex As Exception

      If Players Is Nothing Then
        Ex = New NullReferenceException("Players")
      ElseIf (Players.Count = 0) Then
        Ex = New Exception("No players defined.")
      ElseIf (Players.Count > 100) Then
        Ex = New ArgumentException("Too many players requested. Maximum of 100 players allowed at a time.")
      Else
        Dim RequestData As String = Nothing

        Try
          If Bracket.HasValue Then
            Ex = GetSearchRequestBody(Players, True, Bracket.Value, RequestData)
            If (Ex IsNot Nothing) Then Return Ex

            Result = QueryAndParse(Of PlayerBase())(String.Format("http://sc2ranks.com/api/mass/base/teams/?appKey={0}", Me.m_AppKey), RequestData, GetBasePlayersTeamCacheDuration, Ex)
          Else
            Ex = GetSearchRequestBody(Players, False, Nothing, RequestData)
            If (Ex IsNot Nothing) Then Return Ex

            Result = QueryAndParse(Of PlayerBase())(String.Format("http://sc2ranks.com/api/mass/base/char/?appKey={0}", Me.m_AppKey), RequestData, GetBasePlayersCharCacheDuration, Ex)
          End If
        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return Ex
    End Function

    Public Function GetBasePlayersBegin(ByVal Key As Object,
                                        ByVal Players As IEnumerable(Of PlayerBase),
                                        ByVal Bracket As Nullable(Of eBracket),
                                        ByVal Callback As AsyncCallback) As IAsyncResult
      Dim Ex As Exception
      Dim RequestData As String = Nothing

      If Bracket.HasValue Then
        Ex = GetSearchRequestBody(Players, True, Bracket.Value, RequestData)
        If (Ex IsNot Nothing) Then Return Callback.BeginInvoke(Nothing, Nothing, Ex)

        Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/mass/base/teams/?appKey={0}", Me.m_AppKey), RequestData, GetBasePlayersTeamCacheDuration, Callback)
      Else
        Ex = GetSearchRequestBody(Players, False, Nothing, RequestData)
        If (Ex IsNot Nothing) Then Return Callback.BeginInvoke(Nothing, Nothing, Ex)

        Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/mass/base/char/?appKey={0}", Me.m_AppKey), RequestData, GetBasePlayersCharCacheDuration, Callback)
      End If
    End Function

    Public Function GetBasePlayersEnd(ByVal Result As IAsyncResult,
                                      <Out()> ByRef Key As Object,
                                      <Out()> ByRef Response As Sc2RanksResult(Of PlayerBase())) As Exception
      Return QueryAndParseEnd(Of PlayerBase())(Result, Key, Response)
    End Function

#End Region

#Region "Function GetBonusPools"
    Private Shared ReadOnly GetBonusPoolsCacheDuration As TimeSpan = TimeSpan.FromHours(0)

    Public Function GetBonusPools(<Out()> ByRef Result As Sc2RanksResult(Of BonusPool)) As Exception
      Dim Ex As Exception = Nothing

      Result = QueryAndParse(Of BonusPool)(String.Format("http://sc2ranks.com/api/bonus/pool.json?appKey={0}", Me.m_AppKey), Nothing, GetBonusPoolsCacheDuration, Ex)

      Return Ex
    End Function

    Public Function GetBonusPoolsBegin(ByVal Key As Object,
                                       ByVal Callback As AsyncCallback) As IAsyncResult
      Return QueryAndParseBegin(Key, String.Format("http://sc2ranks.com/api/bonus/pool.json?appKey={0}", Me.m_AppKey), Nothing, GetBonusPoolsCacheDuration, Callback)
    End Function

    Public Function GetBonusPoolsEnd(ByVal Result As IAsyncResult,
                                     <Out()> ByRef Key As Object,
                                     <Out()> ByRef Response As Sc2RanksResult(Of BonusPool)) As Exception
      Return QueryAndParseEnd(Of BonusPool)(Result, Key, Response)
    End Function

#End Region

    Private Shared Function GetSearchRequestBody(ByVal Players As IEnumerable(Of PlayerBase),
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

    Protected Function QueryAndParse(Of T As Class)(ByVal URL As String,
                                                    ByVal Post As String,
                                                    ByVal CacheDuration As TimeSpan,
                                                    <Out()> ByRef Ex As Exception) As Sc2RanksResult(Of T)
      Ex = Nothing
      Dim Result As Sc2RanksResult(Of T) = Nothing
      Dim Serializer As DataContractJsonSerializer
      Dim ResponseStream As Stream

      Try
        Result = New Sc2RanksResult(Of T)()
        ResponseStream = Me.CacheOrQuery(URL, Post, Result.CacheDate, Result.CacheDuration)

        'Create serializer
        Serializer = New DataContractJsonSerializer(GetType(T))

        'Deserialize
        Result.Result = DirectCast(Serializer.ReadObject(ResponseStream), T)

        If Me.UseRequestCache Then
          ResponseStream.Position = 0
          Dim sr As New StreamReader(ResponseStream)

          Result.ResponseRaw = sr.ReadToEnd

          Call Me.Cache.AddResponse(URL, Result.ResponseRaw, CacheDuration)
        End If

        'Close stream
        Call ResponseStream.Close()
        Call ResponseStream.Dispose()
      Catch iEx As Exception
        Ex = iEx
      End Try

      Return Result
    End Function

    Private Function CacheOrQuery(ByVal URL As String,
                                  ByVal Post As String,
                                  ByRef CacheDate As DateTime,
                                  ByVal CacheDuration As TimeSpan) As Stream
      Dim ResponseStream As Stream
      Dim Stream As Stream
      Dim Request As WebRequest
      Dim Response As WebResponse
      Dim ResponseRaw As String = Nothing

      'Only get from cache when there is no POST data
      If (String.IsNullOrEmpty(Post)) Then ResponseRaw = Me.Cache.GetResponse(URL, CacheDate, CacheDuration)

      If String.IsNullOrEmpty(ResponseRaw) Then
        Request = HttpWebRequest.Create(URL)

        If (Not String.IsNullOrEmpty(Post)) Then
          Request.Method = "POST"
          Dim Writer As New StreamWriter(Request.GetRequestStream())
          Call Writer.Write(Post)
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
      Public ReadOnly CacheDuration As TimeSpan
      Public ReadOnly FromCacheResponseRaw As String
      Public ReadOnly FromCacheDate As DateTime
      Public ReadOnly FromCacheDuration As TimeSpan

      Public Sub New(ByVal Key As Object,
                     ByVal State As WebRequest,
                     ByVal Request As String,
                     ByVal CacheDuration As TimeSpan,
                     ByVal FromCacheResponseRaw As String,
                     ByVal FromCacheDate As DateTime,
                     ByVal FromCacheDuration As TimeSpan)
        Me.Key = Key
        Me.Request = State
        Me.RequestString = Request
        Me.CacheDuration = CacheDuration
        Me.FromCacheResponseRaw = FromCacheResponseRaw
        Me.FromCacheDate = FromCacheDate
        Me.FromCacheDuration = FromCacheDuration
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

    Public Function QueryAndParseBegin(ByVal Key As Object,
                                       ByVal URL As String,
                                       ByVal Post As String,
                                       ByVal CacheDuration As TimeSpan,
                                       ByVal Callback As AsyncCallback) As IAsyncResult
      Dim Result As IAsyncResult
      Dim Request As WebRequest
      Dim ResponseRaw As String = Nothing
      Dim tCacheDate As DateTime = Nothing
      Dim tCacheDuration As TimeSpan = Nothing

      Try
        If (String.IsNullOrEmpty(Post)) Then ResponseRaw = Me.Cache.GetResponse(URL, tCacheDate, tCacheDuration)

        If (String.IsNullOrEmpty(ResponseRaw)) Then
          Request = HttpWebRequest.Create(URL)

          If (Not String.IsNullOrEmpty(Post)) Then
            Request.Method = "POST"
            Dim Writer As New StreamWriter(Request.GetRequestStream())
            Call Writer.Write(Post)
            Call Writer.Flush()
          End If

          Result = Request.BeginGetResponse(Callback, New AsyncStateWithKey(Key, Request, URL, CacheDuration, Nothing, Nothing, Nothing))
        Else
          Result = Callback.BeginInvoke(New StateAsyncResult(New AsyncStateWithKey(Key, Nothing, URL, CacheDuration, ResponseRaw, tCacheDate, tCacheDuration)), Nothing, Nothing)
        End If
      Catch iEx As Exception
        Result = Callback.BeginInvoke(New StateAsyncResult(iEx), Nothing, Nothing)
      End Try

      Return Result
    End Function

    Public Function QueryAndParseEnd(Of T As Class)(ByVal Result As IAsyncResult,
                                                    <Out()> ByRef Key As Object,
                                                    <Out()> ByRef Response As Sc2RanksResult(Of T)) As Exception
      Key = Nothing
      Response = Nothing
      Dim Ex As Exception = Nothing
      Dim State As AsyncStateWithKey
      Dim ResponseStream As Stream = Nothing
      Dim Stream As Stream
      Dim Serializer As DataContractJsonSerializer

      If (Result Is Nothing) Then
        Ex = New ArgumentNullException("Result")
      Else
        State = TryCast(Result.AsyncState, AsyncStateWithKey)
        Key = State.Key

        If (State Is Nothing) Then
          Ex = New Exception("Invalid AsyncState.")
        Else
          Try
            Response = New Sc2RanksResult(Of T)

            'Create serializer
            Serializer = New DataContractJsonSerializer(GetType(T))

            If (Not String.IsNullOrEmpty(State.FromCacheResponseRaw)) Then
              ResponseStream = New MemoryStream(Encoding.UTF8.GetBytes(State.FromCacheResponseRaw))

              Response.CacheDate = State.FromCacheDate
              Response.CacheDuration = State.FromCacheDuration
            ElseIf (State.Request Is Nothing) Then
              Ex = New ArgumentNullException("Request")
            Else
              Dim WebResponse As WebResponse = State.Request.EndGetResponse(Result)
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

            Response.Result = DirectCast(Serializer.ReadObject(ResponseStream), T)

            ResponseStream.Position = 0
            Dim sr As New StreamReader(ResponseStream)

            Response.ResponseRaw = sr.ReadToEnd

            Call Me.Cache.AddResponse(State.RequestString, Response.ResponseRaw, State.CacheDuration)

            'Close stream
            Call ResponseStream.Close()
            Call ResponseStream.Dispose()

          Catch iEx As Exception
            Ex = iEx
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