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
Imports System.Runtime.Serialization
Imports NuGardt.SC2Ranks.Helper
Imports System.Text

Namespace SC2Ranks.API.Result.Element
  <DataContract()>
  Public Class Sc2RanksCharacterTeamElement
    '{
    '  "url": "http://www.sc2ranks.com/team/am/11004161722/frozz",
    '  "rank_region": "am",
    '  "expansion": "hots",
    '  "league": "grandmaster",
    '  "last_game_at": 1375307693,
    '  "bracket": 1,
    '  "random": false,
    '  "points": 437,
    '  "wins": 20,
    '  "losses": 33,
    '  "win_ratio": 0.3773,
    '  "division": {
    '    "id": "51fad92d4970cf8401000006",
    '    "rank": 199
    '  },
    '  "rankings": {
    '    "world": 1,
    '    "region": 1
    '  },
    '  "characters": [
    '    {
    '      "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '      "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '      "url": "http://www.sc2ranks.com/character/us/4161722/frozz/hots/1v1",
    '      "race": "zerg",
    '      "region": "us",
    '      "bnet_id": 4161722,
    '      "name": "frozz",
    '      "clan": {
    '        "url": "http://www.sc2ranks.com/clan/us/Monty",
    '        "tag": "Monty"
    '      }
    '  }
    '},

    Private m_Url As String
    Private m_RankRegionRaw As String
    Private m_ExpansionRaw As String
    Private m_LeagueRaw As String
    Private m_LastGameAt As Int64
    Private m_BracketRaw As Int32
    Private m_Random As Boolean
    Private m_Points As Int32
    Private m_Wins As Int32
    Private m_Losses As Int32
    Private m_WinRatio As Double
    Private m_Division As Sc2RanksCharacterDivisionElement
    Private m_Rankings As Sc2RanksRankingElement
    Private m_Characters() As Sc2RanksCharacterElement

    Public Sub New()
      Me.m_Url = Nothing
      Me.m_RankRegionRaw = Nothing
      Me.m_ExpansionRaw = Nothing
      Me.m_LeagueRaw = Nothing
      Me.m_LastGameAt = Nothing
      Me.m_BracketRaw = Nothing
      Me.m_Random = Nothing
      Me.m_Points = Nothing
      Me.m_Wins = Nothing
      Me.m_Losses = Nothing
      Me.m_WinRatio = Nothing
      Me.m_Division = Nothing
      Me.m_Rankings = Nothing
      Me.m_Characters = Nothing
    End Sub

    <DataMember(name := "url")>
    Public Property Url As String
      Get
        Return Me.m_Url
      End Get
      Private Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(name := "rank_region")>
    Private Property RankRegionRaw As String
      Get
        Return Me.m_RankRegionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RankRegionRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property RankRegion As eSc2RanksRankRegion
      Get
        Return Enums.RankRegionBuffer.GetEnum(Me.m_RankRegionRaw)
      End Get
    End Property

    <DataMember(name := "expansion")>
    Public Property ExpansionRaw As String
      Get
        Return Me.m_ExpansionRaw
      End Get
      Private Set(ByVal Value As String)
        Me.m_ExpansionRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Expansion As eSc2RanksExpansion
      Get
        Return Enums.ExpansionBuffer.GetEnum(Me.m_ExpansionRaw)
      End Get
    End Property

    <DataMember(name := "league")>
    Private Property LeagueRaw As String
      Get
        Return Me.m_LeagueRaw
      End Get
      Set(ByVal Value As String)
        Me.m_LeagueRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property League As eSc2RanksLeague
      Get
        Return Enums.LeagueBuffer.GetEnum(Me.m_LeagueRaw)
      End Get
    End Property

    <DataMember(name := "last_game_at")>
    Public Property LastGameAtUnixTime As Int64
      Get
        Return Me.m_LastGameAt
      End Get
      Private Set(ByVal Value As Int64)
        Me.m_LastGameAt = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property LastGameAt As DateTime
      Get
        Return New DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Me.m_LastGameAt)
      End Get
    End Property

    <DataMember(name := "bracket")>
    Private Property BracketRaw As Int32
      Get
        Return Me.m_BracketRaw
      End Get
      Set(ByVal Value As Int32)
        Me.m_BracketRaw = Value
      End Set
    End Property

    <DataMember(name := "random")>
    Private Property Random As Boolean
      Get
        Return Me.m_Random
      End Get
      Set(ByVal Value As Boolean)
        Me.m_Random = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Bracket As eSc2RanksBracket
      Get
        Return Enums.BracketRawToBracket(Me.m_BracketRaw, Me.m_Random)
      End Get
    End Property

    <DataMember(name := "points")>
    Public Property Points As Int32
      Get
        Return Me.m_Points
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Points = Value
      End Set
    End Property

    <DataMember(name := "wins")>
    Public Property Wins As Int32
      Get
        Return Me.m_Wins
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Wins = Value
      End Set
    End Property

    <DataMember(name := "losses")>
    Public Property Losses As Int32
      Get
        Return Me.m_Losses
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Losses = Value
      End Set
    End Property

    <DataMember(name := "win_ratio")>
    Public Property WinRatio As Double
      Get
        Return Me.m_WinRatio
      End Get
      Private Set(ByVal Value As Double)
        Me.m_WinRatio = Value
      End Set
    End Property

    <DataMember(name := "division")>
    Public Property Division As Sc2RanksCharacterDivisionElement
      Get
        Return Me.m_Division
      End Get
      Private Set(ByVal Value As Sc2RanksCharacterDivisionElement)
        Me.m_Division = Value
      End Set
    End Property

    <DataMember(name := "rankings")>
    Public Property Rankings As Sc2RanksRankingElement
      Get
        Return Me.m_Rankings
      End Get
      Private Set(ByVal Value As Sc2RanksRankingElement)
        Me.m_Rankings = Value
      End Set
    End Property

    <DataMember(name := "characters")>
    Public Property Characters As Sc2RanksCharacterElement()
      Get
        Return Me.m_Characters
      End Get
      Private Set(ByVal Value As Sc2RanksCharacterElement())
        Me.m_Characters = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("URL: {0}{1}", Me.Url, vbCrLf)
        Call .AppendFormat("Rank Region: {0}{1}", Me.RankRegion.ToString(), vbCrLf)
        Call .AppendFormat("Expansion: {0}{1}", Me.Expansion.ToString(), vbCrLf)
        Call .AppendFormat("League: {0}{1}", Me.League.ToString(), vbCrLf)
        Call .AppendFormat("Last Game At: {0}{1}", Me.LastGameAt.ToString(), vbCrLf)
        Call .AppendFormat("Bracket: {0}{1}", Me.Bracket.ToString(), vbCrLf)
        Call .AppendFormat("Points: {0}{1}", Me.Points.ToString(), vbCrLf)
        Call .AppendFormat("Wins: {0}{1}", Me.Wins.ToString(), vbCrLf)
        Call .AppendFormat("Losses: {0}{1}", Me.Losses.ToString(), vbCrLf)
        Call .AppendFormat("Win Ratio: {0}{1}", Me.WinRatio.ToString(), vbCrLf)
        Call .AppendFormat("Division: {0}{1}", Me.Division.ToString(), vbCrLf)
        Call .AppendFormat("Rankings: {0}{1}", Me.Rankings.ToString(), vbCrLf)
        Call .AppendFormat("Characters: {0}", vbCrLf)
        If (Me.m_Characters IsNot Nothing) Then
          Dim dMax As Int32 = Me.m_Characters.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Character (#{0}): {1}{2}", i.ToString(), Me.m_Characters(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace